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

        protected override String? TryGetValue(String? key)
        {
            return EnvironmentUtilities.TryGetEnvironmentVariable(key, Target);
        }

        protected override Task<String?> TryGetValueAsync(String? key, CancellationToken token)
        {
            return TryGetValue(key).ToTask();
        }

        protected override Boolean TrySetValue(String? key, String? value)
        {
            return EnvironmentUtilities.TrySetEnvironmentVariable(key, value, Target);
        }

        protected override Task<Boolean> TrySetValueAsync(String? key, String? value, CancellationToken token)
        {
            return TrySetValue(key, value).ToTask();
        }

        protected override String[]? TryGetExists()
        {
            return EnvironmentUtilities.TryGetExistsEnvironmentVariables(Target);
        }

        protected override Task<String[]?> TryGetExistsAsync(CancellationToken token)
        {
            return TryGetExists().ToTask();
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
            return TryGetExistsValues().ToTask();
        }
    }
}