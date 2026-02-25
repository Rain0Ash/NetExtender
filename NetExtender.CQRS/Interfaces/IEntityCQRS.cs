// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.CQRS.Events.Interfaces;
using NetExtender.Types.Entities.Interfaces;

namespace NetExtender.CQRS.Interfaces
{
    public interface IIdPaginationCQRS<T> : IPaginationCQRS, IIdEntityCQRS<T>
    {
    }

    public interface IPaginationCQRS : IEntityCQRS
    {
        public UInt32 Page { get; init; }
        public UInt32 PageSize { get; init; }
        public UInt32 RequestLimit { get; init; }
    }

    public interface IIdEntityCQRS<T, TResult> : IIdEntityCQRS<T>, IEntityCQRS<TResult>
    {
    }

    public interface IValueEntityCQRS<T, TResult> : IValueEntityCQRS<T>, IEntityCQRS<TResult>
    {
    }

    public interface IEntityCQRS<TResult> : IEntityCQRS
    {
        public Type Type
        {
            get
            {
                return typeof(TResult);
            }
        }
    }

    public interface IIdEntityCQRS<T> : IEntityCQRS, IEntityId<T>
    {
        public new T Id { get; init; }
    }

    public interface IValueEntityCQRS<T> : IEntityCQRS, IEntityValue<T>
    {
        public new T Value { get; init; }
    }

    public interface IEntityCQRS : IEventsEntity<IEventCQRS, IEntityEventCollection>
    {
        public Boolean IsEmpty { get; }
    }
}