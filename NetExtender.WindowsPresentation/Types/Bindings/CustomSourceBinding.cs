using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace NetExtender.WindowsPresentation.Types.Bindings
{
    public abstract class CustomSourceBinding : MessageBinding
    {
        private static ConcurrentDictionary<Type, Func<RelativeSource?>> Factory { get; } = new ConcurrentDictionary<Type, Func<RelativeSource?>>();

        private static Assembly? _assembly;
        protected static Assembly? Assembly
        {
            get
            {
                if (_assembly is not null)
                {
                    return _assembly;
                }

                Initialize(out _assembly);
                return _assembly;
            }
        }

        protected CustomSourceBinding()
        {
            RelativeSource = CreateRelativeSource();
        }

        protected CustomSourceBinding(String path)
            : base(path)
        {
            RelativeSource = CreateRelativeSource();
        }
        
        private static Boolean Initialize([MaybeNullWhen(false)] out Assembly assembly)
        {
            const String name = $"{nameof(NetExtender)}.{nameof(UserInterface)}.{nameof(WindowsPresentation)}.{nameof(Localization)}";
            assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(assembly => assembly.GetName().Name == name);
            return assembly is not null;
        }

        private RelativeSource? CreateRelativeSource()
        {
            return Factory.TryGetValue(GetType(), out Func<RelativeSource?>? factory) ? factory.Invoke() : null;
        }

        protected static Boolean Register<T>(Func<RelativeSource?> factory) where T : CustomSourceBinding
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            
            return Factory.TryAdd(typeof(T), factory);
        }
    }
}