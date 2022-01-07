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
        public EnvironmentVariableTarget Target { get; init; } = EnvironmentVariableTarget.Process;
        
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

        protected virtual Boolean Deconstruct(String? entry, [NotNullIfNotNull("entry")] out String? key, out IEnumerable<String>? sections)
        {
            if (entry is null)
            {
                key = default;
                sections = default;
                return true;
            }

            String[] split = entry.Split(Joiner);
            switch (split.Length)
            {
                case 0:
                    key = String.Empty;
                    sections = default;
                    return true;
                case 1:
                    key = split[0];
                    sections = default;
                    return true;
                default:
                    key = split[0];
                    sections = split.Skip(1);
                    return true;
            }
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
                    
                    System.Environment.SetEnvironmentVariable(key, value, Target);
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
                
                System.Environment.SetEnvironmentVariable(key, value, Target);

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
        
        protected virtual ConfigurationEntry EntriesConvert(String entry)
        {
            return new ConfigurationEntry(entry);
        }
        
        protected virtual ConfigurationValueEntry ValueEntriesConvert(String entry)
        {
            return new ConfigurationValueEntry(entry, Get(entry, null));
        }

        public override ConfigurationEntry[]? GetExists(IEnumerable<String>? sections)
        {
            try
            {
                if (sections is null)
                {
                    return System.Environment.GetEnvironmentVariables(Target).Keys.OfType<String>().Select(EntriesConvert).ToArray();
                }

                sections = sections.Materialize(out Int32 count);
                
                Boolean IsEqualSections(String entry)
                {
                    return Deconstruct(entry, out _, out IEnumerable<String>? sequence) && (sequence?.SequencePartialEqual(sections) ?? count <= 0);
                }

                return System.Environment.GetEnvironmentVariables(Target).Keys.OfType<String>().Where(IsEqualSections).Select(EntriesConvert).ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override ConfigurationValueEntry[]? GetExistsValues(IEnumerable<String>? sections)
        {
            try
            {
                if (sections is null)
                {
                    return System.Environment.GetEnvironmentVariables(Target).Keys.OfType<String>().Select(ValueEntriesConvert).ToArray();
                }

                sections = sections.Materialize(out Int32 count);
                
                Boolean IsEqualSections(String entry)
                {
                    return Deconstruct(entry, out _, out IEnumerable<String>? sequence) && (sequence?.SequencePartialEqual(sections) ?? count <= 0);
                }

                return System.Environment.GetEnvironmentVariables(Target).Keys.OfType<String>().Where(IsEqualSections).Select(ValueEntriesConvert).ToArray();
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