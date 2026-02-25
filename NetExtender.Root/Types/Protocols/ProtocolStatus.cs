// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Protocols
{
    public enum ProtocolStatus : Byte
    {
        Unknown,
        Unregister,
        Register,
        Another,
        Error
    }
}