// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Configuration.Common
{
    public enum ConfigType : Byte
    {
        Registry,
        INI,
        XML,
        RAM,
        JSON,
        SQL
    }
}