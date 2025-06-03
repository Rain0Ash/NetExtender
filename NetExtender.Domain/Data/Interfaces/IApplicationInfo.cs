// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Domains.Interfaces
{
    public interface IApplicationInfo : IEquality<IApplicationInfo>, IDisposable
    {
        public static Boolean operator >(IApplicationInfo first, IApplicationInfo second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator <(IApplicationInfo first, IApplicationInfo second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator >=(IApplicationInfo first, IApplicationInfo second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <=(IApplicationInfo first, IApplicationInfo second)
        {
            return first.CompareTo(second) <= 0;
        }

        public ApplicationInfoMessage Message { get; }
        public Guid Guid { get; }
        public String ApplicationName { get; }
        public String ApplicationIdentifier { get; }
        public DateTime StartedAt { get; }
        public ApplicationVersion Version { get; }
        public ApplicationInformation Information { get; }
        public ApplicationStatus Status { get; }
        public String StatusInfo { get; }
        public ApplicationBranch Branch { get; }
        public String BranchInfo { get; }
        public Boolean HasAnotherInstance { get; }
    }
}