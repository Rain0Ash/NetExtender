// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using NetExtender.Types.TextWriters;

namespace NetExtender.Windows.Services.Types.TextWriters
{
    internal class ConsoleOutLockedFilterTextWriter : FilterTextWriterWrapper
    {
        protected TextWriter Original { get; }

        public ConsoleOutLockedFilterTextWriter()
            : base(new LockedTextWriterWrapper(Console.Out))
        {
            Original = Console.Out;
            Console.SetOut(this);
        }

        protected override void Dispose(Boolean disposing)
        {
            Console.SetOut(Original);
            base.Dispose(disposing);
        }
    }
}