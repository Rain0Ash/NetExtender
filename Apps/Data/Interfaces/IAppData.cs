// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Apps.Data.Common;

namespace NetExtender.Apps.Data.Interfaces
{
    public interface IAppData : IComparable<IAppData>, IEquatable<IAppData>, IDisposable
    {
        public static Boolean operator >(IAppData first, IAppData second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator <(IAppData first, IAppData second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator >=(IAppData first, IAppData second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <=(IAppData first, IAppData second)
        {
            return first.CompareTo(second) <= 0;
        }

        public AppDataMessage Message { get; }
        
        public Guid Guid { get; }
        
        public String AppName { get; }
        
        public String AppShortName { get; }
        
        public DateTime StartedAt { get; }

        public AppVersion Version { get; }
        public AppInformation Information { get; }

        public AppStatus Status { get; }

        public AppBranch Branch { get; }

        public String StatusData { get; }
        
        public String BranchData { get; }
    }
}