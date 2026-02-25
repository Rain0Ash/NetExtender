using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NetExtender.Harmony.Utilities;
using NetExtender.Harmony.Types.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Harmony.Types
{
    public readonly struct HarmonyWrapper : IEquatableStruct<HarmonyWrapper>, IUnsafeHarmony, IEquatable<HarmonyLib.Harmony>
    {
        public static implicit operator HarmonyWrapper(HarmonyLib.Harmony value)
        {
            return new HarmonyWrapper(value);
        }

        public static implicit operator HarmonyLib.Harmony(HarmonyWrapper value)
        {
            return value.Harmony;
        }

        public static Boolean operator ==(HarmonyWrapper first, HarmonyWrapper second)
        {
            return first.Harmony == second.Harmony;
        }

        public static Boolean operator !=(HarmonyWrapper first, HarmonyWrapper second)
        {
            return first.Harmony != second.Harmony;
        }

        private readonly HarmonyLib.Harmony? _harmony;
        private HarmonyLib.Harmony Harmony
        {
            get
            {
                return _harmony ?? HarmonyUtilities.NetExtender.Harmony;
            }
        }

        HarmonyLib.Harmony IUnsafeHarmony.Harmony
        {
            get
            {
                return Harmony;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return _harmony is null;
            }
        }

        public HarmonyWrapper(String? identifier)
            : this(identifier is not null ? new HarmonyLib.Harmony(identifier) : HarmonyUtilities.NetExtender.Harmony)
        {
        }

        public HarmonyWrapper(HarmonyLib.Harmony? harmony)
        {
            _harmony = harmony ?? HarmonyUtilities.NetExtender.Harmony;
        }

        private static HarmonyLib.HarmonyMethod? Unpack(IHarmonyMethod? method)
        {
            return method switch
            {
                null => null,
                IUnsafeHarmonyMethod @unsafe => @unsafe.Method,
                _ => throw new NoHarmonyException($"Method must be an instance of {nameof(IUnsafeHarmonyMethod)}.")
            };
        }

        public MethodInfo Patch(MethodBase original, IHarmonyMethod? prefix = null, IHarmonyMethod? postfix = null, IHarmonyMethod? transpiler = null, IHarmonyMethod? finalizer = null)
        {
            return Harmony.Patch(original, Unpack(prefix), Unpack(postfix), Unpack(transpiler), Unpack(finalizer));
        }

        public void Unpatch(MethodBase original, MethodInfo patch)
        {
            Harmony.Unpatch(original, patch);
        }

        public MethodInfo? Transpiler(HarmonySignatureUtilities.Transpiler transpiler, MethodBase original)
        {
            return HarmonyUtilities.Transpiler(this, transpiler, original);
        }

        public MethodInfo? Transpiler(HarmonySignatureUtilities.GeneratorTranspiler transpiler, MethodBase original)
        {
            return HarmonyUtilities.Transpiler(this, transpiler, original);
        }

        public MethodInfo? Transpiler(IHarmonyMethod transpiler, MethodBase original)
        {
            return HarmonyUtilities.Transpiler(this, transpiler, original);
        }

        public ImmutableArray<KeyValuePair<T, MethodInfo>> Transpiler<T>(HarmonySignatureUtilities.Transpiler transpiler, IEnumerable<T?>? originals) where T : MethodBase
        {
            return HarmonyUtilities.Transpiler(this, transpiler, originals);
        }

        public ImmutableArray<KeyValuePair<T, MethodInfo>> Transpiler<T>(HarmonySignatureUtilities.GeneratorTranspiler transpiler, IEnumerable<T?>? originals) where T : MethodBase
        {
            return HarmonyUtilities.Transpiler(this, transpiler, originals);
        }

        public ImmutableArray<KeyValuePair<T, MethodInfo>> Transpiler<T>(IHarmonyMethod transpiler, IEnumerable<T?>? originals) where T : MethodBase
        {
            return HarmonyUtilities.Transpiler(this, transpiler, originals);
        }

        public ImmutableArray<IHarmonyInstruction> Instructions(MethodBase method)
        {
            return HarmonyUtilities.Instructions(this, method);
        }

        public Boolean Instructions(HarmonySignatureUtilities.Transpiler? transpiler, MethodInfo method)
        {
            return HarmonyUtilities.Instructions(this, transpiler, method);
        }

        public Boolean Instructions<T>(TryConverter<ImmutableArray<IHarmonyInstruction>, T>? transpiler, MethodBase method, [MaybeNullWhen(false)] out T result)
        {
            return HarmonyUtilities.Instructions(this, transpiler, method, out result);
        }

        public override Int32 GetHashCode()
        {
            return _harmony?.GetHashCode() ?? 0;
        }

        public override Boolean Equals([NotNullWhen(true)] Object? other)
        {
            return other switch
            {
                HarmonyLib.Harmony value => Equals(value),
                HarmonyWrapper value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(HarmonyLib.Harmony? other)
        {
            return Equals(Harmony, other);
        }

        public Boolean Equals(HarmonyWrapper other)
        {
            return Equals(other.Harmony);
        }

        public override String? ToString()
        {
            return Harmony.ToString();
        }
    }
}