// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;
using System.Windows.Interop;
using Expression = System.Linq.Expressions.Expression;

namespace NetExtender.Utilities.UserInterface
{
    public static class WindowsPresentationWindowUtilities
    {
        private static Action<Window, Boolean?>? SetDialogResultAction { get; }

        static WindowsPresentationWindowUtilities()
        {
            static Action<Window, Boolean?>? DialogResult()
            {
                try
                {
                    FieldInfo? field = typeof(Window).GetField("_dialogResult", BindingFlags.Instance | BindingFlags.NonPublic);

                    if (field is null)
                    {
                        return null;
                    }

                    ParameterExpression window = Expression.Parameter(typeof(Window), nameof(window));
                    ParameterExpression value = Expression.Parameter(typeof(Boolean?), nameof(value));
                    MemberExpression fieldAccess = Expression.Field(window, field);
                    BinaryExpression assign = Expression.Assign(fieldAccess, value);
                    Expression<Action<Window, Boolean?>> lambda = Expression.Lambda<Action<Window, Boolean?>>(assign, window, value);
                    return lambda.Compile();
                }
                catch (Exception)
                {
                    return null;
                }
            }

            SetDialogResultAction = DialogResult();
        }
        
        public static IntPtr GetHandle(this Window window)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return new WindowInteropHelper(window).Handle;
        }

        public static Boolean SetDialogResult(this Window window, Boolean? value)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            if (SetDialogResultAction is not { } action)
            {
                return false;
            }

            action(window, value);
            return true;
        }
    }
}