// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;

namespace NetExtender.Initializer.Types.Region
{
    public partial class CountryInfo
    {
        public readonly struct CountryCalling : IReadOnlyCollection<UInt16>, IEquatable<CountryCalling>
        {
            public static implicit operator UInt16(CountryCalling calling)
            {
                return calling.Official16;
            }
            
            public static implicit operator Int32(CountryCalling calling)
            {
                return calling.Official;
            }

            public static implicit operator String?(CountryCalling calling)
            {
                return calling.ToString();
            }
            
            public Int32 Official
            {
                get
                {
                    return Official16;
                }
            }

            public UInt16 Official16 { get; }
            
            public Int32 Second
            {
                get
                {
                    return Second16;
                }
            }

            public UInt16 Second16 { get; }
            
            public Int32 Third
            {
                get
                {
                    return Third16;
                }
            }

            public UInt16 Third16 { get; }
            
            public Int32 Special { get; }

            public Boolean IsSpecial
            {
                get
                {
                    return Special > 0;
                }
            }
            
            public Int32 Count
            {
                get
                {
                    Int32 counter = 0;

                    if (Official16 > 0)
                    {
                        counter++;
                    }

                    if (Second > 0)
                    {
                        counter++;
                    }

                    if (Third > 0)
                    {
                        counter++;
                    }

                    if (Special > 0)
                    {
                        counter++;
                    }

                    return counter;
                }
            }

            internal CountryCalling(UInt16 official)
                : this(official, 0)
            {
            }

            internal CountryCalling(UInt16 official, UInt16 second)
                : this(official, second, 0)
            {
            }

            internal CountryCalling(UInt16 official, UInt16 second, UInt16 third)
                : this(official, second, third, 0)
            {
            }

            internal CountryCalling(UInt16 official, UInt16 second, UInt16 third, Int32 special)
            {
                Official16 = official;
                Second16 = second;
                Third16 = third;
                Special = special;
            }

            public void Deconstruct(out UInt16 official, out UInt16 second, out UInt16 third)
            {
                Deconstruct(out official, out second, out third, out _);
            }

            public void Deconstruct(out UInt16 official, out UInt16 second, out UInt16 third, out Int32 special)
            {
                official = Official16;
                second = Second16;
                third = Third16;
                special = Special;
            }
            
            public void Deconstruct(out Int32 official, out Int32 second, out Int32 third)
            {
                Deconstruct(out official, out second, out third, out _);
            }

            public void Deconstruct(out Int32 official, out Int32 second, out Int32 third, out Int32 special)
            {
                official = Official;
                second = Second;
                third = Third;
                special = Special;
            }
            
            public Boolean Contains(UInt16 code)
            {
                return code > 0 && (Official16 == code || Second16 == code || Third16 == code || Special == code);
            }

            public Boolean Contains(Int32 code)
            {
                return code > 0 && (Official == code || Second == code || Third == code || Special == code);
            }

            public IEnumerator<UInt16> GetEnumerator()
            {
                if (Official16 > 0)
                {
                    yield return Official16;
                }

                if (Second16 > 0)
                {
                    yield return Second16;
                }

                if (Third16 > 0)
                {
                    yield return Third16;
                }

                if (Special > 0 && Special <= UInt16.MaxValue)
                {
                    yield return (UInt16) Special;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public override Int32 GetHashCode()
            {
                return HashCode.Combine(Official16, Second16, Third16, Special);
            }

            public override Boolean Equals(Object? obj)
            {
                return obj switch
                {
                    CountryCalling calling => Equals(calling),
                    SByte number => Official16 == number || Second16 == number || Third16 == number || Special == number,
                    Byte number => Official16 == number || Second16 == number || Third16 == number || Special == number,
                    Int16 number => Official16 == number || Second16 == number || Third16 == number || Special == number,
                    UInt16 number => Official16 == number || Second16 == number || Third16 == number || Special == number,
                    Int32 number => Official16 == number || Second16 == number || Third16 == number || Special == number,
                    UInt32 number => Official16 == number || Second16 == number || Third16 == number || Special == number,
                    Int64 number => Official16 == number || Second16 == number || Third16 == number || Special == number,
                    UInt64 number => Official16 == number || Second16 == number || Third16 == number || number <= Int32.MaxValue && Special == (Int32) number,
                    _ => false
                };
            }

            public Boolean Equals(CountryCalling other)
            {
                return Official16 == other.Official16 && Second16 == other.Second16 && Third16 == other.Third16 && Special == other.Special;
            }

            public override String? ToString()
            {
                return Official16 > 0 ? $"+{Official16}" : null;
            }
        }
    }
}