// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.CQRS.Interfaces;
using NetExtender.Types.Enumerables.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Requests.Interfaces
{
    public interface IBusinessPaginationIdRequestCQRS<T, TResult> : IBusinessPaginationRequestCQRS<TResult>, IBusinessPaginationIdRequestCQRS<T, TResult, IPaginationEnumerable<TResult>>
    {
    }

    public interface IBusinessPaginationRequestCQRS<TResult> : IBusinessPaginationRequestCQRS<TResult, IPaginationEnumerable<TResult>>
    {
    }

    public interface IBusinessPaginationIdRequestCQRS<T, TResult, TCollection> : IBusinessPaginationRequestCQRS<TResult, TCollection>, IBusinessIdRequestCQRS<T, TCollection>, IIdPaginationCQRS<T> where TCollection : IPaginationEnumerable<TResult>
    {
    }

    public interface IBusinessPaginationRequestCQRS<TResult, TCollection> : IBusinessRequestCQRS<TCollection>, IPaginationCQRS where TCollection : IPaginationEnumerable<TResult>
    {
    }

    public interface IPaginationIdRequestCQRS<T, TResult> : IPaginationRequestCQRS<TResult>, IPaginationIdRequestCQRS<T, TResult, IPaginationEnumerable<TResult>>
    {
    }

    public interface IPaginationRequestCQRS<TResult> : IPaginationRequestCQRS<TResult, IPaginationEnumerable<TResult>>
    {
    }

    public interface IPaginationIdRequestCQRS<T, TResult, TCollection> : IPaginationRequestCQRS<TResult, TCollection>, IIdRequestCQRS<T, TCollection>, IIdPaginationCQRS<T> where TCollection : IPaginationEnumerable<TResult>
    {
    }

    public interface IPaginationRequestCQRS<TResult, TCollection> : IRequestCQRS<TCollection>, IPaginationCQRS where TCollection : IPaginationEnumerable<TResult>
    {
    }

    public interface IBusinessIdRequestCQRS<T, TResult> : IBusinessRequestCQRS<TResult>, IIdRequestCQRS<T, BusinessResult<TResult>>
    {
    }

    public interface IBusinessValueRequestCQRS<T, TResult> : IBusinessRequestCQRS<TResult>, IValueRequestCQRS<T, BusinessResult<TResult>>
    {
    }

    public interface IBusinessRequestCQRS<TResult> : IRequestCQRS<BusinessResult<TResult>>
    {
    }

    public interface IBusinessIdRequestCQRS<T> : IBusinessRequestCQRS, IIdRequestCQRS<T, BusinessResult>
    {
    }

    public interface IBusinessValueRequestCQRS<T> : IBusinessRequestCQRS, IValueRequestCQRS<T, BusinessResult>
    {
    }

    public interface IBusinessRequestCQRS : IRequestCQRS<BusinessResult>
    {
    }

    public interface IIdRequestCQRS<T, TResult> : IIdRequestCQRS<T>, IRequestCQRS<TResult>, IIdEntityCQRS<T, TResult>
    {
    }

    public interface IValueRequestCQRS<T, TResult> : IValueRequestCQRS<T>, IRequestCQRS<TResult>, IValueEntityCQRS<T, TResult>
    {
    }

    public interface IRequestCQRS<TResult> : IRequestCQRS, IEntityCQRS<TResult>
    {
    }

    public interface IIdRequestCQRS<T> : IRequestCQRS, IIdEntityCQRS<T>
    {
    }

    public interface IValueRequestCQRS<T> : IRequestCQRS, IValueEntityCQRS<T>
    {
    }

    public interface IRequestCQRS : IEntityCQRS
    {
    }
}