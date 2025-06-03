// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.CQRS.Interfaces;

namespace NetExtender.CQRS.Events.Interfaces
{
    public interface IBeforeSaveEventCQRS : IBeforeEventCQRS
    {
    }
    
    public interface IBeforeEventCQRS : IEventCQRS
    {
    }

    public interface IAfterSaveEventCQRS : IAfterEventCQRS
    {
    }

    public interface IAfterEventCQRS : IEventCQRS
    {
    }
    
    public interface IEventCQRS : IEntityCQRS
    {
        public Boolean Resolved { get; set; }
    }
}