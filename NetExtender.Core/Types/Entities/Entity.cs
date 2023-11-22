// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Entities.Interfaces;

namespace NetExtender.Types.Entities
{
    public sealed class Id<T> : Entity<T>
    {
    }
    
    public class Entity<T> : IEntity<T>, IEquatable<T>, IEquatable<Entity<T>>, IComparable<T>, IComparable<Entity<T>>, IFormattable
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator T?(Entity<T>? value)
        {
            return value is not null ? value.Id : default;
        }
        
        [return: NotNullIfNotNull("value")]
        public static implicit operator Entity<T>?(T? value)
        {
            return value is not null ? new Entity<T>(value) : null;
        }
        
        public static Boolean operator ==(Entity<T>? first, Entity<T>? second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            return first.Equals(second);
        }

        public static Boolean operator !=(Entity<T>? first, Entity<T>? second)
        {
            return !(first == second);
        }

        public static Boolean operator <(Entity<T>? first, Entity<T>? second)
        {
            if (first is null)
            {
                return second is not null;
            }

            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(Entity<T>? first, Entity<T>? second)
        {
            if (first is null)
            {
                return true;
            }

            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(Entity<T>? first, Entity<T>? second)
        {
            return !(first <= second);
        }

        public static Boolean operator >=(Entity<T>? first, Entity<T>? second)
        {
            return !(first < second);
        }
        
        public T Id { get; init; }

        public Entity()
            : this(default!)
        {
        }

        public Entity(T value)
        {
            Id = value;
        }

        public override Int32 GetHashCode()
        {
            return Id?.GetHashCode() ?? 0;
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                Entity<T> other => Equals(other),
                T other => Equals(other),
                _ => false
            };
        }

        public virtual Boolean Equals(T? other)
        {
            return EqualityComparer<T>.Default.Equals(Id, other);
        }

        public virtual Boolean Equals(Entity<T>? other)
        {
            return other is not null && EqualityComparer<T>.Default.Equals(Id, other.Id);
        }

        public virtual Int32 CompareTo(T? other)
        {
            return Comparer<T>.Default.Compare(Id, other);
        }

        public virtual Int32 CompareTo(Entity<T>? other)
        {
            return other is not null ? Comparer<T>.Default.Compare(Id, other.Id) : 1;
        }

        public override String? ToString()
        {
            return Id?.ToString();
        }

        public String ToString(String? format)
        {
            return ToString(format, null);
        }

        public virtual String ToString(String? format, IFormatProvider? provider)
        {
            return Id is IFormattable formattable ? formattable.ToString(format, provider) : ToString() ?? String.Empty;
        }
    }
}