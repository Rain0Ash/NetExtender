// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Threading;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Domains.WindowsPresentation.Builder
{
    public class DependencyInjectionWindowsPresentationBuilder : DependencyInjectionWindowsPresentationBuilder<Window>
    {
    }

    public class DependencyInjectionWindowsPresentationBuilder<T> : WindowsPresentationBuilder<T> where T : Window
    {
        public virtual IServiceProvider Provider
        {
            get
            {
                return ServiceProviderUtilities.Provider;
            }
        }
        
        protected virtual IReadOnlyCollection<Assembly>? Assemblies
        {
            get
            {
                return Provider is IAssemblyServiceProvider provider ? provider.Assemblies : null;
            }
        }
        
        protected override TType New<TType>(ImmutableArray<String> arguments)
        {
            Setup(arguments);
            
            try
            {
                typeof(ServiceProviderUtilities).CallStaticConstructor();
                
                Int32 tries = 0;
                TType? service = null;
                Exception? issue = null;
                
                do
                {
                    try
                    {
                        service = Provider.GetService<TType>();
                    }
                    catch (Exception exception)
                    {
                        issue = exception;
                        Thread.Sleep(Time.Millisecond.Hundred);
                    }

                } while (service is null && tries++ < 10);
                
                return service ?? throw new InitializeException($"Can't get instance of '{typeof(TType)}' for builder '{GetType()}'.", issue);
            }
            catch (InitializeException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new InitializeException($"Can't get instance of '{typeof(TType)}' for builder '{GetType()}'.", exception);
            }
            finally
            {
                Finish();
            }
        }
        
        public override T Build(ImmutableArray<String> arguments)
        {
            Manager?.Invoke(this, this);
            Manager?.Invoke(this, Provider);
            return New(arguments);
        }
    }
}