// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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