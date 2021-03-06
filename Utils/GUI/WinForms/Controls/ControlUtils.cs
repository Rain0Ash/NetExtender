// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using JetBrains.Annotations;
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
        public static void SetPosition(this Control control, Int32 x, Int32 y)
        {
            SetPosition(control, new Point(x, y));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this Control control, Point point)
        {
            control.Location = point;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this Control control, Control x, Control y)
        {
            SetPosition(control, new Point(x.Location.X, y.Location.Y));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPositionInner(this Control control, Control relative, Int32 distance = GUIUtils.Distance)
        {
            SetPositionInner(control, relative, distance, distance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPositionInner(this Control control, Control relative, Int32 distanceX, Int32 distanceY)
        {
            SetPositionInner(control, relative, PointOffset.UpLeft, distanceX, distanceY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPositionInner(this Control control, Control relative, PointOffset offset, Int32 distance = GUIUtils.Distance)
        {
            SetPositionInner(control, relative, offset, distance, distance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPositionInner(this Control control, Control relative, PointOffset offset, Int32 distanceX, Int32 distanceY)
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
        public static void SetPositionOuter(this Control control, Control relative, Int32 distance = GUIUtils.Distance)
        {
            SetPositionOuter(control, relative, PointOffset.Right, distance);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPositionOuter(this Control control, Control relative, HorizontalAlignment alignment, Int32 distance = GUIUtils.Distance)
        {
            SetPositionOuter(control, relative, PointOffset.Right, alignment, distance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPositionOuter(this Control control, Control relative, PointOffset offset, Int32 distance = GUIUtils.Distance)
        {
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

        public static void SetPositionOuter(this Control control, Control relative, PointOffset offset, HorizontalAlignment alignment, Int32 distance = GUIUtils.Distance)
        {
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
                SetPositionInner(control, form, offset, distance);
                return;
            }
            
            SetPositionOuter(control, relative, offset, distance);
        }

        public static void SetPosition(this Control control, Control relative, PointOffset offset, HorizontalAlignment alignment, Int32 distance = GUIUtils.Distance)
        {
            if (relative is Form form)
            {
                SetPositionInner(control, form, offset, distance);
                return;
            }
            
            SetPositionOuter(control, relative, offset, alignment, distance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSize(this Control control, Int32 size, ControlSizeType type)
        {
            switch (type)
            {
                case ControlSizeType.Both:
                    SetSize(control, size, size);
                    break;
                case ControlSizeType.Width:
                    SetSize(control, size, control.Size.Height);
                    break;
                case ControlSizeType.Height:
                    SetSize(control, control.Size.Width, size);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSize(this Control control, Int32 width, Int32 height)
        {
            SetSize(control, new Size(width, height));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSize(this Control control, Size size)
        {
            control.Size = size;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSize([NotNull] this Control control, [NotNull] Control width, [NotNull] Control height)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (width is null)
            {
                throw new ArgumentNullException(nameof(width));
            }

            if (height is null)
            {
                throw new ArgumentNullException(nameof(height));
            }
            
            SetSizeOuter(control, width, height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSize([NotNull] this Control control, [NotNull] Control relative, ControlSizeType type = ControlSizeType.Both)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }
            
            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            if (relative is Form form)
            {
                SetSizeInner(control, form, type);
                return;
            }
            
            SetSizeOuter(control, relative, type);
        }

        //TODO: от текущей позиции до конца формы согласно Size Type + distance от конца формы, оставшуюся координату можно задать. Если координата не задана - использовать квадрат.

        //TODO: сделать возможность постоянной привязки позиции, на подобии CancellationRegistrationToken

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSizeInner([NotNull] this Control control, [NotNull] Control relative, ControlSizeType type = ControlSizeType.Both)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }
            
            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }
            
            control.Size = type switch
            {
                ControlSizeType.Both => relative.ClientSize,
                ControlSizeType.Width => new Size(relative.ClientSize.Width, control.ClientSize.Height),
                ControlSizeType.Height => new Size(control.ClientSize.Width, relative.ClientSize.Height),
                _ => throw new NotSupportedException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSizeInner([NotNull] this Control control, [NotNull] Control width, [NotNull] Control height)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (width is null)
            {
                throw new ArgumentNullException(nameof(width));
            }

            if (height is null)
            {
                throw new ArgumentNullException(nameof(height));
            }
            
            control.Size = new Size(width.ClientSize.Width, height.ClientSize.Height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSizeOuter([NotNull] this Control control, [NotNull] Control relative, ControlSizeType type = ControlSizeType.Both)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }
            
            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }
            
            control.Size = type switch
            {
                ControlSizeType.Both => relative.Size,
                ControlSizeType.Width => new Size(relative.Size.Width, control.Size.Height),
                ControlSizeType.Height => new Size(control.Size.Width, relative.Size.Height),
                _ => throw new NotSupportedException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSizeOuter([NotNull] this Control control, [NotNull] Control width, [NotNull] Control height)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (width is null)
            {
                throw new ArgumentNullException(nameof(width));
            }

            if (height is null)
            {
                throw new ArgumentNullException(nameof(height));
            }
            
            control.Size = new Size(width.Size.Width, height.Size.Height);
        }

        private static IDisposable LinkTo([NotNull] this Control control, [NotNull] Control relative, [NotNull] String name, Action<EventPattern<EventHandler>> action)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }
            
            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Observable.FromEventPattern<EventHandler>(relative, name).Subscribe(action);
        }
        
        public static IDisposable LinkSizeTo([NotNull] this Control control, [NotNull] Control relative, ControlSizeType type = ControlSizeType.Both)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }
            
            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            return LinkTo(control, relative, nameof(relative.SizeChanged), next => control.SetSize((Control) next.Sender, type));
        }
        
        public static IDisposable LinkSizeInnerTo([NotNull] this Control control, [NotNull] Control relative, ControlSizeType type = ControlSizeType.Both)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }
            
            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            return LinkTo(control, relative, nameof(relative.SizeChanged), next => control.SetSizeInner((Control) next.Sender, type));
        }
        
        public static IDisposable LinkSizeOuterTo([NotNull] this Control control, [NotNull] Control relative, ControlSizeType type = ControlSizeType.Both)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }
            
            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            return LinkTo(control, relative, nameof(relative.SizeChanged), next => control.SetSizeOuter((Control) next.Sender, type));
        }
    }
}