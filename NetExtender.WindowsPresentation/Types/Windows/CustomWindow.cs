// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Input;
using NetExtender.Types.Exceptions.Handlers;
using NetExtender.WindowsPresentation.Types.Exceptions;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class CustomWindow : Window, IWindowsPresentationCommandExceptionHandler
    {
        protected virtual ExceptionHandlerAction Exception(Object? sender, Exception? exception)
        {
            return DependencyObjectExceptionUtilities.Exception(this, sender, exception);
        }
        
        ExceptionHandlerAction IWindowsPresentationExceptionHandler.Exception(Object? sender, Exception? exception)
        {
            return Exception(sender, exception);
        }
        
        protected virtual ExceptionHandlerAction Exception<T>(Object? sender, ICommand? command, T? parameter, Exception? exception)
        {
            return DependencyObjectExceptionUtilities.Exception(this, sender, command, parameter, exception);
        }
        
        ExceptionHandlerAction IWindowsPresentationCommandExceptionHandler.Exception<T>(Object? sender, ICommand? command, T? parameter, Exception? exception) where T : default
        {
            return Exception(sender, command, parameter, exception);
        }
    }
}