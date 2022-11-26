// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using NetExtender.Types.Times.Interfaces;

namespace NetExtender.Types.Times
{
    public class TimePointHistoryWatcher : TimePointWatcher, ITimePointHistoryWatcher
    {
        public const Int32 DefaultHistorySize = 16;
        protected List<DateTimeOffset> Internal { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count + 1;
            }
        }

        public TimePointHistoryWatcher()
            : this(DefaultHistorySize)
        {
        }

        public TimePointHistoryWatcher(Int32 capacity)
        {
            Internal = new List<DateTimeOffset>(capacity);
        }

        public TimePointHistoryWatcher(DateTimeOffset point)
            : this(point, DefaultHistorySize)
        {
        }

        public TimePointHistoryWatcher(DateTimeOffset point, Int32 capacity)
            : base(point)
        {
            Internal = new List<DateTimeOffset>(capacity);
        }

        protected TimePointHistoryWatcher(DateTimeOffset point, IEnumerable<DateTimeOffset>? history)
            : base(point)
        {
            Internal = history is not null ? new List<DateTimeOffset>(history) : new List<DateTimeOffset>(DefaultHistorySize);
        }

        public override Boolean CompareOrUpdate(DateTimeOffset point, TimeSpan epsilon, TimeWatcherComparison comparison, Boolean on)
        {
            DateTimeOffset current = Point;
            Boolean result = base.CompareOrUpdate(point, epsilon, comparison);

            if (result == on)
            {
                Internal.Add(current);
            }

            return result;
        }

        public override Boolean Update(DateTimeOffset point, TimeSpan epsilon, TimeWatcherComparison comparison)
        {
            DateTimeOffset current = Point;
            Boolean result = base.Update(point, epsilon, comparison);
            Internal.Add(current);
            return result;
        }

        public IEnumerator<DateTimeOffset> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public DateTimeOffset this[Int32 index]
        {
            get
            {
                return index < Internal.Count ? Internal[index] : Point;
            }
        }

        public override Object Clone()
        {
            return new TimePointHistoryWatcher(Point, Internal);
        }
    }
}