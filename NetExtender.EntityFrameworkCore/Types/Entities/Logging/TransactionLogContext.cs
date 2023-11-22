// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NetExtender.EntityFrameworkCore.Entities.Logging.Interfaces;
using NetExtender.Utilities.EntityFrameworkCore;
using Newtonsoft.Json;

namespace NetExtender.EntityFrameworkCore.Entities.Logging
{
    internal sealed class TransactionLogContext : IEnumerable<TransactionLog>
    {
        private DbContext Context { get; }
        public Guid Guid { get; } = Guid.NewGuid();
        public DateTime CreateTime { get; } = DateTime.UtcNow;

        private List<EntityEntry> Insert { get; } = new List<EntityEntry>();
        private List<EntityEntry> Update { get; } = new List<EntityEntry>();
        private List<TransactionLog> Delete { get; } = new List<TransactionLog>();

        public TransactionLogContext(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            StoreChangedEntries();
        }

        private void StoreChangedEntries()
        {
            foreach (EntityEntry entry in Context.ChangeTracker.Entries().Where(EntityEntryUtilities.IsChange))
            {
                if (entry.Entity is not ITransactionLoggableEntity)
                {
                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        Insert.Add(entry);
                        continue;
                    case EntityState.Modified:
                        Update.Add(entry);
                        continue;
                    case EntityState.Deleted:
                        Delete.Add(CreateTransactionLog(entry, TransactionLogOperationType.Delete));
                        continue;
                    default:
                        continue;
                }
            }
        }

        private TransactionLog CreateTransactionLog(EntityEntry entity, TransactionLogOperationType type)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Object? value = type != TransactionLogOperationType.Delete ? entity.CurrentValues.ToObject() : entity.Metadata.FindPrimaryKey()?.Properties
                .Select(property => entity.Property(property.Name))
                .ToDictionary(property => property.Metadata.Name, property => property.CurrentValue);

            return new TransactionLog
            {
                Guid = Guid,
                CreateTime = CreateTime,
                Operation = type,
                Schema = entity.Metadata.GetSchema(),
                TableName = entity.Metadata.GetTableName(),
                EntityType = entity.Entity.GetType().AssemblyQualifiedName,
                EntityJson = value is not null ? JsonConvert.SerializeObject(value, Formatting.Indented) : null
            };
        }

        public void AddTransactionLogEntities()
        {
            foreach (TransactionLog log in this)
            {
                Context.Entry(log).State = EntityState.Added;
            }
        }

        public IEnumerator<TransactionLog> GetEnumerator()
        {
            foreach (EntityEntry entity in Insert)
            {
                yield return CreateTransactionLog(entity, TransactionLogOperationType.Insert);
            }

            foreach (EntityEntry entity in Update)
            {
                yield return CreateTransactionLog(entity, TransactionLogOperationType.Update);
            }

            foreach (TransactionLog entity in Delete)
            {
                yield return entity;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}