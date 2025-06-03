// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Input;
using NetExtender.Types.Exceptions.Handlers;

namespace NetExtender.WindowsPresentation.Types.Exceptions
{
    public interface IWindowsPresentationCommandExceptionHandler : IWindowsPresentationExceptionHandler
    {
        public ExceptionHandlerAction Exception<T>(Object? sender, ICommand? command, T? parameter, Exception? exception)
        {
            return Exception(sender, exception);
        }
    }
    
    public interface IWindowsPresentationExceptionHandler
    {
        public ExceptionHandlerAction Exception(Object? sender, Exception? exception);
    }
}