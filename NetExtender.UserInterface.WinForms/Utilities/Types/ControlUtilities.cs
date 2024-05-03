// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Numerics;

namespace NetExtender.Utilities.UserInterface.Types
{
    public enum ControlSizeType
    {
        Both,
        Width,
        Height
    }

    public static class ControlUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEnabled(Object? @object, Boolean enabled)
        {
            SetEnabled(@object as Control, enabled);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("control")]
        public static T? SetEnabled<T>(this T? control, Boolean enabled) where T : Control
        {
            if (control is not null)
            {
                control.Enabled = enabled;
            }
            
            return control;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Enable(Object? @object)
        {
            Enable(@object as Control);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("control")]
        public static T? Enable<T>(this T? control) where T : Control
        {
            return SetEnabled(control, true);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Disable(Object? @object)
        {
            Disable(@object as Control);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("control")]
        public static T? Disable<T>(this T? control) where T : Control
        {
            return SetEnabled(control, false);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetVisible(Object? @object, Boolean visible)
        {
            SetVisible(@object as Control, visible);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("control")]
        public static T? SetVisible<T>(this T? control, Boolean visible) where T : Control
        {
            if (control is not null)
            {
                control.Visible = visible;
            }
            
            return control;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Visible(Object? @object)
        {
            Visible(@object as Control);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("control")]
        public static T? Visible<T>(this T? control) where T : Control
        {
            return SetVisible(control, true);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Invisible(Object? @object)
        {
            Invisible(@object as Control);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("control")]
        public static T? Invisible<T>(this T? control) where T : Control
        {
            return SetVisible(control, false);
        }
        
        private static IDisposable BindTo(Control relative, String name, Action<EventPattern<Object>> action)
        {
            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Observable.FromEventPattern(relative, name).Subscribe(action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetPosition<T>(this T control, Int32 x, Int32 y) where T : Control
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            return SetPosition(control, new Point(x, y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetPosition<T>(this T control, Point point) where T : Control
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            control.Location = point;
            return control;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetPosition<T>(this T control, Control x, Control y) where T : Control
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            return SetPosition(control, new Point(x.Location.X, y.Location.Y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetPosition<T>(this T control, Control relative, Int32 distance = UserInterfaceUtilities.Distance) where T : Control
        {
            return SetPosition(control, relative, PointOffset.Right, distance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetPosition<T>(this T control, Control relative, HorizontalAlignment alignment, Int32 distance = UserInterfaceUtilities.Distance) where T : Control
        {
            return SetPosition(control, relative, PointOffset.Right, alignment, distance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetPosition<T>(this T control, Control relative, PointOffset offset, Int32 distance = UserInterfaceUtilities.Distance) where T : Control
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
                return SetPositionInner(control, form, offset, distance);
            }

            return SetPositionOuter(control, relative, offset, distance);
        }

        public static T SetPosition<T>(this T control, Control relative, PointOffset offset, HorizontalAlignment alignment, Int32 distance = UserInterfaceUtilities.Distance) where T : Control
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
                return SetPositionInner(control, form, offset, distance);
            }

            return SetPositionOuter(control, relative, offset, alignment, distance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetPositionInner<T>(this T control, Control relative, Int32 distance = UserInterfaceUtilities.Distance) where T : Control
        {
            return SetPositionInner(control, relative, distance, distance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetPositionInner<T>(this T control, Control relative, Int32 distanceX, Int32 distanceY) where T : Control
        {
            return SetPositionInner(control, relative, PointOffset.UpLeft, distanceX, distanceY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetPositionInner<T>(this T control, Control relative, PointOffset offset, Int32 distance = UserInterfaceUtilities.Distance) where T : Control
        {
            return SetPositionInner(control, relative, offset, distance, distance);
        }

        public static T SetPositionInner<T>(this T control, Control relative, PointOffset offset, Int32 distanceX, Int32 distanceY) where T : Control
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

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
                _ => throw new EnumUndefinedOrNotSupportedException<PointOffset>(offset, nameof(offset), null)
            };

            return control;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetPositionOuter<T>(this T control, Control relative, Int32 distance = UserInterfaceUtilities.Distance) where T : Control
        {
            return SetPositionOuter(control, relative, PointOffset.Right, distance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetPositionOuter<T>(this T control, Control relative, HorizontalAlignment alignment, Int32 distance = UserInterfaceUtilities.Distance) where T : Control
        {
            return SetPositionOuter(control, relative, PointOffset.Right, alignment, distance);
        }

        public static T SetPositionOuter<T>(this T control, Control relative, PointOffset offset, Int32 distance = UserInterfaceUtilities.Distance) where T : Control
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
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
                _ => throw new EnumUndefinedOrNotSupportedException<PointOffset>(offset, nameof(offset), null)
            };

            return control;
        }

        public static T SetPositionOuter<T>(this T control, Control relative, PointOffset offset, HorizontalAlignment alignment, Int32 distance = UserInterfaceUtilities.Distance) where T : Control
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            control.Location = offset switch
            {
                PointOffset.None => control.Location,
                PointOffset.Up => alignment switch
                {
                    HorizontalAlignment.Left => new Point(relative.Location.X, relative.Location.Y - control.Size.Height - distance),
                    HorizontalAlignment.Right => new Point(relative.Location.X + relative.Size.Width - control.Size.Width, relative.Location.Y - control.Size.Height - distance),
                    HorizontalAlignment.Center => new Point(relative.Location.X + (relative.Size.Width - control.Size.Width) / 2, relative.Location.Y - control.Size.Height - distance),
                    _ => throw new EnumUndefinedOrNotSupportedException<HorizontalAlignment>(alignment, nameof(alignment), null)
                },
                PointOffset.Down => alignment switch
                {
                    HorizontalAlignment.Left => new Point(relative.Location.X, relative.Location.Y + relative.Size.Height + distance),
                    HorizontalAlignment.Right => new Point(relative.Location.X + relative.Size.Width - control.Size.Width, relative.Location.Y + relative.Size.Height + distance),
                    HorizontalAlignment.Center => new Point(relative.Location.X + (relative.Size.Width - control.Size.Width) / 2, relative.Location.Y + relative.Size.Height + distance),
                    _ => throw new EnumUndefinedOrNotSupportedException<HorizontalAlignment>(alignment, nameof(alignment), null)
                },
                PointOffset.Left => alignment switch
                {
                    HorizontalAlignment.Left => new Point(relative.Location.X - control.Size.Width - distance, relative.Location.Y),
                    HorizontalAlignment.Right => new Point(relative.Location.X - control.Size.Width - distance, relative.Location.Y + relative.Size.Height - control.Size.Height),
                    HorizontalAlignment.Center => new Point(relative.Location.X - control.Size.Width - distance, relative.Location.Y + (relative.Size.Height - control.Size.Height) / 2),
                    _ => throw new EnumUndefinedOrNotSupportedException<HorizontalAlignment>(alignment, nameof(alignment), null)
                },
                PointOffset.Right => alignment switch
                {
                    HorizontalAlignment.Left => new Point(relative.Location.X + relative.Size.Width + distance, relative.Location.Y),
                    HorizontalAlignment.Right => new Point(relative.Location.X + relative.Size.Width + distance, relative.Location.Y + relative.Size.Height - control.Size.Height),
                    HorizontalAlignment.Center => new Point(relative.Location.X + relative.Size.Width + distance, relative.Location.Y + (relative.Size.Height - control.Size.Height) / 2),
                    _ => throw new EnumUndefinedOrNotSupportedException<HorizontalAlignment>(alignment, nameof(alignment), null)
                },
                PointOffset.UpLeft => new Point(relative.Location.X - control.Size.Width - distance, relative.Location.Y - control.Size.Height - distance),
                PointOffset.DownLeft => new Point(relative.Location.X - control.Size.Width - distance, relative.Location.Y + relative.Size.Height + distance),
                PointOffset.UpRight => new Point(relative.Location.X + relative.Size.Width + distance, relative.Location.Y - control.Size.Height - distance),
                PointOffset.DownRight => new Point(relative.Location.X + relative.Size.Width + distance, relative.Location.Y + relative.Size.Height + distance),
                _ => throw new EnumUndefinedOrNotSupportedException<PointOffset>(offset, nameof(offset), null)
            };

            return control;
        }

        public static IDisposable BindPositionTo(this Control control, Control relative, Int32 distance = UserInterfaceUtilities.Distance)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            control.SetPosition(relative, distance);
            return BindTo(relative, nameof(relative.LocationChanged), next => control.SetPosition(((Control?) next.Sender)!, distance));
        }

        public static IDisposable BindPositionTo(this Control control, Control relative, PointOffset offset, Int32 distance = UserInterfaceUtilities.Distance)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            control.SetPosition(relative, offset, distance);
            return BindTo(relative, nameof(relative.LocationChanged), next => control.SetPosition(((Control?) next.Sender)!, offset, distance));
        }

        public static IDisposable BindPositionTo(this Control control, Control relative, HorizontalAlignment alignment, Int32 distance = UserInterfaceUtilities.Distance)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            control.SetPosition(relative, alignment, distance);
            return BindTo(relative, nameof(relative.LocationChanged), next => control.SetPosition(((Control?) next.Sender)!, alignment, distance));
        }

        public static IDisposable BindPositionTo(this Control control, Control relative, PointOffset offset, HorizontalAlignment alignment, Int32 distance = UserInterfaceUtilities.Distance)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            control.SetPosition(relative, offset, alignment, distance);
            return BindTo(relative, nameof(relative.LocationChanged), next => control.SetPosition(((Control?) next.Sender)!, offset, alignment, distance));
        }

        public static IDisposable BindPositionInnerTo(this Control control, Control relative, Int32 distance = UserInterfaceUtilities.Distance)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            control.SetPositionInner(relative, distance);
            return BindTo(relative, nameof(relative.LocationChanged), next => control.SetPositionInner(((Control?) next.Sender)!, distance));
        }

        public static IDisposable BindPositionInnerTo(this Control control, Control relative, PointOffset offset, Int32 distance = UserInterfaceUtilities.Distance)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            control.SetPositionInner(relative, offset, distance);
            return BindTo(relative, nameof(relative.LocationChanged), next => control.SetPositionInner(((Control?) next.Sender)!, offset, distance));
        }

        public static IDisposable BindPositionOuterTo(this Control control, Control relative, Int32 distance = UserInterfaceUtilities.Distance)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            control.SetPositionOuter(relative, distance);
            return BindTo(relative, nameof(relative.LocationChanged), next => control.SetPositionOuter(((Control?) next.Sender)!, distance));
        }

        public static IDisposable BindPositionOuterTo(this Control control, Control relative, PointOffset offset, Int32 distance = UserInterfaceUtilities.Distance)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            control.SetPositionOuter(relative, offset, distance);
            return BindTo(relative, nameof(relative.LocationChanged), next => control.SetPositionOuter(((Control?) next.Sender)!, offset, distance));
        }

        public static IDisposable BindPositionOuterTo(this Control control, Control relative, HorizontalAlignment alignment, Int32 distance = UserInterfaceUtilities.Distance)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            control.SetPositionOuter(relative, alignment, distance);
            return BindTo(relative, nameof(relative.LocationChanged), next => control.SetPositionOuter(((Control?) next.Sender)!, alignment, distance));
        }

        public static IDisposable BindPositionOuterTo(this Control control, Control relative, PointOffset offset, HorizontalAlignment alignment, Int32 distance = UserInterfaceUtilities.Distance)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            control.SetPositionOuter(relative, offset, alignment, distance);
            return BindTo(relative, nameof(relative.LocationChanged), next => control.SetPositionOuter(((Control?) next.Sender)!, offset, alignment, distance));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetSize<T>(this T control, Int32 size, ControlSizeType type) where T : Control
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            return type switch
            {
                ControlSizeType.Both => SetSize(control, size, size),
                ControlSizeType.Width => SetSize(control, size, control.Size.Height),
                ControlSizeType.Height => SetSize(control, control.Size.Width, size),
                _ => throw new EnumUndefinedOrNotSupportedException<ControlSizeType>(type, nameof(type), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetSize<T>(this T control, Int32 width, Int32 height) where T : Control
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            return SetSize(control, new Size(width, height));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetSize<T>(this T control, Size size) where T : Control
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            control.Size = size;
            return control;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetSize<T>(this T control, Control width, Control height) where T : Control
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

            return SetSizeOuter(control, width, height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetSize<T>(this T control, Control relative, ControlSizeType type = ControlSizeType.Both) where T : Control
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
                return SetSizeInner(control, form, type);
            }

            return SetSizeOuter(control, relative, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetSizeInner<T>(this T control, Control relative, ControlSizeType type = ControlSizeType.Both) where T : Control
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
                _ => throw new EnumUndefinedOrNotSupportedException<ControlSizeType>(type, nameof(type), null)
            };

            return control;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetSizeInner<T>(this T control, Control width, Control height) where T : Control
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
            return control;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetSizeOuter<T>(this T control, Control relative, ControlSizeType type = ControlSizeType.Both) where T : Control
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
                _ => throw new EnumUndefinedOrNotSupportedException<ControlSizeType>(type, nameof(type), null)
            };

            return control;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetSizeOuter<T>(this T control, Control width, Control height) where T : Control
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
            return control;
        }

        public static IDisposable BindSizeTo(this Control control, Control relative, ControlSizeType type = ControlSizeType.Both)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            control.SetSize(relative, type);
            return BindTo(relative, nameof(relative.SizeChanged), next => control.SetSize(((Control?) next.Sender)!, type));
        }

        public static IDisposable BindSizeInnerTo(this Control control, Control relative, ControlSizeType type = ControlSizeType.Both)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            control.SetSizeInner(relative, type);
            return BindTo(relative, nameof(relative.SizeChanged), next => control.SetSizeInner(((Control?) next.Sender)!, type));
        }

        public static IDisposable BindSizeOuterTo(this Control control, Control relative, ControlSizeType type = ControlSizeType.Both)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (relative is null)
            {
                throw new ArgumentNullException(nameof(relative));
            }

            control.SetSizeOuter(relative, type);
            return BindTo(relative, nameof(relative.SizeChanged), next => control.SetSizeOuter(((Control?) next.Sender)!, type));
        }
    }
}