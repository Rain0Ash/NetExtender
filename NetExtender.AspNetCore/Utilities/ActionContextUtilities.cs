using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetExtender.Types.Exceptions;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class ActionContextUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask Execute(this ActionExecutionDelegate? @delegate)
        {
            if (@delegate is not null)
            {
                await @delegate();
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? Authorization(this ActionContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            
            return context.HttpContext.Authorization();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Authorization(this ActionContext context, [MaybeNullWhen(false)] out String authorization)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            
            return context.HttpContext.Authorization(out authorization);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask Exception(this ActionContext context, BusinessException exception)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.HttpContext.Exception(exception);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask Exception<T>(this ActionContext context) where T : BusinessException, new()
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            
            return context.HttpContext.Exception<T>();
        }
    }
}