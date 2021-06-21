// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using NetExtender.IO.Shortcut.Interfaces;
using NetExtender.Utils.IO;
using NetExtender.Windows.Shortcut;

namespace NetExtender.Utils.Windows.IO
{
    public class ShortcutUtils
    {
        public static Boolean CreateShortcut(String path, String directory)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            if (!PathUtils.IsExistAsFile(path))
            {
                return false;
            }

            String file = Path.GetFileNameWithoutExtension(path) + ".lnk";
            String? working = Path.GetDirectoryName(path);

            if (working is null)
            {
                return false;
            }
            
            IShortcut shortcut = new Shortcut(file)
            {
                TargetPath = path,
                WorkingDirectory = working,
                SaveDirectory = directory,
                IconLocation = path
            };

            try
            {
                shortcut.Save();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}