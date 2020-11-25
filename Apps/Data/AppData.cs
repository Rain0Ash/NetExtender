// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.RegularExpressions;
using NetExtender.Apps.Data.Common;
using NetExtender.Apps.Data.Interfaces;

namespace NetExtender.Apps.Data
{
    public class AppData : IAppData
    {
        private static readonly IDictionary<AppStatus, String> StatusDictionary = new Dictionary<AppStatus, String>
        {
            [AppStatus.None] = String.Empty,
            [AppStatus.NotFunctional] = "NF",
            [AppStatus.PreAlpha] = "PA",
            [AppStatus.ClosedAlpha] = "CA",
            [AppStatus.Alpha] = "A",
            [AppStatus.OpenAlpha] = "OA",
            [AppStatus.PreBeta] = "PB",
            [AppStatus.ClosedBeta] = "CB",
            [AppStatus.Beta] = "B",
            [AppStatus.OpenBeta] = "OB",
            [AppStatus.Release] = "R",
        }.ToImmutableDictionary();

        private static readonly IDictionary<AppBranch, String> BranchDictionary = new Dictionary<AppBranch, String>
        {
            [AppBranch.Master] = String.Empty,
            [AppBranch.Prototype] = "P",
            [AppBranch.Develop] = "DEV",
            [AppBranch.NewArchitecture] = "NA"
        }.ToImmutableDictionary();

        public static Boolean operator ==(AppData first, AppData second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }
            
            if (first is null || second is null)
            {
                return false;
            }
            
            return first.CompareTo(second) == 0;
        }

        public static Boolean operator !=(AppData first, AppData second)
        {
            return !(first == second);
        }

        public static Boolean operator >(AppData first, AppData second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator <(AppData first, AppData second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator >=(AppData first, AppData second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <=(AppData first, AppData second)
        {
            return first.CompareTo(second) <= 0;
        }

        public AppDataMessage Message
        {
            get
            {
                return new AppDataMessage(Guid, AppName, AppShortName, StartedAt, Version, Information, Status, Branch);
            }
        }

        public Guid Guid { get; }
        
        public String AppName { get; }
        
        public String AppShortName { get; }
        
        public DateTime StartedAt { get; }

        public AppVersion Version { get; }
        public AppInformation Information { get; }

        public AppStatus Status { get; }

        public AppBranch Branch { get; }

        public String StatusData
        {
            get
            {
                return StatusDictionary[Status];
            }
        }

        public String BranchData
        {
            get
            {
                return BranchDictionary[Branch];
            }
        }

        protected static String ToShortName(String name)
        {
            return name?.ToLower().Replace(" ", String.Empty);
        }

        public AppData(AppVersion version, AppStatus status = AppStatus.Release, AppBranch branch = AppBranch.Master)
            : this(version, AppInformation.Default, status, branch)
        {
        }

        
        public AppData(AppVersion version, AppInformation information, AppStatus status = AppStatus.Release, AppBranch branch = AppBranch.Master)
            : this(Process.GetCurrentProcess().ProcessName, version, information, status, branch)
        {
        }

        public AppData(String name, AppVersion version, AppStatus status = AppStatus.Release, AppBranch branch = AppBranch.Master)
            : this(name, version, AppInformation.Default, status, branch)
        {
        }
        
        public AppData(String name, AppVersion version, AppInformation information, AppStatus status = AppStatus.Release, AppBranch branch = AppBranch.Master)
            : this(name, ToShortName(name), version, information, status, branch)
        {
        }

        public AppData(String name, String sname, AppVersion version, AppStatus status = AppStatus.Release, AppBranch branch = AppBranch.Master)
            : this(name, sname, version, AppInformation.Default, status, branch)
        {
        }
        
        public AppData(String name, String sname, AppVersion version, AppInformation information, AppStatus status = AppStatus.Release, AppBranch branch = AppBranch.Master)
        {
            if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(sname))
            {
                throw new ArgumentException("Not null or whitespace app name");
            }

            if (!Regex.IsMatch(sname, "^[a-zA-Z0-9]+$", RegexOptions.IgnoreCase))
            {
                throw new ArgumentException(@"Only english chars or numbers", nameof(sname));
            }
            
            Guid = Guid.NewGuid();
            AppName = name.Trim();
            AppShortName = sname.Trim();
            StartedAt = DateTime.Now;
            Version = version;
            Information = information;
            Status = status;
            Branch = branch;
        }

        public Int32 CompareTo(IAppData other)
        {
            return CompareTo(other, false);
        }

        public Int32 CompareTo(IAppData other, Boolean versionFirst)
        {
            if (AppName != other.AppName)
            {
                throw new ArgumentException("Applications name is not equals");
            }
            
            Int32 compare = Version.CompareTo(other.Version);

            if (versionFirst && compare != 0)
            {
                return compare;
            }

            if (Status != AppStatus.None && other.Status != AppStatus.None)
            {
                return Status.CompareTo(other.Status);
            }

            return compare;
        }

        public override String ToString()
        {
            return $"{Version}:{StatusData}{BranchData}";
        }

        public Boolean Equals(IAppData other)
        {
            return other is not null && Version.Equals(other.Version) && Status == other.Status && Branch == other.Branch;
        }

        public override Boolean Equals(Object obj)
        {
            return obj is AppData other && Equals(other);
        }

        public override Int32 GetHashCode()
        {
            unchecked
            {
                Int32 hash = Version.GetHashCode();
                hash = (hash * 397) ^ (Int32) Status;
                hash = (hash * 397) ^ (Int32) Branch;
                return hash;
            }
        }

        public virtual void Dispose()
        {
        }
    }
}