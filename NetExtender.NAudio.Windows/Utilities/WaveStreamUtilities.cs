// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;

namespace NetExtender.Utilities.NAudio
{
    public static class WaveStreamUtilities
    {
        public static BlockAlignReductionStream ToBlockAlignReductionStream(this WaveStream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            
            return new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(stream));
        }
    }
}