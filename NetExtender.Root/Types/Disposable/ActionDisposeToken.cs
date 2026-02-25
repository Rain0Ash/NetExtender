// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Disposable
{
    public class ActionDisposeToken : DisposableToken
    {
        protected Action Action { get; }

        public ActionDisposeToken(Action action)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        protected override Boolean Dispose(Boolean dispose)
        {
            if (!dispose)
            {
                return false;
            }

            Action.Invoke();
            return true;
        }
    }
}