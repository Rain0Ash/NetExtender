using System.Windows.Controls;
using NetExtender.Localization.Property.Localization.Initializers;
using NetExtender.Utilities.Core;

namespace NetExtender.UserInterface.WindowsPresentation.Localization.Types.Controls
{
    [ReflectionNaming]
    public abstract class LocalizationControl : Control
    {
        public abstract LocalizationInitializerAbstraction Localization { get; }
    }
}