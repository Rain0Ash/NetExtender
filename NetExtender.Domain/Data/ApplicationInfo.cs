// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using NetExtender.Domains.Interfaces;
using NetExtender.Domains.Utilities;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.Domains
{
    [Serializable]
    public class ApplicationInfo : IApplicationInfo
    {
        public static Boolean operator ==(ApplicationInfo? first, ApplicationInfo? second)
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

        public static Boolean operator !=(ApplicationInfo first, ApplicationInfo second)
        {
            return !(first == second);
        }

        public static Boolean operator >(ApplicationInfo first, ApplicationInfo second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator <(ApplicationInfo first, ApplicationInfo second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator >=(ApplicationInfo first, ApplicationInfo second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <=(ApplicationInfo first, ApplicationInfo second)
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
        public ApplicationInformation Information { get; }
        public ApplicationStatus Status { get; }

        public virtual String StatusInfo
        {
            get
            {
                return Status.Info();
            }
        }

        public ApplicationBranch Branch { get; }

        public virtual String BranchInfo
        {
            get
            {
                return Branch.Info();
            }
        }

        public Boolean HasAnotherInstance
        {
            get
            {
                return MutexUtilities.Capture(ApplicationName);
            }
        }

        private static Regex IdentifierRegex { get; } = new Regex(@"[^a-zA-Z0-9]", RegexOptions.Compiled);

        [return: NotNullIfNotNull("name")]
        protected static String? ToIdentifier(String? name)
        {
            return name is not null ? IdentifierRegex.Remove(name) : null;
        }

        public ApplicationInfo(ApplicationVersion version, ApplicationStatus status = ApplicationStatus.Release, ApplicationBranch branch = ApplicationBranch.Master)
            : this(version, ApplicationInformation.Default, status, branch)
        {
        }


        public ApplicationInfo(ApplicationVersion version, ApplicationInformation information, ApplicationStatus status = ApplicationStatus.Release, ApplicationBranch branch = ApplicationBranch.Master)
            : this(Process.GetCurrentProcess().ProcessName, version, information, status, branch)
        {
        }

        public ApplicationInfo(String name, ApplicationVersion version, ApplicationStatus status = ApplicationStatus.Release, ApplicationBranch branch = ApplicationBranch.Master)
            : this(name, version, ApplicationInformation.Default, status, branch)
        {
        }

        public ApplicationInfo(String name, ApplicationVersion version, ApplicationInformation information, ApplicationStatus status = ApplicationStatus.Release, ApplicationBranch branch = ApplicationBranch.Master)
            : this(name, name, version, information, status, branch)
        {
        }

        public ApplicationInfo(String name, ApplicationStatus status = ApplicationStatus.Release, ApplicationBranch branch = ApplicationBranch.Master)
            : this(name, name, status, branch)
        {
        }

        public ApplicationInfo(String name, String identifier, ApplicationStatus status = ApplicationStatus.Release, ApplicationBranch branch = ApplicationBranch.Master)
            : this(name, identifier, ApplicationVersion.Default, status, branch)
        {
        }

        public ApplicationInfo(String name, String identifier, ApplicationVersion version, ApplicationStatus status = ApplicationStatus.Release, ApplicationBranch branch = ApplicationBranch.Master)
            : this(name, identifier, version, ApplicationInformation.Default, status, branch)
        {
        }

        public ApplicationInfo(String name, String identifier, ApplicationVersion version, ApplicationInformation information, ApplicationStatus status = ApplicationStatus.Release, ApplicationBranch branch = ApplicationBranch.Master)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            name = name.Trim();
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullOrEmptyStringException(name, nameof(name));
            }

            identifier = ToIdentifier(identifier);
            if (String.IsNullOrEmpty(identifier))
            {
                throw new ArgumentNullOrEmptyStringException(identifier, nameof(identifier));
            }

            Guid = Guid.NewGuid();
            ApplicationName = name;
            ApplicationIdentifier = identifier;
            StartedAt = DateTime.UtcNow;
            Version = version;
            Information = information;
            Status = status;
            Branch = branch;
            MutexUtilities.RegisterMutex(ApplicationName);
        }

        public Int32 CompareTo(IApplicationInfo? other)
        {
            return CompareTo(other, false);
        }

        public Int32 CompareTo(IApplicationInfo? other, Boolean version)
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
            return $"{Version}:{StatusInfo}{BranchInfo}";
        }

        public Boolean Equals(IApplicationInfo? other)
        {
            return other is not null && Version.Equals(other.Version) && Status == other.Status && Branch == other.Branch;
        }

        public override Boolean Equals(Object? other)
        {
            return other is ApplicationInfo info && Equals(info);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Version, Status, Branch);
        }

        private Boolean _disposed;

        public void Dispose()
        {
            DisposeCore(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
        }

        private void DisposeCore(Boolean disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                MutexUtilities.UnregisterMutex(ApplicationName);
            }

            _disposed = true;
            Dispose(disposing);
        }

        ~ApplicationInfo()
        {
            Dispose(false);
        }
    }
}