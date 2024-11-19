using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Entities;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Interception.Interfaces;
using NetExtender.Types.Memory;
using NetExtender.Types.Streams;

namespace NetExtender.Types.Interception
{
    public delegate Stream InterceptStreamWriterOpen(String path, Encoding encoding, FileStreamOptions options);
    
    public static class InterceptStreamWriterUtilities
    {
        internal static Encoding UTF8NoBOM { get; } = new UTF8Encoding(false, true);
        
        public static event InterceptStreamWriterOpen? FileStream;

        internal static Stream Raise(String path, Encoding encoding, FileStreamOptions options)
        {
            return FileStream?.Invoke(path, encoding, options) ?? new InterceptFileStream(path, options);
        }
    }
    
    [SuppressMessage("Design", "CA1041")]
    public class InterceptStreamWriter : StreamWriter, IInterceptIdentifierTarget<InterceptStreamWriter>, IPropertyIntercept<InterceptStreamWriter, IPropertyInterceptEventArgs>, IInterceptTargetRaise<IPropertyInterceptEventArgs>, IMethodIntercept<InterceptStreamWriter, IMethodInterceptEventArgs>, IInterceptTargetRaise<IMethodInterceptEventArgs>
    {
        private const Int32 DefaultBufferSize = 1024;
        private const Int32 DefaultFileStreamBufferSize = 4096;
        
        protected IAnyMemberInterceptor<InterceptStreamWriter, IPropertyInterceptEventArgs, MethodInterceptEventArgs, Any.Value> Interceptor { get; }
        
        public event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyIntercept
        {
            add
            {
                PropertyGetIntercept += value;
                PropertySetIntercept += value;
            }
            remove
            {
                PropertyGetIntercept -= value;
                PropertySetIntercept -= value;
            }
        }
        
        public event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyIntercepting
        {
            add
            {
                PropertyGetIntercepting += value;
                PropertySetIntercepting += value;
            }
            remove
            {
                PropertyGetIntercepting -= value;
                PropertySetIntercepting -= value;
            }
        }
        
        public event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyIntercepted
        {
            add
            {
                PropertyGetIntercepted += value;
                PropertySetIntercepted += value;
            }
            remove
            {
                PropertyGetIntercepted -= value;
                PropertySetIntercepted -= value;
            }
        }
        
        public event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyGetIntercept
        {
            add
            {
                PropertyGetIntercepting += value;
                PropertyGetIntercepted += value;
            }
            remove
            {
                PropertyGetIntercepting -= value;
                PropertyGetIntercepted -= value;
            }
        }
        
        public event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertySetIntercept
        {
            add
            {
                PropertySetIntercepting += value;
                PropertySetIntercepted += value;
            }
            remove
            {
                PropertySetIntercepting -= value;
                PropertySetIntercepted -= value;
            }
        }
        
        public event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyGetIntercepting;
        public event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertySetIntercepting;
        public event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyGetIntercepted;
        public event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertySetIntercepted;
        
        public event EventHandler<InterceptStreamWriter, IMethodInterceptEventArgs>? MethodIntercept
        {
            add
            {
                MethodIntercepting += value;
                MethodIntercepted += value;
            }
            remove
            {
                MethodIntercepting -= value;
                MethodIntercepted -= value;
            }
        }
        
        public event EventHandler<InterceptStreamWriter, IMethodInterceptEventArgs>? MethodIntercepting;
        public event EventHandler<InterceptStreamWriter, IMethodInterceptEventArgs>? MethodIntercepted;
        
