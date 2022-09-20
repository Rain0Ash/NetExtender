// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Timers.Interfaces
{
    public interface IEnumeratorTimer<out T> : ITimer, IEnumerator<T>
    {
        public event EventHandler Finished;
        public event ItemTickHandler<T?> ItemTick;
        public new Boolean Reset();
    }
}