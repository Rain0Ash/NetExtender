using System;
using System.Reflection;
using System.Threading.Tasks;
using NetExtender.Types.Interception.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Delegates;

namespace NetExtender.Types.Interception
{
    public class AnyMemberInterceptor<TSender, TInfo> : AnyMemberInterceptor<TSender, IPropertyInterceptEventArgs, IMethodInterceptEventArgs, TInfo>, IAnyMemberInterceptor<TSender, TInfo> where TSender : IInterceptTargetRaise<IPropertyInterceptEventArgs>, IInterceptTargetRaise<IMethodInterceptEventArgs>
    {
        public static AnyMemberInterceptor<TSender, TInfo> Default { get; } = new AnyMemberInterceptor<TSender, TInfo>();
        
        public AnyMemberInterceptor()
            : base(PropertyInterceptor<TSender, TInfo>.Default, MethodInterceptor<TSender, TInfo>.Default)
        {
        }
    }

    public class AnyMemberInterceptor<TSender, TPropertyArgument, TMethodArgument, TInfo> : IAnyMemberInterceptor<TSender, TPropertyArgument, TMethodArgument, TInfo> where TSender : IInterceptTargetRaise<TPropertyArgument>, IInterceptTargetRaise<TMethodArgument> where TPropertyArgument : class, IPropertyInterceptEventArgs where TMethodArgument : class, IMethodInterceptEventArgs
    {
        private IPropertyInterceptor<TSender, TPropertyArgument, TInfo> PropertyInterceptor { get; }
        private IAnyMethodInterceptor<TSender, TMethodArgument, TInfo> MethodInterceptor { get; }
        
        public AnyMemberInterceptor(IPropertyInterceptEventArgsFactory<TPropertyArgument, TInfo>? property, IMethodInterceptEventArgsFactory<TMethodArgument, TInfo>? method)
        {
            PropertyInterceptor = new PropertyInterceptor<TSender, TPropertyArgument, TInfo> { Factory = property };
            MethodInterceptor = new MethodInterceptor<TSender, TMethodArgument, TInfo> { Factory = method };
        }

        public AnyMemberInterceptor(IPropertyInterceptor<TSender, TPropertyArgument, TInfo> property, IAnyMethodInterceptor<TSender, TMethodArgument, TInfo> method)
        {
            PropertyInterceptor = property ?? throw new ArgumentNullException(nameof(property));
            MethodInterceptor = method ?? throw new ArgumentNullException(nameof(method));
        }
        
        public T InterceptGet<T>(TSender sender, String? property = null)
        {
            return PropertyInterceptor.InterceptGet<T>(sender, property);
        }
        
        public T InterceptGet<T>(TSender sender, TInfo? info, String? property = null)
        {
            return PropertyInterceptor.InterceptGet<T>(sender, info, property);
        }
        
        public T InterceptGet<T>(TSender sender, Boolean @base, String? property = null)
        {
            return PropertyInterceptor.InterceptGet<T>(sender, @base, property);
        }
        
        public T InterceptGet<T>(TSender sender, TInfo? info, Boolean @base, String? property = null)
        {
            return PropertyInterceptor.InterceptGet<T>(sender, info, @base, property);
        }

        public T InterceptGet<T>(TSender sender, TPropertyArgument args)
        {
            return PropertyInterceptor.InterceptGet<T>(sender, args);
        }

        public void InterceptSet<T>(TSender sender, T value, String? property = null)
        {
            PropertyInterceptor.InterceptSet(sender, value, property);
        }

        public void InterceptSet<T>(TSender sender, TInfo? info, T value, String? property = null)
        {
            PropertyInterceptor.InterceptSet(sender, info, value, property);
        }

        public void InterceptSet<T>(TSender sender, T value, Boolean @base, String? property = null)
        {
            PropertyInterceptor.InterceptSet(sender, value, @base, property);
        }

        public void InterceptSet<T>(TSender sender, TInfo? info, T value, Boolean @base, String? property = null)
        {
            PropertyInterceptor.InterceptSet(sender, info, value, @base, property);
        }

