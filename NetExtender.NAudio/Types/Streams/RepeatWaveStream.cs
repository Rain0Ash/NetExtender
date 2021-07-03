// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;

namespace NetExtender.NAudio.Types.Streams
{
    /// <summary>
    /// Stream for looping playback
    /// </summary>
    public class RepeatWaveStream : InternalWaveStream
    {
        /// <summary>
        /// Use this to enable or disable looping
        /// </summary>
        public Boolean Repeat { get; set; } = true;

        /// <summary>
        /// Creates a new <see cref="RepeatWaveStream"/>
        /// </summary>
        /// <param name="stream">The stream to read from.<para>Note: the Read method of this stream should return 0 when it reaches the end
        /// or else we will not loop to the start again.</para></param>
        public RepeatWaveStream(WaveStream stream)
            : base(stream ?? throw new ArgumentNullException(nameof(stream)))
        {
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            Int32 total = 0;

            while (total < count)
            {
                Int32 read = Stream.Read(buffer, offset + total, count - total);
                if (read <= 0 || Stream.Position > Stream.Length)
                {
                    if (!Repeat || Stream.Position == 0)
                    {
                        break;
                    }

                    Stream.Position = 0;
                }

                total += read;
            }

            return total;
        }
    }
}