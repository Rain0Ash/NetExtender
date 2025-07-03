// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Entities;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Intercept;
using NetExtender.Types.Intercept.Interfaces;
using NetExtender.Types.Memory;
using NetExtender.Types.Streams;

namespace NetExtender.FileSystems
{
    public class InterceptStreamReader : StreamReader, IInterceptIdentifierTarget<InterceptStreamReader>, IPropertyIntercept<InterceptStreamReader, IPropertyInterceptEventArgs>, IInterceptTargetRaise<IPropertyInterceptEventArgs>, IMethodIntercept<InterceptStreamReader, IMethodInterceptEventArgs>, IInterceptTargetRaise<IMethodInterceptEventArgs>
    {
        private const Int32 DefaultBufferSize = 1024;
        private const Int32 DefaultFileStreamBufferSize = 4096;
        
        protected IAnyMemberInterceptor<InterceptStreamReader, IPropertyInterceptEventArgs, FileStreamInterceptEventArgs, Any.Value> Interceptor { get; }
        
        public event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyIntercept
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
        
        public event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyIntercepting
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
        
        public event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyIntercepted
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
        
        public event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyGetIntercept
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
        
        public event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertySetIntercept
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
        
        public event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyGetIntercepting;
        public event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertySetIntercepting;
        public event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyGetIntercepted;
        public event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertySetIntercepted;
        
        public event EventHandler<InterceptStreamReader, IMethodInterceptEventArgs>? MethodIntercept
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
        
        public event EventHandler<InterceptStreamReader, IMethodInterceptEventArgs>? MethodIntercepting;
        public event EventHandler<InterceptStreamReader, IMethodInterceptEventArgs>? MethodIntercepted;
        
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

        public override Encoding CurrentEncoding
        {
            get
            {
                return Interceptor.InterceptGet<Encoding>(this);
            }
        }
        
        public InterceptStreamReader(Stream stream)
            : base(stream)
        {
            Interceptor = new AnyMemberInterceptor<InterceptStreamReader, Any.Value>();
        }

        public InterceptStreamReader(Stream stream, Boolean detectEncodingFromByteOrderMarks)
            : base(stream, detectEncodingFromByteOrderMarks)
        {
            Interceptor = new AnyMemberInterceptor<InterceptStreamReader, Any.Value>();
        }

        public InterceptStreamReader(Stream stream, Encoding encoding)
            : base(stream, encoding)
        {
            Interceptor = new AnyMemberInterceptor<InterceptStreamReader, Any.Value>();
        }

        public InterceptStreamReader(Stream stream, Encoding encoding, Boolean detectEncodingFromByteOrderMarks)
            : base(stream, encoding, detectEncodingFromByteOrderMarks)
        {
            Interceptor = new AnyMemberInterceptor<InterceptStreamReader, Any.Value>();
        }

        public InterceptStreamReader(Stream stream, Encoding encoding, Boolean detectEncodingFromByteOrderMarks, Int32 bufferSize)
            : base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize)
        {
            Interceptor = new AnyMemberInterceptor<InterceptStreamReader, Any.Value>();
        }

