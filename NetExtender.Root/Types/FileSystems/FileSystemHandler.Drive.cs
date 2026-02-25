// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.IO;
using NetExtender.FileSystems.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.FileSystems
{
    public partial class FileSystemHandler
    {
        public virtual IDriveInfo[] GetDrives()
        {
            return DriveInfo.GetDrives().ConvertAll(FileSystemInfoWrapper.Create)!;
        }
    }
}