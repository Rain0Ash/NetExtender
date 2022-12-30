// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.File;
using NetExtender.Serialization.Ini;
using NetExtender.Types.Trees;

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
        
        protected virtual String[]? UnpackSection(String? section)
        {
            return !String.IsNullOrEmpty(section) && section != MainSection ? section.Split(Joiner) : null;
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

                        tree.Add(key, value.Value, UnpackSection(section));
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

                FlattenDictionaryTreeEntry<String, String>[]? flatten = Config.Flatten(Joiner);

                if (flatten is null)
                {
                    return null;
                }

                foreach (IGrouping<String, FlattenDictionaryTreeEntry<String, String>> grouping in flatten.GroupBy(entry => entry.Section ?? MainSection))
                {
                    IniSection section = new IniSection();
                    foreach (FlattenDictionaryTreeEntry<String, String> entry in grouping)
                    {
                        section.Add(entry.Key, entry.Value);
                    }
                    
                    file.Add(grouping.Key, section);
                }

                return file.Write();
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected override async Task<String?> SerializeConfigAsync(CancellationToken token)
        {
            try
            {
                IniFile file = new IniFile(StringComparer.Ordinal);

                FlattenDictionaryTreeEntry<String, String>[]? flatten = Config.Flatten(Joiner);

                if (flatten is null)
                {
                    return null;
                }

                foreach (IGrouping<String, FlattenDictionaryTreeEntry<String, String>> grouping in flatten.GroupBy(entry => entry.Section ?? MainSection))
                {
                    IniSection section = new IniSection();
                    foreach (FlattenDictionaryTreeEntry<String, String> entry in grouping)
                    {
                        section.Add(entry.Key, entry.Value);
                    }
                    
                    file.Add(grouping.Key, section);
                }

                return await file.WriteAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}