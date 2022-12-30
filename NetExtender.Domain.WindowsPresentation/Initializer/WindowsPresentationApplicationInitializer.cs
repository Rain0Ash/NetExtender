// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using NetExtender.Domains.Applications;
using NetExtender.Domains.Initializer;
using NetExtender.Domains.View;
using NetExtender.Initializer;

namespace NetExtender.Domain.WindowsPresentation.Initializer
{
    public abstract class WindowsPresentationApplicationInitializer : ApplicationInitializer
    {
        protected override void UnhandledException(Object? sender, Exception? exception, ref InitializerUnhandledExceptionState action)
        {
            base.UnhandledException<MessageBoxExceptionHandler>(sender, exception, ref action);
        }

        protected class MessageBoxExceptionHandler : ExceptionHandler
        {
            public virtual String? Title
            {
                get
                {
                    return null;
                }
            }

            public virtual MessageBoxButton Button
            {
                get
                {
                    return MessageBoxButton.OK;
                }
            }

            public virtual MessageBoxImage Icon
            {
                get
                {
                    return MessageBoxImage.Error;
                }
            }

            public virtual MessageBoxResult Result
            {
                get
                {
                    return MessageBoxResult.OK;
                }
            }

            public virtual MessageBoxOptions Options
            {
                get
                {
                    return MessageBoxOptions.None;
                }
            }

            public override void Handle(Object? sender, Exception? exception, ref InitializerUnhandledExceptionState action)
            {
                String? message = Message(sender, exception, action);
                MessageBox.Show(message, Title, Button, Icon, Result, Options);
            }

            [return: NotNullIfNotNull("exception")]
            protected virtual String? Message(Object? sender, Exception? exception, InitializerUnhandledExceptionState action)
            {
                return exception?.ToString();
            }
        }
    }
    
    public abstract class WindowsPresentationApplicationInitializer<T> : WindowsPresentationApplicationInitializer<System.Windows.Application, T> where T : Window, new()
    {
    }

    public abstract class WindowsPresentationApplicationInitializer<TApplication, TWindow> : ApplicationInitializer<WindowsPresentationApplication<TApplication>, WindowsPresentationView<TWindow>> where TApplication : System.Windows.Application, new() where TWindow : Window, new()
    {
    }
}