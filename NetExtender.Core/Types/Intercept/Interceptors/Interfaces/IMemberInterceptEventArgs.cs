// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;

namespace NetExtender.Types.Intercept.Interfaces
{
    public interface IMemberInterceptEventArgs<out TMember, T> : IMemberInterceptEventArgs<TMember>, ISimpleInterceptEventArgs<T> where TMember : MemberInfo
    {
    }

    public interface IMemberInterceptEventArgs<out TMember> : IMemberInterceptEventArgs where TMember : MemberInfo
    {
        public TMember Member { get; }
    }

    public interface IMemberInterceptEventArgs : ISimpleInterceptEventArgs
    {
        public Boolean IsSeal { get; }
        public Boolean IsIgnore { get; }

        public void Ignore();
    }
}