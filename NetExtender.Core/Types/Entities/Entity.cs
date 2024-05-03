// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Entities.Interfaces;

namespace NetExtender.Types.Entities
{
    public sealed class Id<T> : EntityId<T>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator Id<T>?(T? value)
        {
            return value is not null ? new Id<T>(value) : null;
        }
        
        public Id()
        {
        }

        public Id(T value)
            : base(value)
        {
        }
    }
    
    public class EntityId<T> : Entity<T>, IEntityId<T>, IEquatable<EntityId<T>>, IComparable<EntityId<T>>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator T?(EntityId<T>? value)
        {
            return value is not null ? value.Id : default;
        }
        
        [return: NotNullIfNotNull("value")]
        public static implicit operator EntityId<T>?(T? value)
        {
            return value is not null ? new EntityId<T>(value) : null;
        }
        
        public static Boolean operator ==(EntityId<T>? first, EntityId<T>? second)
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

        public static Boolean operator !=(EntityId<T>? first, EntityId<T>? second)
        {
            return !(first == second);
        }

        public static Boolean operator <(EntityId<T>? first, EntityId<T>? second)
        {
            if (first is null)
            {
                return second is not null;
            }

            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(EntityId<T>? first, EntityId<T>? second)
        {
            if (first is null)
            {
                return true;
            }

            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(EntityId<T>? first, EntityId<T>? second)
        {
            return !(first <= second);
        }

        public static Boolean operator >=(EntityId<T>? first, EntityId<T>? second)
        {
            return !(first < second);
        }
        
        public T Id { get; init; }

        public EntityId()
            : this(default!)
        {
        }

        public EntityId(T value)
        {
            Id = value;
        }

        public sealed override T Get()
        {
            return Id;
        }

        public override Int32 CompareTo(Object? obj)
        {
            return obj switch
            {
                EntityId<T> other => CompareTo(other),
                Entity<T> other => CompareTo(other),
                T other => CompareToInternal(other),
                _ => 1
            };
        }

        public Int32 CompareTo(EntityId<T>? other)
        {
            return other is not null ? CompareToInternal(other.Id) : 1;
        }

        protected override Int32 CompareToInternal(T? other)
        {
            return Comparer<T>.Default.Compare(Id, other);
        }

        public override Int32 GetHashCode()
        {
            return Id?.GetHashCode() ?? 0;
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                EntityId<T> other => Equals(other),
                Entity<T> other => Equals(other),
                T other => EqualsInternal(other),
                _ => false
            };
        }

        public Boolean Equals(EntityId<T>? other)
        {
            return other is not null && EqualsInternal(other.Id);
        }

        protected override Boolean EqualsInternal(T? other)
        {
            return EqualityComparer<T>.Default.Equals(Id, other);
        }

        public override String? ToString()
        {
            return Id?.ToString();
        }

        public override String ToString(String? format, IFormatProvider? provider)
        {
            return Id is IFormattable formattable ? formattable.ToString(format, provider) : ToString() ?? String.Empty;
        }
    }

    public class EntityValue<T> : Entity<T>, IEntityValue<T>, IEquatable<EntityValue<T>>, IComparable<EntityValue<T>>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator T?(EntityValue<T>? value)
        {
            return value is not null ? value.Value : default;
        }
        
        [return: NotNullIfNotNull("value")]
        public static implicit operator EntityValue<T>?(T? value)
        {
            return value is not null ? new EntityValue<T>(value) : null;
        }
        
        public static Boolean operator ==(EntityValue<T>? first, EntityValue<T>? second)
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

        public static Boolean operator !=(EntityValue<T>? first, EntityValue<T>? second)
        {
            return !(first == second);
        }

        public static Boolean operator <(EntityValue<T>? first, EntityValue<T>? second)
        {
            if (first is null)
            {
                return second is not null;
            }

            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(EntityValue<T>? first, EntityValue<T>? second)
        {
            if (first is null)
            {
                return true;
            }

            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(EntityValue<T>? first, EntityValue<T>? second)
        {
            return !(first <= second);
        }

        public static Boolean operator >=(EntityValue<T>? first, EntityValue<T>? second)
        {
            return !(first < second);
        }
        
        public T Value { get; init; }

        public EntityValue(T value)
        {
            Value = value;
        }

        public sealed override T Get()
        {
            return Value;
        }

        public override Int32 CompareTo(Object? obj)
        {
            return obj switch
            {
                EntityValue<T> other => CompareTo(other),
                Entity<T> other => CompareTo(other),
                T other => CompareToInternal(other),
                _ => 1
            };
        }

        public Int32 CompareTo(EntityValue<T>? other)
        {
            return other is not null ? CompareToInternal(other.Value) : 1;
        }

        protected override Int32 CompareToInternal(T? other)
        {
            return Comparer<T>.Default.Compare(Value, other);
        }

        public override Int32 GetHashCode()
        {
            return Value?.GetHashCode() ?? 0;
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                EntityValue<T> other => Equals(other),
                Entity<T> other => Equals(other),
                T other => EqualsInternal(other),
                _ => false
            };
        }

        public Boolean Equals(EntityValue<T>? other)
        {
            return other is not null && EqualsInternal(other.Value);
        }

        protected override Boolean EqualsInternal(T? other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other);
        }

        public override String? ToString()
        {
            return Value?.ToString();
        }

        public override String ToString(String? format, IFormatProvider? provider)
        {
            return Value is IFormattable formattable ? formattable.ToString(format, provider) : ToString() ?? String.Empty;
        }
    }
    
    public abstract class Entity<T> : IEntity<T>, IEquatable<T>, IEquatable<Entity<T>>, IComparable, IComparable<T>, IComparable<Entity<T>>, IFormattable
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator T?(Entity<T>? value)
        {
            return value is not null ? value.Get() : default;
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

        public abstract T Get();
        
        public virtual Int32 CompareTo(Object? obj)
        {
            return obj switch
            {
                Entity<T> other => CompareTo(other),
                T other => CompareToInternal(other),
                _ => 1
            };
        }

        public Int32 CompareTo(T? other)
        {
            return CompareToInternal(other);
        }

        public Int32 CompareTo(Entity<T>? other)
        {
            return other is not null ? CompareTo(other.Get()) : 1;
        }

        protected virtual Int32 CompareToInternal(T? other)
        {
            return Comparer<T>.Default.Compare(Get(), other);
        }

        public override Int32 GetHashCode()
        {
            return Get()?.GetHashCode() ?? 0;
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                Entity<T> other => Equals(other),
                T other => EqualsInternal(other),
                _ => false
            };
        }

        public Boolean Equals(T? other)
        {
            return EqualsInternal(other);
        }

        public Boolean Equals(Entity<T>? other)
        {
            return other is not null && EqualsInternal(other.Get());
        }

        protected virtual Boolean EqualsInternal(T? other)
        {
            return EqualityComparer<T>.Default.Equals(Get(), other);
        }

        public override String? ToString()
        {
            return Get()?.ToString();
        }

        public String ToString(String? format)
        {
            return ToString(format, null);
        }

        public virtual String ToString(String? format, IFormatProvider? provider)
        {
            return Get() is IFormattable formattable ? formattable.ToString(format, provider) : ToString() ?? String.Empty;
        }
    }
}