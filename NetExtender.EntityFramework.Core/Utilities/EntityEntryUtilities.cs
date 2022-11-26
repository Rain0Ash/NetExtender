// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace NetExtender.Utilities.EntityFrameworkCore
{
    public static class EntityEntryUtilities
    {
        public static Boolean IsChange(this EntityEntry entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return IsChange(entity.State);
        }

        public static Boolean IsChange<T>(this EntityEntry<T> entity) where T : class
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return IsChange(entity.State);
        }

        public static Boolean IsChange(this EntityState state)
        {
            return state switch
            {
                EntityState.Detached => false,
                EntityState.Unchanged => false,
                EntityState.Deleted => true,
                EntityState.Modified => true,
                EntityState.Added => true,
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }

        public static Boolean IsModify(this EntityEntry entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return IsModify(entity.State);
        }

        public static Boolean IsModify<T>(this EntityEntry<T> entity) where T : class
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return IsModify(entity.State);
        }

        public static Boolean IsModify(this EntityState state)
        {
            return state switch
            {
                EntityState.Detached => false,
                EntityState.Unchanged => false,
                EntityState.Deleted => true,
                EntityState.Modified => true,
                EntityState.Added => false,
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }
    }
}