        public void InterceptSet<T>(TSender sender, T value, Boolean @base, Boolean init, String? property = null)
        {
            PropertyInterceptor.InterceptSet(sender, value, @base, init, property);
        }

        public void InterceptSet<T>(TSender sender, TInfo? info, T value, Boolean @base, Boolean init, String? property = null)
        {
            PropertyInterceptor.InterceptSet(sender, info, value, @base, init, property);
        }

        public void InterceptInit<T>(TSender sender, T value, String? property = null)
        {
            PropertyInterceptor.InterceptInit(sender, value, property);
        }

        public void InterceptInit<T>(TSender sender, TInfo? info, T value, String? property = null)
        {
            PropertyInterceptor.InterceptInit(sender, info, value, property);
        }

        public void InterceptInit<T>(TSender sender, T value, Boolean @base, String? property = null)
        {
            PropertyInterceptor.InterceptInit(sender, value, @base, property);
        }

        public void InterceptInit<T>(TSender sender, TInfo? info, T value, Boolean @base, String? property = null)
        {
            PropertyInterceptor.InterceptInit(sender, info, value, @base, property);
        }

        public void InterceptSet<T>(TSender sender, T value, TPropertyArgument args)
        {
            PropertyInterceptor.InterceptSet(sender, value, args);
        }

        public void Intercept<TDelegate>(TSender sender, TMethodArgument args, TDelegate @delegate) where TDelegate : struct, IValueAction<TDelegate>
        {
            MethodInterceptor.Intercept(sender, args, @delegate);
        }

        public void Intercept(TSender sender, TInfo? info, Action method)
        {
            MethodInterceptor.Intercept(sender, info, method);
        }

        public void Intercept<T>(TSender sender, TInfo? info, Action<T> method, T argument)
        {
            MethodInterceptor.Intercept(sender, info, method, argument);
        }

        public void Intercept<T1, T2>(TSender sender, TInfo? info, Action<T1, T2> method, T1 first, T2 second)
        {
            MethodInterceptor.Intercept(sender, info, method, first, second);
        }

        public void Intercept<T1, T2, T3>(TSender sender, TInfo? info, Action<T1, T2, T3> method, T1 first, T2 second, T3 third)
        {
            MethodInterceptor.Intercept(sender, info, method, first, second, third);
        }

        public void Intercept<T1, T2, T3, T4>(TSender sender, TInfo? info, Action<T1, T2, T3, T4> method, T1 first, T2 second, T3 third, T4 fourth)
        {
            MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth);
        }

