// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using NAudio.Wave;
using NetExtender.NAudio.Types.Providers;

namespace NetExtender.NAudio.Types.Sound
{
    public class AudioSoundFile : AudioSoundFileAbstraction, IDisposable
    {
        private AudioFileReader? _reader;
        protected AudioFileReader Reader
        {
            get
            {
                return _reader ??= new AudioFileReader(Path);
            }
        }

        private ISampleProvider? _provider;
        protected ISampleProvider Provider
        {
            get
            {
                return _provider ??= new AudioSoundSampleProvider(this, Reader);
            }
        }

        public override WaveFormat WaveFormat
        {
            get
            {
                return Provider.WaveFormat;
            }
        }

        public sealed override TimeSpan TotalTime
        {
            get
            {
                if (_reader is not null)
                {
                    return _reader.TotalTime;
                }

                if (!Exists)
                {
                    throw new FileNotFoundException("File not found", Path);
                }

                using AudioFileReader reader = new AudioFileReader(Path);
                return reader.TotalTime;
            }
        }

        public AudioSoundFile(String filename)
            : this(new FileInfo(filename))
        {
        }

        public AudioSoundFile(String filename, TimeSpan stop)
            : this(new FileInfo(filename), stop)
        {
        }

        public AudioSoundFile(String filename, TimeSpan start, TimeSpan stop)
            : this(new FileInfo(filename), start, stop)
        {
        }

        public AudioSoundFile(FileInfo file)
            : this(file, default, default)
        {
        }

        public AudioSoundFile(FileInfo file, TimeSpan stop)
            : this(file, default, stop)
        {
        }

        public AudioSoundFile(FileInfo file, TimeSpan start, TimeSpan stop)
            : base(file, start, stop)
        {
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
            _reader?.Dispose();
            _reader = null;
            (_provider as IDisposable)?.Dispose();
            _provider = null;
        }

        ~AudioSoundFile()
        {
            Dispose(false);
        }
    }
}