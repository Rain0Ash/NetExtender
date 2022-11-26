// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class RouteUtilities
    {
        public static String GetActionName<TController>(this Expression<Func<TController, Object>> action) where TController : ControllerBase
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MethodInfo method = ((MethodCallExpression) action.Body).Method;
            String result = method.Name;

            ActionNameAttribute[]? attributes = (ActionNameAttribute[]?) method.GetCustomAttributes(typeof(ActionNameAttribute), false);

            if (attributes is not null && attributes.Length > 0)
            {
                result = attributes[0].Name;
            }

            return result;
        }

        public static String GetControllerName<TController>() where TController : ControllerBase
        {
            Type type = typeof(TController);
            String result = type.Name;

            const String controller = "Controller";
            if (result.EndsWith(controller, StringComparison.InvariantCultureIgnoreCase))
            {
                result = result.Substring(0, result.Length - controller.Length);
            }

            return result;
        }

        public static String? GetControllerNamespace<TController>() where TController : ControllerBase
        {
            Type type = typeof(TController);
            String? result = type.FullName;
            result = result?.Substring(0, result.Length - (type.Name.Length + 1));
            return result;
        }
    }
}