using System;
using NetExtender.NAudio.Types.Sound.Interfaces;

namespace NetExtender.NAudio.Types.Sound
{
    public abstract class AudioSoundInformation : IAudioSoundInformation
    {
        public readonly struct Info
        {
            public TimeSpan Start { get; }
            public TimeSpan Stop { get; }
            
            public TimeSpan Length
            {
                get
                {
                    return Stop - Start;
                }
            }
            
            public TimeSpan Total { get; }
            
            public Info(TimeSpan start, TimeSpan stop)
                : this(start, stop, null)
            {
            }
            
            public Info(TimeSpan start, TimeSpan stop, TimeSpan? total)
            {
                if (Validate(start, stop, total) is { } exception)
                {
                    throw exception;
                }

                Start = start;
                Total = total ??= stop - start;
                Stop = TimeSpan.FromTicks(Math.Clamp(stop == default ? total.Value.Ticks : stop.Ticks, start.Ticks, total.Value.Ticks));
            }

            public static Exception? Validate(TimeSpan start, TimeSpan stop)
            {
                return Validate(start, stop, null);
            }

            public static Exception? Validate(TimeSpan start, TimeSpan stop, TimeSpan? total)
            {
                if (start < default(TimeSpan))
                {
                    return new ArgumentOutOfRangeException(nameof(start), start, null);
                }

                if (stop < default(TimeSpan))
                {
                    return new ArgumentOutOfRangeException(nameof(stop), stop, null);
                }

                if (stop != default && stop < start)
                {
                    return new ArgumentOutOfRangeException(nameof(stop), stop, "Stop must be greater than start");
                }

                if (start > total)
                {
                    return new ArgumentOutOfRangeException(nameof(start), start, "Start must be less than total time");
                }

                return null;
            }
        }
        
        public readonly struct TotalInfo
        {
            private IAudioSoundInformation? Information { get; }
            
            public TimeSpan Start
            {
                get
                {
                    if (Information is not { } information)
                    {
                        return TimeSpan.Zero;
                    }
                    
                    return information.Start + information.Active.Start;
                }
            }

            public TimeSpan TotalStopActive
            {
                get
                {
                    if (Information is not { } information)
                    {
                        return TimeSpan.Zero;
                    }
                    
                    return information.Stop + information.Active.Stop;
                }
            }

            public TotalInfo(IAudioSoundInformation information)
            {
                Information = information ?? throw new ArgumentNullException(nameof(information));
            }
        }

        public abstract Int64? Size { get; }
        public abstract Boolean IsVirtual { get; }
        protected abstract Info Information { get; }

        public TimeSpan Start
        {
            get
            {
                return Information.Start;
            }
        }

        public TimeSpan Stop
        {
            get
            {
                return Information.Stop;
            }
        }
        
        public Info Active { get; init; }
        public TotalInfo Total { get; }

        public TimeSpan Length
        {
            get
            {
                return Information.Length;
            }
        }

        public virtual TimeSpan TotalTime
        {
            get
            {
                return Information.Total;
            }
        }

        public virtual Single? Volume { get; init; }

        protected AudioSoundInformation()
        {
            Total = new TotalInfo(this);
        }
    }
}