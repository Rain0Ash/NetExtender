using System;
using NetExtender.Domains.Builder;
using NetExtender.Domains.WinForms.Builder.Interfaces;
using NetExtender.Types.Middlewares;
using NetExtender.Utilities.UserInterface;

namespace NetExtender.Domains.WinForms.Builder.Middlewares
{
    [ApplicationBuilderMiddleware]
    internal sealed class ConsoleVisibilityMiddleware : Middleware<IWinFormsBuilder>
    {
        public override void Invoke(Object? sender, IWinFormsBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            ConsoleWindowUtilities.IsConsoleVisible = builder.IsConsoleVisible;
        }
    }
}