        private protected String? _identifier;
        public virtual String Identifier
        {
            get
            {
                return _identifier ??= GetType().Name;
            }
            init
            {
                _identifier = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public override Stream BaseStream
        {
            get
            {
                return Interceptor.InterceptGet<Stream>(this);
            }
        }

        public override Encoding Encoding
        {
            get
            {
                return Interceptor.InterceptGet<Encoding>(this);
            }
        }

        [AllowNull]
        public override String NewLine
        {
            get
            {
                return Interceptor.InterceptGet<String>(this);
            }
            set
            {
                Interceptor.InterceptSet(this, value);
            }
        }

        public override IFormatProvider FormatProvider
        {
            get
            {
                return Interceptor.InterceptGet<IFormatProvider>(this);
            }
        }

        public override Boolean AutoFlush
        {
            get
            {
                return Interceptor.InterceptGet<Boolean>(this);
            }
            set
            {
                Interceptor.InterceptSet(this, value);
            }
        }

        public InterceptStreamWriter(Stream stream)
            : base(stream)
        {
            Interceptor = new AnyMemberInterceptor<InterceptStreamWriter, Any.Value>();
        }

        public InterceptStreamWriter(Stream stream, Encoding encoding)
            : base(stream, encoding)
        {
            Interceptor = new AnyMemberInterceptor<InterceptStreamWriter, Any.Value>();
        }

        public InterceptStreamWriter(Stream stream, Encoding encoding, Int32 bufferSize)
            : base(stream, encoding, bufferSize)
        {
            Interceptor = new AnyMemberInterceptor<InterceptStreamWriter, Any.Value>();
        }

        public InterceptStreamWriter(Stream stream, Encoding? encoding = null, Int32 bufferSize = -1, Boolean leaveOpen = false)
            : base(stream, encoding, bufferSize, leaveOpen)
        {
            Interceptor = new AnyMemberInterceptor<InterceptStreamWriter, Any.Value>();
        }

        public InterceptStreamWriter(String path)
            : this(path, false, InterceptStreamWriterUtilities.UTF8NoBOM, DefaultBufferSize)
        {
        }

        public InterceptStreamWriter(String path, FileStreamOptions options)
            : this(path, InterceptStreamWriterUtilities.UTF8NoBOM, options)
        {
        }

        public InterceptStreamWriter(String path, Boolean append)
            : this(path, append, InterceptStreamWriterUtilities.UTF8NoBOM, DefaultBufferSize)
        {
        }

        public InterceptStreamWriter(String path, Boolean append, Encoding encoding)
            : this(path, append, encoding, DefaultBufferSize)
        {
        }

        public InterceptStreamWriter(String path, Boolean append, Encoding encoding, Int32 bufferSize)
            : this(ValidateArgsAndOpenPath(path, append, encoding, bufferSize), encoding, bufferSize, false)
        {
        }

        public InterceptStreamWriter(String path, Encoding encoding, FileStreamOptions options)
            : this(ValidateArgsAndOpenPath(path, encoding, options), encoding, DefaultFileStreamBufferSize)
        {
        }

        private static Stream ValidateArgsAndOpenPath(String path, Encoding encoding, FileStreamOptions options)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            
            if ((options.Access & FileAccess.Write) == 0)
            {
                throw new ArgumentException("Stream not writable.", nameof(options));
            }

            return InterceptStreamWriterUtilities.Raise(path, encoding, options);
        }

        private static Stream ValidateArgsAndOpenPath(String path, Boolean append, Encoding encoding, Int32 buffer)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (buffer <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(buffer), buffer, null);
            }

            FileStreamOptions options = new FileStreamOptions
            {
                Mode = append ? FileMode.Append : FileMode.Create,
                Access = FileAccess.Write,
                Share = FileShare.Read,
                BufferSize = DefaultFileStreamBufferSize
            };
            
            return ValidateArgsAndOpenPath(path, encoding, options);
        }

        public override void Write(Boolean value)
        {
            Interceptor.Intercept(this, default, base.Write, value);
        }

        public override void Write(Char value)
        {
            Interceptor.Intercept(this, default, base.Write, value);
        }

        public override void Write(Int32 value)
        {
            Interceptor.Intercept(this, default, base.Write, value);
        }

        public override void Write(UInt32 value)
        {
            Interceptor.Intercept(this, default, base.Write, value);
        }

        public override void Write(Int64 value)
        {
            Interceptor.Intercept(this, default, base.Write, value);
        }

        public override void Write(UInt64 value)
        {
            Interceptor.Intercept(this, default, base.Write, value);
        }

        public override void Write(Single value)
        {
            Interceptor.Intercept(this, default, base.Write, value);
        }

        public override void Write(Double value)
        {
            Interceptor.Intercept(this, default, base.Write, value);
        }

        public override void Write(Decimal value)
        {
            Interceptor.Intercept(this, default, base.Write, value);
        }

        public override void Write(Object? value)
        {
            Interceptor.Intercept(this, default, base.Write, value);
        }

        private void WriteInternal(ReadOnlySpan<Char> buffer)
        {
            base.Write(buffer);
        }

        public sealed override unsafe void Write(ReadOnlySpan<Char> buffer)
        {
            fixed (Char* _ = buffer)
            {
                using UnsafeMemory<Char> memory = new UnsafeMemory<Char>(buffer);
                Write(memory);
            }
        }

