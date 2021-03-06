// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Utils.Types;

namespace NetExtender.Times.Timers.Interfaces
{
    public interface IEnumeratorTimer<out T> : ITimer, IEnumerator<T>
    {
        public event EmptyHandler Finished;
        public event ItemTickHandler<T> ItemTick;
        public Boolean Reset();
    }
}