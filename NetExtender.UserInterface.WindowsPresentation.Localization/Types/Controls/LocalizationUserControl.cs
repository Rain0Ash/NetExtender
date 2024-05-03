using System.Windows.Controls;
using NetExtender.Localization.Property.Localization.Initializers;
using NetExtender.Utilities.Core;

namespace NetExtender.UserInterface.WindowsPresentation.Localization.Types.Controls
{
    [ReflectionNaming]
    public abstract class LocalizationUserControl : UserControl
    {
        public abstract LocalizationInitializerAbstraction Localization { get; }
    }
}