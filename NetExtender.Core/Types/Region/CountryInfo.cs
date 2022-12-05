// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using NetExtender.Types.Culture;
using NetExtender.Types.Currency;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Region
{
    public partial class CountryInfo : IEquatable<CountryInfo>
    {
        public static implicit operator UInt16(CountryInfo? info)
        {
            return info?.Code16 ?? 0;
        }

        public static implicit operator Int32(CountryInfo? info)
        {
            return info?.Code ?? 0;
        }

        public static implicit operator CountryIdentifier(CountryInfo? info)
        {
            return info?.Identifier ?? CountryIdentifier.Default;
        }

        public static implicit operator CountryInfo(CountryIdentifier identifier)
        {
            return CountryInfoCache.Get(identifier);
        }

        [return: NotNullIfNotNull("info")]
        public static implicit operator String?(CountryInfo? info)
        {
            return info?.OfficialName;
        }

        public static implicit operator CurrencyInfo?(CountryInfo? info)
        {
            return info?.Currency;
        }

        public static implicit operator CountryInfo?(CultureIdentifier identifier)
        {
            return identifier.ToCultureInfo();
        }

        public static implicit operator CountryInfo?(LocalizationIdentifier identifier)
        {
            return (CultureInfo) identifier;
        }

        public static implicit operator CountryInfo?(CultureInfo? info)
        {
            return info?.ToRegionInfo();
        }

        public static implicit operator CountryInfo?(RegionInfo? info)
        {
            return info is not null && EnumUtilities.TryParse(info.TwoLetterISORegionName, true, out CountryIdentifier identifier) ? identifier : null;
        }

        public static CountryInfo CurrentCountry
        {
            get
            {
                return CountryInfoCache.Get(CountryIdentifier.Default);
            }
        }

        public static CountryInfo[] Countries
        {
            get
            {
                return EnumUtilities.GetValuesWithoutDefault<CountryIdentifier>().Select(CountryInfoCache.Get).ToArray();
            }
        }

        public String Name { get; }
        public String OfficialName { get; }
        public String NativeName { get; }
        public CountryIdentifier Identifier { get; }

        public Int32 Code
        {
            get
            {
                return Code16;
            }
        }

        public UInt16 Code16
        {
            get
            {
                return (UInt16) Identifier;
            }
        }

        public String TwoLetterISOCountryName
        {
            get
            {
                return Identifier.ToString();
            }
        }

        public RegionIdentifier Region
        {
            get
            {
                return Subregion.ToRegionIdentifier();
            }
        }

        public SubregionIdentifier Subregion { get; }

        public CountryDomain Domain { get; init; }
        public ImmutableHashSet<CountryIdentifier> Border { get; init; } = ImmutableHashSet<CountryIdentifier>.Empty;
        public CountryCurrency Currency { get; init; }
        public CountryCalling Calling { get; init; }

        protected CountryInfo(String name, String official, String native, CountryIdentifier identifier, SubregionIdentifier subregion)
        {
            Name = name;
            OfficialName = official;
            NativeName = native;
            Identifier = identifier;
            Subregion = subregion;
        }

        public override Int32 GetHashCode()
        {
            return Identifier.GetHashCode();
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is CountryInfo info && Equals(info);
        }

        public Boolean Equals(CountryInfo? other)
        {
            return other is not null && Identifier == other.Identifier;
        }

        public override String ToString()
        {
            return Name;
        }
    }

    public partial class CountryInfo
    {
        private static class CountryInfoCache
        {
            private static ConcurrentDictionary<CountryIdentifier, CountryInfo> Cache { get; } = new ConcurrentDictionary<CountryIdentifier, CountryInfo>();

            public static CountryInfo Get(CountryIdentifier identifier)
            {
                if (identifier != CountryIdentifier.Default)
                {
                    return Cache.GetOrAdd(identifier, Create);
                }

                CountryInfo? country = RegionInfo.CurrentRegion;
                country ??= CultureInfo.CurrentCulture;
                return country ?? throw new InvalidOperationException("Can't get current country");
            }

            private static CountryInfo Create(CountryIdentifier identifier)
            {
                return identifier switch
                {
                    CountryIdentifier.Default => Get(CountryIdentifier.Default),
                    CountryIdentifier.Af => new CountryInfo("Afghanistan", "Islamic Republic of Afghanistan", "افغانستان", CountryIdentifier.Af, SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".af"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ir, CountryIdentifier.Pk, CountryIdentifier.Tm, CountryIdentifier.Uz, CountryIdentifier.Tj,
                            CountryIdentifier.Cn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Afn),
                        Calling = new CountryCalling(93)
                    },
                    CountryIdentifier.Al => new CountryInfo("Albania", "Republic of Albania", "Shqipëria", CountryIdentifier.Al, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".al"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Me, CountryIdentifier.Gr, CountryIdentifier.Mk),
                        Currency = new CountryCurrency(CurrencyIdentifier.All),
                        Calling = new CountryCalling(355)
                    },
                    CountryIdentifier.Aq => new CountryInfo("Antarctica", "Antarctica", "Antarctica", CountryIdentifier.Aq, SubregionIdentifier.Antarctic)
                    {
                        Domain = new CountryDomain(".aq"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(),
                        Calling = new CountryCalling()
                    },
                    CountryIdentifier.Dz => new CountryInfo("Algeria", "People's Democratic Republic of Algeria", "الجزائر", CountryIdentifier.Dz, SubregionIdentifier.NorthernAfrica)
                    {
                        Domain = new CountryDomain(".dz", "الجزائر."),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Tn, CountryIdentifier.Ly, CountryIdentifier.Ne, CountryIdentifier.Eh, CountryIdentifier.Mr,
                            CountryIdentifier.Ml, CountryIdentifier.Ma),
                        Currency = new CountryCurrency(CurrencyIdentifier.Dzd),
                        Calling = new CountryCalling(213)
                    },
                    CountryIdentifier.As => new CountryInfo("American Samoa", "American Samoa", "American Samoa", CountryIdentifier.As, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".as"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(1684)
                    },
                    CountryIdentifier.Ad => new CountryInfo("Andorra", "Principality of Andorra", "Andorra", CountryIdentifier.Ad, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".ad"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Fr, CountryIdentifier.Es),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(376)
                    },
                    CountryIdentifier.Ao => new CountryInfo("Angola", "Republic of Angola", "Angola", CountryIdentifier.Ao, SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".ao"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cg, CountryIdentifier.Cd, CountryIdentifier.Zm, CountryIdentifier.Na),
                        Currency = new CountryCurrency(CurrencyIdentifier.Aoa),
                        Calling = new CountryCalling(244)
                    },
                    CountryIdentifier.Ag => new CountryInfo("Antigua and Barbuda", "Antigua and Barbuda", "Antigua and Barbuda", CountryIdentifier.Ag, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".ag"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Xcd),
                        Calling = new CountryCalling(1268)
                    },
                    CountryIdentifier.Az => new CountryInfo("Azerbaijan", "Republic of Azerbaijan", "Azərbaycan", CountryIdentifier.Az, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".az"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Am, CountryIdentifier.Ge, CountryIdentifier.Ir, CountryIdentifier.Ru, CountryIdentifier.Tr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Azn),
                        Calling = new CountryCalling(994)
                    },
                    CountryIdentifier.Ar => new CountryInfo("Argentina", "Argentine Republic", "Argentina", CountryIdentifier.Ar, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".ar"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bo, CountryIdentifier.Br, CountryIdentifier.Cl, CountryIdentifier.Py, CountryIdentifier.Uy),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ars),
                        Calling = new CountryCalling(54)
                    },
                    CountryIdentifier.Au => new CountryInfo("Australia", "Commonwealth of Australia", "Australia", CountryIdentifier.Au, SubregionIdentifier.AustraliaAndNewZealand)
                    {
                        Domain = new CountryDomain(".au"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Aud),
                        Calling = new CountryCalling(61)
                    },
                    CountryIdentifier.At => new CountryInfo("Austria", "Republic of Austria", "Österreich", CountryIdentifier.At, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".at"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cz, CountryIdentifier.De, CountryIdentifier.Hu, CountryIdentifier.It, CountryIdentifier.Li,
                            CountryIdentifier.Sk, CountryIdentifier.Si, CountryIdentifier.Ch),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(43)
                    },
                    CountryIdentifier.Bs => new CountryInfo("Bahamas", "Commonwealth of the Bahamas", "Bahamas", CountryIdentifier.Bs, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".bs"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Bsd),
                        Calling = new CountryCalling(1242)
                    },
                    CountryIdentifier.Bh => new CountryInfo("Bahrain", "Kingdom of Bahrain", "البحرين", CountryIdentifier.Bh, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".bh"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Bhd),
                        Calling = new CountryCalling(973)
                    },
                    CountryIdentifier.Bd => new CountryInfo("Bangladesh", "People's Republic of Bangladesh", "Bangladesh", CountryIdentifier.Bd, SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".bd"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Mm, CountryIdentifier.In),
                        Currency = new CountryCurrency(CurrencyIdentifier.Bdt),
                        Calling = new CountryCalling(880)
                    },
                    CountryIdentifier.Am => new CountryInfo("Armenia", "Republic of Armenia", "Հայաստան", CountryIdentifier.Am, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".am"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Az, CountryIdentifier.Ge, CountryIdentifier.Ir, CountryIdentifier.Tr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Amd),
                        Calling = new CountryCalling(374)
                    },
                    CountryIdentifier.Bb => new CountryInfo("Barbados", "Barbados", "Barbados", CountryIdentifier.Bb, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".bb"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Bbd),
                        Calling = new CountryCalling(1246)
                    },
                    CountryIdentifier.Be => new CountryInfo("Belgium", "Kingdom of Belgium", "België", CountryIdentifier.Be, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".be"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Fr, CountryIdentifier.De, CountryIdentifier.Lu, CountryIdentifier.Nl),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(32)
                    },
                    CountryIdentifier.Bm => new CountryInfo("Bermuda", "Bermuda", "Bermuda", CountryIdentifier.Bm, SubregionIdentifier.NorthAmerica)
                    {
                        Domain = new CountryDomain(".bm"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Bmd),
                        Calling = new CountryCalling(1441)
                    },
                    CountryIdentifier.Bt => new CountryInfo("Bhutan", "Kingdom of Bhutan", "ʼbrug-yul", CountryIdentifier.Bt, SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".bt"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cn, CountryIdentifier.In),
                        Currency = new CountryCurrency(CurrencyIdentifier.Btn, CurrencyIdentifier.Inr),
                        Calling = new CountryCalling(975)
                    },
                    CountryIdentifier.Bo => new CountryInfo("Bolivia", "Plurinational State of Bolivia", "Bolivia", CountryIdentifier.Bo, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".bo"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ar, CountryIdentifier.Br, CountryIdentifier.Cl, CountryIdentifier.Py, CountryIdentifier.Pe),
                        Currency = new CountryCurrency(CurrencyIdentifier.Bob),
                        Calling = new CountryCalling(591)
                    },
                    CountryIdentifier.Ba => new CountryInfo("Bosnia and Herzegovina", "Bosnia and Herzegovina", "Bosna i Hercegovina", CountryIdentifier.Ba,
                        SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".ba"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Hr, CountryIdentifier.Me, CountryIdentifier.Rs),
                        Currency = new CountryCurrency(CurrencyIdentifier.Bam),
                        Calling = new CountryCalling(387)
                    },
                    CountryIdentifier.Bw => new CountryInfo("Botswana", "Republic of Botswana", "Botswana", CountryIdentifier.Bw, SubregionIdentifier.SouthernAfrica)
                    {
                        Domain = new CountryDomain(".bw"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Na, CountryIdentifier.Za, CountryIdentifier.Zm, CountryIdentifier.Zw),
                        Currency = new CountryCurrency(CurrencyIdentifier.Bwp),
                        Calling = new CountryCalling(267)
                    },
                    CountryIdentifier.Bv => new CountryInfo("Bouvet Island", "Bouvet Island", "Bouvetøya", CountryIdentifier.Bv, SubregionIdentifier.Antarctic)
                    {
                        Domain = new CountryDomain(".bv"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Nok),
                        Calling = new CountryCalling()
                    },
                    CountryIdentifier.Br => new CountryInfo("Brazil", "Federative Republic of Brazil", "Brasil", CountryIdentifier.Br, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".br"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ar, CountryIdentifier.Bo, CountryIdentifier.Co, CountryIdentifier.Gf, CountryIdentifier.Gy,
                            CountryIdentifier.Py, CountryIdentifier.Pe, CountryIdentifier.Sr, CountryIdentifier.Uy, CountryIdentifier.Ve),
                        Currency = new CountryCurrency(CurrencyIdentifier.Brl),
                        Calling = new CountryCalling(55)
                    },
                    CountryIdentifier.Bz => new CountryInfo("Belize", "Belize", "Belize", CountryIdentifier.Bz, SubregionIdentifier.CentralAmerica)
                    {
                        Domain = new CountryDomain(".bz"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gt, CountryIdentifier.Mx),
                        Currency = new CountryCurrency(CurrencyIdentifier.Bzd),
                        Calling = new CountryCalling(501)
                    },
                    CountryIdentifier.Io => new CountryInfo("British Indian Ocean Territory", "British Indian Ocean Territory", "British Indian Ocean Territory", CountryIdentifier.Io,
                        SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".io"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(246)
                    },
                    CountryIdentifier.Sb => new CountryInfo("Solomon Islands", "Solomon Islands", "Solomon Islands", CountryIdentifier.Sb, SubregionIdentifier.Melanesia)
                    {
                        Domain = new CountryDomain(".sb"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Sbd),
                        Calling = new CountryCalling(677)
                    },
                    CountryIdentifier.Vg => new CountryInfo("British Virgin Islands", "Virgin Islands", "British Virgin Islands", CountryIdentifier.Vg, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".vg"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(1284)
                    },
                    CountryIdentifier.Bn => new CountryInfo("Brunei", "Nation of Brunei, Abode of Peace", "Negara Brunei Darussalam", CountryIdentifier.Bn,
                        SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".bn"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.My),
                        Currency = new CountryCurrency(CurrencyIdentifier.Bnd),
                        Calling = new CountryCalling(673)
                    },
                    CountryIdentifier.Bg => new CountryInfo("Bulgaria", "Republic of Bulgaria", "България", CountryIdentifier.Bg, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".bg"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gr, CountryIdentifier.Mk, CountryIdentifier.Ro, CountryIdentifier.Rs, CountryIdentifier.Tr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Bgn),
                        Calling = new CountryCalling(359)
                    },
                    CountryIdentifier.Mm => new CountryInfo("Myanmar", "Republic of the Union of Myanmar", "Myanma", CountryIdentifier.Mm, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".mm"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bd, CountryIdentifier.Cn, CountryIdentifier.In, CountryIdentifier.La, CountryIdentifier.Th),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mmk),
                        Calling = new CountryCalling(95)
                    },
                    CountryIdentifier.Bi => new CountryInfo("Burundi", "Republic of Burundi", "Burundi", CountryIdentifier.Bi, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".bi"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cd, CountryIdentifier.Rw, CountryIdentifier.Tz),
                        Currency = new CountryCurrency(CurrencyIdentifier.Bif),
                        Calling = new CountryCalling(257)
                    },
                    CountryIdentifier.By => new CountryInfo("Belarus", "Republic of Belarus", "Белару́сь", CountryIdentifier.By, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".by"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Lv, CountryIdentifier.Lt, CountryIdentifier.Pl, CountryIdentifier.Ru, CountryIdentifier.Ua),
                        Currency = new CountryCurrency(CurrencyIdentifier.Byn),
                        Calling = new CountryCalling(375)
                    },
                    CountryIdentifier.Kh => new CountryInfo("Cambodia", "Kingdom of Cambodia", "Kâmpŭchéa", CountryIdentifier.Kh, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".kh"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.La, CountryIdentifier.Th, CountryIdentifier.Vn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Khr),
                        Calling = new CountryCalling(855)
                    },
                    CountryIdentifier.Cm => new CountryInfo("Cameroon", "Republic of Cameroon", "Cameroon", CountryIdentifier.Cm, SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".cm"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cf, CountryIdentifier.Td, CountryIdentifier.Cg, CountryIdentifier.Gq, CountryIdentifier.Ga,
                            CountryIdentifier.Ng),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xaf),
                        Calling = new CountryCalling(237)
                    },
                    CountryIdentifier.Ca => new CountryInfo("Canada", "Canada", "Canada", CountryIdentifier.Ca, SubregionIdentifier.NorthAmerica)
                    {
                        Domain = new CountryDomain(".ca"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Us),
                        Currency = new CountryCurrency(CurrencyIdentifier.Cad),
                        Calling = new CountryCalling(1)
                    },
                    CountryIdentifier.Cv => new CountryInfo("Cape Verde", "Republic of Cabo Verde", "Cabo Verde", CountryIdentifier.Cv, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".cv"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Cve),
                        Calling = new CountryCalling(238)
                    },
                    CountryIdentifier.Ky => new CountryInfo("Cayman Islands", "Cayman Islands", "Cayman Islands", CountryIdentifier.Ky, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".ky"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Kyd),
                        Calling = new CountryCalling(1345)
                    },
                    CountryIdentifier.Cf => new CountryInfo("Central African Republic", "Central African Republic", "Ködörösêse tî Bêafrîka", CountryIdentifier.Cf,
                        SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".cf"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cm, CountryIdentifier.Td, CountryIdentifier.Cd, CountryIdentifier.Cg, CountryIdentifier.Ss,
                            CountryIdentifier.Sd),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xaf),
                        Calling = new CountryCalling(236)
                    },
                    CountryIdentifier.Lk => new CountryInfo("Sri Lanka", "Democratic Socialist Republic of Sri Lanka", "śrī laṃkāva", CountryIdentifier.Lk,
                        SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".lk", null, ".இலங்கை", ".ලංකා"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.In),
                        Currency = new CountryCurrency(CurrencyIdentifier.Lkr),
                        Calling = new CountryCalling(94)
                    },
                    CountryIdentifier.Td => new CountryInfo("Chad", "Republic of Chad", "Tchad", CountryIdentifier.Td, SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".td"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cm, CountryIdentifier.Cf, CountryIdentifier.Ly, CountryIdentifier.Ne, CountryIdentifier.Ng,
                            CountryIdentifier.Ss),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xaf),
                        Calling = new CountryCalling(235)
                    },
                    CountryIdentifier.Cl => new CountryInfo("Chile", "Republic of Chile", "Chile", CountryIdentifier.Cl, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".cl"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ar, CountryIdentifier.Bo, CountryIdentifier.Pe),
                        Currency = new CountryCurrency(CurrencyIdentifier.Clp),
                        Calling = new CountryCalling(56)
                    },
                    CountryIdentifier.Cn => new CountryInfo("China", "People's Republic of China", "中国", CountryIdentifier.Cn, SubregionIdentifier.EasternAsia)
                    {
                        Domain = new CountryDomain(".cn", null, ".中国", ".中國", ".公司", ".网络"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Af, CountryIdentifier.Bt, CountryIdentifier.Mm, CountryIdentifier.Hk, CountryIdentifier.In,
                            CountryIdentifier.Kz, CountryIdentifier.Kp, CountryIdentifier.Kg, CountryIdentifier.La, CountryIdentifier.Mo, CountryIdentifier.Mn, CountryIdentifier.Pk,
                            CountryIdentifier.Ru, CountryIdentifier.Tj, CountryIdentifier.Vn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Cny),
                        Calling = new CountryCalling(86)
                    },
                    CountryIdentifier.Tw => new CountryInfo("Taiwan", "Republic of China", "臺灣", CountryIdentifier.Tw, SubregionIdentifier.EasternAsia)
                    {
                        Domain = new CountryDomain(".tw", null, ".台湾", ".台灣"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Twd),
                        Calling = new CountryCalling(886)
                    },
                    CountryIdentifier.Cx => new CountryInfo("Christmas Island", "Territory of Christmas Island", "Christmas Island", CountryIdentifier.Cx,
                        SubregionIdentifier.AustraliaAndNewZealand)
                    {
                        Domain = new CountryDomain(".cx"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Aud),
                        Calling = new CountryCalling(61)
                    },
                    CountryIdentifier.Cc => new CountryInfo("Cocos (Keeling) Islands", "Territory of the Cocos (Keeling) Islands", "Cocos (Keeling) Islands", CountryIdentifier.Cc,
                        SubregionIdentifier.AustraliaAndNewZealand)
                    {
                        Domain = new CountryDomain(".cc"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Aud),
                        Calling = new CountryCalling(61)
                    },
                    CountryIdentifier.Co => new CountryInfo("Colombia", "Republic of Colombia", "Colombia", CountryIdentifier.Co, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".co"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Br, CountryIdentifier.Ec, CountryIdentifier.Pa, CountryIdentifier.Pe, CountryIdentifier.Ve),
                        Currency = new CountryCurrency(CurrencyIdentifier.Cop),
                        Calling = new CountryCalling(57)
                    },
                    CountryIdentifier.Km => new CountryInfo("Comoros", "Union of the Comoros", "Komori", CountryIdentifier.Km, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".km"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Kmf),
                        Calling = new CountryCalling(269)
                    },
                    CountryIdentifier.Yt => new CountryInfo("Mayotte", "Department of Mayotte", "Mayotte", CountryIdentifier.Yt, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".yt"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(262)
                    },
                    CountryIdentifier.Cg => new CountryInfo("Republic of the Congo", "Republic of the Congo", "République démocratique du Congo", CountryIdentifier.Cg,
                        SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".cg"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ao, CountryIdentifier.Cm, CountryIdentifier.Cf, CountryIdentifier.Cd, CountryIdentifier.Ga),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xaf),
                        Calling = new CountryCalling(242)
                    },
                    CountryIdentifier.Cd => new CountryInfo("DR Congo", "Democratic Republic of the Congo", "République du Congo", CountryIdentifier.Cd,
                        SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".cd"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ao, CountryIdentifier.Bi, CountryIdentifier.Cf, CountryIdentifier.Cg, CountryIdentifier.Rw,
                            CountryIdentifier.Ss, CountryIdentifier.Tz, CountryIdentifier.Ug, CountryIdentifier.Zm),
                        Currency = new CountryCurrency(CurrencyIdentifier.Cdf),
                        Calling = new CountryCalling(243)
                    },
                    CountryIdentifier.Ck => new CountryInfo("Cook Islands", "Cook Islands", "Cook Islands", CountryIdentifier.Ck, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".ck"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Nzd),
                        Calling = new CountryCalling(682)
                    },
                    CountryIdentifier.Cr => new CountryInfo("Costa Rica", "Republic of Costa Rica", "Costa Rica", CountryIdentifier.Cr, SubregionIdentifier.CentralAmerica)
                    {
                        Domain = new CountryDomain(".cr"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ni, CountryIdentifier.Pa),
                        Currency = new CountryCurrency(CurrencyIdentifier.Crc),
                        Calling = new CountryCalling(506)
                    },
                    CountryIdentifier.Hr => new CountryInfo("Croatia", "Republic of Croatia", "Hrvatska", CountryIdentifier.Hr, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".hr"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ba, CountryIdentifier.Hu, CountryIdentifier.Me, CountryIdentifier.Rs, CountryIdentifier.Si),
                        Currency = new CountryCurrency(CurrencyIdentifier.Hrk),
                        Calling = new CountryCalling(385)
                    },
                    CountryIdentifier.Cu => new CountryInfo("Cuba", "Republic of Cuba", "Cuba", CountryIdentifier.Cu, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".cu"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Cuc, CurrencyIdentifier.Cup),
                        Calling = new CountryCalling(53)
                    },
                    CountryIdentifier.Cy => new CountryInfo("Cyprus", "Republic of Cyprus", "Кэрспт", CountryIdentifier.Cy, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".cy"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gb),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(357)
                    },
                    CountryIdentifier.Cz => new CountryInfo("Czechia", "Czech Republic", "Česká republika", CountryIdentifier.Cz, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".cz"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.At, CountryIdentifier.De, CountryIdentifier.Pl, CountryIdentifier.Sk),
                        Currency = new CountryCurrency(CurrencyIdentifier.Czk),
                        Calling = new CountryCalling(420)
                    },
                    CountryIdentifier.Bj => new CountryInfo("Benin", "Republic of Benin", "Bénin", CountryIdentifier.Bj, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".bj"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bf, CountryIdentifier.Ne, CountryIdentifier.Ng, CountryIdentifier.Tg),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xof),
                        Calling = new CountryCalling(229)
                    },
                    CountryIdentifier.Dk => new CountryInfo("Denmark", "Kingdom of Denmark", "Danmark", CountryIdentifier.Dk, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".dk"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.De),
                        Currency = new CountryCurrency(CurrencyIdentifier.Dkk),
                        Calling = new CountryCalling(45)
                    },
                    CountryIdentifier.Dm => new CountryInfo("Dominica", "Commonwealth of Dominica", "Dominica", CountryIdentifier.Dm, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".dm"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Xcd),
                        Calling = new CountryCalling(1767)
                    },
                    CountryIdentifier.Do => new CountryInfo("Dominican Republic", "Dominican Republic", "República Dominicana", CountryIdentifier.Do, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".do"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ht),
                        Currency = new CountryCurrency(CurrencyIdentifier.Dop),
                        Calling = new CountryCalling(1809, 1829, 1849)
                    },
                    CountryIdentifier.Ec => new CountryInfo("Ecuador", "Republic of Ecuador", "Ecuador", CountryIdentifier.Ec, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".ec"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Co, CountryIdentifier.Pe),
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(593)
                    },
                    CountryIdentifier.Sv => new CountryInfo("El Salvador", "Republic of El Salvador", "El Salvador", CountryIdentifier.Sv, SubregionIdentifier.CentralAmerica)
                    {
                        Domain = new CountryDomain(".sv"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gt, CountryIdentifier.Hn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Svc, CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(503)
                    },
                    CountryIdentifier.Gq => new CountryInfo("Equatorial Guinea", "Republic of Equatorial Guinea", "Equatorial Guinea", CountryIdentifier.Gq,
                        SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".gq"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cm, CountryIdentifier.Ga),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xaf),
                        Calling = new CountryCalling(240)
                    },
                    CountryIdentifier.Et => new CountryInfo("Ethiopia", "Federal Democratic Republic of Ethiopia", "ኢትዮጵያ", CountryIdentifier.Et, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".et"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dj, CountryIdentifier.Er, CountryIdentifier.Ke, CountryIdentifier.So, CountryIdentifier.Ss,
                            CountryIdentifier.Sd),
                        Currency = new CountryCurrency(CurrencyIdentifier.Etb),
                        Calling = new CountryCalling(251)
                    },
                    CountryIdentifier.Er => new CountryInfo("Eritrea", "State of Eritrea", "ኤርትራ", CountryIdentifier.Er, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".er"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dj, CountryIdentifier.Et, CountryIdentifier.Sd),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ern),
                        Calling = new CountryCalling(291)
                    },
                    CountryIdentifier.Ee => new CountryInfo("Estonia", "Republic of Estonia", "Eesti", CountryIdentifier.Ee, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".ee"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Lv, CountryIdentifier.Ru),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(372)
                    },
                    CountryIdentifier.Fo => new CountryInfo("Faroe Islands", "Faroe Islands", "Føroyar", CountryIdentifier.Fo, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".fo"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Dkk),
                        Calling = new CountryCalling(298)
                    },
                    CountryIdentifier.Fk => new CountryInfo("Falkland Islands", "Falkland Islands", "Falkland Islands",
                        CountryIdentifier.Fk, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".fk"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Fkp),
                        Calling = new CountryCalling(500)
                    },
                    CountryIdentifier.Gs => new CountryInfo("South Georgia", "South Georgia and the South Sandwich Islands", "South Georgia",
                        CountryIdentifier.Gs, SubregionIdentifier.Antarctic)
                    {
                        Domain = new CountryDomain(".gs"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Gbp),
                        Calling = new CountryCalling(500)
                    },
                    CountryIdentifier.Fj => new CountryInfo("Fiji", "Republic of Fiji", "Fiji", CountryIdentifier.Fj, SubregionIdentifier.Melanesia)
                    {
                        Domain = new CountryDomain(".fj"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Fjd),
                        Calling = new CountryCalling(679)
                    },
                    CountryIdentifier.Fi => new CountryInfo("Finland", "Republic of Finland", "Suomi", CountryIdentifier.Fi, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".fi"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.No, CountryIdentifier.Se, CountryIdentifier.Ru),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(358)
                    },
                    CountryIdentifier.Ax => new CountryInfo("Åland Islands", "Åland Islands", "Åland", CountryIdentifier.Ax, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".ax"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(358)
                    },
                    CountryIdentifier.Fr => new CountryInfo("France", "French Republic", "France", CountryIdentifier.Fr, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".fr"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ad, CountryIdentifier.Be, CountryIdentifier.De, CountryIdentifier.It, CountryIdentifier.Lu,
                            CountryIdentifier.Mc, CountryIdentifier.Es, CountryIdentifier.Ch),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(33)
                    },
                    CountryIdentifier.Gf => new CountryInfo("French Guiana", "Guiana", "Guyane française", CountryIdentifier.Gf, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".gf"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Br, CountryIdentifier.Sr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(594)
                    },
                    CountryIdentifier.Pf => new CountryInfo("French Polynesia", "French Polynesia", "Polynésie française", CountryIdentifier.Pf, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".pf"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Zar),
                        Calling = new CountryCalling(689)
                    },
                    CountryIdentifier.Tf => new CountryInfo("French Southern and Antarctic Lands", "Territory of the French Southern and Antarctic Lands",
                        "Territoire des Terres australes et antarctiques françaises", CountryIdentifier.Tf, SubregionIdentifier.Antarctic)
                    {
                        Domain = new CountryDomain(".tf"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling()
                    },
                    CountryIdentifier.Dj => new CountryInfo("Djibouti", "Republic of Djibouti", "Djibouti", CountryIdentifier.Dj, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".dj"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Er, CountryIdentifier.Et, CountryIdentifier.So),
                        Currency = new CountryCurrency(CurrencyIdentifier.Djf),
                        Calling = new CountryCalling(253)
                    },
                    CountryIdentifier.Ga => new CountryInfo("Gabon", "Gabonese Republic", "Gabon", CountryIdentifier.Ga, SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".ga"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cm, CountryIdentifier.Cg, CountryIdentifier.Gq),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xaf),
                        Calling = new CountryCalling(241)
                    },
                    CountryIdentifier.Ge => new CountryInfo("Georgia", "Georgia", "საქართველო", CountryIdentifier.Ge, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".ge"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Am, CountryIdentifier.Az, CountryIdentifier.Ru, CountryIdentifier.Tr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Gel),
                        Calling = new CountryCalling(995)
                    },
                    CountryIdentifier.Gm => new CountryInfo("Gambia", "Republic of the Gambia", "Gambia", CountryIdentifier.Gm, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".gm"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Sn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Gmd),
                        Calling = new CountryCalling(220)
                    },
                    CountryIdentifier.Ps => new CountryInfo("Palestine", "State of Palestine", "فلسطين", CountryIdentifier.Ps, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".ps", "فلسطين."),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Il, CountryIdentifier.Eg, CountryIdentifier.Jo),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ils),
                        Calling = new CountryCalling(970)
                    },
                    CountryIdentifier.De => new CountryInfo("Germany", "Federal Republic of Germany", "Deutschland", CountryIdentifier.De, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".de"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.At, CountryIdentifier.Be, CountryIdentifier.Cz, CountryIdentifier.Dk, CountryIdentifier.Fr,
                            CountryIdentifier.Lu, CountryIdentifier.Nl, CountryIdentifier.Pl, CountryIdentifier.Ch),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(49)
                    },
                    CountryIdentifier.Gh => new CountryInfo("Ghana", "Republic of Ghana", "Ghana", CountryIdentifier.Gh, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".gh"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bf, CountryIdentifier.Ci, CountryIdentifier.Tg),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ghs),
                        Calling = new CountryCalling(233)
                    },
                    CountryIdentifier.Gi => new CountryInfo("Gibraltar", "Gibraltar", "Gibraltar", CountryIdentifier.Gi, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".gi"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Es),
                        Currency = new CountryCurrency(CurrencyIdentifier.Gip),
                        Calling = new CountryCalling(350)
                    },
                    CountryIdentifier.Ki => new CountryInfo("Kiribati", "Independent and Sovereign Republic of Kiribati", "Kiribati", CountryIdentifier.Ki,
                        SubregionIdentifier.Micronesia)
                    {
                        Domain = new CountryDomain(".ki"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Aud),
                        Calling = new CountryCalling(686)
                    },
                    CountryIdentifier.Gr => new CountryInfo("Greece", "Hellenic Republic", "ЕллЬдб", CountryIdentifier.Gr, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".gr"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Al, CountryIdentifier.Bg, CountryIdentifier.Tr, CountryIdentifier.Mk),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(30)
                    },
                    CountryIdentifier.Gl => new CountryInfo("Greenland", "Greenland", "Kalaallit Nunaat", CountryIdentifier.Gl, SubregionIdentifier.NorthAmerica)
                    {
                        Domain = new CountryDomain(".gl"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Dkk),
                        Calling = new CountryCalling(299)
                    },
                    CountryIdentifier.Gd => new CountryInfo("Grenada", "Grenada", "Grenada", CountryIdentifier.Gd, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".gd"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Xcd),
                        Calling = new CountryCalling(1473)
                    },
                    CountryIdentifier.Gp => new CountryInfo("Guadeloupe", "Guadeloupe", "Guadeloupe", CountryIdentifier.Gp, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".gp"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(590)
                    },
                    CountryIdentifier.Gu => new CountryInfo("Guam", "Guam", "Guam", CountryIdentifier.Gu, SubregionIdentifier.Micronesia)
                    {
                        Domain = new CountryDomain(".gu"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(1671)
                    },
                    CountryIdentifier.Gt => new CountryInfo("Guatemala", "Republic of Guatemala", "Guatemala", CountryIdentifier.Gt, SubregionIdentifier.CentralAmerica)
                    {
                        Domain = new CountryDomain(".gt"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bz, CountryIdentifier.Sv, CountryIdentifier.Hn, CountryIdentifier.Mx),
                        Currency = new CountryCurrency(CurrencyIdentifier.Gtq),
                        Calling = new CountryCalling(502)
                    },
                    CountryIdentifier.Gn => new CountryInfo("Guinea", "Republic of Guinea", "Guinée", CountryIdentifier.Gn, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".gn"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ci, CountryIdentifier.Gw, CountryIdentifier.Lr, CountryIdentifier.Ml, CountryIdentifier.Sn,
                            CountryIdentifier.Sl),
                        Currency = new CountryCurrency(CurrencyIdentifier.Gnf),
                        Calling = new CountryCalling(224)
                    },
                    CountryIdentifier.Gy => new CountryInfo("Guyana", "Co-operative Republic of Guyana", "Guyana", CountryIdentifier.Gy, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".gy"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Br, CountryIdentifier.Sr, CountryIdentifier.Ve),
                        Currency = new CountryCurrency(CurrencyIdentifier.Gyd),
                        Calling = new CountryCalling(592)
                    },
                    CountryIdentifier.Ht => new CountryInfo("Haiti", "Republic of Haiti", "Haïti", CountryIdentifier.Ht, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".ht"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Do),
                        Currency = new CountryCurrency(CurrencyIdentifier.Htg, CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(509)
                    },
                    CountryIdentifier.Hm => new CountryInfo("Heard Island and McDonald Islands", "Heard Island and McDonald Islands", "Heard Island and McDonald Islands",
                        CountryIdentifier.Hm, SubregionIdentifier.Antarctic)
                    {
                        Domain = new CountryDomain(".hm", ".aq", null),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Aud),
                        Calling = new CountryCalling()
                    },
                    CountryIdentifier.Va => new CountryInfo("Vatican City", "Vatican City State", "Stato della Città del Vaticano", CountryIdentifier.Va,
                        SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".va"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.It),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(379, 0, 0, 3906698)
                    },
                    CountryIdentifier.Hn => new CountryInfo("Honduras", "Republic of Honduras", "Honduras", CountryIdentifier.Hn, SubregionIdentifier.CentralAmerica)
                    {
                        Domain = new CountryDomain(".hn"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gt, CountryIdentifier.Sv, CountryIdentifier.Ni),
                        Currency = new CountryCurrency(CurrencyIdentifier.Hnl),
                        Calling = new CountryCalling(504)
                    },
                    CountryIdentifier.Hk => new CountryInfo("Hong Kong", "Hong Kong Special Administrative Region of the People's Republic of China", "香港", CountryIdentifier.Hk,
                        SubregionIdentifier.EasternAsia)
                    {
                        Domain = new CountryDomain(".hk", ".香港"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Hkd),
                        Calling = new CountryCalling(852)
                    },
                    CountryIdentifier.Hu => new CountryInfo("Hungary", "Hungary", "Magyarország", CountryIdentifier.Hu, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".hu"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.At, CountryIdentifier.Hr, CountryIdentifier.Ro, CountryIdentifier.Rs, CountryIdentifier.Sk,
                            CountryIdentifier.Si, CountryIdentifier.Ua),
                        Currency = new CountryCurrency(CurrencyIdentifier.Huf),
                        Calling = new CountryCalling(36)
                    },
                    CountryIdentifier.Is => new CountryInfo("Iceland", "Iceland", "Ísland", CountryIdentifier.Is, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".is"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Isk),
                        Calling = new CountryCalling(354)
                    },
                    CountryIdentifier.In => new CountryInfo("India", "Republic of India", "भारत", CountryIdentifier.In, SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".in"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Af, CountryIdentifier.Bd, CountryIdentifier.Bt, CountryIdentifier.Mm, CountryIdentifier.Cn,
                            CountryIdentifier.Np, CountryIdentifier.Pk, CountryIdentifier.Lk),
                        Currency = new CountryCurrency(CurrencyIdentifier.Inr),
                        Calling = new CountryCalling(91)
                    },
                    CountryIdentifier.Id => new CountryInfo("Indonesia", "Republic of Indonesia", "Indonesia", CountryIdentifier.Id, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".id"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Tl, CountryIdentifier.My, CountryIdentifier.Pg),
                        Currency = new CountryCurrency(CurrencyIdentifier.Idr),
                        Calling = new CountryCalling(62)
                    },
                    CountryIdentifier.Ir => new CountryInfo("Iran", "Islamic Republic of Iran", "ایران", CountryIdentifier.Ir, SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".ir", "ایران."),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Af, CountryIdentifier.Am, CountryIdentifier.Az, CountryIdentifier.Iq, CountryIdentifier.Pk,
                            CountryIdentifier.Tr, CountryIdentifier.Tm),
                        Currency = new CountryCurrency(CurrencyIdentifier.Irr),
                        Calling = new CountryCalling(98)
                    },
                    CountryIdentifier.Iq => new CountryInfo("Iraq", "Republic of Iraq", "العراق", CountryIdentifier.Iq, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".iq"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ir, CountryIdentifier.Jo, CountryIdentifier.Kw, CountryIdentifier.Sa, CountryIdentifier.Sy,
                            CountryIdentifier.Tr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Iqd),
                        Calling = new CountryCalling(964)
                    },
                    CountryIdentifier.Ie => new CountryInfo("Ireland", "Republic of Ireland", "Éire", CountryIdentifier.Ie, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".ie"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gb),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(353)
                    },
                    CountryIdentifier.Il => new CountryInfo("Israel", "State of Israel", "יִשְׂרָאֵל", CountryIdentifier.Il, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".il"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Eg, CountryIdentifier.Jo, CountryIdentifier.Lb, CountryIdentifier.Sy),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ils),
                        Calling = new CountryCalling(972)
                    },
                    CountryIdentifier.It => new CountryInfo("Italy", "Italian Republic", "Italia", CountryIdentifier.It, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".it"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.At, CountryIdentifier.Fr, CountryIdentifier.Sm, CountryIdentifier.Si, CountryIdentifier.Ch,
                            CountryIdentifier.Va),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(39)
                    },
                    CountryIdentifier.Ci => new CountryInfo("Ivory Coast", "Republic of Côte d'Ivoire", "Côte d'Ivoire", CountryIdentifier.Ci, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".ci"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bf, CountryIdentifier.Gh, CountryIdentifier.Gn, CountryIdentifier.Lr, CountryIdentifier.Ml),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xof),
                        Calling = new CountryCalling(225)
                    },
                    CountryIdentifier.Jm => new CountryInfo("Jamaica", "Jamaica", "Jamaica", CountryIdentifier.Jm, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".jm"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Jmd),
                        Calling = new CountryCalling(1876)
                    },
                    CountryIdentifier.Jp => new CountryInfo("Japan", "Japan", "日本", CountryIdentifier.Jp, SubregionIdentifier.EasternAsia)
                    {
                        Domain = new CountryDomain(".jp", ".みんな"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Jpy),
                        Calling = new CountryCalling(81)
                    },
                    CountryIdentifier.Kz => new CountryInfo("Kazakhstan", "Republic of Kazakhstan", "Қазақстан", CountryIdentifier.Kz, SubregionIdentifier.CentralAsia)
                    {
                        Domain = new CountryDomain(".kz", ".қаз"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cn, CountryIdentifier.Kg, CountryIdentifier.Ru, CountryIdentifier.Tm, CountryIdentifier.Uz),
                        Currency = new CountryCurrency(CurrencyIdentifier.Kzt),
                        Calling = new CountryCalling(76, 77)
                    },
                    CountryIdentifier.Jo => new CountryInfo("Jordan", "Hashemite Kingdom of Jordan", "الأردن", CountryIdentifier.Jo, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".jo", "الاردن."),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Iq, CountryIdentifier.Il, CountryIdentifier.Sa, CountryIdentifier.Sy),
                        Currency = new CountryCurrency(CurrencyIdentifier.Jod),
                        Calling = new CountryCalling(962)
                    },
                    CountryIdentifier.Ke => new CountryInfo("Kenya", "Republic of Kenya", "Kenya", CountryIdentifier.Ke, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".ke"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Et, CountryIdentifier.So, CountryIdentifier.Ss, CountryIdentifier.Tz, CountryIdentifier.Ug),
                        Currency = new CountryCurrency(CurrencyIdentifier.Kes),
                        Calling = new CountryCalling(254)
                    },
                    CountryIdentifier.Kp => new CountryInfo("North Korea", "Democratic People's Republic of Korea", "북한", CountryIdentifier.Kp, SubregionIdentifier.EasternAsia)
                    {
                        Domain = new CountryDomain(".kp"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cn, CountryIdentifier.Kr, CountryIdentifier.Ru),
                        Currency = new CountryCurrency(CurrencyIdentifier.Kpw),
                        Calling = new CountryCalling(850)
                    },
                    CountryIdentifier.Kr => new CountryInfo("South Korea", "Republic of Korea", "대한민국", CountryIdentifier.Kr, SubregionIdentifier.EasternAsia)
                    {
                        Domain = new CountryDomain(".kr", ".한국"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Kp),
                        Currency = new CountryCurrency(CurrencyIdentifier.Krw),
                        Calling = new CountryCalling(82)
                    },
                    CountryIdentifier.Kw => new CountryInfo("Kuwait", "State of Kuwait", "الكويت", CountryIdentifier.Kw, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".kw"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Iq, CountryIdentifier.Sa),
                        Currency = new CountryCurrency(CurrencyIdentifier.Kwd),
                        Calling = new CountryCalling(965)
                    },
                    CountryIdentifier.Kg => new CountryInfo("Kyrgyzstan", "Kyrgyz Republic", "Кыргызстан", CountryIdentifier.Kg, SubregionIdentifier.CentralAsia)
                    {
                        Domain = new CountryDomain(".kg"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cn, CountryIdentifier.Kz, CountryIdentifier.Tj, CountryIdentifier.Uz),
                        Currency = new CountryCurrency(CurrencyIdentifier.Kgs),
                        Calling = new CountryCalling(996)
                    },
                    CountryIdentifier.La => new CountryInfo("Laos", "Lao People's Democratic Republic", "ສປປລາວ", CountryIdentifier.La, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".la"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Mm, CountryIdentifier.Kh, CountryIdentifier.Cn, CountryIdentifier.Th, CountryIdentifier.Vn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Lak),
                        Calling = new CountryCalling(856)
                    },
                    CountryIdentifier.Lb => new CountryInfo("Lebanon", "Lebanese Republic", "لبنان", CountryIdentifier.Lb, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".lb"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Il, CountryIdentifier.Sy),
                        Currency = new CountryCurrency(CurrencyIdentifier.Lbp),
                        Calling = new CountryCalling(961)
                    },
                    CountryIdentifier.Ls => new CountryInfo("Lesotho", "Kingdom of Lesotho", "Lesotho", CountryIdentifier.Ls, SubregionIdentifier.SouthernAfrica)
                    {
                        Domain = new CountryDomain(".ls"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Za),
                        Currency = new CountryCurrency(CurrencyIdentifier.Lsl, CurrencyIdentifier.Xpf),
                        Calling = new CountryCalling(266)
                    },
                    CountryIdentifier.Lv => new CountryInfo("Latvia", "Republic of Latvia", "Latvija", CountryIdentifier.Lv, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".lv"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.By, CountryIdentifier.Ee, CountryIdentifier.Lt, CountryIdentifier.Ru),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(371)
                    },
                    CountryIdentifier.Lr => new CountryInfo("Liberia", "Republic of Liberia", "Liberia", CountryIdentifier.Lr, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".lr"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gn, CountryIdentifier.Ci, CountryIdentifier.Sl),
                        Currency = new CountryCurrency(CurrencyIdentifier.Lrd),
                        Calling = new CountryCalling(231)
                    },
                    CountryIdentifier.Ly => new CountryInfo("Libya", "State of Libya", "ليبيا", CountryIdentifier.Ly, SubregionIdentifier.NorthernAfrica)
                    {
                        Domain = new CountryDomain(".ly"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dz, CountryIdentifier.Td, CountryIdentifier.Eg, CountryIdentifier.Ne, CountryIdentifier.Sd,
                            CountryIdentifier.Tn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Lyd),
                        Calling = new CountryCalling(218)
                    },
                    CountryIdentifier.Li => new CountryInfo("Liechtenstein", "Principality of Liechtenstein", "Liechtenstein", CountryIdentifier.Li, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".li"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.At, CountryIdentifier.Ch),
                        Currency = new CountryCurrency(CurrencyIdentifier.Chf),
                        Calling = new CountryCalling(423)
                    },
                    CountryIdentifier.Lt => new CountryInfo("Lithuania", "Republic of Lithuania", "Lietuva", CountryIdentifier.Lt, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".lt"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.By, CountryIdentifier.Lv, CountryIdentifier.Pl, CountryIdentifier.Ru),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(370)
                    },
                    CountryIdentifier.Lu => new CountryInfo("Luxembourg", "Grand Duchy of Luxembourg", "Luxembourg", CountryIdentifier.Lu, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".lu"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Be, CountryIdentifier.Fr, CountryIdentifier.De),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(352)
                    },
                    CountryIdentifier.Mo => new CountryInfo("Macau", "Macao Special Administrative Region of the People's Republic of China", "澳門", CountryIdentifier.Mo,
                        SubregionIdentifier.EasternAsia)
                    {
                        Domain = new CountryDomain(".mo"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mop),
                        Calling = new CountryCalling(853)
                    },
                    CountryIdentifier.Mg => new CountryInfo("Madagascar", "Republic of Madagascar", "Madagasikara", CountryIdentifier.Mg, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".mg"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Mga),
                        Calling = new CountryCalling(261)
                    },
                    CountryIdentifier.Mw => new CountryInfo("Malawi", "Republic of Malawi", "Malawi", CountryIdentifier.Mw, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".mw"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Mz, CountryIdentifier.Tz, CountryIdentifier.Zm),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mwk),
                        Calling = new CountryCalling(265)
                    },
                    CountryIdentifier.My => new CountryInfo("Malaysia", "Malaysia", "Malaysia", CountryIdentifier.My, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".my"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bn, CountryIdentifier.Id, CountryIdentifier.Th),
                        Currency = new CountryCurrency(CurrencyIdentifier.Myr),
                        Calling = new CountryCalling(60)
                    },
                    CountryIdentifier.Mv => new CountryInfo("Maldives", "Republic of the Maldives", "Maldives", CountryIdentifier.Mv, SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".mv"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Mvr),
                        Calling = new CountryCalling(960)
                    },
                    CountryIdentifier.Ml => new CountryInfo("Mali", "Republic of Mali", "Mali", CountryIdentifier.Ml, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".ml"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dz, CountryIdentifier.Bf, CountryIdentifier.Gn, CountryIdentifier.Ci, CountryIdentifier.Mr,
                            CountryIdentifier.Ne, CountryIdentifier.Sn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xof),
                        Calling = new CountryCalling(223)
                    },
                    CountryIdentifier.Mt => new CountryInfo("Malta", "Republic of Malta", "Malta", CountryIdentifier.Mt, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".mt"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(356)
                    },
                    CountryIdentifier.Mq => new CountryInfo("Martinique", "Martinique", "Martinique", CountryIdentifier.Mq, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".mq"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(596)
                    },
                    CountryIdentifier.Mr => new CountryInfo("Mauritania", "Islamic Republic of Mauritania", "موريتانيا", CountryIdentifier.Mr, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".mr"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dz, CountryIdentifier.Ml, CountryIdentifier.Sn, CountryIdentifier.Eh),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mro),
                        Calling = new CountryCalling(222)
                    },
                    CountryIdentifier.Mu => new CountryInfo("Mauritius", "Republic of Mauritius", "Maurice", CountryIdentifier.Mu, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".mu"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Mur),
                        Calling = new CountryCalling(230)
                    },
                    CountryIdentifier.Mx => new CountryInfo("Mexico", "United Mexican States", "México", CountryIdentifier.Mx, SubregionIdentifier.NorthAmerica)
                    {
                        Domain = new CountryDomain(".mx"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bz, CountryIdentifier.Gt, CountryIdentifier.Us),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mxn),
                        Calling = new CountryCalling(52)
                    },
                    CountryIdentifier.Mc => new CountryInfo("Monaco", "Principality of Monaco", "Monaco", CountryIdentifier.Mc, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".mc"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Fr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(377)
                    },
                    CountryIdentifier.Mn => new CountryInfo("Mongolia", "Mongolia", "Монгол улс", CountryIdentifier.Mn, SubregionIdentifier.EasternAsia)
                    {
                        Domain = new CountryDomain(".mn"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cn, CountryIdentifier.Ru),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mnt),
                        Calling = new CountryCalling(976)
                    },
                    CountryIdentifier.Md => new CountryInfo("Moldova", "Republic of Moldova", "Moldova", CountryIdentifier.Md, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".md"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ro, CountryIdentifier.Ua),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mdl),
                        Calling = new CountryCalling(373)
                    },
                    CountryIdentifier.Me => new CountryInfo("Montenegro", "Montenegro", "Црна Гора", CountryIdentifier.Me, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".me"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Al, CountryIdentifier.Ba, CountryIdentifier.Hr, CountryIdentifier.Rs),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(382)
                    },
                    CountryIdentifier.Ms => new CountryInfo("Montserrat", "Montserrat", "Montserrat", CountryIdentifier.Ms, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".ms"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Xcd),
                        Calling = new CountryCalling(1664)
                    },
                    CountryIdentifier.Ma => new CountryInfo("Morocco", "Kingdom of Morocco", "المغرب", CountryIdentifier.Ma, SubregionIdentifier.NorthernAfrica)
                    {
                        Domain = new CountryDomain(".ma", "المغرب."),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dz, CountryIdentifier.Eh, CountryIdentifier.Es),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mad),
                        Calling = new CountryCalling(212)
                    },
                    CountryIdentifier.Mz => new CountryInfo("Mozambique", "Republic of Mozambique", "Moçambique", CountryIdentifier.Mz, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".mz"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Mw, CountryIdentifier.Za, CountryIdentifier.Sz, CountryIdentifier.Tz, CountryIdentifier.Zm,
                            CountryIdentifier.Zw),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mzn),
                        Calling = new CountryCalling(258)
                    },
                    CountryIdentifier.Om => new CountryInfo("Oman", "Sultanate of Oman", "عمان", CountryIdentifier.Om, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".om"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Sa, CountryIdentifier.Ae, CountryIdentifier.Ye),
                        Currency = new CountryCurrency(CurrencyIdentifier.Omr),
                        Calling = new CountryCalling(968)
                    },
                    CountryIdentifier.Na => new CountryInfo("Namibia", "Republic of Namibia", "Namibia", CountryIdentifier.Na, SubregionIdentifier.SouthernAfrica)
                    {
                        Domain = new CountryDomain(".na"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ao, CountryIdentifier.Bw, CountryIdentifier.Za, CountryIdentifier.Zm),
                        Currency = new CountryCurrency(CurrencyIdentifier.Nad, CurrencyIdentifier.Xpf),
                        Calling = new CountryCalling(264)
                    },
                    CountryIdentifier.Nr => new CountryInfo("Nauru", "Republic of Nauru", "Nauru", CountryIdentifier.Nr, SubregionIdentifier.Micronesia)
                    {
                        Domain = new CountryDomain(".nr"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Aud),
                        Calling = new CountryCalling(674)
                    },
                    CountryIdentifier.Np => new CountryInfo("Nepal", "Federal Democratic Republic of Nepal", "नेपाल", CountryIdentifier.Np, SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".np"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cn, CountryIdentifier.In),
                        Currency = new CountryCurrency(CurrencyIdentifier.Npr),
                        Calling = new CountryCalling(977)
                    },
                    CountryIdentifier.Nl => new CountryInfo("Netherlands", "Netherlands", "Nederland", CountryIdentifier.Nl, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".nl"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Be, CountryIdentifier.De),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(31)
                    },
                    CountryIdentifier.Cw => new CountryInfo("Curaçao", "Country of Curaçao", "Curaçao", CountryIdentifier.Cw, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".cw"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Ang),
                        Calling = new CountryCalling(5999)
                    },
                    CountryIdentifier.Aw => new CountryInfo("Aruba", "Aruba", "Aruba", CountryIdentifier.Aw, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".aw"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Awg),
                        Calling = new CountryCalling(297)
                    },
                    CountryIdentifier.Sx => new CountryInfo("Sint Maarten", "Sint Maarten", "Sint Maarten", CountryIdentifier.Sx, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".sx"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Mf),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ang),
                        Calling = new CountryCalling(1721)
                    },
                    CountryIdentifier.Bq => new CountryInfo("Caribbean Netherlands", "Caribbean Netherlands", "Caribbean Netherlands", CountryIdentifier.Bq,
                        SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".bq", ".nl", null),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(599)
                    },
                    CountryIdentifier.Nc => new CountryInfo("New Caledonia", "New Caledonia", "Nouvelle-Calédonie", CountryIdentifier.Nc, SubregionIdentifier.Melanesia)
                    {
                        Domain = new CountryDomain(".nc"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Zar),
                        Calling = new CountryCalling(687)
                    },
                    CountryIdentifier.Vu => new CountryInfo("Vanuatu", "Republic of Vanuatu", "Vanuatu", CountryIdentifier.Vu, SubregionIdentifier.Melanesia)
                    {
                        Domain = new CountryDomain(".vu"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Vuv),
                        Calling = new CountryCalling(678)
                    },
                    CountryIdentifier.Nz => new CountryInfo("New Zealand", "New Zealand", "New Zealand", CountryIdentifier.Nz, SubregionIdentifier.AustraliaAndNewZealand)
                    {
                        Domain = new CountryDomain(".nz"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Nzd),
                        Calling = new CountryCalling(64)
                    },
                    CountryIdentifier.Ni => new CountryInfo("Nicaragua", "Republic of Nicaragua", "Nicaragua", CountryIdentifier.Ni, SubregionIdentifier.CentralAmerica)
                    {
                        Domain = new CountryDomain(".ni"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cr, CountryIdentifier.Hn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Nio),
                        Calling = new CountryCalling(505)
                    },
                    CountryIdentifier.Ne => new CountryInfo("Niger", "Republic of Niger", "Niger", CountryIdentifier.Ne, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".ne"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dz, CountryIdentifier.Bj, CountryIdentifier.Bf, CountryIdentifier.Td, CountryIdentifier.Ly,
                            CountryIdentifier.Ml, CountryIdentifier.Ng),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xof),
                        Calling = new CountryCalling(227)
                    },
                    CountryIdentifier.Ng => new CountryInfo("Nigeria", "Federal Republic of Nigeria", "Nigeria", CountryIdentifier.Ng, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".ng"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bj, CountryIdentifier.Cm, CountryIdentifier.Td, CountryIdentifier.Ne),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ngn),
                        Calling = new CountryCalling(234)
                    },
                    CountryIdentifier.Nu => new CountryInfo("Niue", "Niue", "Niuē", CountryIdentifier.Nu, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".nu"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Nzd),
                        Calling = new CountryCalling(683)
                    },
                    CountryIdentifier.Nf => new CountryInfo("Norfolk Island", "Territory of Norfolk Island", "Norfolk Island", CountryIdentifier.Nf,
                        SubregionIdentifier.AustraliaAndNewZealand)
                    {
                        Domain = new CountryDomain(".nf"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Aud),
                        Calling = new CountryCalling(672)
                    },
                    CountryIdentifier.No => new CountryInfo("Norway", "Kingdom of Norway", "Norge", CountryIdentifier.No, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".no"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Fi, CountryIdentifier.Se, CountryIdentifier.Ru),
                        Currency = new CountryCurrency(CurrencyIdentifier.Nok),
                        Calling = new CountryCalling(47)
                    },
                    CountryIdentifier.Mp => new CountryInfo("Northern Mariana Islands", "Commonwealth of the Northern Mariana Islands", "Northern Mariana Islands", CountryIdentifier.Mp,
                        SubregionIdentifier.Micronesia)
                    {
                        Domain = new CountryDomain(".mp"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(1670)
                    },
                    CountryIdentifier.Um => new CountryInfo("United States Minor Outlying Islands", "United States Minor Outlying Islands", "United States Minor Outlying Islands",
                        CountryIdentifier.Um, SubregionIdentifier.NorthAmerica)
                    {
                        Domain = new CountryDomain(".us"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling()
                    },
                    CountryIdentifier.Fm => new CountryInfo("Micronesia", "Federated States of Micronesia", "Micronesia", CountryIdentifier.Fm, SubregionIdentifier.Micronesia)
                    {
                        Domain = new CountryDomain(".fm"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(691)
                    },
                    CountryIdentifier.Mh => new CountryInfo("Marshall Islands", "Republic of the Marshall Islands", "M̧ajeļ", CountryIdentifier.Mh, SubregionIdentifier.Micronesia)
                    {
                        Domain = new CountryDomain(".mh"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(692)
                    },
                    CountryIdentifier.Pw => new CountryInfo("Palau", "Republic of Palau", "Palau", CountryIdentifier.Pw, SubregionIdentifier.Micronesia)
                    {
                        Domain = new CountryDomain(".pw"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(680)
                    },
                    CountryIdentifier.Pk => new CountryInfo("Pakistan", "Islamic Republic of Pakistan", "Pakistan", CountryIdentifier.Pk, SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".pk"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Af, CountryIdentifier.Cn, CountryIdentifier.In, CountryIdentifier.Ir),
                        Currency = new CountryCurrency(CurrencyIdentifier.Pkr),
                        Calling = new CountryCalling(92)
                    },
                    CountryIdentifier.Pa => new CountryInfo("Panama", "Republic of Panama", "Panamá", CountryIdentifier.Pa, SubregionIdentifier.CentralAmerica)
                    {
                        Domain = new CountryDomain(".pa"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Co, CountryIdentifier.Cr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Pab, CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(507)
                    },
                    CountryIdentifier.Pg => new CountryInfo("Papua New Guinea", "Independent State of Papua New Guinea", "Papua Niugini", CountryIdentifier.Pg,
                        SubregionIdentifier.Melanesia)
                    {
                        Domain = new CountryDomain(".pg"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Id),
                        Currency = new CountryCurrency(CurrencyIdentifier.Pgk),
                        Calling = new CountryCalling(675)
                    },
                    CountryIdentifier.Py => new CountryInfo("Paraguay", "Republic of Paraguay", "Paraguay", CountryIdentifier.Py, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".py"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ar, CountryIdentifier.Bo, CountryIdentifier.Br),
                        Currency = new CountryCurrency(CurrencyIdentifier.Pyg),
                        Calling = new CountryCalling(595)
                    },
                    CountryIdentifier.Pe => new CountryInfo("Peru", "Republic of Peru", "Perú", CountryIdentifier.Pe, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".pe"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bo, CountryIdentifier.Br, CountryIdentifier.Cl, CountryIdentifier.Co, CountryIdentifier.Ec),
                        Currency = new CountryCurrency(CurrencyIdentifier.Pen),
                        Calling = new CountryCalling(51)
                    },
                    CountryIdentifier.Ph => new CountryInfo("Philippines", "Republic of the Philippines", "Pilipinas", CountryIdentifier.Ph, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".ph"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Php),
                        Calling = new CountryCalling(63)
                    },
                    CountryIdentifier.Pn => new CountryInfo("Pitcairn Islands", "Pitcairn Group of Islands", "Pitcairn Islands", CountryIdentifier.Pn, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".pn"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Nzd),
                        Calling = new CountryCalling(64)
                    },
                    CountryIdentifier.Pl => new CountryInfo("Poland", "Republic of Poland", "Polska", CountryIdentifier.Pl, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".pl"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.By, CountryIdentifier.Cz, CountryIdentifier.De, CountryIdentifier.Lt, CountryIdentifier.Ru,
                            CountryIdentifier.Sk, CountryIdentifier.Ua),
                        Currency = new CountryCurrency(CurrencyIdentifier.Pln),
                        Calling = new CountryCalling(48)
                    },
                    CountryIdentifier.Pt => new CountryInfo("Portugal", "Portuguese Republic", "Portugal", CountryIdentifier.Pt, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".pt"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Es),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(351)
                    },
                    CountryIdentifier.Gw => new CountryInfo("Guinea-Bissau", "Republic of Guinea-Bissau", "Guiné-Bissau", CountryIdentifier.Gw, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".gw"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gn, CountryIdentifier.Sn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xof),
                        Calling = new CountryCalling(245)
                    },
                    CountryIdentifier.Tl => new CountryInfo("Timor-Leste", "Democratic Republic of Timor-Leste", "Timor-Leste", CountryIdentifier.Tl,
                        SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".tl"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Id),
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(670)
                    },
                    CountryIdentifier.Pr => new CountryInfo("Puerto Rico", "Commonwealth of Puerto Rico", "Puerto Rico", CountryIdentifier.Pr, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".pr"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(1787, 1939)
                    },
                    CountryIdentifier.Qa => new CountryInfo("Qatar", "State of Qatar", "قطر", CountryIdentifier.Qa, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".qa", "قطر."),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Sa),
                        Currency = new CountryCurrency(CurrencyIdentifier.Qar),
                        Calling = new CountryCalling(974)
                    },
                    CountryIdentifier.Re => new CountryInfo("Réunion", "Réunion Island", "La Réunion", CountryIdentifier.Re, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".re"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(262)
                    },
                    CountryIdentifier.Ro => new CountryInfo("Romania", "Romania", "România", CountryIdentifier.Ro, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".ro"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bg, CountryIdentifier.Hu, CountryIdentifier.Md, CountryIdentifier.Rs, CountryIdentifier.Ua),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ron),
                        Calling = new CountryCalling(40)
                    },
                    CountryIdentifier.Ru => new CountryInfo("Russia", "Russian Federation", "Россия", CountryIdentifier.Ru, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".ru", ".su", ".рф"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Az, CountryIdentifier.By, CountryIdentifier.Cn, CountryIdentifier.Ee, CountryIdentifier.Fi,
                            CountryIdentifier.Ge, CountryIdentifier.Kz, CountryIdentifier.Kp, CountryIdentifier.Lv, CountryIdentifier.Lt, CountryIdentifier.Mn, CountryIdentifier.No,
                            CountryIdentifier.Pl, CountryIdentifier.Ua),
                        Currency = new CountryCurrency(CurrencyIdentifier.Rub),
                        Calling = new CountryCalling(7)
                    },
                    CountryIdentifier.Rw => new CountryInfo("Rwanda", "Republic of Rwanda", "Rwanda", CountryIdentifier.Rw, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".rw"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bi, CountryIdentifier.Cd, CountryIdentifier.Tz, CountryIdentifier.Ug),
                        Currency = new CountryCurrency(CurrencyIdentifier.Rwf),
                        Calling = new CountryCalling(250)
                    },
                    CountryIdentifier.Bl => new CountryInfo("Saint Barthélemy", "Collectivity of Saint BarthélemySaint Barthélemy", "Saint-Barthélemy", CountryIdentifier.Bl,
                        SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".bl"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(590)
                    },
                    CountryIdentifier.Sh => new CountryInfo("Saint Helena, Ascension and Tristan da Cunha", "Saint Helena, Ascension and Tristan da Cunha", "Saint Helena",
                        CountryIdentifier.Sh, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".sh", ".ac", null),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ar, CountryIdentifier.Br, CountryIdentifier.Cl, CountryIdentifier.Py, CountryIdentifier.Pe),
                        Currency = new CountryCurrency(CurrencyIdentifier.Shp, CurrencyIdentifier.Gbp),
                        Calling = new CountryCalling(290, 247)
                    },
                    CountryIdentifier.Kn => new CountryInfo("Saint Kitts and Nevis", "Federation of Saint Christopher and Nevisa", "Saint Kitts and Nevis", CountryIdentifier.Kn,
                        SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".kn"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Xcd),
                        Calling = new CountryCalling(1869)
                    },
                    CountryIdentifier.Ai => new CountryInfo("Anguilla", "Anguilla", "Anguilla", CountryIdentifier.Ai, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".ai"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Xcd),
                        Calling = new CountryCalling(1264)
                    },
                    CountryIdentifier.Lc => new CountryInfo("Saint Lucia", "Saint Lucia", "Saint Lucia", CountryIdentifier.Lc, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".lc"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Xcd),
                        Calling = new CountryCalling(1758)
                    },
                    CountryIdentifier.Mf => new CountryInfo("Saint Martin", "Saint Martin", "Saint Martin", CountryIdentifier.Mf, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".fr", ".gp", null),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Sx),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(590)
                    },
                    CountryIdentifier.Pm => new CountryInfo("Saint Pierre and Miquelon", "Saint Pierre and Miquelon", "Saint Pierre et Miquelon", CountryIdentifier.Pm,
                        SubregionIdentifier.NorthAmerica)
                    {
                        Domain = new CountryDomain(".pm"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(508)
                    },
                    CountryIdentifier.Vc => new CountryInfo("Saint Vincent and the Grenadines", "Saint Vincent and the Grenadines", "Saint Vincent and the Grenadines",
                        CountryIdentifier.Vc, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".vc"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Xcd),
                        Calling = new CountryCalling(1784)
                    },
                    CountryIdentifier.Sm => new CountryInfo("San Marino", "Most Serene Republic of San Marino", "San Marino", CountryIdentifier.Sm, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".sm"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.It),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(378)
                    },
                    CountryIdentifier.St => new CountryInfo("São Tomé and Príncipe", "Democratic Republic of São Tomé and Príncipe", "São Tomé e Príncipe", CountryIdentifier.St,
                        SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".st"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Std),
                        Calling = new CountryCalling(239)
                    },
                    CountryIdentifier.Sa => new CountryInfo("Saudi Arabia", "Kingdom of Saudi Arabia", "العربية السعودية", CountryIdentifier.Sa, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".sa", ".السعودية"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Iq, CountryIdentifier.Jo, CountryIdentifier.Kw, CountryIdentifier.Om, CountryIdentifier.Qa,
                            CountryIdentifier.Ae, CountryIdentifier.Ye),
                        Currency = new CountryCurrency(CurrencyIdentifier.Sar),
                        Calling = new CountryCalling(966)
                    },
                    CountryIdentifier.Sn => new CountryInfo("Senegal", "Republic of Senegal", "Sénégal", CountryIdentifier.Sn, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".sn"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gm, CountryIdentifier.Gn, CountryIdentifier.Gw, CountryIdentifier.Ml, CountryIdentifier.Mr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xof),
                        Calling = new CountryCalling(221)
                    },
                    CountryIdentifier.Rs => new CountryInfo("Serbia", "Republic of Serbia", "Србија", CountryIdentifier.Rs, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".rs", ".срб"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ba, CountryIdentifier.Bg, CountryIdentifier.Hr, CountryIdentifier.Hu, CountryIdentifier.Mk,
                            CountryIdentifier.Me, CountryIdentifier.Ro),
                        Currency = new CountryCurrency(CurrencyIdentifier.Rsd),
                        Calling = new CountryCalling(381)
                    },
                    CountryIdentifier.Sc => new CountryInfo("Seychelles", "Republic of Seychelles", "Seychelles", CountryIdentifier.Sc, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".sc"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Scr),
                        Calling = new CountryCalling(248)
                    },
                    CountryIdentifier.Sl => new CountryInfo("Sierra Leone", "Republic of Sierra Leone", "Sierra Leone", CountryIdentifier.Sl, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".sl"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gn, CountryIdentifier.Lr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Sll),
                        Calling = new CountryCalling(232)
                    },
                    CountryIdentifier.Sg => new CountryInfo("Singapore", "Republic of Singapore", "Singapore", CountryIdentifier.Sg, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".sg", null, ".新加坡", ".சிங்கப்பூர்"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Sgd),
                        Calling = new CountryCalling(65)
                    },
                    CountryIdentifier.Sk => new CountryInfo("Slovakia", "Slovak Republic", "Slovensko", CountryIdentifier.Sk, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".sk"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.At, CountryIdentifier.Cz, CountryIdentifier.Hu, CountryIdentifier.Pl, CountryIdentifier.Ua),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(421)
                    },
                    CountryIdentifier.Vn => new CountryInfo("Vietnam", "Socialist Republic of Vietnam", "Việt Nam", CountryIdentifier.Vn, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".vn"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Kh, CountryIdentifier.Cn, CountryIdentifier.La),
                        Currency = new CountryCurrency(CurrencyIdentifier.Vnd),
                        Calling = new CountryCalling(84)
                    },
                    CountryIdentifier.Si => new CountryInfo("Slovenia", "Republic of Slovenia", "Slovenija", CountryIdentifier.Si, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".si"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.At, CountryIdentifier.Hr, CountryIdentifier.It, CountryIdentifier.Hu),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(386)
                    },
                    CountryIdentifier.So => new CountryInfo("Somalia", "Federal Republic of Somalia", "Soomaaliya", CountryIdentifier.So, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".so"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dj, CountryIdentifier.Et, CountryIdentifier.Ke),
                        Currency = new CountryCurrency(CurrencyIdentifier.Sos),
                        Calling = new CountryCalling(252)
                    },
                    CountryIdentifier.Za => new CountryInfo("South Africa", "Republic of South Africa", "South Africa", CountryIdentifier.Za, SubregionIdentifier.SouthernAfrica)
                    {
                        Domain = new CountryDomain(".za"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bw, CountryIdentifier.Ls, CountryIdentifier.Mz, CountryIdentifier.Na, CountryIdentifier.Sz,
                            CountryIdentifier.Zw),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xpf),
                        Calling = new CountryCalling(27)
                    },
                    CountryIdentifier.Zw => new CountryInfo("Zimbabwe", "Republic of Zimbabwe", "Zimbabwe", CountryIdentifier.Zw, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".zw"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bw, CountryIdentifier.Mz, CountryIdentifier.Za, CountryIdentifier.Zm),
                        Currency = new CountryCurrency(CurrencyIdentifier.Zwl),
                        Calling = new CountryCalling(263)
                    },
                    CountryIdentifier.Es => new CountryInfo("Spain", "Kingdom of Spain", "España", CountryIdentifier.Es, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".es"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ad, CountryIdentifier.Fr, CountryIdentifier.Gi, CountryIdentifier.Pt, CountryIdentifier.Ma),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(34)
                    },
                    CountryIdentifier.Ss => new CountryInfo("South Sudan", "Republic of South Sudan", "South Sudan", CountryIdentifier.Ss, SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".ss"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cf, CountryIdentifier.Cd, CountryIdentifier.Et, CountryIdentifier.Ke, CountryIdentifier.Sd,
                            CountryIdentifier.Ug),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ssp),
                        Calling = new CountryCalling(211)
                    },
                    CountryIdentifier.Sd => new CountryInfo("Sudan", "Republic of the Sudan", "السودان", CountryIdentifier.Sd, SubregionIdentifier.NorthernAfrica)
                    {
                        Domain = new CountryDomain(".sd"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cf, CountryIdentifier.Td, CountryIdentifier.Eg, CountryIdentifier.Er, CountryIdentifier.Et,
                            CountryIdentifier.Ly, CountryIdentifier.Ss),
                        Currency = new CountryCurrency(CurrencyIdentifier.Sdg),
                        Calling = new CountryCalling(249)
                    },
                    CountryIdentifier.Eh => new CountryInfo("Western Sahara", "Sahrawi Arab Democratic Republic", "الصحراء الغربية", CountryIdentifier.Eh,
                        SubregionIdentifier.NorthernAfrica)
                    {
                        Domain = new CountryDomain(".eh"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dz, CountryIdentifier.Mr, CountryIdentifier.Ma),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mad, CurrencyIdentifier.Dzd, CurrencyIdentifier.Mro),
                        Calling = new CountryCalling(212)
                    },
                    CountryIdentifier.Sr => new CountryInfo("Suriname", "Republic of Suriname", "Suriname", CountryIdentifier.Sr, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".sr"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Br, CountryIdentifier.Gf, CountryIdentifier.Gy),
                        Currency = new CountryCurrency(CurrencyIdentifier.Srd),
                        Calling = new CountryCalling(597)
                    },
                    CountryIdentifier.Sj => new CountryInfo("Svalbard and Jan Mayen", "Svalbard og Jan Mayen", "Svalbard og Jan Mayen", CountryIdentifier.Sj,
                        SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".sj"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Nok),
                        Calling = new CountryCalling(4779)
                    },
                    CountryIdentifier.Sz => new CountryInfo("Eswatini", "Kingdom of Swaziland", "Swaziland", CountryIdentifier.Sz, SubregionIdentifier.SouthernAfrica)
                    {
                        Domain = new CountryDomain(".sz"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Mz, CountryIdentifier.Za),
                        Currency = new CountryCurrency(CurrencyIdentifier.Szl),
                        Calling = new CountryCalling(268)
                    },
                    CountryIdentifier.Se => new CountryInfo("Sweden", "Kingdom of Sweden", "Sverige", CountryIdentifier.Se, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".se"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Fi, CountryIdentifier.No),
                        Currency = new CountryCurrency(CurrencyIdentifier.Sek),
                        Calling = new CountryCalling(46)
                    },
                    CountryIdentifier.Ch => new CountryInfo("Switzerland", "Swiss Confederation", "Schweiz", CountryIdentifier.Ch, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".ch"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.At, CountryIdentifier.Fr, CountryIdentifier.It, CountryIdentifier.Li, CountryIdentifier.De),
                        Currency = new CountryCurrency(CurrencyIdentifier.Chf),
                        Calling = new CountryCalling(41)
                    },
                    CountryIdentifier.Sy => new CountryInfo("Syria", "Syrian Arab Republic", "سوريا", CountryIdentifier.Sy, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".sy", "سوريا."),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Iq, CountryIdentifier.Il, CountryIdentifier.Jo, CountryIdentifier.Lb, CountryIdentifier.Tr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Syp),
                        Calling = new CountryCalling(963)
                    },
                    CountryIdentifier.Tj => new CountryInfo("Tajikistan", "Republic of Tajikistan", "Тоҷикистон", CountryIdentifier.Tj, SubregionIdentifier.CentralAsia)
                    {
                        Domain = new CountryDomain(".tj"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Af, CountryIdentifier.Cn, CountryIdentifier.Kg, CountryIdentifier.Uz),
                        Currency = new CountryCurrency(CurrencyIdentifier.Tjs),
                        Calling = new CountryCalling(992)
                    },
                    CountryIdentifier.Th => new CountryInfo("Thailand", "Kingdom of Thailand", "ประเทศไทย", CountryIdentifier.Th, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".th", ".ไทย"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Mm, CountryIdentifier.Kh, CountryIdentifier.La, CountryIdentifier.My),
                        Currency = new CountryCurrency(CurrencyIdentifier.Thb),
                        Calling = new CountryCalling(66)
                    },
                    CountryIdentifier.Tg => new CountryInfo("Togo", "Togolese Republic", "Togo", CountryIdentifier.Tg, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".tg"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bj, CountryIdentifier.Bf, CountryIdentifier.Gh),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xof),
                        Calling = new CountryCalling(228)
                    },
                    CountryIdentifier.Tk => new CountryInfo("Tokelau", "Tokelau", "Tokelau", CountryIdentifier.Tk, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".tk"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Nzd),
                        Calling = new CountryCalling(690)
                    },
                    CountryIdentifier.To => new CountryInfo("Tonga", "Kingdom of Tonga", "Tonga", CountryIdentifier.To, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".to"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Top),
                        Calling = new CountryCalling(676)
                    },
                    CountryIdentifier.Tt => new CountryInfo("Trinidad and Tobago", "Republic of Trinidad and Tobago", "Trinidad and Tobago", CountryIdentifier.Tt,
                        SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".tt"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Ttd),
                        Calling = new CountryCalling(1868)
                    },
                    CountryIdentifier.Ae => new CountryInfo("United Arab Emirates", "United Arab Emirates", "دولة الإمارات العربية المتحدة", CountryIdentifier.Ae,
                        SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".ae", "امارات."),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Om, CountryIdentifier.Sa),
                        Currency = new CountryCurrency(CurrencyIdentifier.Aed),
                        Calling = new CountryCalling(971)
                    },
                    CountryIdentifier.Tn => new CountryInfo("Tunisia", "Tunisian Republic", "تونس", CountryIdentifier.Tn, SubregionIdentifier.NorthernAfrica)
                    {
                        Domain = new CountryDomain(".tn"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dz, CountryIdentifier.Ly),
                        Currency = new CountryCurrency(CurrencyIdentifier.Tnd),
                        Calling = new CountryCalling(216)
                    },
                    CountryIdentifier.Tr => new CountryInfo("Turkey", "Republic of Turkey", "Türkiye", CountryIdentifier.Tr, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".tr"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Am, CountryIdentifier.Az, CountryIdentifier.Bg, CountryIdentifier.Ge, CountryIdentifier.Gr,
                            CountryIdentifier.Ir, CountryIdentifier.Iq, CountryIdentifier.Sy),
                        Currency = new CountryCurrency(CurrencyIdentifier.Try),
                        Calling = new CountryCalling(90)
                    },
                    CountryIdentifier.Tm => new CountryInfo("Turkmenistan", "Turkmenistan", "Türkmenistan", CountryIdentifier.Tm, SubregionIdentifier.CentralAsia)
                    {
                        Domain = new CountryDomain(".tm"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Af, CountryIdentifier.Ir, CountryIdentifier.Kz, CountryIdentifier.Uz),
                        Currency = new CountryCurrency(CurrencyIdentifier.Tmt),
                        Calling = new CountryCalling(993)
                    },
                    CountryIdentifier.Tc => new CountryInfo("Turks and Caicos Islands", "Turks and Caicos Islands", "Turks and Caicos Islands", CountryIdentifier.Tc,
                        SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".tc"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(1649)
                    },
                    CountryIdentifier.Tv => new CountryInfo("Tuvalu", "Tuvalu", "Tuvalu", CountryIdentifier.Tv, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".tv"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Aud),
                        Calling = new CountryCalling(688)
                    },
                    CountryIdentifier.Ug => new CountryInfo("Uganda", "Republic of Uganda", "Uganda", CountryIdentifier.Ug, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".ug"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cd, CountryIdentifier.Ke, CountryIdentifier.Rw, CountryIdentifier.Ss, CountryIdentifier.Tz),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ugx),
                        Calling = new CountryCalling(256)
                    },
                    CountryIdentifier.Ua => new CountryInfo("Ukraine", "Ukraine", "Україна", CountryIdentifier.Ua, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".ua", ".укр"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.By, CountryIdentifier.Hu, CountryIdentifier.Md, CountryIdentifier.Pl, CountryIdentifier.Ro,
                            CountryIdentifier.Ru, CountryIdentifier.Sk),
                        Currency = new CountryCurrency(CurrencyIdentifier.Uah),
                        Calling = new CountryCalling(380)
                    },
                    CountryIdentifier.Mk => new CountryInfo("North Macedonia", "Republic of North Macedonia", "Северна Македонија", CountryIdentifier.Mk,
                        SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".mk"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Al, CountryIdentifier.Bg, CountryIdentifier.Gr, CountryIdentifier.Rs),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mkd),
                        Calling = new CountryCalling(389)
                    },
                    CountryIdentifier.Eg => new CountryInfo("Egypt", "Arab Republic of Egypt", "مصر", CountryIdentifier.Eg, SubregionIdentifier.NorthernAfrica)
                    {
                        Domain = new CountryDomain(".eg", ".مصر"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Il, CountryIdentifier.Ly, CountryIdentifier.Sd),
                        Currency = new CountryCurrency(CurrencyIdentifier.Egp),
                        Calling = new CountryCalling(20)
                    },
                    CountryIdentifier.Gb => new CountryInfo("United Kingdom", "United Kingdom of Great Britain and Northern Ireland", "United Kingdom", CountryIdentifier.Gb,
                        SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".uk"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ie),
                        Currency = new CountryCurrency(CurrencyIdentifier.Gbp),
                        Calling = new CountryCalling(44)
                    },
                    CountryIdentifier.Gg => new CountryInfo("Guernsey", "Bailiwick of Guernsey", "Guernsey", CountryIdentifier.Gg, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".gg"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Gbp),
                        Calling = new CountryCalling(44)
                    },
                    CountryIdentifier.Je => new CountryInfo("Jersey", "Bailiwick of Jersey", "Jersey", CountryIdentifier.Je, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".je"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Gbp),
                        Calling = new CountryCalling(44)
                    },
                    CountryIdentifier.Im => new CountryInfo("Isle of Man", "Isle of Man", "Isle of Man", CountryIdentifier.Im, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".im"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Gbp),
                        Calling = new CountryCalling(44)
                    },
                    CountryIdentifier.Tz => new CountryInfo("Tanzania", "United Republic of Tanzania", "Tanzania", CountryIdentifier.Tz, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".tz"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bi, CountryIdentifier.Cd, CountryIdentifier.Ke, CountryIdentifier.Mw, CountryIdentifier.Mz,
                            CountryIdentifier.Rw, CountryIdentifier.Ug, CountryIdentifier.Zm),
                        Currency = new CountryCurrency(CurrencyIdentifier.Tzs),
                        Calling = new CountryCalling(255)
                    },
                    CountryIdentifier.Us => new CountryInfo("United States", "United States of America", "United States", CountryIdentifier.Us, SubregionIdentifier.NorthAmerica)
                    {
                        Domain = new CountryDomain(".us"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ca, CountryIdentifier.Mx),
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(1)
                    },
                    CountryIdentifier.Vi => new CountryInfo("United States Virgin Islands", "Virgin Islands of the United States", "Virgin Islands of the United States",
                        CountryIdentifier.Vi, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".vi"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(1340)
                    },
                    CountryIdentifier.Bf => new CountryInfo("Burkina Faso", "Burkina Faso", "Burkina Faso", CountryIdentifier.Bf, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".bf"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bj, CountryIdentifier.Ci, CountryIdentifier.Gh, CountryIdentifier.Ml, CountryIdentifier.Ne,
                            CountryIdentifier.Tg),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xof),
                        Calling = new CountryCalling(226)
                    },
                    CountryIdentifier.Uy => new CountryInfo("Uruguay", "Oriental Republic of Uruguay", "Uruguay", CountryIdentifier.Uy, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".uy"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ar, CountryIdentifier.Br),
                        Currency = new CountryCurrency(CurrencyIdentifier.Uyu),
                        Calling = new CountryCalling(598)
                    },
                    CountryIdentifier.Uz => new CountryInfo("Uzbekistan", "Republic of Uzbekistan", "O‘zbekiston", CountryIdentifier.Uz, SubregionIdentifier.CentralAsia)
                    {
                        Domain = new CountryDomain(".uz"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Af, CountryIdentifier.Kz, CountryIdentifier.Kg, CountryIdentifier.Tj, CountryIdentifier.Tm),
                        Currency = new CountryCurrency(CurrencyIdentifier.Uzs),
                        Calling = new CountryCalling(998)
                    },
                    CountryIdentifier.Ve => new CountryInfo("Venezuela", "Bolivarian Republic of Venezuela", "Venezuela", CountryIdentifier.Ve, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".ve"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Br, CountryIdentifier.Co, CountryIdentifier.Gy),
                        Currency = new CountryCurrency(CurrencyIdentifier.Vef),
                        Calling = new CountryCalling(58)
                    },
                    CountryIdentifier.Wf => new CountryInfo("Wallis and Futuna", "Territory of the Wallis and Futuna Islands", "Wallis et Futuna", CountryIdentifier.Wf,
                        SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".wf"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Zar),
                        Calling = new CountryCalling(681)
                    },
                    CountryIdentifier.Ws => new CountryInfo("Samoa", "Independent State of Samoa", "Samoa", CountryIdentifier.Ws, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".ws"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Wst),
                        Calling = new CountryCalling(685)
                    },
                    CountryIdentifier.Ye => new CountryInfo("Yemen", "Republic of Yemen", "اليَمَن", CountryIdentifier.Ye, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".ye"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Om, CountryIdentifier.Sa),
                        Currency = new CountryCurrency(CurrencyIdentifier.Yer),
                        Calling = new CountryCalling(967)
                    },
                    CountryIdentifier.Zm => new CountryInfo("Zambia", "Republic of Zambia", "Zambia", CountryIdentifier.Zm, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".zm"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ao, CountryIdentifier.Bw, CountryIdentifier.Cd, CountryIdentifier.Mw, CountryIdentifier.Mz,
                            CountryIdentifier.Na, CountryIdentifier.Tz, CountryIdentifier.Zw),
                        Currency = new CountryCurrency(CurrencyIdentifier.Zmw),
                        Calling = new CountryCalling(260)
                    },
                    _ => throw new EnumUndefinedOrNotSupportedException<CountryIdentifier>(identifier, nameof(identifier), null)
                };
            }
        }
    }
}