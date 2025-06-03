// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Intercept.Interfaces
{
    public interface IAnyMemberInterceptor<in TSender, in TInfo> : IAnyMemberInterceptor<TSender, IPropertyInterceptEventArgs, IMethodInterceptEventArgs, TInfo>, IPropertyInterceptor<TSender, TInfo>, IMethodInterceptor<TSender, TInfo>
    {
    }
    
    public interface IAnyMemberInterceptor<in TSender, in TPropertyArgument, in TMethodArgument, in TInfo> : IPropertyInterceptor<TSender, TPropertyArgument, TInfo>, IAnyMethodInterceptor<TSender, TMethodArgument, TInfo>
    {
    }
}