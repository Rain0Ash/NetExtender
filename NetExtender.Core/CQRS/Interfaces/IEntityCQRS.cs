// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Entities.Interfaces;

namespace NetExtender.CQRS.Interfaces
{
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
    
    public interface IEntityCQRS : IEntity
    {
    }
}