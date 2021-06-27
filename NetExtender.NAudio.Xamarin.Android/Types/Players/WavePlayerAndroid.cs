using System;
using System.Threading;
using Android.Media;
using NAudio.Wave;

namespace NetExtender.NAudio.Xamarin.Android
{
    /// <summary>
    ///     Represents an Android wave player implemented using <see cref="AudioTrack" />.
    /// </summary>
    public class WavePlayerAndroid : IWavePlayer
    {
        private AudioTrack? AudioTrack { get; set; }
        
        private Thread? Thread { get; set; }

        private IWaveProvider? WaveProvider { get; set; }
        
        /// <summary>
        ///     Occurs when the player has stopped.
        /// </summary>
        public event EventHandler<StoppedEventArgs> PlaybackStopped = null!;
        
        /// <summary>
        ///     Gets or sets the desired latency in milliseconds.
        /// </summary>
        public Int32 DesiredLatency { get; set; } = 300;

        /// <summary>
        ///     Gets or sets the number of buffers to use.
        /// </summary>
        public Int32 NumberOfBuffers { get; set; } = 2;

        /// <summary>
        ///     Gets or sets the usage.
        /// </summary>
        public AudioUsageKind Usage { get; set; } = AudioUsageKind.Media;

        /// <summary>
        ///     Gets or sets the content type.
        /// </summary>
        public AudioContentType ContentType { get; set; } = AudioContentType.Music;

        /// <summary>
        ///     Gets or sets the performance mode.
        /// </summary>
        public AudioTrackPerformanceMode PerformanceMode { get; set; } = AudioTrackPerformanceMode.None;

        /// <summary>
        ///     Gets the current playback state.
        /// </summary>
        public PlaybackState PlaybackState { get; private set; } = PlaybackState.Stopped;
        
        private Single _volume = 1;
        
