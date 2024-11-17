namespace NetExtender.Types.Interception.Interfaces
{
    public interface IAnyMemberInterceptor<in TSender, in TInfo> : IAnyMemberInterceptor<TSender, IPropertyInterceptEventArgs, IMethodInterceptEventArgs, TInfo>, IPropertyInterceptor<TSender, TInfo>, IMethodInterceptor<TSender, TInfo>
    {
    }
    
    public interface IAnyMemberInterceptor<in TSender, in TPropertyArgument, in TMethodArgument, in TInfo> : IPropertyInterceptor<TSender, TPropertyArgument, TInfo>, IAnyMethodInterceptor<TSender, TMethodArgument, TInfo>
    {
    }
}