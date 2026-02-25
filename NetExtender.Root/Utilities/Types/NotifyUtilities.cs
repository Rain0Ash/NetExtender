// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace NetExtender.Utilities.Types
{
    public static class NotifyUtilities
    {
        public static class Changing
        {
            private static ConcurrentDictionary<String, PropertyChangingEventArgs> Storage { get; } = new ConcurrentDictionary<String, PropertyChangingEventArgs>();

            public static PropertyChangingEventArgs Null { get; } = new PropertyChangingEventArgs(null);
            public static PropertyChangingEventArgs Count { get; } = Get(nameof(Count));
            public static PropertyChangingEventArgs Min { get; } = Get(nameof(Min));
            public static PropertyChangingEventArgs Minimum { get; } = Get(nameof(Minimum));
            public static PropertyChangingEventArgs Max { get; } = Get(nameof(Max));
            public static PropertyChangingEventArgs Maximum { get; } = Get(nameof(Maximum));
            public static PropertyChangingEventArgs Current { get; } = Get(nameof(Current));
            public static PropertyChangingEventArgs IsEmpty { get; } = Get(nameof(IsEmpty));
            public static PropertyChangingEventArgs HasKey { get; } = Get(nameof(HasKey));
            public static PropertyChangingEventArgs Key { get; } = Get(nameof(Key));
            public static PropertyChangingEventArgs SafeKey { get; } = Get(nameof(SafeKey));
            public static PropertyChangingEventArgs HasValue { get; } = Get(nameof(HasValue));
            public static PropertyChangingEventArgs Value { get; } = Get(nameof(Value));
            public static PropertyChangingEventArgs SafeValue { get; } = Get(nameof(SafeValue));
            public static PropertyChangingEventArgs Previous { get; } = Get(nameof(Previous));
            public static PropertyChangingEventArgs HasPrevious { get; } = Get(nameof(HasPrevious));
            public static PropertyChangingEventArgs Next { get; } = Get(nameof(Next));
            public static PropertyChangingEventArgs HasNext { get; } = Get(nameof(HasNext));
            public static PropertyChangingEventArgs IsActive { get; } = Get(nameof(IsActive));
            public static PropertyChangingEventArgs IsEnabled { get; } = Get(nameof(IsEnabled));
            public static PropertyChangingEventArgs Time { get; } = Get(nameof(Time));
            public static PropertyChangingEventArgs IsTime { get; } = Get(nameof(IsTime));
            public static PropertyChangingEventArgs SetTime { get; } = Get(nameof(SetTime));
            public static PropertyChangingEventArgs Period { get; } = Get(nameof(Period));
            public static PropertyChangingEventArgs IsPeriod { get; } = Get(nameof(IsPeriod));
            public static PropertyChangingEventArgs InPeriod { get; } = Get(nameof(InPeriod));
            public static PropertyChangingEventArgs Delay { get; } = Get(nameof(Delay));
            public static PropertyChangingEventArgs IsDelay { get; } = Get(nameof(IsDelay));
            public static PropertyChangingEventArgs InDelay { get; } = Get(nameof(InDelay));
            public static PropertyChangingEventArgs Status { get; } = Get(nameof(Status));
            public static PropertyChangingEventArgs Progress { get; } = Get(nameof(Progress));
            public static PropertyChangingEventArgs IsVisible { get; } = Get(nameof(IsVisible));
            public static PropertyChangingEventArgs IsChecked { get; } = Get(nameof(IsChecked));
            public static PropertyChangingEventArgs IsSelected { get; } = Get(nameof(IsSelected));
            public static PropertyChangingEventArgs Name { get; } = Get(nameof(Name));
            public static PropertyChangingEventArgs Description { get; } = Get(nameof(Description));

            public static PropertyChangingEventArgs Get(String? property)
            {
                static PropertyChangingEventArgs Factory(String? property)
                {
                    return new PropertyChangingEventArgs(property);
                }

                return property is not null ? Storage.GetOrAdd(property, Factory) : Null;
            }
        }

        public static class Changed
        {
            private static ConcurrentDictionary<String, PropertyChangedEventArgs> Storage { get; } = new ConcurrentDictionary<String, PropertyChangedEventArgs>();

            public static PropertyChangedEventArgs Null { get; } = new PropertyChangedEventArgs(null);
            public static PropertyChangedEventArgs Count { get; } = Get(nameof(Count));
            public static PropertyChangedEventArgs Min { get; } = Get(nameof(Min));
            public static PropertyChangedEventArgs Minimum { get; } = Get(nameof(Minimum));
            public static PropertyChangedEventArgs Max { get; } = Get(nameof(Max));
            public static PropertyChangedEventArgs Maximum { get; } = Get(nameof(Maximum));
            public static PropertyChangedEventArgs Current { get; } = Get(nameof(Current));
            public static PropertyChangedEventArgs IsEmpty { get; } = Get(nameof(IsEmpty));
            public static PropertyChangedEventArgs HasKey { get; } = Get(nameof(HasKey));
            public static PropertyChangedEventArgs Key { get; } = Get(nameof(Key));
            public static PropertyChangedEventArgs SafeKey { get; } = Get(nameof(SafeKey));
            public static PropertyChangedEventArgs HasValue { get; } = Get(nameof(HasValue));
            public static PropertyChangedEventArgs Value { get; } = Get(nameof(Value));
            public static PropertyChangedEventArgs SafeValue { get; } = Get(nameof(SafeValue));
            public static PropertyChangedEventArgs Previous { get; } = Get(nameof(Previous));
            public static PropertyChangedEventArgs HasPrevious { get; } = Get(nameof(HasPrevious));
            public static PropertyChangedEventArgs Next { get; } = Get(nameof(Next));
            public static PropertyChangedEventArgs HasNext { get; } = Get(nameof(HasNext));
            public static PropertyChangedEventArgs IsActive { get; } = Get(nameof(IsActive));
            public static PropertyChangedEventArgs IsEnabled { get; } = Get(nameof(IsEnabled));
            public static PropertyChangedEventArgs Time { get; } = Get(nameof(Time));
            public static PropertyChangedEventArgs IsTime { get; } = Get(nameof(IsTime));
            public static PropertyChangedEventArgs SetTime { get; } = Get(nameof(SetTime));
            public static PropertyChangedEventArgs Period { get; } = Get(nameof(Period));
            public static PropertyChangedEventArgs IsPeriod { get; } = Get(nameof(IsPeriod));
            public static PropertyChangedEventArgs InPeriod { get; } = Get(nameof(InPeriod));
            public static PropertyChangedEventArgs Delay { get; } = Get(nameof(Delay));
            public static PropertyChangedEventArgs IsDelay { get; } = Get(nameof(IsDelay));
            public static PropertyChangedEventArgs InDelay { get; } = Get(nameof(InDelay));
            public static PropertyChangedEventArgs Status { get; } = Get(nameof(Status));
            public static PropertyChangedEventArgs Progress { get; } = Get(nameof(Progress));
            public static PropertyChangedEventArgs IsVisible { get; } = Get(nameof(IsVisible));
            public static PropertyChangedEventArgs IsChecked { get; } = Get(nameof(IsChecked));
            public static PropertyChangedEventArgs IsSelected { get; } = Get(nameof(IsSelected));
            public static PropertyChangedEventArgs Name { get; } = Get(nameof(Name));
            public static PropertyChangedEventArgs Description { get; } = Get(nameof(Description));

            public static PropertyChangedEventArgs Get(String? property)
            {
                static PropertyChangedEventArgs Factory(String? property)
                {
                    return new PropertyChangedEventArgs(property);
                }

                return property is not null ? Storage.GetOrAdd(property, Factory) : Null;
            }
        }
    }

    public readonly struct PropertyChanging : IEquality<String?>, IEquality<PropertyChanging>, IEquality<PropertyChangingEventArgs>, IAnyEquality
    {
        public static implicit operator String?(PropertyChanging value)
        {
            return value.Property;
        }

        public static implicit operator PropertyChanging(String? value)
        {
            return new PropertyChanging(value);
        }

        public static implicit operator PropertyChangingEventArgs(PropertyChanging value)
        {
            return NotifyUtilities.Changing.Get(value);
        }

        public static implicit operator PropertyChanging(PropertyChangedEventArgs? value)
        {
            return value?.PropertyName;
        }

        public static Boolean operator ==(String? first, PropertyChanging second)
        {
            return String.Equals(first, second.Property, StringComparison.Ordinal);
        }

        public static Boolean operator !=(String? first, PropertyChanging second)
        {
            return !String.Equals(first, second.Property, StringComparison.Ordinal);
        }

        public static Boolean operator ==(PropertyChanging first, String? second)
        {
            return String.Equals(first.Property, second, StringComparison.Ordinal);
        }

        public static Boolean operator !=(PropertyChanging first, String? second)
        {
            return !String.Equals(first.Property, second, StringComparison.Ordinal);
        }

        public static Boolean operator ==(PropertyChanging first, PropertyChanging second)
        {
            return String.Equals(first.Property, second.Property, StringComparison.Ordinal);
        }

        public static Boolean operator !=(PropertyChanging first, PropertyChanging second)
        {
            return !String.Equals(first.Property, second.Property, StringComparison.Ordinal);
        }

        public static Boolean operator ==(PropertyChangingEventArgs? first, PropertyChanging second)
        {
            return String.Equals(first?.PropertyName, second.Property, StringComparison.Ordinal);
        }

        public static Boolean operator !=(PropertyChangingEventArgs? first, PropertyChanging second)
        {
            return !String.Equals(first?.PropertyName, second.Property, StringComparison.Ordinal);
        }

        public static Boolean operator ==(PropertyChanging first, PropertyChangingEventArgs? second)
        {
            return String.Equals(first.Property, second?.PropertyName, StringComparison.Ordinal);
        }

        public static Boolean operator !=(PropertyChanging first, PropertyChangingEventArgs? second)
        {
            return !String.Equals(first.Property, second?.PropertyName, StringComparison.Ordinal);
        }

        public static Boolean operator <(String? first, PropertyChanging second)
        {
            return String.Compare(first, second.Property, StringComparison.Ordinal) < 0;
        }

        public static Boolean operator >(String? first, PropertyChanging second)
        {
            return String.Compare(first, second.Property, StringComparison.Ordinal) > 0;
        }

        public static Boolean operator <=(String? first, PropertyChanging second)
        {
            return String.Compare(first, second.Property, StringComparison.Ordinal) <= 0;
        }

        public static Boolean operator >=(String? first, PropertyChanging second)
        {
            return String.Compare(first, second.Property, StringComparison.Ordinal) >= 0;
        }

        public static Boolean operator <(PropertyChanging first, String? second)
        {
            return String.Compare(first.Property, second, StringComparison.Ordinal) < 0;
        }

        public static Boolean operator >(PropertyChanging first, String? second)
        {
            return String.Compare(first.Property, second, StringComparison.Ordinal) > 0;
        }

        public static Boolean operator <=(PropertyChanging first, String? second)
        {
            return String.Compare(first.Property, second, StringComparison.Ordinal) <= 0;
        }

        public static Boolean operator >=(PropertyChanging first, String? second)
        {
            return String.Compare(first.Property, second, StringComparison.Ordinal) >= 0;
        }

        public static Boolean operator <(PropertyChanging first, PropertyChanging second)
        {
            return String.Compare(first.Property, second.Property, StringComparison.Ordinal) < 0;
        }

        public static Boolean operator >(PropertyChanging first, PropertyChanging second)
        {
            return String.Compare(first.Property, second.Property, StringComparison.Ordinal) > 0;
        }

        public static Boolean operator <=(PropertyChanging first, PropertyChanging second)
        {
            return String.Compare(first.Property, second.Property, StringComparison.Ordinal) <= 0;
        }

        public static Boolean operator >=(PropertyChanging first, PropertyChanging second)
        {
            return String.Compare(first.Property, second.Property, StringComparison.Ordinal) >= 0;
        }

        public static Boolean operator <(PropertyChangingEventArgs? first, PropertyChanging second)
        {
            return String.Compare(first?.PropertyName, second.Property, StringComparison.Ordinal) < 0;
        }

        public static Boolean operator >(PropertyChangingEventArgs? first, PropertyChanging second)
        {
            return String.Compare(first?.PropertyName, second.Property, StringComparison.Ordinal) > 0;
        }

        public static Boolean operator <=(PropertyChangingEventArgs? first, PropertyChanging second)
        {
            return String.Compare(first?.PropertyName, second.Property, StringComparison.Ordinal) <= 0;
        }

        public static Boolean operator >=(PropertyChangingEventArgs? first, PropertyChanging second)
        {
            return String.Compare(first?.PropertyName, second.Property, StringComparison.Ordinal) >= 0;
        }

        public static Boolean operator <(PropertyChanging first, PropertyChangingEventArgs? second)
        {
            return String.Compare(first.Property, second?.PropertyName, StringComparison.Ordinal) < 0;
        }

        public static Boolean operator >(PropertyChanging first, PropertyChangingEventArgs? second)
        {
            return String.Compare(first.Property, second?.PropertyName, StringComparison.Ordinal) > 0;
        }

        public static Boolean operator <=(PropertyChanging first, PropertyChangingEventArgs? second)
        {
            return String.Compare(first.Property, second?.PropertyName, StringComparison.Ordinal) <= 0;
        }

        public static Boolean operator >=(PropertyChanging first, PropertyChangingEventArgs? second)
        {
            return String.Compare(first.Property, second?.PropertyName, StringComparison.Ordinal) >= 0;
        }

        public String? Property { get; }

        public PropertyChanging(String? property)
        {
            Property = property;
        }

        public override Int32 GetHashCode()
        {
            return Property?.GetHashCode() ?? 0;
        }

        public Int32 CompareTo(Object? other)
        {
            return other switch
            {
                null => Property is not null ? 1 : 0,
                String value => CompareTo(value),
                PropertyChanging value => CompareTo(value),
                PropertyChangingEventArgs value => CompareTo(value),
                _ => CompareTo(other.ToString())
            };
        }

        public Int32 CompareTo(String? other)
        {
            return String.Compare(Property, other, StringComparison.Ordinal);
        }

        public Int32 CompareTo(PropertyChanging other)
        {
            return CompareTo(other.Property);
        }

        public Int32 CompareTo(PropertyChangingEventArgs? other)
        {
            return CompareTo(other?.PropertyName);
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => Property is null,
                String value => Equals(value),
                PropertyChanging value => Equals(value),
                PropertyChangingEventArgs value => Equals(value),
                _ => Equals(other.ToString())
            };
        }

        public Boolean Equals(String? other)
        {
            return String.Equals(Property, other, StringComparison.Ordinal);
        }

        public Boolean Equals(PropertyChanging other)
        {
            return Equals(other.Property);
        }

        public Boolean Equals(PropertyChangingEventArgs? other)
        {
            return Equals(other?.PropertyName);
        }

        public override String? ToString()
        {
            return Property;
        }
    }

    public readonly struct PropertyChanged : IEquality<String?>, IEquality<PropertyChanged>, IEquality<PropertyChangedEventArgs>, IAnyEquality
    {
        public static implicit operator String?(PropertyChanged value)
        {
            return value.Property;
        }

        public static implicit operator PropertyChanged(String? value)
        {
            return new PropertyChanged(value);
        }

        public static implicit operator PropertyChangedEventArgs(PropertyChanged value)
        {
            return NotifyUtilities.Changed.Get(value);
        }

        public static implicit operator PropertyChanged(PropertyChangedEventArgs? value)
        {
            return value?.PropertyName;
        }

        public static Boolean operator ==(String? first, PropertyChanged second)
        {
            return String.Equals(first, second.Property, StringComparison.Ordinal);
        }

        public static Boolean operator !=(String? first, PropertyChanged second)
        {
            return !String.Equals(first, second.Property, StringComparison.Ordinal);
        }

        public static Boolean operator ==(PropertyChanged first, String? second)
        {
            return String.Equals(first.Property, second, StringComparison.Ordinal);
        }

        public static Boolean operator !=(PropertyChanged first, String? second)
        {
            return !String.Equals(first.Property, second, StringComparison.Ordinal);
        }

        public static Boolean operator ==(PropertyChanged first, PropertyChanged second)
        {
            return String.Equals(first.Property, second.Property, StringComparison.Ordinal);
        }

        public static Boolean operator !=(PropertyChanged first, PropertyChanged second)
        {
            return !String.Equals(first.Property, second.Property, StringComparison.Ordinal);
        }

        public static Boolean operator ==(PropertyChangedEventArgs? first, PropertyChanged second)
        {
            return String.Equals(first?.PropertyName, second.Property, StringComparison.Ordinal);
        }

        public static Boolean operator !=(PropertyChangedEventArgs? first, PropertyChanged second)
        {
            return !String.Equals(first?.PropertyName, second.Property, StringComparison.Ordinal);
        }

        public static Boolean operator ==(PropertyChanged first, PropertyChangedEventArgs? second)
        {
            return String.Equals(first.Property, second?.PropertyName, StringComparison.Ordinal);
        }

        public static Boolean operator !=(PropertyChanged first, PropertyChangedEventArgs? second)
        {
            return !String.Equals(first.Property, second?.PropertyName, StringComparison.Ordinal);
        }

        public static Boolean operator <(String? first, PropertyChanged second)
        {
            return String.Compare(first, second.Property, StringComparison.Ordinal) < 0;
        }

        public static Boolean operator >(String? first, PropertyChanged second)
        {
            return String.Compare(first, second.Property, StringComparison.Ordinal) > 0;
        }

        public static Boolean operator <=(String? first, PropertyChanged second)
        {
            return String.Compare(first, second.Property, StringComparison.Ordinal) <= 0;
        }

        public static Boolean operator >=(String? first, PropertyChanged second)
        {
            return String.Compare(first, second.Property, StringComparison.Ordinal) >= 0;
        }

        public static Boolean operator <(PropertyChanged first, String? second)
        {
            return String.Compare(first.Property, second, StringComparison.Ordinal) < 0;
        }

        public static Boolean operator >(PropertyChanged first, String? second)
        {
            return String.Compare(first.Property, second, StringComparison.Ordinal) > 0;
        }

        public static Boolean operator <=(PropertyChanged first, String? second)
        {
            return String.Compare(first.Property, second, StringComparison.Ordinal) <= 0;
        }

        public static Boolean operator >=(PropertyChanged first, String? second)
        {
            return String.Compare(first.Property, second, StringComparison.Ordinal) >= 0;
        }

        public static Boolean operator <(PropertyChanged first, PropertyChanged second)
        {
            return String.Compare(first.Property, second.Property, StringComparison.Ordinal) < 0;
        }

        public static Boolean operator >(PropertyChanged first, PropertyChanged second)
        {
            return String.Compare(first.Property, second.Property, StringComparison.Ordinal) > 0;
        }

        public static Boolean operator <=(PropertyChanged first, PropertyChanged second)
        {
            return String.Compare(first.Property, second.Property, StringComparison.Ordinal) <= 0;
        }

        public static Boolean operator >=(PropertyChanged first, PropertyChanged second)
        {
            return String.Compare(first.Property, second.Property, StringComparison.Ordinal) >= 0;
        }

        public static Boolean operator <(PropertyChangedEventArgs? first, PropertyChanged second)
        {
            return String.Compare(first?.PropertyName, second.Property, StringComparison.Ordinal) < 0;
        }

        public static Boolean operator >(PropertyChangedEventArgs? first, PropertyChanged second)
        {
            return String.Compare(first?.PropertyName, second.Property, StringComparison.Ordinal) > 0;
        }

        public static Boolean operator <=(PropertyChangedEventArgs? first, PropertyChanged second)
        {
            return String.Compare(first?.PropertyName, second.Property, StringComparison.Ordinal) <= 0;
        }

        public static Boolean operator >=(PropertyChangedEventArgs? first, PropertyChanged second)
        {
            return String.Compare(first?.PropertyName, second.Property, StringComparison.Ordinal) >= 0;
        }

        public static Boolean operator <(PropertyChanged first, PropertyChangedEventArgs? second)
        {
            return String.Compare(first.Property, second?.PropertyName, StringComparison.Ordinal) < 0;
        }

        public static Boolean operator >(PropertyChanged first, PropertyChangedEventArgs? second)
        {
            return String.Compare(first.Property, second?.PropertyName, StringComparison.Ordinal) > 0;
        }

        public static Boolean operator <=(PropertyChanged first, PropertyChangedEventArgs? second)
        {
            return String.Compare(first.Property, second?.PropertyName, StringComparison.Ordinal) <= 0;
        }

        public static Boolean operator >=(PropertyChanged first, PropertyChangedEventArgs? second)
        {
            return String.Compare(first.Property, second?.PropertyName, StringComparison.Ordinal) >= 0;
        }

        public String? Property { get; }

        public PropertyChanged(String? property)
        {
            Property = property;
        }

        public override Int32 GetHashCode()
        {
            return Property?.GetHashCode() ?? 0;
        }

        public Int32 CompareTo(Object? other)
        {
            return other switch
            {
                null => Property is not null ? 1 : 0,
                String value => CompareTo(value),
                PropertyChanged value => CompareTo(value),
                PropertyChangedEventArgs value => CompareTo(value),
                _ => CompareTo(other.ToString())
            };
        }

        public Int32 CompareTo(String? other)
        {
            return String.Compare(Property, other, StringComparison.Ordinal);
        }

        public Int32 CompareTo(PropertyChanged other)
        {
            return CompareTo(other.Property);
        }

        public Int32 CompareTo(PropertyChangedEventArgs? other)
        {
            return CompareTo(other?.PropertyName);
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => Property is null,
                String value => Equals(value),
                PropertyChanged value => Equals(value),
                PropertyChangedEventArgs value => Equals(value),
                _ => Equals(other.ToString())
            };
        }

        public Boolean Equals(String? other)
        {
            return String.Equals(Property, other, StringComparison.Ordinal);
        }

        public Boolean Equals(PropertyChanged other)
        {
            return Equals(other.Property);
        }

        public Boolean Equals(PropertyChangedEventArgs? other)
        {
            return Equals(other?.PropertyName);
        }

        public override String? ToString()
        {
            return Property;
        }
    }
}