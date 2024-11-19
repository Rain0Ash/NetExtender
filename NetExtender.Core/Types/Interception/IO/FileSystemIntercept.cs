using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Versioning;
using NetExtender.IO;
using NetExtender.Types.Entities;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Interception.Interfaces;

namespace NetExtender.Types.Interception
{
    public class FileSystemIntercept : FileSystemIntercept<Any.Value>
    {
        public FileSystemIntercept()
            : base(new AnyMemberInterceptor<FileSystemIntercept<Any.Value>, Any.Value>())
        {
        }
    }
    
    public abstract partial class FileSystemIntercept<TInfo> : FileSystemHandler, IUnsafeFileSystemIntercept, IInterceptIdentifierTarget<FileSystemIntercept<TInfo>>, IPropertyIntercept<FileSystemIntercept<TInfo>, IPropertyInterceptEventArgs>, IInterceptTargetRaise<IPropertyInterceptEventArgs>, IMethodIntercept<FileSystemIntercept<TInfo>, IMethodInterceptEventArgs>, IInterceptTargetRaise<IMethodInterceptEventArgs>, IDisposable
    {
        protected IAnyMemberInterceptor<FileSystemIntercept<TInfo>, TInfo> Interceptor { get; }
        
        public event EventHandler<FileSystemIntercept<TInfo>, IPropertyInterceptEventArgs>? PropertyIntercept
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
        
        public event EventHandler<FileSystemIntercept<TInfo>, IPropertyInterceptEventArgs>? PropertyIntercepting
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
        
        public event EventHandler<FileSystemIntercept<TInfo>, IPropertyInterceptEventArgs>? PropertyIntercepted
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
        
        public event EventHandler<FileSystemIntercept<TInfo>, IPropertyInterceptEventArgs>? PropertyGetIntercept
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
        
        public event EventHandler<FileSystemIntercept<TInfo>, IPropertyInterceptEventArgs>? PropertySetIntercept
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
        
        public event EventHandler<FileSystemIntercept<TInfo>, IPropertyInterceptEventArgs>? PropertyGetIntercepting;
        public event EventHandler<FileSystemIntercept<TInfo>, IPropertyInterceptEventArgs>? PropertySetIntercepting;
        public event EventHandler<FileSystemIntercept<TInfo>, IPropertyInterceptEventArgs>? PropertyGetIntercepted;
        public event EventHandler<FileSystemIntercept<TInfo>, IPropertyInterceptEventArgs>? PropertySetIntercepted;
        
        public event EventHandler<FileSystemIntercept<TInfo>, IMethodInterceptEventArgs>? MethodIntercept
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
        
        public event EventHandler<FileSystemIntercept<TInfo>, IMethodInterceptEventArgs>? MethodIntercepting;
        public event EventHandler<FileSystemIntercept<TInfo>, IMethodInterceptEventArgs>? MethodIntercepted;
        
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
        
        protected FileSystemIntercept(IAnyMemberInterceptor<FileSystemIntercept<TInfo>, TInfo> interceptor)
        {
            Interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        }
        
        protected override FileSystemInfo CreateSymbolicLink(String path, String target, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.File => Interceptor.Intercept(this, default, File.CreateSymbolicLink, path, target),
                FileSystemHandlerType.Directory => Interceptor.Intercept(this, default, Directory.CreateSymbolicLink, path, target),
                _ => base.CreateSymbolicLink(path, target, handler)
            };
        }

