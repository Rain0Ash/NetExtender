// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using NetExtender.Types.Numerics;

namespace NetExtender.Utils.GUI.WinForms.Controls
{
    public enum ControlSizeType
    {
        Both,
        Width,
        Height
    }
    
    public static class ControlUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this Control control, Point point)
        {
            control.Location = point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this Control control, Form relative, Int32 distance = GUIUtils.Distance)
        {
            SetPosition(control, relative, distance, distance);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this Control control, Form relative, Int32 distanceX, Int32 distanceY)
        {
            SetPosition(control, relative, PointOffset.UpLeft, distanceX, distanceY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this Control control, Form relative, PointOffset offset, Int32 distance = GUIUtils.Distance)
        {
            SetPosition(control, relative, offset, distance, distance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this Control control, Form relative, PointOffset offset, Int32 distanceX, Int32 distanceY)
        {
            control.Location = offset switch
            {
                PointOffset.None => control.Location,
                PointOffset.Up => new Point((relative.ClientSize.Width - control.Size.Width) / 2 + distanceX, distanceY),
                PointOffset.Down => new Point((relative.ClientSize.Width - control.Size.Width) / 2 + distanceX, relative.ClientSize.Height - control.Size.Height - distanceY),
                PointOffset.Left => new Point(distanceX, (relative.ClientSize.Height - control.Size.Height) / 2 - distanceY),
                PointOffset.Right => new Point(relative.ClientSize.Width - control.Size.Width - distanceX, (relative.ClientSize.Height - control.Size.Height) / 2 - distanceY),
                PointOffset.UpLeft => new Point(distanceX, distanceY),
                PointOffset.DownLeft => new Point(distanceX, relative.ClientSize.Height - control.Size.Height - distanceY),
                PointOffset.UpRight => new Point(relative.ClientSize.Width - control.Size.Width - distanceX, distanceY),
                PointOffset.DownRight => new Point(relative.ClientSize.Width - control.Size.Width - distanceX, relative.ClientSize.Height - control.Size.Height - distanceY),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this Control control, Control relative, Int32 distance = GUIUtils.Distance)
        {
            SetPosition(control, relative, PointOffset.Right, distance);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this Control control, Control relative, HorizontalAlignment alignment, Int32 distance = GUIUtils.Distance)
        {
            SetPosition(control, relative, PointOffset.Right, alignment, distance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this Control control, Control relative, PointOffset offset, Int32 distance = GUIUtils.Distance)
        {
            if (relative is Form form)
            {
                SetPosition(control, form, offset, distance);
                return;
            }
            
            control.Location = offset switch
            {
                PointOffset.None => control.Location,
                PointOffset.Up => new Point(relative.Location.X, relative.Location.Y - control.Size.Height - distance),
                PointOffset.Down => new Point(relative.Location.X, relative.Location.Y + relative.Size.Height + distance),
                PointOffset.Left => new Point(relative.Location.X - control.Size.Width - distance, relative.Location.Y),
                PointOffset.Right => new Point(relative.Location.X + relative.Size.Width + distance, relative.Location.Y),
                PointOffset.UpLeft => new Point(relative.Location.X - control.Size.Width - distance, relative.Location.Y - control.Size.Height - distance),
                PointOffset.DownLeft => new Point(relative.Location.X - control.Size.Width - distance, relative.Location.Y + relative.Size.Height + distance),
                PointOffset.UpRight => new Point(relative.Location.X + relative.Size.Width + distance, relative.Location.Y - control.Size.Height - distance),
                PointOffset.DownRight => new Point(relative.Location.X + relative.Size.Width + distance, relative.Location.Y + relative.Size.Height + distance),
                _ => throw new NotSupportedException()
            };
        }

        public static void SetPosition(this Control control, Control relative, PointOffset offset, HorizontalAlignment alignment, Int32 distance = GUIUtils.Distance)
        {
            if (relative is Form form)
            {
                SetPosition(control, form, offset, distance);
                return;
            }
            
            control.Location = offset switch
            {
                PointOffset.None => control.Location,
                PointOffset.Up => alignment switch
                {
                    HorizontalAlignment.Left => new Point(relative.Location.X, relative.Location.Y - control.Size.Height - distance),
                    HorizontalAlignment.Right => new Point(relative.Location.X + relative.Size.Width - control.Size.Width, relative.Location.Y - control.Size.Height - distance),
                    HorizontalAlignment.Center => new Point(relative.Location.X + (relative.Size.Width - control.Size.Width) / 2, relative.Location.Y - control.Size.Height - distance),
                    _ => throw new NotSupportedException()
                },
                PointOffset.Down => alignment switch
                {
                    HorizontalAlignment.Left => new Point(relative.Location.X, relative.Location.Y + relative.Size.Height + distance),
                    HorizontalAlignment.Right => new Point(relative.Location.X + relative.Size.Width - control.Size.Width, relative.Location.Y + relative.Size.Height + distance),
                    HorizontalAlignment.Center => new Point(relative.Location.X + (relative.Size.Width - control.Size.Width) / 2, relative.Location.Y + relative.Size.Height + distance),
                    _ => throw new NotSupportedException()
                },
                PointOffset.Left => alignment switch
                {
                    HorizontalAlignment.Left => new Point(relative.Location.X - control.Size.Width - distance, relative.Location.Y),
                    HorizontalAlignment.Right => new Point(relative.Location.X - control.Size.Width - distance, relative.Location.Y + relative.Size.Height - control.Size.Height),
                    HorizontalAlignment.Center => new Point(relative.Location.X - control.Size.Width - distance, relative.Location.Y + (relative.Size.Height - control.Size.Height) / 2),
                    _ => throw new NotSupportedException()
                },
                PointOffset.Right => alignment switch
                {
                    HorizontalAlignment.Left => new Point(relative.Location.X + relative.Size.Width + distance, relative.Location.Y),
                    HorizontalAlignment.Right => new Point(relative.Location.X + relative.Size.Width + distance, relative.Location.Y + relative.Size.Height - control.Size.Height),
                    HorizontalAlignment.Center => new Point(relative.Location.X + relative.Size.Width + distance, relative.Location.Y + (relative.Size.Height - control.Size.Height) / 2),
                    _ => throw new NotSupportedException()
                },
                PointOffset.UpLeft => new Point(relative.Location.X - control.Size.Width - distance, relative.Location.Y - control.Size.Height - distance),
                PointOffset.DownLeft => new Point(relative.Location.X - control.Size.Width - distance, relative.Location.Y + relative.Size.Height + distance),
                PointOffset.UpRight => new Point(relative.Location.X + relative.Size.Width + distance, relative.Location.Y - control.Size.Height - distance),
                PointOffset.DownRight => new Point(relative.Location.X + relative.Size.Width + distance, relative.Location.Y + relative.Size.Height + distance),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this Control control, Control x, Control y)
        {
            control.Location = new Point(x.Location.X, y.Location.Y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSize(this Control control, Size size)
        {
            control.Size = size;
        }

        //TODO: от текущей позиции до конца формы согласно Size Type + distance от конца формы, оставшуюся координату можно задать. Если координата не задана - использовать квадрат.
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSize(this Control control, Form relative)
        {
            
        }
        
        //TODO: сделать возможность постоянной привязки позиции, на подобии CancellationRegistrationToken
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSize(this Control control, Control relative)
        {
            if (relative is Form form)
            {
                SetSize(control, form);
                return;
            }
            
            SetSize(control, relative.Size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSize(this Control control, Control relative, ControlSizeType type)
        {
            if (relative is Form form)
            {
                return;
            }
            
            switch (type)
            {
                case ControlSizeType.Both:
                    SetSize(control, relative);
                    break;
                case ControlSizeType.Width:
                    control.Size = new Size(relative.Size.Width, control.Size.Height);
                    break;
                case ControlSizeType.Height:
                    control.Size = new Size(control.Size.Width, relative.Size.Height);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSize(this Control control, Control width, Control height)
        {
            control.Size = new Size(width.Size.Width, height.Size.Height);
        }
    }
}