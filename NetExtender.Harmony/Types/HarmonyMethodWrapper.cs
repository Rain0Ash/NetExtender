using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NetExtender.Harmony.Types.Interfaces;

namespace NetExtender.Harmony.Types
{
    public struct HarmonyMethodWrapper : IEquatableStruct<HarmonyMethodWrapper>, IUnsafeHarmonyMethod, IEquatable<HarmonyLib.HarmonyMethod>
    {
        public static implicit operator HarmonyMethodWrapper(HarmonyLib.HarmonyMethod value)
        {
            return new HarmonyMethodWrapper(value);
        }

        public static implicit operator HarmonyLib.HarmonyMethod(HarmonyMethodWrapper value)
        {
            return value.Method;
        }

        public static Boolean operator ==(HarmonyMethodWrapper first, HarmonyMethodWrapper second)
        {
            return first.Method == second.Method;
        }

        public static Boolean operator !=(HarmonyMethodWrapper first, HarmonyMethodWrapper second)
        {
            return first.Method != second.Method;
        }

        private HarmonyLib.HarmonyMethod? _method;
        public HarmonyLib.HarmonyMethod Method
        {
            get
            {
                return _method ??= new HarmonyLib.HarmonyMethod();
            }
        }

        HarmonyLib.HarmonyMethod IUnsafeHarmonyMethod.Method
        {
            get
            {
                return Method;
            }
        }

        public readonly Boolean IsEmpty
        {
            get
            {
                return _method is null;
            }
        }

        public HarmonyMethodWrapper(Delegate @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            _method = new HarmonyLib.HarmonyMethod(@delegate);
        }

        public HarmonyMethodWrapper(MethodInfo method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            _method = new HarmonyLib.HarmonyMethod(method);
        }

        public HarmonyMethodWrapper(HarmonyLib.HarmonyMethod? method)
        {
            _method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public override readonly Int32 GetHashCode()
        {
            return _method?.GetHashCode() ?? 0;
        }

        public override Boolean Equals([NotNullWhen(true)] Object? other)
        {
            return other switch
            {
                HarmonyLib.HarmonyMethod value => Equals(value),
                HarmonyMethodWrapper value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(HarmonyLib.HarmonyMethod? other)
        {
            return Equals(_method, other);
        }

        public Boolean Equals(HarmonyMethodWrapper other)
        {
            return Equals(other._method);
        }

        public override readonly String? ToString()
        {
            return _method?.ToString();
        }
    }
}