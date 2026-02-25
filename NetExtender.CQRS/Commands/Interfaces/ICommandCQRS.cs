// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.CQRS.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Commands.Interfaces
{
    public interface IBusinessIdCommandCQRS<T, TResult> : IBusinessCommandCQRS<TResult>, IIdCommandCQRS<T, BusinessResult<TResult>>
    {
    }

    public interface IBusinessValueCommandCQRS<T, TResult> : IBusinessCommandCQRS<TResult>, IValueCommandCQRS<T, BusinessResult<TResult>>
    {
    }

    public interface IBusinessCommandCQRS<TResult> : ICommandCQRS<BusinessResult<TResult>>
    {
    }

    public interface IBusinessIdCommandCQRS<T> : IBusinessCommandCQRS, IIdCommandCQRS<T, BusinessResult>
    {
    }

    public interface IBusinessValueCommandCQRS<T> : IBusinessCommandCQRS, IValueCommandCQRS<T, BusinessResult>
    {
    }

    public interface IBusinessCommandCQRS : ICommandCQRS<BusinessResult>
    {
    }

    public interface IIdCommandCQRS<T, TResult> : IIdCommandCQRS<T>, ICommandCQRS<TResult>, IIdEntityCQRS<T, TResult>
    {
    }

    public interface IValueCommandCQRS<T, TResult> : IValueCommandCQRS<T>, ICommandCQRS<TResult>, IValueEntityCQRS<T, TResult>
    {
    }

    public interface ICommandCQRS<TResult> : ICommandCQRS, IEntityCQRS<TResult>
    {
    }

    public interface IIdCommandCQRS<T> : ICommandCQRS, IIdEntityCQRS<T>
    {
    }

    public interface IValueCommandCQRS<T> : ICommandCQRS, IValueEntityCQRS<T>
    {
    }

    public interface ICommandCQRS : IEntityCQRS
    {
    }
}