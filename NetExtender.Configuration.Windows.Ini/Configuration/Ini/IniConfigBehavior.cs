// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.File;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Serialization.Ini;
using NetExtender.Types.Trees;
using NetExtender.Utilities.Static;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Windows.Ini
{
    public class IniConfigBehavior : FileConfigBehavior
    {
        public const String DefaultSection = "Main";
        
        public String MainSection { get; }
        
        protected StringBuilder Buffer { get; } = new StringBuilder(255);

        public IniConfigBehavior()
            : this(ConfigOptions.None)
        {
        }
        
        public IniConfigBehavior(ConfigOptions options)
            : this(null, DefaultSection, null, options)
        {
        }
        
        public IniConfigBehavior(ICryptKey? crypt)
            : this(crypt, ConfigOptions.None)
        {
        }
        
        public IniConfigBehavior(ICryptKey? crypt, ConfigOptions options)
            : this(null, DefaultSection, crypt, options)
        {
        }

        public IniConfigBehavior(String? path)
            : this(path, ConfigOptions.None)
        {
        }

        public IniConfigBehavior(String? path, ConfigOptions options)
            : this(path, DefaultSection, options)
        {
        }
        
        public IniConfigBehavior(String? path, ICryptKey? crypt)
            : this(path, crypt, ConfigOptions.None)
        {
        }
        
        public IniConfigBehavior(String? path, ICryptKey? crypt, ConfigOptions options)
            : this(path, DefaultSection, crypt, options)
        {
        }

        public IniConfigBehavior(String? path, String? section)
            : this(path, section, ConfigOptions.None)
        {
        }

        public IniConfigBehavior(String? path, String? section, ConfigOptions options)
            : this(path, section, null, options)
        {
        }
        
        public IniConfigBehavior(String? path, String? section, ICryptKey? crypt)
            : this(path, section, crypt, ConfigOptions.None)
        {
        }
        
        public IniConfigBehavior(String? path, String? section, ICryptKey? crypt, ConfigOptions options)
            : base(ValidatePathOrGetDefault(path, "ini"), crypt, options)
        {
            MainSection = section ?? DefaultSection;
        }

        [return: NotNullIfNotNull("sections")]
        protected virtual String? ToSection(IEnumerable<String>? sections)
        {
            if (sections is null)
            {
                return null;
            }
            
            String join = Joiner.Join(sections);

            return !String.IsNullOrEmpty(join) ? join : MainSection;
        }
        
        public override String? Get(String? key, IEnumerable<String>? sections)
        {
            return Get(key, ToSection(sections));
        }

        protected virtual String? Get(String? key, String? section)
        {
            if (key is null)
            {
                return null;
            }

            section ??= DefaultSection;
            
            if (GetPrivateProfileString(section, key, String.Empty, Buffer, 255, Path) == 0)
            {
                return null;
            }

            String result = Buffer.Pop();
            return !String.IsNullOrEmpty(result) ? result : null;
        }

        protected override DictionaryTree<String, String>? DeserializeConfig(String config)
        {
            IniFile file = new IniFile(StringComparer.Ordinal);
            file.Read(config);
        }

        protected override String? SerializeConfig()
        {
            throw new NotImplementedException();
        }

        public override Boolean Set(String? key, String? value, IEnumerable<String>? sections)
        {
            return Set(key, value, ToSection(sections));
        }

        protected virtual Boolean Set(String? key, String? value, String? section)
        {
            if (key is null)
            {
                return false;
            }
            
            section ??= DefaultSection;
            
            if (WritePrivateProfileString(section, key, value, Path) == 0)
            {
                WindowsInteropUtilities.ThrowLastWin32Exception();
            }
            
            return true;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern Int32 WritePrivateProfileString(String section, String key, String? value, String path);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern Int32 GetPrivateProfileString(String section, String key, String @default, StringBuilder result, Int32 size, String path);
    }
}