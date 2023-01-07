// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace NetExtender.Types.Culture
{
    public readonly struct LocalizationIdentifiers : IReadOnlyCollection<LocalizationIdentifier>
    {
        public static implicit operator LocalizationIdentifiers(CultureIdentifier value)
        {
            return (LocalizationIdentifier) value;
        }
        
        public static implicit operator LocalizationIdentifiers(LocalizationIdentifier value)
        {
            return new LocalizationIdentifiers(value);
        }
        
        public static implicit operator ImmutableArray<CultureIdentifier>(LocalizationIdentifiers value)
        {
            return value.Internal.Select(identifier => identifier.Identifier).ToImmutableArray();
        }
        
        public static implicit operator CultureIdentifier[](LocalizationIdentifiers value)
        {
            return value.Internal.Select(identifier => identifier.Identifier).ToArray();
        }
        
        public static implicit operator LocalizationIdentifiers(CultureIdentifier[]? value)
        {
            return new LocalizationIdentifiers(value?.Select(identifier => (LocalizationIdentifier) identifier));
        }
        
        public static implicit operator ImmutableArray<LocalizationIdentifier>(LocalizationIdentifiers value)
        {
            return value.Internal;
        }
        
        public static implicit operator LocalizationIdentifier[](LocalizationIdentifiers value)
        {
            return value.Internal.ToArray();
        }
        
        public static implicit operator LocalizationIdentifiers(LocalizationIdentifier[]? value)
        {
            return new LocalizationIdentifiers(value);
        }

        public static LocalizationIdentifiers operator |(LocalizationIdentifiers first, LocalizationIdentifiers second)
        {
            return new LocalizationIdentifiers(first, second);
        }

        public static LocalizationIdentifiers England { get; } = new LocalizationIdentifiers(CultureIdentifier.Us, CultureIdentifier.En);
        public static LocalizationIdentifiers Cis { get; } = new LocalizationIdentifiers(CultureIdentifier.Ru, CultureIdentifier.Uk, CultureIdentifier.Be, CultureIdentifier.Kk);
        public static LocalizationIdentifiers Europe { get; } = England | new LocalizationIdentifiers(CultureIdentifier.Fr, CultureIdentifier.Pl, CultureIdentifier.De, CultureIdentifier.It, CultureIdentifier.Es);
        
        private ImmutableArray<LocalizationIdentifier> Internal { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Length;
            }
        }

        public LocalizationIdentifiers(IEnumerable<LocalizationIdentifier>? identifiers)
        {
            Internal = identifiers?.Distinct().ToImmutableArray() ?? ImmutableArray<LocalizationIdentifier>.Empty;
        }

        public LocalizationIdentifiers(params LocalizationIdentifier[]? identifiers)
            : this((IEnumerable<LocalizationIdentifier>?) identifiers)
        {
        }

        public LocalizationIdentifiers(params LocalizationIdentifiers[]? identifiers)
            : this(identifiers?.SelectMany(item => item).Distinct())
        {
        }

        public ImmutableArray<LocalizationIdentifier>.Enumerator GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator<LocalizationIdentifier> IEnumerable<LocalizationIdentifier>.GetEnumerator()
        {
            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (LocalizationIdentifier item in this)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<LocalizationIdentifier>) this).GetEnumerator();
        }
    }
}