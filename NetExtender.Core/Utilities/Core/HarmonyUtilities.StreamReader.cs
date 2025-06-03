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
        public static class StreamReader
        {
            public readonly struct Handler
            {
                public EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyIntercept { get; init; }
                public EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyIntercepting { get; init; }
                public EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyIntercepted { get; init; }
                public EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyGetIntercept { get; init; }
                public EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertySetIntercept { get; init; }
                public EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyGetIntercepting { get; init; }
                public EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertySetIntercepting { get; init; }
                public EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyGetIntercepted { get; init; }
                public EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertySetIntercepted { get; init; }
                public EventHandler<InterceptStreamReader, IMethodInterceptEventArgs>? MethodIntercept { get; init; }
                public EventHandler<InterceptStreamReader, IMethodInterceptEventArgs>? MethodIntercepting { get; init; }
                public EventHandler<InterceptStreamReader, IMethodInterceptEventArgs>? MethodIntercepted { get; init; }
            }
        }
        
        [SuppressMessage("ReSharper", "PublicConstructorInAbstractClass")]
        public abstract class InterceptHarmonyStreamReader : InterceptStreamReader, IInterceptIdentifierTarget<InterceptHarmonyStreamReader>
        {
            public new static event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyIntercept;
            public new static event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyIntercepting;
            public new static event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyIntercepted;
            public new static event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyGetIntercept;
            public new static event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertySetIntercept;
            public new static event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyGetIntercepting;
            public new static event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertySetIntercepting;
            public new static event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertyGetIntercepted;
            public new static event EventHandler<InterceptStreamReader, IPropertyInterceptEventArgs>? PropertySetIntercepted;
            public new static event EventHandler<InterceptStreamReader, IMethodInterceptEventArgs>? MethodIntercept;
            public new static event EventHandler<InterceptStreamReader, IMethodInterceptEventArgs>? MethodIntercepting;
            public new static event EventHandler<InterceptStreamReader, IMethodInterceptEventArgs>? MethodIntercepted;
            
            static InterceptHarmonyStreamReader()
            {
                Intercept<InterceptHarmonyStreamReader>.Regex = new Regex("(Intercept|StreamReader)", RegexOptions.Compiled);
            }
            
            public sealed override String Identifier
            {
                get
                {
                    return _identifier ??= Intercept<InterceptHarmonyStreamReader>.GetName(GetType());
                }
                init
                {
                    throw new NotSupportedException();
                }
            }
            
            public InterceptHarmonyStreamReader(Stream stream)
                : base(stream)
            {
                Initialize();
            }

            public InterceptHarmonyStreamReader(Stream stream, Boolean detectEncodingFromByteOrderMarks)
                : base(stream, detectEncodingFromByteOrderMarks)
            {
                Initialize();
            }

            public InterceptHarmonyStreamReader(Stream stream, Encoding encoding)
                : base(stream, encoding)
            {
                Initialize();
            }

            public InterceptHarmonyStreamReader(Stream stream, Encoding encoding, Boolean detectEncodingFromByteOrderMarks)
                : base(stream, encoding, detectEncodingFromByteOrderMarks)
            {
                Initialize();
            }

            public InterceptHarmonyStreamReader(Stream stream, Encoding encoding, Boolean detectEncodingFromByteOrderMarks, Int32 bufferSize)
                : base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize)
            {
                Initialize();
            }

            public InterceptHarmonyStreamReader(Stream stream, Encoding? encoding = null, Boolean detectEncodingFromByteOrderMarks = true, Int32 bufferSize = -1, Boolean leaveOpen = false)
                : base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen)
            {
                Initialize();
            }

            public InterceptHarmonyStreamReader(String path)
                : base(path)
            {
                Initialize();
            }

            public InterceptHarmonyStreamReader(String path, FileStreamOptions options)
                : base(path, options)
            {
                Initialize();
            }

            public InterceptHarmonyStreamReader(String path, Boolean detectEncodingFromByteOrderMarks)
                : base(path, detectEncodingFromByteOrderMarks)
            {
                Initialize();
            }

            public InterceptHarmonyStreamReader(String path, Encoding encoding)
                : base(path, encoding)
            {
                Initialize();
            }

            public InterceptHarmonyStreamReader(String path, Encoding encoding, Boolean detectEncodingFromByteOrderMarks)
                : base(path, encoding, detectEncodingFromByteOrderMarks)
            {
                Initialize();
            }

            public InterceptHarmonyStreamReader(String path, Encoding encoding, Boolean detectEncodingFromByteOrderMarks, Int32 bufferSize)
                : base(path, encoding, detectEncodingFromByteOrderMarks, bufferSize)
            {
                Initialize();
            }

            public InterceptHarmonyStreamReader(String path, Encoding encoding, Boolean detectEncodingFromByteOrderMarks, FileStreamOptions options)
                : base(path, encoding, detectEncodingFromByteOrderMarks, options)
            {
                Initialize();
            }

            private static void OnPropertyIntercept(InterceptStreamReader sender, IPropertyInterceptEventArgs args)
            {
                PropertyIntercept?.Invoke(sender, args);
            }

            private static void OnPropertyIntercepting(InterceptStreamReader sender, IPropertyInterceptEventArgs args)
            {
                PropertyIntercepting?.Invoke(sender, args);
            }

            private static void OnPropertyIntercepted(InterceptStreamReader sender, IPropertyInterceptEventArgs args)
            {
                PropertyIntercepted?.Invoke(sender, args);
            }

            private static void OnPropertyGetIntercept(InterceptStreamReader sender, IPropertyInterceptEventArgs args)
            {
                PropertyGetIntercept?.Invoke(sender, args);
            }

            private static void OnPropertySetIntercept(InterceptStreamReader sender, IPropertyInterceptEventArgs args)
            {
                PropertySetIntercept?.Invoke(sender, args);
            }

            private static void OnPropertyGetIntercepting(InterceptStreamReader sender, IPropertyInterceptEventArgs args)
            {
                PropertyGetIntercepting?.Invoke(sender, args);
            }

            private static void OnPropertySetIntercepting(InterceptStreamReader sender, IPropertyInterceptEventArgs args)
            {
                PropertySetIntercepting?.Invoke(sender, args);
            }

            private static void OnPropertyGetIntercepted(InterceptStreamReader sender, IPropertyInterceptEventArgs args)
            {
                PropertyGetIntercepted?.Invoke(sender, args);
            }

            private static void OnPropertySetIntercepted(InterceptStreamReader sender, IPropertyInterceptEventArgs args)
            {
                PropertySetIntercepted?.Invoke(sender, args);
            }

            private static void OnMethodIntercept(InterceptStreamReader sender, IMethodInterceptEventArgs args)
            {
                MethodIntercept?.Invoke(sender, args);
            }

            private static void OnMethodIntercepting(InterceptStreamReader sender, IMethodInterceptEventArgs args)
            {
                MethodIntercepting?.Invoke(sender, args);
            }

            private static void OnMethodIntercepted(InterceptStreamReader sender, IMethodInterceptEventArgs args)
            {
                MethodIntercepted?.Invoke(sender, args);
            }

            // ReSharper disable once CognitiveComplexity
            private void Initialize()
            {
                StreamReader.Handler handler = Intercept<InterceptHarmonyStreamReader, StreamReader.Handler>.Add(this);

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
                Intercept<InterceptHarmonyStreamReader>.Remove(this);
            }
        }
    }
}