// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data.Common;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NetExtender.EntityFrameworkCore;

namespace NetExtender.Utils.EntityFrameworkCore
{
    public static class DbContextUtils
    {
        public static DbConnectionStringBuilder GetConnectionStringBuilder(this DbConnection connection)
        {
            return GetConnectionStringBuilder<DbConnectionStringBuilder>(connection);
        }

        public static TConnection GetConnectionStringBuilder<TConnection>(this DbConnection connection) where TConnection : DbConnectionStringBuilder, new()
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            return new TConnection
            {
                ConnectionString = connection.ConnectionString
            };
        }

        public static T? NoLockTransaction<T, TDbContext>(this TDbContext context, Func<TDbContext, T?> action) where TDbContext : DbContext
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            using TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, TransactionParameters.ReadUncommitted);
            
            T? result = action(context);
            transaction.Complete();

            return result;
        }

        public static T? TransactionExecute<T, TDbContext>(this TDbContext context, Func<TDbContext, T?> action) where TDbContext : DbContext
        {
            return TransactionExecute(context, action, TransactionScopeOption.Required);
        }

        public static T? TransactionExecute<T, TDbContext>(this TDbContext context, Func<TDbContext, T?> action, TransactionScopeOption option) where TDbContext : DbContext
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            using TransactionScope transaction = new TransactionScope(option);
            
            T? result = action(context);
            transaction.Complete();

            return result;
        }

        public static EntityEntry<TEntity>? Remove<TEntity>(this DbContext context, params Object?[] keys) where TEntity : class
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (keys is null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            TEntity? entity = context.Find<TEntity>(keys);
            return entity is not null ? context.Remove(entity) : null;
        }
        
        public static String? GetTableName<TEntity>(this DbContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.Model.GetTableName<TEntity>();
        }
        
        public static String? GetTableName(this DbContext context, Type type)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return context.Model.GetTableName(type);
        }
    }
}