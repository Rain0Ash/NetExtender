// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Intercept;
using NetExtender.Types.Intercept.Interfaces;
using NetExtender.Types.Memory;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Streams
{
    public delegate void FileStreamInterceptEventHandler(InterceptFileStream stream, FileStreamInterceptEventArgs args);
    public delegate ValueTask FileStreamInterceptEventAsyncHandler(InterceptFileStream stream, FileStreamInterceptEventArgs args);

    public enum FileStreamIntercept : Byte
    {
        Invocation = 0,
        Get = 1,
        Set = 2,
        Seek = 3,
        Lock = 4,
        Unlock = 5,
        Read = 6,
        Write = 7,
        Flush = 8,
        Close = 9,
        Copy = 10,
        Dispose = 11,
        GetHashCode = 12,
        Equals = 13,
        ToString = 14
    }
    
    [SuppressMessage("Design", "CA1041")]
    public class InterceptFileStream : FileStreamWrapper, IInterceptIdentifierTarget<InterceptFileStream>, IPropertyIntercept<InterceptFileStream, IPropertyInterceptEventArgs>, IInterceptTargetRaise<IPropertyInterceptEventArgs>, IMethodIntercept<InterceptFileStream, IFileStreamInterceptEventArgs>, IInterceptTargetRaise<IFileStreamInterceptEventArgs>
    {
        protected IAnyMemberInterceptor<InterceptFileStream, IPropertyInterceptEventArgs, FileStreamInterceptEventArgs, Info> Interceptor { get; }
        
        public event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyIntercept
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
        
        public event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyIntercepting
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
        
        public event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyIntercepted
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
        
        public event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyGetIntercept
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
        
        public event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertySetIntercept
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
        
        public event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyGetIntercepting;
        public event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertySetIntercepting;
        public event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyGetIntercepted;
        public event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertySetIntercepted;
        
        public event EventHandler<InterceptFileStream, IFileStreamInterceptEventArgs>? MethodIntercept
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
        
        public event EventHandler<InterceptFileStream, IFileStreamInterceptEventArgs>? MethodIntercepting;
        public event EventHandler<InterceptFileStream, IFileStreamInterceptEventArgs>? MethodIntercepted;
        
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
        
        public override String Name
        {
            get
            {
                return Interceptor.InterceptGet<String>(this);
            }
        }

        [Obsolete]
        public override IntPtr Handle
        {
            get
            {
                return Interceptor.InterceptGet<IntPtr>(this);
            }
        }

        public override SafeFileHandle SafeFileHandle
        {
            get
            {
                return Interceptor.InterceptGet<SafeFileHandle>(this);
            }
        }

        public override Boolean IsAsync
        {
            get
            {
                return Interceptor.InterceptGet<Boolean>(this);
            }
        }

        public override Boolean CanSeek
        {
            get
            {
                return Interceptor.InterceptGet<Boolean>(this);
            }
        }

        public override Boolean CanRead
        {
            get
            {
                return Interceptor.InterceptGet<Boolean>(this);
            }
        }

        public override Boolean CanWrite
        {
            get
            {
                return Interceptor.InterceptGet<Boolean>(this);
            }
        }

        public override Int64 Length
        {
            get
            {
                return Interceptor.InterceptGet<Int64>(this);
            }
        }

        public override Int64 Position
        {
            get
            {
                return Interceptor.InterceptGet<Int64>(this);
            }
            set
            {
                Interceptor.InterceptSet(this, value);
            }
        }

        public override Boolean CanTimeout
        {
            get
            {
                return Interceptor.InterceptGet<Boolean>(this);
            }
        }

        public override Int32 ReadTimeout
        {
            get
            {
                return Interceptor.InterceptGet<Int32>(this);
            }
            set
            {
                Interceptor.InterceptSet(this, value);
            }
        }

        public override Int32 WriteTimeout
        {
            get
            {
                return Interceptor.InterceptGet<Int32>(this);
            }
            set
            {
                Interceptor.InterceptSet(this, value);
            }
        }
        
        protected static ArrayPool<Byte> Pool { get; } = ArrayPool<Byte>.Create();

        public InterceptFileStream(String path, FileMode mode)
            : base(path, mode)
        {
            Interceptor = new FileStreamInterceptor(this);
        }

        public InterceptFileStream(String path, FileMode mode, FileAccess access)
            : base(path, mode, access)
        {
            Interceptor = new FileStreamInterceptor(this);
        }

        public InterceptFileStream(String path, FileMode mode, FileAccess access, FileShare share)
            : base(path, mode, access, share)
        {
            Interceptor = new FileStreamInterceptor(this);
        }

        public InterceptFileStream(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize)
            : base(path, mode, access, share, bufferSize)
        {
            Interceptor = new FileStreamInterceptor(this);
        }

        public InterceptFileStream(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, Boolean useAsync)
            : base(path, mode, access, share, bufferSize, useAsync)
        {
            Interceptor = new FileStreamInterceptor(this);
        }

        public InterceptFileStream(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options)
            : base(path, mode, access, share, bufferSize, options)
        {
            Interceptor = new FileStreamInterceptor(this);
        }

        public InterceptFileStream(String path, FileStreamOptions options)
            : base(path, options)
        {
            Interceptor = new FileStreamInterceptor(this);
        }

        [Obsolete]
        public InterceptFileStream(IntPtr handle, FileAccess access)
            : base(handle, access)
        {
            Interceptor = new FileStreamInterceptor(this);
        }

        [Obsolete]
        public InterceptFileStream(IntPtr handle, FileAccess access, Boolean ownsHandle)
            : base(handle, access, ownsHandle)
        {
            Interceptor = new FileStreamInterceptor(this);
        }

        [Obsolete]
        public InterceptFileStream(IntPtr handle, FileAccess access, Boolean ownsHandle, Int32 bufferSize)
            : base(handle, access, ownsHandle, bufferSize)
        {
            Interceptor = new FileStreamInterceptor(this);
        }

        [Obsolete]
        public InterceptFileStream(IntPtr handle, FileAccess access, Boolean ownsHandle, Int32 bufferSize, Boolean isAsync)
            : base(handle, access, ownsHandle, bufferSize, isAsync)
        {
            Interceptor = new FileStreamInterceptor(this);
        }

        public InterceptFileStream(SafeFileHandle handle, FileAccess access)
            : base(handle, access)
        {
            Interceptor = new FileStreamInterceptor(this);
        }

        public InterceptFileStream(SafeFileHandle handle, FileAccess access, Int32 bufferSize)
            : base(handle, access, bufferSize)
        {
            Interceptor = new FileStreamInterceptor(this);
        }

        public InterceptFileStream(SafeFileHandle handle, FileAccess access, Int32 bufferSize, Boolean isAsync)
            : base(handle, access, bufferSize, isAsync)
        {
            Interceptor = new FileStreamInterceptor(this);
        }
        
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("macos")]
        [UnsupportedOSPlatform("tvos")]
        public override void Lock(Int64 position, Int64 length)
        {
            Info info = new Info(FileStreamIntercept.Lock);
            Interceptor.Intercept(this, info, base.Lock, position, length);
        }

        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("macos")]
        [UnsupportedOSPlatform("tvos")]
        public override void Unlock(Int64 position, Int64 length)
        {
            Info info = new Info(FileStreamIntercept.Unlock);
            Interceptor.Intercept(this, info, base.Unlock, position, length);
        }

        public override void SetLength(Int64 value)
        {
            Info info = new Info(FileStreamIntercept.Set);
            Interceptor.Intercept(this, info, base.SetLength, value);
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            Info info = new Info(FileStreamIntercept.Seek);
            return Interceptor.Intercept(this, info, base.Seek, offset, origin);
        }

        public override Int32 ReadByte()
        {
            Info info = new Info(FileStreamIntercept.Read);
            return Interceptor.Intercept(this, info, base.ReadByte);
        }

        private Int32 ReadCore(Span<Byte> buffer)
        {
            return base.Read(buffer);
        }

        public sealed override unsafe Int32 Read(Span<Byte> buffer)
        {
            fixed (Byte* _ = buffer)
            {
                using UnsafeMemory<Byte> memory = new UnsafeMemory<Byte>(buffer);
                return Read(memory);
            }
        }

        public virtual Int32 Read(Memory<Byte> buffer)
        {
            static Int32 Core(InterceptFileStream stream, Memory<Byte> buffer)
            {
                return stream.ReadCore(buffer.Span);
            }
            
            Info info = new Info(FileStreamIntercept.Read) { Buffer = buffer };
            return Interceptor.Intercept(this, info, Core, this, buffer);
        }

        public override async ValueTask<Int32> ReadAsync(Memory<Byte> buffer, CancellationToken token = default)
        {
            Info info = new Info(FileStreamIntercept.Read) { Buffer = buffer, Token = token };
            return await Interceptor.Intercept(this, info, base.ReadAsync, buffer, token);
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            Info info = new Info(FileStreamIntercept.Read) { Buffer = buffer, Offset = offset, Count = count };
            return Interceptor.Intercept(this, info, base.Read, buffer, offset, count);
        }

        public override async Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            Info info = new Info(FileStreamIntercept.Read) { Buffer = buffer, Offset = offset, Count = count, Token = token };
            return await Interceptor.Intercept(this, info, base.ReadAsync, buffer, offset, count, token);
        }

        public override void WriteByte(Byte value)
        {
            Info info = new Info(FileStreamIntercept.Write);
            Interceptor.Intercept(this, info, base.WriteByte, value);
        }

        private void WriteCore(ReadOnlySpan<Byte> buffer)
        {
            base.Write(buffer);
        }

        public sealed override unsafe void Write(ReadOnlySpan<Byte> buffer)
        {
            fixed (Byte* _ = buffer)
            {
                using UnsafeMemory<Byte> memory = new UnsafeMemory<Byte>(buffer);
                Write(memory);
            }
        }

        public virtual void Write(ReadOnlyMemory<Byte> buffer)
        {
            static void Core(InterceptFileStream stream, ReadOnlyMemory<Byte> buffer)
            {
                stream.WriteCore(buffer.Span);
            }

            Byte[] array = Pool.Rent(buffer.Length);

            try
            {
                buffer.CopyTo(array);
                Info info = new Info(FileStreamIntercept.Write) { Buffer = new Memory<Byte>(array, 0, buffer.Length) };
                Interceptor.Intercept(this, info, Core, this, (ReadOnlyMemory<Byte>) info.Buffer);
            }
            finally
            {
                Pool.Return(array);
            }
        }

        public override async ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer, CancellationToken token = default)
        {
            Byte[] array = Pool.Rent(buffer.Length);

            try
            {
                buffer.CopyTo(array);
                Info info = new Info(FileStreamIntercept.Write) { Buffer = new Memory<Byte>(array, 0, buffer.Length), Token = token };
                await Interceptor.Intercept(this, info, base.WriteAsync, (ReadOnlyMemory<Byte>) info.Buffer, token);
            }
            finally
            {
                Pool.Return(array);
            }
        }

        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            Byte[] array = Pool.Rent(buffer, offset, count, out Memory<Byte> memory);

            try
            {
                Info info = new Info(FileStreamIntercept.Write) { Buffer = memory };
                Interceptor.Intercept(this, info, base.Write, array, 0, memory.Length);
            }
            finally
            {
                Pool.Return(array);
            }
        }

        public override async Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            Byte[] array = Pool.Rent(buffer, offset, count, out Memory<Byte> memory);

            try
            {
                Info info = new Info(FileStreamIntercept.Write) { Buffer = memory, Token = token };
                await Interceptor.Intercept(this, info, base.WriteAsync, array, 0, memory.Length, token);
            }
            finally
            {
                Pool.Return(array);
            }
        }

        public override IAsyncResult BeginRead(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback? callback, Object? state)
        {
            Byte[] array = Pool.Rent(buffer, offset, count, out Memory<Byte> memory);

            try
            {
                Info info = new Info(FileStreamIntercept.Read) { Buffer = memory };
                return Interceptor.Intercept(this, info, base.BeginRead, array, 0, memory.Length, callback, state);
            }
            finally
            {
                Pool.Return(array);
            }
        }

        public override IAsyncResult BeginWrite(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback? callback, Object? state)
        {
            Byte[] array = Pool.Rent(buffer, offset, count, out Memory<Byte> memory);

            try
            {
                Info info = new Info(FileStreamIntercept.Write) { Buffer = memory };
                return Interceptor.Intercept(this, info, base.BeginWrite, array, 0, memory.Length, callback, state);
            }
            finally
            {
                Pool.Return(array);
            }
        }

        public override Int32 EndRead(IAsyncResult result)
        {
            Info info = new Info(FileStreamIntercept.Read);
            return Interceptor.Intercept(this, info, base.EndRead, result);
        }

        public override void EndWrite(IAsyncResult result)
        {
            Info info = new Info(FileStreamIntercept.Write);
            Interceptor.Intercept(this, info, base.EndWrite, result);
        }

        public override void Flush()
        {
            Info info = new Info(FileStreamIntercept.Flush);
            Interceptor.Intercept(this, info, base.Flush);
        }

        public override void Flush(Boolean disk)
        {
            Info info = new Info(FileStreamIntercept.Flush);
            Interceptor.Intercept(this, info, base.Flush, disk);
        }

        public override async Task FlushAsync(CancellationToken token)
        {
            Info info = new Info(FileStreamIntercept.Dispose) { Token = token };
            await Interceptor.Intercept(this, info, base.FlushAsync, token);
        }

        public override void Close()
        {
            Info info = new Info(FileStreamIntercept.Close);
            Interceptor.Intercept(this, info, base.Close);
        }

        public override void CopyTo(Stream destination, Int32 buffer)
        {
            Info info = new Info(FileStreamIntercept.Copy);
            Interceptor.Intercept(this, info, base.CopyTo, destination, buffer);
        }

        public override async Task CopyToAsync(Stream destination, Int32 buffer, CancellationToken token)
        {
            Info info = new Info(FileStreamIntercept.Copy) { Token = token };
            await Interceptor.Intercept(this, info, base.CopyToAsync, destination, buffer, token);
        }

        protected override void Dispose(Boolean disposing)
        {
            Info info = new Info(FileStreamIntercept.Dispose);
            Interceptor.Intercept(this, info, base.Dispose, disposing);
        }

