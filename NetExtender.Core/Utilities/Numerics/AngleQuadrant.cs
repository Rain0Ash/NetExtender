// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Utilities.Numerics
{
    /// <summary>
    /// The four regions divided by the x and y axis.
    /// </summary>
    public enum AngleQuadrant
    {
        /// <summary>
        /// The region where x and y are positive.
        /// </summary>
        First,
        /// <summary>
        /// The region where x is negative and y is positive.
        /// </summary>
        Second,
        /// <summary>
        /// The region where x and y are negative.
        /// </summary>
        Third,
        /// <summary>
        /// The region where x is positive and y is negative.
        /// </summary>
        Fourth
    }
}