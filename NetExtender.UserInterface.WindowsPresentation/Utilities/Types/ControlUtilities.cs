using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using NetExtender.Types.Exceptions;

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
        public static Control? SetIsEnabled(Object? @object, Boolean enabled)
        {
            return SetIsEnabled(@object as Control, enabled);
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
        public static IDisposable SetIsEnabled(Boolean enabled, Object? @object)
        {
            IsEnabledTransaction transaction = new IsEnabledTransaction(@object as Control);
            foreach ((Control control, _) in transaction)
            {
                SetIsEnabled(control, enabled);
            }

            return transaction;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable SetIsEnabled(Boolean enabled, Object? first, Object? second)
        {
            IsEnabledTransaction transaction = new IsEnabledTransaction(first as Control, second as Control);
            foreach ((Control control, _) in transaction)
            {
                SetIsEnabled(control, enabled);
            }

            return transaction;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable SetIsEnabled(Boolean enabled, Object? first, Object? second, Object? third)
        {
            IsEnabledTransaction transaction = new IsEnabledTransaction(first as Control, second as Control, third as Control);
            foreach ((Control control, _) in transaction)
            {
                SetIsEnabled(control, enabled);
            }

            return transaction;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable SetIsEnabled(Boolean enabled, Object? first, Object? second, Object? third, Object? fourth)
        {
            IsEnabledTransaction transaction = new IsEnabledTransaction(first as Control, second as Control, third as Control, fourth as Control);
            foreach ((Control control, _) in transaction)
            {
                SetIsEnabled(control, enabled);
            }

            return transaction;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("objects")]
        public static IDisposable? SetIsEnabled(Boolean enabled, params Object?[]? objects)
        {
            if (objects is null)
            {
                return null;
            }

            IsEnabledTransaction transaction = new IsEnabledTransaction(objects);
            foreach ((Control control, _) in transaction)
            {
                SetIsEnabled(control, enabled);
            }

            return transaction;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("control")]
        public static T? Enable<T>(this T? control) where T : Control
        {
            return SetIsEnabled(control, true);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Enable(Object? @object)
        {
            return SetIsEnabled(true, @object);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Enable(Object? first, Object? second)
        {
            return SetIsEnabled(true, first, second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Enable(Object? first, Object? second, Object? third)
        {
            return SetIsEnabled(true, first, second, third);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Enable(Object? first, Object? second, Object? third, Object? fourth)
        {
            return SetIsEnabled(true, first, second, third, fourth);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("objects")]
        public static IDisposable? Enable(params Object?[]? objects)
        {
            return SetIsEnabled(true, objects);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("control")]
        public static T? Disable<T>(this T? control) where T : Control
        {
            return SetIsEnabled(control, false);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Disable(Object? @object)
        {
            return SetIsEnabled(false, @object);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Disable(Object? first, Object? second)
        {
            return SetIsEnabled(false, first, second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Disable(Object? first, Object? second, Object? third)
        {
            return SetIsEnabled(false, first, second, third);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Disable(Object? first, Object? second, Object? third, Object? fourth)
        {
            return SetIsEnabled(false, first, second, third, fourth);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("objects")]
        public static IDisposable? Disable(params Object?[]? objects)
        {
            return SetIsEnabled(false, objects);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Control? SetVisibility(Object? @object, Visibility visibility)
        {
            return SetVisibility(@object as Control, visibility);
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
        public static IDisposable SetVisibility(Visibility visibility, Object? @object)
        {
            VisibilityTransaction transaction = new VisibilityTransaction(@object as Control);
            foreach ((Control control, _) in transaction)
            {
                SetVisibility(control, visibility);
            }
            
            return transaction;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable SetVisibility(Visibility visibility, Object? first, Object? second)
        {
            VisibilityTransaction transaction = new VisibilityTransaction(first as Control, second as Control);
            foreach ((Control control, _) in transaction)
            {
                SetVisibility(control, visibility);
            }
            
            return transaction;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable SetVisibility(Visibility visibility, Object? first, Object? second, Object? third)
        {
            VisibilityTransaction transaction = new VisibilityTransaction(first as Control, second as Control, third as Control);
            foreach ((Control control, _) in transaction)
            {
                SetVisibility(control, visibility);
            }
            
            return transaction;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable SetVisibility(Visibility visibility, Object? first, Object? second, Object? third, Object? fourth)
        {
            VisibilityTransaction transaction = new VisibilityTransaction(first as Control, second as Control, third as Control, fourth as Control);
            foreach ((Control control, _) in transaction)
            {
                SetVisibility(control, visibility);
            }

            return transaction;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("objects")]
        public static IDisposable? SetVisibility(Visibility visibility, params Object?[]? objects)
        {
            if (objects is null)
            {
                return null;
            }

            VisibilityTransaction transaction = new VisibilityTransaction(objects);
            foreach ((Control control, _) in transaction)
            {
                SetVisibility(control, visibility);
            }

            return transaction;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("control")]
        public static T? Visible<T>(this T? control) where T : Control
        {
            return SetVisibility(control, Visibility.Visible);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Visible(Object? @object)
        {
            return SetVisibility(Visibility.Visible, @object);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Visible(Object? first, Object? second)
        {
            return SetVisibility(Visibility.Visible, first, second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Visible(Object? first, Object? second, Object? third)
        {
            return SetVisibility(Visibility.Visible, first, second, third);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Visible(Object? first, Object? second, Object? third, Object? fourth)
        {
            return SetVisibility(Visibility.Visible, first, second, third, fourth);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("objects")]
        public static IDisposable? Visible(params Object?[]? objects)
        {
            return SetVisibility(Visibility.Visible, objects);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("control")]
        public static T? Hidden<T>(this T? control) where T : Control
        {
            return SetVisibility(control, Visibility.Hidden);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Hidden(Object? @object)
        {
            return SetVisibility(Visibility.Hidden, @object);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Hidden(Object? first, Object? second)
        {
            return SetVisibility(Visibility.Hidden, first, second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Hidden(Object? first, Object? second, Object? third)
        {
            return SetVisibility(Visibility.Hidden, first, second, third);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Hidden(Object? first, Object? second, Object? third, Object? fourth)
        {
            return SetVisibility(Visibility.Hidden, first, second, third, fourth);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("objects")]
        public static IDisposable? Hidden(params Object?[]? objects)
        {
            return SetVisibility(Visibility.Hidden, objects);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("control")]
        public static T? Collapsed<T>(this T? control) where T : Control
        {
            return SetVisibility(control, Visibility.Collapsed);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Collapsed(Object? @object)
        {
            return SetVisibility(Visibility.Collapsed, @object);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Collapsed(Object? first, Object? second)
        {
            return SetVisibility(Visibility.Collapsed, first, second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Collapsed(Object? first, Object? second, Object? third)
        {
            return SetVisibility(Visibility.Collapsed, first, second, third);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Collapsed(Object? first, Object? second, Object? third, Object? fourth)
        {
            return SetVisibility(Visibility.Collapsed, first, second, third, fourth);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("objects")]
        public static IDisposable? Collapsed(params Object?[]? objects)
        {
            return SetVisibility(Visibility.Collapsed, objects);
        }
        
        private abstract class Transaction<T> : IDisposable, IReadOnlyList<T>
        {
            public abstract Int32 Count { get; }
            
            public abstract IEnumerator<T> GetEnumerator();
            
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            
            public abstract void Dispose();
            public abstract T this[Int32 index] { get; }
        }

        private sealed class IsEnabledTransaction : Transaction<KeyValuePair<Control, Boolean>>
        {
            private Transaction<KeyValuePair<Control, Boolean>> Container { get; }
            
            public override Int32 Count
            {
                get
                {
                    return Container.Count;
                }
            }
            
            public IsEnabledTransaction(Control? control)
            {
                Container = new Transaction(control);
            }
            
            public IsEnabledTransaction(Control? first, Control? second)
            {
                Container = new Transaction(first, second);
            }
            
            public IsEnabledTransaction(Control? first, Control? second, Control? third)
            {
                Container = new Transaction(first, second, third);
            }
            
            public IsEnabledTransaction(Control? first, Control? second, Control? third, Control? fourth)
            {
                Container = new Transaction(first, second, third, fourth);
            }

            public IsEnabledTransaction(IEnumerable<Object?> source)
            {
                if (source is null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                Container = new ArrayTransaction(source);
            }
            
            public override IEnumerator<KeyValuePair<Control, Boolean>> GetEnumerator()
            {
                return Container.GetEnumerator();
            }
            
            public override void Dispose()
            {
                Container.Dispose();
            }
            
            public override KeyValuePair<Control, Boolean> this[Int32 index]
            {
                get
                {
                    return Container[index];
                }
            }

            // ReSharper disable once MemberHidesStaticFromOuterClass
            private sealed class Transaction : Transaction<KeyValuePair<Control, Boolean>>
            {
                private readonly KeyValuePair<Control, Boolean>? First;
                private readonly KeyValuePair<Control, Boolean>? Second;
                private readonly KeyValuePair<Control, Boolean>? Third;
                private readonly KeyValuePair<Control, Boolean>? Fourth;
                
                public override Int32 Count
                {
                    get
                    {
                        Int32 i = 0;
                        
                        if (First is not null)
                        {
                            ++i;
                        }
                        
                        if (Second is not null)
                        {
                            ++i;
                        }
                        
                        if (Third is not null)
                        {
                            ++i;
                        }
                        
                        if (Fourth is not null)
                        {
                            ++i;
                        }
                        
                        return i;
                    }
                }
                
                public Transaction(Control? first = null, Control? second = null, Control? third = null, Control? fourth = null)
                {
                    First = Create(first);
                    Second = Create(second);
                    Third = Create(third);
                    Fourth = Create(fourth);
                }
                
                private static KeyValuePair<Control, Boolean>? Create(Control? control)
                {
                    return control is not null ? new KeyValuePair<Control, Boolean>(control, control.IsEnabled) : default;
                }
                
                private static void Dispose(KeyValuePair<Control, Boolean>? pair)
                {
                    if (pair is { Key: { } key, Value: var value})
                    {
                        SetIsEnabled(key, value);
                    }
                }
                
                public override IEnumerator<KeyValuePair<Control, Boolean>> GetEnumerator()
                {
                    if (First is not null)
                    {
                        yield return First.Value;
                    }
                    
                    if (Second is not null)
                    {
                        yield return Second.Value;
                    }
                    
                    if (Third is not null)
                    {
                        yield return Third.Value;
                    }
                    
                    if (Fourth is not null)
                    {
                        yield return Fourth.Value;
                    }
                }
                
                public override void Dispose()
                {
                    Dispose(Fourth);
                    Dispose(Third);
                    Dispose(Second);
                    Dispose(First);
                }
                
                public override KeyValuePair<Control, Boolean> this[Int32 index]
                {
                    get
                    {
                        if (index < 0 || index >= Count)
                        {
                            throw new ArgumentOutOfRangeException(nameof(index), index, null);
                        }
                        
                        Int32 i = -1;
                        if (First is not null && ++i == index)
                        {
                            return First.Value;
                        }
                        
                        if (Second is not null && ++i == index)
                        {
                            return Second.Value;
                        }
                        
                        if (Third is not null && ++i == index)
                        {
                            return Third.Value;
                        }
                        
                        if (Fourth is not null && ++i == index)
                        {
                            return Fourth.Value;
                        }
                        
                        throw new NeverOperationException();
                    }
                }
            }
            
            private sealed class ArrayTransaction : Transaction<KeyValuePair<Control, Boolean>>
            {
                private ImmutableArray<KeyValuePair<Control, Boolean>> Container { get; }
                
                public override Int32 Count
                {
                    get
                    {
                        return Container.Length;
                    }
                }
                
                public ArrayTransaction(IEnumerable<Object?> source)
                {
                    if (source is null)
                    {
                        throw new ArgumentNullException(nameof(source));
                    }
                    
                    Container = source.OfType<Control>().Distinct().Select(static control => new KeyValuePair<Control, Boolean>(control, control.IsEnabled)).ToImmutableArray();
                }
                
                public override IEnumerator<KeyValuePair<Control, Boolean>> GetEnumerator()
                {
                    return ((IEnumerable<KeyValuePair<Control, Boolean>>) Container).GetEnumerator();
                }
                
                public override void Dispose()
                {
                    for (Int32 index = Container.Length - 1; index >= 0; index--)
                    {
                        (Control control, Boolean value) = Container[index];
                        SetIsEnabled(control, value);
                    }
                }
                
                public override KeyValuePair<Control, Boolean> this[Int32 index]
                {
                    get
                    {
                        return Container[index];
                    }
                }
            }
        }
        
        private sealed class VisibilityTransaction : Transaction<KeyValuePair<Control, Visibility>>
        {
            private Transaction<KeyValuePair<Control, Visibility>> Container { get; }
            
            public override Int32 Count
            {
                get
                {
                    return Container.Count;
                }
            }
            
            public VisibilityTransaction(Control? control)
            {
                Container = new Transaction(control);
            }
            
            public VisibilityTransaction(Control? first, Control? second)
            {
                Container = new Transaction(first, second);
            }
            
            public VisibilityTransaction(Control? first, Control? second, Control? third)
            {
                Container = new Transaction(first, second, third);
            }
            
            public VisibilityTransaction(Control? first, Control? second, Control? third, Control? fourth)
            {
                Container = new Transaction(first, second, third, fourth);
            }

            public VisibilityTransaction(IEnumerable<Object?> source)
            {
                if (source is null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                Container = new ArrayTransaction(source);
            }
            
            public override IEnumerator<KeyValuePair<Control, Visibility>> GetEnumerator()
            {
                return Container.GetEnumerator();
            }
            
            public override void Dispose()
            {
                Container.Dispose();
            }
            
            public override KeyValuePair<Control, Visibility> this[Int32 index]
            {
                get
                {
                    return Container[index];
                }
            }

            // ReSharper disable once MemberHidesStaticFromOuterClass
            private sealed class Transaction : Transaction<KeyValuePair<Control, Visibility>>
            {
                private readonly KeyValuePair<Control, Visibility>? First;
                private readonly KeyValuePair<Control, Visibility>? Second;
                private readonly KeyValuePair<Control, Visibility>? Third;
                private readonly KeyValuePair<Control, Visibility>? Fourth;
                
                public override Int32 Count
                {
                    get
                    {
                        Int32 i = 0;
                        
                        if (First is not null)
                        {
                            ++i;
                        }
                        
                        if (Second is not null)
                        {
                            ++i;
                        }
                        
                        if (Third is not null)
                        {
                            ++i;
                        }
                        
                        if (Fourth is not null)
                        {
                            ++i;
                        }
                        
                        return i;
                    }
                }
                
                public Transaction(Control? first = null, Control? second = null, Control? third = null, Control? fourth = null)
                {
                    First = Create(first);
                    Second = Create(second);
                    Third = Create(third);
                    Fourth = Create(fourth);
                }
                
                private static KeyValuePair<Control, Visibility>? Create(Control? control)
                {
                    return control is not null ? new KeyValuePair<Control, Visibility>(control, control.Visibility) : default;
                }
                
                private static void Dispose(KeyValuePair<Control, Visibility>? pair)
                {
                    if (pair is { Key: { } key, Value: var value})
                    {
                        SetVisibility(key, value);
                    }
                }
                
                public override IEnumerator<KeyValuePair<Control, Visibility>> GetEnumerator()
                {
                    if (First is not null)
                    {
                        yield return First.Value;
                    }
                    
                    if (Second is not null)
                    {
                        yield return Second.Value;
                    }
                    
                    if (Third is not null)
                    {
                        yield return Third.Value;
                    }
                    
                    if (Fourth is not null)
                    {
                        yield return Fourth.Value;
                    }
                }
                
                public override void Dispose()
                {
                    Dispose(Fourth);
                    Dispose(Third);
                    Dispose(Second);
                    Dispose(First);
                }
                
                public override KeyValuePair<Control, Visibility> this[Int32 index]
                {
                    get
                    {
                        if (index < 0 || index >= Count)
                        {
                            throw new ArgumentOutOfRangeException(nameof(index), index, null);
                        }
                        
                        Int32 i = -1;
                        if (First is not null && ++i == index)
                        {
                            return First.Value;
                        }
                        
                        if (Second is not null && ++i == index)
                        {
                            return Second.Value;
                        }
                        
                        if (Third is not null && ++i == index)
                        {
                            return Third.Value;
                        }
                        
                        if (Fourth is not null && ++i == index)
                        {
                            return Fourth.Value;
                        }
                        
                        throw new NeverOperationException();
                    }
                }
            }
            
            private sealed class ArrayTransaction : Transaction<KeyValuePair<Control, Visibility>>
            {
                private ImmutableArray<KeyValuePair<Control, Visibility>> Container { get; }
                
                public override Int32 Count
                {
                    get
                    {
                        return Container.Length;
                    }
                }
                
                public ArrayTransaction(IEnumerable<Object?> source)
                {
                    if (source is null)
                    {
                        throw new ArgumentNullException(nameof(source));
                    }
                    
                    Container = source.OfType<Control>().Distinct().Select(static control => new KeyValuePair<Control, Visibility>(control, control.Visibility)).ToImmutableArray();
                }
                
                public override IEnumerator<KeyValuePair<Control, Visibility>> GetEnumerator()
                {
                    return ((IEnumerable<KeyValuePair<Control, Visibility>>) Container).GetEnumerator();
                }
                
                public override void Dispose()
                {
                    for (Int32 index = Container.Length - 1; index >= 0; index--)
                    {
                        (Control control, Visibility value) = Container[index];
                        SetVisibility(control, value);
                    }
                }
                
                public override KeyValuePair<Control, Visibility> this[Int32 index]
                {
                    get
                    {
                        return Container[index];
                    }
                }
            }
        }
    }
}