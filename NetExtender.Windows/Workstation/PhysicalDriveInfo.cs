// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Workstation
{
    public enum HardwareDriveType : UInt16
    {
        Unspecified = 0,
        HDD = 3,
        SSD = 4,
        SCM = 5
    }

    [Serializable]
    public record PhysicalDriveInfo
    {
        public UInt16? UniqueIdFormat { get; init; }
        public String? DeviceId { get; init; }
        public String? FriendlyName { get; init; }
        public String? Description { get; init; }
        public HardwareDriveType? MediaType { get; init; }
        public UInt64? Size { get; init; }
        public UInt64? AllocatedSize { get; init; }
        public BusType? BusType { get; init; }
        public UInt64? PhysicalSectorSize { get; init; }
        public UInt64? LogicalSectorSize { get; init; }
        public UInt32? SpindleSpeed { get; init; }
    }
}