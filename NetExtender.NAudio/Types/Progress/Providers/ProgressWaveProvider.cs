// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;

namespace NetExtender.NAudio.Types.Progress
{
    public class ProgressWaveProvider : IWaveProvider
    {
        protected IWaveProvider Provider { get; }
        protected Action<Int64> Callback { get; }
        
        public WaveFormat WaveFormat
        {
            get
            {
                return Provider.WaveFormat;
            }
        }
        
        protected virtual Int64 Total { get; set; }
        
        public ProgressWaveProvider(IWaveProvider provider, Action<Int64> callback)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }
        
        public Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            Int32 read = Provider.Read(buffer, offset, count);

            if (read <= 0)
            {
                return read;
            }

            Total += read;
            Callback.Invoke(Total);

            return read;
        }
    }
}