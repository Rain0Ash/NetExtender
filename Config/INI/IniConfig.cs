// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NetExtender.Config.Common;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Config.Ini
{
    public class IniConfig : Config
    {
        public const String DefaultJoiner = ".";
        public const String DefaultSection = "Main";
        
        public String MainSection { get; }
        public String Joiner { get; }
        
        protected StringBuilder Buffer { get; } = new StringBuilder(255);

        public IniConfig(String path = null, ConfigOptions options = ConfigOptions.None)
            : this(path, DefaultSection, options)
        {
        }
        
        public IniConfig(String path, String section, ConfigOptions options = ConfigOptions.None)
            : this(path, section, DefaultJoiner, options)
        {
        }
        
        public IniConfig(String path, String section, String joiner = DefaultJoiner, ConfigOptions options = ConfigOptions.None)
            : this(path, section, joiner, null, options)
        {
        }
        
        public IniConfig(String path, ICryptKey crypt, ConfigOptions options = ConfigOptions.None)
            : this(path, DefaultSection, crypt, options)
        {
        }

        public IniConfig(String path, String section, ICryptKey crypt, ConfigOptions options = ConfigOptions.None)
            : this(path, section, DefaultJoiner, crypt, options)
        {
        }
        
        public IniConfig(String path, String section, String joiner, ICryptKey crypt, ConfigOptions options = ConfigOptions.None)
            : base(ValidatePathOrGetDefault(path, "ini"), crypt, options)
        {
            MainSection = section ?? DefaultSection;
            Joiner = joiner ?? DefaultJoiner;
        }

        protected String ToSection(params String[] sections)
        {
            return sections.Length switch
            {
                0 => MainSection,
                1 => sections[0],
                _ => String.Join(Joiner, sections.Where(StringUtils.IsNotNullOrEmpty))
            };
        }

        protected virtual String Get(String key, String section)
        {
            GetPrivateProfileString(section, key, String.Empty, Buffer, 255, Path);

            String result = Buffer.Pop();
            return String.IsNullOrEmpty(result) ? null : result;
        }

        protected override String Get(String key, params String[] sections)
        {
            return Get(key, ToSection(sections));
        }

        protected virtual Boolean Set(String key, String value, String section)
        {
            WritePrivateProfileString(section, key, value, Path);
            return true;
        }

        protected override Boolean Set(String key, String value, params String[] sections)
        {
            return Set(key, value, ToSection(sections));
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern Int64 WritePrivateProfileString(String section, String key, String value, String path);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern Int32 GetPrivateProfileString(String section, String key, String @default, StringBuilder retVal, Int32 size, String path);
    }
}