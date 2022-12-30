// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using NetExtender.Domains.Applications;
using NetExtender.Domains.Initializer;
using NetExtender.Domains.View;
using NetExtender.Initializer;

namespace NetExtender.Domain.WinForms.Initializer
{
    public abstract class WinFormsApplicationInitializer : ApplicationInitializer
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

            public override void Handle(Object? sender, Exception? exception, ref InitializerUnhandledExceptionState action)
            {
                String? message = Message(sender, exception, action);
                MessageBox.Show(message, Title, Buttons, Icon, DefaultButton, Options);
            }

            [return: NotNullIfNotNull("exception")]
            protected virtual String? Message(Object? sender, Exception? exception, InitializerUnhandledExceptionState action)
            {
                return exception?.ToString();
            }
        }
    }
    
    public abstract class WinFormsApplicationInitializer<T> : ApplicationInitializer<WinFormsApplication, WinFormsView<T>> where T : Form, new()
    {
    }
}