// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Documents;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class Text : Run
    {
        static Text()
        {
            TextProperty.OverrideMetadataOneWay<Text>();
        }
        
        public Text()
        {
        }

        public Text(String text)
            : base(text)
        {
        }

        public Text(String text, TextPointer insertionPosition)
            : base(text, insertionPosition)
        {
        }
    }
}