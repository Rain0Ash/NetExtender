// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Times.Interfaces
{
    public interface ITimePointWatcher : IComparable<DateTime>, IComparable<DateTimeOffset>, IComparable<ITimePointWatcher>, IEquatable<DateTime>, IEquatable<DateTimeOffset>, ICloneable
    {
        public DateTimeOffset Point { get; }

        public Boolean Compare(DateTimeOffset point, TimeSpan epsilon, TimeWatcherComparison comparison);
        public Boolean CompareOrUpdate(DateTimeOffset point, TimeSpan epsilon, TimeWatcherComparison comparison);
        public Boolean CompareOrUpdate(DateTimeOffset point, TimeSpan epsilon, TimeWatcherComparison comparison, Boolean on);
        public Boolean Update(DateTimeOffset point, TimeSpan epsilon, TimeWatcherComparison comparison);
    }
}