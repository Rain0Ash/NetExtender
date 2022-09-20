// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Tasks.Interfaces
{
    public interface IStartable
    {
        public Boolean IsStarted { get; }

        public void Start();

        public void Stop();
    }
}