#pragma warning disable CA1816
        public override async ValueTask DisposeAsync()
        {
            Info info = new Info(FileStreamIntercept.Dispose);
            await Interceptor.Intercept(this, info, base.DisposeAsync);
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

        void IInterceptTargetRaise<IFileStreamInterceptEventArgs>.RaiseIntercepting(IFileStreamInterceptEventArgs args)
        {
            MethodIntercepting?.Invoke(this, args);
        }

        void IInterceptTargetRaise<IFileStreamInterceptEventArgs>.RaiseIntercepted(IFileStreamInterceptEventArgs args)
        {
            MethodIntercepted?.Invoke(this, args);
        }

        protected internal readonly struct Info
        {
            public FileStreamIntercept Intercept { get; }
            public Memory<Byte>? Buffer { get; init; }
            public Int32? Offset { get; init; }
            public Int32? Count { get; init; }
            public CancellationToken Token { get; init; }

            public Info(FileStreamIntercept intercept)
            {
                Intercept = intercept;
                Buffer = null;
                Offset = null;
                Count = null;
                Token = CancellationToken.None;
            }
        }
        
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        protected sealed class FileStreamInterceptor : AnyMemberInterceptor<InterceptFileStream, IPropertyInterceptEventArgs, FileStreamInterceptEventArgs, Info>
        {
            public FileStreamInterceptor(InterceptFileStream stream)
                : base(PropertyInterceptor<InterceptFileStream, Info>.Default, new Interceptor(stream))
            {
            }

            private sealed class Interceptor : MethodInterceptor<InterceptFileStream, IFileStreamInterceptEventArgs, Info>, IMethodInterceptEventArgsFactory<IFileStreamInterceptEventArgs, Info>
            {
                private InterceptFileStream Stream { get; }

                public Interceptor(InterceptFileStream stream)
                {
                    Stream = stream ?? throw new ArgumentNullException(nameof(stream));
                    Factory = this;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                private static ValueDelegateUtilities.Info Info(MethodInfo method, IEnumerable<Object?>? parameters)
                {
                    return new ValueDelegateUtilities.Info(method, parameters?.ToArray() ?? Array.Empty<Object?>());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                private static ValueDelegateUtilities.Info Info(MethodInfo method, ImmutableArray<Object?> parameters)
                {
                   return new ValueDelegateUtilities.Info(method, parameters.ToArray());
                }

                public IFileStreamInterceptEventArgs Create(MethodInfo method, IEnumerable<Object?>? parameters, Info info)
                {
                    return new FileStreamInterceptEventArgs(Info(method, parameters), Stream.Internal, info, null);
                }

                public IFileStreamInterceptEventArgs Create(MethodInfo method, ImmutableArray<Object?> parameters, Info info)
                {
                    return new FileStreamInterceptEventArgs(Info(method, parameters), Stream.Internal, info, null);
                }

                public IFileStreamInterceptEventArgs Create(MethodInfo method, IEnumerable<Object?>? parameters, Exception exception, Info info)
                {
                    return new FileStreamInterceptEventArgs(Info(method, parameters), Stream.Internal, info, exception);
                }

                public IFileStreamInterceptEventArgs Create(MethodInfo method, ImmutableArray<Object?> parameters, Exception exception, Info info)
                {
                    return new FileStreamInterceptEventArgs(Info(method, parameters), Stream.Internal, info, exception);
                }

                public IFileStreamInterceptEventArgs Create<T>(MethodInfo method, IEnumerable<Object?>? parameters, Info info)
                {
                    return new FileStreamInterceptEventArgs<T>(Info(method, parameters), Stream.Internal, info, null);
                }

                public IFileStreamInterceptEventArgs Create<T>(MethodInfo method, ImmutableArray<Object?> parameters, Info info)
                {
                    return new FileStreamInterceptEventArgs<T>(Info(method, parameters), Stream.Internal, info, null);
                }

                public IFileStreamInterceptEventArgs Create<T>(MethodInfo method, T value, IEnumerable<Object?>? parameters, Info info)
                {
                    return new FileStreamInterceptEventArgs<T>(Info(method, parameters), value, Stream.Internal, info, null);
                }

                public IFileStreamInterceptEventArgs Create<T>(MethodInfo method, T value, ImmutableArray<Object?> parameters, Info info)
                {
                    return new FileStreamInterceptEventArgs<T>(Info(method, parameters), value, Stream.Internal, info, null);
                }

                public IFileStreamInterceptEventArgs Create<T>(MethodInfo method, IEnumerable<Object?>? parameters, Exception exception, Info info)
                {
                    return new FileStreamInterceptEventArgs<T>(Info(method, parameters), Stream.Internal, info, exception);
                }

                public IFileStreamInterceptEventArgs Create<T>(MethodInfo method, ImmutableArray<Object?> parameters, Exception exception, Info info)
                {
                    return new FileStreamInterceptEventArgs<T>(Info(method, parameters), Stream.Internal, info, exception);
                }
            }
        }
    }
}