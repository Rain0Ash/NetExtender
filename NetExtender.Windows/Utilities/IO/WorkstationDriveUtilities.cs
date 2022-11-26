// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Management;
using NetExtender.Utilities.Types;
using NetExtender.Workstation;

namespace NetExtender.Windows.Utilities.IO
{
    public static class WorkstationDriveUtilities
    {
        // ReSharper disable once CognitiveComplexity
        public static IEnumerable<PartitionDriveInfo> ToHardwareDriveInfo(this LogicalDriveInfo drive)
        {
            if (drive is null)
            {
                throw new ArgumentNullException(nameof(drive));
            }

            using ManagementObjectSearcher drivesearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            using ManagementObjectCollection drivecollection = drivesearcher.Get();

            using ManagementObjectSearcher partitionsearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDriveToDiskPartition");
            using ManagementObjectCollection partitioncollection = partitionsearcher.Get();

            using ManagementObjectSearcher logicalpartitionsearcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDiskToPartition");
            using ManagementObjectCollection logicalpartitioncollection = logicalpartitionsearcher.Get();

            if (drive.DeviceId is null)
            {
                yield break;
            }

            Dictionary<String, HardwareDriveInfo> hardwarecache = new Dictionary<String, HardwareDriveInfo>(8);

            foreach (ManagementBaseObject logical in logicalpartitioncollection)
            {
                if (!logical.TryGetValue("Dependent", out String? dependent) || !dependent.Contains($"Win32_LogicalDisk.DeviceID=\"{drive.DeviceId}\""))
                {
                    continue;
                }

                if (!logical.TryGetValue("Antecedent", out String? antecedent))
                {
                    continue;
                }

                UInt64? starting = logical.TryGetValue("StartingAddress", out UInt64 startingaddress) ? startingaddress : null;
                UInt64? ending = logical.TryGetValue("EndingAddress", out UInt64 endingaddress) ? endingaddress : null;
                foreach (ManagementBaseObject partition in partitioncollection)
                {
                    if (!partition.TryGetValue("Dependent", out dependent) || dependent != antecedent)
                    {
                        continue;
                    }

                    if (!partition.TryGetValue("Antecedent", out antecedent))
                    {
                        continue;
                    }

                    foreach (ManagementBaseObject management in drivecollection)
                    {
                        if (!management.TryGetValue("DeviceID", out String? deviceid) || !antecedent.Replace("\\\\", "\\").Contains($"Win32_DiskDrive.DeviceID=\"{deviceid}\""))
                        {
                            continue;
                        }

                        yield return new PartitionDriveInfo(drive, hardwarecache.GetOrAdd(deviceid, () => Hardware.GetHardwareDrive(management))) { StartingAddress = starting, EndingAddress = ending };
                    }
                }
            }
        }

        // ReSharper disable once CognitiveComplexity
        public static IEnumerable<PartitionDriveInfo> ToHardwareDriveInfo(this IEnumerable<LogicalDriveInfo> drives)
        {
            if (drives is null)
            {
                throw new ArgumentNullException(nameof(drives));
            }

            using ManagementObjectSearcher drivesearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            using ManagementObjectCollection drivecollection = drivesearcher.Get();

            using ManagementObjectSearcher partitionsearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDriveToDiskPartition");
            using ManagementObjectCollection partitioncollection = partitionsearcher.Get();

            using ManagementObjectSearcher logicalpartitionsearcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDiskToPartition");
            using ManagementObjectCollection logicalpartitioncollection = logicalpartitionsearcher.Get();

            Dictionary<String, HardwareDriveInfo> hardwarecache = new Dictionary<String, HardwareDriveInfo>(8);

            foreach (LogicalDriveInfo drive in drives.WhereNotNull())
            {
                if (drive.DeviceId is null)
                {
                    continue;
                }

                foreach (ManagementBaseObject logical in logicalpartitioncollection)
                {
                    if (!logical.TryGetValue("Dependent", out String? dependent) || !dependent.Contains($"Win32_LogicalDisk.DeviceID=\"{drive.DeviceId}\""))
                    {
                        continue;
                    }

                    if (!logical.TryGetValue("Antecedent", out String? antecedent))
                    {
                        continue;
                    }

                    UInt64? starting = logical.TryGetValue("StartingAddress", out UInt64 startingaddress) ? startingaddress : null;
                    UInt64? ending = logical.TryGetValue("EndingAddress", out UInt64 endingaddress) ? endingaddress : null;
                    foreach (ManagementBaseObject partition in partitioncollection)
                    {
                        if (!partition.TryGetValue("Dependent", out dependent) || dependent != antecedent)
                        {
                            continue;
                        }

                        if (!partition.TryGetValue("Antecedent", out antecedent))
                        {
                            continue;
                        }

                        foreach (ManagementBaseObject management in drivecollection)
                        {
                            if (!management.TryGetValue("DeviceID", out String? deviceid) || !antecedent.Replace("\\\\", "\\").Contains($"Win32_DiskDrive.DeviceID=\"{deviceid}\""))
                            {
                                continue;
                            }

                            yield return new PartitionDriveInfo(drive, hardwarecache.GetOrAdd(deviceid, () => Hardware.GetHardwareDrive(management)))
                                { StartingAddress = starting, EndingAddress = ending };
                        }
                    }
                }
            }
        }

