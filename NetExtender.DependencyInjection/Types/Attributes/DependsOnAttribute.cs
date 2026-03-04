// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;

namespace NetExtender.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class DependsOnAttribute : Attribute
    {
        private readonly Type[]? _before;
        public Type[] Before
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _before ?? Type.EmptyTypes;
            }
            init
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _before = Array.IndexOf(value, default) < 0 ? value : throw new ArgumentException("Array contains 'null' element.");
            }
        }

        private readonly Type[]? _after;
        public Type[] After
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _after ?? Type.EmptyTypes;
            }
            init
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _after = Array.IndexOf(value, default) < 0 ? value : throw new ArgumentException("Array contains 'null' element.");
            }
        }

        public Boolean HasDependency
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _before is { Length: > 0 } || _after is { Length: > 0 };
            }
        }

        public DependsOnAttribute(params Type[]? after)
        {
            if (after is not null)
            {
                After = after;
            }
        }
    }
}