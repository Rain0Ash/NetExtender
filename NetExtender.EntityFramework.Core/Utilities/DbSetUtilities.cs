// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace NetExtender.Utilities.EntityFrameworkCore
{
    public static class DbSetUtilities
    {
        public static DbContext? GetDatabaseContext<TEntity>(this DbSet<TEntity> set) where TEntity : class
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            FieldInfo? context = set.GetType().GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
            return (DbContext?) context?.GetValue(set);
        }
    }
}