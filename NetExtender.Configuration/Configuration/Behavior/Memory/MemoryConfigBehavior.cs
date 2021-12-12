// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Configuration.Behavior;
using NetExtender.Configuration.Common;
using NetExtender.Types.Trees;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Memory
{
    public class MemoryConfigBehavior : ConfigBehavior
    {
        protected DictionaryTree<String, String> Config { get; set; }

        public MemoryConfigBehavior()
            : this(ConfigOptions.None)
        {
        }
        
        public MemoryConfigBehavior(ConfigOptions options)
            : this(new DictionaryTree<String, String>(), null, options)
        {
        }

        public MemoryConfigBehavior(String? path)
            : this(path, ConfigOptions.None)
        {
        }
        
        public MemoryConfigBehavior(String? path, ConfigOptions options)
            : this(new DictionaryTree<String, String>(), path, options)
        {
        }
        
        public MemoryConfigBehavior(DictionaryTree<String, String> config)
            : this(config, ConfigOptions.None)
        {
        }
        
        public MemoryConfigBehavior(DictionaryTree<String, String> config, ConfigOptions options)
            : this(config, null, options)
        {
        }

        public MemoryConfigBehavior(DictionaryTree<String, String> config, String? path)
            : this(config, path, ConfigOptions.None)
        {
        }
        
        public MemoryConfigBehavior(DictionaryTree<String, String> config, String? path, ConfigOptions options)
            : base(path ?? "RAM", options)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public override String? Get(String? key, IEnumerable<String>? sections)
        {
            return key is not null ? Config[key, ToSection(sections)].Value : null;
        }

        // ReSharper disable once CognitiveComplexity
        public override Boolean Set(String? key, String? value, IEnumerable<String>? sections)
        {
            if (IsReadOnly)
            {
                return false;
            }
            
            if (key is null)
            {
                return false;
            }
            
            if (IsIgnoreEvent && !IsLazyWrite)
            {
                if (value is null)
                {
                    return Config.Remove(key, sections);
                }
                
                Config[key, sections].Value = value;
                return true;
            }
            
            sections = ToSection(sections).AsIImmutableList();
            
            if (IsLazyWrite && Get(key, sections) == value)
            {
                return true;
            }

            if (value is null)
            {
                if (IsIgnoreEvent)
                {
                    return Config.Remove(key, sections);
                }

                if (!Config.Remove(key, sections))
                {
                    return false;
                }

                OnChanged(new ConfigurationValueEntry(key, value, sections));
                return true;
            }

            Config[key, sections].Value = value;

            if (!IsIgnoreEvent)
            {
                OnChanged(new ConfigurationValueEntry(key, value, sections));
            }
            
            return true;
        }

        public override ConfigurationEntry[]? GetExists()
        {
            return Config.Dump()?.Select(entry => new ConfigurationEntry(entry.Key, entry.Sections)).ToArray();
        }
        
        public override ConfigurationValueEntry[]? GetExistsValues()
        {
            return Config.Dump()?.Select(entry => new ConfigurationValueEntry(entry.Key, entry.Value, entry.Sections)).ToArray();
        }

        public override Boolean Reload()
        {
            return false;
        }
    }
}