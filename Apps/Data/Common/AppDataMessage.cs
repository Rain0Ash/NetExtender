// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Apps.Data.Common
{
    [Serializable]
    public readonly struct AppDataMessage
    {
        public Guid Guid { get; }
        
        public String AppName { get; }
        
        public String AppShortName { get; }
        
        public DateTime StartedAt { get; }

        public AppVersion Version { get; }
        public AppInformation Information { get; }

        public AppStatus Status { get; }

        public AppBranch Branch { get; }

        public AppDataMessage(Guid guid, String appName, String appShortName, DateTime startedAt, AppVersion version, AppInformation information, AppStatus status, AppBranch branch)
        {
            Guid = guid;
            AppName = appName;
            AppShortName = appShortName;
            StartedAt = startedAt;
            Version = version;
            Information = information;
            Status = status;
            Branch = branch;
        }
    }
}