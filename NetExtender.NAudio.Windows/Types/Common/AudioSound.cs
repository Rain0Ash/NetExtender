// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using NAudio.Wave;

namespace NetExtender.NAudio.Types.Common
{
    public class AudioSound : AudioSoundAbstraction
    {
        public override TimeSpan TotalTime
        {
            get
            {
                if (!Exists)
                {
                    throw new FileNotFoundException("File not found", Path);
                }
                
                using AudioFileReader reader = new AudioFileReader(Path);
                return reader.TotalTime;
            }
        }

        public AudioSound(String filename)
            : this(new FileInfo(filename))
        {
        }

        public AudioSound(String filename, TimeSpan stop)
            : this(new FileInfo(filename), stop)
        {
        }

        public AudioSound(String filename, TimeSpan start, TimeSpan stop)
            : this(new FileInfo(filename), start, stop)
        {
        }

        public AudioSound(FileInfo file)
            : this(file, default, default)
        {
        }

        public AudioSound(FileInfo file, TimeSpan stop)
            : this(file, default, stop)
        {
        }

        public AudioSound(FileInfo file, TimeSpan start, TimeSpan stop)
            : base(file, start, stop)
        {
        }
    }
}