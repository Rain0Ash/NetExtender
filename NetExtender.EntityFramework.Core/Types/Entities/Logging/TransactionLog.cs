// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace NetExtender.EntityFrameworkCore.Entities.Logging
{
    public enum TransactionLogOperationType
    {
        Unknown,
        Insert,
        Update,
        Delete
    }
    
    public class TransactionLog
    {
        /// <summary>
        /// Auto incremented primary key.
        /// </summary>
        public Int64 Id { get; init; }

        /// <summary>
        /// An ID of all changes that captured during single <see cref="DbContext.SaveChanges()"/> call.
        /// </summary>
        public Guid Guid { get; init; }

        /// <summary>
        /// UTC timestamp of <see cref="DbContext.SaveChanges()"/> call.
        /// </summary>
        public DateTime CreateTime { get; init; }

        /// <summary>
        /// Transaction operation type.
        /// </summary>
        public TransactionLogOperationType Operation { get; init; }

        /// <summary>
        /// Schema for captured entity.
        /// </summary>
        public String? Schema { get; init; }

        /// <summary>
        /// Table for captured entity.
        /// </summary>
        public String? TableName { get; init; }

        /// <summary>
        /// Assembly qualified type name of captured entity.
        /// </summary>
        public String? EntityType { get; init; }

        public Type? Type
        {
            get
            {
                return !String.IsNullOrEmpty(EntityType) ? Type.GetType(EntityType) : null;
            }
        }

        /// <summary>
        /// The captured entity serialized to JSON.
        /// </summary>
        public String? EntityJson { get; init; }

        private Object? _entity;
        public Object? Entity
        {
            get
            {
                if (_entity is not null)
                {
                    return _entity;
                }

                String? json = EntityJson;

                if (json is null)
                {
                    return null;
                }
                
                Type? type = Type;
                return _entity ??= type is not null ? JsonConvert.DeserializeObject(json, type) : EntityJson;
            }
        }

        /// <summary>
        /// Get strongly typed entity from transaction log.
        /// Can be null if TEntity and type from <see cref="EntityType"/> property are incompatible.
        /// All navigation properties and collections will be empty.
        /// </summary>
        public TEntity? AsEntity<TEntity>() where TEntity : class
        {
            return Entity as TEntity;
        }
    }
}