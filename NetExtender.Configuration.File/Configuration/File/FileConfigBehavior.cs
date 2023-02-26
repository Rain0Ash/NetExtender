// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Memory;
using NetExtender.Types.Trees;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.File
{
    public abstract class FileConfigBehavior : MemoryConfigBehavior
    {
        public Encoding? Encoding { get; init; }

        protected FileConfigBehavior(String? path, ConfigOptions options)
            : base(ValidatePathOrGetDefault(path, "txt"), options | ConfigOptions.LazyWrite)
        {
            Config = ReadConfig();
        }

        protected String? ReadConfigText()
        {
            try
            {
                return System.IO.File.Exists(Path) ? System.IO.File.ReadAllText(Path, Encoding ?? Encoding.UTF8) : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected abstract DictionaryTree<String, String>? DeserializeConfig(String config);

        protected DictionaryTree<String, String> ReadConfig()
        {
            return ReadConfig(out DictionaryTree<String, String>? result) ? result : new DictionaryTree<String, String>();
        }

        protected Boolean ReadConfig([MaybeNullWhen(false)] out DictionaryTree<String, String> result)
        {
            try
            {
                String? config = ReadConfigText();

                if (config is null)
                {
                    result = default;
                    return false;
                }

                result = DeserializeConfig(config);
                return result is not null;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        protected Boolean WriteConfigText(String? config)
        {
            if (String.IsNullOrEmpty(config))
            {
                return false;
            }

            try
            {
                System.IO.File.WriteAllText(Path, config, Encoding ?? Encoding.UTF8);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected async Task<Boolean> WriteConfigTextAsync(String? config, CancellationToken token)
        {
            if (String.IsNullOrEmpty(config))
            {
                return false;
            }

            try
            {
                await System.IO.File.WriteAllTextAsync(Path, config, token);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected abstract String? SerializeConfig();

        protected Task<String?> SerializeConfigAsync()
        {
            return SerializeConfigAsync(CancellationToken.None);
        }

        protected virtual Task<String?> SerializeConfigAsync(CancellationToken token)
        {
            return !token.IsCancellationRequested ? SerializeConfig().ToTask() : StringUtilities.Null;
        }

        protected Boolean WriteConfig()
        {
            String? config = SerializeConfig();
            return WriteConfigText(config);
        }

        protected Task<Boolean> WriteConfigAsync()
        {
            return WriteConfigAsync(CancellationToken.None);
        }

        protected Task<Boolean> WriteConfigAsync(CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return TaskUtilities.False;
            }

            String? config = SerializeConfig();
            return WriteConfigTextAsync(config, token);
        }

        public override Boolean Set(String? key, String? value, IEnumerable<String>? sections)
        {
            return base.Set(key, value, sections) && WriteConfig();
        }

        public override async Task<Boolean> SetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return await base.SetAsync(key, value, sections, token) && await WriteConfigAsync(token);
        }

        public override Boolean Clear(IEnumerable<String>? sections)
        {
            return base.Clear(sections) && WriteConfig();
        }

        public override Boolean Merge(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return base.Merge(entries) && WriteConfig();
        }

        public override async Task<Boolean> MergeAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return await base.MergeAsync(entries, token) && await WriteConfigAsync(token);
        }

        public override Boolean Replace(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return base.Replace(entries) && WriteConfig();
        }

        public override async Task<Boolean> ReplaceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return await base.ReplaceAsync(entries, token) && await WriteConfigAsync(token);
        }

        public override Boolean Reload()
        {
            if (!ReadConfig(out DictionaryTree<String, String>? config))
            {
                return false;
            }

            if (!Reset())
            {
                return false;
            }

            Config.AddRange(config);
            return true;
        }
    }
}