        /// <summary>
        ///     Gets or sets the volume in % (0.0 to 1.0).
        /// </summary>
        public Single Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                _volume = value < 0 ? 0 : value > 1 ? 1 : value;
                AudioTrack?.SetVolume(_volume);
            }
        }

        /// <summary>
        ///     Initializes the player with the specified wave provider.
        /// </summary>
        /// <param name="provider">The wave provider to be played.</param>
        public void Init(IWaveProvider provider)
        {
            ThrowIfDisposed();

            if (WaveProvider != null)
            {
                throw new InvalidOperationException("This wave player instance has already been initialized");
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (provider.WaveFormat.Encoding != WaveFormatEncoding.Pcm && provider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
            {
                throw new ArgumentException("Input wave provider must be PCM or IEEE float", nameof(provider));
            }

            Encoding encoding = provider.WaveFormat.BitsPerSample switch
            {
                8 => Encoding.Pcm8bit,
                16 => Encoding.Pcm16bit,
                32 => Encoding.PcmFloat,
                _ => throw new ArgumentException("Input wave provider must be 8-bit, 16-bit, or 32-bit", nameof(provider))
            };

            WaveProvider = provider;

            ChannelOut channel = WaveProvider.WaveFormat.Channels switch
            {
                1 => ChannelOut.Mono,
                2 => ChannelOut.Stereo,
                _ => throw new ArgumentException("Input wave provider must be mono or stereo", nameof(provider))
            };

            Int32 minsize = AudioTrack.GetMinBufferSize(WaveProvider.WaveFormat.SampleRate, channel, encoding);
            Int32 size = WaveProvider.WaveFormat.ConvertLatencyToByteSize(DesiredLatency);
            
            if (size < minsize)
            {
                size = minsize;
            }

            AudioTrack = new AudioTrack.Builder()
                .SetAudioAttributes(new AudioAttributes.Builder()
                    .SetUsage(Usage)?
                    .SetContentType(ContentType)?
                    .Build() ?? throw new InvalidOperationException())
                .SetAudioFormat(new AudioFormat.Builder()
                    .SetEncoding(encoding)?
                    .SetSampleRate(WaveProvider.WaveFormat.SampleRate)?
                    .SetChannelMask(channel)
                    .Build() ?? throw new InvalidOperationException())
                .SetBufferSizeInBytes(size)
                .SetTransferMode(AudioTrackMode.Stream)
                .SetPerformanceMode(PerformanceMode)
                .Build();
            
            AudioTrack.SetVolume(Volume);
        }

        /// <summary>
        ///     Starts the player.
        /// </summary>
        public void Play()
        {
            ThrowIfDisposed();

            ThrowIfNotInitialized();
            if (PlaybackState == PlaybackState.Playing)
            {
                return;
            }

            PlaybackState = PlaybackState.Playing;
            AudioTrack?.Play();
            
            if (Thread is not null && Thread.IsAlive)
            {
                return;
            }

            Thread = new Thread(PlaybackThread) {Priority = ThreadPriority.Highest};
            Thread.Start();
        }

        /// <summary>
        ///     Pauses the player.
        /// </summary>
        public void Pause()
        {
            ThrowIfDisposed();

            ThrowIfNotInitialized();
            if (PlaybackState == PlaybackState.Stopped || PlaybackState == PlaybackState.Paused)
            {
                return;
            }

            PlaybackState = PlaybackState.Paused;
            AudioTrack?.Pause();
        }

        /// <summary>
        ///     Stops the player.
        /// </summary>
        public void Stop()
        {
            //Make sure we haven't been disposed
            ThrowIfDisposed();

            //Check the player state
            ThrowIfNotInitialized();
            if (PlaybackState == PlaybackState.Stopped)
            {
                return;
            }

            //Stop the wave player
            PlaybackState = PlaybackState.Stopped;
            AudioTrack?.Stop();
            Thread?.Join();
        }

        private Boolean _disposed;

        /// <summary>
        ///     Raises the <see cref="PlaybackStopped" /> event with the provided arguments.
        /// </summary>
        /// <param name="exception">An optional exception that occured.</param>
        protected virtual void OnPlaybackStopped(Java.Lang.Exception? exception = null)
        {
            PlaybackStopped?.Invoke(this, new StoppedEventArgs(exception));
        }

        private void PlaybackThread()
        {
            try
            {
                PlaybackLogic();
            }
            catch (Java.Lang.Exception exception)
            {
                PlaybackState = PlaybackState.Stopped;
                OnPlaybackStopped(exception);
                return;
            }
            
            PlaybackState = PlaybackState.Stopped;
            OnPlaybackStopped();
        }

        private void PlaybackLogic()
        {
            Int32 size = (AudioTrack!.BufferSizeInFrames + NumberOfBuffers - 1) / NumberOfBuffers * WaveProvider!.WaveFormat.BlockAlign;
            size = (size + 3) & ~3;
            WaveBuffer buffer = new WaveBuffer(size) {ByteBufferCount = size};

            while (PlaybackState != PlaybackState.Stopped)
            {
                if (PlaybackState != PlaybackState.Playing)
                {
                    Thread.Sleep(10);
                    continue;
                }

                Int32 read = WaveProvider.Read(buffer.ByteBuffer, 0, buffer.ByteBufferCount);
                if (read > 0)
                {
                    if (read < buffer.ByteBufferCount)
                    {
                        buffer.ByteBufferCount = (read + 3) & ~3;
                        Array.Clear(buffer.ByteBuffer, read, buffer.ByteBufferCount - read);
                    }

                    WriteBuffer(buffer);
                    continue;
                }

                AudioTrack.Stop();
                break;
            }

            AudioTrack.Flush();
        }

        private void WriteBuffer(IWaveBuffer buffer)
        {
            switch (WaveProvider?.WaveFormat.Encoding)
            {
                case WaveFormatEncoding.Pcm:
                    AudioTrack?.Write(buffer.ByteBuffer, 0, buffer.ByteBufferCount);
                    return;
                case WaveFormatEncoding.IeeeFloat:
                {
                    Single[] array = new Single[buffer.FloatBufferCount];
                    
                    for (Int32 i = 0; i < buffer.FloatBufferCount; i++)
                    {
                        array[i] = buffer.FloatBuffer[i];
                    }

                    AudioTrack?.Write(array, 0, array.Length, WriteMode.Blocking);
                    break;
                }
                default:
                    break;
            }
        }

        /// <summary>
        ///     Releases all resources used by the current instance of the <see cref="WavePlayerAndroid" /> class.
        /// </summary>
        public void Dispose()
        {
            //Dispose of this object
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        /// <summary>
        ///     Releases the unmanaged resources used by the <see cref="WavePlayerAndroid" />, and optionally releases the managed
        ///     resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(Boolean disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (PlaybackState != PlaybackState.Stopped)
                {
                    Stop();
                }

                AudioTrack?.Release();
                AudioTrack?.Dispose();
            }

            _disposed = true;
        }
        
        private void ThrowIfNotInitialized()
        {
            if (WaveProvider is null)
            {
                throw new InvalidOperationException("This wave player instance has not been initialized");
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        /// <summary>
        ///     Releases the unmanaged resources used by the current instance of the <see cref="WavePlayerAndroid" /> class.
        /// </summary>
        ~WavePlayerAndroid()
        {
            Dispose(false);
        }
    }
}