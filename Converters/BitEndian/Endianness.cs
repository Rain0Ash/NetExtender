// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Converters.BitEndian
{
    /// <summary>
    /// Endianness of a converter
    /// </summary>
    public enum Endianness
    {
        /// <summary>
        /// Little endian - least significant byte first
        /// </summary>
        LittleEndian,

        /// <summary>
        /// Big endian - most significant byte first
        /// </summary>
        BigEndian
    }
}