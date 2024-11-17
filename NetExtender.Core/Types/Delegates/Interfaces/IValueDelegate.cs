using System;
using System.Reflection;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Delegates
{
    public interface IAsyncValueFunc<T, TResult> : IAsyncValueDelegate<T> where T : struct, IAsyncValueFunc<T, TResult>
    {
        public Task<TResult> AsTask();
        public ValueTask<TResult> AsValueTask();
        public Task<TResult> InvokeTask();
        public ValueTask<TResult> InvokeValueTask();
    }
    
    public interface IAsyncValueAction<T> : IAsyncValueDelegate<T> where T : struct, IAsyncValueAction<T>
    {
        public Task AsTask();
        public ValueTask AsValueTask();
        public Task InvokeTask();
        public ValueTask InvokeValueTask();
    }
    
    public interface IAsyncValueDelegate<T> : IValueDelegate<T> where T : struct, IAsyncValueDelegate<T>
    {
        public Boolean IsValue { get; }
    }
    
    public interface IValueAction<T> : IValueDelegate<T> where T : struct, IValueAction<T>
    {
        public new void Invoke();
    }

    public interface IValueFunc<T, out TResult> : IValueDelegate<T> where T : struct, IValueFunc<T, TResult>
    {
        public new TResult Invoke();
    }

    public interface IValueDelegate<T> : IEquatable<T> where T : struct, IValueDelegate<T>
    {
        public MethodInfo? Method { get; }
        public Int32 Arguments { get; }
        public Object?[] GetArguments();
        public Object? Invoke();
        public Object? this[Int32 argument] { get; set; }
    }
}