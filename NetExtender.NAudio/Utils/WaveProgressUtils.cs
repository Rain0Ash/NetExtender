// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;
using NetExtender.NAudio.Types.Progress;

namespace NetExtender.Utils.NAudio
{
    public static class WaveProgressUtils
    {
        public static IWaveProvider Progress(this IWaveProvider provider, IProgress<Int64> progress)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (progress is null)
            {
                throw new ArgumentNullException(nameof(progress));
            }

            return Progress(provider, progress.Report);
        }
        
        public static IWaveProvider Progress(this IWaveProvider provider, Action<Int64> callback)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            return new ProgressWaveProvider(provider, callback);
        }
        
        public static WaveStream Progress(this WaveStream stream, IProgress<Int64> progress)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (progress is null)
            {
                throw new ArgumentNullException(nameof(progress));
            }

            return Progress(stream, progress.Report);
        }
        
        public static WaveStream Progress(this WaveStream stream, Action<Int64> callback)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            return new ProgressWaveStream(stream, callback);
        }

        public static WaveStream Progress(this WaveStream stream, IProgress<(Int64, Int64)> progress)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (progress is null)
            {
                throw new ArgumentNullException(nameof(progress));
            }

            return Progress(stream, (position, length) => progress.Report((position, length)));
        }

        public static WaveStream Progress(this WaveStream stream, Action<Int64, Int64> callback)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            return new ProgressWaveStream(stream, callback);
        }
        
        public static WaveStream TimeProgress(this WaveStream stream, IProgress<TimeSpan> progress)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (progress is null)
            {
                throw new ArgumentNullException(nameof(progress));
            }

            return TimeProgress(stream, progress.Report);
        }
        
        public static WaveStream TimeProgress(this WaveStream stream, Action<TimeSpan> callback)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            return new ProgressWaveStream(stream, callback);
        }

        public static WaveStream TimeProgress(this WaveStream stream, IProgress<(TimeSpan, TimeSpan)> progress)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (progress is null)
            {
                throw new ArgumentNullException(nameof(progress));
            }

            return TimeProgress(stream, (current, total) => progress.Report((current, total)));
        }

        public static WaveStream TimeProgress(this WaveStream stream, Action<TimeSpan, TimeSpan> callback)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            return new ProgressWaveStream(stream, callback);
        }
    }
}