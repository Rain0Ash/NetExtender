// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Linq.Expressions;
using DynamicData.Annotations;

namespace NetExtender.Utils.Types.Queryable
{
    public static class QueryableUtils
    {
        /// <summary>
		/// Sorts the elements of a sequence in ascending order according to a key.
		/// </summary>
		/// <typeparam name="T">Type of elements in source.</typeparam>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="property">The property name.</param>
		/// <returns>
		/// An <see cref="IOrderedQueryable"/> whose elements are sorted according to a key.
		/// </returns>
		[NotNull, Pure]
		public static IOrderedQueryable<T> OrderBy<T>([NotNull] this IQueryable<T> source, [NotNull] String property)
        {
	        return ApplyOrder(source, property, nameof(OrderBy));
        }

        /// <summary>
		/// Sorts the elements of a sequence in descending order according to a key.
		/// </summary>
		/// <typeparam name="T">Type of elements in source.</typeparam>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="property">The property name.</param>
		/// <returns>
		/// An <see cref="IOrderedQueryable{TElement}"/> whose elements are sorted according to a key.
		/// </returns>
		[NotNull, Pure]
		public static IOrderedQueryable<T> OrderByDescending<T>(
			[NotNull] this IQueryable<T> source, [NotNull] String property)
        {
	        return ApplyOrder(source, property, nameof(OrderByDescending));
        }

        /// <summary>
		/// Performs a subsequent ordering of the elements in a sequence in ascending order according to a key.
		/// </summary>
		/// <typeparam name="T">Type of elements in source.</typeparam>
		/// <param name="source">An <see cref="IOrderedEnumerable{TElement}"/> that contains elements to sort.</param>
		/// <param name="property">The property name.</param>
		/// <returns>
		/// An <see cref="IOrderedQueryable{TElement}"/> whose elements are sorted according to a key.
		/// </returns>
		[NotNull, Pure]
		public static IOrderedQueryable<T> ThenBy<T>([NotNull] this IOrderedQueryable<T> source, [NotNull] String property)
        {
	        return ApplyOrder(source, property, nameof(ThenBy));
        }

        /// <summary>
		/// Performs a subsequent ordering of the elements in a sequence in descending order according to a key.
		/// </summary>
		/// <typeparam name="T">Type of elements in source.</typeparam>
		/// <param name="source">An <see cref="IOrderedEnumerable{TElement}"/> that contains elements to sort.</param>
		/// <param name="property">The property name.</param>
		/// <returns>
		/// An <see cref="IOrderedQueryable{TElement}"/> whose elements are sorted according to a key.
		/// </returns>
		[NotNull, Pure]
		public static IOrderedQueryable<T> ThenByDescending<T>([NotNull] this IOrderedQueryable<T> source, [NotNull] String property)
        {
	        return ApplyOrder(source, property, nameof(ThenByDescending));
        }

        [NotNull, Pure]
		private static IOrderedQueryable<T> ApplyOrder<T>([NotNull] this IQueryable<T> source, [NotNull] String property, [NotNull] String method)
		{
			if (source is null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (property is null)
			{
				throw new ArgumentNullException(nameof(property));
			}
			
			ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
			Expression member = property.IndexOf('.') == -1
				? Expression.PropertyOrField(parameter, property)
				: property.Split('.').Aggregate((Expression)parameter, Expression.PropertyOrField);
			LambdaExpression expression = Expression.Lambda(member, parameter);

			return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(
				Expression.Call(
					typeof(System.Linq.Queryable),
					method,
					new[] { typeof(T), ((MemberExpression)member).Member.DeclaringType },
					source.Expression,
					Expression.Quote(expression)));
		}
		
		/// <summary>
		/// Extracts <paramref name="count"/> elements from a sequence at a particular zero-based starting index.
		/// </summary>
		/// <remarks>
		/// If the starting position or count specified result in slice extending past the end of the sequence,
		/// it will return all elements up to that point. There is no guarantee that the resulting sequence will
		/// contain the number of elements requested - it may have anywhere from 0 to <paramref name="count"/>.
		/// </remarks>
		/// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
		/// <param name="source">The sequence from which to extract elements.</param>
		/// <param name="startIndex">The zero-based index at which to begin slicing.</param>
		/// <param name="count">The number of items to slice out of the index.</param>
		/// <returns>
		/// A new sequence containing any elements sliced out from the source sequence.
		/// </returns>
		[NotNull, Pure, LinqTunnel]
		public static IQueryable<T> Slice<T>([NotNull] this IQueryable<T> source, Int32 startIndex, Int32 count)
		{
			if (startIndex > 0)
			{
				source = source.Skip(startIndex);
			}

			if (count > 0)
			{
				source = source.Take(count);
			}

			return source;
		}

		/// <summary>
		/// Extracts <paramref name="pageSize"/> elements from a sequence at a particular one-based page number.
		/// </summary>
		/// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
		/// <param name="source">The sequence from which to page.</param>
		/// <param name="pageIndex">The one-based page number.</param>
		/// <param name="pageSize">The size of the page.</param>
		/// <returns>
		/// A new sequence containing elements are at the specified <paramref name="pageIndex"/> from the source sequence.
		/// </returns>
		[NotNull, Pure, LinqTunnel]
		public static IQueryable<T> Page<T>([NotNull] this IQueryable<T> source, Int32 pageIndex, Int32 pageSize)
		{
			if (pageIndex > 1 && pageSize > 0)
			{
				source = source.Skip((pageIndex - 1) * pageSize);
			}

			if (pageSize > 0)
			{
				source = source.Take(pageSize);
			}

			return source;
		}
    }
}