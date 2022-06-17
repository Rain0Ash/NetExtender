// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    public abstract class ActiveBindingToken
    {
        public Int32 Start { get; }
        public Int32 End { get; }
        public abstract ActiveBindingPathTokenId Id { get; }

        protected ActiveBindingToken(Int32 start, Int32 end)
        {
            Start = start;
            End = end;
        }
    }
}
