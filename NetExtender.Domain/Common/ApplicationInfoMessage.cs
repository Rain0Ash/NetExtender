// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Domains
{
    [Serializable]
    public record ApplicationInfoMessage
    {
        public Guid Guid { get; }

        public String ApplicationName { get; }

        public String ApplicationIdentifier { get; }

        public DateTime StartedAt { get; }

        public ApplicationVersion Version { get; }
        public ApplicationInfo Information { get; }

        public ApplicationStatus Status { get; }

        public ApplicationBranch Branch { get; }

        public ApplicationInfoMessage(Guid guid, String name, String identifier, DateTime startedAt, ApplicationVersion version, ApplicationInfo information, ApplicationStatus status, ApplicationBranch branch)
        {
            Guid = guid;
            ApplicationName = name;
            ApplicationIdentifier = identifier;
            StartedAt = startedAt;
            Version = version;
            Information = information;
            Status = status;
            Branch = branch;
        }
    }
}