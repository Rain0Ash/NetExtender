using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Transactions;
using NetExtender.Types.Culture;
using NetExtender.Types.Enums;
using NetExtender.Types.Enums.Attributes;
using NetExtender.Types.Enums.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Transactions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public static partial class EnumUtilities
    {
        // ReSharper disable once CognitiveComplexity
        public static void Register<T>(EnumSynchronizationMember<T> members) where T : unmanaged, Enum
        {
            if (members is null)
            {
                throw new ArgumentNullException(nameof(members));
            }

            using RegisterTransaction<T> transaction = new RegisterTransaction<T>(); 
            
            try
            {
                if (!CacheValues<T>.Register(members))
                {
                    throw new Exception(nameof(CacheValues<T>));
                }

                if (!CacheValuesWithoutDefault<T>.Register())
                {
                    throw new Exception(nameof(CacheValuesWithoutDefault<T>));
                }

                if (!CacheNames<T>.Register(members))
                {
                    throw new Exception(nameof(CacheNames<T>));
                }

                if (!CacheNamesWithoutDefault<T>.Register())
                {
                    throw new Exception(nameof(CacheNamesWithoutDefault<T>));
                }

                if (!CacheDescription<T>.Register(members))
                {
                    throw new Exception(nameof(CacheDescription<T>));
                }

                if (!CacheDescriptionToEnum<T>.Register(members))
                {
                    throw new Exception(nameof(CacheDescriptionToEnum<T>));
                }

                if (!CacheMembers<T>.Register())
                {
                    throw new Exception(nameof(CacheMembers<T>));
                }

                if (!CacheMembersWithoutDefault<T>.Register())
                {
                    throw new Exception(nameof(CacheMembersWithoutDefault<T>));
                }

                if (!CacheMembersByName<T>.Register())
                {
                    throw new Exception(nameof(CacheMembersByName<T>));
                }

                if (!CacheUnderlyingOperation<T>.Register())
                {
                    throw new Exception(nameof(CacheUnderlyingOperation<T>));
                }

                if (!CacheEnum<T>.Register(members))
                {
                    throw new Exception(nameof(CacheEnum<T>));
                }

                if (!Synchronization.Register(members))
                {
                    throw new Exception(nameof(Synchronization));
                }

                transaction.Commit();
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw new InvalidOperationException($"Exception at: {exception.Message}", exception);
            }
        }

        public static Boolean TryRegister<T>(EnumSynchronizationMember<T> members) where T : unmanaged, Enum
        {
            if (members is null)
            {
                throw new ArgumentNullException(nameof(members));
            }

            try
            {
                Register(members);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // ReSharper disable once CognitiveComplexity
        public static void Register<T, TEnum>(EnumSynchronizationMember<T, TEnum> members) where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
        {
            if (members is null)
            {
                throw new ArgumentNullException(nameof(members));
            }
            
            using RegisterTransaction<T, TEnum> transaction = new RegisterTransaction<T, TEnum>(); 
            
            try
            {
                if (!CacheValues<T>.Register(members))
                {
                    throw new Exception(nameof(CacheValues<T>));
                }

                if (!CacheValuesWithoutDefault<T>.Register())
                {
                    throw new Exception(nameof(CacheValuesWithoutDefault<T>));
                }

                if (!CacheNames<T>.Register(members))
                {
                    throw new Exception(nameof(CacheNames<T>));
                }

                if (!CacheNamesWithoutDefault<T>.Register())
                {
                    throw new Exception(nameof(CacheNamesWithoutDefault<T>));
                }

                if (!CacheDescription<T>.Register(members))
                {
                    throw new Exception(nameof(CacheDescription<T>));
                }

                if (!CacheDescriptionToEnum<T>.Register(members))
                {
                    throw new Exception(nameof(CacheDescriptionToEnum<T>));
                }

                if (!CacheMembers<T>.Register())
                {
                    throw new Exception(nameof(CacheMembers<T>));
                }

                if (!CacheMembersWithoutDefault<T>.Register())
                {
                    throw new Exception(nameof(CacheMembersWithoutDefault<T>));
                }

                if (!CacheMembersByName<T>.Register())
                {
                    throw new Exception(nameof(CacheMembersByName<T>));
                }

                if (!CacheUnderlyingOperation<T>.Register())
                {
                    throw new Exception(nameof(CacheUnderlyingOperation<T>));
                }

                if (!CacheEnum<T>.Register(members))
                {
                    throw new Exception(nameof(CacheEnum<T>));
                }

                if (!Synchronization.Register(members))
                {
                    throw new Exception(nameof(Synchronization));
                }

                transaction.Commit();
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw new InvalidOperationException($"Exception at: {exception.Message}", exception);
            }
        }
        
        public static Boolean TryRegister<T, TEnum>(EnumSynchronizationMember<T, TEnum> members) where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
        {
            if (members is null)
            {
                throw new ArgumentNullException(nameof(members));
            }

            try
            {
                Register(members);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private class RegisterTransaction<T> : ITransaction where T : unmanaged, Enum
        {
            public Boolean? IsCommit { get; private set; }
            public TransactionCommitPolicy Policy { get; init; } = TransactionCommitPolicy.Manual;
            public ImmutableArray<ITransaction> Transactions { get; } = Initialize();

            private static ImmutableArray<ITransaction> Initialize()
            {
                return new[]
                {
                    CacheValues<T>.Transaction(),
                    CacheValuesWithoutDefault<T>.Transaction(),
                    CacheNames<T>.Transaction(),
                    CacheNamesWithoutDefault<T>.Transaction(),
                    CacheDescription<T>.Transaction(),
                    CacheDescriptionToEnum<T>.Transaction(),
                    CacheMembers<T>.Transaction(),
                    CacheMembersWithoutDefault<T>.Transaction(),
                    CacheMembersByName<T>.Transaction(),
                    CacheUnderlyingOperation<T>.Transaction(),
                    CacheEnum<T>.Transaction(),
                    Synchronization.Transaction<T>()
                }.ToImmutableArray();
            }

            public Boolean Commit()
            {
                IsCommit = true;
                
                foreach (ITransaction transaction in Transactions)
                {
                    transaction.Commit();
                }

                return true;
            }

            public Boolean Rollback()
            {
                if (IsCommit == true)
                {
                    return false;
                }

                IsCommit = false;
                
                foreach (ITransaction transaction in Transactions)
                {
                    transaction.Rollback();
                }

                return true;
            }

            public void Dispose()
            {
                if (Policy.IsCommit(IsCommit))
                {
                    Rollback();
                }
            }
        }
        
        private class RegisterTransaction<T, TEnum> : ITransaction where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
        {
            public Boolean? IsCommit { get; private set; }
            public TransactionCommitPolicy Policy { get; init; } = TransactionCommitPolicy.Manual;
            public ImmutableArray<ITransaction> Transactions { get; } = Initialize();

            private static ImmutableArray<ITransaction> Initialize()
            {
                return new[]
                {
                    CacheValues<T>.Transaction(),
                    CacheValuesWithoutDefault<T>.Transaction(),
                    CacheNames<T>.Transaction(),
                    CacheNamesWithoutDefault<T>.Transaction(),
                    CacheDescription<T>.Transaction(),
                    CacheDescriptionToEnum<T>.Transaction(),
                    CacheMembers<T>.Transaction(),
                    CacheMembersWithoutDefault<T>.Transaction(),
                    CacheMembersByName<T>.Transaction(),
                    CacheUnderlyingOperation<T>.Transaction(),
                    CacheEnum<T>.Transaction<TEnum>(),
                    Synchronization.Transaction<T, TEnum>(),
                }.ToImmutableArray();
            }

            public Boolean Commit()
            {
                IsCommit = true;
                
                foreach (ITransaction transaction in Transactions)
                {
                    transaction.Commit();
                }

                return true;
            }

            public Boolean Rollback()
            {
                if (IsCommit == true)
                {
                    return false;
                }

                IsCommit = false;
                
                foreach (ITransaction transaction in Transactions)
                {
                    transaction.Rollback();
                }

                return true;
            }

            public void Dispose()
            {
                if (Policy.IsCommit(IsCommit))
                {
                    Rollback();
                }
            }
        }

        private abstract class CacheTransactionAbstraction : ITransaction
        {
            public static ITransaction Create<T>() where T : CacheTransactionAbstraction, new()
            {
                CacheTransactionAbstraction transaction = new T();
                transaction.Read();
                return transaction;
            }
            
            public Boolean? IsCommit { get; protected set; }
            public TransactionCommitPolicy Policy { get; init; } = TransactionCommitPolicy.Manual;

            protected internal abstract void Read();
            protected internal abstract void Write();

            public Boolean Commit()
            {
                IsCommit = true;
                Read();
                return true;
            }

            public Boolean Rollback()
            {
                IsCommit = false;
                Write();
                return true;
            }

            public void Dispose()
            {
                if (Policy.IsCommit(IsCommit))
                {
                    Rollback();
                }
            }
        }
        
        public static void Reset<T>() where T : unmanaged, Enum
        {
            CacheValues<T>.Reset();
            CacheValuesWithoutDefault<T>.Reset();
            CacheNames<T>.Reset();
            CacheNamesWithoutDefault<T>.Reset();
            CacheDescription<T>.Reset();
            CacheDescriptionToEnum<T>.Reset();
            CacheMembers<T>.Reset();
            CacheMembersWithoutDefault<T>.Reset();
            CacheMembersByName<T>.Reset();
            CacheUnderlyingOperation<T>.Reset();
            CacheEnum<T>.Reset();
            Synchronization.Reset(typeof(T));
        }

        public static void Reset<T, TEnum>() where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
        {
            CacheValues<T>.Reset();
            CacheValuesWithoutDefault<T>.Reset();
            CacheNames<T>.Reset();
            CacheNamesWithoutDefault<T>.Reset();
            CacheDescription<T>.Reset();
            CacheDescriptionToEnum<T>.Reset();
            CacheMembers<T>.Reset();
            CacheMembersWithoutDefault<T>.Reset();
            CacheMembersByName<T>.Reset();
            CacheUnderlyingOperation<T>.Reset();
            CacheEnum<T>.Reset<TEnum>();
            Synchronization.Reset(typeof(T));
            Synchronization.Reset(typeof(TEnum));
        }

        private static class CacheType<T> where T : unmanaged, Enum
        {
            public static Type Type { get; }
            public static Type Underlying { get; }

            static CacheType()
            {
                Type = typeof(T);
                Underlying = Enum.GetUnderlyingType(Type);
            }
        }

        private static class CacheValues<T> where T : unmanaged, Enum
        {
            public static ImmutableArray<T> Values { get; private set; }
            public static ImmutableDictionary<T, Int32> Set { get; private set; } = null!;
            public static ImmutableDictionary<T, Decimal> Decimal { get; private set; } = null!;
            public static T? Minimum { get; private set; }
            public static T? Maximum { get; private set; }
            
            public static Boolean IsEmpty
            {
                get
                {
                    return Values.Length <= 0;
                }
            }

            static CacheValues()
            {
                Initialize();
            }

            private static void Initialize()
            {
                Initialize(null);
            }

            private static void Initialize(IEnumerable<T>? source)
            {
                T[] values = source is not null ? source.ToArray() : Enum.GetValues(CacheType<T>.Type) as T[] ?? throw new ArgumentException(nameof(T));

                Int32 i = 0;
                ImmutableDictionary<T, Int32> set = values.ToImmutableDictionary(value => value, _ => i++);
                ImmutableDictionary<T, Decimal> @decimal = values.ToImmutableDictionary(value => value, value => ((IConvertible) value).ToDecimal());
                (T? minimum, T? maximum) = values.Length > 0 ? values.MinMax() : (default(T?), default(T?));

                Values = values.ToImmutableArray();
                Set = set;
                Decimal = @decimal;
                Minimum = minimum;
                Maximum = maximum;
            }

            public static Boolean Contains(T value)
            {
                return Set.ContainsKey(value);
            }
            
            public static Boolean TryGetValue(T value, out Decimal result)
            {
                if (Decimal.TryGetValue(value, out result))
                {
                    return true;
                }

                result = value.ToDecimal();
                return false;
            }

            public static Int32 IndexOf(T value)
            {
                return TryIndexOf(value, out Int32 result) ? result : -1;
            }

            public static Boolean TryIndexOf(T value, out Int32 result)
            {
                return Set.TryGetValue(value, out result);
            }

            internal static ITransaction Transaction()
            {
                return new CacheTransaction();
            }
            
            internal static Boolean Register(EnumSynchronizationMember<T> member)
            {
                if (member is null)
                {
                    throw new ArgumentNullException(nameof(member));
                }

                try
                {
                    Initialize(member.Select(item => item.Id));
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            internal static void Reset()
            {
                Initialize();
            }

            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            private class CacheTransaction : CacheTransactionAbstraction
            {
                public ImmutableArray<T> Values { get; private set; }
                public ImmutableDictionary<T, Int32> Set { get; private set; } = null!;
                public ImmutableDictionary<T, Decimal> Decimal { get; private set; } = null!;
                public T? Minimum { get; private set; }
                public T? Maximum { get; private set; }

                protected internal override void Read()
                {
                    Values = CacheValues<T>.Values;
                    Set = CacheValues<T>.Set;
                    Decimal = CacheValues<T>.Decimal;
                    Minimum = CacheValues<T>.Minimum;
                    Maximum = CacheValues<T>.Maximum;
                }

                protected internal override void Write()
                {
                    CacheValues<T>.Values = Values;
                    CacheValues<T>.Set = Set;
                    CacheValues<T>.Decimal = Decimal;
                    CacheValues<T>.Minimum = Minimum;
                    CacheValues<T>.Maximum = Maximum;
                }
            }
        }

        private static class CacheValuesWithoutDefault<T> where T : unmanaged, Enum
        {
            public static ImmutableArray<T> Values { get; private set; }
            public static ImmutableDictionary<T, Int32> Set { get; private set; } = null!;
            public static ImmutableDictionary<T, Decimal> Decimal { get; private set; } = null!;
            public static T? Minimum { get; private set; }
            public static T? Maximum { get; private set; }
            
            public static Boolean IsEmpty
            {
                get
                {
                    return Values.Length <= 0;
                }
            }

            static CacheValuesWithoutDefault()
            {
                Initialize();
            }

            private static void Initialize()
            {
                Initialize(null);
            }

            private static void Initialize(IEnumerable<T>? source)
            {
                ImmutableArray<T> values = source?.ToImmutableArray() ?? CacheValues<T>.Values.Where(GenericUtilities.IsNotDefault).ToImmutableArray();
                
                Int32 i = 0;
                ImmutableDictionary<T, Int32> set = values.ToImmutableDictionary(value => value, _ => i++);
                ImmutableDictionary<T, Decimal> @decimal = values.ToImmutableDictionary(value => value, value => ((IConvertible) value).ToDecimal());
                (T? minimum, T? maximum) = values.Length > 0 ? values.MinMax() : (default(T?), default(T?));

                Values = values;
                Set = set;
                Decimal = @decimal;
                Minimum = minimum;
                Maximum = maximum;
            }

            public static Boolean Contains(T value)
            {
                return Set.ContainsKey(value);
            }
            
            public static Boolean TryGetValue(T value, out Decimal result)
            {
                if (Decimal.TryGetValue(value, out result))
                {
                    return true;
                }

                result = value.ToDecimal();
                return false;
            }

            public static Int32 IndexOf(T value)
            {
                return TryIndexOf(value, out Int32 result) ? result : -1;
            }

            public static Boolean TryIndexOf(T value, out Int32 result)
            {
                return Set.TryGetValue(value, out result);
            }

            internal static ITransaction Transaction()
            {
                return new CacheTransaction();
            }
            
            internal static Boolean Register()
            {
                try
                {
                    Initialize();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            internal static void Reset()
            {
                Initialize();
            }
            
            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            private class CacheTransaction : CacheTransactionAbstraction
            {
                public ImmutableArray<T> Values { get; private set; }
                public ImmutableDictionary<T, Int32> Set { get; private set; } = null!;
                public ImmutableDictionary<T, Decimal> Decimal { get; private set; } = null!;
                public T? Minimum { get; private set; }
                public T? Maximum { get; private set; }

                protected internal override void Read()
                {
                    Values = CacheValuesWithoutDefault<T>.Values;
                    Set = CacheValuesWithoutDefault<T>.Set;
                    Decimal = CacheValuesWithoutDefault<T>.Decimal;
                    Minimum = CacheValuesWithoutDefault<T>.Minimum;
                    Maximum = CacheValuesWithoutDefault<T>.Maximum;
                }

                protected internal override void Write()
                {
                    CacheValuesWithoutDefault<T>.Values = Values;
                    CacheValuesWithoutDefault<T>.Set = Set;
                    CacheValuesWithoutDefault<T>.Decimal = Decimal;
                    CacheValuesWithoutDefault<T>.Minimum = Minimum;
                    CacheValuesWithoutDefault<T>.Maximum = Maximum;
                }
            }
        }

        private static class CacheNames<T> where T : unmanaged, Enum
        {
            public static ReadOnlyCollection<String> Names { get; private set; } = null!;
            public static ImmutableHashSet<String> Set { get; private set; } = null!;
            
            public static Boolean IsEmpty
            {
                get
                {
                    return Set.Count <= 0;
                }
            }

            static CacheNames()
            {
                Initialize();
            }

            private static void Initialize()
            {
                Initialize(Enum.GetNames(CacheType<T>.Type));
            }

            private static void Initialize(IEnumerable<T> source)
            {
                if (source is null)
                {
                    throw new ArgumentNullException(nameof(source));
                }
                
                Initialize(source.Select(item => item.ToString()));
            }

            private static void Initialize(IEnumerable<String> source)
            {
                if (source is null)
                {
                    throw new ArgumentNullException(nameof(source));
                }
                
                Names = source.ToReadOnlyArray();
                Set = Names.ToImmutableHashSet();
            }

            public static Boolean Contains(String name)
            {
                return Set.Contains(name);
            }

            internal static ITransaction Transaction()
            {
                return new CacheTransaction();
            }
            
            internal static Boolean Register(EnumSynchronizationMember<T> member)
            {
                if (member is null)
                {
                    throw new ArgumentNullException(nameof(member));
                }

                try
                {
                    Initialize(member.Where(item => item.HasIdentifier).Select(item => item.Title));
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            internal static void Reset()
            {
                Initialize();
            }
            
            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            private class CacheTransaction : CacheTransactionAbstraction
            {
                public ReadOnlyCollection<String> Names { get; private set; } = null!;
                public ImmutableHashSet<String> Set { get; private set; } = null!;

                protected internal override void Read()
                {
                    Names = CacheNames<T>.Names;
                    Set = CacheNames<T>.Set;
                }

                protected internal override void Write()
                {
                    CacheNames<T>.Names = Names;
                    CacheNames<T>.Set = Set;
                }
            }
        }

        private static class CacheNamesWithoutDefault<T> where T : unmanaged, Enum
        {
            public static ReadOnlyCollection<String> Names { get; private set; } = null!;
            public static ImmutableHashSet<String> Set { get; private set; } = null!;
            
            public static Boolean IsEmpty
            {
                get
                {
                    return Set.Count <= 0;
                }
            }

            static CacheNamesWithoutDefault()
            {
                Initialize();
            }
            
            private static void Initialize()
            {
                Initialize(null);
            }

            private static void Initialize(IEnumerable<String>? source)
            {
                source ??= CacheNames<T>.Names.Where(GenericUtilities.IsNotDefault);
                Names = source.ToReadOnlyArray();
                Set = Names.ToImmutableHashSet();
            }

            public static Boolean Contains(String name)
            {
                return Set.Contains(name);
            }

            internal static ITransaction Transaction()
            {
                return new CacheTransaction();
            }
            
            internal static Boolean Register()
            {
                try
                {
                    Initialize();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            internal static void Reset()
            {
                Initialize();
            }
            
            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            private class CacheTransaction : CacheTransactionAbstraction
            {
                public ReadOnlyCollection<String> Names { get; private set; } = null!;
                public ImmutableHashSet<String> Set { get; private set; } = null!;

                protected internal override void Read()
                {
                    Names = CacheNamesWithoutDefault<T>.Names;
                    Set = CacheNamesWithoutDefault<T>.Set;
                }

                protected internal override void Write()
                {
                    CacheNamesWithoutDefault<T>.Names = Names;
                    CacheNamesWithoutDefault<T>.Set = Set;
                }
            }
        }

        private static class CacheDescription<T> where T : unmanaged, Enum
        {
            public static ImmutableDictionary<T, String> Values { get; private set; } = null!;

            static CacheDescription()
            {
                Initialize();
            }

            private static void Initialize()
            {
                Initialize(null);
            }

            private static void Initialize(IEnumerable<T>? source)
            {
                source ??= CacheValues<T>.Values;
                Values = source.Select(Convert).WhereValueNotNull().ToImmutableDictionary();
            }

            private static KeyValuePair<T, String?> Convert(T value)
            {
                return new KeyValuePair<T, String?>(value, Get(value));
            }

            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            private static class Description
            {
                public static ImmutableDictionary<T, ImmutableDictionary<LocalizationIdentifier, String>> Values { get; private set; } = null!;

                static Description()
                {
                    Initialize();
                }
                
                private static void Initialize()
                {
                    Initialize(null);
                }

                private static void Initialize(IEnumerable<T>? source)
                {
                    source ??= CacheValues<T>.Values;
                    Values = source.Select(Convert).WhereValueNotNull().ToImmutableDictionary();
                }

                private static KeyValuePair<T, ImmutableDictionary<LocalizationIdentifier, String>?> Convert(T value)
                {
                    static ImmutableDictionary<LocalizationIdentifier, String>? Internal(T value)
                    {
                        if (Get(value) is not { Length: > 0 } array)
                        {
                            return null;
                        }

                        ImmutableDictionary<LocalizationIdentifier, String> dictionary = array.ToImmutableDictionary();

                        if (dictionary.ContainsKey(LocalizationIdentifier.Invariant))
                        {
                            return dictionary;
                        }

                        if (TryGet(value, out String? invariant) ||
                            dictionary.TryGetValue(LocalizationIdentifier.Default, out invariant) || dictionary.TryGetValue(LocalizationIdentifier.English, out invariant) ||
                            dictionary.TryGetValue(LocalizationIdentifier.Current, out invariant) || dictionary.TryGetValue(LocalizationIdentifier.System, out invariant))
                        {
                            return dictionary.Add(LocalizationIdentifier.Invariant, invariant);
                        }

                        return dictionary.Count == 1 ? dictionary.Add(LocalizationIdentifier.Invariant, dictionary.Values.First()) : dictionary;
                    }

                    return new KeyValuePair<T, ImmutableDictionary<LocalizationIdentifier, String>?>(value, Internal(value));
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Boolean Contains(T value)
                {
                    return Values.ContainsKey(value);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static ImmutableDictionary<LocalizationIdentifier, String>? GetValue(T value)
                {
                    return TryGetValue(value, out ImmutableDictionary<LocalizationIdentifier, String>? result) ? result : null;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static String? GetValue(T value, LocalizationIdentifier identifier)
                {
                    return TryGetValue(value, identifier, out String? result) ? result : null;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Boolean TryGetValue(T value, [MaybeNullWhen(false)] out ImmutableDictionary<LocalizationIdentifier, String> result)
                {
                    return Values.TryGetValue(value, out result);
                }

                public static Boolean TryGetValue(T value, LocalizationIdentifier identifier, [MaybeNullWhen(false)] out String result)
                {
                    if (TryGetValue(value, out ImmutableDictionary<LocalizationIdentifier, String>? container))
                    {
                        return container.TryGetValue(identifier, out result);
                    }

                    result = default;
                    return false;
                }

                [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
                private static KeyValuePair<LocalizationIdentifier, String>[]? Get(T value)
                {
                    Type type = typeof(T);
                    String? name = Enum.GetName(type, value);

                    if (name is null)
                    {
                        return null;
                    }

                    FieldInfo? field = type.GetField(name);
                    if (field is null)
                    {
                        return null;
                    }

                    Attribute[] attributes = Attribute.GetCustomAttributes(field, typeof(EnumDescriptionAttribute));
                    return attributes.Length > 0 ? attributes.Cast<KeyValuePair<LocalizationIdentifier, String>>().ToArray() : null;
                }
                
                internal static Boolean Register(EnumSynchronizationMember<T> member)
                {
                    if (member is null)
                    {
                        throw new ArgumentNullException(nameof(member));
                    }

                    try
                    {
                        Initialize(member.Values.Select(item => item.Id));
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

                internal static void Reset()
                {
                    Initialize();
                }
                
                internal class CacheTransaction : CacheTransactionAbstraction
                {
                    public ImmutableDictionary<T, ImmutableDictionary<LocalizationIdentifier, String>> Values { get; private set; } = null!;

                    protected internal override void Read()
                    {
                        Values = Description.Values;
                    }

                    protected internal override void Write()
                    {
                        Description.Values = Values;
                    }
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains(T value)
            {
                return Values.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static String? GetValue(T value)
            {
                return TryGetValue(value, out String? result) ? result : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static String? GetValue(T value, LocalizationIdentifier identifier)
            {
                return Description.GetValue(value, identifier);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryGetValue(T value, [MaybeNullWhen(false)] out String result)
            {
                return Values.TryGetValue(value, out result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryGetValue(T value, [MaybeNullWhen(false)] out ImmutableDictionary<LocalizationIdentifier, String> result)
            {
                return Description.TryGetValue(value, out result);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryGetValue(T value, LocalizationIdentifier identifier, [MaybeNullWhen(false)] out String result)
            {
                return Description.TryGetValue(value, identifier, out result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Get(T value)
            {
                return TryGet(value, out String? result) ? result : TryGetValue(value, LocalizationIdentifier.Invariant, out result) ? result : null;
            }

            private static Boolean TryGet(T value, [MaybeNullWhen(false)] out String result)
            {
                Type type = typeof(T);
                String? name = Enum.GetName(type, value);

                if (name is null)
                {
                    result = default;
                    return false;
                }

                FieldInfo? field = type.GetField(name);
                if (field is null)
                {
                    result = default;
                    return false;
                }

                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute), false) is not DescriptionAttribute attribute)
                {
                    result = default;
                    return false;
                }

                result = attribute.Description;
                return true;
            }

            internal static ITransaction Transaction()
            {
                return new CacheTransaction();
            }
            
            internal static Boolean Register(EnumSynchronizationMember<T> member)
            {
                if (member is null)
                {
                    throw new ArgumentNullException(nameof(member));
                }

                try
                {
                    Initialize();
                    return Description.Register(member);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            internal static void Reset()
            {
                Initialize();
                Description.Reset();
            }
            
            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            private class CacheTransaction : CacheTransactionAbstraction
            {
                private CacheTransactionAbstraction Description { get; } = new Description.CacheTransaction();
                public ImmutableDictionary<T, String> Values { get; private set; } = null!;

                protected internal override void Read()
                {
                    Values = CacheDescription<T>.Values;
                    Description.Read();
                }

                protected internal override void Write()
                {
                    CacheDescription<T>.Values = Values;
                    Description.Write();
                }
            }
        }
        
        private static class CacheDescriptionToEnum<T> where T : unmanaged, Enum
        {
            public static ImmutableDictionary<String, T> Values { get; private set; } = null!;

            private static void Initialize()
            {
                Initialize((IEnumerable<T>?) null);
            }

            private static void Initialize(IEnumerable<T>? source)
            {
                source ??= CacheValues<T>.Values;
                Dictionary<String, T> values = new Dictionary<String, T>(32);

                foreach (T value in source)
                {
                    String name = value.ToString();
                    if (!CacheDescription<T>.TryGetValue(value, out String? description))
                    {
                        values[name] = value;
                        continue;
                    }

                    values.TryAdd(name, value);
                    values[description] = value;
                }

                Initialize(values);
            }

            private static void Initialize(Dictionary<String, T> source)
            {
                if (source is null)
                {
                    throw new ArgumentNullException(nameof(source));
                }
                
                Values = source.ToImmutableDictionary();
            }

            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            private static class Description
            {
                public static ImmutableDictionary<(LocalizationIdentifier Identifier, String Title), T> Values { get; private set; } = null!;
                public static ImmutableSortedSet<LocalizationIdentifier> Identifiers { get; private set; } = null!;

                static Description()
                {
                    Initialize();
                }

                private static void Initialize()
                {
                    Initialize((IEnumerable<T>?) null);
                }

                private static void Initialize(IEnumerable<T>? source)
                {
                    source ??= CacheValues<T>.Values;
                    Dictionary<(LocalizationIdentifier, String), T> values = new Dictionary<(LocalizationIdentifier, String), T>(32);

                    foreach (T value in source)
                    {
                        String name = value.ToString();
                        if (!CacheDescription<T>.TryGetValue(value, out ImmutableDictionary<LocalizationIdentifier, String>? result))
                        {
                            values[(LocalizationIdentifier.Invariant, name)] = value;
                            continue;
                        }

                        foreach ((LocalizationIdentifier identifier, String description) in result)
                        {
                            (LocalizationIdentifier, String) namekey = (identifier, name);
                            (LocalizationIdentifier, String) key = (identifier, description);

                            values.TryAdd(namekey, value);
                            values[key] = value;
                        }
                    }
                    
                    Initialize(values);
                }

                private static void Initialize(IEnumerable<KeyValuePair<(LocalizationIdentifier, String), T>>? source)
                {
                    if (source is null)
                    {
                        Initialize();
                        return;
                    }
                    
                    Dictionary<(LocalizationIdentifier, String), T> values = new Dictionary<(LocalizationIdentifier, String), T>();

                    foreach (((LocalizationIdentifier, String) key, T value) in source)
                    {
                        values[key] = value;
                    }
                    
                    Initialize(values);
                }

                private static void Initialize(Dictionary<(LocalizationIdentifier, String), T> source)
                {
                    if (source is null)
                    {
                        throw new ArgumentNullException(nameof(source));
                    }
                    
                    Values = source.ToImmutableDictionary();
                    Identifiers = Values.Keys.Select(item => item.Identifier).ToImmutableSortedSet();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Boolean Contains(String value)
                {
                    return value is not null ? Values.ContainsKey((LocalizationIdentifier.Invariant, value)) : throw new ArgumentNullException(nameof(value));
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Boolean Contains(LocalizationIdentifier identifier, String value)
                {
                    return value is not null ? Values.ContainsKey((identifier, value)) : throw new ArgumentNullException(nameof(value));
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Boolean Contains((LocalizationIdentifier Identifier, String Key) value)
                {
                    return value.Key is not null ? Values.ContainsKey(value) : throw new ArgumentNullException(nameof(value));
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Boolean Contains(LocalizationIdentifier identifier)
                {
                    return Identifiers.Contains(identifier);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static ImmutableSortedSet<LocalizationIdentifier> Get()
                {
                    return Identifiers;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Boolean TryGetValue(String value, out T result)
                {
                    return value is not null ? Values.TryGetValue((LocalizationIdentifier.Invariant, value), out result) : throw new ArgumentNullException(nameof(value));
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Boolean TryGetValue(LocalizationIdentifier identifier, String value, out T result)
                {
                    return value is not null ? Values.TryGetValue((identifier, value), out result) : throw new ArgumentNullException(nameof(value));
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Boolean TryGetValue((LocalizationIdentifier Identifier, String Key) value, out T result)
                {
                    return value.Key is not null ? Values.TryGetValue(value, out result) : throw new ArgumentNullException(nameof(value));
                }
                
                internal static Boolean Register(EnumSynchronizationMember<T> member)
                {
                    if (member is null)
                    {
                        throw new ArgumentNullException(nameof(member));
                    }

                    try
                    {
                        Dictionary<(LocalizationIdentifier, String), T> values = new Dictionary<(LocalizationIdentifier, String), T>(member.Values.Length);
                        
                        foreach (IGrouping<LocalizationIdentifier?, Enum<T>> group in member.Values.GroupBy(item => item.Identifier))
                        {
                            foreach (Enum<T> @enum in group)
                            {
                                (LocalizationIdentifier Key, String Title) key = (group.Key ?? LocalizationIdentifier.Invariant, @enum.Title);
                                values[key] = @enum.Id;
                            }
                        }
                        
                        Initialize(values);
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

                internal static void Reset()
                {
                    Initialize();
                }
                
                internal class CacheTransaction : CacheTransactionAbstraction
                {
                    public ImmutableDictionary<(LocalizationIdentifier Identifier, String Title), T> Values { get; private set; } = null!;
                    public ImmutableSortedSet<LocalizationIdentifier> Identifiers { get; private set; } = null!;

                    protected internal override void Read()
                    {
                        Values = Description.Values;
                        Identifiers = Description.Identifiers;
                    }

                    protected internal override void Write()
                    {
                        Description.Values = Values;
                        Description.Identifiers = Identifiers;
                    }
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains(String value)
            {
                return value is not null ? Values.ContainsKey(value) || Description.Contains(value) : throw new ArgumentNullException(nameof(value));
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains(LocalizationIdentifier identifier, String value)
            {
                return Description.Contains(identifier, value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains((LocalizationIdentifier Identifier, String Key) value)
            {
                return Description.Contains(value);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains(LocalizationIdentifier identifier)
            {
                return Description.Contains(identifier);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static ImmutableSortedSet<LocalizationIdentifier> Get()
            {
                return Description.Get();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryGetValue(String value, out T result)
            {
                return value is not null ? Values.TryGetValue(value, out result) || Description.TryGetValue(value, out result) : throw new ArgumentNullException(nameof(value));
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryGetValue(LocalizationIdentifier identifier, String value, out T result)
            {
                return Description.TryGetValue(identifier, value, out result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryGetValue((LocalizationIdentifier Identifier, String Key) value, out T result)
            {
                return Description.TryGetValue(value, out result);
            }

            internal static ITransaction Transaction()
            {
                return new CacheTransaction();
            }
            
            internal static Boolean Register(EnumSynchronizationMember<T> member)
            {
                if (member is null)
                {
                    throw new ArgumentNullException(nameof(member));
                }

                try
                {
                    Dictionary<String, T> values = new Dictionary<String, T>(member.Values.Length);

                    foreach (Enum<T> @enum in member.Values.Where(item => item.Identifier is null || item.Identifier == LocalizationIdentifier.Invariant))
                    {
                        values[@enum.Title] = @enum.Id;
                    }
                    
                    Initialize(values);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            internal static void Reset()
            {
                Initialize();
            }
            
            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            private class CacheTransaction : CacheTransactionAbstraction
            {
                private CacheTransactionAbstraction Description { get; } = new Description.CacheTransaction();
                public ImmutableDictionary<String, T> Values { get; private set; } = null!;

                protected internal override void Read()
                {
                    Values = CacheDescriptionToEnum<T>.Values;
                    Description.Read();
                }

                protected internal override void Write()
                {
                    CacheDescriptionToEnum<T>.Values = Values;
                    Description.Write();
                }
            }
        }

        private static class CacheMembers<T> where T : unmanaged, Enum
        {
            public static ImmutableArray<EnumMember<T>> Members { get; private set; }

            static CacheMembers()
            {
                Initialize();
            }

            private static void Initialize()
            {
                Initialize(null);
            }

            private static void Initialize(IEnumerable<String>? source)
            {
                source ??= CacheNames<T>.Names;
                Members = source.Select(value => new EnumMember<T>(value)).ToImmutableArray();
            }

            internal static ITransaction Transaction()
            {
                return new CacheTransaction();
            }

            internal static Boolean Register()
            {
                try
                {
                    Initialize();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            internal static void Reset()
            {
                Initialize();
            }
            
            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            private class CacheTransaction : CacheTransactionAbstraction
            {
                public ImmutableArray<EnumMember<T>> Members { get; private set; }

                protected internal override void Read()
                {
                    Members = CacheMembers<T>.Members;
                }

                protected internal override void Write()
                {
                    CacheMembers<T>.Members = Members;
                }
            }
        }

        private static class CacheMembersWithoutDefault<T> where T : unmanaged, Enum
        {
            public static ImmutableArray<EnumMember<T>> Members { get; private set; }

            static CacheMembersWithoutDefault()
            {
                Initialize();
            }

            private static void Initialize()
            {
                Initialize(null);
            }
            
            private static void Initialize(IEnumerable<EnumMember<T>>? source)
            {
                if (source is null)
                {
                    Initialize(CacheMembers<T>.Members);
                    return;
                }
                
                Members = source.Where(member => member.Value.IsNotDefault()).ToImmutableArray();
            }
            
            private static void Initialize(ImmutableArray<EnumMember<T>> source)
            {
                Members = source.Where(member => member.Value.IsNotDefault()).ToImmutableArray();
            }

            internal static ITransaction Transaction()
            {
                return new CacheTransaction();
            }

            internal static Boolean Register()
            {
                try
                {
                    Initialize();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            internal static void Reset()
            {
                Initialize();
            }
            
            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            private class CacheTransaction : CacheTransactionAbstraction
            {
                public ImmutableArray<EnumMember<T>> Members { get; private set; }

                protected internal override void Read()
                {
                    Members = CacheMembersWithoutDefault<T>.Members;
                }

                protected internal override void Write()
                {
                    CacheMembersWithoutDefault<T>.Members = Members;
                }
            }
        }

        /// <summary>
        /// Provides cache for enum attributes.
        /// </summary>
        /// <typeparam name="T">Enum Type</typeparam>
        /// <typeparam name="TAttribute">Attribute Type</typeparam>
        internal static class CacheAttributes<T, TAttribute> where T : unmanaged, Enum where TAttribute : Attribute
        {
            public static ImmutableDictionary<T, ImmutableArray<TAttribute>> Cache { get; }

            static CacheAttributes()
            {
                Cache = GetValues<T>().ToImmutableDictionary(key => key, value => value.ToMember().FieldInfo!
                    .GetCustomAttributes(typeof(TAttribute), true).OfType<TAttribute>().ToImmutableArray());
            }

            public static IReadOnlyList<TAttribute> Get(T value)
            {
                return Cache[value];
            }
        }

        private static class CacheIsFlags<T> where T : unmanaged, Enum
        {
            public static Boolean IsFlags { get; }

            static CacheIsFlags()
            {
                IsFlags = Attribute.IsDefined(CacheType<T>.Type, typeof(FlagsAttribute));
            }
        }

        private static class CacheMembersByName<T> where T : unmanaged, Enum
        {
            public static ImmutableDictionary<String, EnumMember<T>> MembersByName { get; private set; } = null!;
            public static ImmutableDictionary<String, EnumMember<T>> MembersByNameInsensitive { get; private set; } = null!;

            static CacheMembersByName()
            {
                Initialize();
            }

            private static void Initialize()
            {
                static String Selector(EnumMember<T> member)
                {
                    return member.Name;
                }

                MembersByName = CacheMembers<T>.Members.ToImmutableDictionary(Selector);
                MembersByNameInsensitive = CacheMembers<T>.Members.DistinctBy(Selector, StringComparer.OrdinalIgnoreCase).ToImmutableDictionary(Selector, StringComparer.OrdinalIgnoreCase);
            }

            internal static ITransaction Transaction()
            {
                return new CacheTransaction();
            }

            internal static Boolean Register()
            {
                try
                {
                    Initialize();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            internal static void Reset()
            {
                Initialize();
            }
            
            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            private class CacheTransaction : CacheTransactionAbstraction
            {
                public ImmutableDictionary<String, EnumMember<T>> MembersByName { get; private set; } = null!;
                public ImmutableDictionary<String, EnumMember<T>> MembersByNameInsensitive { get; private set; } = null!;

                protected internal override void Read()
                {
                    MembersByName = CacheMembersByName<T>.MembersByName;
                    MembersByNameInsensitive = CacheMembersByName<T>.MembersByNameInsensitive;
                }

                protected internal override void Write()
                {
                    CacheMembersByName<T>.MembersByName = MembersByName;
                    CacheMembersByName<T>.MembersByNameInsensitive = MembersByNameInsensitive;
                }
            }
        }

        private static class CacheUnderlyingOperation<T> where T : unmanaged, Enum
        {
            public static Type Underlying { get; }
            public static IUnderlyingEnumOperation<T> Operation { get; private set; } = null!;

            static CacheUnderlyingOperation()
            {
                Underlying = CacheType<T>.Underlying;
                Initialize();
            }

            [SuppressMessage("ReSharper", "SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault")]
            private static void Initialize()
            {
                T minimum = Minimum<T>();
                T maximum = Maximum<T>();
                EnumMember<T>[] distinct = CacheMembers<T>.Members.OrderBy(member => member.Value).Distinct(new EnumMember<T>.ValueComparer()).ToArray();

                TypeCode type = Type.GetTypeCode(CacheType<T>.Type);
                Operation = type switch
                {
                    TypeCode.SByte => SByteOperation<T>.Create(minimum, maximum, distinct),
                    TypeCode.Byte => ByteOperation<T>.Create(minimum, maximum, distinct),
                    TypeCode.Int16 => Int16Operation<T>.Create(minimum, maximum, distinct),
                    TypeCode.UInt16 => UInt16Operation<T>.Create(minimum, maximum, distinct),
                    TypeCode.Int32 => Int32Operation<T>.Create(minimum, maximum, distinct),
                    TypeCode.UInt32 => UInt32Operation<T>.Create(minimum, maximum, distinct),
                    TypeCode.Int64 => Int64Operation<T>.Create(minimum, maximum, distinct),
                    TypeCode.UInt64 => UInt64Operation<T>.Create(minimum, maximum, distinct),
                    _ => throw new EnumUndefinedOrNotSupportedException<TypeCode>(type, nameof(type), null)
                };
            }

            internal static ITransaction Transaction()
            {
                return new CacheTransaction();
            }

            internal static Boolean Register()
            {
                try
                {
                    Initialize();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            internal static void Reset()
            {
                Initialize();
            }
            
            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            private class CacheTransaction : CacheTransactionAbstraction
            {
                public IUnderlyingEnumOperation<T> Operation { get; private set; } = null!;

                protected internal override void Read()
                {
                    Operation = CacheUnderlyingOperation<T>.Operation;
                }

                protected internal override void Write()
                {
                    CacheUnderlyingOperation<T>.Operation = Operation;
                }
            }
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        internal static class CacheEnum<T> where T : unmanaged, Enum
        {
            public static ImmutableSortedSet<Enum<T>> Values { get; private set; } = null!;
            public static ImmutableDictionary<T, Enum<T>> Enums { get; private set; } = null!;
            public static ImmutableDictionary<T, ImmutableDictionary<LocalizationIdentifier, Enum<T>>> Identifiers { get; private set; } = null!;
            public static ImmutableDictionary<LocalizationIdentifier, ImmutableSortedSet<Enum<T>>> Grouping { get; private set; } = null!;

            static CacheEnum()
            {
                Initialize();
            }
            
            private static void Initialize()
            {
                Initialize((IEnumerable<T>?) null);
            }

            private static void Initialize(IEnumerable<T>? source)
            {
                source ??= CacheValues<T>.Values;
                Initialize(source.Select(value => Enum<T>.Create(value)));
            }

            private static void Initialize(IEnumerable<Enum<T>>? source)
            {
                if (source is null)
                {
                    Initialize();
                    return;
                }
                
                ImmutableSortedSet<Enum<T>> values = source.ToImmutableSortedSet();
                ImmutableDictionary<T, Enum<T>> enums = values.ToImmutableDictionary(value => value.Id, value => value);
                ImmutableDictionary<T, ImmutableDictionary<LocalizationIdentifier, Enum<T>>> identifiers = values.ToImmutableDictionary(value => value.Id, value => CacheDescriptionToEnum<T>.Get()
                    .ToImmutableDictionary(identifier => identifier, identifier => Enum<T>.Create(value, identifier)));
                ImmutableDictionary<LocalizationIdentifier, ImmutableSortedSet<Enum<T>>> grouping = identifiers.SelectMany(pair => pair.Value).GroupBy(pair => pair.Key, pair => pair.Value)
                    .ToImmutableDictionary(group => group.Key, group => group.Select(x => x).ToImmutableSortedSet());
                
                Values = values;
                Enums = enums;
                Identifiers = identifiers;
                Grouping = grouping;
            }

            private static class Type<TEnum> where TEnum : Enum<T, TEnum>, new()
            {
                public static ImmutableSortedSet<TEnum> Values { get; private set; } = null!;
                public static ImmutableDictionary<T, TEnum> Enums { get; private set; } = null!;
                public static ImmutableDictionary<T, ImmutableDictionary<LocalizationIdentifier, TEnum>> Identifiers { get; private set; } = null!;
                public static ImmutableDictionary<LocalizationIdentifier, ImmutableSortedSet<TEnum>> Grouping { get; private set; } = null!;

                static Type()
                {
                    Initialize();
                }

                private static void Initialize()
                {
                    Initialize((IEnumerable<T>?) null);
                }

                private static void Initialize(IEnumerable<T>? source)
                {
                    source ??= CacheValues<T>.Values;
                    Initialize(source.Select(value => Enum<T>.Create<TEnum>(value)));
                }

                private static void Initialize(IEnumerable<TEnum>? source)
                {
                    if (source is null)
                    {
                        Initialize();
                        return;
                    }
                    
                    ImmutableSortedSet<TEnum> values = source.ToImmutableSortedSet();
                    ImmutableDictionary<T, TEnum> enums = values.ToImmutableDictionary(value => value.Id, value => value);
                    ImmutableDictionary<T, ImmutableDictionary<LocalizationIdentifier, TEnum>> identifiers = values.ToImmutableDictionary(value => value.Id, value => CacheDescriptionToEnum<T>.Get()
                        .ToImmutableDictionary(identifier => identifier, identifier => Enum<T>.Create<TEnum>(value, identifier)));
                    ImmutableDictionary<LocalizationIdentifier, ImmutableSortedSet<TEnum>> grouping = identifiers.SelectMany(pair => pair.Value).GroupBy(pair => pair.Key, pair => pair.Value)
                        .ToImmutableDictionary(group => group.Key, group => group.Select(x => x).ToImmutableSortedSet());

                    Values = values;
                    Enums = enums;
                    Identifiers = identifiers;
                    Grouping = grouping;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static ImmutableSortedSet<TEnum> Get()
                {
                    return Values;
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static ImmutableSortedSet<TEnum>? Get(LocalizationIdentifier identifier)
                {
                    return Grouping.TryGetValue(identifier, out ImmutableSortedSet<TEnum>? result) ? result : null;
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Boolean Contains(T value)
                {
                    return Enums.ContainsKey(value);
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Boolean Contains(LocalizationIdentifier identifier, T value)
                {
                    return Identifiers.TryGetValue(value, out ImmutableDictionary<LocalizationIdentifier, TEnum>? container) && container.ContainsKey(identifier);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Boolean Contains(String value)
                {
                    if (value is null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    return CacheDescriptionToEnum<T>.TryGetValue(value, out T result) && Contains(result);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Boolean Contains(LocalizationIdentifier identifier, String value)
                {
                    return CacheDescriptionToEnum<T>.TryGetValue(identifier, value, out T result) && Contains(result);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Boolean Contains(TEnum value)
                {
                    if (value is null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }
                    
                    return (value.Identifier is { } identifier ? TryParse(identifier, value.Id, out TEnum? result) : TryParse(value.Id, out result)) && String.Equals(value.Title, result.Title, StringComparison.Ordinal);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Boolean IsIntern(TEnum value)
                {
                    if (value is null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    return Values.Contains(value);
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Boolean TryParse(T value, [MaybeNullWhen(false)] out TEnum result)
                {
                    return Enums.TryGetValue(value, out result);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Boolean TryParse(LocalizationIdentifier identifier, T value, [MaybeNullWhen(false)] out TEnum result)
                {
                    if (Identifiers.TryGetValue(value, out ImmutableDictionary<LocalizationIdentifier, TEnum>? container))
                    {
                        return container.TryGetValue(identifier, out result);
                    }

                    result = default;
                    return false;
                }

                [MethodImpl(MethodImplOptions.AggressiveOptimization)]
                public static Boolean TryParse(String value, [MaybeNullWhen(false)] out TEnum result)
                {
                    if (value is null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }
                    
                    if (CacheDescriptionToEnum<T>.TryGetValue(value, out T @enum))
                    {
                        return TryParse(@enum, out result);
                    }

                    result = default;
                    return false;
                }

                [MethodImpl(MethodImplOptions.AggressiveOptimization)]
                public static Boolean TryParse(LocalizationIdentifier identifier, String value, [MaybeNullWhen(false)] out TEnum result)
                {
                    if (value is null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }
                    
                    if (CacheDescriptionToEnum<T>.TryGetValue(identifier, value, out T @enum))
                    {
                        return TryParse(@enum, out result);
                    }

                    result = default;
                    return false;
                }

                internal static Boolean Register(EnumSynchronizationMember<T, TEnum> member)
                {
                    if (member is null)
                    {
                        throw new ArgumentNullException(nameof(member));
                    }

                    try
                    {
                        Initialize(member.Values);
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

                internal static void Reset()
                {
                    Initialize();
                }
            
                [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
                internal class CacheTransaction : CacheTransactionAbstraction
                {
                    public ImmutableSortedSet<TEnum> Values { get; private set; } = null!;
                    public ImmutableDictionary<T, TEnum> Enums { get; private set; } = null!;
                    public ImmutableDictionary<T, ImmutableDictionary<LocalizationIdentifier, TEnum>> Identifiers { get; private set; } = null!;
                    public ImmutableDictionary<LocalizationIdentifier, ImmutableSortedSet<TEnum>> Grouping { get; private set; } = null!;

                    protected internal override void Read()
                    {
                        Values = Type<TEnum>.Values;
                        Enums = Type<TEnum>.Enums;
                        Identifiers = Type<TEnum>.Identifiers;
                        Grouping = Type<TEnum>.Grouping;
                    }

                    protected internal override void Write()
                    {
                        Type<TEnum>.Values = Values;
                        Type<TEnum>.Enums = Enums;
                        Type<TEnum>.Identifiers = Identifiers;
                        Type<TEnum>.Grouping = Grouping;
                    }
                }
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static ImmutableSortedSet<Enum<T>> Get()
            {
                return Values;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static ImmutableSortedSet<Enum<T>>? Get(LocalizationIdentifier identifier)
            {
                return Grouping.TryGetValue(identifier, out ImmutableSortedSet<Enum<T>>? result) ? result : null;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static ImmutableSortedSet<TEnum> Get<TEnum>() where TEnum : Enum<T, TEnum>, new()
            {
                return Type<TEnum>.Get();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static ImmutableSortedSet<TEnum>? Get<TEnum>(LocalizationIdentifier identifier) where TEnum : Enum<T, TEnum>, new()
            {
                return Type<TEnum>.Get(identifier);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains(T value)
            {
                return Enums.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains(LocalizationIdentifier identifier, T value)
            {
                return Identifiers.TryGetValue(value, out ImmutableDictionary<LocalizationIdentifier, Enum<T>>? container) && container.ContainsKey(identifier);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains(String value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return CacheDescriptionToEnum<T>.TryGetValue(value, out T result) && Contains(result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains(LocalizationIdentifier identifier, String value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return CacheDescriptionToEnum<T>.TryGetValue(identifier, value, out T result) && Contains(result);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains(Enum<T> value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return (value.Identifier is { } identifier ? TryParse(identifier, value.Id, out Enum<T>? result) : TryParse(value.Id, out result)) && String.Equals(value.Title, result.Title, StringComparison.Ordinal);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean IsIntern(Enum<T> value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return value.Identifier is { } identifier ? Grouping.TryGetValue(identifier, out ImmutableSortedSet<Enum<T>>? container) && container.Contains(value) : Values.Contains(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains<TEnum>(T value) where TEnum : Enum<T, TEnum>, new()
            {
                return Type<TEnum>.Contains(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains<TEnum>(LocalizationIdentifier identifier, T value) where TEnum : Enum<T, TEnum>, new()
            {
                return Type<TEnum>.Contains(identifier, value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains<TEnum>(String value) where TEnum : Enum<T, TEnum>, new()
            {
                return Type<TEnum>.Contains(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains<TEnum>(LocalizationIdentifier identifier, String value) where TEnum : Enum<T, TEnum>, new()
            {
                return Type<TEnum>.Contains(identifier, value);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains<TEnum>(TEnum value) where TEnum : Enum<T, TEnum>, new()
            {
                return Type<TEnum>.Contains(value);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean IsIntern<TEnum>(TEnum value) where TEnum : Enum<T, TEnum>, new()
            {
                return Type<TEnum>.IsIntern(value);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryParse(T value, [MaybeNullWhen(false)] out Enum<T> result)
            {
                return Enums.TryGetValue(value, out result);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryParse(LocalizationIdentifier identifier, T value, [MaybeNullWhen(false)] out Enum<T> result)
            {
                if (Identifiers.TryGetValue(value, out ImmutableDictionary<LocalizationIdentifier, Enum<T>>? container))
                {
                    return container.TryGetValue(identifier, out result);
                }

                result = default;
                return false;
            }
        
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public static Boolean TryParse(String value, [MaybeNullWhen(false)] out Enum<T> result)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (CacheDescriptionToEnum<T>.TryGetValue(value, out T @enum))
                {
                    return TryParse(@enum, out result);
                }

                result = default;
                return false;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public static Boolean TryParse(LocalizationIdentifier identifier, String value, [MaybeNullWhen(false)] out Enum<T> result)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                    
                if (CacheDescriptionToEnum<T>.TryGetValue(identifier, value, out T @enum))
                {
                    return TryParse(@enum, out result);
                }

                result = default;
                return false;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryParse<TEnum>(T value, [MaybeNullWhen(false)] out TEnum result) where TEnum : Enum<T, TEnum>, new()
            {
                return Type<TEnum>.TryParse(value, out result);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryParse<TEnum>(LocalizationIdentifier identifier, T value, [MaybeNullWhen(false)] out TEnum result) where TEnum : Enum<T, TEnum>, new()
            {
                return Type<TEnum>.TryParse(identifier, value, out result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryParse<TEnum>(String value, [MaybeNullWhen(false)] out TEnum result) where TEnum : Enum<T, TEnum>, new()
            {
                return Type<TEnum>.TryParse(value, out result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryParse<TEnum>(LocalizationIdentifier identifier, String value, [MaybeNullWhen(false)] out TEnum result) where TEnum : Enum<T, TEnum>, new()
            {
                return Type<TEnum>.TryParse(identifier, value, out result);
            }

            internal static ITransaction Transaction()
            {
                return new CacheTransaction();
            }

            internal static ITransaction Transaction<TEnum>() where TEnum : Enum<T, TEnum>, new()
            {
                return new CacheTransaction<TEnum>();
            }
            
            internal static Boolean Register(EnumSynchronizationMember<T> member)
            {
                if (member is null)
                {
                    throw new ArgumentNullException(nameof(member));
                }

                try
                {
                    Initialize(member.Values);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            
            internal static Boolean Register<TEnum>(EnumSynchronizationMember<T, TEnum> member) where TEnum : Enum<T, TEnum>, new()
            {
                if (member is null)
                {
                    throw new ArgumentNullException(nameof(member));
                }

                try
                {
                    return Type<TEnum>.Register(member) && Register((EnumSynchronizationMember<T>) member);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            internal static void Reset()
            {
                Initialize();
            }
            
            internal static void Reset<TEnum>() where TEnum : Enum<T, TEnum>, new()
            {
                Type<TEnum>.Reset();
                Reset();
            }

            private class CacheTransaction : CacheTransactionAbstraction
            {
                public ImmutableSortedSet<Enum<T>> Values { get; private set; } = null!;
                public ImmutableDictionary<T, Enum<T>> Enums { get; private set; } = null!;
                public ImmutableDictionary<T, ImmutableDictionary<LocalizationIdentifier, Enum<T>>> Identifiers { get; private set; } = null!;
                public ImmutableDictionary<LocalizationIdentifier, ImmutableSortedSet<Enum<T>>> Grouping { get; private set; } = null!;

                protected internal override void Read()
                {
                    Values = CacheEnum<T>.Values;
                    Enums = CacheEnum<T>.Enums;
                    Identifiers = CacheEnum<T>.Identifiers;
                    Grouping = CacheEnum<T>.Grouping;
                }

                protected internal override void Write()
                {
                    CacheEnum<T>.Values = Values;
                    CacheEnum<T>.Enums = Enums;
                    CacheEnum<T>.Identifiers = Identifiers;
                    CacheEnum<T>.Grouping = Grouping;
                }
            }

            private class CacheTransaction<TEnum> : CacheTransaction where TEnum : Enum<T, TEnum>, new()
            {
                private Type<TEnum>.CacheTransaction Type { get; } = new Type<TEnum>.CacheTransaction();

                protected internal override void Read()
                {
                    base.Read();
                    Type.Read();
                }

                protected internal override void Write()
                {
                    base.Write();
                    Type.Write();
                }
            }
        }

        public static class Synchronization
        {
            private static ConcurrentDictionary<Type, EnumSynchronizationMember> Enums { get; set; } = null!;
            private static ImmutableArray<KeyValuePair<Type, EnumSynchronizationMember>> Array { get; set; }

            static Synchronization()
            {
                Initialize();
            }

            private static void Initialize()
            {
                static Boolean Predicate(Type type)
                {
                    if (type is null)
                    {
                        throw new ArgumentNullException(nameof(type));
                    }

                    return type.IsDefined(typeof(EnumSynchronizeAttribute), false);
                }
                
                Enums = new ConcurrentDictionary<Type, EnumSynchronizationMember>(ReflectionUtilities.Domain.CustomTypes.Where(Predicate).Select(Member).WhereValueNotNull());
                Array = Enums.OrderByDescending(pair => pair.Value.Order).ToImmutableArray();
            }

            private static KeyValuePair<Type, EnumSynchronizationMember?> Member(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                if (!type.HasInterface<IEnum>() || !type.IsSuperclassOfRawGeneric(typeof(Enum<,>)))
                {
                    return new KeyValuePair<Type, EnumSynchronizationMember?>(type, null);
                }

                if (ReflectionUtilities.GetCustomAttribute<EnumSynchronizeAttribute>(type) is not { } attribute || type.GetGenericArguments(typeof(Enum<,>)) is not { Length: 2 } generic)
                {
                    return new KeyValuePair<Type, EnumSynchronizationMember?>(type, null);
                }

                if (!generic[0].IsEnum)
                {
                    return new KeyValuePair<Type, EnumSynchronizationMember?>(type, null);
                }

                MethodInfo? getter = typeof(CacheEnum<>).GetNestedType(nameof(Type)+"`1", BindingFlags.Static | BindingFlags.NonPublic)?
                    .MakeGenericType(generic).GetMethod(nameof(Get), BindingFlags.Static | BindingFlags.Public, System.Array.Empty<Type>());

                if (getter?.Invoke(null, null) is not IEnumerable enumerable)
                {
                    return new KeyValuePair<Type, EnumSynchronizationMember?>(type, null);
                }
                
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance;
                EnumSynchronizationMember? member = (EnumSynchronizationMember?) Activator.CreateInstance(typeof(EnumSynchronizationMember<,>).MakeGenericType(generic), binding, null, new Object[] { attribute, enumerable }, null);

                return new KeyValuePair<Type, EnumSynchronizationMember?>(type, member);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                return Enums.ContainsKey(type);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains<T, TEnum>() where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
            {
                return Contains(typeof(T));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryGet(Type type, [MaybeNullWhen(false)] out EnumSynchronizationMember result)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                return Enums.TryGetValue(type, out result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryGet<T, TEnum>([MaybeNullWhen(false)] out EnumSynchronizationMember result) where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
            {
                return TryGet(typeof(TEnum), out result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static ImmutableArray<KeyValuePair<Type, EnumSynchronizationMember>> Get()
            {
                return Array;
            }

            internal static ITransaction Transaction<T>() where T : unmanaged, Enum
            {
                return new CacheTransaction<T>();
            }

            internal static ITransaction Transaction<T, TEnum>() where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
            {
                return new CacheTransaction<T, TEnum>();
            }

            internal static Boolean Register(EnumSynchronizationMember member)
            {
                if (member is null)
                {
                    throw new ArgumentNullException(nameof(member));
                }

                try
                {
                    Enums.AddOrUpdate(member.Type, _ => member, (_, _) => member);
                    Array = Enums.OrderByDescending(pair => pair.Value.Order).ToImmutableArray();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            internal static Boolean Register(IEnumerable<EnumSynchronizationMember> source)
            {
                if (source is null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                Boolean successful = false;
                foreach (EnumSynchronizationMember member in source)
                {
                    try
                    {
                        Enums.AddOrUpdate(member.Type, _ => member, (_, _) => member);
                        successful = true;
                    }
                    catch (Exception)
                    {
                    }
                }

                if (successful)
                {
                    Array = Enums.OrderByDescending(pair => pair.Value.Order).ToImmutableArray();
                }
                
                return successful;
            }

            internal static void Reset()
            {
                Initialize();
            }

            internal static void Reset(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                if (Enums.TryRemove(type, out _))
                {
                    Array = Enums.OrderByDescending(pair => pair.Value.Order).ToImmutableArray();
                }
            }

            internal static void Reset(IEnumerable<Type> source)
            {
                if (source is null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                Boolean successful = false;
                foreach (Type type in source)
                {
                    try
                    {
                        Enums.TryRemove(type, out _);
                        successful = true;
                    }
                    catch (Exception)
                    {
                    }
                }
                
                if (successful)
                {
                    Array = Enums.OrderByDescending(pair => pair.Value.Order).ToImmutableArray();
                }
            }
            
            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            private class CacheTransaction<T> : CacheTransactionAbstraction where T : unmanaged, Enum
            {
                public EnumSynchronizationMember? Member { get; private set; }

                protected internal override void Read()
                {
                    Member = Enums.GetValueOrDefault(typeof(T));
                }

                protected internal override void Write()
                {
                    if (Member is null)
                    {
                        Reset(typeof(T));
                        return;
                    }
                    
                    Enums.AddOrUpdate(Member.Type, _ => Member, (_, member) => ReferenceEquals(member, Member) ? member : throw new InvalidOperationException());
                    Array = Enums.OrderByDescending(pair => pair.Value.Order).ToImmutableArray();
                }
            }
            
            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            private class CacheTransaction<T, TEnum> : CacheTransaction<T> where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
            {
            }
        }
    }
}