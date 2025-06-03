// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NetExtender.Types.Intercept;
using NetExtender.Types.Intercept.Interfaces;

namespace NetExtender.Utilities.Core
{
    public static partial class HarmonyUtilities
    {
        public static class StreamWriter
        {
            public readonly struct Handler
            {
                public EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyIntercept { get; init; }
                public EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyIntercepting { get; init; }
                public EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyIntercepted { get; init; }
                public EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyGetIntercept { get; init; }
                public EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertySetIntercept { get; init; }
                public EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyGetIntercepting { get; init; }
                public EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertySetIntercepting { get; init; }
                public EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyGetIntercepted { get; init; }
                public EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertySetIntercepted { get; init; }
                public EventHandler<InterceptStreamWriter, IMethodInterceptEventArgs>? MethodIntercept { get; init; }
                public EventHandler<InterceptStreamWriter, IMethodInterceptEventArgs>? MethodIntercepting { get; init; }
                public EventHandler<InterceptStreamWriter, IMethodInterceptEventArgs>? MethodIntercepted { get; init; }
            }
        }
        
        [SuppressMessage("ReSharper", "PublicConstructorInAbstractClass")]
        public abstract class InterceptHarmonyStreamWriter : InterceptStreamWriter, IInterceptIdentifierTarget<InterceptHarmonyStreamWriter>
        {
            public new static event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyIntercept;
            public new static event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyIntercepting;
            public new static event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyIntercepted;
            public new static event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyGetIntercept;
            public new static event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertySetIntercept;
            public new static event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyGetIntercepting;
            public new static event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertySetIntercepting;
            public new static event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertyGetIntercepted;
            public new static event EventHandler<InterceptStreamWriter, IPropertyInterceptEventArgs>? PropertySetIntercepted;
            public new static event EventHandler<InterceptStreamWriter, IMethodInterceptEventArgs>? MethodIntercept;
            public new static event EventHandler<InterceptStreamWriter, IMethodInterceptEventArgs>? MethodIntercepting;
            public new static event EventHandler<InterceptStreamWriter, IMethodInterceptEventArgs>? MethodIntercepted;
            
            static InterceptHarmonyStreamWriter()
            {
                Intercept<InterceptHarmonyStreamWriter>.Regex = new Regex("(Intercept|StreamWriter)", RegexOptions.Compiled);
            }
            
            public sealed override String Identifier
            {
                get
                {
                    return _identifier ??= Intercept<InterceptHarmonyStreamWriter>.GetName(GetType());
                }
                init
                {
                    throw new NotSupportedException();
                }
            }
            
            protected InterceptHarmonyStreamWriter(Stream stream)
                : base(stream)
            {
                Initialize();
            }

            protected InterceptHarmonyStreamWriter(Stream stream, Encoding encoding)
                : base(stream, encoding)
            {
                Initialize();
            }

            protected InterceptHarmonyStreamWriter(Stream stream, Encoding encoding, Int32 bufferSize)
                : base(stream, encoding, bufferSize)
            {
                Initialize();
            }

            protected InterceptHarmonyStreamWriter(Stream stream, Encoding? encoding = null, Int32 bufferSize = -1, Boolean leaveOpen = false)
                : base(stream, encoding, bufferSize, leaveOpen)
            {
                Initialize();
            }

            protected InterceptHarmonyStreamWriter(String path)
                : base(path)
            {
                Initialize();
            }

            protected InterceptHarmonyStreamWriter(String path, FileStreamOptions options)
                : base(path, options)
            {
                Initialize();
            }

            protected InterceptHarmonyStreamWriter(String path, Boolean append)
                : base(path, append)
            {
                Initialize();
            }

            protected InterceptHarmonyStreamWriter(String path, Boolean append, Encoding encoding)
                : base(path, append, encoding)
            {
                Initialize();
            }

            protected InterceptHarmonyStreamWriter(String path, Boolean append, Encoding encoding, Int32 bufferSize)
                : base(path, append, encoding, bufferSize)
            {
                Initialize();
            }

            protected InterceptHarmonyStreamWriter(String path, Encoding encoding, FileStreamOptions options)
                : base(path, encoding, options)
            {
                Initialize();
            }

            private static void OnPropertyIntercept(InterceptStreamWriter sender, IPropertyInterceptEventArgs args)
            {
                PropertyIntercept?.Invoke(sender, args);
            }

            private static void OnPropertyIntercepting(InterceptStreamWriter sender, IPropertyInterceptEventArgs args)
            {
                PropertyIntercepting?.Invoke(sender, args);
            }

            private static void OnPropertyIntercepted(InterceptStreamWriter sender, IPropertyInterceptEventArgs args)
            {
                PropertyIntercepted?.Invoke(sender, args);
            }

            private static void OnPropertyGetIntercept(InterceptStreamWriter sender, IPropertyInterceptEventArgs args)
            {
                PropertyGetIntercept?.Invoke(sender, args);
            }

            private static void OnPropertySetIntercept(InterceptStreamWriter sender, IPropertyInterceptEventArgs args)
            {
                PropertySetIntercept?.Invoke(sender, args);
            }

            private static void OnPropertyGetIntercepting(InterceptStreamWriter sender, IPropertyInterceptEventArgs args)
            {
                PropertyGetIntercepting?.Invoke(sender, args);
            }

            private static void OnPropertySetIntercepting(InterceptStreamWriter sender, IPropertyInterceptEventArgs args)
            {
                PropertySetIntercepting?.Invoke(sender, args);
            }

            private static void OnPropertyGetIntercepted(InterceptStreamWriter sender, IPropertyInterceptEventArgs args)
            {
                PropertyGetIntercepted?.Invoke(sender, args);
            }

            private static void OnPropertySetIntercepted(InterceptStreamWriter sender, IPropertyInterceptEventArgs args)
            {
                PropertySetIntercepted?.Invoke(sender, args);
            }

            private static void OnMethodIntercept(InterceptStreamWriter sender, IMethodInterceptEventArgs args)
            {
                MethodIntercept?.Invoke(sender, args);
            }

            private static void OnMethodIntercepting(InterceptStreamWriter sender, IMethodInterceptEventArgs args)
            {
                MethodIntercepting?.Invoke(sender, args);
            }

            private static void OnMethodIntercepted(InterceptStreamWriter sender, IMethodInterceptEventArgs args)
            {
                MethodIntercepted?.Invoke(sender, args);
            }

            // ReSharper disable once CognitiveComplexity
            private void Initialize()
            {
                StreamWriter.Handler handler = Intercept<InterceptHarmonyStreamWriter, StreamWriter.Handler>.Add(this);

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
                Intercept<InterceptHarmonyStreamWriter>.Remove(this);
            }
        }
    }
}