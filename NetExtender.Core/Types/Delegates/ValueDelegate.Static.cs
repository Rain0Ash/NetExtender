// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Delegates
{
    public static class ValueDelegate
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueAction Create(Action @delegate)
        {
            return new ValueAction(@delegate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueAction<T> Create<T>(Action<T> @delegate, T argument)
        {
            return new ValueAction<T>(@delegate, argument);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueAction<T1, T2> Create<T1, T2>(Action<T1, T2> @delegate, T1 first, T2 second)
        {
            return new ValueAction<T1, T2>(@delegate, first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueAction<T1, T2, T3> Create<T1, T2, T3>(Action<T1, T2, T3> @delegate, T1 first, T2 second, T3 third)
        {
            return new ValueAction<T1, T2, T3>(@delegate, first, second, third);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueAction<T1, T2, T3, T4> Create<T1, T2, T3, T4>(Action<T1, T2, T3, T4> @delegate, T1 first, T2 second, T3 third, T4 fourth)
        {
            return new ValueAction<T1, T2, T3, T4>(@delegate, first, second, third, fourth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueAction<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            return new ValueAction<T1, T2, T3, T4, T5>(@delegate, first, second, third, fourth, fifth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueAction<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            return new ValueAction<T1, T2, T3, T4, T5, T6>(@delegate, first, second, third, fourth, fifth, sixth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueAction<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            return new ValueAction<T1, T2, T3, T4, T5, T6, T7>(@delegate, first, second, third, fourth, fifth, sixth, seventh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueAction<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            return new ValueAction<T1, T2, T3, T4, T5, T6, T7, T8>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            return new ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            return new ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            return new ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            return new ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            return new ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            return new ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            return new ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            return new ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueFunc<TResult> Create<TResult>(Func<TResult> @delegate)
        {
            return new ValueFunc<TResult>(@delegate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueFunc<T, TResult> Create<T, TResult>(Func<T, TResult> @delegate, T argument)
        {
            return new ValueFunc<T, TResult>(@delegate, argument);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueFunc<T1, T2, TResult> Create<T1, T2, TResult>(Func<T1, T2, TResult> @delegate, T1 first, T2 second)
        {
            return new ValueFunc<T1, T2, TResult>(@delegate, first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueFunc<T1, T2, T3, TResult> Create<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> @delegate, T1 first, T2 second, T3 third)
        {
            return new ValueFunc<T1, T2, T3, TResult>(@delegate, first, second, third);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueFunc<T1, T2, T3, T4, TResult> Create<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth)
        {
            return new ValueFunc<T1, T2, T3, T4, TResult>(@delegate, first, second, third, fourth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueFunc<T1, T2, T3, T4, T5, TResult> Create<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            return new ValueFunc<T1, T2, T3, T4, T5, TResult>(@delegate, first, second, third, fourth, fifth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueFunc<T1, T2, T3, T4, T5, T6, TResult> Create<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            return new ValueFunc<T1, T2, T3, T4, T5, T6, TResult>(@delegate, first, second, third, fourth, fifth, sixth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueFunc<T1, T2, T3, T4, T5, T6, T7, TResult> Create<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            return new ValueFunc<T1, T2, T3, T4, T5, T6, T7, TResult>(@delegate, first, second, third, fourth, fifth, sixth, seventh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            return new ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            return new ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            return new ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            return new ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            return new ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            return new ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            return new ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            return new ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            return new ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueAction<ValueFunc<Task>> CreateAsync(Func<Task> @delegate)
        {
            return Create(@delegate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueAction<ValueFunc<T, Task>> CreateAsync<T>(Func<T, Task> @delegate, T argument)
        {
            return Create(@delegate, argument);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueAction<ValueFunc<T1, T2, Task>> CreateAsync<T1, T2>(Func<T1, T2, Task> @delegate, T1 first, T2 second)
        {
            return Create(@delegate, first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueAction<ValueFunc<T1, T2, T3, Task>> CreateAsync<T1, T2, T3>(Func<T1, T2, T3, Task> @delegate, T1 first, T2 second, T3 third)
        {
            return Create(@delegate, first, second, third);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueAction<ValueFunc<T1, T2, T3, T4, Task>> CreateAsync<T1, T2, T3, T4>(Func<T1, T2, T3, T4, Task> @delegate, T1 first, T2 second, T3 third, T4 fourth)
        {
            return Create(@delegate, first, second, third, fourth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, Task>> CreateAsync<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, Task> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            return Create(@delegate, first, second, third, fourth, fifth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, Task>> CreateAsync<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, Task> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, Task>> CreateAsync<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, Task> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, Task>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, Task> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueAction<ValueFunc<ValueTask>> CreateAsync(Func<ValueTask> @delegate)
        {
            return Create(@delegate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueAction<ValueFunc<T, ValueTask>> CreateAsync<T>(Func<T, ValueTask> @delegate, T argument)
        {
            return Create(@delegate, argument);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueAction<ValueFunc<T1, T2, ValueTask>> CreateAsync<T1, T2>(Func<T1, T2, ValueTask> @delegate, T1 first, T2 second)
        {
            return Create(@delegate, first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueAction<ValueFunc<T1, T2, T3, ValueTask>> CreateAsync<T1, T2, T3>(Func<T1, T2, T3, ValueTask> @delegate, T1 first, T2 second, T3 third)
        {
            return Create(@delegate, first, second, third);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueAction<ValueFunc<T1, T2, T3, T4, ValueTask>> CreateAsync<T1, T2, T3, T4>(Func<T1, T2, T3, T4, ValueTask> @delegate, T1 first, T2 second, T3 third, T4 fourth)
        {
            return Create(@delegate, first, second, third, fourth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, ValueTask>> CreateAsync<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, ValueTask> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            return Create(@delegate, first, second, third, fourth, fifth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, ValueTask>> CreateAsync<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, ValueTask> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, ValueTask>> CreateAsync<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, ValueTask> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, ValueTask>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, ValueTask> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, ValueTask>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, ValueTask> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, ValueTask>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, ValueTask> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, ValueTask>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, ValueTask> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, ValueTask>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, ValueTask> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, ValueTask>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, ValueTask> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, ValueTask>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, ValueTask> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, ValueTask>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, ValueTask> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueAction<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, ValueTask>> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, ValueTask> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueFunc<ValueFunc<Task<TResult>>, TResult> CreateAsync<TResult>(Func<Task<TResult>> @delegate)
        {
            return Create(@delegate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueFunc<ValueFunc<T, Task<TResult>>, TResult> CreateAsync<T, TResult>(Func<T, Task<TResult>> @delegate, T argument)
        {
            return Create(@delegate, argument);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueFunc<ValueFunc<T1, T2, Task<TResult>>, TResult> CreateAsync<T1, T2, TResult>(Func<T1, T2, Task<TResult>> @delegate, T1 first, T2 second)
        {
            return Create(@delegate, first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueFunc<ValueFunc<T1, T2, T3, Task<TResult>>, TResult> CreateAsync<T1, T2, T3, TResult>(Func<T1, T2, T3, Task<TResult>> @delegate, T1 first, T2 second, T3 third)
        {
            return Create(@delegate, first, second, third);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueFunc<ValueFunc<T1, T2, T3, T4, Task<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, Task<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth)
        {
            return Create(@delegate, first, second, third, fourth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, Task<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, Task<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            return Create(@delegate, first, second, third, fourth, fifth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, Task<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, Task<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, Task<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, Task<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, Task<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, Task<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueFunc<ValueFunc<ValueTask<TResult>>, TResult> CreateAsync<TResult>(Func<ValueTask<TResult>> @delegate)
        {
            return Create(@delegate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueFunc<ValueFunc<T, ValueTask<TResult>>, TResult> CreateAsync<T, TResult>(Func<T, ValueTask<TResult>> @delegate, T argument)
        {
            return Create(@delegate, argument);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueFunc<ValueFunc<T1, T2, ValueTask<TResult>>, TResult> CreateAsync<T1, T2, TResult>(Func<T1, T2, ValueTask<TResult>> @delegate, T1 first, T2 second)
        {
            return Create(@delegate, first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueFunc<ValueFunc<T1, T2, T3, ValueTask<TResult>>, TResult> CreateAsync<T1, T2, T3, TResult>(Func<T1, T2, T3, ValueTask<TResult>> @delegate, T1 first, T2 second, T3 third)
        {
            return Create(@delegate, first, second, third);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueFunc<ValueFunc<T1, T2, T3, T4, ValueTask<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, ValueTask<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth)
        {
            return Create(@delegate, first, second, third, fourth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, ValueTask<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, ValueTask<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            return Create(@delegate, first, second, third, fourth, fifth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, ValueTask<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, ValueTask<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, ValueTask<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, ValueTask<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, ValueTask<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, ValueTask<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, ValueTask<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, ValueTask<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, ValueTask<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, ValueTask<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, ValueTask<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, ValueTask<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, ValueTask<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, ValueTask<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, ValueTask<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, ValueTask<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, ValueTask<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, ValueTask<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, ValueTask<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, ValueTask<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncValueTaskValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, ValueTask<TResult>>, TResult> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, ValueTask<TResult>> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            return Create(@delegate, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth);
        }
    }
}