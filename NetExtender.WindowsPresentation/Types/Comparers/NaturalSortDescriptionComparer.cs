using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NetExtender.Types.Comparers;

namespace NetExtender.UserInterface.WindowsPresentation.Types.Comparers
{
    public class NaturalSortDescriptionComparer : ISortDescriptionComparer
    {
        public SortDescriptionCollection Descriptions { get; }
        protected IComparer<IEnumerable<Object?>> Comparer { get; }
        
        public NaturalSortDescriptionComparer()
            : this(null)
        {
        }

        public NaturalSortDescriptionComparer(IComparer<IEnumerable<Object?>>? comparer)
        {
            Descriptions = new SortDescriptionCollection();
            Comparer = comparer ?? EnumerableComparer.Default;
        }

        public Int32 Compare(Object? x, Object? y)
        {
            IEnumerable<Object?> first = Descriptions.Select(description => GetValue(description, x, y));
            IEnumerable<Object?> second = Descriptions.Select(description => GetValue(description, y, x));

            return Comparer.Compare(first, second);
        }
        
        private static Object? GetValue(SortDescription description, Object? x, Object? y)
        {
            Object? result = description.Direction == ListSortDirection.Ascending ? x : y;
            return result?.GetType().GetProperty(description.PropertyName)?.GetValue(result);
        }
    }
}