        public virtual void Write(ReadOnlyMemory<Char> buffer)
        {
            static void Internal(InterceptStreamWriter writer, ReadOnlyMemory<Char> buffer)
            {
                writer.WriteInternal(buffer.Span);
            }
            
            Interceptor.Intercept(this, default, Internal, this, buffer);
        }

        public override void Write(Char[]? buffer)
        {
            Interceptor.Intercept(this, default, base.Write, buffer);
        }

        public override void Write(Char[] buffer, Int32 index, Int32 count)
        {
            Interceptor.Intercept(this, default, base.Write, buffer, index, count);
        }

        public override void Write(StringBuilder? value)
        {
            Interceptor.Intercept(this, default, base.Write, value);
        }

        public override void Write(String? value)
        {
            Interceptor.Intercept(this, default, base.Write, value);
        }

        public override void Write(String format, Object? arg0)
        {
            Interceptor.Intercept(this, default, base.Write, format, arg0);
        }

        public override void Write(String format, Object? arg0, Object? arg1)
        {
            Interceptor.Intercept(this, default, base.Write, format, arg0, arg1);
        }

        public override void Write(String format, Object? arg0, Object? arg1, Object? arg2)
        {
            Interceptor.Intercept(this, default, base.Write, format, arg0, arg1, arg2);
        }

        public override void Write(String format, params Object?[] args)
        {
            Interceptor.Intercept(this, default, base.Write, format, args);
        }

        public override async Task WriteAsync(Char value)
        {
            await Interceptor.Intercept(this, default, base.WriteAsync, value);
        }

        public override async Task WriteAsync(ReadOnlyMemory<Char> buffer, CancellationToken token = default)
        {
            await Interceptor.Intercept(this, default, base.WriteAsync, buffer, token);
        }

        public override async Task WriteAsync(Char[] buffer, Int32 index, Int32 count)
        {
            await Interceptor.Intercept(this, default, base.WriteAsync, buffer, index, count);
        }

        public override async Task WriteAsync(StringBuilder? value, CancellationToken token = default)
        {
            await Interceptor.Intercept(this, default, base.WriteAsync, value, token);
        }

        public override async Task WriteAsync(String? value)
        {
            await Interceptor.Intercept(this, default, base.WriteAsync, value);
        }

        public override void WriteLine()
        {
            Interceptor.Intercept(this, default, base.WriteLine);
        }

        public override void WriteLine(Boolean value)
        {
            Interceptor.Intercept(this, default, base.WriteLine, value);
        }

        public override void WriteLine(Char value)
        {
            Interceptor.Intercept(this, default, base.WriteLine, value);
        }

        public override void WriteLine(Int32 value)
        {
            Interceptor.Intercept(this, default, base.WriteLine, value);
        }

        public override void WriteLine(UInt32 value)
        {
            Interceptor.Intercept(this, default, base.WriteLine, value);
        }

        public override void WriteLine(Int64 value)
        {
            Interceptor.Intercept(this, default, base.WriteLine, value);
        }

        public override void WriteLine(UInt64 value)
        {
            Interceptor.Intercept(this, default, base.WriteLine, value);
        }

        public override void WriteLine(Single value)
        {
            Interceptor.Intercept(this, default, base.WriteLine, value);
        }

        public override void WriteLine(Double value)
        {
            Interceptor.Intercept(this, default, base.WriteLine, value);
        }

        public override void WriteLine(Decimal value)
        {
            Interceptor.Intercept(this, default, base.WriteLine, value);
        }

        public override void WriteLine(Object? value)
        {
            Interceptor.Intercept(this, default, base.WriteLine, value);
        }

        private void WriteLineInternal(ReadOnlySpan<Char> buffer)
        {
            base.WriteLine(buffer);
        }

        public sealed override unsafe void WriteLine(ReadOnlySpan<Char> buffer)
        {
            fixed (Char* _ = buffer)
            {
                using UnsafeMemory<Char> memory = new UnsafeMemory<Char>(buffer);
                WriteLine(memory);
            }
        }

        public virtual void WriteLine(ReadOnlyMemory<Char> buffer)
        {
            static void Internal(InterceptStreamWriter writer, ReadOnlyMemory<Char> buffer)
            {
                writer.WriteLineInternal(buffer.Span);
            }
            
            Interceptor.Intercept(this, default, Internal, this, buffer);
        }

        public override void WriteLine(Char[]? buffer)
        {
            Interceptor.Intercept(this, default, base.WriteLine, buffer);
        }

