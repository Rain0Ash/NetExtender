// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Workstation
{
    public record PartitionDriveInfo
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator HardwareDriveInfo?(PartitionDriveInfo? value)
        {
            return value?.Hardware;
        }
        
        [return: NotNullIfNotNull("value")]
        public static implicit operator LogicalDriveInfo?(PartitionDriveInfo? value)
        {
            return value?.Logical;
        }

        public LogicalDriveInfo Logical { get; }
        public HardwareDriveInfo Hardware { get; }
        public UInt64? StartingAddress { get; init; }
        public UInt64? EndingAddress { get; init; }
        
        public PartitionDriveInfo(LogicalDriveInfo logical, HardwareDriveInfo hardware)
        {
            Logical = logical ?? throw new ArgumentNullException(nameof(logical));
            Hardware = hardware ?? throw new ArgumentNullException(nameof(hardware));
        }
    }
}