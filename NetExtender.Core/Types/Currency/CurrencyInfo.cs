// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using NetExtender.Types.Culture;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Region;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Currency
{
    public partial class CurrencyInfo : IEquatable<CurrencyInfo>
    {
        public static implicit operator UInt16(CurrencyInfo? info)
        {
            return info?.Code16 ?? 0;
        }

        public static implicit operator Int32(CurrencyInfo? info)
        {
            return info?.Code ?? 0;
        }

        public static implicit operator CurrencyIdentifier(CurrencyInfo? info)
        {
            return info?.Identifier ?? CurrencyIdentifier.Default;
        }

        public static implicit operator CurrencyInfo(CurrencyIdentifier identifier)
        {
            return CurrencyInfoStorage.Get(identifier);
        }

        public static implicit operator CurrencyInfo?(CountryIdentifier identifier)
        {
            return (CountryInfo) identifier;
        }

        [return: NotNullIfNotNull("info")]
        public static implicit operator String?(CurrencyInfo? info)
        {
            return info?.FullName;
        }

        public static implicit operator CurrencyInfo?(CultureIdentifier identifier)
        {
            return (CountryInfo?) identifier;
        }

        public static implicit operator CurrencyInfo?(LocalizationIdentifier identifier)
        {
            return (CountryInfo?) identifier;
        }

        public static implicit operator CurrencyInfo?(CultureInfo? info)
        {
            return info?.ToRegionInfo();
        }

        public static implicit operator CurrencyInfo?(RegionInfo? info)
        {
            return (CountryInfo?) info;
        }

        public static CurrencyInfo? CurrentCurrency
        {
            get
            {
                return CountryInfo.CurrentCountry;
            }
        }

        public static CurrencyInfo[] Currencies
        {
            get
            {
                return EnumUtilities.GetValuesWithoutDefault<CurrencyIdentifier>().Select(CurrencyInfoStorage.Get).ToArray();
            }
        }

        public String Symbol { get; }
        public String Name { get; }
        public String FullName { get; }

        public String ThreeLetterISOCurrencyName
        {
            get
            {
                return Identifier.ToString();
            }
        }

        public CurrencyIdentifier Identifier { get; }

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

        protected CurrencyInfo(String symbol, String name, String fullname, CurrencyIdentifier identifier)
        {
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            FullName = fullname ?? throw new ArgumentNullException(nameof(fullname));
            Identifier = identifier;
        }

        public override Int32 GetHashCode()
        {
            return Identifier.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other is CurrencyInfo info && Equals(info);
        }

        public Boolean Equals(CurrencyInfo? other)
        {
            return other is not null && Identifier == other.Identifier;
        }

        public override String ToString()
        {
            return $"{FullName} ({Symbol})";
        }
    }

    public partial class CurrencyInfo
    {
        private static class CurrencyInfoStorage
        {
            private static ConcurrentDictionary<CurrencyIdentifier, CurrencyInfo> Storage { get; } = new ConcurrentDictionary<CurrencyIdentifier, CurrencyInfo>();

            public static CurrencyInfo Get(CurrencyIdentifier identifier)
            {
                if (identifier == CurrencyIdentifier.Default)
                {
                    identifier = CurrencyIdentifier.Usd;
                }

                return Storage.GetOrAdd(identifier, Create);
            }

            private static CurrencyInfo Create(CurrencyIdentifier identifier)
            {
                return identifier switch
                {
                    CurrencyIdentifier.Default => Get(CurrencyIdentifier.Default),
                    CurrencyIdentifier.All => new CurrencyInfo("L", "Lek", "Albanian lek", CurrencyIdentifier.All),
                    CurrencyIdentifier.Dzd => new CurrencyInfo("د.ج.‏", "Dinar", "Algerian dinar", CurrencyIdentifier.Dzd),
                    CurrencyIdentifier.Ars => new CurrencyInfo("$", "Dollar", "Argentine peso", CurrencyIdentifier.Ars),
                    CurrencyIdentifier.Aud => new CurrencyInfo("$", "Dollar", "Australian dollar", CurrencyIdentifier.Aud),
                    CurrencyIdentifier.Bsd => new CurrencyInfo("$", "Dollar", "Bahamian dollar", CurrencyIdentifier.Bsd),
                    CurrencyIdentifier.Bhd => new CurrencyInfo("د.ب.‏", "Dinar", "Bahraini dinar", CurrencyIdentifier.Bhd),
                    CurrencyIdentifier.Bdt => new CurrencyInfo("৳", "Taka", "Bangladeshi taka", CurrencyIdentifier.Bdt),
                    CurrencyIdentifier.Amd => new CurrencyInfo("֏", "Dram", "Armenian dram", CurrencyIdentifier.Amd),
                    CurrencyIdentifier.Bbd => new CurrencyInfo("$", "Dollar", "Barbadian dollar", CurrencyIdentifier.Bbd),
                    CurrencyIdentifier.Bmd => new CurrencyInfo("$", "Dollar", "Bermudian dollar", CurrencyIdentifier.Bmd),
                    CurrencyIdentifier.Btn => new CurrencyInfo("Nu.", "Ngultrum", "Bhutanese ngultrum", CurrencyIdentifier.Btn),
                    CurrencyIdentifier.Bob => new CurrencyInfo("Bs", "Boliviano", "Bolivian boliviano", CurrencyIdentifier.Bob),
                    CurrencyIdentifier.Bwp => new CurrencyInfo("P", "Pula", "Botswana pula", CurrencyIdentifier.Bwp),
                    CurrencyIdentifier.Bzd => new CurrencyInfo("$", "Dollar", "Belize dollar", CurrencyIdentifier.Bzd),
                    CurrencyIdentifier.Sbd => new CurrencyInfo("$", "Solomon Islands dollar", "Solomon Islands dollar", CurrencyIdentifier.Sbd),
                    CurrencyIdentifier.Bnd => new CurrencyInfo("$", "Dollar", "Brunei dollar", CurrencyIdentifier.Bnd),
                    CurrencyIdentifier.Mmk => new CurrencyInfo("K", "Myanmar Kyat", "Myanmar Kyat", CurrencyIdentifier.Mmk),
                    CurrencyIdentifier.Bif => new CurrencyInfo("FBu", "Franc", "Burundian franc", CurrencyIdentifier.Bif),
                    CurrencyIdentifier.Khr => new CurrencyInfo("៛", "Cambodian Riel", "Cambodian Riel", CurrencyIdentifier.Khr),
                    CurrencyIdentifier.Cad => new CurrencyInfo("$", "Dollar", "Canadian dollar", CurrencyIdentifier.Cad),
                    CurrencyIdentifier.Cve => new CurrencyInfo("$", "Escudo", "Cape Verdean escudo", CurrencyIdentifier.Cve),
                    CurrencyIdentifier.Kyd => new CurrencyInfo("$", "Cayman Islands dollar", "Cayman Islands dollar", CurrencyIdentifier.Kyd),
                    CurrencyIdentifier.Lkr => new CurrencyInfo("රු.", "Sri Lankan Rupee", "Sri Lankan Rupee", CurrencyIdentifier.Lkr),
                    CurrencyIdentifier.Clp => new CurrencyInfo("$", "Peso", "Chilean peso", CurrencyIdentifier.Clp),
                    CurrencyIdentifier.Cny => new CurrencyInfo("¥", "Renminbi", "Renminbi", CurrencyIdentifier.Cny),
                    CurrencyIdentifier.Cop => new CurrencyInfo("$", "Peso", "Colombian peso", CurrencyIdentifier.Cop),
                    CurrencyIdentifier.Kmf => new CurrencyInfo("CF", "Comoro franc", "Comoro franc", CurrencyIdentifier.Kmf),
                    CurrencyIdentifier.Crc => new CurrencyInfo("₡", "Colón", "Costa Rican colon", CurrencyIdentifier.Crc),
                    CurrencyIdentifier.Hrk => new CurrencyInfo("kn", "Croatian Kuna", "Croatian Kuna", CurrencyIdentifier.Hrk),
                    CurrencyIdentifier.Cup => new CurrencyInfo("$", "Peso", "Cuban peso", CurrencyIdentifier.Cup),
                    CurrencyIdentifier.Czk => new CurrencyInfo("Kč", "Koruna", "Czech koruna", CurrencyIdentifier.Czk),
                    CurrencyIdentifier.Dkk => new CurrencyInfo("kr.", "Krone", "Danish krone", CurrencyIdentifier.Dkk),
                    CurrencyIdentifier.Dop => new CurrencyInfo("$", "Peso", "Dominican peso", CurrencyIdentifier.Dop),
                    CurrencyIdentifier.Svc => new CurrencyInfo("₡", "Colón", "Salvadoran colón", CurrencyIdentifier.Svc),
                    CurrencyIdentifier.Etb => new CurrencyInfo("ብር", "Birr", "Ethiopian birr", CurrencyIdentifier.Etb),
                    CurrencyIdentifier.Ern => new CurrencyInfo("Nfk", "Nakfa", "Eritrean nakfa", CurrencyIdentifier.Ern),
                    CurrencyIdentifier.Fkp => new CurrencyInfo("£", "Pound", "Falkland Islands pound", CurrencyIdentifier.Fkp),
                    CurrencyIdentifier.Fjd => new CurrencyInfo("FJ$", "Dollar", "Fiji dollar", CurrencyIdentifier.Fjd),
                    CurrencyIdentifier.Djf => new CurrencyInfo("Fdj", "Franc", "Djiboutian franc", CurrencyIdentifier.Djf),
                    CurrencyIdentifier.Gmd => new CurrencyInfo("D", "Gambian dalasi", "Gambian dalasi", CurrencyIdentifier.Gmd),
                    CurrencyIdentifier.Gip => new CurrencyInfo("Ј", "Gibraltar pound", "Gibraltar pound", CurrencyIdentifier.Gip),
                    CurrencyIdentifier.Gtq => new CurrencyInfo("Q", "Guatemalan Quetzal", "Guatemalan Quetzal", CurrencyIdentifier.Gtq),
                    CurrencyIdentifier.Gnf => new CurrencyInfo("FG", "Guinean franc", "Guinean franc", CurrencyIdentifier.Gnf),
                    CurrencyIdentifier.Gyd => new CurrencyInfo("$", "Dollar", "Guyanese dollar", CurrencyIdentifier.Gyd),
                    CurrencyIdentifier.Htg => new CurrencyInfo("G", "Haitian Gourde", "Haitian Gourde", CurrencyIdentifier.Htg),
                    CurrencyIdentifier.Hnl => new CurrencyInfo("L", "Honduran Lempira", "Honduran Lempira", CurrencyIdentifier.Hnl),
                    CurrencyIdentifier.Hkd => new CurrencyInfo("$", "Hong Kong Dollar", "Hong Kong Dollar", CurrencyIdentifier.Hkd),
                    CurrencyIdentifier.Huf => new CurrencyInfo("Ft", "Hungarian Forint", "Hungarian Forint", CurrencyIdentifier.Huf),
                    CurrencyIdentifier.Isk => new CurrencyInfo("kr", "Icelandic Króna", "Icelandic Króna", CurrencyIdentifier.Isk),
                    CurrencyIdentifier.Inr => new CurrencyInfo("₹", "Indian Rupee", "Indian Rupee", CurrencyIdentifier.Inr),
                    CurrencyIdentifier.Idr => new CurrencyInfo("Rp", "Indonesian Rupiah", "Indonesian Rupiah", CurrencyIdentifier.Idr),
                    CurrencyIdentifier.Irr => new CurrencyInfo("ريال", "Iranian Rial", "Iranian Rial", CurrencyIdentifier.Irr),
                    CurrencyIdentifier.Iqd => new CurrencyInfo("د.ع.‏", "Iraqi Dinar", "Iraqi Dinar", CurrencyIdentifier.Iqd),
                    CurrencyIdentifier.Ils => new CurrencyInfo("₪", "Israeli New Shekel", "Israeli New Shekel", CurrencyIdentifier.Ils),
                    CurrencyIdentifier.Jmd => new CurrencyInfo("$", "Jamaican Dollar", "Jamaican Dollar", CurrencyIdentifier.Jmd),
                    CurrencyIdentifier.Jpy => new CurrencyInfo("¥", "Japanese Yen", "Japanese Yen", CurrencyIdentifier.Jpy),
                    CurrencyIdentifier.Kzt => new CurrencyInfo("₸", "Kazakhstani Tenge", "Kazakhstani Tenge", CurrencyIdentifier.Kzt),
                    CurrencyIdentifier.Jod => new CurrencyInfo("د.ا.‏", "Jordanian Dinar", "Jordanian Dinar", CurrencyIdentifier.Jod),
                    CurrencyIdentifier.Kes => new CurrencyInfo("Ksh", "Kenyan Shilling", "Kenyan Shilling", CurrencyIdentifier.Kes),
                    CurrencyIdentifier.Kpw => new CurrencyInfo("₩", "North Korean won", "North Korean won", CurrencyIdentifier.Kpw),
                    CurrencyIdentifier.Krw => new CurrencyInfo("₩", "South Korean Won", "South Korean Won", CurrencyIdentifier.Krw),
                    CurrencyIdentifier.Kwd => new CurrencyInfo("د.ك.‏", "Kuwaiti Dinar", "Kuwaiti Dinar", CurrencyIdentifier.Kwd),
                    CurrencyIdentifier.Kgs => new CurrencyInfo("сом", "Kyrgystani Som", "Kyrgystani Som", CurrencyIdentifier.Kgs),
                    CurrencyIdentifier.Lak => new CurrencyInfo("₭", "Laotian Kip", "Laotian Kip", CurrencyIdentifier.Lak),
                    CurrencyIdentifier.Lbp => new CurrencyInfo("ل.ل.‏", "Lebanese Pound", "Lebanese Pound", CurrencyIdentifier.Lbp),
                    CurrencyIdentifier.Lsl => new CurrencyInfo("L", "Loti", "Lesotho loti", CurrencyIdentifier.Lsl),
                    CurrencyIdentifier.Lrd => new CurrencyInfo("$", "Liberian dollar", "Liberian dollar", CurrencyIdentifier.Lrd),
                    CurrencyIdentifier.Lyd => new CurrencyInfo("د.ل.‏", "Libyan Dinar", "Libyan Dinar", CurrencyIdentifier.Lyd),
                    CurrencyIdentifier.Mop => new CurrencyInfo("MOP", "Macanese Pataca", "Macanese Pataca", CurrencyIdentifier.Mop),
                    CurrencyIdentifier.Mwk => new CurrencyInfo("K", "Kwacha", "Malawian kwacha", CurrencyIdentifier.Mwk),
                    CurrencyIdentifier.Myr => new CurrencyInfo("RM", "Malaysian Ringgit", "Malaysian Ringgit", CurrencyIdentifier.Myr),
                    CurrencyIdentifier.Mvr => new CurrencyInfo("ރ.", "Maldivian Rufiyaa", "Maldivian Rufiyaa", CurrencyIdentifier.Mvr),
                    CurrencyIdentifier.Mro => new CurrencyInfo("UM", "Mauritanian ouguiya", "Mauritanian ouguiya", CurrencyIdentifier.Mro),
                    CurrencyIdentifier.Mur => new CurrencyInfo("₨", "Mauritian rupee", "Mauritian rupee", CurrencyIdentifier.Mur),
                    CurrencyIdentifier.Mxn => new CurrencyInfo("$", "Mexican Peso", "Mexican Peso", CurrencyIdentifier.Mxn),
                    CurrencyIdentifier.Mnt => new CurrencyInfo("₮", "Mongolian Tugrik", "Mongolian Tugrik", CurrencyIdentifier.Mnt),
                    CurrencyIdentifier.Mdl => new CurrencyInfo("L", "Moldovan Leu", "Moldovan Leu", CurrencyIdentifier.Mdl),
                    CurrencyIdentifier.Mad => new CurrencyInfo("د.م.‏", "Moroccan Dirham", "Moroccan Dirham", CurrencyIdentifier.Mad),
                    CurrencyIdentifier.Omr => new CurrencyInfo("ر.ع.‏", "Omani Rial", "Omani Rial", CurrencyIdentifier.Omr),
                    CurrencyIdentifier.Nad => new CurrencyInfo("$", "Namibian dollar", "Namibian dollar", CurrencyIdentifier.Nad),
                    CurrencyIdentifier.Npr => new CurrencyInfo("रु", "Nepalese Rupee", "Nepalese Rupee", CurrencyIdentifier.Npr),
                    CurrencyIdentifier.Ang => new CurrencyInfo("NAƒ", "Guilder", "Netherlands Antillean guilder", CurrencyIdentifier.Ang),
                    CurrencyIdentifier.Awg => new CurrencyInfo("Afl", "Florin", "Aruban florin", CurrencyIdentifier.Awg),
                    CurrencyIdentifier.Vuv => new CurrencyInfo("VT", "Vatu", "Vanuatu vatu", CurrencyIdentifier.Vuv),
                    CurrencyIdentifier.Nzd => new CurrencyInfo("$", "New Zealand Dollar", "New Zealand Dollar", CurrencyIdentifier.Nzd),
                    CurrencyIdentifier.Nio => new CurrencyInfo("C$", "Nicaraguan Córdoba", "Nicaraguan Córdoba", CurrencyIdentifier.Nio),
                    CurrencyIdentifier.Ngn => new CurrencyInfo("₦", "Nigerian Naira", "Nigerian Naira", CurrencyIdentifier.Ngn),
                    CurrencyIdentifier.Nok => new CurrencyInfo("kr", "Norwegian Krone", "Norwegian Krone", CurrencyIdentifier.Nok),
                    CurrencyIdentifier.Pkr => new CurrencyInfo("Rs", "Pakistani Rupee", "Pakistani Rupee", CurrencyIdentifier.Pkr),
                    CurrencyIdentifier.Pab => new CurrencyInfo("B/.", "Panamanian Balboa", "Panamanian Balboa", CurrencyIdentifier.Pab),
                    CurrencyIdentifier.Pgk => new CurrencyInfo("K", "Kina", "Papua New Guinean kina", CurrencyIdentifier.Pgk),
                    CurrencyIdentifier.Pyg => new CurrencyInfo("₲", "Paraguayan Guarani", "Paraguayan Guarani", CurrencyIdentifier.Pyg),
                    CurrencyIdentifier.Pen => new CurrencyInfo("S/", "Peruvian Sol", "Peruvian Sol", CurrencyIdentifier.Pen),
                    CurrencyIdentifier.Php => new CurrencyInfo("₱", "Philippine Piso", "Philippine Piso", CurrencyIdentifier.Php),
                    CurrencyIdentifier.Qar => new CurrencyInfo("ر.ق.‏", "Qatari Rial", "Qatari Rial", CurrencyIdentifier.Qar),
                    CurrencyIdentifier.Rub => new CurrencyInfo("₽", "Russian Ruble", "Russian Ruble", CurrencyIdentifier.Rub),
                    CurrencyIdentifier.Rwf => new CurrencyInfo("RF", "Rwandan Franc", "Rwandan Franc", CurrencyIdentifier.Rwf),
                    CurrencyIdentifier.Shp => new CurrencyInfo("Ј", "Saint Helena pound", "Saint Helena pound", CurrencyIdentifier.Shp),
                    CurrencyIdentifier.Std => new CurrencyInfo("Db", "Dobra", "São Tomé and Príncipe dobra", CurrencyIdentifier.Std),
                    CurrencyIdentifier.Sar => new CurrencyInfo("ر.س.‏", "Saudi Riyal", "Saudi Riyal", CurrencyIdentifier.Sar),
                    CurrencyIdentifier.Scr => new CurrencyInfo("SCR", "Rupee", "Seychelles rupee", CurrencyIdentifier.Scr),
                    CurrencyIdentifier.Sll => new CurrencyInfo("Le", "Leone", "Sierra Leonean leone", CurrencyIdentifier.Sll),
                    CurrencyIdentifier.Sgd => new CurrencyInfo("$", "Singapore Dollar", "Singapore Dollar", CurrencyIdentifier.Sgd),
                    CurrencyIdentifier.Vnd => new CurrencyInfo("₫", "Vietnamese Dong", "Vietnamese Dong", CurrencyIdentifier.Vnd),
                    CurrencyIdentifier.Sos => new CurrencyInfo("S", "Somali Shilling", "Somali Shilling", CurrencyIdentifier.Sos),
                    CurrencyIdentifier.Zar => new CurrencyInfo("R", "Rand", "South African Rand", CurrencyIdentifier.Zar),
                    CurrencyIdentifier.Ssp => new CurrencyInfo("SSЈ", "South Sudanese pound", "South Sudanese pound", CurrencyIdentifier.Ssp),
                    CurrencyIdentifier.Szl => new CurrencyInfo("L", "Swazi lilangeni", "Swazi lilangeni", CurrencyIdentifier.Szl),
                    CurrencyIdentifier.Sek => new CurrencyInfo("kr", "Swedish Krona", "Swedish Krona", CurrencyIdentifier.Sek),
                    CurrencyIdentifier.Chf => new CurrencyInfo("CHF", "Franc", "Swiss franc", CurrencyIdentifier.Chf),
                    CurrencyIdentifier.Syp => new CurrencyInfo("ل.س.‏", "Syrian Pound", "Syrian Pound", CurrencyIdentifier.Syp),
                    CurrencyIdentifier.Thb => new CurrencyInfo("฿", "Thai Baht", "Thai Baht", CurrencyIdentifier.Thb),
                    CurrencyIdentifier.Top => new CurrencyInfo("T$", "Paʻanga", "Tongan paʻanga", CurrencyIdentifier.Top),
                    CurrencyIdentifier.Ttd => new CurrencyInfo("$", "Trinidad and Tobago Dollar", "Trinidad and Tobago Dollar", CurrencyIdentifier.Ttd),
                    CurrencyIdentifier.Aed => new CurrencyInfo("د.إ.‏", "Dirham", "United Arab Emirates dirham", CurrencyIdentifier.Aed),
                    CurrencyIdentifier.Tnd => new CurrencyInfo("د.ت.‏", "Tunisian Dinar", "Tunisian Dinar", CurrencyIdentifier.Tnd),
                    CurrencyIdentifier.Ugx => new CurrencyInfo("USh", "Shilling", "Ugandan shilling", CurrencyIdentifier.Ugx),
                    CurrencyIdentifier.Mkd => new CurrencyInfo("ден", "Macedonian Denar", "Macedonian Denar", CurrencyIdentifier.Mkd),
                    CurrencyIdentifier.Egp => new CurrencyInfo("ج.م.‏", "Pound", "Egyptian pound", CurrencyIdentifier.Egp),
                    CurrencyIdentifier.Gbp => new CurrencyInfo("£", "British Pound", "British Pound", CurrencyIdentifier.Gbp),
                    CurrencyIdentifier.Tzs => new CurrencyInfo("TSh", "Shilling", "Tanzanian shilling", CurrencyIdentifier.Tzs),
                    CurrencyIdentifier.Usd => new CurrencyInfo("$", "US Dollar", "US Dollar", CurrencyIdentifier.Usd),
                    CurrencyIdentifier.Uyu => new CurrencyInfo("$", "Uruguayan Peso", "Uruguayan Peso", CurrencyIdentifier.Uyu),
                    CurrencyIdentifier.Uzs => new CurrencyInfo("сўм", "Uzbekistani Som", "Uzbekistani Som", CurrencyIdentifier.Uzs),
                    CurrencyIdentifier.Wst => new CurrencyInfo("WS$", "Samoan tala", "Samoan tala", CurrencyIdentifier.Wst),
                    CurrencyIdentifier.Yer => new CurrencyInfo("ر.ي.‏", "Yemeni Rial", "Yemeni Rial", CurrencyIdentifier.Yer),
                    CurrencyIdentifier.Twd => new CurrencyInfo("NT$", "New Taiwan Dollar", "New Taiwan Dollar", CurrencyIdentifier.Twd),
                    CurrencyIdentifier.Ves => new CurrencyInfo("Bs.S", "Venezuelan Bolívar", "Venezuelan Bolívar", CurrencyIdentifier.Ves),
                    CurrencyIdentifier.Cuc => new CurrencyInfo("$", "Peso", "Cuban convertible peso", CurrencyIdentifier.Cuc),
                    CurrencyIdentifier.Zwl => new CurrencyInfo("", "Zimbabwean dollar", "Zimbabwean dollar", CurrencyIdentifier.Zwl),
                    CurrencyIdentifier.Byn => new CurrencyInfo("Br", "Rouble", "Belarusian ruble", CurrencyIdentifier.Byn),
                    CurrencyIdentifier.Tmt => new CurrencyInfo("m.", "Turkmenistani Manat", "Turkmenistani Manat", CurrencyIdentifier.Tmt),
                    CurrencyIdentifier.Ghs => new CurrencyInfo("GH₵", "Ghanaian cedi", "Ghanaian cedi", CurrencyIdentifier.Ghs),
                    CurrencyIdentifier.Vef => new CurrencyInfo("Bs.S", "Bolívar soberano", "Venezuelan bolívar fuerte", CurrencyIdentifier.Vef),
                    CurrencyIdentifier.Sdg => new CurrencyInfo("SDG", "Sudanese pound", "Sudanese pound", CurrencyIdentifier.Sdg),
                    CurrencyIdentifier.Rsd => new CurrencyInfo("дин.", "Serbian Dinar", "Serbian Dinar", CurrencyIdentifier.Rsd),
                    CurrencyIdentifier.Mzn => new CurrencyInfo("MT", "Mozambican metical", "Mozambican metical", CurrencyIdentifier.Mzn),
                    CurrencyIdentifier.Azn => new CurrencyInfo("₼", "Manat", "Azerbaijani manat", CurrencyIdentifier.Azn),
                    CurrencyIdentifier.Ron => new CurrencyInfo("lei", "Romanian Leu", "Romanian Leu", CurrencyIdentifier.Ron),
                    CurrencyIdentifier.Try => new CurrencyInfo("₺", "Turkish Lira", "Turkish Lira", CurrencyIdentifier.Try),
                    CurrencyIdentifier.Xaf => new CurrencyInfo("FCFA", "Central African CFA Franc", "Central African CFA Franc", CurrencyIdentifier.Xaf),
                    CurrencyIdentifier.Xcd => new CurrencyInfo("EC$", "East Caribbean Dollar", "East Caribbean Dollar", CurrencyIdentifier.Xcd),
                    CurrencyIdentifier.Xof => new CurrencyInfo("CFA", "West African CFA Franc", "West African CFA Franc", CurrencyIdentifier.Xof),
                    CurrencyIdentifier.Xpf => new CurrencyInfo("", "CFP franc", "CFP franc", CurrencyIdentifier.Xpf),
                    CurrencyIdentifier.Xdr => new CurrencyInfo("XDR", "Special Drawing Rights", "Special Drawing Rights", CurrencyIdentifier.Xdr),
                    CurrencyIdentifier.Zmw => new CurrencyInfo("K", "Kwacha", "Zambian kwacha", CurrencyIdentifier.Zmw),
                    CurrencyIdentifier.Srd => new CurrencyInfo("$", "Surinamese dollar", "Surinamese dollar", CurrencyIdentifier.Srd),
                    CurrencyIdentifier.Mga => new CurrencyInfo("Ar", "Malagasy ariary", "Malagasy ariary", CurrencyIdentifier.Mga),
                    CurrencyIdentifier.Afn => new CurrencyInfo("؋", "Af", "Afghan afghani", CurrencyIdentifier.Afn),
                    CurrencyIdentifier.Tjs => new CurrencyInfo("смн", "Tajikistani Somoni", "Tajikistani Somoni", CurrencyIdentifier.Tjs),
                    CurrencyIdentifier.Aoa => new CurrencyInfo("Kz", "Kwanza", "Angolan kwanza", CurrencyIdentifier.Aoa),
                    CurrencyIdentifier.Bgn => new CurrencyInfo("лв.", "Lev", "Bulgarian lev", CurrencyIdentifier.Bgn),
                    CurrencyIdentifier.Cdf => new CurrencyInfo("FC", "Franc", "Congolese franc", CurrencyIdentifier.Cdf),
                    CurrencyIdentifier.Bam => new CurrencyInfo("КМ", "Mark", "Bosnia and Herzegovina convertible mark", CurrencyIdentifier.Bam),
                    CurrencyIdentifier.Eur => new CurrencyInfo("€", "Euro", "Euro", CurrencyIdentifier.Eur),
                    CurrencyIdentifier.Uah => new CurrencyInfo("₴", "Ukrainian Hryvnia", "Ukrainian Hryvnia", CurrencyIdentifier.Uah),
                    CurrencyIdentifier.Gel => new CurrencyInfo("₾", "Georgian Lari", "Georgian Lari", CurrencyIdentifier.Gel),
                    CurrencyIdentifier.Bov => new CurrencyInfo("", "Bolivian Mvdol", "Bolivian Mvdol", CurrencyIdentifier.Bov),
                    CurrencyIdentifier.Pln => new CurrencyInfo("zł", "Polish Zloty", "Polish Zloty", CurrencyIdentifier.Pln),
                    CurrencyIdentifier.Brl => new CurrencyInfo("R$", "Real", "Brazilian real", CurrencyIdentifier.Brl),
                    _ => throw new EnumUndefinedOrNotSupportedException<CurrencyIdentifier>(identifier, nameof(identifier), null)
                };
            }
        }
    }
}