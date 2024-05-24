// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Flac;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NetExtender.NAudio.Types.Providers;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.NAudio;
using NetExtender.Utilities.Types;

namespace NetExtender.NAudio.Types.Sound
{
    public class AudioSoundStream : AudioSoundSampleProviderAbstraction, IDisposable
    {
        protected WaveStream Stream { get; }
        protected ISampleProvider Provider { get; }

        public override WaveFormat WaveFormat
        {
            get
            {
                return Provider.WaveFormat;
            }
        }

        public override Int64? Size
        {
            get
            {
                try
                {
                    return Stream.Length;
                }
                catch (ObjectDisposedException)
                {
                    throw;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        protected sealed override Info Information { get; }

        public sealed override Boolean IsVirtual
        {
            get
            {
                return false;
            }
        }

        public sealed override TimeSpan TotalTime
        {
            get
            {
                return Stream.TotalTime;
            }
        }

        public AudioSoundStream(Stream stream, TimeSpan start, TimeSpan stop)
            : this(stream, start, stop, AudioSoundType.Unknown)
        {
        }

        public AudioSoundStream(Stream stream, TimeSpan start, TimeSpan stop, AudioSoundType type)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (Info.Validate(start, stop) is { } exception)
            {
                throw exception;
            }

            Stream = stream as WaveStream ?? type switch
            {
                AudioSoundType.Unknown => ToWaveStream(stream),
                AudioSoundType.Wave => ToWaveStream(stream),
                AudioSoundType.Mp3 => new Mp3FileReader(stream),
                AudioSoundType.Aiff => new AiffFileReader(stream),
                AudioSoundType.Flac => new FlacReader(stream),
                _ => throw new EnumUndefinedOrNotSupportedException<AudioSoundType>(type, nameof(type), null)
            };

            Provider = new AudioSoundSampleProvider(this, new WaveToSampleProvider(Stream));
            Information = new Info(start, stop, TotalTime);
        }

        private static WaveStream ToWaveStream(Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            WaveStream wave = new WaveFileReader(stream);
            if (wave.WaveFormat.Encoding == WaveFormatEncoding.Pcm || wave.WaveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
            {
                return wave;
            }

            return new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(wave));
        }

        public override Boolean TryRead(Span<Byte> destination, out Int32 written)
        {
            if (destination.Length < Stream.Length || !Stream.TryPosition(out Int64 position))
            {
                written = 0;
                return false;
            }

            Stream.ResetPosition();
            written = Stream.Read(destination);
            Stream.Position = position;
            return true;
        }

        public override Byte[] Read()
        {
            Int64 position = Stream.Position;

            try
            {
                Stream.ResetPosition();
                Byte[] data = Stream.ReadAsByteArray();
                return data;
            }
            finally
            {
                Stream.Position = position;
            }
        }

        public override async Task<Byte[]> ReadAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            Int64 position = Stream.Position;

            try
            {
                Stream.ResetPosition();
                Byte[] data = await Stream.ReadAsByteArrayAsync(token).ConfigureAwait(false);
                return data;
            }
            finally
            {
                Stream.Position = position;
            }
        }

        public override Int32 Read(Single[] buffer, Int32 offset, Int32 count)
        {
            return Provider.Read(buffer, offset, count);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            Stream.Dispose();
        }

        ~AudioSoundStream()
        {
            Dispose(false);
        }
    }
}