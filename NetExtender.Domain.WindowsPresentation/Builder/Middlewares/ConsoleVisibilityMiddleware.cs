using System;
using NetExtender.Domains.Builder;
using NetExtender.Domains.WindowsPresentation.Builder.Interfaces;
using NetExtender.Types.Middlewares;
using NetExtender.Utilities.UserInterface;

namespace NetExtender.Domains.WindowsPresentation.Builder.Middlewares
{
    [ApplicationBuilderMiddleware]
    internal sealed class ConsoleVisibilityMiddleware : Middleware<IWindowsPresentationBuilder>
    {
        public override void Invoke(Object? sender, IWindowsPresentationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            ConsoleWindowUtilities.IsConsoleVisible = builder.IsConsoleVisible;
        }
    }
}