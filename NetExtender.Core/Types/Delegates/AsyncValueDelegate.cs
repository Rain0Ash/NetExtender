// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Delegates
{
    public struct AsyncTaskValueAction<TDelegate> : IAsyncValueAction<AsyncTaskValueAction<TDelegate>> where TDelegate : struct, IValueFunc<TDelegate, Task>
    {
        public static implicit operator AsyncTaskValueAction<TDelegate>(TDelegate value)
        {
            return new AsyncTaskValueAction<TDelegate>(value);
        }
        
        public static implicit operator TDelegate(AsyncTaskValueAction<TDelegate> value)
        {
            return value._internal;
        }
        
        public static Boolean operator ==(AsyncTaskValueAction<TDelegate> first, AsyncTaskValueAction<TDelegate> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(AsyncTaskValueAction<TDelegate> first, AsyncTaskValueAction<TDelegate> second)
        {
            return !(first == second);
        }

        private TDelegate _internal;

        public MethodInfo? Method
        {
            get
            {
                return _internal.Method;
            }
        }

        public Boolean IsValue
        {
            get
            {
                return false;
            }
        }

        public Int32 Arguments
        {
            get
            {
                return _internal.Arguments;
            }
        }

        public AsyncTaskValueAction(TDelegate @delegate)
        {
            _internal = @delegate;
        }

        public Object?[] GetArguments()
        {
            return _internal.GetArguments();
        }

        public Task AsTask()
        {
            return _internal.Invoke();
        }

        public ValueTask AsValueTask()
        {
            return new ValueTask(_internal.Invoke());
        }

        public async Task InvokeTask()
        {
            await _internal.Invoke();
        }

        public async ValueTask InvokeValueTask()
        {
            await _internal.Invoke();
        }

        Object IValueDelegate<AsyncTaskValueAction<TDelegate>>.Invoke()
        {
            return AsTask();
        }

        public override Int32 GetHashCode()
        {
            return _internal.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(AsyncTaskValueAction<TDelegate> other)
        {
            return _internal.Equals(other._internal);
        }

        public override String? ToString()
        {
            return _internal.ToString();
        }

        public Object? this[Int32 argument]
        {
            get
            {
                return _internal[argument];
            }
            set
            {
                _internal[argument] = value;
            }
        }
    }
    
    public struct AsyncValueTaskValueAction<TDelegate> : IAsyncValueAction<AsyncValueTaskValueAction<TDelegate>> where TDelegate : struct, IValueFunc<TDelegate, ValueTask>
    {
        public static implicit operator AsyncValueTaskValueAction<TDelegate>(TDelegate value)
        {
            return new AsyncValueTaskValueAction<TDelegate>(value);
        }
        
        public static implicit operator TDelegate(AsyncValueTaskValueAction<TDelegate> value)
        {
            return value._internal;
        }
        
        public static Boolean operator ==(AsyncValueTaskValueAction<TDelegate> first, AsyncValueTaskValueAction<TDelegate> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(AsyncValueTaskValueAction<TDelegate> first, AsyncValueTaskValueAction<TDelegate> second)
        {
            return !(first == second);
        }

        private TDelegate _internal;

        public MethodInfo? Method
        {
            get
            {
                return _internal.Method;
            }
        }

        public Boolean IsValue
        {
            get
            {
                return true;
            }
        }

        public Int32 Arguments
        {
            get
            {
                return _internal.Arguments;
            }
        }

        public AsyncValueTaskValueAction(TDelegate @delegate)
        {
            _internal = @delegate;
        }

        public Object?[] GetArguments()
        {
            return _internal.GetArguments();
        }

        public Task AsTask()
        {
            return _internal.Invoke().AsTask();
        }

        public ValueTask AsValueTask()
        {
            return _internal.Invoke();
        }

        public async Task InvokeTask()
        {
            await _internal.Invoke();
        }

        public async ValueTask InvokeValueTask()
        {
            await _internal.Invoke();
        }

        Object IValueDelegate<AsyncValueTaskValueAction<TDelegate>>.Invoke()
        {
            return AsTask();
        }

        public override Int32 GetHashCode()
        {
            return _internal.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(AsyncValueTaskValueAction<TDelegate> other)
        {
            return _internal.Equals(other._internal);
        }

        public override String? ToString()
        {
            return _internal.ToString();
        }

        public Object? this[Int32 argument]
        {
            get
            {
                return _internal[argument];
            }
            set
            {
                _internal[argument] = value;
            }
        }
    }
    
    public struct AsyncTaskValueFunc<TDelegate, TResult> : IAsyncValueFunc<AsyncTaskValueFunc<TDelegate, TResult>, TResult> where TDelegate : struct, IValueFunc<TDelegate, Task<TResult>>
    {
        public static implicit operator AsyncTaskValueFunc<TDelegate, TResult>(TDelegate value)
        {
            return new AsyncTaskValueFunc<TDelegate, TResult>(value);
        }
        
        public static implicit operator TDelegate(AsyncTaskValueFunc<TDelegate, TResult> value)
        {
            return value._internal;
        }
        
        public static Boolean operator ==(AsyncTaskValueFunc<TDelegate, TResult> first, AsyncTaskValueFunc<TDelegate, TResult> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(AsyncTaskValueFunc<TDelegate, TResult> first, AsyncTaskValueFunc<TDelegate, TResult> second)
        {
            return !(first == second);
        }

        private TDelegate _internal;

        public MethodInfo? Method
        {
            get
            {
                return _internal.Method;
            }
        }

        public Boolean IsValue
        {
            get
            {
                return false;
            }
        }

        public Int32 Arguments
        {
            get
            {
                return _internal.Arguments;
            }
        }

        public AsyncTaskValueFunc(TDelegate @delegate)
        {
            _internal = @delegate;
        }

        public Object?[] GetArguments()
        {
            return _internal.GetArguments();
        }

        public Task<TResult> AsTask()
        {
            return _internal.Invoke();
        }

        public ValueTask<TResult> AsValueTask()
        {
            return new ValueTask<TResult>(_internal.Invoke());
        }

        public async Task<TResult> InvokeTask()
        {
            return await _internal.Invoke();
        }

        public async ValueTask<TResult> InvokeValueTask()
        {
            return await _internal.Invoke();
        }

        Object IValueDelegate<AsyncTaskValueFunc<TDelegate, TResult>>.Invoke()
        {
            return AsTask();
        }

        public override Int32 GetHashCode()
        {
            return _internal.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(AsyncTaskValueFunc<TDelegate, TResult> other)
        {
            return _internal.Equals(other._internal);
        }

        public override String? ToString()
        {
            return _internal.ToString();
        }

        public Object? this[Int32 argument]
        {
            get
            {
                return _internal[argument];
            }
            set
            {
                _internal[argument] = value;
            }
        }
    }
    
    public struct AsyncValueTaskValueFunc<TDelegate, TResult> : IAsyncValueFunc<AsyncValueTaskValueFunc<TDelegate, TResult>, TResult> where TDelegate : struct, IValueFunc<TDelegate, ValueTask<TResult>>
    {
        public static implicit operator AsyncValueTaskValueFunc<TDelegate, TResult>(TDelegate value)
        {
            return new AsyncValueTaskValueFunc<TDelegate, TResult>(value);
        }
        
        public static implicit operator TDelegate(AsyncValueTaskValueFunc<TDelegate, TResult> value)
        {
            return value._internal;
        }
        
        public static Boolean operator ==(AsyncValueTaskValueFunc<TDelegate, TResult> first, AsyncValueTaskValueFunc<TDelegate, TResult> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(AsyncValueTaskValueFunc<TDelegate, TResult> first, AsyncValueTaskValueFunc<TDelegate, TResult> second)
        {
            return !(first == second);
        }

        private TDelegate _internal;

        public MethodInfo? Method
        {
            get
            {
                return _internal.Method;
            }
        }

        public Boolean IsValue
        {
            get
            {
                return true;
            }
        }

        public Int32 Arguments
        {
            get
            {
                return _internal.Arguments;
            }
        }

        public AsyncValueTaskValueFunc(TDelegate @delegate)
        {
            _internal = @delegate;
        }

        public Object?[] GetArguments()
        {
            return _internal.GetArguments();
        }

        public Task<TResult> AsTask()
        {
            return _internal.Invoke().AsTask();
        }

        public ValueTask<TResult> AsValueTask()
        {
            return _internal.Invoke();
        }

        public async Task<TResult> InvokeTask()
        {
            return await _internal.Invoke();
        }

        public async ValueTask<TResult> InvokeValueTask()
        {
            return await _internal.Invoke();
        }

        Object IValueDelegate<AsyncValueTaskValueFunc<TDelegate, TResult>>.Invoke()
        {
            return AsTask();
        }

        public override Int32 GetHashCode()
        {
            return _internal.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(AsyncValueTaskValueFunc<TDelegate, TResult> other)
        {
            return _internal.Equals(other._internal);
        }

        public override String? ToString()
        {
            return _internal.ToString();
        }

        public Object? this[Int32 argument]
        {
            get
            {
                return _internal[argument];
            }
            set
            {
                _internal[argument] = value;
            }
        }
    }
}