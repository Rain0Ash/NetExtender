// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.CQRS.Interfaces;
using NetExtender.Types.Monads.Result;

namespace NetExtender.CQRS.Commands.Interfaces
{
    public interface IBusinessCommandCQRS<TResult> : ICommandCQRS<BusinessResult<TResult>>
    {
    }
    
    public interface IBusinessCommandCQRS : ICommandCQRS<BusinessResult>
    {
    }
    
    public interface ICommandCQRS<TResult> : ICommandCQRS, IEntityCQRS<TResult>
    {
    }
    
    public interface ICommandCQRS : IEntityCQRS
    {
    }
}