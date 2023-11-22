// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.Initializer;
using NetExtender.Domains.WinForms.Applications;
using NetExtender.Domains.WinForms.Builder;
using NetExtender.Domains.WinForms.View;
using NetExtender.Initializer;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.IO;

namespace NetExtender.Domains.WinForms.Initializer
{
    public abstract class WinFormsApplicationInitializer : ApplicationInitializer
    {
        protected enum ApplicationExceptionHandleType : Byte
        {
            Custom,
            Console,
            Message,
            MessageConsole
        }

        protected virtual ApplicationExceptionHandleType ExceptionHandleType
        {
            get
            {
                return ApplicationExceptionHandleType.Custom;
            }
        }

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

            public virtual MessageBoxButtons Buttons
            {
                get
                {
                    return MessageBoxButtons.OK;
                }
            }

            public virtual MessageBoxIcon Icon
            {
                get
                {
                    return MessageBoxIcon.Error;
                }
            }

            public virtual MessageBoxDefaultButton DefaultButton
            {
                get
                {
                    return MessageBoxDefaultButton.Button1;
                }
            }

            public virtual MessageBoxOptions Options
            {
                get
                {
                    return default;
                }
            }

            protected virtual void Handle(WinFormsApplicationInitializer initializer, Object? sender, Exception? exception, ref InitializerUnhandledExceptionState action)
            {
                switch (initializer.ExceptionHandleType)
                {
                    case ApplicationExceptionHandleType.Custom:
                        Handle(sender, exception, ref action);
                        return;
                    case ApplicationExceptionHandleType.Console:
                        Console(sender, exception, ref action);
                        return;
                    case ApplicationExceptionHandleType.Message:
                        Message(sender, exception, ref action);
                        return;
                    case ApplicationExceptionHandleType.MessageConsole:
                        Console(sender, exception, ref action);
                        goto case ApplicationExceptionHandleType.Message;
                    default:
                        throw new EnumUndefinedOrNotSupportedException<ApplicationExceptionHandleType>(initializer.ExceptionHandleType, nameof(ExceptionHandleType), null);
                }
            }

            protected virtual void Console(Object? sender, Exception? exception, ref InitializerUnhandledExceptionState action)
            {
                Text(sender, exception, action).ToConsole();
            }

            protected virtual void Message(Object? sender, Exception? exception, ref InitializerUnhandledExceptionState action)
            {
                MessageBox.Show(Text(sender, exception, action), Title, Buttons, Icon, DefaultButton, Options);
            }

            public override void Handle(Object? sender, Exception? exception, ref InitializerUnhandledExceptionState action)
            {
                Message(sender, exception, ref action);
            }

            [return: NotNullIfNotNull("exception")]
            protected virtual String? Text(Object? sender, Exception? exception, InitializerUnhandledExceptionState action)
            {
                return exception?.ToString();
            }
        }
    }
    
    public abstract class WinFormsApplicationInitializer<T> : ApplicationInitializer<WinFormsApplication, WinFormsView<T>> where T : Form, new()
    {
    }
    
    public abstract class WinFormsApplicationInitializer<T, TBuilder> : ApplicationInitializer<WinFormsApplication, WinFormsView<T, TBuilder>> where T : Form where TBuilder : IApplicationBuilder<T>, new()
    {
        public abstract class Builder : WinFormsBuilder<T>
        {
        }
    }
}