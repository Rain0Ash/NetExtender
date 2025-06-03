using System;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Monads.Result;
using NetExtender.Types.Numerics;

namespace NetExtender.Utilities.Types
{
    public static class ResultUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void Verify<T>(ref Result<T> result, Span<Byte> bitmap, Byte index, ref Byte position, ref Byte count)
        {
            if (result)
            {
                return;
            }

            bitmap[position = index] = position;
            ++count;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void Verify<T, TException>(ref Result<T, TException> result, Span<Byte> bitmap, Byte index, ref Byte position, ref Byte count) where TException : Exception
        {
            if (result)
            {
                return;
            }

            bitmap[position = index] = position;
            ++count;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void Verify<T>(ref MathResult<T> result, Span<Byte> bitmap, Byte index, ref Byte position, ref Byte count) where T : struct, IEquatable<T>, IFormattable
        {
            if (result)
            {
                return;
            }

            bitmap[position = index] = position;
            ++count;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception? Exception<T>(Result<T> result)
        {
            return result.Exception;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Exception? Exception<T>(Result<T> first, Result<T> second)
        {
            const Byte size = 2;
            Span<Byte> bitmap = stackalloc Byte[size + 1];
            Byte position = 0;
            Byte count = 0;
            
            Verify(ref second, bitmap, size, ref position, ref count);
            Verify(ref first, bitmap, size - 1, ref position, ref count);

            switch (count)
            {
                case 0:
                {
                    return null;
                }
                case 1:
                {
                    return position switch
                    {
                        1 => first,
                        2 => second,
                        _ => throw new NeverOperationException()
                    };
                }
                default:
                {
                    position = 0;
                    Exception[] exceptions = new Exception[count];
                    foreach (Byte exception in bitmap)
                    {
                        if (exception <= 0)
                        {
                            continue;
                        }
                        
                        exceptions[position++] = (exception switch
                        {
                            1 => first,
                            2 => second,
                            _ => throw new NeverOperationException()
                        })!;
                    }

                    return ExceptionUtilities.FastAggregate(exceptions);
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Exception? Exception<T>(Result<T> first, Result<T> second, Result<T> third)
        {
            const Int32 size = 3;
            Span<Byte> bitmap = stackalloc Byte[size + 1];
            Byte position = 0;
            Byte count = 0;
            
            Verify(ref third, bitmap, size, ref position, ref count);
            Verify(ref second, bitmap, size - 1, ref position, ref count);
            Verify(ref first, bitmap, size - 2, ref position, ref count);

            switch (count)
            {
                case 0:
                {
                    return null;
                }
                case 1:
                {
                    return position switch
                    {
                        1 => first,
                        2 => second,
                        3 => third,
                        _ => throw new NeverOperationException()
                    };
                }
                default:
                {
                    position = 0;
                    Exception[] exceptions = new Exception[count];
                    foreach (Byte exception in bitmap)
                    {
                        if (exception <= 0)
                        {
                            continue;
                        }
                        
                        exceptions[position++] = (exception switch
                        {
                            1 => first,
                            2 => second,
                            3 => third,
                            _ => throw new NeverOperationException()
                        })!;
                    }

                    return ExceptionUtilities.FastAggregate(exceptions);
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Exception? Exception<T>(Result<T> first, Result<T> second, Result<T> third, Result<T> fourth)
        {
            const Int32 size = 4;
            Span<Byte> bitmap = stackalloc Byte[size + 1];
            Byte position = 0;
            Byte count = 0;
            
            Verify(ref fourth, bitmap, size, ref position, ref count);
            Verify(ref third, bitmap, size - 1, ref position, ref count);
            Verify(ref second, bitmap, size - 2, ref position, ref count);
            Verify(ref first, bitmap, size - 3, ref position, ref count);

            switch (count)
            {
                case 0:
                {
                    return null;
                }
                case 1:
                {
                    return position switch
                    {
                        1 => first,
                        2 => second,
                        3 => third,
                        4 => fourth,
                        _ => throw new NeverOperationException()
                    };
                }
                default:
                {
                    position = 0;
                    Exception[] exceptions = new Exception[count];
                    foreach (Byte exception in bitmap)
                    {
                        if (exception <= 0)
                        {
                            continue;
                        }
                        
                        exceptions[position++] = (exception switch
                        {
                            1 => first,
                            2 => second,
                            3 => third,
                            4 => fourth,
                            _ => throw new NeverOperationException()
                        })!;
                    }

                    return ExceptionUtilities.FastAggregate(exceptions);
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Exception? Exception<T>(Result<T> first, Result<T> second, Result<T> third, Result<T> fourth, Result<T> fifth)
        {
            const Int32 size = 5;
            Span<Byte> bitmap = stackalloc Byte[size + 1];
            Byte position = 0;
            Byte count = 0;
            
            Verify(ref fifth, bitmap, size, ref position, ref count);
            Verify(ref fourth, bitmap, size - 1, ref position, ref count);
            Verify(ref third, bitmap, size - 2, ref position, ref count);
            Verify(ref second, bitmap, size - 3, ref position, ref count);
            Verify(ref first, bitmap, size - 4, ref position, ref count);

            switch (count)
            {
                case 0:
                {
                    return null;
                }
                case 1:
                {
                    return position switch
                    {
                        1 => first,
                        2 => second,
                        3 => third,
                        4 => fourth,
                        5 => fifth,
                        _ => throw new NeverOperationException()
                    };
                }
                default:
                {
                    position = 0;
                    Exception[] exceptions = new Exception[count];
                    foreach (Byte exception in bitmap)
                    {
                        if (exception <= 0)
                        {
                            continue;
                        }
                        
                        exceptions[position++] = (exception switch
                        {
                            1 => first,
                            2 => second,
                            3 => third,
                            4 => fourth,
                            5 => fifth,
                            _ => throw new NeverOperationException()
                        })!;
                    }

                    return ExceptionUtilities.FastAggregate(exceptions);
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Exception? Exception<T>(params Result<T>[]? result)
        {
            if (result is null)
            {
                return null;
            }

            return result.Length switch
            {
                0 => null,
                1 => Exception(result[0]),
                2 => Exception(result[0], result[1]),
                3 => Exception(result[0], result[1], result[2]),
                4 => Exception(result[0], result[1], result[2], result[3]),
                5 => Exception(result[0], result[1], result[2], result[3], result[4]),
                _ => result.Select(static result => result.Exception).WhereNotNull().ToArray() switch
                {
                    { Length: 0 } => null,
                    { Length: 1 } array => array[0],
                    { } array => ExceptionUtilities.FastAggregate(array)
                },
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TException? Exception<T, TException>(Result<T, TException> result) where TException : Exception
        {
            return result.Exception;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception? Exception<T, TException>(Result<T, TException> first, Result<T, TException> second) where TException : Exception
        {
            return Exception((Result<T>) first, second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception? Exception<T, TException>(Result<T, TException> first, Result<T, TException> second, Result<T, TException> third) where TException : Exception
        {
            return Exception((Result<T>) first, second, third);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception? Exception<T, TException>(Result<T, TException> first, Result<T, TException> second, Result<T, TException> third, Result<T, TException> fourth) where TException : Exception
        {
            return Exception((Result<T>) first, second, third, fourth);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception? Exception<T, TException>(Result<T, TException> first, Result<T, TException> second, Result<T, TException> third, Result<T, TException> fourth, Result<T, TException> fifth) where TException : Exception
        {
            return Exception((Result<T>) first, second, third, fourth, fifth);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Exception? Exception<T, TException>(params Result<T, TException>[]? result) where TException : Exception
        {
            if (result is null)
            {
                return null;
            }

            return result.Length switch
            {
                0 => null,
                1 => Exception(result[0]),
                2 => Exception(result[0], result[1]),
                3 => Exception(result[0], result[1], result[2]),
                4 => Exception(result[0], result[1], result[2], result[3]),
                5 => Exception(result[0], result[1], result[2], result[3], result[4]),
                _ => result.Select(static Exception? (result) => result.Exception).WhereNotNull().ToArray() switch
                {
                    { Length: 0 } => null,
                    { Length: 1 } array => array[0],
                    { } array => ExceptionUtilities.FastAggregate(array)
                },
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TNext Match<T, TNext>(this Result<T> value, Func<TNext> successful, Func<Exception, TNext> error)
        {
            if (successful is null)
            {
                throw new ArgumentNullException(nameof(successful));
            }

            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            return value ? successful.Invoke() : error.Invoke(value.Exception!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TNext Match<T, TNext>(this Result<T> value, Func<T, TNext> successful, Func<Exception, TNext> error)
        {
            if (successful is null)
            {
                throw new ArgumentNullException(nameof(successful));
            }

            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            return value ? successful.Invoke(value) : error.Invoke(value.Exception!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TNext Match<T, TNext, TException>(this Result<T, TException> value, Func<TNext> successful, Func<TException, TNext> error) where TException : Exception
        {
            if (successful is null)
            {
                throw new ArgumentNullException(nameof(successful));
            }

            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            return value ? successful.Invoke() : error.Invoke(value.Exception!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TNext Match<T, TNext, TException>(this Result<T, TException> value, Func<T, TNext> successful, Func<TException, TNext> error) where TException : Exception
        {
            if (successful is null)
            {
                throw new ArgumentNullException(nameof(successful));
            }

            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            return value ? successful.Invoke(value) : error.Invoke(value.Exception!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TNext Match<TNext>(this BusinessResult value, Func<TNext> successful, Func<BusinessException, TNext> error)
        {
            if (successful is null)
            {
                throw new ArgumentNullException(nameof(successful));
            }

            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            return value ? successful.Invoke() : error.Invoke(value.Exception!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TNext Match<T, TNext>(this BusinessResult<T> value, Func<TNext> successful, Func<BusinessException, TNext> error)
        {
            if (successful is null)
            {
                throw new ArgumentNullException(nameof(successful));
            }

            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            return value ? successful.Invoke() : error.Invoke(value.Exception!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TNext Match<T, TNext>(this BusinessResult<T> value, Func<T, TNext> successful, Func<BusinessException, TNext> error)
        {
            if (successful is null)
            {
                throw new ArgumentNullException(nameof(successful));
            }

            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            return value ? successful.Invoke(value) : error.Invoke(value.Exception!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TNext Match<T, TNext, TBusiness>(this BusinessResult<T, TBusiness> value, Func<TNext> successful, Func<BusinessException<TBusiness>, TNext> error)
        {
            if (successful is null)
            {
                throw new ArgumentNullException(nameof(successful));
            }

            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            return value ? successful.Invoke() : error.Invoke(value.Exception!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TNext Match<T, TNext, TBusiness>(this BusinessResult<T, TBusiness> value, Func<T, TNext> successful, Func<BusinessException<TBusiness>, TNext> error)
        {
            if (successful is null)
            {
                throw new ArgumentNullException(nameof(successful));
            }

            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            return value ? successful.Invoke(value) : error.Invoke(value.Exception!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TNext Switch<T, TNext>(this Result<T> value, Func<TNext> successful, Func<Exception, TNext> error)
        {
            if (successful is null)
            {
                throw new ArgumentNullException(nameof(successful));
            }

            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            return value ? successful.Invoke() : error.Invoke(value.Exception!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Switch<T>(this Result<T> value, Action<T>? successful, Action<Exception>? error)
        {
            if (value)
            {
                successful?.Invoke(value);
                return;
            }
            
            error?.Invoke(value.Exception!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Switch<T, TException>(this Result<T, TException> value, Action? successful, Action<TException>? error) where TException : Exception
        {
            if (value)
            {
                successful?.Invoke();
                return;
            }
            
            error?.Invoke(value.Exception!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Switch<T, TException>(this Result<T, TException> value, Action<T>? successful, Action<TException>? error) where TException : Exception
        {
            if (value)
            {
                successful?.Invoke(value);
                return;
            }
            
            error?.Invoke(value.Exception!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Switch(this BusinessResult value, Action? successful, Action<BusinessException>? error)
        {
            if (value)
            {
                successful?.Invoke();
                return;
            }
            
            error?.Invoke(value.Exception!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Switch<T>(this BusinessResult<T> value, Action? successful, Action<BusinessException>? error)
        {
            if (value)
            {
                successful?.Invoke();
                return;
            }
            
            error?.Invoke(value.Exception!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Switch<T>(this BusinessResult<T> value, Action<T>? successful, Action<BusinessException>? error)
        {
            if (value)
            {
                successful?.Invoke(value);
                return;
            }
            
            error?.Invoke(value.Exception!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Switch<T, TBusiness>(this BusinessResult<T, TBusiness> value, Action? successful, Action<BusinessException<TBusiness>>? error)
        {
            if (value)
            {
                successful?.Invoke();
                return;
            }
            
            error?.Invoke(value.Exception!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Switch<T, TBusiness>(this BusinessResult<T, TBusiness> value, Action<T>? successful, Action<BusinessException<TBusiness>>? error)
        {
            if (value)
            {
                successful?.Invoke(value);
                return;
            }
            
            error?.Invoke(value.Exception!);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Then<T>(this Result<T> value, Func<T> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke() : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Then<T>(this Result<T> value, Func<T, T> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke(value) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Then<T>(this Result<T> value, Func<Result<T>> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke() : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Then<T>(this Result<T> value, Func<T, Result<T>> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke(value) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Then<T, TException>(this Result<T, TException> value, Func<T> next) where TException : Exception
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke() : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Then<T, TException>(this Result<T, TException> value, Func<T, T> next) where TException : Exception
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke(value) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Then<T, TException>(this Result<T, TException> value, Func<Result<T>> next) where TException : Exception
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke() : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Then<T, TException>(this Result<T, TException> value, Func<T, Result<T>> next) where TException : Exception
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke(value) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TException> Then<T, TException>(this Result<T, TException> value, Func<Result<T, TException>> next) where TException : Exception
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke() : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TException> Then<T, TException>(this Result<T, TException> value, Func<T, Result<T, TException>> next) where TException : Exception
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke(value) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult Then(this BusinessResult value, Func<BusinessResult> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke() : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Then<T>(this BusinessResult<T> value, Func<T> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke() : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Then<T>(this BusinessResult<T> value, Func<T, T> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke(value) : value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Then<T>(this BusinessResult<T> value, Func<Result<T>> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke() : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Then<T>(this BusinessResult<T> value, Func<T, Result<T>> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke(value) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Then<T>(this BusinessResult<T> value, Func<BusinessResult<T>> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke() : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Then<T>(this BusinessResult<T> value, Func<T, BusinessResult<T>> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke(value) : value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Then<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<Result<T>> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke() : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Then<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<T, Result<T>> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke(value) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Then<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<T> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke() : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Then<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<T, T> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke(value) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult Then<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<BusinessResult> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke() : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult Then<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<T, BusinessResult> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke(value) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Then<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<BusinessResult<T>> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke() : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Then<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<T, BusinessResult<T>> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke(value) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Then<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<BusinessResult<T, TBusiness>> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke() : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Then<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<T, BusinessResult<T, TBusiness>> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return value ? next.Invoke(value) : value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Then<T>(this Result<T> value, Action? action)
        {
            if (value)
            {
                action?.Invoke();
            }
            
            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Then<T>(this Result<T> value, Action<T>? action)
        {
            if (value)
            {
                action?.Invoke(value);
            }
            
            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TException> Then<T, TException>(this Result<T, TException> value, Action? action) where TException : Exception
        {
            if (value)
            {
                action?.Invoke();
            }
            
            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TException> Then<T, TException>(this Result<T, TException> value, Action<T>? action) where TException : Exception
        {
            if (value)
            {
                action?.Invoke(value);
            }
            
            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult Then(this BusinessResult value, Action? action)
        {
            if (value)
            {
                action?.Invoke();
            }
            
            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Then<T>(this BusinessResult<T> value, Action? action)
        {
            if (value)
            {
                action?.Invoke();
            }
            
            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Then<T>(this BusinessResult<T> value, Action<T>? action)
        {
            if (value)
            {
                action?.Invoke(value);
            }
            
            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Then<T, TBusiness>(this BusinessResult<T, TBusiness> value, Action? action)
        {
            if (value)
            {
                action?.Invoke();
            }
            
            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Then<T, TBusiness>(this BusinessResult<T, TBusiness> value, Action<T>? action)
        {
            if (value)
            {
                action?.Invoke(value);
            }
            
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Fail<T>(this Result<T> value, Exception? exception)
        {
            return value && exception is not null ? exception : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Fail<T>(this Result<T> value, Func<Exception?>? exception)
        {
            return value && exception?.Invoke() is { } result ? result : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Fail<T>(this Result<T> value, Func<T, Exception?>? exception)
        {
            return value && exception?.Invoke(value) is { } result ? result : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TException> Fail<T, TException>(this Result<T> value, TException? exception) where TException : Exception
        {
            return value && exception is not null ? exception : new Result<T, TException>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TException> Fail<T, TException>(this Result<T> value, Func<TException?>? exception) where TException : Exception
        {
            return value && exception?.Invoke() is { } result ? result : new Result<T, TException>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TException> Fail<T, TException>(this Result<T> value, Func<T, TException?>? exception) where TException : Exception
        {
            return value && exception?.Invoke(value) is { } result ? result : new Result<T, TException>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Fail<T>(this Result<T> value, BusinessException? exception)
        {
            return value && exception is not null ? exception : new BusinessResult<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Fail<T>(this Result<T> value, Func<BusinessException?>? exception)
        {
            return value && exception?.Invoke() is { } result ? result : new BusinessResult<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Fail<T>(this Result<T> value, Func<T, BusinessException?>? exception)
        {
            return value && exception?.Invoke(value) is { } result ? result : new BusinessResult<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Fail<T, TBusiness>(this Result<T> value, BusinessException<TBusiness>? exception)
        {
            return value && exception is not null ? exception : new BusinessResult<T, TBusiness>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Fail<T, TBusiness>(this Result<T> value, Func<BusinessException<TBusiness>?>? exception)
        {
            return value && exception?.Invoke() is { } result ? result : new BusinessResult<T, TBusiness>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Fail<T, TBusiness>(this Result<T> value, Func<T, BusinessException<TBusiness>?>? exception)
        {
            return value && exception?.Invoke(value) is { } result ? result : new BusinessResult<T, TBusiness>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Fail<T, TException>(this Result<T, TException> value, Exception? exception) where TException : Exception
        {
            return value && exception is not null ? exception : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Fail<T, TException>(this Result<T, TException> value, Func<Exception?>? exception) where TException : Exception
        {
            return value && exception?.Invoke() is { } result ? result : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Fail<T, TException>(this Result<T, TException> value, Func<T, Exception?>? exception) where TException : Exception
        {
            return value && exception?.Invoke(value) is { } result ? result : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TException> Fail<T, TException>(this Result<T, TException> value, TException? exception) where TException : Exception
        {
            return value && exception is not null ? exception : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TException> Fail<T, TException>(this Result<T, TException> value, Func<TException?>? exception) where TException : Exception
        {
            return value && exception?.Invoke() is { } result ? result : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TException> Fail<T, TException>(this Result<T, TException> value, Func<T, TException?>? exception) where TException : Exception
        {
            return value && exception?.Invoke(value) is { } result ? result : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Fail<T, TException>(this Result<T, TException> value, BusinessException? exception) where TException : Exception
        {
            return value && exception is not null ? exception : new BusinessResult<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Fail<T, TException>(this Result<T, TException> value, Func<BusinessException?>? exception) where TException : Exception
        {
            return value && exception?.Invoke() is { } result ? result : new BusinessResult<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Fail<T, TException>(this Result<T, TException> value, Func<T, BusinessException?>? exception) where TException : Exception
        {
            return value && exception?.Invoke(value) is { } result ? result : new BusinessResult<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Fail<T, TException, TBusiness>(this Result<T, TException> value, BusinessException<TBusiness>? exception) where TException : Exception
        {
            return value && exception is not null ? exception : new BusinessResult<T, TBusiness>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Fail<T, TException, TBusiness>(this Result<T, TException> value, Func<BusinessException<TBusiness>?>? exception) where TException : Exception
        {
            return value && exception?.Invoke() is { } result ? result : new BusinessResult<T, TBusiness>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Fail<T, TException, TBusiness>(this Result<T, TException> value, Func<T, BusinessException<TBusiness>?>? exception) where TException : Exception
        {
            return value && exception?.Invoke(value) is { } result ? result : new BusinessResult<T, TBusiness>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult Fail(this BusinessResult value, BusinessException? exception)
        {
            return value && exception is not null ? exception : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult Fail(this BusinessResult value, Func<BusinessException?>? exception)
        {
            return value && exception?.Invoke() is { } result ? result : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Fail<T>(this BusinessResult<T> value, Exception? exception)
        {
            return value && exception is not null ? exception : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Fail<T>(this BusinessResult<T> value, Func<Exception?>? exception)
        {
            return value && exception?.Invoke() is { } result ? result : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Fail<T>(this BusinessResult<T> value, Func<T, Exception?>? exception)
        {
            return value && exception?.Invoke(value) is { } result ? result : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TException> Fail<T, TException>(this BusinessResult<T> value, TException? exception) where TException : Exception
        {
            return value && exception is not null ? exception : new Result<T, TException>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TException> Fail<T, TException>(this BusinessResult<T> value, Func<TException?>? exception) where TException : Exception
        {
            return value && exception?.Invoke() is { } result ? result : new Result<T, TException>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TException> Fail<T, TException>(this BusinessResult<T> value, Func<T, TException?>? exception) where TException : Exception
        {
            return value && exception?.Invoke(value) is { } result ? result : new Result<T, TException>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Fail<T>(this BusinessResult<T> value, BusinessException? exception)
        {
            return value && exception is not null ? exception : new BusinessResult<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Fail<T>(this BusinessResult<T> value, Func<BusinessException?>? exception)
        {
            return value && exception?.Invoke() is { } result ? result : new BusinessResult<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Fail<T>(this BusinessResult<T> value, Func<T, BusinessException?>? exception)
        {
            return value && exception?.Invoke(value) is { } result ? result : new BusinessResult<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Fail<T, TBusiness>(this BusinessResult<T> value, BusinessException<TBusiness>? exception)
        {
            return value && exception is not null ? exception : new BusinessResult<T, TBusiness>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Fail<T, TBusiness>(this BusinessResult<T> value, Func<BusinessException<TBusiness>?>? exception)
        {
            return value && exception?.Invoke() is { } result ? result : new BusinessResult<T, TBusiness>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Fail<T, TBusiness>(this BusinessResult<T> value, Func<T, BusinessException<TBusiness>?>? exception)
        {
            return value && exception?.Invoke(value) is { } result ? result : new BusinessResult<T, TBusiness>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Fail<T, TBusiness>(this BusinessResult<T, TBusiness> value, Exception? exception) where TBusiness : Exception
        {
            return value && exception is not null ? exception : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Fail<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<Exception?>? exception) where TBusiness : Exception
        {
            return value && exception?.Invoke() is { } result ? result : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Fail<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<T, Exception?>? exception) where TBusiness : Exception
        {
            return value && exception?.Invoke(value) is { } result ? result : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TBusiness> Fail<T, TBusiness>(this BusinessResult<T, TBusiness> value, TBusiness? exception) where TBusiness : Exception
        {
            return value && exception is not null ? exception : new Result<T, TBusiness>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TBusiness> Fail<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<TBusiness?>? exception) where TBusiness : Exception
        {
            return value && exception?.Invoke() is { } result ? result : new Result<T, TBusiness>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TBusiness> Fail<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<T, TBusiness?>? exception) where TBusiness : Exception
        {
            return value && exception?.Invoke(value) is { } result ? result : new Result<T, TBusiness>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Fail<T, TBusiness>(this BusinessResult<T, TBusiness> value, BusinessException? exception) where TBusiness : Exception
        {
            return value && exception is not null ? exception : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Fail<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<BusinessException?>? exception) where TBusiness : Exception
        {
            return value && exception?.Invoke() is { } result ? result : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Fail<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<T, BusinessException?>? exception) where TBusiness : Exception
        {
            return value && exception?.Invoke(value) is { } result ? result : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Fail<T, TBusiness>(this BusinessResult<T, TBusiness> value, BusinessException<TBusiness>? exception) where TBusiness : Exception
        {
            return value && exception is not null ? exception : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Fail<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<BusinessException<TBusiness>?>? exception) where TBusiness : Exception
        {
            return value && exception?.Invoke() is { } result ? result : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Fail<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<T, BusinessException<TBusiness>?>? exception) where TBusiness : Exception
        {
            return value && exception?.Invoke(value) is { } result ? result : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Else<T>(this Result<T> value, T result)
        {
            return value ? value : result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Else<T>(this Result<T> value, Func<T> result)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            
            return value ? value : result.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Else<T>(this Result<T> value, Func<Exception, T> result)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            
            return value ? value : result.Invoke(value.Exception!);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TException> Else<T, TException>(this Result<T, TException> value, Func<T> result) where TException : Exception
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            
            return value ? value : result.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TException> Else<T, TException>(this Result<T, TException> value, Func<TException, T> result) where TException : Exception
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            
            return value ? value : result.Invoke(value.Exception!);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Else<T>(this BusinessResult<T> value, Func<T> result)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            
            return value ? value : result.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Else<T>(this BusinessResult<T> value, Func<BusinessException, T> result)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            
            return value ? value : result.Invoke(value.Exception!);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Else<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<T> result)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            
            return value ? value : result.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Else<T, TBusiness>(this BusinessResult<T, TBusiness> value, Func<BusinessException<TBusiness>, T> result)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            return value ? value : result.Invoke(value.Exception!);
        }
    }
}