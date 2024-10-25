using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using NetExtender.Utilities.UserInterface;
using NetExtender.WindowsPresentation.Types;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public partial class ExcelGrid
    {
        protected override void OnPreviewKeyDown(KeyEventArgs args)
        {
            base.OnPreviewKeyDown(args);
            
            if (args.Key != Key.ImeProcessed)
            {
                return;
            }
            
            if (CurrentEditControl is TextBox { IsFocused: false })
            {
                ShowTextBoxEditControl();
            }
        }
        
        // ReSharper disable once CognitiveComplexity
        protected override void OnKeyDown(KeyEventArgs args)
        {
            base.OnKeyDown(args);
            
            KeyInfo info = args;
            Int32 column = info.Shift ? SelectionCell.Column : CurrentCell.Column;
            Int32 row = info.Shift ? SelectionCell.Row : CurrentCell.Row;
            
            switch (args.Key)
            {
                case Key.Enter:
                {
                    if (IsMoveAfterEnter)
                    {
                        if (InputDirection == InputDirection.Vertical)
                        {
                            ChangeCurrentCell(info.Shift ? -1 : 1, 0);
                        }
                        else
                        {
                            ChangeCurrentCell(0, info.Shift ? -1 : 1);
                        }
                    }
                    
                    args.Handled = true;
                    return;
                }
                
                case Key.Up:
                {
                    if (row > 0)
                    {
                        row--;
                    }
                    
                    if (EndKeyIsPressed)
                    {
                        row = FindNextRow(row, column, -1);
                    }
                    
                    if (info.Control)
                    {
                        row = 0;
                    }
                    
                    break;
                }
                case Key.Down:
                {
                    if (row < Rows - 1 || (CanInsertRows && EasyInsertByKeyboard))
                    {
                        row++;
                    }
                    
                    if (EndKeyIsPressed)
                    {
                        row = FindNextRow(row, column, 1);
                    }
                    
                    if (info.Control)
                    {
                        row = Rows - 1;
                    }
                    
                    break;
                }
                case Key.Left:
                {
                    if (column > 0)
                    {
                        column--;
                    }
                    
                    if (EndKeyIsPressed)
                    {
                        column = FindNextColumn(row, column, -1);
                    }
                    
                    if (info.Control)
                    {
                        column = 0;
                    }
                    
                    break;
                }
                case Key.Right:
                    if (column < Columns - 1 || (CanInsertColumns && EasyInsertByKeyboard))
                    {
                        column++;
                    }

                    if (EndKeyIsPressed)
                    {
                        column = FindNextColumn(row, column, 1);
                    }

                    if (info.Control)
                    {
                        column = Columns - 1;
                    }

                    break;
                case Key.End:

                    // Flag that the next key should be handled differently
                    EndKeyIsPressed = true;
                    args.Handled = true;
                    return;
                case Key.Home:
                    column = 0;
                    row = 0;
                    break;
                case Key.Back:
                case Key.Delete:
                    if (CanClear)
                    {
                        Clear();
                        args.Handled = true;
                    }

                    return;
                case Key.F2:
                    if (ShowTextBoxEditControl())
                    {
                        args.Handled = true;
                    }

                    return;
                case Key.F4:
                    if (!OpenComboBoxControl())
                    {
                        return;
                    }
                    
                    args.Handled = true;
                    return;
                case Key.Space:
                    if (!ToggleCheckInCell())
                    {
                        return;
                    }
                    
                    args.Handled = true;
                    return;
                case Key.A:
                    if (!info.Control)
                    {
                        return;
                    }
                    
                    SelectAll();
                    args.Handled = true;
                    return;
                case Key.C:
                    if (!info.Control || !info.Alt)
                    {
                        return;
                    }
                    
                    Clipboard.SetText(ToCsv(SelectionRange));
                    args.Handled = true;
                    return;
                default:
                    return;
            }

            if (args.Key != Key.End)
            {
                EndKeyIsPressed = false;
            }

            ExcelCell cell = new ExcelCell(row, column);
            if (!HandleAutoInsert(cell))
            {
                SelectionCell = cell;

                if (!info.Shift)
                {
                    CurrentCell = cell;
                }

                ScrollIntoView(cell);
            }

            args.Handled = true;
        }
        
        // ReSharper disable once CognitiveComplexity
        private void TextEditorPreviewKeyDown(Object? sender, KeyEventArgs args)
        {
            if (sender is not TextBox textbox)
            {
                return;
            }
            
            Boolean all = textbox.SelectionLength > 0 && textbox.SelectionLength == textbox.Text.Length;
            switch (args.Key)
            {
                case Key.Left:
                {
                    if (all || textbox.CaretIndex != 0)
                    {
                        return;
                    }
                    
                    RemoveEditControl();
                    OnKeyDown(args);
                    args.Handled = true;
                    return;
                }
                case Key.Right:
                {
                    if (all || textbox.CaretIndex != textbox.Text.Length)
                    {
                        return;
                    }
                    
                    RemoveEditControl();
                    OnKeyDown(args);
                    args.Handled = true;
                    return;
                }
                case Key.Down:
                case Key.Up:
                {
                    RemoveEditControl();
                    OnKeyDown(args);
                    args.Handled = true;
                    return;
                }
                case Key.Enter:
                {
                    RemoveEditControl();
                    OnKeyDown(args);
                    args.Handled = true;
                    return;
                }
                case Key.Escape:
                {
                    if (CurrentEditControl is { } control)
                    {
                        BindingOperations.ClearBinding(control, TextBox.TextProperty);
                    }
                    
                    RemoveEditControl();
                    args.Handled = true;
                    return;
                }
                default:
                {
                    return;
                }
            }
        }
    }
}