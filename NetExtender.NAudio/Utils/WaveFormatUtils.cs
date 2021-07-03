// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NAudio.Wave;

namespace NetExtender.Utils.NAudio
{
    public static class WaveFormatUtils
    {
        public static WaveFormat Empty { get; } = new WaveFormat();
    }
}