using System;
using System.Reflection;

namespace NetExtender.Types.Interception.Interfaces
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