// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Data;
using NetExtender.WindowsPresentation.Localization.Types.Converters;

namespace NetExtender.WindowsPresentation.Localization.Types.Bindings
{
    public class LocalizationBinding : Binding
    {
        public LocalizationBinding()
        {
            Converter = LocalizationPropertyConverter.Instance;
        }

        public LocalizationBinding(String path)
            : base(path)
        {
            Converter = LocalizationPropertyConverter.Instance;
        }
    }
}