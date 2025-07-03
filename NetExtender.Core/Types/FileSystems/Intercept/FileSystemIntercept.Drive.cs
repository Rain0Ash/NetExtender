// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.FileSystems.Interfaces;

namespace NetExtender.FileSystems
{
    public partial class FileSystemIntercept<T, TInfo>
    {
        public override IDriveInfo[] GetDrives()
        {
            return Interceptor.Intercept(this, default, Drive.GetDrives);
        }
    }
}