        public void Intercept<T1, T2, T3, T4, T5>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth);
        }

        public void Intercept<T1, T2, T3, T4, T5, T6>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth);
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh);
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth);
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth);
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth);
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh);
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth);
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth);
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth);
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth);
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth);
        }

        public TResult Intercept<TDelegate, TResult>(TSender sender, TMethodArgument args, TDelegate @delegate) where TDelegate : struct, IValueFunc<TDelegate, TResult>
        {
            return MethodInterceptor.Intercept<TDelegate, TResult>(sender, args, @delegate);
        }

        public TResult Intercept<TDelegate, TResult>(TSender sender, TMethodArgument args, TDelegate @delegate, Maybe<TResult> result) where TDelegate : struct, IValueFunc<TDelegate, TResult>
        {
            return MethodInterceptor.Intercept(sender, args, @delegate, result);
        }

        public TResult Intercept<TDelegate, TResult>(TSender sender, TMethodArgument args, TDelegate @delegate, out Maybe<TResult> result) where TDelegate : struct, IValueFunc<TDelegate, TResult>
        {
            return MethodInterceptor.Intercept(sender, args, @delegate, out result);
        }

        public TResult Intercept<TResult>(TSender sender, TInfo? info, Func<TResult> method)
        {
            return MethodInterceptor.Intercept(sender, info, method);
        }

        public TResult Intercept<T, TResult>(TSender sender, TInfo? info, Func<T, TResult> method, T argument)
        {
            return MethodInterceptor.Intercept(sender, info, method, argument);
        }

        public TResult Intercept<T1, T2, TResult>(TSender sender, TInfo? info, Func<T1, T2, TResult> method, T1 first, T2 second)
        {
            return MethodInterceptor.Intercept(sender, info, method, first, second);
        }

        public TResult Intercept<T1, T2, T3, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, TResult> method, T1 first, T2 second, T3 third)
        {
            return MethodInterceptor.Intercept(sender, info, method, first, second, third);
        }

        public TResult Intercept<T1, T2, T3, T4, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, TResult> method, T1 first, T2 second, T3 third, T4 fourth)
        {
            return MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth);
        }

        public TResult Intercept<T1, T2, T3, T4, T5, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            return MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth);
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            return MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth);
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            return MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh);
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            return MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth);
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            return MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth);
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            return MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth);
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            return MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh);
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            return MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth);
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            return MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth);
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            return MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth);
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            return MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth);
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            return MethodInterceptor.Intercept(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth);
        }

        public ValueTask InterceptAsync<TDelegate>(TSender sender, TMethodArgument args, TDelegate @delegate) where TDelegate : struct, IAsyncValueAction<TDelegate>
        {
            return MethodInterceptor.InterceptAsync(sender, args, @delegate);
        }

        public ValueTask InterceptAsync(TSender sender, TInfo? info, Func<Task> method)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method);
        }

        public ValueTask InterceptAsync<T>(TSender sender, TInfo? info, Func<T, Task> method, T argument)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, argument);
        }

        public ValueTask InterceptAsync<T1, T2>(TSender sender, TInfo? info, Func<T1, T2, Task> method, T1 first, T2 second)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second);
        }

        public ValueTask InterceptAsync<T1, T2, T3>(TSender sender, TInfo? info, Func<T1, T2, T3, Task> method, T1 first, T2 second, T3 third)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, Task> method, T1 first, T2 second, T3 third, T4 fourth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth);
        }

        public ValueTask InterceptAsync(TSender sender, TInfo? info, Func<ValueTask> method)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method);
        }

        public ValueTask InterceptAsync<T>(TSender sender, TInfo? info, Func<T, ValueTask> method, T argument)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, argument);
        }

        public ValueTask InterceptAsync<T1, T2>(TSender sender, TInfo? info, Func<T1, T2, ValueTask> method, T1 first, T2 second)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second);
        }

        public ValueTask InterceptAsync<T1, T2, T3>(TSender sender, TInfo? info, Func<T1, T2, T3, ValueTask> method, T1 first, T2 second, T3 third)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth);
        }

        public ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth);
        }

        public ValueTask<TResult> InterceptAsync<TDelegate, TResult>(TSender sender, TMethodArgument args, TDelegate @delegate) where TDelegate : struct, IAsyncValueFunc<TDelegate, TResult>
        {
            return MethodInterceptor.InterceptAsync<TDelegate, TResult>(sender, args, @delegate);
        }

        public ValueTask<TResult> InterceptAsync<TResult>(TSender sender, TInfo? info, Func<Task<TResult>> method)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method);
        }

        public ValueTask<TResult> InterceptAsync<T, TResult>(TSender sender, TInfo? info, Func<T, Task<TResult>> method, T argument)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, argument);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, TResult>(TSender sender, TInfo? info, Func<T1, T2, Task<TResult>> method, T1 first, T2 second)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, Task<TResult>> method, T1 first, T2 second, T3 third)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth);
        }

        public ValueTask<TResult> InterceptAsync<TResult>(TSender sender, TInfo? info, Func<ValueTask<TResult>> method)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method);
        }

        public ValueTask<TResult> InterceptAsync<T, TResult>(TSender sender, TInfo? info, Func<T, ValueTask<TResult>> method, T argument)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, argument);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, TResult>(TSender sender, TInfo? info, Func<T1, T2, ValueTask<TResult>> method, T1 first, T2 second)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, ValueTask<TResult>> method, T1 first, T2 second, T3 third)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth);
        }

        public ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            return MethodInterceptor.InterceptAsync(sender, info, method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth);
        }
    }
    
    public abstract class MemberInterceptor<TSender, TArgument, TMember> : Interceptor<TSender, TArgument> where TSender : IInterceptTargetRaise<TArgument> where TArgument : IMemberInterceptEventArgs<TMember> where TMember : MemberInfo
    {
    }
}