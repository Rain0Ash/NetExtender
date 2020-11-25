// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using NetExtender.Utils.Types;
using NetExtender.Types.Dictionaries;

// ReSharper disable MemberHidesStaticFromOuterClass

namespace NetExtender.Types.Enums
{
    /// <summary>
    /// Provides underlying type specified operation interface.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal interface IUnderlyingOperation<T> where T : unmanaged, Enum
    {
        public Boolean IsContinuous { get; }
        public Boolean IsDefined(ref T value);
        public Boolean TryParse(String text, out T result);
        public EnumMember<T> GetMember(ref T value);
    }

    /// <summary>
    /// Provides sbyte specified operation.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal static class SByteOperation<T> where T : unmanaged, Enum
    {
        private abstract class UnderlyingOperation : IUnderlyingOperation<T>
        {
            public abstract Boolean IsContinuous { get; }
            public abstract Boolean IsDefined(ref T value);
            public abstract Boolean IsDefined(ref SByte value);
            public abstract EnumMember<T> GetMember(ref T value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean TryParse(String text, out T result)
            {
                result = default;
                ref SByte x = ref Unsafe.As<T, SByte>(ref result);
                return SByte.TryParse(text, out x);
            }
        }

        private sealed class Continuous : UnderlyingOperation
        {
            private readonly SByte _min;
            private readonly SByte _max;
            private readonly EnumMember<T>[] _members;

            public Continuous(SByte min, SByte max, EnumMember<T>[] members)
            {
                _min = min;
                _max = max;
                _members = members;
            }

            public override Boolean IsContinuous
            {
                get
                {
                    return true;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref T value)
            {
                ref SByte val = ref Unsafe.As<T, SByte>(ref value);
                return _min <= val && val <= _max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref SByte value)
            {
                return _min <= value && value <= _max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(ref T value)
            {
                ref SByte val = ref Unsafe.As<T, SByte>(ref value);
                Int32 index = val - _min;
                return _members[index];
            }
        }

        private sealed class Discontinuous : UnderlyingOperation
        {
            private readonly FrozenSByteKeyDictionary<EnumMember<T>> _mvalue;

            public Discontinuous(FrozenSByteKeyDictionary<EnumMember<T>> mvalue)
            {
                _mvalue = mvalue;
            }

            public override Boolean IsContinuous
            {
                get
                {
                    return false;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref T value)
            {
                ref SByte val = ref Unsafe.As<T, SByte>(ref value);
                return _mvalue.ContainsKey(val);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref SByte value)
            {
                return _mvalue.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(ref T value)
            {
                ref SByte val = ref Unsafe.As<T, SByte>(ref value);
                return _mvalue[val];
            }
        }

        private static UnderlyingOperation operation;

        public static IUnderlyingOperation<T> Create(T min, T max, EnumMember<T>[] members)
        {
            SByte minValue = Unsafe.As<T, SByte>(ref min);
            SByte maxValue = Unsafe.As<T, SByte>(ref max);
            FrozenSByteKeyDictionary<EnumMember<T>> mvalue
                = members.ToFrozenSByteKeyDictionary(x =>
                {
                    T value = x.Value;
                    return Unsafe.As<T, SByte>(ref value);
                });
            if (mvalue.Count > 0)
            {
                Int32 length = maxValue - minValue;
                Int32 count = mvalue.Count - 1;
                if (length == count)
                {
                    operation = new Continuous(minValue, maxValue, members);
                    return operation;
                }
            }

            operation = new Discontinuous(mvalue);
            return operation;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined(ref SByte value)
        {
            return operation.IsDefined(ref value);
        }
    }

    /// <summary>
    /// Provides byte specified operation.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal static class ByteOperation<T> where T : unmanaged, Enum
    {
        private abstract class UnderlyingOperation : IUnderlyingOperation<T>
        {
            public abstract Boolean IsContinuous { get; }
            public abstract Boolean IsDefined(ref T value);
            public abstract Boolean IsDefined(ref Byte value);
            public abstract EnumMember<T> GetMember(ref T value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean TryParse(String text, out T result)
            {
                result = default;
                ref Byte x = ref Unsafe.As<T, Byte>(ref result);
                return Byte.TryParse(text, out x);
            }
        }

        private sealed class Continuous : UnderlyingOperation
        {
            private readonly Byte _min;
            private readonly Byte _max;
            private readonly EnumMember<T>[] _members;

            public Continuous(Byte min, Byte max, EnumMember<T>[] members)
            {
                _min = min;
                _max = max;
                _members = members;
            }

            public override Boolean IsContinuous
            {
                get
                {
                    return true;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref T value)
            {
                ref Byte val = ref Unsafe.As<T, Byte>(ref value);
                return _min <= val && val <= _max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref Byte value)
            {
                return _min <= value && value <= _max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(ref T value)
            {
                ref Byte val = ref Unsafe.As<T, Byte>(ref value);
                Int32 index = val - _min;
                return _members[index];
            }
        }
        
        private sealed class Discontinuous : UnderlyingOperation
        {
            private readonly FrozenByteKeyDictionary<EnumMember<T>> _mvalue;

            public Discontinuous(FrozenByteKeyDictionary<EnumMember<T>> mvalue)
            {
                _mvalue = mvalue;
            }

            public override Boolean IsContinuous
            {
                get
                {
                    return false;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref T value)
            {
                ref Byte val = ref Unsafe.As<T, Byte>(ref value);
                return _mvalue.ContainsKey(val);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref Byte value)
            {
                return _mvalue.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(ref T value)
            {
                ref Byte val = ref Unsafe.As<T, Byte>(ref value);
                return _mvalue[val];
            }
        }

        private static UnderlyingOperation operation;

        public static IUnderlyingOperation<T> Create(T min, T max, EnumMember<T>[] members)
        {
            Byte minValue = Unsafe.As<T, Byte>(ref min);
            Byte maxValue = Unsafe.As<T, Byte>(ref max);
            FrozenByteKeyDictionary<EnumMember<T>> mvalue
                = members.ToFrozenByteKeyDictionary(x =>
                {
                    T value = x.Value;
                    return Unsafe.As<T, Byte>(ref value);
                });
            if (mvalue.Count > 0)
            {
                Int32 length = maxValue - minValue;
                Int32 count = mvalue.Count - 1;
                if (length == count)
                {
                    operation = new Continuous(minValue, maxValue, members);
                    return operation;
                }
            }

            operation = new Discontinuous(mvalue);
            return operation;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined(ref Byte value)
        {
            return operation.IsDefined(ref value);
        }
    }

    /// <summary>
    /// Provides short specified operation.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal static class Int16Operation<T> where T : unmanaged, Enum
    {
        private abstract class UnderlyingOperation : IUnderlyingOperation<T>
        {
            public abstract Boolean IsContinuous { get; }
            public abstract Boolean IsDefined(ref T value);
            public abstract Boolean IsDefined(ref Int16 value);
            public abstract EnumMember<T> GetMember(ref T value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean TryParse(String text, out T result)
            {
                result = default;
                ref Int16 x = ref Unsafe.As<T, Int16>(ref result);
                return Int16.TryParse(text, out x);
            }
        }
        
        private sealed class Continuous : UnderlyingOperation
        {
            private readonly Int16 _min;
            private readonly Int16 _max;
            private readonly EnumMember<T>[] _members;

            public Continuous(Int16 min, Int16 max, EnumMember<T>[] members)
            {
                _min = min;
                _max = max;
                _members = members;
            }

            public override Boolean IsContinuous
            {
                get
                {
                    return true;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref T value)
            {
                ref Int16 val = ref Unsafe.As<T, Int16>(ref value);
                return _min <= val && val <= _max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref Int16 value)
            {
                return _min <= value && value <= _max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(ref T value)
            {
                ref Int16 val = ref Unsafe.As<T, Int16>(ref value);
                Int32 index = val - _min;
                return _members[index];
            }
        }

        private sealed class Discontinuous : UnderlyingOperation
        {
            private readonly FrozenInt16KeyDictionary<EnumMember<T>> _mvalue;

            public Discontinuous(FrozenInt16KeyDictionary<EnumMember<T>> mvalue)
            {
                _mvalue = mvalue;
            }

            public override Boolean IsContinuous
            {
                get
                {
                    return false;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref T value)
            {
                ref Int16 val = ref Unsafe.As<T, Int16>(ref value);
                return _mvalue.ContainsKey(val);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref Int16 value)
            {
                return _mvalue.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(ref T value)
            {
                ref Int16 val = ref Unsafe.As<T, Int16>(ref value);
                return _mvalue[val];
            }
        }
        
        private static UnderlyingOperation operation;
        
        public static IUnderlyingOperation<T> Create(T min, T max, EnumMember<T>[] members)
        {
            Int16 minValue = Unsafe.As<T, Int16>(ref min);
            Int16 maxValue = Unsafe.As<T, Int16>(ref max);
            FrozenInt16KeyDictionary<EnumMember<T>> mvalue
                = members.ToFrozenInt16KeyDictionary(x =>
                {
                    T value = x.Value;
                    return Unsafe.As<T, Int16>(ref value);
                });
            if (mvalue.Count > 0)
            {
                Int32 length = maxValue - minValue;
                Int32 count = mvalue.Count - 1;
                if (length == count)
                {
                    operation = new Continuous(minValue, maxValue, members);
                    return operation;
                }
            }

            operation = new Discontinuous(mvalue);
            return operation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined(ref Int16 value)
        {
            return operation.IsDefined(ref value);
        }
    }
    
    /// <summary>
    /// Provides ushort specified operation.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal static class UInt16Operation<T> where T : unmanaged, Enum
    {
        private abstract class UnderlyingOperation : IUnderlyingOperation<T>
        {
            public abstract Boolean IsContinuous { get; }
            public abstract Boolean IsDefined(ref T value);
            public abstract Boolean IsDefined(ref UInt16 value);
            public abstract EnumMember<T> GetMember(ref T value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean TryParse(String text, out T result)
            {
                result = default;
                ref UInt16 x = ref Unsafe.As<T, UInt16>(ref result);
                return UInt16.TryParse(text, out x);
            }
        }
        
        private sealed class Continuous : UnderlyingOperation
        {
            private readonly UInt16 _min;
            private readonly UInt16 _max;
            private readonly EnumMember<T>[] _members;

            public Continuous(UInt16 min, UInt16 max, EnumMember<T>[] members)
            {
                _min = min;
                _max = max;
                _members = members;
            }

            public override Boolean IsContinuous
            {
                get
                {
                    return true;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref T value)
            {
                ref UInt16 val = ref Unsafe.As<T, UInt16>(ref value);
                return _min <= val && val <= _max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref UInt16 value)
            {
                return _min <= value && value <= _max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(ref T value)
            {
                ref UInt16 val = ref Unsafe.As<T, UInt16>(ref value);
                Int32 index = val - _min;
                return _members[index];
            }
        }
        
        private sealed class Discontinuous : UnderlyingOperation
        {
            private readonly FrozenUInt16KeyDictionary<EnumMember<T>> _mvalue;

            public Discontinuous(FrozenUInt16KeyDictionary<EnumMember<T>> mvalue)
            {
                _mvalue = mvalue;
            }

            public override Boolean IsContinuous
            {
                get
                {
                    return false;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref T value)
            {
                ref UInt16 val = ref Unsafe.As<T, UInt16>(ref value);
                return _mvalue.ContainsKey(val);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref UInt16 value)
            {
                return _mvalue.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(ref T value)
            {
                ref UInt16 val = ref Unsafe.As<T, UInt16>(ref value);
                return _mvalue[val];
            }
        }
        
        private static UnderlyingOperation operation;

        public static IUnderlyingOperation<T> Create(T min, T max, EnumMember<T>[] members)
        {
            UInt16 minValue = Unsafe.As<T, UInt16>(ref min);
            UInt16 maxValue = Unsafe.As<T, UInt16>(ref max);
            FrozenUInt16KeyDictionary<EnumMember<T>> mvalue
                = members.ToFrozenUInt16KeyDictionary(x =>
                {
                    T value = x.Value;
                    return Unsafe.As<T, UInt16>(ref value);
                });
            
            if (mvalue.Count > 0)
            {
                Int32 length = maxValue - minValue;
                Int32 count = mvalue.Count - 1;
                if (length == count)
                {
                    operation = new Continuous(minValue, maxValue, members);
                    return operation;
                }
            }

            operation = new Discontinuous(mvalue);
            return operation;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined(ref UInt16 value)
        {
            return operation.IsDefined(ref value);
        }
    }
    
    /// <summary>
    /// Provides int specified operation.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal static class Int32Operation<T> where T : unmanaged, Enum
    {
        private abstract class UnderlyingOperation : IUnderlyingOperation<T>
        {
            public abstract Boolean IsContinuous { get; }
            public abstract Boolean IsDefined(ref T value);
            public abstract Boolean IsDefined(ref Int32 value);
            public abstract EnumMember<T> GetMember(ref T value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean TryParse(String text, out T result)
            {
                result = default;
                ref Int32 x = ref Unsafe.As<T, Int32>(ref result);
                return Int32.TryParse(text, out x);
            }
        }
        
        private sealed class Continuous : UnderlyingOperation
        {
            private readonly Int32 _min;
            private readonly Int32 _max;
            private readonly EnumMember<T>[] _members;

            public Continuous(Int32 min, Int32 max, EnumMember<T>[] members)
            {
                _min = min;
                _max = max;
                _members = members;
            }

            public override Boolean IsContinuous
            {
                get
                {
                    return true;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref T value)
            {
                ref Int32 val = ref Unsafe.As<T, Int32>(ref value);
                return _min <= val && val <= _max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref Int32 value)
            {
                return _min <= value && value <= _max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(ref T value)
            {
                ref Int32 val = ref Unsafe.As<T, Int32>(ref value);
                Int32 index = val - _min;
                return _members[index];
            }
        }

        private sealed class Discontinuous : UnderlyingOperation
        {
            private readonly FrozenInt32KeyDictionary<EnumMember<T>> _mvalue;

            public Discontinuous(FrozenInt32KeyDictionary<EnumMember<T>> mvalue)
            {
                _mvalue = mvalue;
            }

            public override Boolean IsContinuous
            {
                get
                {
                    return false;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref T value)
            {
                ref Int32 val = ref Unsafe.As<T, Int32>(ref value);
                return _mvalue.ContainsKey(val);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref Int32 value)
            {
                return _mvalue.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(ref T value)
            {
                ref Int32 val = ref Unsafe.As<T, Int32>(ref value);
                return _mvalue[val];
            }
        }
        
        private static UnderlyingOperation operation;

        public static IUnderlyingOperation<T> Create(T min, T max, EnumMember<T>[] members)
        {
            Int32 minValue = Unsafe.As<T, Int32>(ref min);
            Int32 maxValue = Unsafe.As<T, Int32>(ref max);
            FrozenInt32KeyDictionary<EnumMember<T>> mvalue
                = members.ToFrozenInt32KeyDictionary(x =>
                {
                    T value = x.Value;
                    return Unsafe.As<T, Int32>(ref value);
                });
            if (mvalue.Count > 0)
            {
                Int32 length = maxValue - minValue;
                Int32 count = mvalue.Count - 1;
                if (length == count)
                {
                    operation = new Continuous(minValue, maxValue, members);
                    return operation;
                }
            }

            operation = new Discontinuous(mvalue);
            return operation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined(ref Int32 value)
        {
            return operation.IsDefined(ref value);
        }
    }

    /// <summary>
    /// Provides uint specified operation.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal static class UInt32Operation<T> where T : unmanaged, Enum
    {
        private abstract class UnderlyingOperation : IUnderlyingOperation<T>
        {
            public abstract Boolean IsContinuous { get; }
            public abstract Boolean IsDefined(ref T value);
            public abstract Boolean IsDefined(ref UInt32 value);
            public abstract EnumMember<T> GetMember(ref T value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean TryParse(String text, out T result)
            {
                result = default;
                ref UInt32 x = ref Unsafe.As<T, UInt32>(ref result);
                return UInt32.TryParse(text, out x);
            }
        }
        
        private sealed class Continuous : UnderlyingOperation
        {
            private readonly UInt32 _min;
            private readonly UInt32 _max;
            private readonly EnumMember<T>[] _members;

            public Continuous(UInt32 min, UInt32 max, EnumMember<T>[] members)
            {
                _min = min;
                _max = max;
                _members = members;
            }

            public override Boolean IsContinuous
            {
                get
                {
                    return true;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref T value)
            {
                ref UInt32 val = ref Unsafe.As<T, UInt32>(ref value);
                return _min <= val && val <= _max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref UInt32 value)
            {
                return _min <= value && value <= _max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(ref T value)
            {
                ref UInt32 val = ref Unsafe.As<T, UInt32>(ref value);
                UInt32 index = val - _min;
                return _members[index];
            }
        }

        private sealed class Discontinuous : UnderlyingOperation
        {
            private readonly FrozenUInt32KeyDictionary<EnumMember<T>> _mvalue;

            public Discontinuous(FrozenUInt32KeyDictionary<EnumMember<T>> mvalue)
            {
                _mvalue = mvalue;
            }

            public override Boolean IsContinuous
            {
                get
                {
                    return false;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref T value)
            {
                ref UInt32 val = ref Unsafe.As<T, UInt32>(ref value);
                return _mvalue.ContainsKey(val);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref UInt32 value)
            {
                return _mvalue.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(ref T value)
            {
                ref UInt32 val = ref Unsafe.As<T, UInt32>(ref value);
                return _mvalue[val];
            }
        }
        
        private static UnderlyingOperation operation;
        
        public static IUnderlyingOperation<T> Create(T min, T max, EnumMember<T>[] members)
        {
            UInt32 minValue = Unsafe.As<T, UInt32>(ref min);
            UInt32 maxValue = Unsafe.As<T, UInt32>(ref max);
            FrozenUInt32KeyDictionary<EnumMember<T>> mvalue
                = members.ToFrozenUInt32KeyDictionary(x =>
                {
                    T value = x.Value;
                    return Unsafe.As<T, UInt32>(ref value);
                });
            if (mvalue.Count > 0)
            {
                UInt32 length = maxValue - minValue;
                Int32 count = mvalue.Count - 1;
                if (length == count)
                {
                    operation = new Continuous(minValue, maxValue, members);
                    return operation;
                }
            }

            operation = new Discontinuous(mvalue);
            return operation;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined(ref UInt32 value)
        {
            return operation.IsDefined(ref value);
        }
    }


    /// <summary>
    /// Provides long specified operation.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal static class Int64Operation<T> where T : unmanaged, Enum
    {
        private abstract class UnderlyingOperation : IUnderlyingOperation<T>
        {
            public abstract Boolean IsContinuous { get; }
            public abstract Boolean IsDefined(ref T value);
            public abstract Boolean IsDefined(ref Int64 value);
            public abstract EnumMember<T> GetMember(ref T value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean TryParse(String text, out T result)
            {
                result = default;
                ref Int64 x = ref Unsafe.As<T, Int64>(ref result);
                return Int64.TryParse(text, out x);
            }
        }
        
        private sealed class Continuous : UnderlyingOperation
        {
            private readonly Int64 _min;
            private readonly Int64 _max;
            private readonly EnumMember<T>[] _members;

            public Continuous(Int64 min, Int64 max, EnumMember<T>[] members)
            {
                _min = min;
                _max = max;
                _members = members;
            }

            public override Boolean IsContinuous
            {
                get
                {
                    return true;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref T value)
            {
                ref Int64 val = ref Unsafe.As<T, Int64>(ref value);
                return _min <= val && val <= _max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref Int64 value)
            {
                return _min <= value && value <= _max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(ref T value)
            {
                ref Int64 val = ref Unsafe.As<T, Int64>(ref value);
                Int64 index = val - _min;
                return _members[index];
            }
        }
        
        private sealed class Discontinuous : UnderlyingOperation
        {
            private readonly FrozenInt64KeyDictionary<EnumMember<T>> _mvalue;

            public Discontinuous(FrozenInt64KeyDictionary<EnumMember<T>> mvalue)
            {
                _mvalue = mvalue;
            }

            public override Boolean IsContinuous
            {
                get
                {
                    return false;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref T value)
            {
                ref Int64 val = ref Unsafe.As<T, Int64>(ref value);
                return _mvalue.ContainsKey(val);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref Int64 value)
            {
                return _mvalue.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(ref T value)
            {
                ref Int64 val = ref Unsafe.As<T, Int64>(ref value);
                return _mvalue[val];
            }
        }
        
        private static UnderlyingOperation operation;

        public static IUnderlyingOperation<T> Create(T min, T max, EnumMember<T>[] members)
        {
            Int64 minValue = Unsafe.As<T, Int64>(ref min);
            Int64 maxValue = Unsafe.As<T, Int64>(ref max);
            FrozenInt64KeyDictionary<EnumMember<T>> mvalue
                = members.ToFrozenInt64KeyDictionary(x =>
                {
                    T value = x.Value;
                    return Unsafe.As<T, Int64>(ref value);
                });
            if (mvalue.Count > 0)
            {
                Int64 length = maxValue - minValue;
                Int32 count = mvalue.Count - 1;
                if (length == count)
                {
                    operation = new Continuous(minValue, maxValue, members);
                    return operation;
                }
            }

            operation = new Discontinuous(mvalue);
            return operation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined(ref Int64 value)
        {
            return operation.IsDefined(ref value);
        }
    }
    
    /// <summary>
    /// Provides ulong specified operation.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal static class UInt64Operation<T> where T : unmanaged, Enum
    {
        private abstract class UnderlyingOperation : IUnderlyingOperation<T>
        {
            public abstract Boolean IsContinuous { get; }
            public abstract Boolean IsDefined(ref T value);
            public abstract Boolean IsDefined(ref UInt64 value);
            public abstract EnumMember<T> GetMember(ref T value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean TryParse(String text, out T result)
            {
                result = default;
                ref UInt64 x = ref Unsafe.As<T, UInt64>(ref result);
                return UInt64.TryParse(text, out x);
            }
        }
        
        private sealed class Continuous : UnderlyingOperation
        {
            private readonly UInt64 _min;
            private readonly UInt64 _max;
            private readonly EnumMember<T>[] _members;

            public Continuous(UInt64 min, UInt64 max, EnumMember<T>[] members)
            {
                _min = min;
                _max = max;
                _members = members;
            }

            public override Boolean IsContinuous
            {
                get
                {
                    return true;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref T value)
            {
                ref UInt64 val = ref Unsafe.As<T, UInt64>(ref value);
                return _min <= val && val <= _max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref UInt64 value)
            {
                return _min <= value && value <= _max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(ref T value)
            {
                ref UInt64 val = ref Unsafe.As<T, UInt64>(ref value);
                UInt64 index = val - _min;
                return _members[index];
            }
        }

        private sealed class Discontinuous : UnderlyingOperation
        {
            private readonly FrozenUInt64KeyDictionary<EnumMember<T>> _mvalue;

            public Discontinuous(FrozenUInt64KeyDictionary<EnumMember<T>> mvalue)
            {
                _mvalue = mvalue;
            }

            public override Boolean IsContinuous
            {
                get
                {
                    return false;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref T value)
            {
                ref UInt64 val = ref Unsafe.As<T, UInt64>(ref value);
                return _mvalue.ContainsKey(val);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(ref UInt64 value)
            {
                return _mvalue.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(ref T value)
            {
                ref UInt64 val = ref Unsafe.As<T, UInt64>(ref value);
                return _mvalue[val];
            }
        }

        private static UnderlyingOperation operation;
        
        public static IUnderlyingOperation<T> Create(T min, T max, EnumMember<T>[] members)
        {
            UInt64 minval = Unsafe.As<T, UInt64>(ref min);
            UInt64 maxval = Unsafe.As<T, UInt64>(ref max);
            FrozenUInt64KeyDictionary<EnumMember<T>> mvalue = members.ToFrozenUInt64KeyDictionary(x =>
            {
                T value = x.Value;
                return Unsafe.As<T, UInt64>(ref value);
            });

            if (mvalue.Count > 0)
            {
                UInt64 length = maxval - minval;
                Int32 count = mvalue.Count - 1;
                if (length == (UInt64) count)
                {
                    operation = new Continuous(minval, maxval, members);
                    return operation;
                }
            }

            operation = new Discontinuous(mvalue);
            return operation;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined(ref UInt64 value)
        {
            return operation.IsDefined(ref value);
        }
    }
}