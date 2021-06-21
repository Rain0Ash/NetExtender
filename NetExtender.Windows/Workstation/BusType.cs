// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Workstation
{
    public enum BusType : UInt16
    {
        Unknown,
        SCSI,
        ATAPI,
        ATA,
        IEEE1394,
        SSA,
        FibreChannel,
        USB,
        RAID,
        ISCSI,
        SAS,
        SATA,
        SD,
        MMC,
        MAX,
        FileBackedVirtual,
        StorageSpaces,
        NVMe,
        MicrosoftReserved
    }
}