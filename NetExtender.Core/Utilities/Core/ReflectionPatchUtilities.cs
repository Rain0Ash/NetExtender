using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Reflection;
using NetExtender.Types.Reflection.Interfaces;

namespace NetExtender.Utilities.Core
{
    public static class ReflectionPatchUtilities
    {
        private static ConcurrentDictionary<IReflectionPatchInfo, Exception?> Storage { get; } = new ConcurrentDictionary<IReflectionPatchInfo, Exception?>();
        
        public static IEnumerable<IReflectionPatchInfo> Applied
        {
            get
            {
                return Storage.Keys.Where(static patch => patch.State == ReflectionPatchState.Apply);
            }
        }
        
        public static IEnumerable<KeyValuePair<IReflectionPatchInfo, Exception?>> Failed
        {
            get
            {
                return Storage.Where(static patch => patch.Key.State == ReflectionPatchState.Failed);
            }
        }
        
        internal static void Set(IReflectionPatchInfo patch)
        {
            Set(patch, null);
        }
        
        internal static void Set(IReflectionPatchInfo patch, Exception? exception)
        {
            if (patch is null)
            {
                throw new ArgumentNullException(nameof(patch));
            }
            
            Storage[patch] = exception;
        }
    }
}