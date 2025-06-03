using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.Filters;
using NetExtender.AspNetCore.Filters;
using NetExtender.AspNetCore.Identity.Interfaces;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class FilterCollectionUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FilterCollection AddIdentityFilter<TId, TUser, TRole, TFilter>(this FilterCollection collection) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole> where TFilter : IdentityServiceFilter<TId, TUser, TRole>
        {
            return AddIdentityFilter<TId, TUser, TRole, ActionInfoServiceFilter<TId, TUser, TRole>, TFilter>(collection);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FilterCollection AddIdentityFilter<TId, TUser, TRole, TActionFilter, TIdentityFilter>(this FilterCollection collection) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole> where TIdentityFilter : IdentityServiceFilter<TId, TUser, TRole> where TActionFilter : ActionInfoServiceFilter<TId, TUser, TRole>
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.Add<TActionFilter>();
            collection.Add<TIdentityFilter>();
            return collection;
        }
    }
}