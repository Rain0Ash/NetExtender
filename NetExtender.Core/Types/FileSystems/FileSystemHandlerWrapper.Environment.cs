// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.FileSystems.Interfaces;

namespace NetExtender.FileSystems
{
    public partial class FileSystemHandlerWrapper<T>
    {
        public override String NewLine
        {
            get
            {
                return FileSystem.Environment.NewLine;
            }
        }

        public override String CommandLine
        {
            get
            {
                return FileSystem.Environment.CommandLine;
            }
        }

        public override Int32 CurrentManagedThreadId
        {
            get
            {
                return FileSystem.Environment.CurrentManagedThreadId;
            }
        }

        public override Int32 ProcessId
        {
            get
            {
                return FileSystem.Environment.ProcessId;
            }
        }

        public override String? ProcessPath
        {
            get
            {
                return FileSystem.Environment.ProcessPath;
            }
        }

        public override Boolean Is64BitProcess
        {
            get
            {
                return FileSystem.Environment.Is64BitProcess;
            }
        }

        public override Boolean Is64BitOperatingSystem
        {
            get
            {
                return FileSystem.Environment.Is64BitOperatingSystem;
            }
        }

        public override Boolean IsSingleProcessor
        {
            get
            {
                return FileSystem.Environment.IsSingleProcessor;
            }
        }

        public override Int32 ProcessorCount
        {
            get
            {
                return FileSystem.Environment.ProcessorCount;
            }
        }

        public override Int32 TickCount
        {
            get
            {
                return FileSystem.Environment.TickCount;
            }
        }

        public override Int64 TickCount64
        {
            get
            {
                return FileSystem.Environment.TickCount64;
            }
        }

        public override Int32 SystemPageSize
        {
            get
            {
                return FileSystem.Environment.SystemPageSize;
            }
        }

        public override Int64 WorkingSet
        {
            get
            {
                return FileSystem.Environment.WorkingSet;
            }
        }

        public override String MachineName
        {
            get
            {
                return FileSystem.Environment.MachineName;
            }
        }

        public override String UserName
        {
            get
            {
                return FileSystem.Environment.UserName;
            }
        }

        public override String UserDomainName
        {
            get
            {
                return FileSystem.Environment.UserDomainName;
            }
        }

        public override Boolean UserInteractive
        {
            get
            {
                return FileSystem.Environment.UserInteractive;
            }
        }

        public override String StackTrace
        {
            get
            {
                return FileSystem.Environment.StackTrace;
            }
        }

        public override Version Version
        {
            get
            {
                return FileSystem.Environment.Version;
            }
        }

        public override OperatingSystem OSVersion
        {
            get
            {
                return FileSystem.Environment.OSVersion;
            }
        }

        public override String CurrentDirectory
        {
            get
            {
                return FileSystem.Environment.CurrentDirectory;
            }
            set
            {
                FileSystem.Environment.CurrentDirectory = value;
            }
        }

        public override String SystemDirectory
        {
            get
            {
                return FileSystem.Environment.SystemDirectory;
            }
        }

        public override Int32 ExitCode
        {
            get
            {
                return FileSystem.Environment.ExitCode;
            }
            set
            {
                FileSystem.Environment.ExitCode = value;
            }
        }

        public override Boolean HasShutdownStarted
        {
            get
            {
                return FileSystem.Environment.HasShutdownStarted;
            }
        }

        public override String[] GetCommandLineArgs()
        {
            return FileSystem.Environment.GetCommandLineArgs();
        }

        public override Boolean SetCommandLineArgs(String[]? arguments)
        {
            return FileSystem.Environment.SetCommandLineArgs(arguments);
        }

        public override Boolean ResetCommandLineArgs()
        {
            return FileSystem.Environment.ResetCommandLineArgs();
        }

        public override Boolean ClearCommandLineArgs()
        {
            return FileSystem.Environment.ClearCommandLineArgs();
        }

        [return: NotNullIfNotNull("variable")]
        public override String? ExpandEnvironmentVariables(String? variable)
        {
            return FileSystem.Environment.ExpandEnvironmentVariables(variable);
        }

        public override String? GetEnvironmentVariable(String? variable)
        {
            return FileSystem.Environment.GetEnvironmentVariable(variable);
        }

        public override String? GetEnvironmentVariable(String? variable, EnvironmentVariableTarget target)
        {
            return FileSystem.Environment.GetEnvironmentVariable(variable, target);
        }

        public override IReadOnlyDictionary<String, String?> GetEnvironmentVariables()
        {
            return FileSystem.Environment.GetEnvironmentVariables();
        }

        public override IReadOnlyDictionary<String, String?> GetEnvironmentVariables(EnvironmentVariableTarget target)
        {
            return FileSystem.Environment.GetEnvironmentVariables(target);
        }

        public override Boolean SetEnvironmentVariable(String? variable, String? value)
        {
            return FileSystem.Environment.SetEnvironmentVariable(variable, value);
        }

        public override Boolean SetEnvironmentVariable(String? variable, String? value, EnvironmentVariableTarget target)
        {
            return FileSystem.Environment.SetEnvironmentVariable(variable, value, target);
        }

        public override IDirectoryInfo? GetFolderPath(Environment.SpecialFolder folder)
        {
            return FileSystem.Environment.GetFolderPath(folder);
        }

        public override IDirectoryInfo? GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
        {
            return FileSystem.Environment.GetFolderPath(folder, option);
        }

        public override void Exit(Int32 code)
        {
            FileSystem.Environment.Exit(code);
        }

        public override void FailFast(String? message)
        {
            FileSystem.Environment.FailFast(message);
        }

        public override void FailFast(String? message, Exception? exception)
        {
            FileSystem.Environment.FailFast(message, exception);
        }
    }
}