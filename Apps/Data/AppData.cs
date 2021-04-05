// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NetExtender.Apps.Data.Common;
using NetExtender.Apps.Data.Interfaces;
using NetExtender.Events.Args;
using NetExtender.Exceptions;
using NetExtender.Network.IPC.Messaging;
using NetExtender.Protocols;
using NetExtender.Protocols.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Apps.Data
{
    [Serializable]
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

        private IUrlSchemeProtocol _protocol;
        protected IUrlSchemeProtocol UrlSchemeProtocol
        {
            get
            {
                return _protocol ??= new UrlSchemeProtocol(AppShortName);
            }
            init
            {
                if (_protocol is not null)
                {
                    throw new AlreadyInitializedException(nameof(UrlSchemeProtocol));
                }
                
                _protocol = value;
            }
        }

        public String UrlSchemeProtocolName
        {
            get
            {
                return UrlSchemeProtocol.Name;
            }
        }

        public Boolean? IsUrlSchemeProtocolRegister
        {
            get
            {
                return UrlSchemeProtocol.RegisterStatus;
            }
            set
            {
                if (!value.HasValue)
                {
                    return;
                }
                
                UrlSchemeProtocol.IsRegister = value.Value;
            }
        }

        public Boolean HasAnotherInstance
        {
            get
            {
                return MutexUtils.CaptureMutex(AppName);
            }
        }

        public Boolean? IsExternalMessageBus { get; private set; }
        
        private IInterprocessMessageBus _bus;
        protected IInterprocessMessageBus MessageBus
        {
            get
            {
                if (_bus is not null)
                {
                    return _bus;
                }
                
                _bus = new InterprocessMessageBus(AppShortName);
                IsExternalMessageBus = false;
                return _bus;
            }
            init
            {
                if (_bus is not null)
                {
                    throw new AlreadyInitializedException(nameof(MessageBus));
                }
                
                if (value is null)
                {
                    return;
                }
                
                _bus = value;
                IsExternalMessageBus = true;
            }
        }

        public event SenderTypeHandler<TypeHandledEventArgs<Byte[]>> MessageReceived
        {
            add
            {
                MessageBus.MessageReceived += value;
            }
            remove
            {
                MessageBus.MessageReceived -= value;
            }
        }

        public Int64 SendedMessages
        {
            get
            {
                return IsExternalMessageBus is not null ? MessageBus.SendedMessages : 0;
            }
        }

        public Int64 ReceivedMessages
        {
            get
            {
                return IsExternalMessageBus is not null ? MessageBus.ReceivedMessages : 0;
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

        public AppData(String name, String shortname, AppVersion version, AppStatus status = AppStatus.Release, AppBranch branch = AppBranch.Master)
            : this(name, shortname, version, AppInformation.Default, status, branch)
        {
        }
        
        public AppData(String name, String shortname, AppVersion version, AppInformation information, AppStatus status = AppStatus.Release, AppBranch branch = AppBranch.Master)
        {
            if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(shortname))
            {
                throw new ArgumentException("Not null or whitespace app name");
            }

            if (!Regex.IsMatch(shortname, "^[\x21-\x7E]+$"))
            {
                throw new ArgumentException(@"Only latin chars and digits", nameof(shortname));
            }
            
            Guid = Guid.NewGuid();
            AppName = name.Trim();
            AppShortName = shortname.Trim();
            StartedAt = DateTime.Now;
            Version = version;
            Information = information;
            Status = status;
            Branch = branch;
            MutexUtils.RegisterMutex(AppName);
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

        public Task SendMessageAsync(Byte[] message)
        {
            return MessageBus.SendMessageAsync(message);
        }

        public Task SendMessageAsync(IEnumerable<Byte[]> messages)
        {
            return MessageBus.SendMessageAsync(messages);
        }

        public void ResetMetrics()
        {
            if (IsExternalMessageBus is not null)
            {
                MessageBus.ResetMetrics();
            }
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
            return HashCode.Combine(Version, Status, Branch);
        }

        private Boolean _disposed;
        
        public void Dispose()
        {
            DisposeInternal(true);
            GC.SuppressFinalize(this);
        }

        private void DisposeInternal(Boolean disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                MutexUtils.UnregisterMutex(AppName);
                
                if (IsExternalMessageBus == false)
                {
                    MessageBus?.Dispose();
                }
            }

            _disposed = true;
            Dispose(disposing);
        }

        protected virtual void Dispose(Boolean disposing)
        {
        }

        ~AppData()
        {
            Dispose(false);
        }
    }
}