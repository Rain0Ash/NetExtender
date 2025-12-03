// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Reflection
{
    public sealed class SRInfo : IEquatable<SRInfo>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator String?(SRInfo? value)
        {
            return value?.Value;
        }

        public String Name { get; }
        public Type SR { get; }

        public Assembly Assembly
        {
            get
            {
                return SR.Assembly;
            }
        }

        private readonly Func<String>? _handler;
        private Func<String>? Handler
        {
            get
            {
                return _handler;
            }
        }

        public String Value
        {
            get
            {
                Get(out String result);
                return result;
            }
        }

        public SRInfo(Type type, [CallerMemberName][NotNull] String? name = null)
            : this(type, name ?? throw new ArgumentNullException(nameof(name)), out _)
        {
        }

        private SRInfo(Type type, String name, out Boolean success)
        {
            SR = type ?? throw new ArgumentNullException(nameof(type));

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullOrEmptyStringException(name, nameof(name));
            }

            Name = name;
            success = SRUtilities.TryGet(type, name, out _handler);
        }

        public SRInfo(Assembly assembly, [CallerMemberName, NotNull] String? name = null)
            : this(assembly, name ?? throw new ArgumentNullException(nameof(name)), out _)
        {
        }

        private SRInfo(Assembly assembly, String name, out Boolean success)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullOrEmptyStringException(name, nameof(name));
            }

            SR = SRUtilities.SRType(assembly);
            Name = name;
            success = SRUtilities.TryGet(SR, Name, out _handler);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SRInfo Get(Type type, [CallerMemberName, NotNull] String? name = null)
        {
            SRInfo result = new SRInfo(type, name ?? throw new ArgumentNullException(nameof(name)), out Boolean success);
            return success ? result : throw new SRNotFoundException(type, name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SRInfo Get(Assembly assembly, [CallerMemberName, NotNull] String? name = null)
        {
            SRInfo result = new SRInfo(assembly, name ?? throw new ArgumentNullException(nameof(name)), out Boolean success);
            return success ? result : throw new SRNotFoundException(assembly, name);
        }

        public Boolean Get(out String result)
        {
            if (Handler is not null)
            {
                result = Handler.Invoke();
                return true;
            }

            result = Name;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String Format(Object? arg0)
        {
            return Get(out String result) ? String.Format(result, arg0) : result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String Format(Object? arg0, Object? arg1)
        {
            return Get(out String result) ? String.Format(result, arg0, arg1) : result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String Format(Object? arg0, Object? arg1, Object? arg2)
        {
            return Get(out String result) ? String.Format(result, arg0, arg1, arg2) : result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String Format(params Object?[] args)
        {
            return Get(out String result) ? String.Format(result, args) : result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String Format(IFormatProvider? provider, Object? arg0)
        {
            return Get(out String result) ? String.Format(provider, result, arg0) : result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String Format(IFormatProvider? provider, Object? arg0, Object? arg1)
        {
            return Get(out String result) ? String.Format(provider, result, arg0, arg1) : result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String Format(IFormatProvider? provider, Object? arg0, Object? arg1, Object? arg2)
        {
            return Get(out String result) ? String.Format(provider, result, arg0, arg1, arg2) : result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String Format(IFormatProvider? provider, params Object?[] args)
        {
            return Get(out String result) ? String.Format(provider, result, args) : result;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Name, SR);
        }

        public override Boolean Equals(Object? other)
        {
            return Equals(other as SRInfo);
        }

        public Boolean Equals(SRInfo? other)
        {
            return ReferenceEquals(this, other) || other is not null && Name == other.Name && SR == other.SR;
        }

        public override String ToString()
        {
            return $"{SR.FullName}.{Name}";
        }
    }

    [Serializable]
    public sealed class SRNotFoundException : NotFoundException
    {
        public String Name { get; }

        public SRNotFoundException(Type type, String name)
            : base($"System resource not found: '{name}' in '{type.Name}'.")
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public SRNotFoundException(Type type, String name, Exception exception)
            : base($"System resource not found: '{name}' in '{type.Name}'.", exception)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public SRNotFoundException(Assembly assembly, String name)
            : base($"System resource not found: '{name}' in '{assembly.Name()}'.")
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public SRNotFoundException(Assembly assembly, String name, Exception exception)
            : base($"System resource not found: '{name}' in '{assembly.Name()}'.", exception)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private SRNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Name = info.GetString(nameof(Name)) ?? throw new SerializationException("Name is null.");
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Name), Name);
        }
    }
}