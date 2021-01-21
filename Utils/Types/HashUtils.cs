// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NetExtender.Crypto;

namespace NetExtender.Utils.Types
{
    public static class HashUtils
    {
        public static Byte[] GetHash(this Object obj, HashType type = HashType.MD5)
        {
            return obj switch
            {
                Byte[] bytes => bytes.Hashing(type),
                IEnumerable<Byte> bytes => bytes.ToArray().Hashing(type),
                String str => str.ToBytes().Hashing(type),
                _ => obj.Serialize().Hashing(type),
            };
        }

        #region HashCodes

        /// <summary>
		/// Combines hash codes.
		/// </summary>
		/// <param name="h1">Hash code 1</param>
		/// <param name="h2">Hash code 2</param>
		/// <returns>Combined hash code</returns>
		[Pure]
		public static Int32 Combine(Int32 h1, Int32 h2)
		{
			// RyuJIT optimizes this to use the ROL instruction
			// Related GitHub pull request: dotnet/coreclr#1830
			UInt32 rol5 = ((UInt32)h1 << 5) | ((UInt32)h1 >> 27);
			return ((Int32)rol5 + h1) ^ h2;
		}

		/// <summary>
		/// Combines hash codes.
		/// </summary>
		/// <param name="h1">Hash code 1</param>
		/// <param name="h2">Hash code 2</param>
		/// <param name="h3">Hash code 3</param>
		/// <returns>Combined hash code</returns>
		[Pure]
		public static Int32 Combine(Int32 h1, Int32 h2, Int32 h3)
		{
			return Combine(Combine(h1, h2), h3);
		}

		/// <summary>
		/// Combines hash codes.
		/// </summary>
		/// <param name="h1">Hash code 1</param>
		/// <param name="h2">Hash code 2</param>
		/// <param name="h3">Hash code 3</param>
		/// <param name="h4">Hash code 4</param>
		/// <returns>Combined hash code</returns>
		[Pure]
		public static Int32 Combine(Int32 h1, Int32 h2, Int32 h3, Int32 h4)
		{
			return Combine(Combine(h1, h2), Combine(h3, h4));
		}

		/// <summary>
		/// Combines hash codes.
		/// </summary>
		/// <param name="h1">Hash code 1</param>
		/// <param name="h2">Hash code 2</param>
		/// <param name="h3">Hash code 3</param>
		/// <param name="h4">Hash code 4</param>
		/// <param name="h5">Hash code 5</param>
		/// <returns>Combined hash code</returns>
		[Pure]
		public static Int32 Combine(Int32 h1, Int32 h2, Int32 h3, Int32 h4, Int32 h5)
		{
			return Combine(Combine(h1, h2, h3, h4), h5);
		}

		/// <summary>
		/// Combines hash codes.
		/// </summary>
		/// <param name="h1">Hash code 1</param>
		/// <param name="h2">Hash code 2</param>
		/// <param name="h3">Hash code 3</param>
		/// <param name="h4">Hash code 4</param>
		/// <param name="h5">Hash code 5</param>
		/// <param name="h6">Hash code 6</param>
		/// <returns>Combined hash code</returns>
		[Pure]
		public static Int32 Combine(Int32 h1, Int32 h2, Int32 h3, Int32 h4, Int32 h5, Int32 h6)
		{
			return Combine(Combine(h1, h2, h3, h4), Combine(h5, h6));
		}

		/// <summary>
		/// Combines hash codes.
		/// </summary>
		/// <param name="h1">Hash code 1</param>
		/// <param name="h2">Hash code 2</param>
		/// <param name="h3">Hash code 3</param>
		/// <param name="h4">Hash code 4</param>
		/// <param name="h5">Hash code 5</param>
		/// <param name="h6">Hash code 6</param>
		/// <param name="h7">Hash code 7</param>
		/// <returns>Combined hash code</returns>
		[Pure]
		public static Int32 Combine(Int32 h1, Int32 h2, Int32 h3, Int32 h4, Int32 h5, Int32 h6, Int32 h7)
		{
			return Combine(Combine(h1, h2, h3, h4), Combine(h5, h6, h7));
		}

		/// <summary>
		/// Combines hash codes.
		/// </summary>
		/// <param name="h1">Hash code 1</param>
		/// <param name="h2">Hash code 2</param>
		/// <param name="h3">Hash code 3</param>
		/// <param name="h4">Hash code 4</param>
		/// <param name="h5">Hash code 5</param>
		/// <param name="h6">Hash code 6</param>
		/// <param name="h7">Hash code 7</param>
		/// <param name="h8">Hash code 8</param>
		/// <returns>Combined hash code</returns>
		[Pure]
		public static Int32 Combine(Int32 h1, Int32 h2, Int32 h3, Int32 h4, Int32 h5, Int32 h6, Int32 h7, Int32 h8)
		{
			return Combine(Combine(h1, h2, h3, h4), Combine(h5, h6, h7, h8));
		}

		/// <summary>
		/// Combines hash codes.
		/// </summary>
		/// <typeparam name="T">Type of array values.</typeparam>
		/// <param name="values">The collection to combine hash codes.</param>
		/// <returns>
		/// Combined hash code.
		/// </returns>
		[Pure]
		public static Int32 CombineValues<T>([CanBeNull] params T[] values)
		{
			return values.IsNullOrEmpty() ? 0 : values.Where(value => value is not null).Aggregate(0, (current, value) => Combine(value.GetHashCode(), current));
		}

		/// <summary>
		/// Combines hash codes.
		/// </summary>
		/// <typeparam name="T">Type of collection values.</typeparam>
		/// <param name="values">The sequence to combine hash codes.</param>
		/// <returns>
		/// Combined hash code.
		/// </returns>
		[Pure]
		public static Int32 CombineValues<T>([CanBeNull, InstantHandle] IEnumerable<T> values)
		{
			if (values is null)
			{
				return 0;
			}

			Int32 hashCode = 0;

			if (values is IList<T> list)
			{
				for (Int32 i = 0, count = list.Count; i < count; i++)
				{
					T value = list[i];
					if (value is not null)
					{
						hashCode = Combine(value.GetHashCode(), hashCode);
					}
				}
			}
			else
			{
				hashCode = values.Where(value => value is not null).Aggregate(hashCode, (current, value) => Combine(value.GetHashCode(), current));
			}

			return hashCode;
		}
		
		#endregion
    }
}