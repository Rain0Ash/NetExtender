// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Environments;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Application
{
    public static class EnvironmentUtilities
    {
        public static String ExpandEnvironmentVariables(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Environment.ExpandEnvironmentVariables(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetEnvironmentVariable(String key)
        {
            return GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetEnvironmentVariable(String key, EnvironmentVariableTarget target)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            try
            {
                return Environment.GetEnvironmentVariable(key, target);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? TryGetEnvironmentVariable(String? key)
        {
            return TryGetEnvironmentVariable(key, out String? result) ? result : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? TryGetEnvironmentVariable(String? key, EnvironmentVariableTarget target)
        {
            return TryGetEnvironmentVariable(key, target, out String? result) ? result : default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetEnvironmentVariable(String? key, [MaybeNullWhen(false)] out String result)
        {
            return TryGetEnvironmentVariable(key, EnvironmentVariableTarget.Process, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetEnvironmentVariable(String? key, EnvironmentVariableTarget target, [MaybeNullWhen(false)] out String result)
        {
            if (key is null)
            {
                result = default;
                return false;
            }
            
            try
            {
                result = Environment.GetEnvironmentVariable(key, target);
                return result is not null;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetEnvironmentVariable(String key, String? value)
        {
            return SetEnvironmentVariable(key, value, EnvironmentVariableTarget.Process);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetEnvironmentVariable(String key, String? value, EnvironmentVariableTarget target)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            try
            {
                Environment.SetEnvironmentVariable(key, value, target);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TrySetEnvironmentVariable(String? key, String? value)
        {
            return TrySetEnvironmentVariable(key, value, EnvironmentVariableTarget.Process);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TrySetEnvironmentVariable(String? key, String? value, EnvironmentVariableTarget target)
        {
            if (key is null)
            {
                return false;
            }
            
            try
            {
                Environment.SetEnvironmentVariable(key, value, target);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean RemoveEnvironmentVariable(String key)
        {
            return RemoveEnvironmentVariable(key, EnvironmentVariableTarget.Process);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean RemoveEnvironmentVariable(String key, EnvironmentVariableTarget target)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            try
            {
                Environment.SetEnvironmentVariable(key, null, target);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean RemoveEnvironmentVariable(String key, out String? value)
        {
            return RemoveEnvironmentVariable(key, EnvironmentVariableTarget.Process, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean RemoveEnvironmentVariable(String key, EnvironmentVariableTarget target, [MaybeNullWhen(false)] out String value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            try
            {
                if (!TryGetEnvironmentVariable(key, target, out value))
                {
                    return false;
                }
                
                Environment.SetEnvironmentVariable(key, null, target);
                return true;
            }
            catch (Exception)
            {
                value = default;
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryRemoveEnvironmentVariable(String? key)
        {
            return TryRemoveEnvironmentVariable(key, EnvironmentVariableTarget.Process);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryRemoveEnvironmentVariable(String? key, EnvironmentVariableTarget target)
        {
            if (key is null)
            {
                return false;
            }
            
            try
            {
                Environment.SetEnvironmentVariable(key, null, target);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryRemoveEnvironmentVariable(String? key, out String? value)
        {
            return TryRemoveEnvironmentVariable(key, EnvironmentVariableTarget.Process, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryRemoveEnvironmentVariable(String? key, EnvironmentVariableTarget target, [MaybeNullWhen(false)] out String value)
        {
            if (key is null)
            {
                value = default;
                return false;
            }

            try
            {
                if (!TryGetEnvironmentVariable(key, target, out value))
                {
                    return false;
                }
                
                Environment.SetEnvironmentVariable(key, null, target);
                return true;
            }
            catch (Exception)
            {
                value = default;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String[]? GetExistsEnvironmentVariables()
        {
            return GetExistsEnvironmentVariables(EnvironmentVariableTarget.Process);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String[]? GetExistsEnvironmentVariables(EnvironmentVariableTarget target)
        {
            try
            {
                return Environment.GetEnvironmentVariables(target).Keys.OfType<String>().ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String[]? TryGetExistsEnvironmentVariables()
        {
            return TryGetExistsEnvironmentVariables(out String[]? result) ? result : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String[]? TryGetExistsEnvironmentVariables(EnvironmentVariableTarget target)
        {
            return TryGetExistsEnvironmentVariables(target, out String[]? result) ? result : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetExistsEnvironmentVariables([MaybeNullWhen(false)] out String[] result)
        {
            return TryGetExistsEnvironmentVariables(EnvironmentVariableTarget.Process, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetExistsEnvironmentVariables(EnvironmentVariableTarget target, [MaybeNullWhen(false)] out String[] result)
        {
            try
            {
                result = Environment.GetEnvironmentVariables(target).Keys.OfType<String>().ToArray();
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnvironmentValueEntry[]? GetExistsValuesEnvironmentVariables()
        {
            return GetExistsValuesEnvironmentVariables(EnvironmentVariableTarget.Process);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnvironmentValueEntry[]? GetExistsValuesEnvironmentVariables(EnvironmentVariableTarget target)
        {
            try
            {
                IDictionary dictionary = Environment.GetEnvironmentVariables(target);
                IDictionaryEnumerator enumerator = dictionary.GetEnumerator();
                List<EnvironmentValueEntry> entries = new List<EnvironmentValueEntry>(dictionary.Count);

                while (enumerator.MoveNext())
                {
                    String? key = enumerator.Key?.ToString();

                    if (key is null)
                    {
                        continue;
                    }

                    String? value = enumerator.Value?.ToString();

                    if (value is null && enumerator.Value is not null)
                    {
                        continue;
                    }

                    entries.Add(new EnvironmentValueEntry(key, value));
                }

                return entries.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnvironmentValueEntry[]? TryGetExistsValuesEnvironmentVariables()
        {
            return TryGetExistsValuesEnvironmentVariables(out EnvironmentValueEntry[]? result) ? result : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnvironmentValueEntry[]? TryGetExistsValuesEnvironmentVariables(EnvironmentVariableTarget target)
        {
            return TryGetExistsValuesEnvironmentVariables(target, out EnvironmentValueEntry[]? result) ? result : default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetExistsValuesEnvironmentVariables([MaybeNullWhen(false)] out EnvironmentValueEntry[] result)
        {
            return TryGetExistsValuesEnvironmentVariables(EnvironmentVariableTarget.Process, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetExistsValuesEnvironmentVariables(EnvironmentVariableTarget target, [MaybeNullWhen(false)] out EnvironmentValueEntry[] result)
        {
            result = GetExistsValuesEnvironmentVariables(target);
            return result is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception FailFast()
        {
            return ExceptionUtilities.FailFast();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception FailFast(String? message)
        {
            return ExceptionUtilities.FailFast(message);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception FailFast(String? message, Exception? exception)
        {
            return ExceptionUtilities.FailFast(message, exception);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception FailFast(Exception? exception)
        {
            return exception.FailFast();
        }
    }
}