// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Localization.Property.Localization.Initializers;
using NetExtender.Utilities.Core;

namespace NetExtender.UserInterface.WindowsPresentation.Windows
{
    [ReflectionNaming]
    public abstract class LocalizationWindow : CenterWindow
    {
        public abstract LocalizationInitializerAbstraction Localization { get; }
    }
}