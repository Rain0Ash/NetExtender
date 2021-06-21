// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Workstation
{
    public enum HardwareDiskType : UInt16
    {
        Unspecified = 0,
        HDD = 3,
        SSD = 4,
        SCM = 5
    }

    [Serializable]
    public readonly struct HardwareDiskInfo
    {
        public UInt16 UniqueIDFormat { get; init; }
        public String? DeviceID { get; init; }
        public String? FriendlyName { get; init; }
        public String? Description { get; init; }
        public HardwareDiskType DiskType { get; init; }
        public UInt64 Size { get; init; }
        public UInt64 AllocatedSize { get; init; }
        public BusType BusType { get; init; }
        public UInt64 PhysicalSectorSize { get; init; }
        public UInt64 LogicalSectorSize { get; init; }
        public UInt32 SpindleSpeed { get; init; }

        public HardwareDiskInfo(UInt16 id, String? device, String? name, String? description, UInt16 disk, UInt64 size, UInt64 allocated, UInt16 bus,
            UInt64 physical, UInt64 logical, UInt32 spindle)
            : this(id, device, name, description, (HardwareDiskType) disk, size, allocated, (BusType) bus, physical, logical, spindle)
        {
        }
        
        public HardwareDiskInfo(UInt16 id, String? device, String? name, String? description, HardwareDiskType disk, UInt64 size, UInt64 allocated, BusType bus,
            UInt64 physical, UInt64 logical, UInt32 spindle)
        {
            UniqueIDFormat = id;
            DeviceID = device;
            FriendlyName = name;
            Description = description;
            DiskType = disk;
            Size = size;
            AllocatedSize = allocated;
            BusType = bus;
            PhysicalSectorSize = physical;
            LogicalSectorSize = logical;
            SpindleSpeed = spindle;
        }
    }
}