// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;

namespace NetExtender.Core.Types.Multithreading
{
    public class MutexSlim : SemaphoreSlim
    {
        public Boolean IsFree
        {
            get
            {
                return CurrentCount > 0;
            }
        }
        
        public MutexSlim()
            : base(0, 1)
        {
        }
    }
}