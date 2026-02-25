// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.CQRS.Interfaces;

namespace NetExtender.CQRS.Events.Interfaces
{
    public interface IBeforeSaveIdEventCQRS<T> : IBeforeSaveEventCQRS, IBeforeIdEventCQRS<T>
    {
    }

    public interface IBeforeSaveValueEventCQRS<T> : IBeforeSaveEventCQRS, IBeforeValueEventCQRS<T>
    {
    }

    public interface IBeforeSaveEventCQRS : IBeforeEventCQRS
    {
    }

    public interface IBeforeIdEventCQRS<T> : IBeforeEventCQRS, IIdEventCQRS<T>
    {
    }

    public interface IBeforeValueEventCQRS<T> : IBeforeEventCQRS, IValueEventCQRS<T>
    {
    }

    public interface IBeforeEventCQRS : IEventCQRS
    {
    }

    public interface IAfterSaveIdEventCQRS<T> : IAfterSaveEventCQRS, IAfterIdEventCQRS<T>
    {
    }

    public interface IAfterSaveValueEventCQRS<T> : IAfterSaveEventCQRS, IAfterValueEventCQRS<T>
    {
    }

    public interface IAfterSaveEventCQRS : IAfterEventCQRS
    {
    }

    public interface IAfterIdEventCQRS<T> : IAfterEventCQRS, IIdEventCQRS<T>
    {
    }

    public interface IAfterValueEventCQRS<T> : IAfterEventCQRS, IValueEventCQRS<T>
    {
    }

    public interface IAfterEventCQRS : IEventCQRS
    {
    }

    public interface IIdEventCQRS<T> : IEventCQRS, IIdEntityCQRS<T>
    {
    }

    public interface IValueEventCQRS<T> : IEventCQRS, IValueEntityCQRS<T>
    {
    }

    public interface IEventCQRS : IEntityCQRS
    {
        public Boolean Resolved { get; set; }
    }
}