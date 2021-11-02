// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Configuration.Behavior;
using NetExtender.Configuration.Common;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Environment
{
    public class EnvironmentConfigBehavior : ConfigBehavior
    {
        public EnvironmentConfigBehavior()
            : this(ConfigOptions.None)
        {
        }
        
        public EnvironmentConfigBehavior(ConfigOptions options)
            : this(null, null, options)
        {
        }
        
        public EnvironmentConfigBehavior(ICryptKey? crypt)
            : this(crypt, ConfigOptions.None)
        {
        }
        
        public EnvironmentConfigBehavior(ICryptKey? crypt, ConfigOptions options)
            : this(null, crypt, options)
        {
        }

        public EnvironmentConfigBehavior(String? path)
            : this(path, ConfigOptions.None)
        {
        }

        public EnvironmentConfigBehavior(String? path, ConfigOptions options)
            : this(path, null, options)
        {
        }
        
        public EnvironmentConfigBehavior(String? path, ICryptKey? crypt)
            : this(path, crypt, ConfigOptions.None)
        {
        }
        
        public EnvironmentConfigBehavior(String? path, ICryptKey? crypt, ConfigOptions options)
            : base(path ?? nameof(System.Environment), crypt, options)
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
                return key is not null ? System.Environment.GetEnvironmentVariable(key) : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override Boolean Set(String? key, String? value, IEnumerable<String>? sections)
        {
            try
            {
                key = Join(key, sections);

                if (key is null)
                {
                    return false;
                }
                
                System.Environment.SetEnvironmentVariable(key, value);
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

        public override Boolean Reload()
        {
            return false;
        }
    }
}