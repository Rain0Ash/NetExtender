// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using NetExtender.FileSystems.Interfaces;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.IO;

namespace NetExtender.FileSystems
{
    public partial class FileSystemHandler
    {
        private static partial class Handler
        {
            private delegate void SetCommandLineArgsDelegate(String[] arguments);
            
            private static SetCommandLineArgsDelegate? SetCommandLineArgsHandler { get; }
            
            [ReflectionSignature]
            public static void SetCommandLineArgs(String[] arguments)
            {
                if (SetCommandLineArgsHandler is not { } handler)
                {
                    throw new NotSupportedException();
                }
                
                handler.Invoke(arguments);
            }
        }
        
        public virtual String NewLine
        {
            get
            {
                return System.Environment.NewLine;
            }
        }

        public virtual String CommandLine
        {
            get
            {
                return System.Environment.CommandLine;
            }
        }

        public virtual Int32 CurrentManagedThreadId
        {
            get
            {
                return System.Environment.CurrentManagedThreadId;
            }
        }

        public virtual Int32 ProcessId
        {
            get
            {
                return System.Environment.ProcessId;
            }
        }

        public virtual String? ProcessPath
        {
            get
            {
                return System.Environment.ProcessPath;
            }
        }

        public virtual Boolean Is64BitProcess
        {
            get
            {
                return System.Environment.Is64BitProcess;
            }
        }

        public virtual Boolean Is64BitOperatingSystem
        {
            get
            {
                return System.Environment.Is64BitOperatingSystem;
            }
        }

        public virtual Boolean IsSingleProcessor
        {
            get
            {
                return ProcessorCount == 1;
            }
        }

        public virtual Int32 ProcessorCount
        {
            get
            {
                return System.Environment.ProcessorCount;
            }
        }

        public virtual Int32 TickCount
        {
            get
            {
                return System.Environment.TickCount;
            }
        }

        public virtual Int64 TickCount64
        {
            get
            {
                return System.Environment.TickCount64;
            }
        }

        public virtual Int32 SystemPageSize
        {
            get
            {
                return System.Environment.SystemPageSize;
            }
        }

        public virtual Int64 WorkingSet
        {
            get
            {
                return System.Environment.WorkingSet;
            }
        }

        public virtual String MachineName
        {
            get
            {
                return System.Environment.MachineName;
            }
        }

        public virtual String UserName
        {
            get
            {
                return System.Environment.UserName;
            }
        }

        public virtual String UserDomainName
        {
            get
            {
                return System.Environment.UserDomainName;
            }
        }

        public virtual Boolean UserInteractive
        {
            get
            {
                return System.Environment.UserInteractive;
            }
        }

        public virtual String StackTrace
        {
            get
            {
                return System.Environment.StackTrace;
            }
        }

        public virtual Version Version
        {
            get
            {
                return System.Environment.Version;
            }
        }

        public virtual OperatingSystem OSVersion
        {
            get
            {
                return System.Environment.OSVersion;
            }
        }

        public virtual String CurrentDirectory
        {
            get
            {
                return System.Environment.CurrentDirectory;
            }
            set
            {
                System.Environment.CurrentDirectory = value;
            }
        }

        public virtual String SystemDirectory
        {
            get
            {
                return System.Environment.SystemDirectory;
            }
        }

        public virtual Int32 ExitCode
        {
            get
            {
                return System.Environment.ExitCode;
            }
            set
            {
                System.Environment.ExitCode = value;
            }
        }

        public virtual Boolean HasShutdownStarted
        {
            get
            {
                return System.Environment.HasShutdownStarted;
            }
        }

        public virtual String[] GetCommandLineArgs()
        {
            return System.Environment.GetCommandLineArgs();
        }

        public virtual Boolean SetCommandLineArgs(String[]? arguments)
        {
            try
            {
                Handler.SetCommandLineArgs(arguments ?? Array.Empty<String>());
                return true;
            }
            catch (NotSupportedException)
            {
                return false;
            }
        }

        public virtual Boolean ResetCommandLineArgs()
        {
            Handler.SetCommandLineArgs(null!);
            return SetCommandLineArgs(GetCommandLineArgs());
        }

        public virtual Boolean ClearCommandLineArgs()
        {
            return SetCommandLineArgs(null);
        }

        [return: NotNullIfNotNull("variable")]
        public virtual String? ExpandEnvironmentVariables(String? variable)
        {
            return variable is not null ? System.Environment.ExpandEnvironmentVariables(variable) : null;
        }

        public virtual String? GetEnvironmentVariable(String? variable)
        {
            return variable is not null ? System.Environment.GetEnvironmentVariable(variable) : null;
        }

        public virtual String? GetEnvironmentVariable(String? variable, EnvironmentVariableTarget target)
        {
            return variable is not null ? System.Environment.GetEnvironmentVariable(variable, target) : null;
        }

        [return: NotNullIfNotNull("variables")]
        protected virtual ImmutableDictionary<String, String?>? WrapEnvironmentVariables(IDictionary? variables)
        {
            return variables is not null ? EnvironmentUtilities.WrapEnvironmentVariables(variables) : null;
        }

        public virtual IReadOnlyDictionary<String, String?> GetEnvironmentVariables()
        {
            return WrapEnvironmentVariables(System.Environment.GetEnvironmentVariables());
        }

        public virtual IReadOnlyDictionary<String, String?> GetEnvironmentVariables(EnvironmentVariableTarget target)
        {
            return WrapEnvironmentVariables(System.Environment.GetEnvironmentVariables(target));
        }

        public virtual Boolean SetEnvironmentVariable(String? variable, String? value)
        {
            if (String.IsNullOrEmpty(variable))
            {
                return false;
            }
            
            System.Environment.SetEnvironmentVariable(variable, value);
            return true;
        }

        public virtual Boolean SetEnvironmentVariable(String? variable, String? value, EnvironmentVariableTarget target)
        {
            if (String.IsNullOrEmpty(variable))
            {
                return false;
            }
            
            System.Environment.SetEnvironmentVariable(variable, value, target);
            return true;
        }

        public virtual IDirectoryInfo? GetFolderPath(Environment.SpecialFolder folder)
        {
            return folder.ToDirectoryInfo() is { } directory ? new DirectoryInfoWrapper(directory) : null;
        }

        public virtual IDirectoryInfo? GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
        {
            return folder.ToDirectoryInfo(option) is { } directory ? new DirectoryInfoWrapper(directory) : null;
        }

        public virtual void Exit(Int32 code)
        {
            System.Environment.Exit(code);
        }

        public virtual void FailFast(String? message)
        {
            System.Environment.FailFast(message);
        }

        public virtual void FailFast(String? message, Exception? exception)
        {
            System.Environment.FailFast(message, exception);
        }
    }
}