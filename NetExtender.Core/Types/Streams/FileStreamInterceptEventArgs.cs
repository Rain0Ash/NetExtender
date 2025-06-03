// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Reflection;
using NetExtender.Types.Intercept;
using NetExtender.Types.Intercept.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Streams
{
    public interface IFileStreamInterceptEventArgs<T> : IFileStreamInterceptEventArgs, IMethodInterceptEventArgs<T>
    {
    }

    public interface IFileStreamInterceptEventArgs : IMethodInterceptEventArgs
    {
        public FileStreamIntercept Type { get; }
        public Boolean HasStream { get; }
        public Memory<Byte>? FullBuffer { get; }
        public ReadOnlyMemory<Byte>? FullReadOnlyBuffer { get; }
        public Memory<Byte>? Buffer { get; }
        public ReadOnlyMemory<Byte>? ReadOnlyBuffer { get; }
        public Int32? Length { get; }
        public Int32 Offset { get; }
        public Int32 Count { get; }
    }

    public interface IFileStreamInterceptArgumentInfo<T> : IFileStreamInterceptArgumentInfo, IMemberInterceptArgumentInfo<MethodInfo, T>
    {
    }

    public interface IFileStreamInterceptArgumentInfo : IMemberInterceptArgumentInfo<MethodInfo>
    {
        public FileStreamIntercept Intercept { get; }
        public ImmutableArray<Object?> Arguments { get; }
        public Lazy<Stream> Stream { get; }
        public Boolean HasStream { get; }
        public Memory<Byte>? Buffer { get; }
    }

    public class FileStreamInterceptEventArgs<T> : FileStreamInterceptAbstractionEventArgs<FileStreamInterceptEventArgs<T>.Information>, IFileStreamInterceptEventArgs<T>
    {
        public readonly struct Information : IFileStreamInterceptArgumentInfo<T>
        {
            public MethodInfo Member { get; }
            public FileStreamIntercept Intercept { get; }
            public ImmutableArray<Object?> Arguments { get; }
            public Maybe<T> Value { get; }
            public Lazy<Stream> Stream { get; }

            public Boolean HasStream
            {
                get
                {
                    return Stream.IsValueCreated;
                }
            }

            public Memory<Byte>? Buffer { get; }
            public Exception? Exception { get; }

            public Information(MethodInfo method, FileStreamIntercept intercept, Maybe<T> value, IEnumerable<Object?>? arguments, Lazy<Stream> stream, Memory<Byte>? buffer, Exception? exception)
                : this(method, intercept, value, arguments?.ToImmutableArray() ?? ImmutableArray<Object?>.Empty, stream, buffer, exception)
            {
            }

            public Information(MethodInfo method, FileStreamIntercept intercept, Maybe<T> value, ImmutableArray<Object?> arguments, Lazy<Stream> stream, Memory<Byte>? buffer, Exception? exception)
            {
                Member = method ?? throw new ArgumentNullException(nameof(method));
                Intercept = intercept;
                Value = value;
                Arguments = arguments;
                Stream = stream ?? throw new ArgumentNullException(nameof(stream));
                Buffer = buffer;
                Exception = exception;
            }
        }
        
        private protected Maybe<T> _value;
        public virtual T Value
        {
            get
            {
                return _value.HasValue ? _value.Value : Info.Value.HasValue ? Info.Value.Value : throw new InvalidOperationException("Cannot get value when method result is not set.");
            }
            set
            {
                if (IsSeal)
                {
                    throw new InvalidOperationException("Cannot change value when intercept is seal.");
                }
                
                _value = value;
                Seal();
            }
        }

        public FileStreamInterceptEventArgs(ValueDelegateUtilities.Info @delegate, FileStreamIntercept intercept, Lazy<Stream> stream)
            : base(new Information(@delegate.Method ?? throw new ArgumentNullException(nameof(@delegate)), intercept, default, @delegate.Arguments, stream ?? throw new ArgumentNullException(nameof(stream)), default, null))
        {
        }

        public FileStreamInterceptEventArgs(ValueDelegateUtilities.Info @delegate, FileStreamIntercept intercept, Lazy<Stream> stream, Memory<Byte> buffer)
            : base(new Information(@delegate.Method ?? throw new ArgumentNullException(nameof(@delegate)), intercept, default, @delegate.Arguments, stream ?? throw new ArgumentNullException(nameof(stream)), buffer, null))
        {
            _count = buffer.Length;
        }

        public FileStreamInterceptEventArgs(ValueDelegateUtilities.Info @delegate, FileStreamIntercept intercept, T value, Lazy<Stream> stream)
            : base(new Information(@delegate.Method ?? throw new ArgumentNullException(nameof(@delegate)), intercept, value, @delegate.Arguments, stream ?? throw new ArgumentNullException(nameof(stream)), default, null))
        {
        }

        public FileStreamInterceptEventArgs(ValueDelegateUtilities.Info @delegate, FileStreamIntercept intercept, T value, Lazy<Stream> stream, Memory<Byte> buffer)
            : base(new Information(@delegate.Method ?? throw new ArgumentNullException(nameof(@delegate)), intercept, value, @delegate.Arguments, stream ?? throw new ArgumentNullException(nameof(stream)), buffer, null))
        {
            _count = buffer.Length;
        }

        public FileStreamInterceptEventArgs(ValueDelegateUtilities.Info @delegate, FileStreamIntercept intercept, Lazy<Stream> stream, Exception exception)
            : base(new Information(@delegate.Method ?? throw new ArgumentNullException(nameof(@delegate)), intercept, default, @delegate.Arguments, stream ?? throw new ArgumentNullException(nameof(stream)), null, exception ?? throw new ArgumentNullException(nameof(exception))))
        {
        }

        internal FileStreamInterceptEventArgs(ValueDelegateUtilities.Info @delegate, Lazy<Stream> stream, InterceptFileStream.Info intercept, Exception? exception)
            : base(new Information(@delegate.Method ?? throw new ArgumentNullException(nameof(@delegate)), intercept.Intercept, default, @delegate.Arguments, stream ?? throw new ArgumentNullException(nameof(stream)), intercept.Buffer, exception))
        {
            _offset = intercept.Offset ?? 0;
            _count = intercept.Count ?? intercept.Buffer?.Length ?? 0;
            Token = intercept.Token;
        }

        internal FileStreamInterceptEventArgs(ValueDelegateUtilities.Info @delegate, T value, Lazy<Stream> stream, InterceptFileStream.Info intercept, Exception? exception)
            : base(new Information(@delegate.Method ?? throw new ArgumentNullException(nameof(@delegate)), intercept.Intercept, value, @delegate.Arguments, stream ?? throw new ArgumentNullException(nameof(stream)), intercept.Buffer, exception))
        {
            _offset = intercept.Offset ?? 0;
            _count = intercept.Count ?? intercept.Buffer?.Length ?? 0;
            Token = intercept.Token;
        }

        protected internal virtual void Intercept(T value)
        {
            Intercept();
            Info = new Information(Info.Member, Info.Intercept, value, Info.Arguments, Info.Stream, Info.Buffer, null);
            Invoke();
        }

        void ISimpleInterceptEventArgs<T>.Intercept(T value)
        {
            Intercept(value);
        }

        protected internal override void Intercept(Exception exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            Intercept();
            Info = new Information(Info.Member, Info.Intercept, default, Info.Arguments, Info.Stream, Info.Buffer, exception);
            Invoke();
        }
    }
    
    public class FileStreamInterceptEventArgs : FileStreamInterceptAbstractionEventArgs<FileStreamInterceptEventArgs.Information>
    {
        public readonly struct Information : IFileStreamInterceptArgumentInfo
        {
            public MethodInfo Member { get; }
            public FileStreamIntercept Intercept { get; }
            public ImmutableArray<Object?> Arguments { get; }
            public Lazy<Stream> Stream { get; }

            public Boolean HasStream
            {
                get
                {
                    return Stream.IsValueCreated;
                }
            }

            public Memory<Byte>? Buffer { get; }
            public Exception? Exception { get; }

            public Information(MethodInfo method, FileStreamIntercept intercept, IEnumerable<Object?>? arguments, Lazy<Stream> stream, Memory<Byte>? buffer, Exception? exception)
                : this(method, intercept, arguments?.ToImmutableArray() ?? ImmutableArray<Object?>.Empty, stream, buffer, exception)
            {
            }

            public Information(MethodInfo method, FileStreamIntercept intercept, ImmutableArray<Object?> arguments, Lazy<Stream> stream, Memory<Byte>? buffer, Exception? exception)
            {
                Member = method ?? throw new ArgumentNullException(nameof(method));
                Intercept = intercept;
                Arguments = arguments;
                Stream = stream ?? throw new ArgumentNullException(nameof(stream));
                Buffer = buffer;
                Exception = exception;
            }
        }

        public FileStreamInterceptEventArgs(ValueDelegateUtilities.Info @delegate, FileStreamIntercept intercept, Lazy<Stream> stream)
            : base(new Information(@delegate.Method ?? throw new ArgumentNullException(nameof(@delegate)), intercept, @delegate.Arguments, stream ?? throw new ArgumentNullException(nameof(stream)), default, null))
        {
        }

        public FileStreamInterceptEventArgs(ValueDelegateUtilities.Info @delegate, FileStreamIntercept intercept, Lazy<Stream> stream, Memory<Byte> buffer)
            : base(new Information(@delegate.Method ?? throw new ArgumentNullException(nameof(@delegate)), intercept, @delegate.Arguments, stream ?? throw new ArgumentNullException(nameof(stream)), buffer, null))
        {
            _count = buffer.Length;
        }

        public FileStreamInterceptEventArgs(ValueDelegateUtilities.Info @delegate, FileStreamIntercept intercept, Lazy<Stream> stream, Exception exception)
            : base(new Information(@delegate.Method ?? throw new ArgumentNullException(nameof(@delegate)), intercept, @delegate.Arguments, stream ?? throw new ArgumentNullException(nameof(stream)), null, exception ?? throw new ArgumentNullException(nameof(exception))))
        {
        }

        internal FileStreamInterceptEventArgs(ValueDelegateUtilities.Info @delegate, Lazy<Stream> stream, InterceptFileStream.Info intercept, Exception? exception)
            : base(new Information(@delegate.Method ?? throw new ArgumentNullException(nameof(@delegate)), intercept.Intercept, @delegate.Arguments, stream ?? throw new ArgumentNullException(nameof(stream)), intercept.Buffer, exception))
        {
            _offset = intercept.Offset ?? 0;
            _count = intercept.Count ?? intercept.Buffer?.Length ?? 0;
            Token = intercept.Token;
        }

        protected internal override void Intercept(Exception exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            Intercept();
            Info = new Information(Info.Member, Info.Intercept, Info.Arguments, Info.Stream, Info.Buffer, exception);
            Invoke();
        }
    }

    public abstract class FileStreamInterceptAbstractionEventArgs<T> : MethodInterceptAbstractionEventArgs<T>, IFileStreamInterceptEventArgs where T : IFileStreamInterceptArgumentInfo
    {
        public FileStreamIntercept Type
        {
            get
            {
                return Info.Intercept;
            }
        }

        public override ImmutableArray<Object?> Arguments
        {
            get
            {
                return Info.Arguments;
            }
        }

        public Boolean HasStream
        {
            get
            {
                return Info.HasStream;
            }
        }

        private protected Memory<Byte>? _buffer;
        public Memory<Byte>? FullBuffer
        {
            get
            {
                Memory<Byte>? buffer = _buffer ?? Info.Buffer;
                return buffer is null || !IsSeal ? buffer : throw new ObjectDisposedException(nameof(Buffer));
            }
            set
            {
                if (value is null != Info.Buffer is null)
                {
                    throw new ArgumentException("Buffer nullability mismatch with original buffer.", nameof(value));
                }

                if (value is not null && Info.Buffer is not null && value.Value.Length != Info.Buffer.Value.Length)
                {
                    throw new ArgumentException("Buffer length mismatch with original buffer.", nameof(value));
                }
                
                _buffer = value;
            }
        }

        public ReadOnlyMemory<Byte>? FullReadOnlyBuffer
        {
            get
            {
                return _buffer ?? Info.Buffer;
            }
        }

        public Memory<Byte>? Buffer
        {
            get
            {
                try
                {
                    return FullBuffer?.Slice(Offset, Count);
                }
                catch (ArgumentOutOfRangeException)
                {
                    return null;
                }
            }
        }

        public ReadOnlyMemory<Byte>? ReadOnlyBuffer
        {
            get
            {
                try
                {
                    return FullReadOnlyBuffer?.Slice(Offset, Count);
                }
                catch (ArgumentOutOfRangeException)
                {
                    return null;
                }
            }
        }

        public Int32? Length
        {
            get
            {
                return ReadOnlyBuffer?.Length;
            }
        }

        private protected Int32 _offset;
        public Int32 Offset
        {
            get
            {
                return FullBuffer is not null ? _offset : throw new InvalidOperationException($"Cannot get offset without {nameof(Buffer)}.");
            }
            set
            {
                if (FullBuffer is null)
                {
                    throw new InvalidOperationException($"Cannot set offset without {nameof(Buffer)}.");
                }

                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be greater than or equal to zero.");
                }
                
                _offset = value;
            }
        }

        private protected Int32 _count;
        public virtual Int32 Count
        {
            get
            {
                return FullBuffer is not null ? _count : throw new InvalidOperationException($"Cannot get count without {nameof(Buffer)}.");
            }
            set
            {
                if (FullBuffer is null)
                {
                    throw new InvalidOperationException($"Cannot set offset without {nameof(Buffer)}.");
                }

                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be greater than or equal to zero.");
                }

                _count = value;
            }
        }
        
        protected FileStreamInterceptAbstractionEventArgs(T value)
            : base(value)
        {
        }
    }
}