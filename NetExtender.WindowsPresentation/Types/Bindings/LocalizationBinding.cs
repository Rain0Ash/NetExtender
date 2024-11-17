using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using NetExtender.Utilities.Core;

namespace NetExtender.WindowsPresentation.Types.Bindings
{
    public class LocalizationBinding : CustomSourceBinding
    {
        static LocalizationBinding()
        {
            Initialize(out Type? type);

            if (type is null)
            {
                return;
            }

            RelativeSource Factory()
            {
                return new RelativeSource(RelativeSourceMode.FindAncestor, type ?? typeof(Window), 1);
            }
            
            Register<LocalizationBinding>(Factory);
        }

        private static Boolean Initialize([MaybeNullWhen(false)] out Type type)
        {
            type = Assembly?.GetSafeTypes().FirstOrDefault(type => type.Name == nameof(Localization) + nameof(Window) && type.HasAttribute<ReflectionNamingAttribute>());
            return type is not null;
        }

        public LocalizationBinding()
        {
        }

        public LocalizationBinding(String path)
            : base(path)
        {
        }
    }

    public class ControlLocalizationBinding : CustomSourceBinding
    {
        static ControlLocalizationBinding()
        {
            Initialize(out Type? type);

            if (type is null)
            {
                return;
            }

            RelativeSource Factory()
            {
                return new RelativeSource(RelativeSourceMode.FindAncestor, type ?? typeof(Control), 1);
            }
            
            Register<LocalizationBinding>(Factory);
        }
        
        private static Boolean Initialize([MaybeNullWhen(false)] out Type type)
        {
            type = Assembly?.GetSafeTypes().FirstOrDefault(type => type.Name == nameof(Localization) + nameof(Control) && type.HasAttribute<ReflectionNamingAttribute>());
            return type is not null;
        }
        
        public ControlLocalizationBinding()
        {
        }

        public ControlLocalizationBinding(String path)
            : base(path)
        {
        }
    }

    public class UserControlLocalizationBinding : CustomSourceBinding
    {
        static UserControlLocalizationBinding()
        {
            Initialize(out Type? type);

            if (type is null)
            {
                return;
            }

            RelativeSource Factory()
            {
                return new RelativeSource(RelativeSourceMode.FindAncestor, type ?? typeof(UserControl), 1);
            }
            
            Register<LocalizationBinding>(Factory);
        }
        
        private static Boolean Initialize([MaybeNullWhen(false)] out Type type)
        {
            type = Assembly?.GetSafeTypes().FirstOrDefault(type => type.Name == nameof(Localization) + nameof(UserControl) && type.HasAttribute<ReflectionNamingAttribute>());
            return type is not null;
        }
        
        public UserControlLocalizationBinding()
        {
        }

        public UserControlLocalizationBinding(String path)
            : base(path)
        {
        }
    }

    public class SelfLocalizationBinding : LocalizationBinding
    {
        static SelfLocalizationBinding()
        {
            static RelativeSource Factory()
            {
                return new RelativeSource(RelativeSourceMode.Self);
            }
            
            Register<SelfLocalizationBinding>(Factory);
        }
        
        public SelfLocalizationBinding()
        {
        }

        public SelfLocalizationBinding(String path)
            : base(path)
        {
        }
    }
}