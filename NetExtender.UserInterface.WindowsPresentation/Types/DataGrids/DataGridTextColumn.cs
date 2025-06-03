// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class DataGridTextColumn : System.Windows.Controls.DataGridTextColumn
    {
        public new static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(String), typeof(DataGridTextColumn));

        [Bindable(true)]
        public new String Header
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (String) GetValue(HeaderProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(HeaderProperty, value);
            }
        }

        public Object? View
        {
            get
            {
                return base.Header;
            }
            set
            {
                base.Header = value;
            }
        }

        public DataGridTextColumn()
        {
            HeaderStyle = new Style(typeof(DataGridColumnHeader))
            {
                Setters =
                {
                    new Setter(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center),
                    new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Center),
                }
            };

            /*HeaderTemplate = WindowsPresentationTemplateUtilities.CreateDataTemplate(() =>
            {
                CenterTextBlock block = new CenterTextBlock
                {
                };

                block.SetBinding(TextBlock.TextProperty, new Binding(nameof(HeaderProperty)) { Source = this, Mode = BindingMode.OneWay });
                return block;
            });*/

            /*FrameworkElementFactory block = new FrameworkElementFactory(typeof(CenterTextBlock));
            block.SetBinding(TextBlock.TextProperty, new TwoWayBinding(nameof(Header))
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent)
            });

            HeaderTemplate = new DataTemplate(typeof(DataGridColumnHeader))
            {
                VisualTree = block
            };*/
        }
    }
}