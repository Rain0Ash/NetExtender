// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using NetExtender.Apps.Domains;

namespace NetExtender.Apps.Updater
{
    public sealed partial class Updater
    {
        private static String Path
        {
            get
            {
                return Domain.Path;
            }
        }

        private static String Directory
        {
            get
            {
                return Domain.Directory;
            }
        }

        private static String Backup
        {
            get
            {
                return System.IO.Path.Join(Directory, System.IO.Path.GetFileNameWithoutExtension(Path) + ".bak");
            }
        }

        private static void BackupOriginalFile()
        {
            try
            {
                File.Move(Path, Backup);
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        
        private static void RemoveBackupFile()
        {
            try
            {
                if (File.Exists(Backup))
                {
                    File.Delete(Backup);
                }
            }
            catch (Exception)
            {
                //ignore
            }
        }
    }
}