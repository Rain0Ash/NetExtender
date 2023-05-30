// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.Initializer;
using NetExtender.Domains.WindowsPresentation.Applications;
using NetExtender.Domains.WindowsPresentation.Builder;
using NetExtender.Domains.WindowsPresentation.View;
using NetExtender.Initializer;

namespace NetExtender.Domains.WindowsPresentation.Initializer
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
    
    public abstract class WindowsPresentationApplicationInitializer<T> : ApplicationInitializer<WindowsPresentationApplication, WindowsPresentationView<T>> where T : Window, new()
    {
    }
    
    public abstract class WindowsPresentationApplicationInitializer<T, TBuilder> : WindowsPresentationApplicationInitializer<T, TBuilder, Application> where T : Window where TBuilder : IApplicationBuilder<T>, new()
    {
        public new abstract class Builder : WindowsPresentationApplicationInitializer<T, TBuilder, Application>.Builder
        {
        }
    }

    public abstract class WindowsPresentationApplicationInitializer<T, TBuilder, TApplication> : ApplicationInitializer<WindowsPresentationApplication<TApplication>, WindowsPresentationView<T, TBuilder>> where T : Window where TBuilder : IApplicationBuilder<T>, new() where TApplication : Application, new()
    {
        public abstract class Builder : WindowsPresentationBuilder<T>
        {
        }
    }
}