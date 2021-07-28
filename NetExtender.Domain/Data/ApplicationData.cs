// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using NetExtender.Domains.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Domains
{
    [Serializable]
    public class ApplicationData : IApplicationData
    {
        private static readonly IDictionary<ApplicationStatus, String> StatusDictionary = new Dictionary<ApplicationStatus, String>
        {
            [ApplicationStatus.None] = String.Empty,
            [ApplicationStatus.NotFunctional] = "NF",
            [ApplicationStatus.PreAlpha] = "PA",
            [ApplicationStatus.ClosedAlpha] = "CA",
            [ApplicationStatus.Alpha] = "A",
            [ApplicationStatus.OpenAlpha] = "OA",
            [ApplicationStatus.PreBeta] = "PB",
            [ApplicationStatus.ClosedBeta] = "CB",
            [ApplicationStatus.Beta] = "B",
            [ApplicationStatus.OpenBeta] = "OB",
            [ApplicationStatus.Release] = "R",
        }.ToImmutableDictionary();

        private static readonly IDictionary<ApplicationBranch, String> BranchDictionary = new Dictionary<ApplicationBranch, String>
        {
            [ApplicationBranch.Master] = String.Empty,
            [ApplicationBranch.Stable] = "ST",
            [ApplicationBranch.Unstable] = "UNST",
            [ApplicationBranch.Develop] = "DEV",
            [ApplicationBranch.Prototype] = "P",
            [ApplicationBranch.Nightly] = "N",
            [ApplicationBranch.NewArchitecture] = "NA"
        }.ToImmutableDictionary();

        public static Boolean operator ==(ApplicationData? first, ApplicationData? second)
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

        public static Boolean operator !=(ApplicationData first, ApplicationData second)
        {
            return !(first == second);
        }

        public static Boolean operator >(ApplicationData first, ApplicationData second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator <(ApplicationData first, ApplicationData second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator >=(ApplicationData first, ApplicationData second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <=(ApplicationData first, ApplicationData second)
        {
            return first.CompareTo(second) <= 0;
        }

        private ApplicationInfoMessage? _message;
        public ApplicationInfoMessage Message
        {
            get
            {
                return _message ??= new ApplicationInfoMessage(Guid, ApplicationName, ApplicationIdentifier, StartedAt, Version, Information, Status, Branch);
            }
        }

        public Guid Guid { get; }
        
        public String ApplicationName { get; }
        
        public String ApplicationIdentifier { get; }
        
        public DateTime StartedAt { get; }

        public ApplicationVersion Version { get; }
        public ApplicationInfo Information { get; }

        public ApplicationStatus Status { get; }

        public ApplicationBranch Branch { get; }

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

        public Boolean HasAnotherInstance
        {
            get
            {
                return MutexUtils.CaptureMutex(ApplicationName);
            }
        }

        [return: NotNullIfNotNull("name")]
        protected static String? ToIdentifier(String? name)
        {
            return name is not null ? Regex.Replace(name, @"[^a-zA-Z0-9]", String.Empty) : null;
        }

        public ApplicationData(ApplicationVersion version, ApplicationStatus status = ApplicationStatus.Release, ApplicationBranch branch = ApplicationBranch.Master)
            : this(version, ApplicationInfo.Default, status, branch)
        {
        }

        
        public ApplicationData(ApplicationVersion version, ApplicationInfo information, ApplicationStatus status = ApplicationStatus.Release, ApplicationBranch branch = ApplicationBranch.Master)
            : this(Process.GetCurrentProcess().ProcessName, version, information, status, branch)
        {
        }

        public ApplicationData(String name, ApplicationVersion version, ApplicationStatus status = ApplicationStatus.Release, ApplicationBranch branch = ApplicationBranch.Master)
            : this(name, version, ApplicationInfo.Default, status, branch)
        {
        }
        
        public ApplicationData(String name, ApplicationVersion version, ApplicationInfo information, ApplicationStatus status = ApplicationStatus.Release, ApplicationBranch branch = ApplicationBranch.Master)
            : this(name, name, version, information, status, branch)
        {
        }
        
        public ApplicationData(String name, ApplicationStatus status = ApplicationStatus.Release, ApplicationBranch branch = ApplicationBranch.Master)
            : this(name, name, status, branch)
        {
        }
        
        public ApplicationData(String name, String identifier, ApplicationStatus status = ApplicationStatus.Release, ApplicationBranch branch = ApplicationBranch.Master)
            : this(name, identifier, ApplicationVersion.Default, status, branch)
        {
        }

        public ApplicationData(String name, String identifier, ApplicationVersion version, ApplicationStatus status = ApplicationStatus.Release, ApplicationBranch branch = ApplicationBranch.Master)
            : this(name, identifier, version, ApplicationInfo.Default, status, branch)
        {
        }
        
        public ApplicationData(String name, String identifier, ApplicationVersion version, ApplicationInfo information, ApplicationStatus status = ApplicationStatus.Release, ApplicationBranch branch = ApplicationBranch.Master)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            name = name.Trim();
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            identifier = ToIdentifier(identifier);
            if (String.IsNullOrEmpty(identifier))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(identifier));
            }

            Guid = Guid.NewGuid();
            ApplicationName = name;
            ApplicationIdentifier = identifier;
            StartedAt = DateTime.Now;
            Version = version;
            Information = information;
            Status = status;
            Branch = branch;
            MutexUtils.RegisterMutex(ApplicationName);
        }

        public Int32 CompareTo(IApplicationData? other)
        {
            return CompareTo(other, false);
        }

        public Int32 CompareTo(IApplicationData? other, Boolean version)
        {
            if (other is null)
            {
                return 2;
            }
            
            if (ApplicationName != other.ApplicationName)
            {
                throw new ArgumentException("Applications name is not equals");
            }
            
            Int32 compare = Version.CompareTo(other.Version);

            if (version && compare != 0)
            {
                return compare;
            }

            if (Status != ApplicationStatus.None && other.Status != ApplicationStatus.None)
            {
                return Status.CompareTo(other.Status);
            }

            return compare;
        }
        
        public override String ToString()
        {
            return $"{Version}:{StatusData}{BranchData}";
        }

        public Boolean Equals(IApplicationData? other)
        {
            return other is not null && Version.Equals(other.Version) && Status == other.Status && Branch == other.Branch;
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is ApplicationData other && Equals(other);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Version, Status, Branch);
        }

        private Boolean _disposed;
        
        public void Dispose()
        {
            DisposeInternal(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
        }
        
        private void DisposeInternal(Boolean disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                MutexUtils.UnregisterMutex(ApplicationName);
            }

            _disposed = true;
            Dispose(disposing);
        }

        ~ApplicationData()
        {
            Dispose(false);
        }
    }
}