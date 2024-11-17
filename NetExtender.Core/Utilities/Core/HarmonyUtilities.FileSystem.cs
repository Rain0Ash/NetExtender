using System;
using NetExtender.Types.Interception;

namespace NetExtender.Utilities.Core
{
    public static partial class HarmonyUtilities
    {
        public sealed class HarmonyFileSystemIntercept : FileSystemIntercept
        {
            public Type Type { get; }
            
            public HarmonyFileSystemIntercept(Type type)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
            }
        }
    }
}