using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Core;

namespace NetExtender.Exceptions.Interfaces
{
    internal interface ISRException<T, out TSR> : ISRException<T>, ITSRException<TSR> where TSR : class, ISRExceptionIdentifier<T, TSR>, new()
    {
        Type ISRException.Type
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Identifier.Type;
            }
        }

        Assembly ISRException.Assembly
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Identifier.Assembly;
            }
        }

        Type ISRException.SRType
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Identifier.SRType;
            }
        }

        String ISRException.SR
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Identifier.SR;
            }
        }

        SRInfo ISRException.Resource
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Identifier.Resource;
            }
        }
    }

    internal interface ITSRException<out TSR> : ISRException where TSR : class, ISRExceptionIdentifier<TSR>, new()
    {
        public TSR Identifier
        {
            get
            {
                return ISRExceptionIdentifier<TSR>.Instance.This;
            }
        }
    }

    public interface ISRException<T> : ISRException
    {
        Type ISRException.Type
        {
            get
            {
                return typeof(T);
            }
        }
    }

    public interface ISRException : IException
    {
        public Type Type { get; }

        public Assembly Assembly
        {
            get
            {
                return Type.Assembly;
            }
        }

        public Type SRType
        {
            get
            {
                return SRUtilities.SRType(Assembly);
            }
        }

        public String SR { get; }

        public SRInfo Resource
        {
            get
            {
                return new SRInfo(SRType, SR);
            }
        }
    }
}