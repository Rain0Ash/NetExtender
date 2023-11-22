// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.IO;

namespace NetExtender.Types.Streams
{
    public class UnclosableStreamWrapper : StreamWrapper
    {
        public UnclosableStreamWrapper(Stream stream)
            : base(stream)
        {
        }

        public override void Close()
        {
        }
    }
}