        public InterceptStreamReader(Stream stream, Encoding? encoding = null, Boolean detectEncodingFromByteOrderMarks = true, Int32 bufferSize = -1, Boolean leaveOpen = false)
            : base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen)
        {
            Interceptor = new AnyMemberInterceptor<InterceptStreamReader, Any.Value>();
        }

        public InterceptStreamReader(String path)
            : this(path, true)
        {
        }

        public InterceptStreamReader(String path, FileStreamOptions options)
            : this(path, Encoding.UTF8, true, options)
        {
        }

        public InterceptStreamReader(String path, Boolean detectEncodingFromByteOrderMarks)
            : this(path, Encoding.UTF8, detectEncodingFromByteOrderMarks, DefaultBufferSize)
        {
        }

        public InterceptStreamReader(String path, Encoding encoding)
            : this(path, encoding, true, DefaultBufferSize)
        {
        }

        public InterceptStreamReader(String path, Encoding encoding, Boolean detectEncodingFromByteOrderMarks)
            : this(path, encoding, detectEncodingFromByteOrderMarks, DefaultBufferSize)
        {
        }

        public InterceptStreamReader(String path, Encoding encoding, Boolean detectEncodingFromByteOrderMarks, Int32 bufferSize)
            : this(ValidateArgsAndOpenPath(path, encoding, bufferSize), encoding, detectEncodingFromByteOrderMarks, bufferSize, false)
        {
        }

        public InterceptStreamReader(String path, Encoding encoding, Boolean detectEncodingFromByteOrderMarks, FileStreamOptions options)
            : this(ValidateArgsAndOpenPath(path, encoding, options), encoding, detectEncodingFromByteOrderMarks, DefaultBufferSize)
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
            
            if ((options.Access & FileAccess.Read) == 0)
            {
                throw new ArgumentException("Stream not readable.", nameof(options));
            }

            return InterceptStreamReaderUtilities.Raise(path, encoding, options);
        }

        private static Stream ValidateArgsAndOpenPath(String path, Encoding encoding, Int32 buffer)
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
                Mode = FileMode.Open,
                Access = FileAccess.Read,
                Share = FileShare.Read,
                BufferSize = DefaultFileStreamBufferSize
            };
            
            return ValidateArgsAndOpenPath(path, encoding, options);
        }

        public override Int32 Peek()
        {
            return Interceptor.Intercept(this, default, base.Peek);
        }

        public override Int32 Read()
        {
            return Interceptor.Intercept(this, default, base.Read);
        }

        private Int32 ReadCore(Span<Char> buffer)
        {
            return base.Read(buffer);
        }

        public sealed override unsafe Int32 Read(Span<Char> buffer)
        {
            fixed (Char* _ = buffer)
            {
                using UnsafeMemory<Char> memory = new UnsafeMemory<Char>(buffer);
                return Read(memory);
            }
        }

        public virtual Int32 Read(Memory<Char> buffer)
        {
            static Int32 Core(InterceptStreamReader reader, Memory<Char> buffer)
            {
                return reader.ReadCore(buffer.Span);
            }
            
            return Interceptor.Intercept(this, default, Core, this, buffer);
        }

        public override Int32 Read(Char[] buffer, Int32 index, Int32 count)
        {
            return Interceptor.Intercept(this, default, base.Read, buffer, index, count);
        }

        public override async ValueTask<Int32> ReadAsync(Memory<Char> buffer, CancellationToken token = default)
        {
            return await Interceptor.Intercept(this, default, base.ReadAsync, buffer, token);
        }

        public override async Task<Int32> ReadAsync(Char[] buffer, Int32 index, Int32 count)
        {
            return await Interceptor.Intercept(this, default, base.ReadAsync, buffer, index, count);
        }

        public override String? ReadLine()
        {
            return Interceptor.Intercept(this, default, base.ReadLine);
        }

        public override async Task<String?> ReadLineAsync()
        {
            return await Interceptor.Intercept(this, default, base.ReadLineAsync);
        }

        private Int32 ReadBlockCore(Span<Char> buffer)
        {
            return base.ReadBlock(buffer);
        }

        public sealed override unsafe Int32 ReadBlock(Span<Char> buffer)
        {
            fixed (Char* _ = buffer)
            {
                using UnsafeMemory<Char> memory = new UnsafeMemory<Char>(buffer);
                return ReadBlock(memory);
            }
        }

        public virtual Int32 ReadBlock(Memory<Char> buffer)
        {
            static Int32 Core(InterceptStreamReader reader, Memory<Char> buffer)
            {
                return reader.ReadBlockCore(buffer.Span);
            }
            
            return Interceptor.Intercept(this, default, Core, this, buffer);
        }

        public override Int32 ReadBlock(Char[] buffer, Int32 index, Int32 count)
        {
            return Interceptor.Intercept(this, default, base.ReadBlock, buffer, index, count);
        }

        public override async ValueTask<Int32> ReadBlockAsync(Memory<Char> buffer, CancellationToken token = default)
        {
            return await Interceptor.Intercept(this, default, base.ReadBlockAsync, buffer, token);
        }

        public override async Task<Int32> ReadBlockAsync(Char[] buffer, Int32 index, Int32 count)
        {
            return await Interceptor.Intercept(this, default, base.ReadBlockAsync, buffer, index, count);
        }

        public override String ReadToEnd()
        {
            return Interceptor.Intercept(this, default, base.ReadToEnd);
        }

        public override async Task<String> ReadToEndAsync()
        {
            return await Interceptor.Intercept(this, default, base.ReadToEndAsync);
        }

        public override void Close()
        {
            Interceptor.Intercept(this, default, base.Close);
        }

#pragma warning disable CA1816
        protected override void Dispose(Boolean disposing)
        {
            Interceptor.Intercept(this, default, base.Dispose, disposing);
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
    
    public delegate Stream InterceptStreamReaderOpen(String path, Encoding encoding, FileStreamOptions options);
    
    public static class InterceptStreamReaderUtilities
    {
        public static event InterceptStreamReaderOpen? FileStream;

        internal static Stream Raise(String path, Encoding encoding, FileStreamOptions options)
        {
            return FileStream?.Invoke(path, encoding, options) ?? new InterceptFileStream(path, options);
        }
    }
}