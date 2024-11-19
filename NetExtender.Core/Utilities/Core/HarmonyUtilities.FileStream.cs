using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32.SafeHandles;
using NetExtender.Types.Interception.Interfaces;
using NetExtender.Types.Streams;

namespace NetExtender.Utilities.Core
{
    public static partial class HarmonyUtilities
    {
        [ReflectionSignature]
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private static Object? ChooseStrategy(System.IO.FileStream fileStream, SafeFileHandle handle, FileAccess access, Int32 bufferSize, Boolean isAsync)
        {
            return null;
        }

        [ReflectionSignature]
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private static Object? ChooseStrategy(System.IO.FileStream fileStream, String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options, Int64 preallocationSize)
        {
            return null;
        }

        [ReflectionSignature]
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private static Object? ChooseStrategy(System.IO.FileStream fileStream, String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options, Int64 preallocationSize, Int32? unixCreateMode)
        {
            return null;
        }
        
        public static class FileStream
        {
            public readonly struct Handler
            {
                public EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyIntercept { get; init; }
                public EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyIntercepting { get; init; }
                public EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyIntercepted { get; init; }
                public EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyGetIntercept { get; init; }
                public EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertySetIntercept { get; init; }
                public EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyGetIntercepting { get; init; }
                public EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertySetIntercepting { get; init; }
                public EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyGetIntercepted { get; init; }
                public EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertySetIntercepted { get; init; }
                public EventHandler<InterceptFileStream, IFileStreamInterceptEventArgs>? MethodIntercept { get; init; }
                public EventHandler<InterceptFileStream, IFileStreamInterceptEventArgs>? MethodIntercepting { get; init; }
                public EventHandler<InterceptFileStream, IFileStreamInterceptEventArgs>? MethodIntercepted { get; init; }
            }
        }
        
        [SuppressMessage("Design", "CA1041")]
        [SuppressMessage("ReSharper", "PublicConstructorInAbstractClass")]
        public abstract class InterceptHarmonyFileStream : InterceptFileStream, IInterceptIdentifierTarget<InterceptHarmonyFileStream>
        {
            public new static event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyIntercept;
            public new static event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyIntercepting;
            public new static event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyIntercepted;
            public new static event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyGetIntercept;
            public new static event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertySetIntercept;
            public new static event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyGetIntercepting;
            public new static event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertySetIntercepting;
            public new static event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertyGetIntercepted;
            public new static event EventHandler<InterceptFileStream, IPropertyInterceptEventArgs>? PropertySetIntercepted;
            public new static event EventHandler<InterceptFileStream, IFileStreamInterceptEventArgs>? MethodIntercept;
            public new static event EventHandler<InterceptFileStream, IFileStreamInterceptEventArgs>? MethodIntercepting;
            public new static event EventHandler<InterceptFileStream, IFileStreamInterceptEventArgs>? MethodIntercepted;
            
            static InterceptHarmonyFileStream()
            {
                Intercept<InterceptHarmonyFileStream>.Regex = new Regex("(Intercept|FileStream)", RegexOptions.Compiled);
            }
            
            public sealed override String Identifier
            {
                get
                {
                    return _identifier ??= Intercept<InterceptHarmonyFileStream>.GetName(GetType());
                }
                init
                {
                    throw new NotSupportedException();
                }
            }
            
            public InterceptHarmonyFileStream(String path, FileMode mode)
                : base(path, mode)
            {
                Initialize();
            }

            public InterceptHarmonyFileStream(String path, FileMode mode, FileAccess access)
                : base(path, mode, access)
            {
                Initialize();
            }

            public InterceptHarmonyFileStream(String path, FileMode mode, FileAccess access, FileShare share)
                : base(path, mode, access, share)
            {
                Initialize();
            }

            public InterceptHarmonyFileStream(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize)
                : base(path, mode, access, share, bufferSize)
            {
                Initialize();
            }

