// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Configuration.Behavior;
using NetExtender.Configuration.Common;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Environment
{
    public class EnvironmentConfigBehavior : ConfigBehavior
    {
        public EnvironmentVariableTarget Target { get; init; }
        
        public EnvironmentConfigBehavior()
            : this(ConfigOptions.None)
        {
        }
        
        public EnvironmentConfigBehavior(ConfigOptions options)
            : this(null, options)
        {
        }

        public EnvironmentConfigBehavior(String? path)
            : this(path, ConfigOptions.None)
        {
        }
        
        public EnvironmentConfigBehavior(String? path, ConfigOptions options)
            : base(path ?? nameof(System.Environment), options)
        {
        }
        
        [return: NotNullIfNotNull("key")]
        protected virtual String? Join(String? key, IEnumerable<String>? sections)
        {
            if (key is null)
            {
                return null;
            }
            
            return sections is not null ? Joiner.Join(sections.Append(key)) : key;
        }

        public override String? Get(String? key, IEnumerable<String>? sections)
        {
            try
            {
                key = Join(key, sections);
                return key is not null ? System.Environment.GetEnvironmentVariable(key, Target) : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // ReSharper disable once CognitiveComplexity
        public override Boolean Set(String? key, String? value, IEnumerable<String>? sections)
        {
            if (IsReadOnly)
            {
                return false;
            }
            
            try
            {
                if (IsIgnoreEvent && !IsLazyWrite)
                {
                    key = Join(key, sections);
                    
                    if (key is null)
                    {
                        return false;
                    }
                    
                    System.Environment.SetEnvironmentVariable(key, value);
                    return true;
                }
                
                sections = ToSection(sections).AsIImmutableList();
                
                if (IsLazyWrite && Get(key, sections) == value)
                {
                    return true;
                }
                
                key = Join(key, sections);
                    
                if (key is null)
                {
                    return false;
                }
                
                System.Environment.SetEnvironmentVariable(key, value);

                if (!IsIgnoreEvent)
                {
                    OnChanged(new ConfigurationValueEntry(key, value, sections));
                }
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override ConfigurationEntry[]? GetExists()
        {
            try
            {
                return System.Environment.GetEnvironmentVariables().Keys.OfType<String>().Select(key => new ConfigurationEntry(key)).ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public override ConfigurationValueEntry[]? GetExistsValues()
        {
            try
            {
                return System.Environment.GetEnvironmentVariables().Keys.OfType<String>().Select(key => new ConfigurationValueEntry(key, Get(key, null))).ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override Boolean Reload()
        {
            return false;
        }
    }
}