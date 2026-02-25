// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Concurrent.Interfaces
{
    public interface ILockable
    {
        public Boolean IsLock { get; }

        public Boolean Lock();
        public Boolean Lock(TimeSpan timeout);
        public Boolean Unlock();
    }
}