            public InterceptHarmonyFileStream(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, Boolean useAsync)
                : base(path, mode, access, share, bufferSize, useAsync)
            {
                Initialize();
            }

            public InterceptHarmonyFileStream(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options)
                : base(path, mode, access, share, bufferSize, options)
            {
                Initialize();
            }

            public InterceptHarmonyFileStream(String path, FileStreamOptions options)
                : base(path, options)
            {
                Initialize();
            }
            
            [Obsolete]
            public InterceptHarmonyFileStream(IntPtr handle, FileAccess access)
                : base(handle, access)
            {
                Initialize();
            }

            [Obsolete]
            public InterceptHarmonyFileStream(IntPtr handle, FileAccess access, Boolean ownsHandle)
                : base(handle, access, ownsHandle)
            {
                Initialize();
            }

            [Obsolete]
            public InterceptHarmonyFileStream(IntPtr handle, FileAccess access, Boolean ownsHandle, Int32 bufferSize)
                : base(handle, access, ownsHandle, bufferSize)
            {
                Initialize();
            }

            [Obsolete]
            public InterceptHarmonyFileStream(IntPtr handle, FileAccess access, Boolean ownsHandle, Int32 bufferSize, Boolean isAsync)
                : base(handle, access, ownsHandle, bufferSize, isAsync)
            {
                Initialize();
            }
            
            public InterceptHarmonyFileStream(SafeFileHandle handle, FileAccess access)
                : base(handle, access)
            {
                Initialize();
            }

            public InterceptHarmonyFileStream(SafeFileHandle handle, FileAccess access, Int32 bufferSize)
                : base(handle, access, bufferSize)
            {
                Initialize();
            }

            public InterceptHarmonyFileStream(SafeFileHandle handle, FileAccess access, Int32 bufferSize, Boolean isAsync)
                : base(handle, access, bufferSize, isAsync)
            {
                Initialize();
            }

            private static void OnPropertyIntercept(InterceptFileStream sender, IPropertyInterceptEventArgs args)
            {
                PropertyIntercept?.Invoke(sender, args);
            }

            private static void OnPropertyIntercepting(InterceptFileStream sender, IPropertyInterceptEventArgs args)
            {
                PropertyIntercepting?.Invoke(sender, args);
            }

            private static void OnPropertyIntercepted(InterceptFileStream sender, IPropertyInterceptEventArgs args)
            {
                PropertyIntercepted?.Invoke(sender, args);
            }

            private static void OnPropertyGetIntercept(InterceptFileStream sender, IPropertyInterceptEventArgs args)
            {
                PropertyGetIntercept?.Invoke(sender, args);
            }

            private static void OnPropertySetIntercept(InterceptFileStream sender, IPropertyInterceptEventArgs args)
            {
                PropertySetIntercept?.Invoke(sender, args);
            }

            private static void OnPropertyGetIntercepting(InterceptFileStream sender, IPropertyInterceptEventArgs args)
            {
                PropertyGetIntercepting?.Invoke(sender, args);
            }

            private static void OnPropertySetIntercepting(InterceptFileStream sender, IPropertyInterceptEventArgs args)
            {
                PropertySetIntercepting?.Invoke(sender, args);
            }

            private static void OnPropertyGetIntercepted(InterceptFileStream sender, IPropertyInterceptEventArgs args)
            {
                PropertyGetIntercepted?.Invoke(sender, args);
            }

            private static void OnPropertySetIntercepted(InterceptFileStream sender, IPropertyInterceptEventArgs args)
            {
                PropertySetIntercepted?.Invoke(sender, args);
            }

            private static void OnMethodIntercept(InterceptFileStream sender, IFileStreamInterceptEventArgs args)
            {
                MethodIntercept?.Invoke(sender, args);
            }

            private static void OnMethodIntercepting(InterceptFileStream sender, IFileStreamInterceptEventArgs args)
            {
                MethodIntercepting?.Invoke(sender, args);
            }

