// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.CQRS.Interfaces;
using NetExtender.Types.Enumerables.Interfaces;

namespace NetExtender.CQRS.Requests.Interfaces
{
    public interface IPaginationRequestCQRS<TResult> : IPaginationRequestCQRS<TResult, IPaginationEnumerable<TResult>>
    {
    }

    public interface IPaginationRequestCQRS<TResult, TCollection> : IRequestCQRS<TCollection> where TCollection : IPaginationEnumerable<TResult>
    {
    }
    
    public interface IRequestCQRS<TResult> : IRequestCQRS, IEntityCQRS<TResult>
    {
    }
    
    public interface IRequestCQRS : IEntityCQRS
    {
    }
}