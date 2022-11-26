// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.File;
using NetExtender.Serialization.Ini;
using NetExtender.Types.Trees;
using NetExtender.Types.Trees.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Ini
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
            : this(null, DefaultSection, options)
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

        public IniConfigBehavior(String? path, String? section)
            : this(path, section, ConfigOptions.None)
        {
        }

        public IniConfigBehavior(String? path, String? section, ConfigOptions options)
            : base(ValidatePathOrGetDefault(path, "ini"), options)
        {
            MainSection = section ?? DefaultSection;
        }

        [return: NotNullIfNotNull("sections")]
        protected override IEnumerable<String>? ToSection(IEnumerable<String>? sections)
        {
            if (sections is null)
            {
                return null;
            }

            String join = Joiner.Join(sections);
            return !String.IsNullOrEmpty(join) ? new[] { join } : new []{ MainSection };
        }

        protected override DictionaryTree<String, String>? DeserializeConfig(String config)
        {
            try
            {
                IniFile file = new IniFile(StringComparer.Ordinal);
                file.Read(config);

                DictionaryTree<String, String> tree = new DictionaryTree<String, String>();

                foreach ((String section, IniSection ini) in file)
                {
                    foreach ((String key, IniValue value) in ini)
                    {
                        if (value.Value is null)
                        {
                            continue;
                        }

                        tree.Add(key, value.Value, section);
                    }
                }

                return tree;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected override String? SerializeConfig()
        {
            try
            {
                IniFile file = new IniFile(StringComparer.Ordinal);

                foreach ((String section, IDictionaryTreeNode<String, String> node) in Config)
                {
                    IniSection ini = new IniSection();

                    foreach ((String key, IDictionaryTreeNode<String, String> value) in node)
                    {
                        ini.Add(key, value.Value);
                    }

                    file.Add(section, ini);
                }

                return file.Write();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}