// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Reflection;
using NetExtender.IO.Shortcut.Interfaces;
using NetExtender.Utils.IO;

namespace NetExtender.IO.Shortcut
{
    public class Shortcut : IWshShortcut
    {
        public const String ShortcutExtension = ".lnk";
        
        private static Type ShellType { get; } = Type.GetTypeFromProgID("WScript.Shell");
        private static Object MShell { get; } = Activator.CreateInstance(ShellType);
        
        private static IWshShortcut Create(String name)
        {
            const String shortcut = "CreateShortcut";
            return (IWshShortcut)ShellType.InvokeMember(shortcut, BindingFlags.InvokeMethod, null, MShell, new Object[] { name });
        }
        
        private readonly IWshShortcut _shortcut;

        public String Name { get; }
        
        public String FullName
        {
            get
            {
                return _shortcut.FullName;
            }
        }
        
        public String Arguments
        {
            get
            {
                return _shortcut.Arguments;
            }
            set
            {
                _shortcut.Arguments = value;
            }
        }

        public String Description
        {
            get
            {
                return _shortcut.Description;
            }
            set
            {
                _shortcut.Description = value;
            }
        }

        public String Hotkey
        {
            get
            {
                return _shortcut.Hotkey;
            }
            set
            {
                _shortcut.Hotkey = value;
            }
        }

        public String IconLocation
        {
            get
            {
                return _shortcut.IconLocation;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    return;
                }
                
                _shortcut.IconLocation = value;
            }
        }

        public String RelativePath
        {
            set
            {
                _shortcut.RelativePath = value;
            }
        }

        public String TargetPath
        {
            get
            {
                return _shortcut.TargetPath;
            }
            set
            {
                _shortcut.TargetPath = value;
            }
        }

        public Int32 WindowStyle
        {
            get
            {
                return _shortcut.WindowStyle;
            }
            set
            {
                _shortcut.WindowStyle = value;
            }
        }

        public String WorkingDirectory
        {
            get
            {
                return _shortcut.WorkingDirectory;
            }
            set
            {
                _shortcut.WorkingDirectory = value;
            }
        }

        public String CreatingPath
        {
            get
            {
                return Path.Join(WorkingDirectory, Name);
            }
        }

        public String SavePath
        {
            get
            {
                return Path.Join(PathUtils.IsValidPath(SaveDirectory) ? SaveDirectory : WorkingDirectory, Name);
            }
        }

        private String _savedir;
        public String SaveDirectory
        {
            get
            {
                return _savedir;
            }
            set
            {
                if (!PathUtils.IsValidPath(value))
                {
                    throw new ArgumentException(@"Path is not valid", nameof(value));
                }

                _savedir = value;
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
            _shortcut = Create(name);
        }

        public void Load(String path)
        {
            _shortcut.Load(path);
        }

        public void Save()
        {
            Boolean exists = File.Exists(SavePath);
            if (!Overwrite && exists)
            {
                return;
            }
            
            if (SaveDirectory is null)
            {
                _shortcut.Save();
                return;
            }

            if (exists)
            {
                File.Delete(SavePath);
            }
            
            Directory.CreateDirectory(SaveDirectory);
            _shortcut.Save();
            File.Move(CreatingPath, SavePath);
        }
    }
}