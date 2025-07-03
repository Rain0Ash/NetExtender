// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.FileSystems;
using NetExtender.FileSystems.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Core
{
    public static partial class HarmonyUtilities
    {
        [ReflectionSignature]
        public static class DriveInfo
        {
            public static IInterceptDriveHandler Interceptor { get; }

            static DriveInfo()
            {
                Interceptor = new InterceptHarmonyFileSystem(typeof(DriveInfo));
            }

            public static System.IO.DriveInfo[] GetDrives()
            {
                return Interceptor.GetDrives().ConvertAll(static drive => drive.Info ?? throw new FileSystemIsNotRealException(drive));
            }
        }
    }
}