// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using NetExtender.Configuration.Common;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utils.Static;
using NetExtender.Utils.Types;

namespace NetExtender.Configuration.Ini
{
    public class IniConfigBehavior : ConfigBehavior
    {
        public const String DefaultJoiner = ".";
        public const String DefaultSection = "Main";
        
        public String MainSection { get; }
        public String Joiner { get; }
        
        protected StringBuilder Buffer { get; } = new StringBuilder(255);

        public IniConfigBehavior(String path = null, ConfigOptions options = ConfigOptions.None)
            : this(path, DefaultSection, options)
        {
        }
        
        public IniConfigBehavior(String path, String section, ConfigOptions options = ConfigOptions.None)
            : this(path, section, DefaultJoiner, options)
        {
        }
        
        public IniConfigBehavior(String path, String section, String joiner = DefaultJoiner, ConfigOptions options = ConfigOptions.None)
            : this(path, section, joiner, null, options)
        {
        }
        
        public IniConfigBehavior(String path, ICryptKey crypt, ConfigOptions options = ConfigOptions.None)
            : this(path, DefaultSection, crypt, options)
        {
        }

        public IniConfigBehavior(String path, String section, ICryptKey crypt, ConfigOptions options = ConfigOptions.None)
            : this(path, section, DefaultJoiner, crypt, options)
        {
        }
        
        public IniConfigBehavior(String path, String section, String joiner, ICryptKey crypt, ConfigOptions options = ConfigOptions.None)
            : base(ValidatePathOrGetDefault(path, "ini"), crypt, options)
        {
            MainSection = section ?? DefaultSection;
            Joiner = joiner ?? DefaultJoiner;
        }

        protected String ToSection(IEnumerable<String> sections)
        {
            String join = Joiner.Join(sections);

            return String.IsNullOrEmpty(join) ? MainSection : join;
        }

        protected virtual String Get(String key, String section)
        {
            if (GetPrivateProfileString(section, key, String.Empty, Buffer, 255, Path) == 0)
            {
                return null;
            }

            String result = Buffer.Pop();
            return String.IsNullOrEmpty(result) ? null : result;
        }

        public override String Get(String key, IEnumerable<String> sections)
        {
            return Get(key, ToSection(sections));
        }

        protected virtual Boolean Set(String key, String value, String section)
        {
            if (WritePrivateProfileString(section, key, value, Path) == 0)
            {
                InteropUtils.ThrowLastWin32Exception();
            }
            
            return true;
        }

        public override Boolean Set(String key, String value, IEnumerable<String> sections)
        {
            return Set(key, value, ToSection(sections));
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern Int32 WritePrivateProfileString(String section, String key, String value, String path);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern Int32 GetPrivateProfileString(String section, String key, String @default, StringBuilder retVal, Int32 size, String path);
    }
}