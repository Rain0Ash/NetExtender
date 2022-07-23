// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NetExtender.Types.Times.Interfaces;

namespace NetExtender.Types.Times
{
    public class TimePointWatcher : ITimePointWatcher, IFormattable
    {
        public static implicit operator DateTimeOffset(TimePointWatcher? value)
        {
            return value?.Point ?? default;
        }
        
        public static implicit operator TimePointWatcher(DateTimeOffset value)
        {
            return new TimePointWatcher(value);
        }
        
        public event EventHandler<TimePointEventArgs>? Changed;
        
        private DateTimeOffset _point;
        public DateTimeOffset Point
        {
            get
            {
                return _point;
            }
            protected set
            {
                DateTimeOffset offset = Point;
                if (offset == value)
                {
                    return;
                }
                
                _point = value;
                Changed?.Invoke(this, new TimePointEventArgs(offset, value));
            }
        }

        public TimePointWatcher()
            : this(DateTimeOffset.UtcNow)
        {
        }
        
        public TimePointWatcher(DateTimeOffset point)
        {
            Point = point;
        }

        public virtual Boolean Compare(DateTimeOffset point, TimeSpan epsilon, TimeWatcherComparison comparison)
        {
            TimeSpan difference = point.UtcDateTime.Subtract(Point.UtcDateTime);
            
            return comparison switch
            {
                TimeWatcherComparison.Less => difference < epsilon,
                TimeWatcherComparison.LessAbsolute => difference.Duration() < epsilon,
                TimeWatcherComparison.LessOrEqual => difference <= epsilon,
                TimeWatcherComparison.LessOrEqualAbsolute => difference.Duration() <= epsilon,
                TimeWatcherComparison.Equal => difference == epsilon,
                TimeWatcherComparison.EqualAbsolute => difference.Duration() == epsilon,
                TimeWatcherComparison.GreaterOrEqual => difference >= epsilon,
                TimeWatcherComparison.GreaterOrEqualAbsolute => difference.Duration() >= epsilon,
                TimeWatcherComparison.Greater => difference > epsilon,
                TimeWatcherComparison.GreaterAbsolute => difference.Duration() > epsilon,
                TimeWatcherComparison.NotEqual => difference != epsilon,
                TimeWatcherComparison.NotEqualAbsolute => difference.Duration() != epsilon,
                _ => throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null)
            };
        }

        public Boolean CompareNow(TimeSpan epsilon, TimeWatcherComparison comparison)
        {
            return Compare(DateTimeOffset.UtcNow, epsilon, comparison);
        }

        public Boolean CompareOrUpdate(DateTimeOffset point, TimeSpan epsilon, TimeWatcherComparison comparison)
        {
            return CompareOrUpdate(point, epsilon, comparison, true);
        }

        public virtual Boolean CompareOrUpdate(DateTimeOffset point, TimeSpan epsilon, TimeWatcherComparison comparison, Boolean on)
        {
            Boolean result = Compare(point, epsilon, comparison);

            if (result == on)
            {
                Point = point;
            }
            
            return result;
        }

        public Boolean CompareOrUpdateNow(TimeSpan epsilon, TimeWatcherComparison comparison)
        {
            return CompareOrUpdateNow(epsilon, comparison, true);
        }

        public Boolean CompareOrUpdateNow(TimeSpan epsilon, TimeWatcherComparison comparison, Boolean on)
        {
            return CompareOrUpdate(DateTimeOffset.Now, epsilon, comparison, on);
        }

        public Boolean CompareOrUpdateUtcNow(TimeSpan epsilon, TimeWatcherComparison comparison)
        {
            return CompareOrUpdateUtcNow(epsilon, comparison, true);
        }

        public Boolean CompareOrUpdateUtcNow(TimeSpan epsilon, TimeWatcherComparison comparison, Boolean on)
        {
            return CompareOrUpdate(DateTimeOffset.UtcNow, epsilon, comparison, on);
        }

        public virtual Boolean Update(DateTimeOffset point, TimeSpan epsilon, TimeWatcherComparison comparison)
        {
            Boolean result = Compare(point, epsilon, comparison);
            Point = point;
            return result;
        }
        
        public Boolean UpdateNow(TimeSpan epsilon, TimeWatcherComparison comparison)
        {
            return Update(DateTimeOffset.Now, epsilon, comparison);
        }
        
        public Boolean UpdateUtcNow(TimeSpan epsilon, TimeWatcherComparison comparison)
        {
            return Update(DateTimeOffset.UtcNow, epsilon, comparison);
        }
        
        public Boolean Set(DateTimeOffset point)
        {
            Point = point;
            return true;
        }
        
        public Boolean SetNow()
        {
            return Set(DateTimeOffset.Now);
        }
        
        public Boolean SetUtcNow()
        {
            return Set(DateTimeOffset.UtcNow);
        }
        
        public Int32 CompareTo(DateTime other)
        {
            return Point.UtcDateTime.CompareTo(other);
        }

        public Int32 CompareTo(DateTimeOffset other)
        {
            return Point.CompareTo(other);
        }

        public Int32 CompareTo(ITimePointWatcher? other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            return Point.CompareTo(other.Point);
        }

        public override Int32 GetHashCode()
        {
            return RuntimeHelpers.GetHashCode(this);
        }

        public Boolean Equals(DateTime other)
        {
            return Point.UtcDateTime.Equals(other);
        }

        public Boolean Equals(DateTimeOffset other)
        {
            return Point.Equals(other);
        }

        public Boolean Equals(TimePointWatcher? other)
        {
            return ReferenceEquals(this, other) || !ReferenceEquals(null, other) && Point.Equals(other.Point);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                DateTime other => Equals(other),
                DateTimeOffset other => Equals(other),
                TimePointWatcher other => Equals(other),
                _ => false
            };
        }

        public override String ToString()
        {
            return Point.ToString();
        }

        public String ToString(String? format)
        {
            return Point.ToString(format);
        }
        
        public String ToString(IFormatProvider? provider)
        {
            return Point.ToString(provider);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return Point.ToString(format, provider);
        }

        public virtual Object Clone()
        {
            return new TimePointWatcher(Point);
        }
    }

    public class TimePointEventArgs : HandledEventArgs
    {
        public DateTimeOffset Previous { get; }
        public DateTimeOffset Current { get; }

        public TimePointEventArgs(DateTimeOffset previous, DateTimeOffset current)
            : this(previous, current, false)
        {
        }

        public TimePointEventArgs(DateTimeOffset previous, DateTimeOffset current, Boolean handled)
            : base(handled)
        {
            Previous = previous;
            Current = current;
        }
    }
}