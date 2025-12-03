using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Exceptions.Interfaces
{
    public interface ISRExceptionIdentifier<T, TSR> : ISRExceptionIdentifier<TSR> where TSR : class, ISRExceptionIdentifier<T, TSR>, new()
    {
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        private static class Info
        {
            public static Type Type
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return typeof(T);
                }
            }

            public static Assembly Assembly
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.Assembly;
                }
            }

            public static Type SRType
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return SRUtilities.SRType(Assembly);
                }
            }

            public static String SR
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return typeof(TSR).Name;
                }
            }

            public static readonly SRInfo Resource = SRInfo.Get(SRType, SR);
        }

        Type ISRExceptionIdentifier.Type
        {
            get
            {
                return Info.Type;
            }
        }

        Assembly ISRExceptionIdentifier.Assembly
        {
            get
            {
                return Info.Assembly;
            }
        }

        Type ISRExceptionIdentifier.SRType
        {
            get
            {
                return Info.SRType;
            }
        }

        String ISRExceptionIdentifier.SR
        {
            get
            {
                return Info.SR;
            }
        }

        SRInfo ISRExceptionIdentifier.Resource
        {
            get
            {
                return Info.Resource;
            }
        }
    }

    public interface ISRExceptionIdentifier<out TSR> : ISRExceptionIdentifier where TSR : class, ISRExceptionIdentifier<TSR>, new()
    {
        public static ISRExceptionIdentifier<TSR> Instance { get; } = new TSR();

        public TSR This
        {
            get
            {
                return (TSR) this;
            }
        }
    }

    public interface ISRExceptionIdentifier
    {
        public Type Type { get; }
        public Assembly Assembly { get; }
        public Type SRType { get; }
        public String SR { get; }
        public SRInfo Resource { get; }
    }
}