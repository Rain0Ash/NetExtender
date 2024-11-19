using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using NetExtender.UserInterface.WindowsPresentation;
using NetExtender.WindowsPresentation.Types.Commands;

namespace NetExtender.DependencyInjection
{
    internal static class NetExtenderWindowsPresentationInitializer
    {
#pragma warning disable CA2255
        [ModuleInitializer]
        public static void Initialize()
        {
            EventManager.RegisterClassHandler(typeof(FrameworkElement), FrameworkElement.ContextMenuOpeningEvent, new RoutedEventHandler(OnContextMenuOpening));
        }
        
        // ReSharper disable once CognitiveComplexity
        private static void OnContextMenuOpening(Object sender, RoutedEventArgs args)
        {
            if (args.Handled)
            {
                return;
            }

            ContextMenu? menu = sender switch
            {
                ContextMenu element => element,
                FrameworkElement element => element.ContextMenu,
                _ => null
            };

            if (menu is null || menu.GetValue(TargetContextMenu.AutoHideProperty) is false)
            {
                return;
            }

            args.Handled = true;

            if (menu.Items.Count <= 0)
            {
                args.Handled = true;
                return;
            }

            foreach (Object item in menu.Items.OfType<Object>())
            {
                switch (item)
                {
                    case AutoHideMenuItem element:
                    {
                        if (element is { AutoHide: true, Command: not null })
                        {
                            Object? parameter = element.CommandParameter ?? menu.DataContext ?? (sender as FrameworkElement)?.DataContext;
                            if (element.Command.CanExecute(new CommandSenderArgs(sender, parameter)) || element.Command.CanExecute(parameter))
                            {
                                args.Handled = false;
                                return;
                            }

                            continue;
                        }
                        
                        if (element.Visibility == Visibility.Visible)
                        {
                            args.Handled = false;
                            return;
                        }

                        if (element.IsEnabled)
                        {
                            args.Handled = false;
                            return;
                        }
                        
                        continue;
                    }
                    case FrameworkElement element:
                    {
                        if (element.Visibility == Visibility.Visible)
                        {
                            args.Handled = false;
                            return;
                        }
                        
                        continue;
                    }
                    default:
                    {
                        continue;
                    }
                }
            }

            args.Handled = true;
        }
#pragma warning restore CA2255
    }
}