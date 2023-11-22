// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.CQRS.Interfaces;

namespace NetExtender.CQRS.Events.Interfaces
{
    public interface IEventCQRS : IEntityCQRS
    {
        public Boolean Resolved { get; set; }
    }
}