// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;
using NetExtender.Exceptions;
using NetExtender.Harmony.Types;
using NetExtender.Harmony.Types.Interfaces;
using NetExtender.Types.Threads;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Harmony.Utilities
{
    public sealed class HarmonyTranspiler : IDisposable, IList<IHarmonyInstruction>
    {
        private SyncRoot SyncRoot { get; } = SyncRoot.Create();
        private static volatile HarmonyTranspiler? Instance;

        private IHarmony? Harmony { get; set; }

        private volatile ILGenerator? _generator;
        private ILGenerator? Generator
        {
            get
            {
                return _generator;
            }
            set
            {
                _generator = value;
            }
        }

        private readonly MethodBase? _method;
        private MethodBase Method
        {
            get
            {
                return _method ?? throw new NotInitializedException();
            }
        }

        private ImmutableArray<IHarmonyInstruction>? _instructions;

        private volatile List<IHarmonyInstruction>? _memory;
        private List<IHarmonyInstruction> Memory
        {
            get
            {
                if (_memory is null && Harmony is null)
                {
                    throw new ObjectDisposedException(nameof(HarmonyTranspiler));
                }

                if (_memory is not null)
                {
                    return _memory;
                }

                Instructions();
                return _memory!;
            }
        }

        public Int32 Count
        {
            get
            {
                return Memory.Count;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return ((ICollection<IHarmonyInstruction>) Memory).IsReadOnly;
            }
        }

        private Thread? Thread { get; set; }

        private ManualResetEventSlim TranspilerSignal { get; } = new ManualResetEventSlim(false);
        private ManualResetEventSlim ProcessSignal { get; } = new ManualResetEventSlim(false);
        private ManualResetEventSlim DisposeSignal { get; } = new ManualResetEventSlim(false);

        private Boolean IsCancel { get; set; }

        private HarmonyTranspiler(String harmony)
        {
            if (String.IsNullOrEmpty(harmony))
            {
                throw new ArgumentNullOrEmptyStringException(harmony, nameof(harmony));
            }

            Harmony = new HarmonyWrapper(harmony);
        }

        public HarmonyTranspiler(MethodBase method)
            : this(HarmonyUtilities.NetExtender, method)
        {
        }

        public HarmonyTranspiler(String harmony, MethodBase method)
            : this(harmony)
        {
            _method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public HarmonyTranspiler(IHarmony harmony, MethodBase method)
        {
            Harmony = harmony ?? throw new ArgumentNullException(nameof(harmony));
            _method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public HarmonyTranspiler(Delegate @delegate)
            : this(HarmonyUtilities.NetExtender, @delegate)
        {
        }

        public HarmonyTranspiler(String harmony, Delegate @delegate)
            : this(harmony)
        {
            _method = @delegate is not null ? @delegate.Method : throw new ArgumentNullException(nameof(@delegate));
        }

        public HarmonyTranspiler(IHarmony harmony, Delegate @delegate)
        {
            Harmony = harmony ?? throw new ArgumentNullException(nameof(harmony));
            _method = @delegate is not null ? @delegate.Method : throw new ArgumentNullException(nameof(@delegate));
        }

        public void Apply()
        {
            Dispose();
        }

        public Boolean Contains(IHarmonyInstruction item)
        {
            return Memory.Contains(item);
        }

        public Int32 IndexOf(IHarmonyInstruction item)
        {
            return Memory.IndexOf(item);
        }

        public void Add(IHarmonyInstruction item)
        {
            Memory.Add(item);
        }

        public IHarmonyInstruction Add(OpCode code)
        {
            IHarmonyInstruction instruction = new CodeInstructionWrapper(code);
            Memory.Add(instruction);
            return instruction;
        }

        public IHarmonyInstruction Add(OpCode code, Object operand)
        {
            IHarmonyInstruction instruction = new CodeInstructionWrapper(code, operand);
            Memory.Add(instruction);
            return instruction;
        }

        public Label NewLabel()
        {
            if (Harmony is null && Generator is null)
            {
                throw new ObjectDisposedException(nameof(HarmonyTranspiler));
            }

            if (Generator is null)
            {
                Instructions();
            }

            return Generator!.DefineLabel();
        }

        public void Insert(Int32 index, IHarmonyInstruction item)
        {
            Memory.Insert(index, item);
        }

        public Boolean Remove(IHarmonyInstruction item)
        {
            return Memory.Remove(item);
        }

        public void RemoveAt(Int32 index)
        {
            Memory.RemoveAt(index);
        }

        public void Clear()
        {
            Memory.Clear();
        }

        public void CopyTo(IHarmonyInstruction[] array, Int32 index)
        {
            Memory.CopyTo(array, index);
        }

        public IHarmonyInstruction this[Int32 index]
        {
            get
            {
                return Memory[index];
            }
            set
            {
                Memory[index] = value;
            }
        }

        private static IEnumerable<IHarmonyInstruction> InstructionsTranspiler(IEnumerable<IHarmonyInstruction> instructions, ILGenerator generator)
        {
            if (Instance is null)
            {
                throw new InvalidOperationException();
            }

            Instance._instructions = instructions.ToImmutableArray();

            List<IHarmonyInstruction> memory = new List<IHarmonyInstruction>(Instance._instructions.Value.Length);
            memory.AddRange(Instance._instructions);
            Instance._memory = memory;
            Instance._generator = generator;

            Instance.TranspilerSignal.Set();
            Instance.ProcessSignal.Wait();
            Instance.DisposeSignal.Wait();

            if (Instance.IsCancel || Instance._instructions!.Value.SequenceEqual(Instance._memory))
            {
                throw new SuccessfulOperationException();
            }

            return Instance._memory;
        }

        public ImmutableArray<IHarmonyInstruction> Instructions()
        {
            if (_instructions is not null)
            {
                return _instructions.Value;
            }

            using IEnumerator<IHarmonyInstruction> enumerator = GetEnumerator();

            List<IHarmonyInstruction> instructions = new List<IHarmonyInstruction>();
            while (enumerator.MoveNext())
            {
                instructions.Add(enumerator.Current);
            }

            _instructions = instructions.ToImmutableArray();
            return _instructions.Value;
        }

        public IEnumerator<IHarmonyInstruction> GetEnumerator()
        {
            if (Harmony is null)
            {
                throw new ObjectDisposedException(nameof(HarmonyTranspiler));
            }

            lock (SyncRoot)
            {
                if (_memory is not null)
                {
                    foreach (IHarmonyInstruction instruction in _memory)
                    {
                        yield return instruction;
                    }

                    yield break;
                }

                Instance = this;

                Thread = new Thread<IHarmony>(harmony =>
                {
                    harmony.Transpiler(InstructionsTranspiler, Method);
                });

                Thread.Start(Harmony);
                TranspilerSignal.Wait();

                foreach (IHarmonyInstruction instruction in _memory!)
                {
                    yield return instruction;
                }

                ProcessSignal.Set();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(Boolean disposing)
        {
            if (Harmony is null)
            {
                return;
            }

            IsCancel = !disposing;
            DisposeSignal.Set();
            Thread?.Join();

            Harmony = null;
            Instance = null;
            Thread = null;

            TranspilerSignal.Dispose();
            ProcessSignal.Dispose();
            DisposeSignal.Dispose();
        }

        ~HarmonyTranspiler()
        {
            Dispose(false);
        }
    }

    public static partial class HarmonyUtilities
    {
        internal static IUnsafeHarmony NetExtender { get; } = new HarmonyWrapper(nameof(NetExtender));

        [field: ThreadStatic]
        private static ImmutableArray<IHarmonyInstruction> Memory { get; set; }

        [field: ThreadStatic]
        internal static ChangeInstantiationMemory InstantiationMemory { get; set; }

        public static Type[]? Self
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return null;
            }
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class Transpilers
        {
            public static IHarmonyMethod InstructionsTranspiler { get; } = new HarmonyMethodWrapper(HarmonyUtilities.InstructionsTranspiler);
            public static IHarmonyMethod InstantiationTranspiler { get; } = new HarmonyMethodWrapper(HarmonyUtilities.InstantiationTranspiler);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? Transpiler(this MethodBase original, HarmonySignatureUtilities.Transpiler transpiler)
        {
            return Transpiler(NetExtender, transpiler, original);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? Transpiler(this IHarmony harmony, HarmonySignatureUtilities.Transpiler transpiler, MethodBase original)
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (transpiler is null)
            {
                throw new ArgumentNullException(nameof(transpiler));
            }

            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            IHarmonyMethod method = new HarmonyMethodWrapper(transpiler);
            return Transpiler(harmony, method, original);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? Transpiler(this MethodBase original, HarmonySignatureUtilities.GeneratorTranspiler transpiler)
        {
            return Transpiler(NetExtender, transpiler, original);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? Transpiler(this IHarmony harmony, HarmonySignatureUtilities.GeneratorTranspiler transpiler, MethodBase original)
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (transpiler is null)
            {
                throw new ArgumentNullException(nameof(transpiler));
            }

            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            IHarmonyMethod method = new HarmonyMethodWrapper(transpiler);
            return Transpiler(harmony, method, original);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? Transpiler(this MethodBase original, IHarmonyMethod transpiler)
        {
            return Transpiler(NetExtender, transpiler, original);
        }

        public static MethodInfo? Transpiler(this IHarmony harmony, IHarmonyMethod transpiler, MethodBase original)
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (transpiler is null)
            {
                throw new ArgumentNullException(nameof(transpiler));
            }

            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            try
            {
                return harmony.Patch(original, transpiler: transpiler);
            }
            catch (TargetInvocationException exception) when (exception.InnerException is SuccessfulOperationException)
            {
                return original as MethodInfo;
            }
            catch (SuccessfulOperationException)
            {
                return original as MethodInfo;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<KeyValuePair<T, MethodInfo>> Transpiler<T>(this HarmonySignatureUtilities.Transpiler transpiler, IEnumerable<T?>? originals) where T : MethodBase
        {
            return Transpiler(NetExtender, transpiler, originals);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static ImmutableArray<KeyValuePair<T, MethodInfo>> Transpiler<T>(this IHarmony harmony, HarmonySignatureUtilities.Transpiler transpiler, IEnumerable<T?>? originals) where T : MethodBase
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (transpiler is null)
            {
                throw new ArgumentNullException(nameof(transpiler));
            }

            if (originals is null)
            {
                return ImmutableArray<KeyValuePair<T, MethodInfo>>.Empty;
            }

            List<KeyValuePair<T, MethodInfo>> result = new List<KeyValuePair<T, MethodInfo>>(8);
            List<Exception> exceptions = new List<Exception>(4);

            foreach (T? original in originals)
            {
                try
                {
                    if (original is not null && Transpiler(harmony, transpiler, original) is { } method)
                    {
                        result.Add(new KeyValuePair<T, MethodInfo>(original, method));
                    }
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }

            return exceptions.Count <= 0 ? result.ToImmutableArray() : throw new AggregateException<ImmutableArray<KeyValuePair<T, MethodInfo>>>(result.ToImmutableArray(), exceptions);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<KeyValuePair<T, MethodInfo>> Transpiler<T>(this HarmonySignatureUtilities.GeneratorTranspiler transpiler, IEnumerable<T?>? originals) where T : MethodBase
        {
            return Transpiler(NetExtender, transpiler, originals);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static ImmutableArray<KeyValuePair<T, MethodInfo>> Transpiler<T>(this IHarmony harmony, HarmonySignatureUtilities.GeneratorTranspiler transpiler, IEnumerable<T?>? originals) where T : MethodBase
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (transpiler is null)
            {
                throw new ArgumentNullException(nameof(transpiler));
            }

            if (originals is null)
            {
                return ImmutableArray<KeyValuePair<T, MethodInfo>>.Empty;
            }

            List<KeyValuePair<T, MethodInfo>> result = new List<KeyValuePair<T, MethodInfo>>(8);
            List<Exception> exceptions = new List<Exception>(4);

            foreach (T? original in originals)
            {
                try
                {
                    if (original is not null && Transpiler(harmony, transpiler, original) is { } method)
                    {
                        result.Add(new KeyValuePair<T, MethodInfo>(original, method));
                    }
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }

            return exceptions.Count <= 0 ? result.ToImmutableArray() : throw new AggregateException<ImmutableArray<KeyValuePair<T, MethodInfo>>>(result.ToImmutableArray(), exceptions);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<KeyValuePair<T, MethodInfo>> Transpiler<T>(this IHarmonyMethod transpiler, IEnumerable<T?>? originals) where T : MethodBase
        {
            return Transpiler(NetExtender, transpiler, originals);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static ImmutableArray<KeyValuePair<T, MethodInfo>> Transpiler<T>(this IHarmony harmony, IHarmonyMethod transpiler, IEnumerable<T?>? originals) where T : MethodBase
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (transpiler is null)
            {
                throw new ArgumentNullException(nameof(transpiler));
            }

            if (originals is null)
            {
                return ImmutableArray<KeyValuePair<T, MethodInfo>>.Empty;
            }

            List<KeyValuePair<T, MethodInfo>> result = new List<KeyValuePair<T, MethodInfo>>(8);
            List<Exception> exceptions = new List<Exception>(4);

            foreach (T? original in originals)
            {
                try
                {
                    if (original is not null && Transpiler(harmony, transpiler, original) is { } method)
                    {
                        result.Add(new KeyValuePair<T, MethodInfo>(original, method));
                    }
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }

            return exceptions.Count <= 0 ? result.ToImmutableArray() : throw new AggregateException<ImmutableArray<KeyValuePair<T, MethodInfo>>>(result.ToImmutableArray(), exceptions);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<IHarmonyInstruction> Instructions(this MethodBase method)
        {
            return Instructions(NetExtender, method);
        }

        public static ImmutableArray<IHarmonyInstruction> Instructions(this IHarmony harmony, MethodBase method)
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            try
            {
                Transpiler(harmony, Transpilers.InstructionsTranspiler, method);
                return Memory;
            }
            catch (TargetInvocationException exception) when (exception.InnerException is SuccessfulOperationException)
            {
                return Memory;
            }
            catch (SuccessfulOperationException)
            {
                return Memory;
            }
            finally
            {
                Memory = Memory.Clear();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Instructions(this MethodInfo method, HarmonySignatureUtilities.Transpiler? transpiler)
        {
            return Instructions(NetExtender, transpiler, method);
        }

        public static Boolean Instructions(this IHarmony harmony, HarmonySignatureUtilities.Transpiler? transpiler, MethodInfo method)
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            if (transpiler is null)
            {
                return false;
            }

            try
            {
                transpiler(Instructions(harmony, method));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean Instructions<T>(this MethodBase method, TryConverter<ImmutableArray<IHarmonyInstruction>, T>? transpiler, [MaybeNullWhen(false)] out T result)
        {
            return Instructions(NetExtender, transpiler, method, out result);
        }

        public static Boolean Instructions<T>(this IHarmony harmony, TryConverter<ImmutableArray<IHarmonyInstruction>, T>? transpiler, MethodBase method, [MaybeNullWhen(false)] out T result)
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            result = default;
            return transpiler is not null && transpiler(Instructions(harmony, method), out result);
        }

        private static IEnumerable<IHarmonyInstruction> InstructionsTranspiler(IEnumerable<IHarmonyInstruction> instructions)
        {
            Memory = instructions.ToImmutableArray();
            throw new SuccessfulOperationException();
        }

        private static ImmutableHashSet<Type?>? Allow(MemberInfo member, params Type?[]? declaring)
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            return declaring switch
            {
                null => ImmutableHashSet<Type?>.Empty.Add(member.DeclaringType),
                { Length: <= 0 } => null,
                _ => ImmutableHashSet.Create(declaring)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IEnumerable<T> Members<T>(this IHarmony harmony, MethodBase method) where T : MemberInfo
        {
            return Members<T>(harmony, method, Type.EmptyTypes);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IEnumerable<T> Members<T>(this IHarmony harmony, MethodBase method, params Type?[]? declaring) where T : MemberInfo
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            ImmutableArray<IHarmonyInstruction> instructions;

            try
            {
                instructions = harmony.Instructions(method);
            }
            catch (Exception)
            {
                yield break;
            }

            ImmutableHashSet<Type?>? allow = Allow(method, declaring);
            ConcurrentHashSet<T> set = new ConcurrentHashSet<T>();
            foreach (IHarmonyInstruction instruction in instructions)
            {
                if (instruction.Operand is T member && allow?.Contains(member.DeclaringType) is not false && set.Add(member))
                {
                    yield return member;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MemberInfo> Members(this MethodBase method)
        {
            return Members(NetExtender, method);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MemberInfo> Members(this IHarmony harmony, MethodBase method)
        {
            return Members<MemberInfo>(harmony, method);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MemberInfo> Members(this MethodBase method, params Type?[]? declaring)
        {
            return Members(NetExtender, method, declaring);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MemberInfo> Members(this IHarmony harmony, MethodBase method, params Type?[]? declaring)
        {
            return Members<MemberInfo>(harmony, method, declaring);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> Fields(this MethodBase method)
        {
            return Fields(NetExtender, method);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> Fields(this IHarmony harmony, MethodBase method)
        {
            return Members<FieldInfo>(harmony, method);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> Fields(this MethodBase method, params Type?[]? declaring)
        {
            return Fields(NetExtender, method, declaring);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> Fields(this IHarmony harmony, MethodBase method, params Type?[]? declaring)
        {
            return Members<FieldInfo>(harmony, method, declaring);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> Properties(this MethodBase method)
        {
            return Properties(NetExtender, method);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> Properties(this IHarmony harmony, MethodBase method)
        {
            return Members<PropertyInfo>(harmony, method);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> Properties(this MethodBase method, params Type?[]? declaring)
        {
            return Properties(NetExtender, method, declaring);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> Properties(this IHarmony harmony, MethodBase method, params Type?[]? declaring)
        {
            return Members<PropertyInfo>(harmony, method, declaring);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<EventInfo> Events(this MethodBase method)
        {
            return Events(NetExtender, method);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<EventInfo> Events(this IHarmony harmony, MethodBase method)
        {
            return Members<EventInfo>(harmony, method);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<EventInfo> Events(this MethodBase method, params Type?[]? declaring)
        {
            return Events(NetExtender, method, declaring);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<EventInfo> Events(this IHarmony harmony, MethodBase method, params Type?[]? declaring)
        {
            return Members<EventInfo>(harmony, method, declaring);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean IsSpecialMethod(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            ReadOnlySpan<Char> name = method.Name;
            return name.StartsWith("get_") || name.StartsWith("set_") || name.StartsWith("init_") || name.StartsWith("add_") || name.StartsWith("remove_");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> Methods(this MethodBase method)
        {
            return Methods(NetExtender, method);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> Methods(this IHarmony harmony, MethodBase method)
        {
            return Members<MethodInfo>(harmony, method);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> Methods(this MethodBase method, params Type?[]? declaring)
        {
            return Methods(NetExtender, method, declaring);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> Methods(this IHarmony harmony, MethodBase method, params Type?[]? declaring)
        {
            return Members<MethodInfo>(harmony, method, declaring);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> Methods(this MethodBase method, Boolean? special)
        {
            return Methods(NetExtender, method, special);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> Methods(this IHarmony harmony, MethodBase method, Boolean? special)
        {
            return special switch
            {
                null => Methods(harmony, method),
                false => Methods(harmony, method).WhereNot(IsSpecialMethod),
                true => Methods(harmony, method).Where(IsSpecialMethod)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> Methods(this MethodBase method, Boolean? special, params Type?[]? declaring)
        {
            return Methods(NetExtender, method, special, declaring);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> Methods(this IHarmony harmony, MethodBase method, Boolean? special, params Type?[]? declaring)
        {
            return special switch
            {
                null => Methods(harmony, method, declaring),
                false => Methods(harmony, method, declaring).WhereNot(IsSpecialMethod),
                true => Methods(harmony, method, declaring).Where(IsSpecialMethod)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ConstructorInfo> Constructors(this MethodBase method)
        {
            return Constructors(NetExtender, method);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ConstructorInfo> Constructors(this IHarmony harmony, MethodBase method)
        {
            return Members<ConstructorInfo>(harmony, method);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ConstructorInfo> Constructors(this MethodBase method, params Type?[]? declaring)
        {
            return Constructors(NetExtender, method, declaring);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ConstructorInfo> Constructors(this IHarmony harmony, MethodBase method, params Type?[]? declaring)
        {
            return Members<ConstructorInfo>(harmony, method, declaring);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TMember> Extract<T, TMember>(this T method, params TMember[]? contains) where T : MethodBase where TMember : MemberInfo
        {
            return Extract(method, (IEnumerable<TMember>?) contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TMember> Extract<T, TMember>(this T method, IEnumerable<TMember>? contains) where T : MethodBase where TMember : MemberInfo
        {
            return Extract(NetExtender, method, contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TMember> Extract<T, TMember>(this IHarmony harmony, T method, params TMember[]? contains) where T : MethodBase where TMember : MemberInfo
        {
            return Extract(harmony, method, (IEnumerable<TMember>?) contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TMember> Extract<T, TMember>(this IHarmony harmony, T method, IEnumerable<TMember>? contains) where T : MethodBase where TMember : MemberInfo
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            ImmutableHashSet<TMember>? set = contains?.AsImmutableHashSet();
            return Extract(harmony, method, set);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<TMember> Extract<T, TMember>(this IHarmony harmony, T method, ImmutableHashSet<TMember>? contains) where T : MethodBase where TMember : MemberInfo
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            if (contains is not { Count: > 0 })
            {
                yield break;
            }

            foreach (IHarmonyInstruction instruction in harmony.Instructions(method))
            {
                if (instruction.Operand is TMember member && contains.Contains(member))
                {
                    yield return member;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<T, ImmutableHashSet<TMember>>> Extract<T, TMember>(this IEnumerable<T?> methods, params TMember[]? contains) where T : MethodBase where TMember : MemberInfo
        {
            return Extract(methods, (IEnumerable<TMember>?) contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<T, ImmutableHashSet<TMember>>> Extract<T, TMember>(this IEnumerable<T?> methods, IEnumerable<TMember>? contains) where T : MethodBase where TMember : MemberInfo
        {
            return Extract(NetExtender, methods, contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<T, ImmutableHashSet<TMember>>> Extract<T, TMember>(this IHarmony harmony, IEnumerable<T?> methods, params TMember[]? contains) where T : MethodBase where TMember : MemberInfo
        {
            return Extract(harmony, methods, (IEnumerable<TMember>?) contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<T, ImmutableHashSet<TMember>>> Extract<T, TMember>(this IHarmony harmony, IEnumerable<T?> methods, IEnumerable<TMember>? contains) where T : MethodBase where TMember : MemberInfo
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (methods is null)
            {
                throw new ArgumentNullException(nameof(methods));
            }

            ImmutableHashSet<TMember>? set = contains?.AsImmutableHashSet();
            return Extract(harmony, methods, set);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IEnumerable<KeyValuePair<T, ImmutableHashSet<TMember>>> Extract<T, TMember>(this IHarmony harmony, IEnumerable<T?> methods, ImmutableHashSet<TMember>? contains) where T : MethodBase where TMember : MemberInfo
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (methods is null)
            {
                throw new ArgumentNullException(nameof(methods));
            }

            if (contains is not { Count: > 0 })
            {
                yield break;
            }

            foreach (T? method in methods)
            {
                if (method is not null && Extract(harmony, method, contains).ToImmutableHashSet() is { Count: > 0 } result)
                {
                    yield return new KeyValuePair<T, ImmutableHashSet<TMember>>(method, result);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<MethodInfo, ImmutableHashSet<TMember>>> Methods<TMember>(this Type type, params TMember[]? contains) where TMember : MemberInfo
        {
            return Methods(type, (IEnumerable<TMember>?) contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<MethodInfo, ImmutableHashSet<TMember>>> Methods<TMember>(this Type type, IEnumerable<TMember>? contains) where TMember : MemberInfo
        {
            return Methods(NetExtender, type, contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<MethodInfo, ImmutableHashSet<TMember>>> Methods<TMember>(this IHarmony harmony, Type type, params TMember[]? contains) where TMember : MemberInfo
        {
            return Methods(harmony, type, (IEnumerable<TMember>?) contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<MethodInfo, ImmutableHashSet<TMember>>> Methods<TMember>(this IHarmony harmony, Type type, IEnumerable<TMember>? contains) where TMember : MemberInfo
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Extract(harmony, type.GetMethods(), contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<MethodInfo, ImmutableHashSet<TMember>>> Methods<TMember>(this Type type, BindingFlags binding, params TMember[]? contains) where TMember : MemberInfo
        {
            return Methods(type, binding, (IEnumerable<TMember>?) contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<MethodInfo, ImmutableHashSet<TMember>>> Methods<TMember>(this Type type, BindingFlags binding, IEnumerable<TMember>? contains) where TMember : MemberInfo
        {
            return Methods(NetExtender, type, binding, contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<MethodInfo, ImmutableHashSet<TMember>>> Methods<TMember>(this IHarmony harmony, Type type, BindingFlags binding, params TMember[]? contains) where TMember : MemberInfo
        {
            return Methods(harmony, type, binding, (IEnumerable<TMember>?) contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<MethodInfo, ImmutableHashSet<TMember>>> Methods<TMember>(this IHarmony harmony, Type type, BindingFlags binding, IEnumerable<TMember>? contains) where TMember : MemberInfo
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Extract(harmony, type.GetMethods(binding), contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ConstructorInfo, ImmutableHashSet<TMember>>> Constructors<TMember>(this Type type, params TMember[]? contains) where TMember : MemberInfo
        {
            return Constructors(type, (IEnumerable<TMember>?) contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ConstructorInfo, ImmutableHashSet<TMember>>> Constructors<TMember>(this Type type, IEnumerable<TMember>? contains) where TMember : MemberInfo
        {
            return Constructors(NetExtender, type, contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ConstructorInfo, ImmutableHashSet<TMember>>> Constructors<TMember>(this IHarmony harmony, Type type, params TMember[]? contains) where TMember : MemberInfo
        {
            return Constructors(harmony, type, (IEnumerable<TMember>?) contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ConstructorInfo, ImmutableHashSet<TMember>>> Constructors<TMember>(this IHarmony harmony, Type type, IEnumerable<TMember>? contains) where TMember : MemberInfo
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Extract(harmony, type.GetConstructors(), contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ConstructorInfo, ImmutableHashSet<TMember>>> Constructors<TMember>(this Type type, BindingFlags binding, params TMember[]? contains) where TMember : MemberInfo
        {
            return Constructors(type, binding, (IEnumerable<TMember>?) contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ConstructorInfo, ImmutableHashSet<TMember>>> Constructors<TMember>(this Type type, BindingFlags binding, IEnumerable<TMember>? contains) where TMember : MemberInfo
        {
            return Constructors(NetExtender, type, binding, contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ConstructorInfo, ImmutableHashSet<TMember>>> Constructors<TMember>(this IHarmony harmony, Type type, BindingFlags binding, params TMember[]? contains) where TMember : MemberInfo
        {
            return Constructors(harmony, type, binding, (IEnumerable<TMember>?) contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ConstructorInfo, ImmutableHashSet<TMember>>> Constructors<TMember>(this IHarmony harmony, Type type, BindingFlags binding, IEnumerable<TMember>? contains) where TMember : MemberInfo
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Extract(harmony, type.GetConstructors(binding), contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? Always(this MethodInfo original, Object? @return)
        {
            return Always(NetExtender, original, @return);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? Always(this IHarmony harmony, MethodInfo original, Object? @return)
        {
            return Always(harmony, original, @return, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? Always(this MethodInfo original, Object? @return, params Object?[]? values)
        {
            return Always(NetExtender, original, @return, values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? Always(this IHarmony harmony, MethodInfo original, Object? @return, params Object?[]? values)
        {
            return AlwaysPatch.Apply(harmony, original, @return, values);
        }

        internal readonly struct ChangeInstantiationMemory
        {
            public Type New { get; }
            public Type Old { get; }
            public MethodBase Method { get; }

            public ChangeInstantiationMemory(Type @new, Type old, MethodBase method)
            {
                New = @new ?? throw new ArgumentNullException(nameof(@new));
                Old = old ?? throw new ArgumentNullException(nameof(old));
                Method = method ?? throw new ArgumentNullException(nameof(method));

                if (!New.IsAssignableTo(Old))
                {
                    throw new ArgumentException($"The type '{New.FullName}' must be assignable to '{Old.FullName}'.");
                }
            }
        }

        // ReSharper disable once CognitiveComplexity
        internal static ConstructorInfo? FindConstructorForInstantiation(Type @new, ConstructorInfo old)
        {
            if (@new is null)
            {
                throw new ArgumentNullException(nameof(@new));
            }

            if (old is null)
            {
                throw new ArgumentNullException(nameof(old));
            }

            Type[] oldtypes = old.GetParameterTypes();
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance;
            if (@new.GetConstructor(binding, oldtypes) is { } result)
            {
                return result;
            }

            ConstructorInfo[] constructors = @new.GetConstructors(binding);
            foreach (ConstructorInfo constructor in constructors)
            {
                Type[] newtypes = constructor.GetParameterTypes();

                if (newtypes.Length != oldtypes.Length)
                {
                    continue;
                }

                for (Int32 i = 0; i < oldtypes.Length; i++)
                {
                    if (!oldtypes[i].IsAssignableTo(newtypes[i]))
                    {
                        goto next;
                    }
                }

                return constructor;
                next:;
            }

            return null;
        }

        private static IEnumerable<IHarmonyInstruction> InstantiationTranspiler(IEnumerable<IHarmonyInstruction> instructions)
        {
            if (InstantiationMemory.New is not { } @new || InstantiationMemory.Old is not { } old || InstantiationMemory.Method is null)
            {
                throw new InvalidOperationException();
            }

            Boolean any = false;
            foreach (IHarmonyInstruction instruction in instructions)
            {
                if (instruction.Operand is not ConstructorInfo constructor || constructor.DeclaringType != old)
                {
                    yield return instruction;
                    continue;
                }

                constructor = FindConstructorForInstantiation(@new, constructor) ?? throw new MissingMemberException($"Could not find a constructor for '{@new}'.");
                instruction.Operand = constructor;
                yield return instruction;
                any = true;
            }

            if (!any)
            {
                throw new SuccessfulOperationException();
            }
        }

        private static MethodInfo? ChangeInstantiation(this IHarmony harmony, MethodBase original, IHarmonyMethod transpiler, Type old, Type @new)
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            if (old is null)
            {
                throw new ArgumentNullException(nameof(old));
            }

            if (@new is null)
            {
                throw new ArgumentNullException(nameof(@new));
            }

            try
            {
                InstantiationMemory = new ChangeInstantiationMemory(@new, old, original);
                return harmony.Transpiler(transpiler, original);
            }
            catch (TargetInvocationException exception) when (exception.InnerException is SuccessfulOperationException)
            {
                return original as MethodInfo;
            }
            catch (SuccessfulOperationException)
            {
                return original as MethodInfo;
            }
            catch (TargetInvocationException exception) when (exception.InnerException is MissingMethodException or NeverOperationException)
            {
                throw exception.InnerException;
            }
            finally
            {
                InstantiationMemory = default;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? ChangeInstantiation(this MethodBase original, Type old, Type @new)
        {
            return ChangeInstantiation(NetExtender, original, old, @new);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? ChangeInstantiation(this IHarmony harmony, MethodBase original, Type old, Type @new)
        {
            return ChangeInstantiation(harmony, original, Transpilers.InstantiationTranspiler, old, @new);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? ChangeInstantiation<TOld, TNew>(this MethodBase original)
        {
            return ChangeInstantiation<TOld, TNew>(NetExtender, original);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? ChangeInstantiation<TOld, TNew>(this IHarmony harmony, MethodBase original)
        {
            return ChangeInstantiation(harmony, original, typeof(TOld), typeof(TNew));
        }

        public static HarmonyLib.ExceptionBlock ToBlock(this HarmonyExceptionBlock exception)
        {
            return new HarmonyLib.ExceptionBlock(exception.Block.ToBlockType(), exception.Type!);
        }

        public static HarmonyExceptionBlock ToHarmonyBlock(this HarmonyLib.ExceptionBlock exception)
        {
            return new HarmonyExceptionBlock(exception.catchType, exception.blockType.FromBlockType());
        }

        public static HarmonyLib.ExceptionBlockType ToBlockType(this HarmonyExceptionBlockType value)
        {
            return value switch
            {
                HarmonyExceptionBlockType.BeginExceptionBlock => HarmonyLib.ExceptionBlockType.BeginExceptionBlock,
                HarmonyExceptionBlockType.BeginCatchBlock => HarmonyLib.ExceptionBlockType.BeginCatchBlock,
                HarmonyExceptionBlockType.BeginExceptFilterBlock => HarmonyLib.ExceptionBlockType.BeginExceptFilterBlock,
                HarmonyExceptionBlockType.BeginFaultBlock => HarmonyLib.ExceptionBlockType.BeginFaultBlock,
                HarmonyExceptionBlockType.BeginFinallyBlock => HarmonyLib.ExceptionBlockType.BeginFinallyBlock,
                HarmonyExceptionBlockType.EndExceptionBlock => HarmonyLib.ExceptionBlockType.EndExceptionBlock,
                _ => throw new EnumUndefinedOrNotSupportedException<HarmonyExceptionBlockType>(value, nameof(value), null)
            };
        }

        public static HarmonyExceptionBlockType FromBlockType(this HarmonyLib.ExceptionBlockType value)
        {
            return value switch
            {
                HarmonyLib.ExceptionBlockType.BeginExceptionBlock => HarmonyExceptionBlockType.BeginExceptionBlock,
                HarmonyLib.ExceptionBlockType.BeginCatchBlock => HarmonyExceptionBlockType.BeginCatchBlock,
                HarmonyLib.ExceptionBlockType.BeginExceptFilterBlock => HarmonyExceptionBlockType.BeginExceptFilterBlock,
                HarmonyLib.ExceptionBlockType.BeginFaultBlock => HarmonyExceptionBlockType.BeginFaultBlock,
                HarmonyLib.ExceptionBlockType.BeginFinallyBlock => HarmonyExceptionBlockType.BeginFinallyBlock,
                HarmonyLib.ExceptionBlockType.EndExceptionBlock => HarmonyExceptionBlockType.EndExceptionBlock,
                _ => throw new EnumUndefinedOrNotSupportedException<HarmonyLib.ExceptionBlockType>(value, nameof(value), null)
            };
        }
    }
}