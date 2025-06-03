// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using NetExtender.Types.Currency;

namespace NetExtender.Types.Region
{
    public partial class CountryInfo
    {
        public readonly struct CountryCurrency : IEquatableStruct<CountryCurrency>, IReadOnlyCollection<CurrencyInfo>, IEquatable<CurrencyInfo>
        {
            public static implicit operator CurrencyInfo?(CountryCurrency currency)
            {
                return currency.Official ?? currency.Second ?? currency.Third;
            }

            public static Boolean operator ==(CountryCurrency first, CountryCurrency second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(CountryCurrency first, CountryCurrency second)
            {
                return !(first == second);
            }

            public CurrencyInfo? Official { get; }
            public CurrencyInfo? Second { get; }
            public CurrencyInfo? Third { get; }

            public Int32 Count
            {
                get
                {
                    Int32 counter = 0;

                    if (Official is not null)
                    {
                        counter++;
                    }

                    if (Second is not null)
                    {
                        counter++;
                    }

                    if (Third is not null)
                    {
                        counter++;
                    }

                    return counter;
                }
            }
            
            public Boolean IsEmpty
            {
                get
                {
                    return Official is null && Second is null && Third is null;
                }
            }

            internal CountryCurrency(CurrencyInfo official)
                : this(official, null)
            {
            }

            internal CountryCurrency(CurrencyInfo official, CurrencyInfo? second)
                : this(official, second, null)
            {
            }

            internal CountryCurrency(CurrencyInfo official, CurrencyInfo? second, CurrencyInfo? third)
            {
                Official = official;
                Second = second;
                Third = third;
            }

            public void Deconstruct(out CurrencyInfo? official, out CurrencyInfo? second)
            {
                Deconstruct(out official, out second, out _);
            }

            public void Deconstruct(out CurrencyInfo? official, out CurrencyInfo? second, out CurrencyInfo? third)
            {
                official = Official;
                second = Second;
                third = Third;
            }

            public Boolean Contains(CurrencyInfo? currency)
            {
                return currency is not null && (Equals(Official, currency) || Equals(Second, currency) || Equals(Third, currency));
            }

            public IEnumerator<CurrencyInfo> GetEnumerator()
            {
                if (Official is not null)
                {
                    yield return Official;
                }

                if (Second is not null)
                {
                    yield return Second;
                }

                if (Third is not null)
                {
                    yield return Third;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public override Int32 GetHashCode()
            {
                return HashCode.Combine(Official, Second, Third);
            }

            public override Boolean Equals(Object? other)
            {
                return other switch
                {
                    null => IsEmpty,
                    CurrencyInfo value => Equals(value),
                    CountryCurrency value => Equals(value),
                    _ => false
                };
            }

            public Boolean Equals(CurrencyInfo? other)
            {
                return Official == other;
            }

            public Boolean Equals(CountryCurrency other)
            {
                return Official == other.Official && Second == other.Second && Third == other.Third;
            }

            public override String? ToString()
            {
                return (Official ?? Second ?? Third)?.ToString();
            }
        }
    }
}