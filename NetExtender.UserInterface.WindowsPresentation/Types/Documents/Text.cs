using System;
using System.Windows;
using System.Windows.Documents;

namespace NetExtender.UserInterface.WindowsPresentation.Types.Documents
{
    public class Text : TextRun
    {
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
    
    public abstract class TextRun : Run
    {
        public new static readonly DependencyProperty TextProperty;
        
        static TextRun()
        {
            FrameworkPropertyMetadata metadata = Run.TextProperty.GetMetadata(typeof(Run)) as FrameworkPropertyMetadata ?? throw new InvalidOperationException();
            metadata = new FrameworkPropertyMetadata(metadata.DefaultValue, FrameworkPropertyMetadataOptions.None, metadata.PropertyChangedCallback, metadata.CoerceValueCallback);
            TextProperty = DependencyProperty.Register(nameof(Text), typeof(String), typeof(TextRun), metadata);
        }
        
        public new String Text
        {
            get
            {
                return (String) GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        protected TextRun()
        {
        }

        protected TextRun(String text)
            : base(text)
        {
        }

        protected TextRun(String text, TextPointer insertionPosition)
            : base(text, insertionPosition)
        {
        }
    }
}