// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.EntityFrameworkCore.Entities.Tracking.Interfaces
{
    public interface IDeletableEntity
    {
        public Boolean Active { get; }
        public void Delete();
    }

    public interface ICreationTrackableEntity
    {
        public DateTime CreateTime { get; set; }
    }
    
    public interface IModificationTrackableEntity
    {
        public DateTime? UpdateTime { get; set; }
    }
    
    public interface IDeletionTrackableEntity : IDeletableEntity
    {
        public DateTime? DeleteTime { get; set; }
    }
    
    public interface ITrackableEntity : ICreationTrackableEntity, IModificationTrackableEntity, IDeletionTrackableEntity
    {
    }
}