        // ReSharper disable once CognitiveComplexity
        public static IEnumerable<PartitionDriveInfo> ToLogicalDriveInfo(this HardwareDriveInfo drive)
        {
            if (drive is null)
            {
                throw new ArgumentNullException(nameof(drive));
            }

            using ManagementObjectSearcher partitionsearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDriveToDiskPartition");
            using ManagementObjectCollection partitioncollection = partitionsearcher.Get();

            using ManagementObjectSearcher logicalpartitionsearcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDiskToPartition");
            using ManagementObjectCollection logicalpartitioncollection = logicalpartitionsearcher.Get();

            using ManagementObjectSearcher drivesearcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk");
            using ManagementObjectCollection drivecollection = drivesearcher.Get();

            if (drive.DeviceId is null)
            {
                yield break;
            }

            Dictionary<String, LogicalDriveInfo> logicalcache = new Dictionary<String, LogicalDriveInfo>(8);

            foreach (ManagementBaseObject partition in partitioncollection)
            {
                if (!partition.TryGetValue("Antecedent", out String? antecedent) || !antecedent.Replace("\\\\", "\\").Contains($"Win32_DiskDrive.DeviceID=\"{drive.DeviceId}\""))
                {
                    continue;
                }

                if (!partition.TryGetValue("Dependent", out String? dependent))
                {
                    continue;
                }

                foreach (ManagementBaseObject logical in logicalpartitioncollection)
                {
                    if (!logical.TryGetValue("Antecedent", out antecedent) || antecedent != dependent)
                    {
                        continue;
                    }

                    if (!logical.TryGetValue("Dependent", out dependent))
                    {
                        continue;
                    }

                    UInt64? starting = logical.TryGetValue("StartingAddress", out UInt64 startingaddress) ? startingaddress : null;
                    UInt64? ending = logical.TryGetValue("EndingAddress", out UInt64 endingaddress) ? endingaddress : null;

                    foreach (ManagementBaseObject management in drivecollection)
                    {
                        if (!management.TryGetValue("DeviceID", out String? deviceid) || !dependent.Contains($"Win32_LogicalDisk.DeviceID=\"{deviceid}\""))
                        {
                            continue;
                        }

                        yield return new PartitionDriveInfo(logicalcache.GetOrAdd(deviceid, () => Hardware.GetLogicalDrive(management)), drive) { StartingAddress = starting, EndingAddress = ending };
                    }
                }
            }
        }

        // ReSharper disable once CognitiveComplexity
        public static IEnumerable<PartitionDriveInfo> ToLogicalDriveInfo(this IEnumerable<HardwareDriveInfo> drives)
        {
            if (drives is null)
            {
                throw new ArgumentNullException(nameof(drives));
            }

            using ManagementObjectSearcher partitionsearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDriveToDiskPartition");
            using ManagementObjectCollection partitioncollection = partitionsearcher.Get();

            using ManagementObjectSearcher logicalpartitionsearcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDiskToPartition");
            using ManagementObjectCollection logicalpartitioncollection = logicalpartitionsearcher.Get();

            using ManagementObjectSearcher drivesearcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk");
            using ManagementObjectCollection drivecollection = drivesearcher.Get();

            Dictionary<String, LogicalDriveInfo> logicalcache = new Dictionary<String, LogicalDriveInfo>(8);

            foreach (HardwareDriveInfo drive in drives.WhereNotNull())
            {
                if (drive.DeviceId is null)
                {
                    continue;
                }

                foreach (ManagementBaseObject partition in partitioncollection)
                {
                    if (!partition.TryGetValue("Antecedent", out String? antecedent) || !antecedent.Replace("\\\\", "\\").Contains($"Win32_DiskDrive.DeviceID=\"{drive.DeviceId}\""))
                    {
                        continue;
                    }

                    if (!partition.TryGetValue("Dependent", out String? dependent))
                    {
                        continue;
                    }

                    foreach (ManagementBaseObject logical in logicalpartitioncollection)
                    {
                        if (!logical.TryGetValue("Antecedent", out antecedent) || antecedent != dependent)
                        {
                            continue;
                        }

                        if (!logical.TryGetValue("Dependent", out dependent))
                        {
                            continue;
                        }

                        UInt64? starting = logical.TryGetValue("StartingAddress", out UInt64 startingaddress) ? startingaddress : null;
                        UInt64? ending = logical.TryGetValue("EndingAddress", out UInt64 endingaddress) ? endingaddress : null;

                        foreach (ManagementBaseObject management in drivecollection)
                        {
                            if (!management.TryGetValue("DeviceID", out String? deviceid) || !dependent.Contains($"Win32_LogicalDisk.DeviceID=\"{deviceid}\""))
                            {
                                continue;
                            }

                            yield return new PartitionDriveInfo(logicalcache.GetOrAdd(deviceid, () => Hardware.GetLogicalDrive(management)), drive) { StartingAddress = starting, EndingAddress = ending };
                        }
                    }
                }
            }
        }
    }
}