        public override void WriteLine(Char[] buffer, Int32 index, Int32 count)
        {
            Interceptor.Intercept(this, default, base.WriteLine, buffer, index, count);
        }

        public override void WriteLine(StringBuilder? value)
        {
            Interceptor.Intercept(this, default, base.WriteLine, value);
        }

        public override void WriteLine(String? value)
        {
            Interceptor.Intercept(this, default, base.WriteLine, value);
        }

        public override void WriteLine(String format, Object? arg0)
        {
            Interceptor.Intercept(this, default, base.WriteLine, format, arg0);
        }

        public override void WriteLine(String format, Object? arg0, Object? arg1)
        {
            Interceptor.Intercept(this, default, base.WriteLine, format, arg0, arg1);
        }

        public override void WriteLine(String format, Object? arg0, Object? arg1, Object? arg2)
        {
            Interceptor.Intercept(this, default, base.WriteLine, format, arg0, arg1, arg2);
        }

        public override void WriteLine(String format, params Object?[] args)
        {
            Interceptor.Intercept(this, default, base.WriteLine, format, args);
        }

        public override async Task WriteLineAsync()
        {
            await Interceptor.Intercept(this, default, base.WriteLineAsync);
        }

        public override async Task WriteLineAsync(Char value)
        {
            await Interceptor.Intercept(this, default, base.WriteLineAsync, value);
        }

        public override async Task WriteLineAsync(ReadOnlyMemory<Char> buffer, CancellationToken token = default)
        {
            await Interceptor.Intercept(this, default, base.WriteLineAsync, buffer, token);
        }

        public override async Task WriteLineAsync(Char[] buffer, Int32 index, Int32 count)
        {
            await Interceptor.Intercept(this, default, base.WriteLineAsync, buffer, index, count);
        }

        public override async Task WriteLineAsync(StringBuilder? value, CancellationToken token = default)
        {
            await Interceptor.Intercept(this, default, base.WriteLineAsync, value, token);
        }

        public override async Task WriteLineAsync(String? value)
        {
            await Interceptor.Intercept(this, default, base.WriteLineAsync, value);
        }

        public override void Flush()
        {
            Interceptor.Intercept(this, default, base.Flush);
        }

        public override async Task FlushAsync()
        {
            await Interceptor.Intercept(this, default, base.FlushAsync);
        }

        public override void Close()
        {
            Interceptor.Intercept(this, default, base.Close);
        }

        protected override void Dispose(Boolean disposing)
        {
            Interceptor.Intercept(this, default, base.Dispose);
        }

#pragma warning disable CA1816
        public override async ValueTask DisposeAsync()
        {
            await Interceptor.Intercept(this, default, base.DisposeAsync);
        }
#pragma warning restore CA1816

        void IInterceptTargetRaise<IPropertyInterceptEventArgs>.RaiseIntercepting(IPropertyInterceptEventArgs args)
        {
            switch (args.Accessor)
            {
                case PropertyInterceptAccessor.Get:
                    PropertyGetIntercepting?.Invoke(this, args);
                    return;
                case PropertyInterceptAccessor.Set:
                case PropertyInterceptAccessor.Init:
                    PropertySetIntercepting?.Invoke(this, args);
                    return;
                default:
                    throw new EnumUndefinedOrNotSupportedException<PropertyInterceptAccessor>(args.Accessor, nameof(args.Accessor), null);
            }
        }

        void IInterceptTargetRaise<IPropertyInterceptEventArgs>.RaiseIntercepted(IPropertyInterceptEventArgs args)
        {
            switch (args.Accessor)
            {
                case PropertyInterceptAccessor.Get:
                    PropertyGetIntercepted?.Invoke(this, args);
                    return;
                case PropertyInterceptAccessor.Set:
                case PropertyInterceptAccessor.Init:
                    PropertySetIntercepted?.Invoke(this, args);
                    return;
                default:
                    throw new EnumUndefinedOrNotSupportedException<PropertyInterceptAccessor>(args.Accessor, nameof(args.Accessor), null);
            }
        }

        void IInterceptTargetRaise<IMethodInterceptEventArgs>.RaiseIntercepting(IMethodInterceptEventArgs args)
        {
            MethodIntercepting?.Invoke(this, args);
        }

        void IInterceptTargetRaise<IMethodInterceptEventArgs>.RaiseIntercepted(IMethodInterceptEventArgs args)
        {
            MethodIntercepted?.Invoke(this, args);
        }
    }
}