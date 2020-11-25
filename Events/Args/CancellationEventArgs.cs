// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Events.Args
{
    public class CancellationEventArgs : EventArgs
    {
        public Boolean IsCancelled { get; private set; }

        public CancellationEventArgs(Boolean cancel = false)
        {
            IsCancelled = cancel;
        }

        public void Cancel()
        {
            if (IsCancelled)
            {
                return;
            }

            IsCancelled = true;
        }
    }
}