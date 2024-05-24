// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.NAudio.Types.Sound.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.NAudio.Types.Sound
{
    public abstract class AudioSoundFileAbstraction : AudioSoundSampleProviderAbstraction, IAudioSoundFile
    {
        protected FileInfo File { get; }

        public override Int64? Size
        {
            get
            {
                return File.Length;
            }
        }

        public String Path
        {
            get
            {
                return File.FullName;
            }
        }

        public String Extension
        {
            get
            {
                return File.Extension;
            }
        }

        public Boolean Exists
        {
            get
            {
                return File.Exists;
            }
        }

        public override Boolean IsVirtual
        {
            get
            {
                return false;
            }
        }

        protected AudioSoundFileAbstraction(FileInfo file)
        {
            File = file ?? throw new ArgumentNullException(nameof(file));
        }

        public override Boolean TryRead(Span<Byte> destination, out Int32 written)
        {
            if (destination.Length == 0)
            {
                written = 0;
                return false;
            }

            try
            {
                using FileStream stream = File.OpenRead();
                written = stream.Read(destination);
                return written > 0;
            }
            catch (Exception)
            {
                written = 0;
                return false;
            }
        }

        public override Byte[] Read()
        {
            using FileStream stream = File.OpenRead();
            return stream.ReadAsByteArray();
        }

        public override async Task<Byte[]> ReadAsync(CancellationToken token)
        {
            await using FileStream stream = File.OpenRead();
            return await stream.ReadAsByteArrayAsync(token).ConfigureAwait(false);
        }
    }
}