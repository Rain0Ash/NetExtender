// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Text.RegularExpressions;
using NetExtender.FileSystems.Interfaces;
using NetExtender.Types.Entities;
using NetExtender.Types.Intercept;
using NetExtender.Types.Intercept.Interfaces;
#pragma warning disable CS0618

namespace NetExtender.Utilities.Core
{
    public static partial class HarmonyUtilities
    {
        public static class FileSystem
        {
            public readonly struct Handler
            {
                public EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertyIntercept { get; init; }
                public EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertyIntercepting { get; init; }
                public EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertyIntercepted { get; init; }
                public EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertyGetIntercept { get; init; }
                public EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertySetIntercept { get; init; }
                public EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertyGetIntercepting { get; init; }
                public EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertySetIntercepting { get; init; }
                public EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertyGetIntercepted { get; init; }
                public EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertySetIntercepted { get; init; }
                public EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IMethodInterceptEventArgs>? MethodIntercept { get; init; }
                public EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IMethodInterceptEventArgs>? MethodIntercepting { get; init; }
                public EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IMethodInterceptEventArgs>? MethodIntercepted { get; init; }
            }
        }
        
        public class InterceptHarmonyFileSystem : FileSystemIntercept, IInterceptIdentifierTarget<InterceptHarmonyFileSystem>
        {
            public new static event EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertyIntercept;
            public new static event EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertyIntercepting;
            public new static event EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertyIntercepted;
            public new static event EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertyGetIntercept;
            public new static event EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertySetIntercept;
            public new static event EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertyGetIntercepting;
            public new static event EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertySetIntercepting;
            public new static event EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertyGetIntercepted;
            public new static event EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IPropertyInterceptEventArgs>? PropertySetIntercepted;
            public new static event EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IMethodInterceptEventArgs>? MethodIntercept;
            public new static event EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IMethodInterceptEventArgs>? MethodIntercepting;
            public new static event EventHandler<FileSystemIntercept<IFileSystem, Any.Value>, IMethodInterceptEventArgs>? MethodIntercepted;
            
            static InterceptHarmonyFileSystem()
            {
                Intercept<InterceptHarmonyFileSystem>.Regex = new Regex("(Intercept|FileSystem)", RegexOptions.Compiled);
            }
            
            public override String Identifier
            {
                get
                {
                    return _identifier ??= Intercept<InterceptHarmonyFileSystem>.GetName(GetType());
                }
                init
                {
                    throw new NotSupportedException();
                }
            }
            
            public Type Type { get; }
            
            public InterceptHarmonyFileSystem(Type type)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
                Initialize();
            }

            private static void OnPropertyIntercept(FileSystemIntercept<IFileSystem, Any.Value> sender, IPropertyInterceptEventArgs args)
            {
                PropertyIntercept?.Invoke(sender, args);
            }

            private static void OnPropertyIntercepting(FileSystemIntercept<IFileSystem, Any.Value> sender, IPropertyInterceptEventArgs args)
            {
                PropertyIntercepting?.Invoke(sender, args);
            }

            private static void OnPropertyIntercepted(FileSystemIntercept<IFileSystem, Any.Value> sender, IPropertyInterceptEventArgs args)
            {
                PropertyIntercepted?.Invoke(sender, args);
            }

            private static void OnPropertyGetIntercept(FileSystemIntercept<IFileSystem, Any.Value> sender, IPropertyInterceptEventArgs args)
            {
                PropertyGetIntercept?.Invoke(sender, args);
            }

            private static void OnPropertySetIntercept(FileSystemIntercept<IFileSystem, Any.Value> sender, IPropertyInterceptEventArgs args)
            {
                PropertySetIntercept?.Invoke(sender, args);
            }

            private static void OnPropertyGetIntercepting(FileSystemIntercept<IFileSystem, Any.Value> sender, IPropertyInterceptEventArgs args)
            {
                PropertyGetIntercepting?.Invoke(sender, args);
            }

            private static void OnPropertySetIntercepting(FileSystemIntercept<IFileSystem, Any.Value> sender, IPropertyInterceptEventArgs args)
            {
                PropertySetIntercepting?.Invoke(sender, args);
            }

            private static void OnPropertyGetIntercepted(FileSystemIntercept<IFileSystem, Any.Value> sender, IPropertyInterceptEventArgs args)
            {
                PropertyGetIntercepted?.Invoke(sender, args);
            }

            private static void OnPropertySetIntercepted(FileSystemIntercept<IFileSystem, Any.Value> sender, IPropertyInterceptEventArgs args)
            {
                PropertySetIntercepted?.Invoke(sender, args);
            }

            private static void OnMethodIntercept(FileSystemIntercept<IFileSystem, Any.Value> sender, IMethodInterceptEventArgs args)
            {
                MethodIntercept?.Invoke(sender, args);
            }

            private static void OnMethodIntercepting(FileSystemIntercept<IFileSystem, Any.Value> sender, IMethodInterceptEventArgs args)
            {
                MethodIntercepting?.Invoke(sender, args);
            }

            private static void OnMethodIntercepted(FileSystemIntercept<IFileSystem, Any.Value> sender, IMethodInterceptEventArgs args)
            {
                MethodIntercepted?.Invoke(sender, args);
            }

            // ReSharper disable once CognitiveComplexity
            private void Initialize()
            {
                FileSystem.Handler handler = Intercept<InterceptHarmonyFileSystem, FileSystem.Handler>.Add(this);

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
                Intercept<InterceptHarmonyFileSystem>.Remove(this);
            }
        }
    }
}