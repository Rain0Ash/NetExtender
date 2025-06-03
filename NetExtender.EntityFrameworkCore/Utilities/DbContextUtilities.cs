// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using NetExtender.EntityFrameworkCore;
using NetExtender.EntityFrameworkCore.Entities.Auditing.Interfaces;
using NetExtender.EntityFrameworkCore.Entities.Concurrent.Interfaces;
using NetExtender.EntityFrameworkCore.Entities.Logging;
using NetExtender.EntityFrameworkCore.Entities.Tracking.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.Utilities.EntityFrameworkCore
{
    public static class DbContextUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UseLazyLoading(this DbContextOptionsBuilder builder)
        {
            UseLazyLoading(builder, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UseLazyLoading(this DbContextOptionsBuilder builder, IInterceptor? interceptor)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (interceptor is not null)
            {
                builder.AddInterceptors(interceptor);
            }

            builder.UseLazyLoadingProxies();
        }
        
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

        public static T ExecuteInTransaction<T>(this DbContext context, Func<T> method)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            IDbContextTransaction? current = context.Database.CurrentTransaction;
            IDbContextTransaction transaction = current ?? context.Database.BeginTransaction();

            try
            {
                T result = method.Invoke();
                if (transaction != current)
                {
                    transaction.Commit();
                }

                return result;
            }
            catch (Exception)
            {
                if (transaction == current)
                {
                    throw;
                }

                transaction.Rollback();
                throw;
            }
            finally
            {
                if (transaction != current)
                {
                    transaction.Dispose();
                }
            }
        }

        public static async Task<T> ExecuteInTransactionAsync<T>(this DbContext context, Func<Task<T>> method)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            IDbContextTransaction? current = context.Database.CurrentTransaction;
            IDbContextTransaction transaction = current ?? await context.Database.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                T result = await method.Invoke().ConfigureAwait(false);
                if (transaction != current)
                {
                    await transaction.CommitAsync().ConfigureAwait(false);
                }

                return result;
            }
            catch
            {
                if (transaction == current)
                {
                    throw;
                }

                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }
            finally
            {
                if (transaction != current)
                {
                    await transaction.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        private const String RowVersion = nameof(IConcurrencyCheckableEntity<Guid>.RowVersion);

        private static void UpdateRowVersion(EntityEntry entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Object value = entity.OriginalValues[RowVersion]!;
            switch (entity.Entity)
            {
                case IConcurrencyCheckableEntity<Byte[]> checkable:
                    checkable.RowVersion = (Byte[]) value;
                    return;
                case IConcurrencyCheckableEntity<SByte> checkable:
                    checkable.RowVersion = (SByte) value;
                    return;
                case IConcurrencyCheckableEntity<Byte> checkable:
                    checkable.RowVersion = (Byte) value;
                    return;
                case IConcurrencyCheckableEntity<Int16> checkable:
                    checkable.RowVersion = (Int16) value;
                    return;
                case IConcurrencyCheckableEntity<UInt16> checkable:
                    checkable.RowVersion = (UInt16) value;
                    return;
                case IConcurrencyCheckableEntity<Int32> checkable:
                    checkable.RowVersion = (Int32) value;
                    return;
                case IConcurrencyCheckableEntity<UInt32> checkable:
                    checkable.RowVersion = (UInt32) value;
                    return;
                case IConcurrencyCheckableEntity<Int64> checkable:
                    checkable.RowVersion = (Int64) value;
                    return;
                case IConcurrencyCheckableEntity<UInt64> checkable:
                    checkable.RowVersion = (UInt64) value;
                    return;
                case IConcurrencyCheckableEntity<Half> checkable:
                    checkable.RowVersion = (Half) value;
                    return;
                case IConcurrencyCheckableEntity<Single> checkable:
                    checkable.RowVersion = (Single) value;
                    return;
                case IConcurrencyCheckableEntity<Double> checkable:
                    checkable.RowVersion = (Double) value;
                    return;
                case IConcurrencyCheckableEntity<Decimal> checkable:
                    checkable.RowVersion = (Decimal) value;
                    return;
                case IConcurrencyCheckableEntity<TimeSpan> checkable:
                    checkable.RowVersion = (TimeSpan) value;
                    return;
                case IConcurrencyCheckableEntity<DateTime> checkable:
                    checkable.RowVersion = (DateTime) value;
                    return;
                case IConcurrencyCheckableEntity<Guid> checkable:
                    checkable.RowVersion = (Guid) value;
                    return;
                case IConcurrencyCheckableEntity<String> checkable:
                    checkable.RowVersion = (String) value;
                    return;
                case IConcurrencyCheckableEntity<Object> checkable:
                    checkable.RowVersion = value;
                    return;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static void UpdateConcurrentEntities(this DbContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            foreach (EntityEntry entity in context.ChangeTracker.Entries().Where(entity => entity.State == EntityState.Modified || entity.State == EntityState.Deleted))
            {
                switch (entity.Entity)
                {
                    case IConcurrencyCheckableEntity<Byte[]> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        continue;
                    case IConcurrencyCheckableEntity<SByte> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        continue;
                    case IConcurrencyCheckableEntity<Byte> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        continue;
                    case IConcurrencyCheckableEntity<Int16> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        continue;
                    case IConcurrencyCheckableEntity<UInt16> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        continue;
                    case IConcurrencyCheckableEntity<Int32> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        continue;
                    case IConcurrencyCheckableEntity<UInt32> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        continue;
                    case IConcurrencyCheckableEntity<Int64> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        continue;
                    case IConcurrencyCheckableEntity<UInt64> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        continue;
                    case IConcurrencyCheckableEntity<Half> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        continue;
                    case IConcurrencyCheckableEntity<Single> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        continue;
                    case IConcurrencyCheckableEntity<Double> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        continue;
                    case IConcurrencyCheckableEntity<Decimal> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        continue;
                    case IConcurrencyCheckableEntity<TimeSpan> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        continue;
                    case IConcurrencyCheckableEntity<DateTime> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        continue;
                    case IConcurrencyCheckableEntity<Guid> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        checkable.RowVersion = Guid.NewGuid();
                        continue;
                    case IConcurrencyCheckableEntity<String> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        continue;
                    case IConcurrencyCheckableEntity<Object> checkable:
                        entity.OriginalValues[RowVersion] = checkable.RowVersion;
                        continue;
                    default:
                        continue;
                }
            }
        }

        public static void SaveChangesIgnoreConcurrency(this DbContext context)
        {
            SaveChangesIgnoreConcurrency(context, 3);
        }

        public static void SaveChangesIgnoreConcurrency(this DbContext context, Int32 retry)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Int32 errors = 0;
            while (true)
            {
                try
                {
                    context.SaveChanges();
                    return;
                }
                catch (DbUpdateConcurrencyException exception)
                {
                    if (errors++ >= retry)
                    {
                        throw;
                    }

                    EntityEntry entity = exception.Entries.Single();
                    PropertyValues? values = entity.GetDatabaseValues();
                    entity.OriginalValues.SetValues(values!);
                    UpdateRowVersion(entity);
                }
            }
        }

        public static Task SaveChangesIgnoreConcurrencyAsync(this DbContext context)
        {
            return SaveChangesIgnoreConcurrencyAsync(context, CancellationToken.None);
        }

        public static Task SaveChangesIgnoreConcurrencyAsync(this DbContext context, CancellationToken token)
        {
            return SaveChangesIgnoreConcurrencyAsync(context, 3, token);
        }

        public static Task SaveChangesIgnoreConcurrencyAsync(this DbContext context, Int32 retry)
        {
            return SaveChangesIgnoreConcurrencyAsync(context, retry, CancellationToken.None);
        }

        public static async Task SaveChangesIgnoreConcurrencyAsync(this DbContext context, Int32 retry, CancellationToken token)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Int32 errors = 0;
            while (true)
            {
                try
                {
                    token.ThrowIfCancellationRequested();
                    await context.SaveChangesAsync(token).ConfigureAwait(false);
                    return;
                }
                catch (DbUpdateConcurrencyException exception)
                {
                    if (errors++ >= retry)
                    {
                        throw;
                    }

                    token.ThrowIfCancellationRequested();
                    EntityEntry entity = exception.Entries.Single();
                    PropertyValues? values = await entity.GetDatabaseValuesAsync(token).ConfigureAwait(false);
                    entity.OriginalValues.SetValues(values!);
                    UpdateRowVersion(entity);
                }
            }
        }

        private static void UpdateAuditableEntity(EntityEntry entity, DateTime timestamp, String editor)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (String.IsNullOrEmpty(editor))
            {
                throw new ArgumentNullOrEmptyStringException(editor, nameof(editor));
            }

            EntityState state = entity.State;
            switch (state)
            {
                case EntityState.Added when entity.Entity is ICreationAuditableEntity auditable:
                {
                    UpdateTrackableEntity(entity, timestamp);
                    auditable.CreatorId = editor;
                    return;
                }
                case EntityState.Modified when entity.Entity is IModificationAuditableEntity auditable:
                {
                    UpdateTrackableEntity(entity, timestamp);
                    auditable.UpdaterId = editor;
                    entity.CurrentValues[nameof(IModificationAuditableEntity.UpdaterId)] = editor;

                    if (entity.Entity is not ICreationAuditableEntity)
                    {
                        return;
                    }

                    PreventPropertyOverwrite<String>(entity, nameof(ICreationAuditableEntity.CreatorId));
                    return;
                }
                case EntityState.Deleted when entity.Entity is IDeletionAuditableEntity auditable:
                {
                    UpdateTrackableEntity(entity, timestamp);
                    auditable.DeleterId = editor;
                    entity.CurrentValues[nameof(IDeletionAuditableEntity.DeleterId)] = editor;
                    return;
                }
                default:
                    throw new EnumUndefinedOrNotSupportedException<EntityState>(state, nameof(state), null);
            }
        }

        private static void UpdateAuditableEntity<T>(EntityEntry entity, DateTime timestamp, T editor)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            EntityState state = entity.State;
            switch (state)
            {
                case EntityState.Added when entity.Entity is ICreationAuditableEntity<T> auditable:
                {
                    UpdateTrackableEntity(entity, timestamp);
                    auditable.CreatorId = editor;
                    return;
                }
                case EntityState.Modified when entity.Entity is IModificationAuditableEntity<T> auditable:
                {
                    UpdateTrackableEntity(entity, timestamp);
                    auditable.UpdaterId = editor;
                    entity.CurrentValues[nameof(IModificationAuditableEntity<T>.UpdaterId)] = editor;

                    if (entity.Entity is not ICreationAuditableEntity<T>)
                    {
                        return;
                    }

                    PreventPropertyOverwrite<T>(entity, nameof(ICreationAuditableEntity<T>.CreatorId));
                    return;
                }
                case EntityState.Deleted when entity.Entity is IDeletionAuditableEntity<T> auditable:
                {
                    UpdateTrackableEntity(entity, timestamp);
                    auditable.DeleterId = editor;
                    entity.CurrentValues[nameof(IDeletionAuditableEntity<T>.DeleterId)] = editor;
                    return;
                }
                default:
                    throw new EnumUndefinedOrNotSupportedException<EntityState>(state, nameof(state), null);
            }
        }

        public static void UpdateAuditableEntities(this DbContext context, String editor)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (String.IsNullOrEmpty(editor))
            {
                throw new ArgumentNullOrEmptyStringException(editor, nameof(editor));
            }

            DateTime now = DateTime.UtcNow;
            foreach (EntityEntry entity in context.ChangeTracker.Entries().Where(EntityEntryUtilities.IsChange))
            {
                UpdateAuditableEntity(entity, now, editor);
            }
        }

        public static void UpdateAuditableEntities<T>(this DbContext context, T editor)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            DateTime now = DateTime.UtcNow;
            foreach (EntityEntry entity in context.ChangeTracker.Entries().Where(EntityEntryUtilities.IsChange))
            {
                UpdateAuditableEntity(entity, now, editor);
            }
        }

        private static void UpdateTrackableEntity(EntityEntry entity, DateTime timestamp)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            EntityState state = entity.State;
            switch (state)
            {
                case EntityState.Added when entity.Entity is ICreationTrackableEntity trackable:
                {
                    trackable.CreateTime = timestamp;
                    return;
                }
                case EntityState.Modified when entity.Entity is IModificationTrackableEntity trackable:
                {
                    trackable.UpdateTime = timestamp;
                    entity.CurrentValues[nameof(IModificationTrackableEntity.UpdateTime)] = timestamp;

                    if (entity.Entity is not ICreationTrackableEntity)
                    {
                        return;
                    }

                    PreventPropertyOverwrite<DateTime>(entity, nameof(ICreationTrackableEntity.CreateTime));
                    return;
                }
                case EntityState.Deleted when entity.Entity is IDeletableEntity deletable:
                {
                    entity.State = EntityState.Unchanged;
                    deletable.Delete();
                    entity.CurrentValues[nameof(IDeletableEntity.Active)] = false;

                    if (entity.Entity is not IDeletionTrackableEntity trackable)
                    {
                        return;
                    }

                    trackable.DeleteTime = timestamp;
                    entity.CurrentValues[nameof(IDeletionTrackableEntity.DeleteTime)] = timestamp;
                    return;
                }
                default:
                    throw new EnumUndefinedOrNotSupportedException<EntityState>(state, nameof(state), null);
            }
        }

        public static void UpdateTrackableEntities(this DbContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            DateTime now = DateTime.UtcNow;
            foreach (EntityEntry entity in context.ChangeTracker.Entries().Where(EntityEntryUtilities.IsChange))
            {
                UpdateTrackableEntity(entity, now);
            }
        }

        private static void PreventPropertyOverwrite<T>(EntityEntry entity, String propertyName)
        {
            PropertyEntry entry = entity.Property(propertyName);
            if (!entry.IsModified || !Equals(entity.CurrentValues[propertyName], default(T)))
            {
                return;
            }

            entry.IsModified = false;
        }

        public static Int32 SaveChangesWithTransactionLog(this DbContext context, Func<Boolean, Int32> changes)
        {
            return SaveChangesWithTransactionLog(context, changes, true);
        }

        public static Int32 SaveChangesWithTransactionLog(this DbContext context, Func<Boolean, Int32> changes, Boolean accept)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (changes is null)
            {
                throw new ArgumentNullException(nameof(changes));
            }

            return context.ExecuteInTransaction(() =>
            {
                TransactionLogContext transaction = new TransactionLogContext(context);
                Int32 count = changes.Invoke(accept);

                transaction.AddTransactionLogEntities();
                changes.Invoke(accept);
                return count;
            });
        }

        public static Task<Int32> SaveChangesWithTransactionLogAsync(this DbContext context, Func<Boolean, CancellationToken, Task<Int32>> changes)
        {
            return SaveChangesWithTransactionLogAsync(context, changes, CancellationToken.None);
        }

        public static Task<Int32> SaveChangesWithTransactionLogAsync(this DbContext context, Func<Boolean, CancellationToken, Task<Int32>> changes, CancellationToken token)
        {
            return SaveChangesWithTransactionLogAsync(context, changes, true, token);
        }

        public static Task<Int32> SaveChangesWithTransactionLogAsync(this DbContext context, Func<Boolean, CancellationToken, Task<Int32>> changes, Boolean accept)
        {
            return SaveChangesWithTransactionLogAsync(context, changes, accept, CancellationToken.None);
        }

        public static Task<Int32> SaveChangesWithTransactionLogAsync(this DbContext context, Func<Boolean, CancellationToken, Task<Int32>> changes, Boolean accept, CancellationToken token)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (changes is null)
            {
                throw new ArgumentNullException(nameof(changes));
            }

            return context.ExecuteInTransactionAsync(async () =>
            {
                TransactionLogContext transaction = new TransactionLogContext(context);
                Int32 count = await changes.Invoke(accept, token).ConfigureAwait(false);

                transaction.AddTransactionLogEntities();
                await changes.Invoke(accept, token).ConfigureAwait(false);
                return count;
            });
        }
    }
}