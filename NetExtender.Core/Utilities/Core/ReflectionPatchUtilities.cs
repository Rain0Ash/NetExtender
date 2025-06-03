// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Patch;
using NetExtender.Types.Reflection.Interfaces;

namespace NetExtender.Utilities.Core
{
    public static class ReflectionPatchUtilities
    {
        public static IEnumerable<IReflectionPatchInfo> Applied
        {
            get
            {
                return Initializer.Initializer.PatchUtilities.Applied.Cast<IReflectionPatchInfo>();
            }
        }
        
        public static IEnumerable<KeyValuePair<IReflectionPatchInfo, Exception?>> Failed
        {
            get
            {
                static KeyValuePair<IReflectionPatchInfo, Exception?> Selector(KeyValuePair<IPatchInfo, Exception?> pair)
                {
                    return new KeyValuePair<IReflectionPatchInfo, Exception?>((IReflectionPatchInfo) pair.Key, pair.Value);
                }
                
                return Initializer.Initializer.PatchUtilities.Failed.Select(Selector);
            }
        }
        
        public static IEnumerable<IReflectionPatchInfo> NotRequired
        {
            get
            {
                return Initializer.Initializer.PatchUtilities.NotRequired.Cast<IReflectionPatchInfo>();
            }
        }

        public static Exception? Require(this IReflectionPatch? patch)
        {
            if (patch is null)
            {
                return null;
            }

            if (patch.State is ReflectionPatchState.None)
            {
                patch.Apply();
            }

            return patch.State switch
            {
                ReflectionPatchState.Apply => null,
                ReflectionPatchState.NotRequired => new NotSupportedException(),
                _ => Exception(patch) ?? new NotSupportedException()
            };
        }

        public static Exception? Exception(this IReflectionPatchInfo? patch)
        {
            return Initializer.Initializer.PatchUtilities.Exception(patch?.Patch);
        }
        
        internal static Boolean Set(IReflectionPatchInfo? patch)
        {
            return Set(patch, null);
        }
        
        internal static Boolean Set(IReflectionPatchInfo? patch, Exception? exception)
        {
            return Initializer.Initializer.PatchUtilities.Set(patch?.Patch, exception);
        }

        internal static Boolean IsAllowAutoInit(IReflectionPatchInfo? patch)
        {
            return Initializer.Initializer.PatchUtilities.IsAllowAutoInit(patch?.Patch);
        }

        internal static Boolean IncludeAutoInit(IReflectionPatchInfo? patch)
        {
            return Initializer.Initializer.PatchUtilities.IncludeAutoInit(patch?.Patch);
        }

        internal static Boolean ExcludeAutoInit(IReflectionPatchInfo? patch)
        {
            return Initializer.Initializer.PatchUtilities.ExcludeAutoInit(patch?.Patch);
        }
    }
}