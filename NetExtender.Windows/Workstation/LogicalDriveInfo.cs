// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;

namespace NetExtender.Workstation
{
    public record LogicalDriveInfo
    {
        public String? DeviceId { get; init; }
        public String? Name { get; init; }
        public String? Caption { get; init; }
        public String? Description { get; init; }
        public DriveType? DriveType { get; init; }
        public String? FileSystem { get; init; }
        public UInt64? Size { get; init; }
        public String? VolumeName { get; init; }
        public String? VolumeSerialNumber { get; init; }
    }
}