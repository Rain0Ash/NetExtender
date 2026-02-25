// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.FileSystems.Interfaces;

namespace NetExtender.FileSystems
{
    public partial class FileSystemHandlerWrapper<T>
    {
        public override IDriveInfo[] GetDrives()
        {
            return FileSystem.Drive.GetDrives();
        }
    }
}