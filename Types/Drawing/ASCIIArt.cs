// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.IO;
using JetBrains.Annotations;
using NetExtender.Utils.IO;
using NetExtender.Utils.Types;

namespace NetExtender.Types.Drawing
{
    public class ASCIIArt : IConsoleMessage
    {
        public static implicit operator String(ASCIIArt art)
        {
            return art.ToString();
        }
        
        public Size Size { get; }
        
        public Boolean VTCode { get; init; }

        private ImmutableArray<String> Art { get; }

        public ASCIIArt([NotNull] Stream stream)
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
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public ASCIIArt([NotNull] IEnumerable<String> art)
        {
            if (art is null)
            {
                throw new ArgumentNullException(nameof(art));
            }
            
            Size = art.GetMatrixSize();
            Art = art.ToImmutableArray();
        }

        public String GetConsoleText(IFormatProvider provider = null)
        {
            return this;
        }
        
        public override String ToString()
        {
            return Art.Join();
        }
    }
}