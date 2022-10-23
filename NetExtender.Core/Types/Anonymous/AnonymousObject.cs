// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetExtender.Types.Anonymous.Interfaces;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Stores;
using NetExtender.Types.Stores.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Anonymous
{
    public abstract class AnonymousObject : IAnonymousObject
    {
        public AnonymousObjectProperty this[Int32 index]
        {
            get
            {
                return new AnonymousObjectProperty(this, index);
            }
        }

        public AnonymousObjectProperty this[String property]
        {
            get
            {
                return new AnonymousObjectProperty(this, property);
            }
        }

        public IEnumerator<KeyValuePair<String, MemberInfo>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<String, MemberInfo>>) AnonymousObjectProperties.Enumerate(this)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    public static class AnonymousObjectProperties
    {
        private static IStore<Type, IndexDictionary<String, MemberInfo>> Store { get; } = new WeakStore<Type, IndexDictionary<String, MemberInfo>>();
        
        private static IndexDictionary<String, MemberInfo> Find(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            static IndexDictionary<String, MemberInfo> Internal(Type type)
            {
                IndexDictionary<String, MemberInfo> store = new IndexDictionary<String, MemberInfo>();

                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

                List<MemberInfo> members = new List<MemberInfo>(16);
                members.AddRange(type.GetProperties(binding).Where(property => !property.IsSpecialName && !property.IsIndexer()));
                members.AddRange(type.GetFields(binding).Where(field => !field.IsSpecialName));
                
                store.AddRange(members.OrderBy(member => member.Name, StringComparer.Ordinal).Select(member => new KeyValuePair<String, MemberInfo>(member.Name, member)));
                return store;
            }

            return Store.GetOrAdd(type, Internal);
        }

        public static void Register(Type type, IndexDictionary<String, MemberInfo> store)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (store is null)
            {
                throw new ArgumentNullException(nameof(store));
            }
            
            Store.AddOrUpdate(type, store);
        }

        // ReSharper disable once ReturnTypeCanBeEnumerable.Global
        public static KeyValuePair<String, MemberInfo>[] Enumerate(IAnonymousObject value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            Type type = value.GetType();
            IndexDictionary<String, MemberInfo> properties = Store.GetOrAdd(type, Find);

            lock (properties)
            {
                return properties.ToArray();
            }
        }

        private static MemberInfo Member(IAnonymousObject value, Int32 index)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            Type type = value.GetType();
            IndexDictionary<String, MemberInfo> properties = Store.GetOrAdd(type, Find);

            try
            {
                return properties.GetValueByIndex(index);
            }
            catch (Exception)
            {
                throw new MissingMemberException(type.FullName, index.ToString());
            }
        }

        private static MemberInfo Member(IAnonymousObject value, String name)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Type type = value.GetType();
            IndexDictionary<String, MemberInfo> properties = Store.GetOrAdd(type, Find);
            return properties.TryGetValue(name, out MemberInfo? member) ? member : throw new MissingMemberException(type.FullName, name);
        }
        
        public static Int32 Count(IAnonymousObject type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Store.GetOrAdd(type.GetType(), Find).Count;
        }
        
        public static Type Type(IAnonymousObject anonymous, Int32 index)
        {
            if (anonymous is null)
            {
                throw new ArgumentNullException(nameof(anonymous));
            }
            
            return Member(anonymous, index) switch
            {
                PropertyInfo property => property.PropertyType,
                FieldInfo field => field.FieldType,
                _ => throw new MissingMemberException(anonymous.GetType().FullName, index.ToString())
            };
        }
        
        public static Type Type(IAnonymousObject anonymous, String name)
        {
            if (anonymous is null)
            {
                throw new ArgumentNullException(nameof(anonymous));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Member(anonymous, name) switch
            {
                PropertyInfo property => property.PropertyType,
                FieldInfo field => field.FieldType,
                _ => throw new MissingMemberException(anonymous.GetType().FullName, name)
            };
        }
        
        public static (Type Type, Object? Value) Get(IAnonymousObject anonymous, Int32 index)
        {
            if (anonymous is null)
            {
                throw new ArgumentNullException(nameof(anonymous));
            }

            return Member(anonymous, index) switch
            {
                PropertyInfo property => (property.PropertyType, property.GetValue(anonymous)),
                FieldInfo field => (field.FieldType, field.GetValue(anonymous)),
                _ => throw new MissingMemberException(anonymous.GetType().FullName, index.ToString())
            };
        }
        
        public static (Type Type, Object? Value) Get(IAnonymousObject anonymous, String name)
        {
            if (anonymous is null)
            {
                throw new ArgumentNullException(nameof(anonymous));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Member(anonymous, name) switch
            {
                PropertyInfo property => (property.PropertyType, property.GetValue(anonymous)),
                FieldInfo field => (field.FieldType, field.GetValue(anonymous)),
                _ => throw new MissingMemberException(anonymous.GetType().FullName, name)
            };
        }

        public static void Set(IAnonymousObject anonymous, Int32 index, Object? value)
        {
            if (anonymous is null)
            {
                throw new ArgumentNullException(nameof(anonymous));
            }

            switch (Member(anonymous, index))
            {
                case PropertyInfo property:
                    property.SetValue(anonymous, value);
                    return;
                case FieldInfo field:
                    field.SetValue(anonymous, value);
                    return;
                default:
                    throw new MissingMemberException(anonymous.GetType().FullName, index.ToString());
            }
        }

        public static void Set(IAnonymousObject anonymous, String name, Object? value)
        {
            if (anonymous is null)
            {
                throw new ArgumentNullException(nameof(anonymous));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            switch (Member(anonymous, name))
            {
                case PropertyInfo property:
                    property.SetValue(anonymous, value);
                    return;
                case FieldInfo field:
                    field.SetValue(anonymous, value);
                    return;
                default:
                    throw new MissingMemberException(anonymous.GetType().FullName, name);
            }
        }
    }
}