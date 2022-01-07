// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using NetExtender.Types.Culture;

namespace NetExtender.Types.Comparers
{
    public class LocalizationComparer : OrderedComparer<LocalizationIdentifier>
    {
        public static IComparer<LocalizationIdentifier> Default { get; } = new LocalizationComparer(CultureIdentifier.En, CultureIdentifier.Ru, CultureIdentifier.Zh, CultureIdentifier.De, CultureIdentifier.Fr);
        
        public LocalizationComparer()
        {
        }

        public LocalizationComparer(IComparer<LocalizationIdentifier>? comparer)
            : base(comparer)
        {
        }
        
        public LocalizationComparer(params LocalizationIdentifier[]? order)
            : base(order)
        {
        }

        public LocalizationComparer(IEnumerable<LocalizationIdentifier>? order)
            : base(order)
        {
        }
        
        public LocalizationComparer(IComparer<LocalizationIdentifier>? comparer, params LocalizationIdentifier[]? order)
            : base(order, comparer)
        {
        }
        
        public LocalizationComparer(IEnumerable<LocalizationIdentifier>? order, IComparer<LocalizationIdentifier>? comparer)
            : base(order, comparer)
        {
        }
    }
}