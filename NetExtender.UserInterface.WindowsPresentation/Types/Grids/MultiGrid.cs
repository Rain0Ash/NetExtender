// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class MultiGrid : Grid
    {
        public static readonly DependencyProperty ColumnsDependencyProperty = DependencyProperty.Register(nameof(Columns), typeof(Int32), typeof(MultiGrid), new PropertyMetadata(1));
        public static readonly DependencyProperty RowsDependencyProperty = DependencyProperty.Register(nameof(Rows), typeof(Int32), typeof(MultiGrid), new PropertyMetadata(1));
        
        private DependencyPropertyDescriptor ColumnsPropertyDescriptor { get; } = DependencyPropertyDescriptor.FromProperty(ColumnsDependencyProperty, typeof(MultiGrid));
        private DependencyPropertyDescriptor RowsPropertyDescriptor { get; } = DependencyPropertyDescriptor.FromProperty(RowsDependencyProperty, typeof(MultiGrid));
        
        public Int32 Columns
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Int32) GetValue(ColumnsDependencyProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(ColumnsDependencyProperty, value);
            }
        }
        
        public Int32 Rows
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Int32) GetValue(RowsDependencyProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(RowsDependencyProperty, value);
            }
        }

        public MultiGrid()
        {
            ColumnsPropertyDescriptor?.AddValueChanged(this, ColumnsHandler);
            RowsPropertyDescriptor?.AddValueChanged(this, RowsHandler);
        }

        private void ColumnsHandler(Object? sender, EventArgs args)
        {
            ColumnDefinitions.Clear();

            for (Int32 i = 0; i < Columns; i++)
            {
                ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private void RowsHandler(Object? sender, EventArgs args)
        {
            RowDefinitions.Clear();

            for (Int32 i = 0; i < Rows; i++)
            {
                RowDefinitions.Add(new RowDefinition());
            }
        }
    }
}