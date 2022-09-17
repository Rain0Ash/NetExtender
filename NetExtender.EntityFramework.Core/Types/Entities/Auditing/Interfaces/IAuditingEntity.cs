// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.EntityFrameworkCore.Entities.Tracking.Interfaces;

namespace NetExtender.EntityFrameworkCore.Entities.Auditing.Interfaces
{
    public interface ICreationAuditableEntity : ICreationAuditableEntity<String?>
    {
    }

    public interface ICreationAuditableEntity<T> : ICreationTrackableEntity
    {
        public T CreatorId { get; set; }
    }

    public interface IModificationAuditableEntity : IModificationAuditableEntity<String?>
    {
    }

    public interface IModificationAuditableEntity<T> : IModificationTrackableEntity
    {
        public T UpdaterId { get; set; }
    }

    public interface IDeletionAuditableEntity : IDeletionAuditableEntity<String?>
    {
    }

    public interface IDeletionAuditableEntity<T> : IDeletionTrackableEntity
    {
        public T DeleterId { get; set; }
    }

    public interface IAuditableEntity : IAuditableEntity<String?>, ICreationAuditableEntity, IModificationAuditableEntity, IDeletionAuditableEntity
    {
    }

    public interface IAuditableEntity<T> : ITrackableEntity, ICreationAuditableEntity<T>, IModificationAuditableEntity<T>, IDeletionAuditableEntity<T>
    {
    }
}