// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Interfaces.Notify;

namespace NetExtender.FileSystems.Interfaces
{
    public interface IEnvironmentHandler : INotifyProperty, IDisposable
    {
        public Object SyncRoot { get; }
        public Boolean IsSynchronized { get; }
        public Guid Id { get; }
        public String? Name { get; }
        public DateTime CreationTime { get; }
        public DateTime CreationTimeUtc { get; }
        public Boolean IsReal { get; }
        public StringComparer Comparer { get; }
        public Boolean IsCaseSensitive { get; }

        /// <inheritdoc cref="System.Environment.NewLine" />
        public String NewLine { get; }

        /// <inheritdoc cref="System.Environment.CommandLine" />
        public String CommandLine { get; }

        /// <inheritdoc cref="System.Environment.CurrentManagedThreadId" />
        public Int32 CurrentManagedThreadId { get; }

        /// <inheritdoc cref="System.Environment.ProcessId" />
        public Int32 ProcessId { get; }

        /// <inheritdoc cref="System.Environment.ProcessPath" />
        public String? ProcessPath { get; }

        /// <inheritdoc cref="System.Environment.Is64BitProcess" />
        public Boolean Is64BitProcess { get; }

        /// <inheritdoc cref="System.Environment.Is64BitOperatingSystem" />
        public Boolean Is64BitOperatingSystem { get; }

        public Boolean IsSingleProcessor { get; }

        /// <inheritdoc cref="System.Environment.ProcessorCount" />
        public Int32 ProcessorCount { get; }

        /// <inheritdoc cref="System.Environment.TickCount" />
        public Int32 TickCount { get; }

        /// <inheritdoc cref="System.Environment.TickCount64" />
        public Int64 TickCount64 { get; }

        /// <inheritdoc cref="System.Environment.SystemPageSize" />
        public Int32 SystemPageSize { get; }

        /// <inheritdoc cref="System.Environment.WorkingSet" />
        public Int64 WorkingSet { get; }

        /// <inheritdoc cref="System.Environment.MachineName" />
        public String MachineName { get; }

        /// <inheritdoc cref="System.Environment.UserName" />
        public String UserName { get; }

        /// <inheritdoc cref="System.Environment.UserDomainName" />
        public String UserDomainName { get; }

        /// <inheritdoc cref="System.Environment.UserInteractive" />
        public Boolean UserInteractive { get; }

        /// <inheritdoc cref="System.Environment.StackTrace" />
        public String StackTrace { get; }

        /// <inheritdoc cref="System.Environment.Version" />
        public Version Version { get; }

        /// <inheritdoc cref="System.Environment.OSVersion" />
        public OperatingSystem OSVersion { get; }

        /// <inheritdoc cref="System.Environment.CurrentDirectory" />
        public String CurrentDirectory { get; set; }

        /// <inheritdoc cref="System.Environment.SystemDirectory" />
        public String SystemDirectory { get; }

        /// <inheritdoc cref="System.Environment.ExitCode" />
        public Int32 ExitCode { get; set; }

        /// <inheritdoc cref="System.Environment.HasShutdownStarted" />
        public Boolean HasShutdownStarted { get; }

        /// <inheritdoc cref="System.Environment.GetLogicalDrives()" />
        public String[] GetLogicalDrives();

        /// <inheritdoc cref="System.Environment.GetCommandLineArgs()" />
        public String[] GetCommandLineArgs();
        public Boolean SetCommandLineArgs(String[]? arguments);
        public Boolean ResetCommandLineArgs();
        public Boolean ClearCommandLineArgs();

        /// <inheritdoc cref="System.Environment.ExpandEnvironmentVariables(System.String)" />
        [return: NotNullIfNotNull("variable")]
        public String? ExpandEnvironmentVariables(String? variable);

        /// <inheritdoc cref="System.Environment.GetEnvironmentVariable(System.String)" />
        public String? GetEnvironmentVariable(String? variable);

        /// <inheritdoc cref="System.Environment.GetEnvironmentVariable(System.String, System.EnvironmentVariableTarget)" />
        public String? GetEnvironmentVariable(String? variable, EnvironmentVariableTarget target);

        /// <inheritdoc cref="System.Environment.GetEnvironmentVariables()" />
        public IReadOnlyDictionary<String, String?> GetEnvironmentVariables();

        /// <inheritdoc cref="System.Environment.GetEnvironmentVariables(System.EnvironmentVariableTarget)" />
        public IReadOnlyDictionary<String, String?> GetEnvironmentVariables(EnvironmentVariableTarget target);

        /// <inheritdoc cref="System.Environment.SetEnvironmentVariable(System.String, System.String?)" />
        public Boolean SetEnvironmentVariable(String? variable, String? value);

        /// <inheritdoc cref="System.Environment.SetEnvironmentVariable(System.String, System.String?, System.EnvironmentVariableTarget)" />
        public Boolean SetEnvironmentVariable(String? variable, String? value, EnvironmentVariableTarget target);

        /// <inheritdoc cref="System.Environment.GetFolderPath(System.Environment.SpecialFolder)" />
        public IDirectoryInfo? GetFolderPath(Environment.SpecialFolder folder);

        /// <inheritdoc cref="System.Environment.GetFolderPath(System.Environment.SpecialFolder, System.Environment.SpecialFolderOption)" />
        public IDirectoryInfo? GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option);

        /// <inheritdoc cref="System.Environment.Exit(System.Int32)" />
        public void Exit(Int32 code);

        /// <inheritdoc cref="System.Environment.FailFast(System.String?)" />
        public void FailFast(String? message);

        /// <inheritdoc cref="System.Environment.FailFast(System.String?, System.Exception?)" />
        public void FailFast(String? message, Exception? exception);
    }
}