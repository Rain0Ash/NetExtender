// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Domains
{
    [Serializable]
    public readonly struct ApplicationInfoMessage
    {
        public Guid Guid { get; }
        
        public String AppName { get; }
        
        public String AppShortName { get; }
        
        public DateTime StartedAt { get; }

        public ApplicationVersion Version { get; }
        public ApplicationInfo Information { get; }

        public ApplicationStatus Status { get; }

        public ApplicationBranch Branch { get; }

        public ApplicationInfoMessage(Guid guid, String name, String shortname, DateTime startedAt, ApplicationVersion version, ApplicationInfo information, ApplicationStatus status, ApplicationBranch branch)
        {
            Guid = guid;
            AppName = name;
            AppShortName = shortname;
            StartedAt = startedAt;
            Version = version;
            Information = information;
            Status = status;
            Branch = branch;
        }
    }
}