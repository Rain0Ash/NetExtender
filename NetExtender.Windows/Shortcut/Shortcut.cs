// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Reflection;
using NetExtender.IO.Shortcut.Interfaces;
using NetExtender.Utilities.IO;
using NetExtender.Windows.Shortcut.Interfaces;

namespace NetExtender.Windows.Shortcut
{
    public class Shortcut : IShortcut
    {
        public static Shortcut? Open(String path)
        {
            if (String.IsNullOrEmpty(path))
            {
                return null;
            }

            if (!PathUtilities.IsExistAsFile(path))
            {
                return null;
            }

            try
            {
                Shortcut shortcut = new Shortcut(path);
                return shortcut.Load(path);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public const String ShortcutExtension = ".lnk";
        
        private static Type? ShellType { get; } = Type.GetTypeFromProgID("WScript.Shell");
        private static Object? MShell { get; } = ShellType is not null ? Activator.CreateInstance(ShellType) : null;
        
        private static IWshShortcut? Create(String name)
        {
            try
            {
                return (IWshShortcut?) ShellType?.InvokeMember("CreateShortcut", BindingFlags.InvokeMethod, null, MShell, new Object[] {name});
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        private IWshShortcut Internal { get; }

        public String Name { get; }
        
        public String FullName
        {
            get
            {
                return Internal.FullName;
            }
        }
        
        public String? Arguments
        {
            get
            {
                return Internal.Arguments;
            }
            init
            {
                Internal.Arguments = value;
            }
        }

        public String? Description
        {
            get
            {
                return Internal.Description;
            }
            init
            {
                Internal.Description = value;
            }
        }

        public String? Hotkey
        {
            get
            {
                return Internal.Hotkey;
            }
            init
            {
                Internal.Hotkey = value;
            }
        }

        public String? IconLocation
        {
            get
            {
                return Internal.IconLocation;
            }
            init
            {
                if (String.IsNullOrEmpty(value))
                {
                    return;
                }
                
                Internal.IconLocation = value;
            }
        }

        public String RelativePath
        {
            init
            {
                Internal.RelativePath = value;
            }
        }

        public String TargetPath
        {
            get
            {
                return Internal.TargetPath;
            }
            init
            {
                Internal.TargetPath = value;
            }
        }

        public Int32 WindowStyle
        {
            get
            {
                return Internal.WindowStyle;
            }
            init
            {
                Internal.WindowStyle = value;
            }
        }

        public String WorkingDirectory
        {
            get
            {
                return Internal.WorkingDirectory;
            }
            init
            {
                Internal.WorkingDirectory = value;
            }
        }

        public String CreatingPath
        {
            get
            {
                return Path.Join(WorkingDirectory, Name);
            }
        }

        public String? SavePath
        {
            get
            {
                return SaveDirectory is not null ? Path.Join(PathUtilities.IsValidPath(SaveDirectory) ? SaveDirectory : WorkingDirectory, Name) : null;
            }
        }

        private readonly String? _directory;
        public String? SaveDirectory
        {
            get
            {
                return _directory;
            }
            init
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                
                if (!PathUtilities.IsValidPath(value))
                {
                    throw new ArgumentException(@"Path is not valid", nameof(value));
                }

                _directory = value;
            }
        }

        public Boolean Overwrite { get; set; }

        public Shortcut(String name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            String ext = Path.GetExtension(name);
            
            if (String.IsNullOrEmpty(ext))
            {
                name += ".lnk";
            }
            else if (ext != ShortcutExtension)
            {
                throw new ArgumentException(@$"Name can contain only {ShortcutExtension} extension", nameof(name));
            }

            Name = name;
            Internal = Create(name) ?? throw new NotSupportedException();
        }

        private Shortcut Load(String path)
        {
            Internal.Load(path);
            return this;
        }

        public Boolean Save()
        {
            String? path = SavePath;
            if (path is null)
            {
                return false;
            }
            
            Boolean exists = File.Exists(path);
            if (!Overwrite && exists)
            {
                return false;
            }

            String? directory = SaveDirectory;
            if (directory is null)
            {
                Internal.Save();
                return false;
            }

            if (exists)
            {
                File.Delete(path);
            }
            
            Directory.CreateDirectory(directory);
            Internal.Save();
            File.Move(CreatingPath, path);
            return true;
        }
    }
}