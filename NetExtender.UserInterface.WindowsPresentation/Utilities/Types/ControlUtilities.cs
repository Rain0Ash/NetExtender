using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace NetExtender.Utilities.UserInterface.Types
{
    public static class ControlUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean? IsEnabled(Object? @object)
        {
            return @object is Control control ? control.IsEnabled : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsEnabled(Object? @object, Boolean enabled)
        {
            SetIsEnabled(@object as Control, enabled);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("control")]
        public static T? SetIsEnabled<T>(this T? control, Boolean enabled) where T : Control
        {
            if (control is not null)
            {
                control.IsEnabled = enabled;
            }
            
            return control;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("objects")]
        public static IDisposable? SetIsEnabled(Boolean enabled, params Object?[]? objects)
        {
            if (objects is null)
            {
                return null;
            }

            IDisposable transaction = new IsEnabledTransaction(objects);
            foreach (Object? @object in objects)
            {
                SetIsEnabled(@object, enabled);
            }

            return transaction;
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
            return SetIsEnabled(control, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("objects")]
        public static IDisposable? Enable(params Object?[]? objects)
        {
            return SetIsEnabled(true, objects);
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
            return SetIsEnabled(control, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("objects")]
        public static IDisposable? Disable(params Object?[]? objects)
        {
            return SetIsEnabled(false, objects);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetVisibility(Object? @object, Visibility visibility)
        {
            SetVisibility(@object as Control, visibility);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("control")]
        public static T? SetVisibility<T>(this T? control, Visibility visibility) where T : Control
        {
            if (control is not null)
            {
                control.Visibility = visibility;
            }
            
            return control;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("objects")]
        public static IDisposable? SetVisibility(Visibility visibility, params Object?[]? objects)
        {
            if (objects is null)
            {
                return null;
            }

            IDisposable transaction = new VisibilityTransaction(objects);
            foreach (Object? @object in objects)
            {
                SetVisibility(@object, visibility);
            }

            return transaction;
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
            return SetVisibility(control, Visibility.Visible);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("objects")]
        public static IDisposable? Visible(params Object?[]? objects)
        {
            return SetVisibility(Visibility.Visible, objects);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Hidden(Object? @object)
        {
            Hidden(@object as Control);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("control")]
        public static T? Hidden<T>(this T? control) where T : Control
        {
            return SetVisibility(control, Visibility.Hidden);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("objects")]
        public static IDisposable? Hidden(params Object?[]? objects)
        {
            return SetVisibility(Visibility.Hidden, objects);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Collapsed(Object? @object)
        {
            Collapsed(@object as Control);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("control")]
        public static T? Collapsed<T>(this T? control) where T : Control
        {
            return SetVisibility(control, Visibility.Collapsed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("objects")]
        public static IDisposable? Collapsed(params Object?[]? objects)
        {
            return SetVisibility(Visibility.Collapsed, objects);
        }

        private sealed class IsEnabledTransaction : IDisposable
        {
            private Dictionary<Control, Boolean> Container { get; }

            public IsEnabledTransaction(IEnumerable<Object?> source)
            {
                if (source is null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                Container = source.OfType<Control>().Distinct().ToDictionary(control => control, control => control.IsEnabled);
            }

            public void Dispose()
            {
                foreach ((Control control, Boolean value) in Container)
                {
                    SetIsEnabled(control, value);
                }
            }
        }

        private sealed class VisibilityTransaction : IDisposable
        {
            private Dictionary<Control, Visibility> Container { get; }

            public VisibilityTransaction(IEnumerable<Object?> source)
            {
                if (source is null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                Container = source.OfType<Control>().Distinct().ToDictionary(control => control, control => control.Visibility);
            }

            public void Dispose()
            {
                foreach ((Control control, Visibility value) in Container)
                {
                    SetVisibility(control, value);
                }
            }
        }
    }
}