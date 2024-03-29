// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Localization.Behavior.Interfaces;
using NetExtender.Types.Culture;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Comparers
{
    public class LocalizationComparer : OrderedComparer<LocalizationIdentifier>, IComparer<CultureIdentifier>
    {
        public static LocalizationComparer Default { get; } = new LocalizationComparer(CultureIdentifier.Invariant, CultureIdentifier.Us, CultureIdentifier.En,
            CultureIdentifier.Ru, CultureIdentifier.Uk, CultureIdentifier.Be, CultureIdentifier.Kk, CultureIdentifier.Pl,
            CultureIdentifier.De, CultureIdentifier.Fr, CultureIdentifier.It, CultureIdentifier.Es, CultureIdentifier.Tr,
            CultureIdentifier.Lt, CultureIdentifier.Lv, CultureIdentifier.Et, CultureIdentifier.Zh);

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

        public LocalizationComparer(params LocalizationIdentifiers[]? order)
            : base(order?.SelectMany(identifier => identifier).Distinct())
        {
        }

        public LocalizationComparer(IEnumerable<LocalizationIdentifier>? order)
            : base(order)
        {
        }

        public LocalizationComparer(IEnumerable<LocalizationIdentifiers>? order)
            : base(order?.SelectMany(identifier => identifier).Distinct())
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

        public LocalizationComparer(IEnumerable<LocalizationIdentifiers>? order, IComparer<LocalizationIdentifier>? comparer)
            : base(order?.SelectMany(identifier => identifier).Distinct(), comparer)
        {
        }

        public Int32 Compare(CultureIdentifier x, CultureIdentifier y)
        {
            return base.Compare(x, y);
        }
    }

    public class LocalizationIdentifierBehaviorComparer : IComparer<String>, IComparer<LocalizationIdentifier>, IComparer<CultureIdentifier>
    {
        private ILocalizationBehavior Behavior { get; }
        public IComparer<LocalizationIdentifier> Comparer { get; }

        public LocalizationIdentifierBehaviorComparer(ILocalizationBehavior behavior, IComparer<LocalizationIdentifier>? comparer)
        {
            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
            Comparer = comparer ?? LocalizationComparer.Default;
        }

        public Int32 Compare(String? x, String? y)
        {
            if (x is null || !CultureUtilities.TryGetIdentifier(x, out CultureIdentifier first))
            {
                return y is null || !CultureUtilities.TryGetIdentifier(y, out CultureIdentifier _) ? 0 : -1;
            }

            if (y is null || !CultureUtilities.TryGetIdentifier(y, out CultureIdentifier second))
            {
                return 1;
            }

            return Compare(first, second);
        }

        public Int32 Compare(LocalizationIdentifier x, LocalizationIdentifier y)
        {
            Int32 compare = Comparer.Compare(x, y);
            return compare != 0 ? x == Behavior.Localization ? 4 : x == Behavior.System ? 3 : x == Behavior.Default ? 2 : compare : 0;
        }

        public Int32 Compare(CultureIdentifier x, CultureIdentifier y)
        {
            Int32 compare = Comparer.Compare(x, y);
            return compare != 0 ? x == Behavior.Localization ? 4 : x == Behavior.System ? 3 : x == Behavior.Default ? 2 : compare : 0;
        }
    }
}