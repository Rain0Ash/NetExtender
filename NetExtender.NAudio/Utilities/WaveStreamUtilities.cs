// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;

namespace NetExtender.Utilities.NAudio
{
    public static class WaveStreamUtilities
    {
        public static void Skip(this WaveStream stream, TimeSpan skip)
        {
            Int64 position = stream.Position + (Int64) (stream.WaveFormat.AverageBytesPerSecond * skip.TotalSeconds);
            
            if (position > stream.Length)
            {
                stream.Position = stream.Length;
                return;
            }

            stream.Position = position >= 0 ? position : 0;
        }
    }
}