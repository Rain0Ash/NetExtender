// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.Domains.Builder;
using NetExtender.Utilities.Types;

namespace NetExtender.Domains.WindowsPresentation.Builder
{
    public abstract class DependencyInjectionWindowsPresentationBuilder : ApplicationBuilder<Window>
    {
    }

    public class DependencyInjectionWindowsPresentationBuilder<T> : ApplicationBuilder<T> where T : Window
    {
        public virtual IServiceProvider Provider
        {
            get
            {
                return ServiceProviderUtilities.Provider;
            }
        }
        
        protected override TType New<TType>(ImmutableArray<String> arguments)
        {
            try
            {
                Arguments = arguments;
                return Provider.GetService<TType>() ?? throw new InvalidOperationException($"Can't get instance of {typeof(TType)} for builder");
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException($"Can't get instance of {typeof(TType)} for builder", exception);
            }
            finally
            {
                if (Confidential)
                {
                    Arguments = null;
                }
            }
        }
        
        public override T Build(ImmutableArray<String> arguments)
        {
            Manager?.Invoke(this, Provider);
            return New(arguments);
        }
    }
}