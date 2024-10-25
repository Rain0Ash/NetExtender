using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using NetExtender.Types.Exceptions.Handlers;
using NetExtender.Types.Exceptions.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;
using NetExtender.WindowsPresentation.Types.Exceptions;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    public static class DependencyObjectExceptionUtilities
    {
        internal static ExceptionHandlerAction Exception(this DependencyObject @object, Object? sender, Exception? exception)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }
            
            (String? title, String? message) = exception switch
            {
                null => ("Unknown exception", $"{(sender is not null ? $"Sender '{sender}' throw unknown exception" : "Unknown exception")}."),
                ICommandException => ("Command exception", $"{(sender is not null ? $"Sender '{sender}' throw command exception" : "Exception")} '{exception.GetType()}' with message '{exception.Message}':{Environment.NewLine}{exception.StackTrace}."),
                IBusinessException => ("Business exception", $"{(sender is not null ? $"Sender '{sender}' throw business exception" : "Exception")} '{exception.GetType()}' with message '{exception.Message}':{Environment.NewLine}{exception.StackTrace}."),
                _ => (null, null)
            };
            
            if (title is null)
            {
                return ExceptionHandlerAction.Throw;
            }
            
            MessageBoxUtilities.OK.None.Show(@object as Window, title, message, MessageBoxImage.Error);
            return ExceptionHandlerAction.Successful;
        }
        
        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        internal static ExceptionHandlerAction Exception<T>(this DependencyObject @object, Object? sender, ICommand? command, T? parameter, Exception? exception)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }
            
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            if (@object is IWindowsPresentationExceptionHandler handler && @object.GetType().GetMethod(nameof(Exception), binding, new []{ typeof(Object), typeof(Exception) }) is { } method && method.IsOverridden())
            {
                return handler.Exception(sender, exception);
            }
            
            if (exception is not IBusinessException)
            {
                return Exception(@object, sender, exception);
            }
            
            String title = exception switch
            {
                ICommandException => "Command exception",
                IBusinessException => "Business exception",
                _ => "Exception"
            };
            
            String message = $"{(sender is not null ? $"Sender '{sender}' " : String.Empty)}{(command is not null ? $"{(sender is null ? "Command" : "command")} '{command}' " : String.Empty)}{(sender is null && command is null ? "Exception" : "throw exception")} '{exception.GetType()}' with message '{exception.Message}' for parameter '{parameter?.ToString() ?? StringUtilities.NullString}':{Environment.NewLine}{exception.StackTrace}.";
            
            MessageBoxUtilities.OK.None.Show(@object as Window, title, message, MessageBoxImage.Error);
            return ExceptionHandlerAction.Successful;
        }
    }
}