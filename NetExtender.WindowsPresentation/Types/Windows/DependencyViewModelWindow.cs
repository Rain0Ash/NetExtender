// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows;
using NetExtender.DependencyInjection.Interfaces;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation.Windows
{
    public class DependencyViewModelWindow : Window
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static T? ViewModel<T>() where T : IDependencyViewModel
        {
            return DependencyViewModelUtilities.Get<T>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static T ViewModel<T>(Func<T> alternate) where T : IDependencyViewModel
        {
            return DependencyViewModelUtilities.Get(alternate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static T? ViewModel<T>(Boolean @throw) where T : IDependencyViewModel
        {
            return @throw ? DependencyViewModelUtilities.Require<T>() : ViewModel<T>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static T ViewModel<T, TAlternate>() where T : IDependencyViewModel where TAlternate : T, new()
        {
            return DependencyViewModelUtilities.Get<T, TAlternate>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static Boolean ViewModel<T>([MaybeNullWhen(false)] out T result) where T : IDependencyViewModel
        {
            return DependencyViewModelUtilities.Get(out result);
        }
    }
}