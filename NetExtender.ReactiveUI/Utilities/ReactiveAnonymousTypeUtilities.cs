// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.ReactiveUI.Anonymous.Core;
using NetExtender.Types.Anonymous;
using NetExtender.Utilities.Types;

namespace NetExtender.ReactiveUI.Utilities
{
    public static class ReactiveAnonymousTypeUtilities
    {
        private static class ReactiveAnonymousType
        {
            private const String ReactiveAnonymousTypeAssembly = nameof(ReactiveAnonymousTypeAssembly);
            public static ReactiveAnonymousTypeGenerator Generator { get; }

            static ReactiveAnonymousType()
            {
                Generator = new ReactiveAnonymousTypeGenerator(ReactiveAnonymousTypeAssembly);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type DefineReactiveAnonymousType(this ExpandoObject value)
        {
            return ReactiveAnonymousType.Generator.DefineAnonymousType(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type DefineReactiveAnonymousType(this IEnumerable<PropertyInfo> properties)
        {
            return ReactiveAnonymousType.Generator.DefineAnonymousType(properties);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type DefineReactiveAnonymousType(this IEnumerable<KeyValuePair<String, Type>> properties)
        {
            return ReactiveAnonymousType.Generator.DefineAnonymousType(properties);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type DefineReactiveAnonymousType(this AnonymousTypePropertyInfo[] properties)
        {
            return ReactiveAnonymousType.Generator.DefineAnonymousType(properties);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveAnonymousObject CreateReactiveAnonymousObject(this IEnumerable<KeyValuePair<String, Object?>> properties)
        {
            return ReactiveAnonymousType.Generator.CreateAnonymousObject<ReactiveAnonymousObject>(properties);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveAnonymousObject CreateReactiveAnonymousObject(this IEnumerable<KeyValuePair<String, (Type Type, Object? Value)>> properties)
        {
            return ReactiveAnonymousType.Generator.CreateAnonymousObject<ReactiveAnonymousObject>(properties);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveAnonymousObject CreateReactiveAnonymousObject(this ExpandoObject value)
        {
            return ReactiveAnonymousType.Generator.CreateAnonymousObject<ReactiveAnonymousObject>(value);
        }
    }
}