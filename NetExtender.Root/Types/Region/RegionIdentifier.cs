// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Region
{
    public enum RegionIdentifier : Byte
    {
        /// <summary>
        /// None
        /// </summary>
        None,

        /// <summary>
        /// Europe
        /// </summary>
        Europe,

        /// <summary>
        /// America
        /// </summary>
        America,

        /// <summary>
        /// Asia
        /// </summary>
        Asia,

        /// <summary>
        /// Africa
        /// </summary>
        Africa,

        /// <summary>
        /// Oceania
        /// </summary>
        Oceania,

        /// <summary>
        /// Arctic
        /// </summary>
        Arctic,

        /// <summary>
        /// Antarctic
        /// </summary>
        Antarctic
    }
}