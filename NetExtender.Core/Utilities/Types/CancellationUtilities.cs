// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utilities.Static;

namespace NetExtender.Utilities.Types
{
    public static class CancellationUtilities
    {
        private static CancellationToken GetWeakToken(this CancellationTokenSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            CancellationToken token = source.Token;
            token.Register(source.Dispose);
            return token;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationToken CreateToken(TimeSpan delay)
        {
            if (delay <= Time.Millisecond.Ten)
            {
                return new CancellationToken(true);
            }
            
            CancellationTokenSource source = new CancellationTokenSource(delay);
            return source.GetWeakToken();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationToken CreateToken(Int32 milliseconds)
        {
            if (milliseconds <= 10)
            {
                return new CancellationToken(true);
            }
            
            CancellationTokenSource source = new CancellationTokenSource(milliseconds);
            return source.GetWeakToken();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationTokenSource CreateLinkedSource(this CancellationToken token)
        {
            return CreateLinkedSource(CancellationToken.None, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationTokenSource CreateLinkedSource(this CancellationToken token, TimeSpan timeout)
        {
            return CreateLinkedSource(CancellationToken.None, token, timeout);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationTokenSource CreateLinkedSource(this CancellationToken token, TimeSpan? timeout)
        {
            return timeout.HasValue ? CreateLinkedSource(token, timeout.Value) : CreateLinkedSource(token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationTokenSource CreateLinkedSource(this CancellationToken first, CancellationToken second)
        {
            return CancellationTokenSource.CreateLinkedTokenSource(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "CA1068")]
        public static CancellationTokenSource CreateLinkedSource(this CancellationToken first, CancellationToken second, TimeSpan timeout)
        {
            CancellationTokenSource source = CreateLinkedSource(first, second);
            source.CancelAfter(timeout);
            return source;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "CA1068")]
        public static CancellationTokenSource CreateLinkedSource(this CancellationToken first, CancellationToken second, TimeSpan? timeout)
        {
            return timeout.HasValue ? CreateLinkedSource(first, second, timeout.Value) : CreateLinkedSource(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationTokenSource CreateLinkedSource(this CancellationToken[] tokens)
        {
            return CancellationTokenSource.CreateLinkedTokenSource(tokens);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationTokenSource CreateLinkedSource(this CancellationToken[] tokens, TimeSpan timeout)
        {
            CancellationTokenSource source = CreateLinkedSource(tokens);
            source.CancelAfter(timeout);
            return source;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationTokenSource CreateLinkedSource(this CancellationToken[] tokens, TimeSpan? timeout)
        {
            return timeout.HasValue ? CreateLinkedSource(tokens, timeout.Value) : CreateLinkedSource(tokens);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationTokenSource CreateLinkedSource(this CancellationToken token, params CancellationToken[] tokens)
        {
            return tokens.Length switch
            {
                0 => CreateLinkedSource(token),
                1 => CreateLinkedSource(token, tokens[0]),
                _ => CreateLinkedSource(tokens.Prepend(token).ToArray())
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationTokenSource CreateLinkedSource(this CancellationToken token, TimeSpan timeout, params CancellationToken[] tokens)
        {
            CancellationTokenSource source = CreateLinkedSource(token, tokens);
            source.CancelAfter(timeout);
            return source;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationTokenSource CreateLinkedSource(this CancellationToken first, TimeSpan? timeout, params CancellationToken[] tokens)
        {
            return timeout.HasValue ? CreateLinkedSource(first, timeout.Value, tokens) : CreateLinkedSource(first, tokens);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationTokenSource CreateLinkedSource(this IEnumerable<CancellationToken> tokens)
        {
            if (tokens is null)
            {
                throw new ArgumentNullException(nameof(tokens));
            }

            return CancellationTokenSource.CreateLinkedTokenSource(tokens.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationTokenSource CreateLinkedSource(this IEnumerable<CancellationToken> tokens, TimeSpan timeout)
        {
            if (tokens is null)
            {
                throw new ArgumentNullException(nameof(tokens));
            }

            CancellationTokenSource source = CreateLinkedSource(tokens);
            source.CancelAfter(timeout);
            return source;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationTokenSource CreateLinkedSource(this IEnumerable<CancellationToken> tokens, TimeSpan? timeout)
        {
            if (tokens is null)
            {
                throw new ArgumentNullException(nameof(tokens));
            }

            return timeout.HasValue ? CreateLinkedSource(tokens, timeout.Value) : CreateLinkedSource(tokens);
        }
        
        /// <summary>
        /// Allows a cancellation token to be awaited.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static CancellationTokenAwaiter GetAwaiter(this CancellationToken token)
        {
            return new CancellationTokenAwaiter(token);
        }

        /// <summary>
        /// The awaiter for cancellation tokens.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly struct CancellationTokenAwaiter : ICriticalNotifyCompletion
        {
            public static explicit operator CancellationTokenAwaiter(CancellationToken token)
            {
                return new CancellationTokenAwaiter(token);
            }
            
            public CancellationToken CancellationToken { get; }
            
            public Boolean IsCompleted
            {
                get
                {
                    return CancellationToken.IsCancellationRequested;
                }
            }
            
            public CancellationTokenAwaiter(CancellationToken token)
            {
                CancellationToken = token;
            }

            // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
            public Task GetResult()
            {
                if (IsCompleted)
                {
                    return Task.CompletedTask;
                }

                throw new InvalidOperationException("The cancellation token has not yet been cancelled.");
            }

            public void OnCompleted(Action continuation)
            {
                CancellationToken.Register(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                CancellationToken.Register(continuation);
            }
        }
    }
}