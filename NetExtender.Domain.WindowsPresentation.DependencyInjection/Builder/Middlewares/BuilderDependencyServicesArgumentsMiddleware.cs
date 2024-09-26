using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.DependencyInjection.Attributes;
using NetExtender.Domains.Builder;
using NetExtender.Types.Middlewares;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.IO;

namespace NetExtender.Domains.WindowsPresentation.Builder.Middlewares
{
    [ApplicationBuilderMiddleware]
    public class BuilderDependencyServicesArgumentsMiddleware : Middleware<IServiceProvider>
    {
        public override void Invoke(Object? sender, IServiceProvider provider)
        {
            _ = provider.GetService<Arguments>();
        }
    }
}

namespace NetExtender.Domains.WindowsPresentation
{
    [Singleton]
    public sealed class Arguments : IReadOnlyList<String>
    {
        public static implicit operator ImmutableArray<String>(Arguments? arguments)
        {
            return arguments?.Internal ?? ImmutableArray<String>.Empty;
        }

        private ImmutableArray<String> Internal { get; }
        
        public Int32 Count
        {
            get
            {
                return Internal.Length;
            }
        }
        
        private FileInfo? _executable;
        public FileInfo? Executable
        {
            get
            {
                return _executable ??= Internal.Length > 0 && PathUtilities.IsExist(Internal[0], PathType.LocalFile) ? new FileInfo(Internal[0]) : null;
            }
        }
        
        public Arguments()
            : this(ApplicationUtilities.Arguments)
        {
        }

        public Arguments(ImmutableArray<String> arguments)
        {
            Internal = arguments;
        }
        
        public ImmutableArray<String>.Enumerator GetEnumerator()
        {
            return Internal.GetEnumerator();
        }
        
        IEnumerator<String> IEnumerable<String>.GetEnumerator()
        {
            return ((IEnumerable<String>) Internal).GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Internal).GetEnumerator();
        }
        
        public String this[Int32 index]
        {
            get
            {
                return Internal[index];
            }
        }
        
        public String this[Index index]
        {
            get
            {
                return Internal[index];
            }
        }
    }
}