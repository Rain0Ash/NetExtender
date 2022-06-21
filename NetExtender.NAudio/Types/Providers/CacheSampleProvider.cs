// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.IO;
using NAudio.Wave;
using NetExtender.Utilities.Types;

namespace NetExtender.NAudio.Types.Providers
{
    public sealed class CacheSampleProvider : ISampleProvider, INotifyPropertyChanged
    {
        public ISampleProvider Source { get; }
        private MemoryStream Cache { get; }
        
        public WaveFormat WaveFormat
        {
            get
            {
                return Source.WaveFormat;
            }
        }
        
        public Int32 Count { get; private set; }

        public event EventHandler<Int32>? Repeat;
        
        private event PropertyChangedEventHandler? PropertyChanged;
        event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                PropertyChanged += value;
            }
            remove
            {
                PropertyChanged -= value;
            }
        }

        public CacheSampleProvider(ISampleProvider source)
        {
            Source = source;
            Cache = new MemoryStream();
        }

        public Int32 Read(Single[] buffer, Int32 offset, Int32 count)
        {
            Span<Single> span = buffer.AsSpan(offset, count);

            Int32 read;
            if (Count <= 0)
            {
                read = Source.Read(buffer, offset, count);
                Cache.Write(span.Slice(0, read).As<Single, Byte>());
            }
            else
            {
                read = Cache.Read(span.As<Single, Byte>()) / sizeof(Single);
            }

            if (read >= count)
            {
                return read;
            }

            Count++;
            Repeat?.Invoke(this, Count);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
            Cache.Position = 0;
            read += Read(buffer, offset + read, count - read);
            return read;
        }
    }
}