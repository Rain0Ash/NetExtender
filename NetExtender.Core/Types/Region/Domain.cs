// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetExtender.Types.Sets;
using NetExtender.Utilities.Types;

namespace NetExtender.Initializer.Types.Region
{
    public partial class CountryInfo
    {
        public readonly struct CountryDomain : IReadOnlyCollection<String>
        {
            public static implicit operator String?(CountryDomain value)
            {
                return value.ToString();
            }

            public static implicit operator CountryDomain(String? value)
            {
                return new CountryDomain(value);
            }

            private static Regex GlobalValidator { get; } = new Regex(@"^\.[a-z]{1,3}.$", RegexOptions.Compiled);
            private static Regex RegionalValidator { get; } = new Regex(@"^\..{1,20}$", RegexOptions.Compiled);

            public String? Global { get; }
            public String? Additional { get; }
            public String? Regional { get; }
            private OrderedSet<String>? Other { get; }

            public Int32 Count
            {
                get
                {
                    Int32 counter = 0;

                    if (Global is not null)
                    {
                        counter++;
                    }
                    
                    if (Additional is not null)
                    {
                        counter++;
                    }

                    if (Regional is not null)
                    {
                        counter++;
                    }

                    if (Other is not null)
                    {
                        counter += Other.Count;
                    }

                    return counter;
                }
            }

            internal CountryDomain(String? domain)
            {
                if (domain is null)
                {
                    Global = null;
                    Additional = null;
                    Regional = null;
                    Other = null;
                    return;
                }

                if (GlobalValidator.IsMatch(domain))
                {
                    Global = domain;
                    Additional = null;
                    Regional = null;
                    Other = null;
                    return;
                }

                if (IsRegional(domain))
                {
                    Global = null;
                    Additional = null;
                    Regional = domain;
                    Other = null;
                    return;
                }

                throw new ArgumentException("Invalid domain", nameof(domain));
            }

            internal CountryDomain(String? global, String? regional)
                : this(global, null, regional)
            {
            }

            internal CountryDomain(String? global, String? additional, String? regional, params String?[]? other)
            {
                if (global is not null && !GlobalValidator.IsMatch(global))
                {
                    throw new ArgumentException($"Invalid global domain '{global}'", nameof(global));
                }

                if (additional is not null && !GlobalValidator.IsMatch(additional))
                {
                    throw new ArgumentException($"Invalid additional domain '{additional}'", nameof(additional));
                }

                if (regional is not null && !IsRegional(regional))
                {
                    throw new ArgumentException($"Invalid regional domain '{regional}'", nameof(regional));
                }

                Global = global;
                Additional = additional;
                Regional = regional;

                if (other is null)
                {
                    Other = null;
                    return;
                }

                OrderedSet<String>? set = null;

                foreach (String value in other.WhereNotNull())
                {
                    if (!IsRegional(value))
                    {
                        throw new ArgumentException($"Invalid regional domain '{value}'", nameof(other));
                    }
                    
                    set ??= new OrderedSet<String>(StringComparer.OrdinalIgnoreCase);
                    set.Add(value);
                }

                Other = set;
            }

            private static Boolean IsRegional(String? domain)
            {
                return domain is not null && (RegionalValidator.IsMatch(domain) || domain.IsRightToLeft() && domain.Length < 20 && domain.EndsWith("."));
            }
            
            public void Deconstruct(out String? global, out String? regional)
            {
                Deconstruct(out global, out _, out regional);
            }

            public void Deconstruct(out String? global, out String? additional, out String? regional)
            {
                global = Global;
                additional = Additional;
                regional = Regional;
            }

            public void Deconstruct(out String? global, out String? additional, out String? regional, out String[]? other)
            {
                global = Global;
                additional = Additional;
                regional = Regional;
                other = Other?.ToArray();
            }

            public Boolean Contains(String? domain)
            {
                return domain is not null && (String.Equals(Global, domain, StringComparison.OrdinalIgnoreCase) ||
                                              String.Equals(Additional, domain, StringComparison.OrdinalIgnoreCase) ||
                                              String.Equals(Regional, domain, StringComparison.OrdinalIgnoreCase) ||
                                              Other is not null && Other.Contains(domain));
            }

            public IEnumerator<String> GetEnumerator()
            {
                if (Global is not null)
                {
                    yield return Global;
                }

                if (Additional is not null)
                {
                    yield return Additional;
                }

                if (Regional is not null)
                {
                    yield return Regional;
                }

                if (Other is null)
                {
                    yield break;
                }

                foreach (String domain in Other)
                {
                    yield return domain;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public override String? ToString()
            {
                return Global ?? Regional ?? Additional;
            }
        }
    }
}