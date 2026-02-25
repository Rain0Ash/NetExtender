using System;
using System.Collections.Generic;

namespace NetExtender.Monads
{
    public abstract partial class RayIdPayload
    {
        private readonly struct Comparer : IComparer<KeyValuePair<String, Element>>
        {
            private readonly StringComparer Internal;

            public Comparer(StringComparer? comparer)
            {
                Internal = comparer ?? Many.Comparer;
            }

            public Int32 Compare(KeyValuePair<String, Element> x, KeyValuePair<String, Element> y)
            {
                Int32 compare = -x.Value.Version.CompareTo(y.Value.Version);

                if (compare != 0)
                {
                    return compare;
                }

                StringComparer comparer = Internal;
                return comparer.Compare(x.Key, y.Key);
            }
        }
    }
}