using System;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace NetExtender.Utilities.Types
{
    public static class NotifyUtilities
    {
        public static class Changing
        {
            private static ConcurrentDictionary<String, PropertyChangingEventArgs> Store { get; } = new ConcurrentDictionary<String, PropertyChangingEventArgs>();

            public static PropertyChangingEventArgs Null { get; } = new PropertyChangingEventArgs(null);
            public static PropertyChangingEventArgs Count { get; }
            public static PropertyChangingEventArgs Min { get; }
            public static PropertyChangingEventArgs Minimum { get; }
            public static PropertyChangingEventArgs Max { get; }
            public static PropertyChangingEventArgs Maximum { get; }
            public static PropertyChangingEventArgs Current { get; }
            public static PropertyChangingEventArgs Value { get; }
            public static PropertyChangingEventArgs HasValue { get; }
            public static PropertyChangingEventArgs Previous { get; }
            public static PropertyChangingEventArgs HasPrevious { get; }
            public static PropertyChangingEventArgs Next { get; }
            public static PropertyChangingEventArgs HasNext { get; }
            public static PropertyChangingEventArgs IsActive { get; }
            public static PropertyChangingEventArgs IsEnabled { get; }
            public static PropertyChangingEventArgs Status { get; }
            public static PropertyChangingEventArgs Progress { get; }
            public static PropertyChangingEventArgs IsVisible { get; }
            public static PropertyChangingEventArgs IsChecked { get; }
            public static PropertyChangingEventArgs IsSelected { get; }
            public static PropertyChangingEventArgs Name { get; }
            public static PropertyChangingEventArgs Description { get; }

            static Changing()
            {
                Count = Get(nameof(Count));
                Min = Get(nameof(Min));
                Minimum = Get(nameof(Minimum));
                Max = Get(nameof(Max));
                Maximum = Get(nameof(Maximum));
                Current = Get(nameof(Current));
                Value = Get(nameof(Value));
                HasValue = Get(nameof(HasValue));
                Previous = Get(nameof(Previous));
                HasPrevious = Get(nameof(HasPrevious));
                Next = Get(nameof(Next));
                HasNext = Get(nameof(HasNext));
                IsActive = Get(nameof(IsActive));
                IsEnabled = Get(nameof(IsEnabled));
                Status = Get(nameof(Status));
                Progress = Get(nameof(Progress));
                IsVisible = Get(nameof(IsVisible));
                IsChecked = Get(nameof(IsChecked));
                IsSelected = Get(nameof(IsSelected));
                Name = Get(nameof(Name));
                Description = Get(nameof(Description));
            }
            
            public static PropertyChangingEventArgs Get(String? property)
            {
                static PropertyChangingEventArgs Factory(String? property)
                {
                    return new PropertyChangingEventArgs(property);
                }

                return property is not null ? Store.GetOrAdd(property, Factory) : Null;
            }
        }

        public static class Changed
        {
            private static ConcurrentDictionary<String, PropertyChangedEventArgs> Store { get; } = new ConcurrentDictionary<String, PropertyChangedEventArgs>();

            public static PropertyChangedEventArgs Null { get; } = new PropertyChangedEventArgs(null);
            public static PropertyChangedEventArgs Count { get; }
            public static PropertyChangedEventArgs Min { get; }
            public static PropertyChangedEventArgs Minimum { get; }
            public static PropertyChangedEventArgs Max { get; }
            public static PropertyChangedEventArgs Maximum { get; }
            public static PropertyChangedEventArgs Current { get; }
            public static PropertyChangedEventArgs Value { get; }
            public static PropertyChangedEventArgs HasValue { get; }
            public static PropertyChangedEventArgs Previous { get; }
            public static PropertyChangedEventArgs HasPrevious { get; }
            public static PropertyChangedEventArgs Next { get; }
            public static PropertyChangedEventArgs HasNext { get; }
            public static PropertyChangedEventArgs IsActive { get; }
            public static PropertyChangedEventArgs IsEnabled { get; }
            public static PropertyChangedEventArgs Status { get; }
            public static PropertyChangedEventArgs Progress { get; }
            public static PropertyChangedEventArgs IsVisible { get; }
            public static PropertyChangedEventArgs IsChecked { get; }
            public static PropertyChangedEventArgs IsSelected { get; }
            public static PropertyChangedEventArgs Name { get; }
            public static PropertyChangedEventArgs Description { get; }

            static Changed()
            {
                Count = Get(nameof(Count));
                Min = Get(nameof(Min));
                Minimum = Get(nameof(Minimum));
                Max = Get(nameof(Max));
                Maximum = Get(nameof(Maximum));
                Current = Get(nameof(Current));
                Value = Get(nameof(Value));
                HasValue = Get(nameof(HasValue));
                Previous = Get(nameof(Previous));
                HasPrevious = Get(nameof(HasPrevious));
                Next = Get(nameof(Next));
                HasNext = Get(nameof(HasNext));
                IsActive = Get(nameof(IsActive));
                IsEnabled = Get(nameof(IsEnabled));
                Status = Get(nameof(Status));
                Progress = Get(nameof(Progress));
                IsVisible = Get(nameof(IsVisible));
                IsChecked = Get(nameof(IsChecked));
                IsSelected = Get(nameof(IsSelected));
                Name = Get(nameof(Name));
                Description = Get(nameof(Description));
            }
            
            public static PropertyChangedEventArgs Get(String? property)
            {
                static PropertyChangedEventArgs Factory(String? property)
                {
                    return new PropertyChangedEventArgs(property);
                }

                return property is not null ? Store.GetOrAdd(property, Factory) : Null;
            }
        }
    }
    
    public readonly struct PropertyChanging : IEquatable<String?>, IEquatable<PropertyChanging>, IEquatable<PropertyChangingEventArgs>, IComparable, IComparable<String?>, IComparable<PropertyChanging>, IComparable<PropertyChangingEventArgs>
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

        public Int32 CompareTo(Object? obj)
        {
            return obj switch
            {
                null => Property is not null ? 1 : 0,
                String other => CompareTo(other),
                PropertyChanging other => CompareTo(other),
                PropertyChangingEventArgs other => CompareTo(other),
                _ => CompareTo(obj.ToString())
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
    
    public readonly struct PropertyChanged : IEquatable<String?>, IEquatable<PropertyChanged>, IEquatable<PropertyChangedEventArgs>, IComparable, IComparable<String?>, IComparable<PropertyChanged>, IComparable<PropertyChangedEventArgs>
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

        public Int32 CompareTo(Object? obj)
        {
            return obj switch
            {
                null => Property is not null ? 1 : 0,
                String other => CompareTo(other),
                PropertyChanged other => CompareTo(other),
                PropertyChangedEventArgs other => CompareTo(other),
                _ => CompareTo(obj.ToString())
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