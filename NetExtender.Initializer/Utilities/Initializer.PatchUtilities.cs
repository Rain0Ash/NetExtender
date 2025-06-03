// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Patch;

namespace NetExtender.Initializer
{
    public abstract partial class Initializer
    {
        internal static class PatchUtilities
        {
            private static ConcurrentDictionary<IPatchInfo, Exception?> Storage { get; } = new ConcurrentDictionary<IPatchInfo, Exception?>();
            
            public static IEnumerable<IPatchInfo> Applied
            {
                get
                {
                    return Find(ReflectionPatchState.Apply).Select(static patch => patch.Key);
                }
            }
            
            public static IEnumerable<KeyValuePair<IPatchInfo, Exception?>> Failed
            {
                get
                {
                    return Find(ReflectionPatchState.Failed);
                }
            }
            
            public static IEnumerable<IPatchInfo> NotRequired
            {
                get
                {
                    return Find(ReflectionPatchState.NotRequired).Select(static patch => patch.Key);
                }
            }
            
            public static ReflectionPatchCategory AutoInitPatchCategory
            {
                get
                {
                    return NetExtenderFrameworkInitializer.IncludePatchCategory;
                }
                set
                {
                    NetExtenderFrameworkInitializer.IncludePatchCategory = value;
                }
            }

            public static ISet<String> IncludeAutoInitPatch
            {
                get
                {
                    return NetExtenderFrameworkInitializer.IncludePatch;
                }
            }

            public static ISet<String> ExcludeAutoInitPatch
            {
                get
                {
                    return NetExtenderFrameworkInitializer.ExcludePatch;
                }
            }

            public static Exception? Exception(IPatchInfo? patch)
            {
                return patch is not null && Storage.TryGetValue(patch, out Exception? exception) ? exception : null;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Boolean Set(IPatchInfo? patch)
            {
                return Set(patch, null);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Boolean Set(IPatchInfo? patch, Exception? exception)
            {
                if (patch is null)
                {
                    return false;
                }
                
                Storage[patch] = exception;
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Boolean IsAllowAutoInit(IPatchInfo? patch)
            {
                return patch is not null && (AutoInitPatchCategory.HasFlag(patch.Category) || IncludeAutoInitPatch.Contains(patch.Name)) && !ExcludeAutoInitPatch.Contains(patch.Name);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Boolean AutoInit(String? patch, Boolean exclude)
            {
                return exclude ? ExcludeAutoInit(patch) : IncludeAutoInit(patch);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Boolean AutoInit(IPatchInfo? patch, Boolean exclude)
            {
                return patch is not null && (exclude ? ExcludeAutoInit(patch) : IncludeAutoInit(patch));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Boolean IncludeAutoInit(String? patch)
            {
                return patch is not null && NetExtenderFrameworkInitializer.IncludePatch.Add(patch);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Boolean IncludeAutoInit(IPatchInfo? patch)
            {
                return patch is not null && IncludeAutoInit(patch.Name);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Boolean ExcludeAutoInit(String? patch)
            {
                return patch is not null && NetExtenderFrameworkInitializer.ExcludePatch.Add(patch);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Boolean ExcludeAutoInit(IPatchInfo? patch)
            {
                return patch is not null && ExcludeAutoInit(patch.Name);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static IEnumerable<KeyValuePair<IPatchInfo, Exception?>> Find(ReflectionPatchState state)
            {
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (KeyValuePair<IPatchInfo, Exception?> pair in Storage)
                {
                    if (pair.Key.State == state)
                    {
                        yield return pair;
                    }
                }
            }
        }
    }
}