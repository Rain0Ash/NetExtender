// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    public static class DependencyObjectUtilities
    {
        public static PropertyChangedCallback Reactive(this PropertyChangedCallback? callback)
        {
            if (callback is null)
            {
                return static (@object, args) =>
                {
                    if (Equals(args.OldValue, args.NewValue))
                    {
                        return;
                    }

                    @object.RaisePropertyChanging(args.Property.Name);
                    @object.RaisePropertyChanged(args.Property.Name);
                };
            }
            
            return (@object, args) =>
            {
                if (Equals(args.OldValue, args.NewValue))
                {
                    callback(@object, args);
                    return;
                }

                @object.RaisePropertyChanging(args.Property.Name);
                callback(@object, args);
                @object.RaisePropertyChanged(args.Property.Name);
            };
        }
        
        public static TMetadata Reactive<TMetadata>(this TMetadata metadata) where TMetadata : PropertyMetadata
        {
            if (metadata is null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }
            
            metadata.PropertyChangedCallback = Reactive(metadata.PropertyChangedCallback);
            return metadata;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? TryFindAncestor<T>(this DependencyObject @object) where T : DependencyObject
        {
            return TryFindAncestor<T>(@object, true);
        }
        
        public static T? TryFindAncestor<T>(this DependencyObject @object, Boolean template) where T : DependencyObject
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }
            
            do
            {
                if (@object is T result)
                {
                    return result;
                }
                
                if (GetAncestor(@object, template) is not { } ancestor)
                {
                    return null;
                }
                
                @object = ancestor;
            } while (true);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? TryFindParentAncestor<T>(this DependencyObject @object) where T : DependencyObject
        {
            return TryFindParentAncestor<T>(@object, true);
        }
        
        public static T? TryFindParentAncestor<T>(this DependencyObject @object, Boolean template) where T : DependencyObject
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }
            
            do
            {
                if (GetAncestor(@object, template) is not { } ancestor)
                {
                    return null;
                }
                
                if (@object is T result)
                {
                    return result;
                }
                
                @object = ancestor;
            } while (true);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DependencyObject? GetAncestor(this DependencyObject @object)
        {
            return GetAncestor(@object, true);
        }
        
        public static DependencyObject? GetAncestor(this DependencyObject @object, Boolean template)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }
            
            return @object switch
            {
                ContextMenu menu => menu.PlacementTarget,
                FrameworkElement { TemplatedParent: { } parent } when template => parent,
                _ => VisualTreeHelper.GetParent(@object)
            };
        }
        
        public static T? TryFindParent<T>(this DependencyObject @object) where T : DependencyObject
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            while (@object.GetParent() is { } parent)
            {
                if (parent is T dependency)
                {
                    return dependency;
                }

                @object = parent;
            }

            return null;
        }

        public static DependencyObject? GetParent(this DependencyObject @object)
        {
            return @object switch
            {
                null => throw new ArgumentNullException(nameof(@object)),
                ContentElement content => ContentOperations.GetParent(content) ?? (content as FrameworkContentElement)?.Parent,
                _ => VisualTreeHelper.GetParent(@object) ?? (@object as FrameworkElement)?.Parent
            };
        }
    }
}