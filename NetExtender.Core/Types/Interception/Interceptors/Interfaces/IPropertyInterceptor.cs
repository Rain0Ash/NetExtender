using System;
using System.Runtime.CompilerServices;

namespace NetExtender.Types.Interception.Interfaces
{
    public interface IPropertyInterceptor<in TSender, in TInfo> : IPropertyInterceptor<TSender, IPropertyInterceptEventArgs, TInfo>
    {
    }
    
    public interface IPropertyInterceptor<in TSender, in TArgument, in TInfo>
    {
        public T InterceptGet<T>(TSender sender, [CallerMemberName] String? property = null);
        public T InterceptGet<T>(TSender sender, TInfo? info, [CallerMemberName] String? property = null);
        public T InterceptGet<T>(TSender sender, Boolean @base, [CallerMemberName] String? property = null);
        public T InterceptGet<T>(TSender sender, TInfo? info, Boolean @base, [CallerMemberName] String? property = null);
        public T InterceptGet<T>(TSender sender, TArgument args);
        public void InterceptSet<T>(TSender sender, T value, [CallerMemberName] String? property = null);
        public void InterceptSet<T>(TSender sender, TInfo? info, T value, [CallerMemberName] String? property = null);
        public void InterceptSet<T>(TSender sender, T value, Boolean @base, [CallerMemberName] String? property = null);
        public void InterceptSet<T>(TSender sender, TInfo? info, T value, Boolean @base, [CallerMemberName] String? property = null);
        public void InterceptSet<T>(TSender sender, T value, Boolean @base, Boolean init, [CallerMemberName] String? property = null);
        public void InterceptSet<T>(TSender sender, TInfo? info, T value, Boolean @base, Boolean init, [CallerMemberName] String? property = null);
        public void InterceptInit<T>(TSender sender, T value, [CallerMemberName] String? property = null);
        public void InterceptInit<T>(TSender sender, TInfo? info, T value, [CallerMemberName] String? property = null);
        public void InterceptInit<T>(TSender sender, T value, Boolean @base, [CallerMemberName] String? property = null);
        public void InterceptInit<T>(TSender sender, TInfo? info, T value, Boolean @base, [CallerMemberName] String? property = null);
        public void InterceptSet<T>(TSender sender, T value, TArgument args);
    }
}