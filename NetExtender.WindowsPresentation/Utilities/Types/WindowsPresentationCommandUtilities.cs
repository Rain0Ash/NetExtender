// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using NetExtender.Types.Exceptions.Handlers;
using NetExtender.WindowsPresentation.Types.Exceptions;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    public static class WindowsPresentationCommandUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange(this CommandBindingCollection collection, params CommandBinding?[]? commands)
        {
            AddRange(collection, (IEnumerable<CommandBinding?>?) commands);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange(this CommandBindingCollection collection, IEnumerable<CommandBinding?>? commands)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (commands is null)
            {
                return;
            }
            
            foreach (CommandBinding? command in commands)
            {
                if (command is null)
                {
                    continue;
                }
                
                collection.Add(command);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ExceptionHandlerAction Exception<T>(ICommand? command, Object? sender, T? parameter, Exception? exception)
        {
            return Exception(command, sender as DependencyObject, parameter, exception);
        }
        
        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static ExceptionHandlerAction Exception<T>(ICommand? command, DependencyObject? sender, T? parameter, Exception? exception)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            
            DependencyObject? current = sender;
            
            while (current is not null && current is not IWindowsPresentationExceptionHandler)
            {
                current = current.GetAncestor();
            }
            
            if (current is null)
            {
                current = sender;
                while (current is not null && current is not IWindowsPresentationExceptionHandler)
                {
                    current = current.GetParent();
                }
            }
            
            current ??= sender is not null ? Window.GetWindow(sender) : null;

            return current switch
            {
                IWindowsPresentationCommandExceptionHandler handler => handler.Exception(sender, command, parameter, exception),
                IWindowsPresentationExceptionHandler handler => handler.Exception(sender, exception),
                _ => ExceptionHandlerAction.Throw
            };
        }
    }
}