        protected override FileSystemInfo? ResolveLinkTarget(String path, Boolean target, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.File => Interceptor.Intercept(this, default, File.ResolveLinkTarget, path, target),
                FileSystemHandlerType.Directory => Interceptor.Intercept(this, default, Directory.ResolveLinkTarget, path, target),
                _ => base.ResolveLinkTarget(path, target, handler)
            };
        }

        protected override Boolean Exists([NotNullWhen(true)] String? path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.File => Interceptor.Intercept(this, default, File.Exists, path),
                FileSystemHandlerType.Directory => Interceptor.Intercept(this, default, Directory.Exists, path),
                _ => base.Exists(path, handler)
            };
        }

        protected override DirectoryInfo? GetParent(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Directory => Interceptor.Intercept(this, default, Directory.GetParent, path),
                _ => base.GetParent(path, handler)
            };
        }

        protected override FileAttributes GetAttributes(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.File => Interceptor.Intercept(this, default, File.GetAttributes, path),
                _ => base.GetAttributes(path, handler)
            };
        }

        protected override void SetAttributes(String path, FileAttributes attributes, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.File:
                    Interceptor.Intercept(this, default, File.SetAttributes, path, attributes);
                    return;
                default:
                    base.SetAttributes(path, attributes, handler);
                    return;
            }
        }

        protected override DateTime GetCreationTime(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.File => Interceptor.Intercept(this, default, File.GetCreationTime, path),
                FileSystemHandlerType.Directory => Interceptor.Intercept(this, default, Directory.GetCreationTime, path),
                _ => base.GetCreationTime(path, handler)
            };
        }

        protected override DateTime GetCreationTimeUtc(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.File => Interceptor.Intercept(this, default, File.GetCreationTimeUtc, path),
                FileSystemHandlerType.Directory => Interceptor.Intercept(this, default, Directory.GetCreationTimeUtc, path),
                _ => base.GetCreationTimeUtc(path, handler)
            };
        }

        protected override DateTime GetLastAccessTime(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.File => Interceptor.Intercept(this, default, File.GetLastAccessTime, path),
                FileSystemHandlerType.Directory => Interceptor.Intercept(this, default, Directory.GetLastAccessTime, path),
                _ => base.GetLastAccessTime(path, handler)
            };
        }

        protected override DateTime GetLastAccessTimeUtc(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.File => Interceptor.Intercept(this, default, File.GetLastAccessTimeUtc, path),
                FileSystemHandlerType.Directory => Interceptor.Intercept(this, default, Directory.GetLastAccessTimeUtc, path),
                _ => base.GetLastAccessTimeUtc(path, handler)
            };
        }

        protected override DateTime GetLastWriteTime(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.File => Interceptor.Intercept(this, default, File.GetLastWriteTime, path),
                FileSystemHandlerType.Directory => Interceptor.Intercept(this, default, Directory.GetLastWriteTime, path),
                _ => base.GetLastWriteTime(path, handler)
            };
        }

        protected override DateTime GetLastWriteTimeUtc(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.File => Interceptor.Intercept(this, default, File.GetLastWriteTimeUtc, path),
                FileSystemHandlerType.Directory => Interceptor.Intercept(this, default, Directory.GetLastWriteTimeUtc, path),
                _ => base.GetLastWriteTimeUtc(path, handler)
            };
        }

        protected override void SetCreationTime(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.File:
                    Interceptor.Intercept(this, default, File.SetCreationTime, path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    Interceptor.Intercept(this, default, Directory.SetCreationTime, path, time);
                    return;
                default:
                    base.SetCreationTime(path, time, handler);
                    return;
            }
        }

        protected override void SetCreationTimeUtc(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.File:
                    Interceptor.Intercept(this, default, File.SetCreationTimeUtc, path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    Interceptor.Intercept(this, default, Directory.SetCreationTimeUtc, path, time);
                    return;
                default:
                    base.SetCreationTimeUtc(path, time, handler);
                    return;
            }
        }

        protected override void SetLastAccessTime(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.File:
                    Interceptor.Intercept(this, default, File.SetLastAccessTime, path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    Interceptor.Intercept(this, default, Directory.SetLastAccessTime, path, time);
                    return;
                default:
                    base.SetLastAccessTime(path, time, handler);
                    return;
            }
        }

        protected override void SetLastAccessTimeUtc(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.File:
                    Interceptor.Intercept(this, default, File.SetLastAccessTimeUtc, path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    Interceptor.Intercept(this, default, Directory.SetLastAccessTimeUtc, path, time);
                    return;
                default:
                    base.SetLastAccessTimeUtc(path, time, handler);
                    return;
            }
        }

        protected override void SetLastWriteTime(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.File:
                    Interceptor.Intercept(this, default, File.SetLastWriteTime, path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    Interceptor.Intercept(this, default, Directory.SetLastWriteTime, path, time);
                    return;
                default:
                    base.SetLastWriteTime(path, time, handler);
                    return;
            }
        }

        protected override void SetLastWriteTimeUtc(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.File:
                    Interceptor.Intercept(this, default, File.SetLastWriteTimeUtc, path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    Interceptor.Intercept(this, default, Directory.SetLastWriteTimeUtc, path, time);
                    return;
                default:
                    base.SetLastWriteTimeUtc(path, time, handler);
                    return;
            }
        }

        protected override FileStream Create(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.File => Interceptor.Intercept(this, default, File.Create, path),
                _ => base.Create(path, handler)
            };
        }

        [SupportedOSPlatform("windows")]
        protected override void Encrypt(String path, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.File:
                    Interceptor.Intercept(this, default, File.Encrypt, path);
                    return;
                default:
                    base.Encrypt(path, handler);
                    return;
            }
        }

        [SupportedOSPlatform("windows")]
        protected override void Decrypt(String path, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.File:
                    Interceptor.Intercept(this, default, File.Decrypt, path);
                    return;
                default:
                    base.Decrypt(path, handler);
                    return;
            }
        }

        protected override void Move(String source, String destination, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.File:
                    Interceptor.Intercept(this, default, File.Move, source, destination);
                    return;
                case FileSystemHandlerType.Directory:
                    Interceptor.Intercept(this, default, Directory.Move, source, destination);
                    return;
                default:
                    base.Move(source, destination, handler);
                    return;
            }
        }

        protected override void Move(String source, String destination, Boolean overwrite, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.File:
                    Interceptor.Intercept(this, default, File.Move, source, destination);
                    return;
                default:
                    base.Move(source, destination, overwrite, handler);
                    return;
            }
        }

        protected override void Copy(String source, String destination, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.File:
                    Interceptor.Intercept(this, default, File.Copy, source, destination);
                    return;
                default:
                    base.Copy(source, destination, handler);
                    return;
            }
        }

        protected override void Copy(String source, String destination, Boolean overwrite, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.File:
                    Interceptor.Intercept(this, default, File.Copy, source, destination);
                    return;
                default:
                    base.Copy(source, destination, overwrite, handler);
                    return;
            }
        }

        protected override void Replace(String source, String destination, String? backup, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.File:
                    Interceptor.Intercept(this, default, File.Replace, source, destination, backup);
                    return;
                default:
                    base.Replace(source, destination, backup, handler);
                    return;
            }
        }

        protected override void Replace(String source, String destination, String? backup, Boolean suppress, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.File:
                    Interceptor.Intercept(this, default, File.Replace, source, destination, backup, suppress);
                    return;
                default:
                    base.Replace(source, destination, backup, suppress, handler);
                    return;
            }
        }

        protected override void Delete(String path, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.File:
                    Interceptor.Intercept(this, default, File.Delete, path);
                    return;
                case FileSystemHandlerType.Directory:
                    Interceptor.Intercept(this, default, Directory.Delete, path);
                    return;
                default:
                    base.Delete(path, handler);
                    return;
            }
        }

        protected override void Delete(String path, Boolean recursive, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Directory:
                    Interceptor.Intercept(this, default, Directory.Delete, path, recursive);
                    return;
                default:
                    base.Delete(path, recursive, handler);
                    return;
            }
        }

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
        }

        ~FileSystemIntercept()
        {
            Dispose(false);
        }
    }
}