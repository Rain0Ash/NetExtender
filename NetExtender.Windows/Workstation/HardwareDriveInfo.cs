// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Workstation
{
    public record HardwareDriveInfo
    {
        public String? DeviceId { get; init; }
        public String? Name { get; init; }
        public UInt32? Index { get; init; }
        public String? Model { get; init; }
        public String? Caption { get; init; }
        public String? Description { get; init; }
        public BusType? InterfaceType { get; init; }
        public String? Manufacturer { get; init; }
        public String? MediaType { get; init; }
        public String? SerialNumber { get; init; }
        public String? FirmwareRevision { get; init; }
        public UInt64? Size { get; init; }
        public UInt32? Partitions { get; init; }
        public UInt32? TotalHeads { get; init; }
        public UInt64? TotalCylinders { get; init; }
        public UInt64? TotalTracks { get; init; }
        public UInt64? TotalSectors { get; init; }
        public UInt32? TracksPerCylinder { get; init; }
        public UInt32? SectorsPerTrack { get; init; }
        public UInt32? BytesPerSector { get; init; }
        public DateTime? InstallDate { get; init; }
    }
}