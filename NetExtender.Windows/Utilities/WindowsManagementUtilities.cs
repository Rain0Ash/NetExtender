// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;

namespace NetExtender.Windows.Utilities
{
    public static class WindowsManagementUtilities
    {
        public static IEnumerator<ManagementBaseObject> GetEnumerator(this ManagementObjectCollection.ManagementObjectEnumerator enumerator)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
        
        public static IEnumerable<ManagementBaseObject> AsEnumerable(this ManagementObjectCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            foreach (ManagementBaseObject management in collection)
            {
                yield return management;
            }
        }

        public static IEnumerable<ManagementObject> AsManagement(this ManagementObjectCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            foreach (ManagementObject management in collection.OfType<ManagementObject>())
            {
                yield return management;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean TryGetValue(this ManagementBaseObject management, String property, [MaybeNullWhen(false)] out Object result)
        {
            if (management is null)
            {
                throw new ArgumentNullException(nameof(management));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            try
            {
                result = management[property];
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean TryGetValue(this ManagementBaseObject management, String property, [MaybeNullWhen(false)] out String result)
        {
            if (TryGetValue(management, property, out Object? value))
            {
                result = value.ToString();
                return result is not null;
            }

            result = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean TryGetValue<T>(this ManagementBaseObject management, String property, [MaybeNullWhen(false)] out T result)
        {
            if (management is null)
            {
                throw new ArgumentNullException(nameof(management));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            try
            {
                Object? item = management[property];

                if (item is T generic)
                {
                    result = generic;
                    return true;
                }

                if (!typeof(T).IsEnum)
                {
                    result = (T) (dynamic) item;
                    return true;
                }

                switch (item)
                {
                    case String value when Enum.TryParse(typeof(T), value, out item):
                        result = (T?) item;
                        return result is not null;
                    case String:
                        result = default;
                        return false;
                    case IConvertible convertible:
                        result = (T) Enum.ToObject(typeof(T), convertible);
                        return true;
                }

                if (Enum.TryParse(typeof(T), item.ToString(), out item))
                {
                    result = (T?) item;
                    return item is not null;
                }

                result = default;
                return false;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
    }
}