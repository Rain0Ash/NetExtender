// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Localization.Property.Localization.Initializers;
using NetExtender.UserInterface.WindowsPresentation.Controls;
using NetExtender.Utilities.Core;
using NetExtender.WindowsPresentation.Types.Bindings;

namespace NetExtender.UserInterface.WindowsPresentation.Localization.Types.Controls
{
    [ReflectionNaming(typeof(LocalizationBinding))]
    public abstract class LocalizationUserControl : DependencyUserControl
    {
        public abstract LocalizationInitializerAbstraction Localization { get; }
    }
}