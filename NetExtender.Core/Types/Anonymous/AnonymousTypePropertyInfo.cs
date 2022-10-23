// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Reflection;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Anonymous
{
    public readonly struct AnonymousTypePropertyInfo : IEquatable<AnonymousTypePropertyInfo>
    {
        public static implicit operator AnonymousTypePropertyInfo(PropertyInfo? info)
        {
            return info is not null ? new AnonymousTypePropertyInfo(info.Name, info.PropertyType, info.IsRead(), info.IsWrite()) : default;
        }

        public static implicit operator AnonymousTypePropertyInfo(KeyValuePair<String, Type> info)
        {
            return new AnonymousTypePropertyInfo(info.Key, info.Value, MethodVisibilityType.Public, MethodVisibilityType.Public);
        }
        
        public String Name { get; }
        public Type Type { get; }
        public Boolean? Read { get; }
        public Boolean? Write { get; }
        
        private AnonymousTypePropertyInfo(String name, Type type, MethodVisibilityType read, MethodVisibilityType write)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Read = read.ToBoolean();
            Write = write.ToBoolean();
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Name, Type);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is AnonymousTypePropertyInfo info && Equals(info);
        }

        public Boolean Equals(AnonymousTypePropertyInfo other)
        {
            return Name == other.Name && Type == other.Type;
        }

        public override String ToString()
        {
            return $"{Type.FullName}.{Name}";
        }
    }
}