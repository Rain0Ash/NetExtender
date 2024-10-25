// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior;
using NetExtender.Configuration.Common;
using NetExtender.Types.Environments;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Environment
{
    public class EnvironmentConfigBehavior : SingleKeyConfigBehavior
    {
        public EnvironmentVariableTarget Target { get; }

        public EnvironmentConfigBehavior(EnvironmentVariableTarget target)
            : this(target, ConfigOptions.None)
        {
        }

        public EnvironmentConfigBehavior(EnvironmentVariableTarget target, ConfigOptions options)
            : this(null, target, options)
        {
        }

        public EnvironmentConfigBehavior(String? path, EnvironmentVariableTarget target)
            : this(path, target, ConfigOptions.None)
        {
        }

        public EnvironmentConfigBehavior(String? path, EnvironmentVariableTarget target, ConfigOptions options)
            : base(path ?? nameof(System.Environment), options)
        {
            Target = target;
        }

        protected override String? TryGetValue(String? key)
        {
            return EnvironmentUtilities.TryGetEnvironmentVariable(key, Target);
        }

        protected override Task<String?> TryGetValueAsync(String? key, CancellationToken token)
        {
            return Task.Run(() => TryGetValue(key), token);
        }

        protected override Boolean TrySetValue(String? key, String? value)
        {
            return EnvironmentUtilities.TrySetEnvironmentVariable(key, value, Target);
        }

        protected override Task<Boolean> TrySetValueAsync(String? key, String? value, CancellationToken token)
        {
            return Task.Run(() => TrySetValue(key, value), token);
        }

        protected override String[]? TryGetExists()
        {
            return EnvironmentUtilities.TryGetExistsEnvironmentVariables(Target);
        }

        protected override Task<String[]?> TryGetExistsAsync(CancellationToken token)
        {
            return Task.Run(TryGetExists, token);
        }

        protected override ConfigurationSingleKeyEntry[]? TryGetExistsValues()
        {
            static ConfigurationSingleKeyEntry Convert(EnvironmentValueEntry entry)
            {
                return new ConfigurationSingleKeyEntry(entry.Key, entry.Value);
            }
            
            return EnvironmentUtilities.TryGetExistsValuesEnvironmentVariables(Target)?.Select(Convert).ToArray();
        }

        protected override Task<ConfigurationSingleKeyEntry[]?> TryGetExistsValuesAsync(CancellationToken token)
        {
            return Task.Run(TryGetExistsValues, token);
        }
    }
}