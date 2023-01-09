using System;
using System.Windows.Data;
using NetExtender.WindowsPresentation.Localization.Types.Converters;

namespace NetExtender.UserInterface.WindowsPresentation.Windows
{
    public class MessageBinding : Binding
    {
        public MessageBinding()
        {
            Converter = LocalizationPropertyConverter.Instance;
        }

        public MessageBinding(String path)
            : base(path)
        {
            Converter = LocalizationPropertyConverter.Instance;
        }
    }
}