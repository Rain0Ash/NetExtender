// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static partial class ArrayUtilities
    {
        private const Int32 BoundLength = 5;

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Char[]? first, Char[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            switch (first.Length)
            {
                case <= 0:
                    return true;
                case < BoundLength:
                    return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pf = first, ps = second)
            {
                return BitUtilities.BitwiseEquals(pf, ps, first.Length);
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this SByte[]? first, SByte[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            switch (first.Length)
            {
                case <= 0:
                    return true;
                case < BoundLength:
                    return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pf = first, ps = second)
            {
                return BitUtilities.BitwiseEquals(pf, ps, first.Length);
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Byte[]? first, Byte[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            switch (first.Length)
            {
                case <= 0:
                    return true;
                case < BoundLength:
                    return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pf = first, ps = second)
            {
                return BitUtilities.BitwiseEquals(pf, ps, first.Length);
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Int16[]? first, Int16[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            switch (first.Length)
            {
                case <= 0:
                    return true;
                case < BoundLength:
                    return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pf = first, ps = second)
            {
                return BitUtilities.BitwiseEquals(pf, ps, first.Length);
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this UInt16[]? first, UInt16[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            switch (first.Length)
            {
                case <= 0:
                    return true;
                case < BoundLength:
                    return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pf = first, ps = second)
            {
                return BitUtilities.BitwiseEquals(pf, ps, first.Length);
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Int32[]? first, Int32[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            switch (first.Length)
            {
                case <= 0:
                    return true;
                case < BoundLength:
                    return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pf = first, ps = second)
            {
                return BitUtilities.BitwiseEquals(pf, ps, first.Length);
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this UInt32[]? first, UInt32[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            switch (first.Length)
            {
                case <= 0:
                    return true;
                case < BoundLength:
                    return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pf = first, ps = second)
            {
                return BitUtilities.BitwiseEquals(pf, ps, first.Length);
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Int64[]? first, Int64[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            switch (first.Length)
            {
                case <= 0:
                    return true;
                case < BoundLength:
                    return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pf = first, ps = second)
            {
                return BitUtilities.BitwiseEquals(pf, ps, first.Length);
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this UInt64[]? first, UInt64[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            switch (first.Length)
            {
                case <= 0:
                    return true;
                case < BoundLength:
                    return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pf = first, ps = second)
            {
                return BitUtilities.BitwiseEquals(pf, ps, first.Length);
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Single[]? first, Single[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            switch (first.Length)
            {
                case <= 0:
                    return true;
                case < BoundLength:
                    return !first.Where((value, i) => Math.Abs(value - second[i]) >= Single.Epsilon).Any();
            }

            fixed (void* pf = first, ps = second)
            {
                return BitUtilities.BitwiseEquals(pf, ps, first.Length);
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Double[]? first, Double[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            switch (first.Length)
            {
                case <= 0:
                    return true;
                case < BoundLength:
                    return !first.Where((value, i) => Math.Abs(value - second[i]) >= Double.Epsilon).Any();
            }

            fixed (void* pf = first, ps = second)
            {
                return BitUtilities.BitwiseEquals(pf, ps, first.Length);
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Decimal[]? first, Decimal[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            switch (first.Length)
            {
                case <= 0:
                    return true;
                case < BoundLength:
                    return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pf = first, ps = second)
            {
                return BitUtilities.BitwiseEquals(pf, ps, first.Length);
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this TimeSpan[]? first, TimeSpan[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            switch (first.Length)
            {
                case <= 0:
                    return true;
                case < BoundLength:
                    return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pf = first, ps = second)
            {
                return BitUtilities.BitwiseEquals(pf, ps, first.Length);
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Guid[]? first, Guid[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            if (first.Length <= 0)
            {
                return true;
            }

            fixed (void* pf = first, ps = second)
            {
                return BitUtilities.BitwiseEquals(pf, ps, first.Length);
            }
        }
    }
}