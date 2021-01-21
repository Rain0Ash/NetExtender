// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Strings.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NetExtender.Types.Strings
{
    public abstract class MultiString : ReactiveObject, IString, IDisposable
    {
        public static implicit operator String(MultiString str)
        {
            return str.ToString();
        }

        [Reactive]
        public String Text { get; protected set; }

        public override String ToString()
        {
            return Text;
        }

        public abstract void Dispose();
    }
}