            private static void OnMethodIntercepted(InterceptFileStream sender, IFileStreamInterceptEventArgs args)
            {
                MethodIntercepted?.Invoke(sender, args);
            }

            // ReSharper disable once CognitiveComplexity
            private void Initialize()
            {
                FileStream.Handler handler = Intercept<InterceptHarmonyFileStream, FileStream.Handler>.Add(this);

                if (handler.PropertyIntercept is not null)
                {
                    base.PropertyIntercept += handler.PropertyIntercept;
                }

                if (handler.PropertyIntercepting is not null)
                {
                    base.PropertyIntercepting += handler.PropertyIntercepting;
                }

                if (handler.PropertyIntercepted is not null)
                {
                    base.PropertyIntercepted += handler.PropertyIntercepted;
                }

                if (handler.PropertyGetIntercept is not null)
                {
                    base.PropertyGetIntercept += handler.PropertyGetIntercept;
                }

                if (handler.PropertyGetIntercepting is not null)
                {
                    base.PropertyGetIntercepting += handler.PropertyGetIntercepting;
                }

                if (handler.PropertyGetIntercepted is not null)
                {
                    base.PropertyGetIntercepted += handler.PropertyGetIntercepted;
                }

                if (handler.PropertySetIntercept is not null)
                {
                    base.PropertySetIntercept += handler.PropertySetIntercept;
                }

                if (handler.PropertySetIntercepting is not null)
                {
                    base.PropertySetIntercepting += handler.PropertySetIntercepting;
                }

                if (handler.PropertySetIntercepted is not null)
                {
                    base.PropertySetIntercepted += handler.PropertySetIntercepted;
                }

                if (handler.MethodIntercept is not null)
                {
                    base.MethodIntercept += handler.MethodIntercept;
                }

                if (handler.MethodIntercepting is not null)
                {
                    base.MethodIntercepting += handler.MethodIntercepting;
                }

                if (handler.MethodIntercepted is not null)
                {
                    base.MethodIntercepted += handler.MethodIntercepted;
                }

                base.PropertyIntercept += OnPropertyIntercept;
                base.PropertyIntercepting += OnPropertyIntercepting;
                base.PropertyIntercepted += OnPropertyIntercepted;
                base.PropertyGetIntercept += OnPropertyGetIntercept;
                base.PropertyGetIntercepting += OnPropertyGetIntercepting;
                base.PropertyGetIntercepted += OnPropertyGetIntercepted;
                base.PropertySetIntercept += OnPropertySetIntercept;
                base.PropertySetIntercepting += OnPropertySetIntercepting;
                base.PropertySetIntercepted += OnPropertySetIntercepted;
                base.MethodIntercept += OnMethodIntercept;
                base.MethodIntercepting += OnMethodIntercepting;
                base.MethodIntercepted += OnMethodIntercepted;
            }

            protected override void Dispose(Boolean disposing)
            {
                base.Dispose(disposing);
                base.PropertyIntercept -= OnPropertyIntercept;
                base.PropertyIntercepting -= OnPropertyIntercepting;
                base.PropertyIntercepted -= OnPropertyIntercepted;
                base.PropertyGetIntercept -= OnPropertyGetIntercept;
                base.PropertyGetIntercepting -= OnPropertyGetIntercepting;
                base.PropertyGetIntercepted -= OnPropertyGetIntercepted;
                base.PropertySetIntercept -= OnPropertySetIntercept;
                base.PropertySetIntercepting -= OnPropertySetIntercepting;
                base.PropertySetIntercepted -= OnPropertySetIntercepted;
                base.MethodIntercept -= OnMethodIntercept;
                base.MethodIntercepting -= OnMethodIntercepting;
                base.MethodIntercepted -= OnMethodIntercepted;
                Intercept<InterceptHarmonyFileStream>.Remove(this);
            }
        }
    }
}