// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NetExtender.Types.Exceptions.Handlers;
using NetExtender.Utilities.Core;
using NetExtender.WindowsPresentation.Types.Exceptions;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class CustomUserControl : UserControl, IWindowsPresentationCommandExceptionHandler
    {
        protected virtual ExceptionHandlerAction Handle(Object? sender, Exception? exception)
        {
            if (Window.GetWindow(this) is IWindowsPresentationExceptionHandler handler)
            {
                return handler.Exception(sender, exception);
            }
            
            return ExceptionHandlerAction.Throw;
        }
        
        ExceptionHandlerAction IWindowsPresentationExceptionHandler.Exception(Object? sender, Exception? exception)
        {
            return Handle(sender, exception);
        }
        
        protected virtual ExceptionHandlerAction Handle<T>(Object? sender, ICommand? command, T? parameter, Exception? exception)
        {
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            if (GetType().GetMethod(nameof(Handle), binding, new []{ typeof(Object), typeof(Exception) }) is { } method && method.IsOverridden())
            {
                return Handle(sender, exception);
            }
            
            if (Window.GetWindow(this) is IWindowsPresentationCommandExceptionHandler handler)
            {
                return handler.Exception(sender, command, parameter, exception);
            }
            
            return Handle(sender, exception);
        }
        
        ExceptionHandlerAction IWindowsPresentationCommandExceptionHandler.Exception<T>(Object? sender, ICommand? command, T? parameter, Exception? exception) where T : default
        {
            return Handle(sender, command, parameter, exception);
        }
    }
}