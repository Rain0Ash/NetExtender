using System;
using System.Threading.Tasks;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Delegates;

namespace NetExtender.Types.Interception.Interfaces
{
    public interface IAnyMethodInterceptor<in TSender, in TInfo> : IAnyMethodInterceptor<TSender, IMethodInterceptEventArgs, TInfo>, IMethodInterceptor<TSender, TInfo>, IAsyncMethodInterceptor<TSender, TInfo>
    {
    }
    
    public interface IAnyMethodInterceptor<in TSender, in TArgument, in TInfo> : IMethodInterceptor<TSender, TArgument, TInfo>, IAsyncMethodInterceptor<TSender, TArgument, TInfo>
    {
    }
    
    public interface IAsyncMethodInterceptor<in TSender, in TInfo> : IAsyncMethodInterceptor<TSender, IMethodInterceptEventArgs, TInfo>
    {
    }
    
    public interface IAsyncMethodInterceptor<in TSender, in TArgument, in TInfo>
    {
        public ValueTask InterceptAsync<TDelegate>(TSender sender, TArgument args, TDelegate @delegate) where TDelegate : struct, IAsyncValueAction<TDelegate>;
        public ValueTask InterceptAsync(TSender sender, TInfo? info, Func<Task> method);
        public ValueTask InterceptAsync<T>(TSender sender, TInfo? info, Func<T, Task> method, T argument);
        public ValueTask InterceptAsync<T1, T2>(TSender sender, TInfo? info, Func<T1, T2, Task> method, T1 first, T2 second);
        public ValueTask InterceptAsync<T1, T2, T3>(TSender sender, TInfo? info, Func<T1, T2, T3, Task> method, T1 first, T2 second, T3 third);
        public ValueTask InterceptAsync<T1, T2, T3, T4>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, Task> method, T1 first, T2 second, T3 third, T4 fourth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth);
        
        public ValueTask InterceptAsync(TSender sender, TInfo? info, Func<ValueTask> method);
        public ValueTask InterceptAsync<T>(TSender sender, TInfo? info, Func<T, ValueTask> method, T argument);
        public ValueTask InterceptAsync<T1, T2>(TSender sender, TInfo? info, Func<T1, T2, ValueTask> method, T1 first, T2 second);
        public ValueTask InterceptAsync<T1, T2, T3>(TSender sender, TInfo? info, Func<T1, T2, T3, ValueTask> method, T1 first, T2 second, T3 third);
        public ValueTask InterceptAsync<T1, T2, T3, T4>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth);
        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth);
        
        public ValueTask<TResult> InterceptAsync<TDelegate, TResult>(TSender sender, TArgument args, TDelegate @delegate) where TDelegate : struct, IAsyncValueFunc<TDelegate, TResult>;
        public ValueTask<TResult> InterceptAsync<TResult>(TSender sender, TInfo? info, Func<Task<TResult>> method);
        public ValueTask<TResult> InterceptAsync<T, TResult>(TSender sender, TInfo? info, Func<T, Task<TResult>> method, T argument);
        public ValueTask<TResult> InterceptAsync<T1, T2, TResult>(TSender sender, TInfo? info, Func<T1, T2, Task<TResult>> method, T1 first, T2 second);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, Task<TResult>> method, T1 first, T2 second, T3 third);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth);

        public ValueTask<TResult> InterceptAsync<TResult>(TSender sender, TInfo? info, Func<ValueTask<TResult>> method);
        public ValueTask<TResult> InterceptAsync<T, TResult>(TSender sender, TInfo? info, Func<T, ValueTask<TResult>> method, T argument);
        public ValueTask<TResult> InterceptAsync<T1, T2, TResult>(TSender sender, TInfo? info, Func<T1, T2, ValueTask<TResult>> method, T1 first, T2 second);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, ValueTask<TResult>> method, T1 first, T2 second, T3 third);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth);
        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth);
    }
    
    public interface IMethodInterceptor<in TSender, in TInfo> : IMethodInterceptor<TSender, IMethodInterceptEventArgs, TInfo>
    {
    }
    
    public interface IMethodInterceptor<in TSender, in TArgument, in TInfo>
    {
        public void Intercept<TDelegate>(TSender sender, TArgument args, TDelegate @delegate) where TDelegate : struct, IValueAction<TDelegate>;
        public void Intercept(TSender sender, TInfo? info, Action method);
        public void Intercept<T>(TSender sender, TInfo? info, Action<T> method, T argument);
        public void Intercept<T1, T2>(TSender sender, TInfo? info, Action<T1, T2> method, T1 first, T2 second);
        public void Intercept<T1, T2, T3>(TSender sender, TInfo? info, Action<T1, T2, T3> method, T1 first, T2 second, T3 third);
        public void Intercept<T1, T2, T3, T4>(TSender sender, TInfo? info, Action<T1, T2, T3, T4> method, T1 first, T2 second, T3 third, T4 fourth);
        public void Intercept<T1, T2, T3, T4, T5>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth);
        public void Intercept<T1, T2, T3, T4, T5, T6>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth);
        public void Intercept<T1, T2, T3, T4, T5, T6, T7>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh);
        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth);
        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth);
        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth);
        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh);
        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth);
        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth);
        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth);
        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth);
        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth);

        public TResult Intercept<TDelegate, TResult>(TSender sender, TArgument args, TDelegate @delegate) where TDelegate : struct, IValueFunc<TDelegate, TResult>;
        public TResult Intercept<TDelegate, TResult>(TSender sender, TArgument args, TDelegate @delegate, Maybe<TResult> result) where TDelegate : struct, IValueFunc<TDelegate, TResult>;
        public TResult Intercept<TDelegate, TResult>(TSender sender, TArgument args, TDelegate @delegate, out Maybe<TResult> result) where TDelegate : struct, IValueFunc<TDelegate, TResult>;
        public TResult Intercept<TResult>(TSender sender, TInfo? info, Func<TResult> method);
        public TResult Intercept<T, TResult>(TSender sender, TInfo? info, Func<T, TResult> method, T argument);
        public TResult Intercept<T1, T2, TResult>(TSender sender, TInfo? info, Func<T1, T2, TResult> method, T1 first, T2 second);
        public TResult Intercept<T1, T2, T3, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, TResult> method, T1 first, T2 second, T3 third);
        public TResult Intercept<T1, T2, T3, T4, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, TResult> method, T1 first, T2 second, T3 third, T4 fourth);
        public TResult Intercept<T1, T2, T3, T4, T5, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth);
        public TResult Intercept<T1, T2, T3, T4, T5, T6, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth);
        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh);
        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth);
        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth);
        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth);
        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh);
        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth);
        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth);
        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth);
        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth);
        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth);
    }
}