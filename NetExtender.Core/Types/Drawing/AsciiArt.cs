// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.IO;
using NetExtender.Utils.IO;
using NetExtender.Utils.Types;

namespace NetExtender.Types.Drawing
{
    public class AsciiArt : IConsoleMessage
    {
        public static implicit operator String(AsciiArt art)
        {
            return art.ToString();
        }
        
        public Size Size { get; }
        
        public Boolean VTCode { get; init; }

        private ImmutableArray<String> Art { get; }

        public AsciiArt(Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new ArgumentException(@"Stream cannot be read", nameof(stream));
            }

            String[] art = stream.ReadAsLines();
            
            Size = art.GetMatrixSize();
            Art = art.ToImmutableArray();
        }
        
        public AsciiArt(IEnumerable<String> art)
        {
            if (art is null)
            {
                throw new ArgumentNullException(nameof(art));
            }

            ImmutableArray<String> array = art.ToImmutableArray();
            
            Size = array.GetMatrixSize();
            Art = array;
        }

        public String GetConsoleText(IFormatProvider? provider = null)
        {
            return this;
        }
        
        public override String ToString()
        {
            return Art.Join();
        }
    }
}