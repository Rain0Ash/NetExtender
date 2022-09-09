// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Initializer.Types.Currency
{
    public enum CurrencyIdentifier : UInt16
    {
        /// <summary>
        /// Default
        /// </summary>
        Default = 0,
        
        /// <summary>
        /// Albanian lek
        /// </summary>
        All = 008,

        /// <summary>
        /// Algerian dinar
        /// </summary>
        Dzd = 012,

        /// <summary>
        /// Argentine peso
        /// </summary>
        Ars = 032,

        /// <summary>
        /// Australian dollar
        /// </summary>
        Aud = 036,

        /// <summary>
        /// Bahamian dollar
        /// </summary>
        Bsd = 044,

        /// <summary>
        /// Bahraini dinar
        /// </summary>
        Bhd = 048,

        /// <summary>
        /// Bangladeshi taka
        /// </summary>
        Bdt = 050,

        /// <summary>
        /// Armenian dram
        /// </summary>
        Amd = 051,

        /// <summary>
        /// Barbadian dollar
        /// </summary>
        Bbd = 052,

        /// <summary>
        /// Bermudian dollar
        /// </summary>
        Bmd = 060,

        /// <summary>
        /// Bhutanese ngultrum
        /// </summary>
        Btn = 064,

        /// <summary>
        /// Bolivian boliviano
        /// </summary>
        Bob = 068,

        /// <summary>
        /// Botswana pula
        /// </summary>
        Bwp = 072,

        /// <summary>
        /// Belize dollar
        /// </summary>
        Bzd = 084,

        /// <summary>
        /// Solomon Islands dollar
        /// </summary>
        Sbd = 090,

        /// <summary>
        /// Brunei dollar
        /// </summary>
        Bnd = 096,

        /// <summary>
        /// Myanmar Kyat
        /// </summary>
        Mmk = 104,

        /// <summary>
        /// Burundian franc
        /// </summary>
        Bif = 108,

        /// <summary>
        /// Cambodian Riel
        /// </summary>
        Khr = 116,

        /// <summary>
        /// Canadian dollar
        /// </summary>
        Cad = 124,

        /// <summary>
        /// Cape Verdean escudo
        /// </summary>
        Cve = 132,

        /// <summary>
        /// Cayman Islands dollar
        /// </summary>
        Kyd = 136,

        /// <summary>
        /// Sri Lankan Rupee
        /// </summary>
        Lkr = 144,

        /// <summary>
        /// Chilean peso
        /// </summary>
        Clp = 152,

        /// <summary>
        /// Renminbi
        /// </summary>
        Cny = 156,

        /// <summary>
        /// Colombian peso
        /// </summary>
        Cop = 170,

        /// <summary>
        /// Comoro franc
        /// </summary>
        Kmf = 174,

        /// <summary>
        /// Costa Rican colon
        /// </summary>
        Crc = 188,

        /// <summary>
        /// Croatian Kuna
        /// </summary>
        Hrk = 191,

        /// <summary>
        /// Cuban peso
        /// </summary>
        Cup = 192,

        /// <summary>
        /// Czech koruna
        /// </summary>
        Czk = 203,

        /// <summary>
        /// Danish krone
        /// </summary>
        Dkk = 208,

        /// <summary>
        /// Dominican peso
        /// </summary>
        Dop = 214,

        /// <summary>
        /// Salvadoran colon
        /// </summary>
        Svc = 222,

        /// <summary>
        /// Ethiopian birr
        /// </summary>
        Etb = 230,

        /// <summary>
        /// Eritrean nakfa
        /// </summary>
        Ern = 232,

        /// <summary>
        /// Falkland Islands pound
        /// </summary>
        Fkp = 238,

        /// <summary>
        /// Fiji dollar
        /// </summary>
        Fjd = 242,

        /// <summary>
        /// Djiboutian franc
        /// </summary>
        Djf = 262,

        /// <summary>
        /// Gambian dalasi
        /// </summary>
        Gmd = 270,

        /// <summary>
        /// Gibraltar pound
        /// </summary>
        Gip = 292,

        /// <summary>
        /// Guatemalan Quetzal
        /// </summary>
        Gtq = 320,

        /// <summary>
        /// Guinean franc
        /// </summary>
        Gnf = 324,

        /// <summary>
        /// Guyanese dollar
        /// </summary>
        Gyd = 328,

        /// <summary>
        /// Haitian Gourde
        /// </summary>
        Htg = 332,

        /// <summary>
        /// Honduran Lempira
        /// </summary>
        Hnl = 340,

        /// <summary>
        /// Hong Kong Dollar
        /// </summary>
        Hkd = 344,

        /// <summary>
        /// Hungarian Forint
        /// </summary>
        Huf = 348,

        /// <summary>
        /// Icelandic Krona
        /// </summary>
        Isk = 352,

        /// <summary>
        /// Indian Rupee
        /// </summary>
        Inr = 356,

        /// <summary>
        /// Indonesian Rupiah
        /// </summary>
        Idr = 360,

        /// <summary>
        /// Iranian Rial
        /// </summary>
        Irr = 364,

        /// <summary>
        /// Iraqi Dinar
        /// </summary>
        Iqd = 368,

        /// <summary>
        /// Israeli New Shekel
        /// </summary>
        Ils = 376,

        /// <summary>
        /// Jamaican Dollar
        /// </summary>
        Jmd = 388,

        /// <summary>
        /// Japanese Yen
        /// </summary>
        Jpy = 392,

        /// <summary>
        /// Kazakhstani Tenge
        /// </summary>
        Kzt = 398,

        /// <summary>
        /// Jordanian Dinar
        /// </summary>
        Jod = 400,

        /// <summary>
        /// Kenyan Shilling
        /// </summary>
        Kes = 404,

        /// <summary>
        /// North Korean won
        /// </summary>
        Kpw = 408,

        /// <summary>
        /// South Korean Won
        /// </summary>
        Krw = 410,

        /// <summary>
        /// Kuwaiti Dinar
        /// </summary>
        Kwd = 414,

        /// <summary>
        /// Kyrgystani Som
        /// </summary>
        Kgs = 417,

        /// <summary>
        /// Laotian Kip
        /// </summary>
        Lak = 418,

        /// <summary>
        /// Lebanese Pound
        /// </summary>
        Lbp = 422,

        /// <summary>
        /// Lesotho loti
        /// </summary>
        Lsl = 426,

        /// <summary>
        /// Liberian dollar
        /// </summary>
        Lrd = 430,

        /// <summary>
        /// Libyan Dinar
        /// </summary>
        Lyd = 434,

        /// <summary>
        /// Macanese Pataca
        /// </summary>
        Mop = 446,

        /// <summary>
        /// Malawian kwacha
        /// </summary>
        Mwk = 454,

        /// <summary>
        /// Malaysian Ringgit
        /// </summary>
        Myr = 458,

        /// <summary>
        /// Maldivian Rufiyaa
        /// </summary>
        Mvr = 462,

        /// <summary>
        ///     Mauritanian ouguiya
        /// </summary>
        Mro = 478,

        /// <summary>
        /// Mauritian rupee
        /// </summary>
        Mur = 480,

        /// <summary>
        /// Mexican Peso
        /// </summary>
        Mxn = 484,

        /// <summary>
        /// Mongolian Tugrik
        /// </summary>
        Mnt = 496,

        /// <summary>
        /// Moldovan Leu
        /// </summary>
        Mdl = 498,

        /// <summary>
        /// Moroccan Dirham
        /// </summary>
        Mad = 504,

        /// <summary>
        /// Omani Rial
        /// </summary>
        Omr = 512,

        /// <summary>
        /// Namibian dollar
        /// </summary>
        Nad = 516,

        /// <summary>
        /// Nepalese Rupee
        /// </summary>
        Npr = 524,

        /// <summary>
        /// Netherlands Antillean guilder
        /// </summary>
        Ang = 532,

        /// <summary>
        /// Aruban florin
        /// </summary>
        Awg = 533,

        /// <summary>
        /// Vanuatu vatu
        /// </summary>
        Vuv = 548,

        /// <summary>
        /// New Zealand Dollar
        /// </summary>
        Nzd = 554,

        /// <summary>
        /// Nicaraguan Cordoba
        /// </summary>
        Nio = 558,

        /// <summary>
        /// Nigerian Naira
        /// </summary>
        Ngn = 566,

        /// <summary>
        /// Norwegian Krone
        /// </summary>
        Nok = 578,

        /// <summary>
        /// Pakistani Rupee
        /// </summary>
        Pkr = 586,

        /// <summary>
        /// Panamanian Balboa
        /// </summary>
        Pab = 590,

        /// <summary>
        /// Papua New Guinean kina
        /// </summary>
        Pgk = 598,

        /// <summary>
        /// Paraguayan Guarani
        /// </summary>
        Pyg = 600,

        /// <summary>
        /// Peruvian Sol
        /// </summary>
        Pen = 604,

        /// <summary>
        /// Philippine Piso
        /// </summary>
        Php = 608,

        /// <summary>
        /// Qatari Rial
        /// </summary>
        Qar = 634,

        /// <summary>
        /// Russian Ruble
        /// </summary>
        Rub = 643,

        /// <summary>
        /// Rwandan Franc
        /// </summary>
        Rwf = 646,

        /// <summary>
        /// Saint Helena pound
        /// </summary>
        Shp = 654,

        /// <summary>
        /// Sao Tome and Principe dobra
        /// </summary>
        Std = 678,

        /// <summary>
        /// Saudi Riyal
        /// </summary>
        Sar = 682,

        /// <summary>
        /// Seychelles rupee
        /// </summary>
        Scr = 690,

        /// <summary>
        /// Sierra Leonean leone
        /// </summary>
        Sll = 694,

        /// <summary>
        /// Singapore Dollar
        /// </summary>
        Sgd = 702,

        /// <summary>
        /// Vietnamese Dong
        /// </summary>
        Vnd = 704,

        /// <summary>
        /// Somali Shilling
        /// </summary>
        Sos = 706,

        /// <summary>
        /// South African Rand
        /// </summary>
        Zar = 710,

        /// <summary>
        /// South Sudanese pound
        /// </summary>
        Ssp = 728,

        /// <summary>
        /// Swazi lilangeni
        /// </summary>
        Szl = 748,

        /// <summary>
        /// Swedish Krona
        /// </summary>
        Sek = 752,

        /// <summary>
        /// Swiss franc
        /// </summary>
        Chf = 756,

        /// <summary>
        /// Syrian Pound
        /// </summary>
        Syp = 760,

        /// <summary>
        /// Thai Baht
        /// </summary>
        Thb = 764,

        /// <summary>
        /// Tongan pa?anga
        /// </summary>
        Top = 776,

        /// <summary>
        /// Trinidad and Tobago Dollar
        /// </summary>
        Ttd = 780,

        /// <summary>
        /// United Arab Emirates dirham
        /// </summary>
        Aed = 784,

        /// <summary>
        /// Tunisian Dinar
        /// </summary>
        Tnd = 788,

        /// <summary>
        /// Ugandan shilling
        /// </summary>
        Ugx = 800,

        /// <summary>
        /// Macedonian Denar
        /// </summary>
        Mkd = 807,

        /// <summary>
        /// Egyptian pound
        /// </summary>
        Egp = 818,

        /// <summary>
        /// British Pound
        /// </summary>
        Gbp = 826,

        /// <summary>
        /// Tanzanian shilling
        /// </summary>
        Tzs = 834,

        /// <summary>
        /// US Dollar
        /// </summary>
        Usd = 840,

        /// <summary>
        /// Uruguayan Peso
        /// </summary>
        Uyu = 858,

        /// <summary>
        /// Uzbekistani Som
        /// </summary>
        Uzs = 860,

        /// <summary>
        /// Samoan tala
        /// </summary>
        Wst = 882,

        /// <summary>
        /// Yemeni Rial
        /// </summary>
        Yer = 886,

        /// <summary>
        /// New Taiwan Dollar
        /// </summary>
        Twd = 901,

        /// <summary>
        /// Venezuelan Bolivar
        /// </summary>
        Ves = 928,

        /// <summary>
        /// Cuban convertible peso
        /// </summary>
        Cuc = 931,

        /// <summary>
        /// Zimbabwean dollar
        /// </summary>
        Zwl = 932,

        /// <summary>
        /// Belarusian ruble
        /// </summary>
        Byn = 933,

        /// <summary>
        /// Turkmenistani Manat
        /// </summary>
        Tmt = 934,

        /// <summary>
        /// Ghanaian cedi
        /// </summary>
        Ghs = 936,

        /// <summary>
        /// Venezuelan bolivar fuerte
        /// </summary>
        Vef = 937,

        /// <summary>
        /// Sudanese pound
        /// </summary>
        Sdg = 938,

        /// <summary>
        /// Serbian Dinar
        /// </summary>
        Rsd = 941,

        /// <summary>
        /// Mozambican metical
        /// </summary>
        Mzn = 943,

        /// <summary>
        /// Azerbaijani manat
        /// </summary>
        Azn = 944,

        /// <summary>
        /// Romanian Leu
        /// </summary>
        Ron = 946,

        /// <summary>
        /// Turkish Lira
        /// </summary>
        Try = 949,

        /// <summary>
        /// Central African CFA Franc
        /// </summary>
        Xaf = 950,

        /// <summary>
        /// East Caribbean Dollar
        /// </summary>
        Xcd = 951,

        /// <summary>
        /// West African CFA Franc
        /// </summary>
        Xof = 952,

        /// <summary>
        /// CFP franc
        /// </summary>
        Xpf = 953,

        /// <summary>
        /// Special Drawing Rights
        /// </summary>
        Xdr = 960,

        /// <summary>
        /// Zambian kwacha
        /// </summary>
        Zmw = 967,

        /// <summary>
        /// Surinamese dollar
        /// </summary>
        Srd = 968,

        /// <summary>
        /// Malagasy ariary
        /// </summary>
        Mga = 969,

        /// <summary>
        /// Afghan afghani
        /// </summary>
        Afn = 971,

        /// <summary>
        /// Tajikistani Somoni
        /// </summary>
        Tjs = 972,

        /// <summary>
        /// Angolan kwanza
        /// </summary>
        Aoa = 973,

        /// <summary>
        /// Bulgarian lev
        /// </summary>
        Bgn = 975,

        /// <summary>
        /// Congolese franc
        /// </summary>
        Cdf = 976,

        /// <summary>
        /// Bosnia and Herzegovina convertible mark
        /// </summary>
        Bam = 977,

        /// <summary>
        /// Euro
        /// </summary>
        Eur = 978,

        /// <summary>
        /// Ukrainian Hryvnia
        /// </summary>
        Uah = 980,

        /// <summary>
        /// Georgian Lari
        /// </summary>
        Gel = 981,

        /// <summary>
        /// Bolivian Mvdol (funds code)
        /// </summary>
        Bov = 984,

        /// <summary>
        /// Polish Zloty
        /// </summary>
        Pln = 985,

        /// <summary>
        /// Brazilian real
        /// </summary>
        Brl = 986
    }
}