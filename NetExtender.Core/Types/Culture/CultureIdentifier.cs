// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;

namespace NetExtender.Types.Culture
{
    public enum CultureIdentifier : UInt16
    {
        /// <summary>
        /// Invariant
        /// </summary>
        [Description("Invariant")]
        Invariant = 0x7F,

        /// <summary>
        /// Arabic
        /// </summary>
        [Description("Arabic")]
        Ar = 0x401,

        /// <summary>
        /// Bulgarian
        /// </summary>
        [Description("Bulgarian")]
        Bg = 0x402,

        /// <summary>
        /// Catalan
        /// </summary>
        [Description("Catalan")]
        Ca = 0x403,

        /// <summary>
        /// Chinese (Taiwan)
        /// </summary>
        [Description("Chinese (Taiwan)")]
        Zh = 0x404,

        /// <summary>
        /// Czech
        /// </summary>
        [Description("Czech")]
        Cs = 0x405,

        /// <summary>
        /// Danish
        /// </summary>
        [Description("Danish")]
        Da = 0x406,

        /// <summary>
        /// German
        /// </summary>
        [Description("German")]
        De = 0x407,

        /// <summary>
        /// Greek
        /// </summary>
        [Description("Greek")]
        El = 0x408,

        /// <summary>
        /// English (United States)
        /// </summary>
        [Description("English (United States)")]
        Us = 0x409,
        
        /// <summary>
        /// English (United Kingdom)
        /// </summary>
        [Description("English (United Kingdom)")]
        En = 0x809,

        /// <summary>
        /// Spanish
        /// </summary>
        [Description("Spanish")]
        Es = 0x40A,

        /// <summary>
        /// Finnish
        /// </summary>
        [Description("Finnish")]
        Fi = 0x40B,

        /// <summary>
        /// French
        /// </summary>
        [Description("French")]
        Fr = 0x40C,

        /// <summary>
        /// Hebrew
        /// </summary>
        [Description("Hebrew")]
        He = 0x40D,

        /// <summary>
        /// Hungarian
        /// </summary>
        [Description("Hungarian")]
        Hu = 0x40E,

        /// <summary>
        /// Icelandic
        /// </summary>
        [Description("Icelandic")]
        Is = 0x40F,

        /// <summary>
        /// Italian
        /// </summary>
        [Description("Italian")]
        It = 0x410,

        /// <summary>
        /// Japanese
        /// </summary>
        [Description("Japanese")]
        Ja = 0x411,

        /// <summary>
        /// Korean
        /// </summary>
        [Description("Korean")]
        Ko = 0x412,

        /// <summary>
        /// Dutch
        /// </summary>
        [Description("Dutch")]
        Nl = 0x413,

        /// <summary>
        /// Norwegian
        /// </summary>
        [Description("Norwegian")]
        Nb = 0x414,

        /// <summary>
        /// Polish
        /// </summary>
        [Description("Polish")]
        Pl = 0x415,

        /// <summary>
        /// Portuguese
        /// </summary>
        [Description("Portuguese")]
        Pt = 0x416,

        /// <summary>
        /// Rhaeto-Romanic
        /// </summary>
        [Description("Rhaeto-Romanic")]
        Rm = 0x417,

        /// <summary>
        /// Romanian
        /// </summary>
        [Description("Romanian")]
        Ro = 0x418,

        /// <summary>
        /// Russian
        /// </summary>
        [Description("Russian")]
        Ru = 0x419,

        /// <summary>
        /// Croatian
        /// </summary>
        [Description("Croatian")]
        Hr = 0x41A,

        /// <summary>
        /// Slovak
        /// </summary>
        [Description("Slovak")]
        Sk = 0x41B,

        /// <summary>
        /// Albanian
        /// </summary>
        [Description("Albanian")]
        Sq = 0x41C,

        /// <summary>
        /// Swedish
        /// </summary>
        [Description("Swedish")]
        Sv = 0x41D,

        /// <summary>
        /// Thai
        /// </summary>
        [Description("Thai")]
        Th = 0x41E,

        /// <summary>
        /// Turkish
        /// </summary>
        [Description("Turkish")]
        Tr = 0x41F,

        /// <summary>
        /// Urdu
        /// </summary>
        [Description("Urdu")]
        Ur = 0x420,

        /// <summary>
        /// Indonesian
        /// </summary>
        [Description("Indonesian")]
        Id = 0x421,

        /// <summary>
        /// Ukrainian
        /// </summary>
        [Description("Ukrainian")]
        Uk = 0x422,

        /// <summary>
        /// Belarusian
        /// </summary>
        [Description("Belarusian")]
        Be = 0x423,

        /// <summary>
        /// Slovenian
        /// </summary>
        [Description("Slovenian")]
        Sl = 0x424,
        
        /// <summary>
        /// Estonian
        /// </summary>
        [Description("Estonian")]
        Et = 0x425,
        
        /// <summary>
        /// Latvian
        /// </summary>
        [Description("Latvian")]
        Lv = 0x426,
        
        /// <summary>
        /// Lithuanian
        /// </summary>
        [Description("Lithuanian")]
        Lt = 0x427,
        
        /// <summary>
        /// Tajik
        /// </summary>
        [Description("Tajik")]
        Tg = 0x428,
        
        /// <summary>
        /// Persian
        /// </summary>
        [Description("Persian")]
        Fa = 0x429,
        
        /// <summary>
        /// Vietnamese
        /// </summary>
        [Description("Vietnamese")]
        Vi = 0x42A,
        
        /// <summary>
        /// Armenian
        /// </summary>
        [Description("Armenian")]
        Hy = 0x42B,
        
        /// <summary>
        /// Azeri
        /// </summary>
        [Description("Azeri")]
        Az = 0x42C,
        
        /// <summary>
        /// Basque
        /// </summary>
        [Description("Basque")]
        Eu = 0x42D,
        
        /// <summary>
        /// Sorbian
        /// </summary>
        [Description("Sorbian")]
        Hsb = 0x42E,
        
        /// <summary>
        /// Macedonian
        /// </summary>
        [Description("Macedonian")]
        Mk = 0x42F,
        
        /// <summary>
        /// Sutu
        /// </summary>
        [Description("Sutu")]
        St = 0x430,
        
        /// <summary>
        /// Tsonga
        /// </summary>
        [Description("Tsonga")]
        Ts = 0x431,
        
        /// <summary>
        /// Tswana
        /// </summary>
        [Description("Tswana")]
        Tn = 0x432,
        
        /// <summary>
        /// Venda
        /// </summary>
        [Description("Venda")]
        Ve = 0x433,
        
        /// <summary>
        /// Xhosa
        /// </summary>
        [Description("Xhosa")]
        Xh = 0x434,
        
        /// <summary>
        /// Zulu
        /// </summary>
        [Description("Zulu")]
        Zu = 0x435,
        
        /// <summary>
        /// Afrikaans
        /// </summary>
        [Description("Afrikaans")]
        Af = 0x436,
        
        /// <summary>
        /// Georgian
        /// </summary>
        [Description("Georgian")]
        Ka = 0x437,
        
        /// <summary>
        /// Faroese
        /// </summary>
        [Description("Faroese")]
        Fo = 0x438,
        
        /// <summary>
        /// Hindi
        /// </summary>
        [Description("Hindi")]
        Hi = 0x439,
        
        /// <summary>
        /// Maltese
        /// </summary>
        [Description("Maltese")]
        Mt = 0x43A,
        
        /// <summary>
        /// Sami
        /// </summary>
        [Description("Sami")]
        Se = 0x43B,
        
        /// <summary>
        /// Gaelic
        /// </summary>
        [Description("Gaelic")]
        Gd = 0x43C,
        
        /// <summary>
        /// Yiddish
        /// </summary>
        [Description("Yiddish")]
        Yi = 0x43D,
        
        /// <summary>
        /// Malaysian
        /// </summary>
        [Description("Malaysian")]
        Ms = 0x43E,
        
        /// <summary>
        /// Kazakh
        /// </summary>
        [Description("Kazakh")]
        Kk = 0x43F,
        
        /// <summary>
        /// Kyrgyz
        /// </summary>
        [Description("Kyrgyz")]
        Ky = 0x440,
        
        /// <summary>
        /// Swahili
        /// </summary>
        [Description("Swahili")]
        Sw = 0x441,
        
        /// <summary>
        /// Turkmen
        /// </summary>
        [Description("Turkmen")]
        Tk = 0x442,
        
        /// <summary>
        /// Uzbek
        /// </summary>
        [Description("Uzbek")]
        Uz = 0x443,
        
        /// <summary>
        /// Tatar
        /// </summary>
        [Description("Tatar")]
        Tt = 0x444,
        
        /// <summary>
        /// Bengali
        /// </summary>
        [Description("Bengali")]
        Bn = 0x445,
        
        /// <summary>
        /// Punjabi
        /// </summary>
        [Description("Punjabi")]
        Pa = 0x446,
        
        /// <summary>
        /// Gujarati
        /// </summary>
        [Description("Gujarati")]
        Gu = 0x447,
        
        /// <summary>
        /// Oriya
        /// </summary>
        [Description("Oriya")]
        Or = 0x448,
        
        /// <summary>
        /// Tamil
        /// </summary>
        [Description("Tamil")]
        Ta = 0x449,
        
        /// <summary>
        /// Telugu
        /// </summary>
        [Description("Telugu")]
        Te = 0x44A,
        
        /// <summary>
        /// Kannada
        /// </summary>
        [Description("Kannada")]
        Kn = 0x44B,
        
        /// <summary>
        /// Malayalam
        /// </summary>
        [Description("Malayalam")]
        Ml = 0x44C,
        
        /// <summary>
        /// Assamese
        /// </summary>
        [Description("Assamese")]
        As = 0x44D,
        
        /// <summary>
        /// Marathi
        /// </summary>
        [Description("Marathi")]
        Mr = 0x44E,
        
        /// <summary>
        /// Sanskrit
        /// </summary>
        [Description("Sanskrit")]
        Sa = 0x44F,
        
        /// <summary>
        /// Mongolian
        /// </summary>
        [Description("Mongolian")]
        Mn = 0x450,
        
        /// <summary>
        /// Tibetan
        /// </summary>
        [Description("Tibetan")]
        Bo = 0x451,
        
        /// <summary>
        /// Welsh
        /// </summary>
        [Description("Welsh")]
        Cy = 0x452,
        
        /// <summary>
        /// Khmer
        /// </summary>
        [Description("Khmer")]
        Km = 0x453,
        
        /// <summary>
        /// Lao
        /// </summary>
        [Description("Lao")]
        Lo = 0x454,
        
        /// <summary>
        /// Burmese
        /// </summary>
        [Description("Burmese")]
        My = 0x455,
        
        /// <summary>
        /// Galician
        /// </summary>
        [Description("Galician")]
        Gl = 0x456,
        
        /// <summary>
        /// Konkani
        /// </summary>
        [Description("Konkani")]
        Kok = 0x457,
        
        /// <summary>
        /// Manipuri
        /// </summary>
        [Description("Manipuri")]
        Mni = 0x458,
        
        /// <summary>
        /// Sindhi
        /// </summary>
        [Description("Sindhi")]
        Sd = 0x459,
        
        /// <summary>
        /// Syriac
        /// </summary>
        [Description("Syriac")]
        Syr = 0x45A,
        
        /// <summary>
        /// Sinhalese
        /// </summary>
        [Description("Sinhalese")]
        Si = 0x45B,
        
        /// <summary>
        /// Cherokee
        /// </summary>
        [Description("Cherokee")]
        Chr = 0x45C,
        
        /// <summary>
        /// Inuktitut
        /// </summary>
        [Description("Inuktitut")]
        Inuktitut = 0x45D,
        
        /// <summary>
        /// Amharic
        /// </summary>
        [Description("Amharic")]
        Am = 0x45E,
        
        /// <summary>
        /// Tamazight
        /// </summary>
        [Description("Tamazight")]
        Tmz = 0x45F,
        
        /// <summary>
        /// Kashmiri
        /// </summary>
        [Description("Kashmiri")]
        Ks = 0x460,
        
        /// <summary>
        /// Nepali
        /// </summary>
        [Description("Nepali")]
        Ne = 0x461,
        
        /// <summary>
        /// Frisian
        /// </summary>
        [Description("Frisian")]
        Fy = 0x462,
        
        /// <summary>
        /// Pashto
        /// </summary>
        [Description("Pashto")]
        Ps = 0x463,
        
        /// <summary>
        /// Filipino
        /// </summary>
        [Description("Filipino")]
        Fil = 0x464,
        
        /// <summary>
        /// Divehi
        /// </summary>
        [Description("Divehi")]
        Dv = 0x465,
        
        /// <summary>
        /// Edo
        /// </summary>
        [Description("Edo")]
        Bin = 0x466,
        
        /// <summary>
        /// Fulfulde
        /// </summary>
        [Description("Fulfulde")]
        Fuv = 0x467,
        
        /// <summary>
        /// Hausa
        /// </summary>
        [Description("Hausa")]
        Ha = 0x468,
        
        /// <summary>
        /// Ibibio
        /// </summary>
        [Description("Ibibio")]
        Ibb = 0x469,
        
        /// <summary>
        /// Yoruba
        /// </summary>
        [Description("Yoruba")]
        Yo = 0x46A,
        
        /// <summary>
        /// Quechua
        /// </summary>
        [Description("Quechua")]
        Quz = 0x46B,
        
        /// <summary>
        /// Sepedi
        /// </summary>
        [Description("Sepedi")]
        Nso = 0x46C,
        
        /// <summary>
        /// Bashkir
        /// </summary>
        [Description("Bashkir")]
        Ba = 0x46D,
        
        /// <summary>
        /// Luxembourgish
        /// </summary>
        [Description("Luxembourgish")]
        Lb = 0x46E,
        
        /// <summary>
        /// Greenlandic
        /// </summary>
        [Description("Greenlandic")]
        Kl = 0x46F,
        
        /// <summary>
        /// Igbo
        /// </summary>
        [Description("Igbo")]
        Ig = 0x470,
        
        /// <summary>
        /// Kanuri
        /// </summary>
        [Description("Kanuri")]
        Kr = 0x471,
        
        /// <summary>
        /// Oromo
        /// </summary>
        [Description("Oromo")]
        Om = 0x472,
        
        /// <summary>
        /// Tigrigna
        /// </summary>
        [Description("Tigrigna")]
        Ti = 0x473,
        
        /// <summary>
        /// Guarani
        /// </summary>
        [Description("Guarani")]
        Gn = 0x474,
        
        /// <summary>
        /// Hawaiian
        /// </summary>
        [Description("Hawaiian")]
        Haw = 0x475,
        
        /// <summary>
        /// Latin
        /// </summary>
        [Description("Latin")]
        La = 0x476,
        
        /// <summary>
        /// Somali
        /// </summary>
        [Description("Somali")]
        So = 0x477,
        
        /// <summary>
        /// Yi
        /// </summary>
        [Description("Yi")]
        Ii = 0x478,
        
        /// <summary>
        /// Papiamento
        /// </summary>
        [Description("Papiamento")]
        Pap = 0x479,
        
        /// <summary>
        /// Mapudungun
        /// </summary>
        [Description("Mapudungun")]
        Arn = 0x47A,
        
        /// <summary>
        /// Mohawk
        /// </summary>
        [Description("Mohawk")]
        Moh = 0x47C,
        
        /// <summary>
        /// Breton
        /// </summary>
        [Description("Breton")]
        Br = 0x47E,

        /// <summary>
        /// Uyghur
        /// </summary>
        [Description("Uyghur")]
        Ug = 0x480,
        
        /// <summary>
        /// Maori
        /// </summary>
        [Description("Maori")]
        Mi = 0x481,
        
        /// <summary>
        /// Occitan
        /// </summary>
        [Description("Occitan")]
        Oc = 0x482,
        
        /// <summary>
        /// Corsican
        /// </summary>
        [Description("Corsican")]
        Co = 0x483,
        
        /// <summary>
        /// Alsatian
        /// </summary>
        [Description("Alsatian")]
        Gsw = 0x484,
        
        /// <summary>
        /// Sakha
        /// </summary>
        [Description("Sakha")]
        Sah = 0x485,
        
        /// <summary>
        /// K'iche'
        /// </summary>
        [Description("K'iche'")]
        Quc = 0x486,
        
        /// <summary>
        /// Kinyarwanda
        /// </summary>
        [Description("Kinyarwanda")]
        Rw = 0x487,
        
        /// <summary>
        /// Wolof
        /// </summary>
        [Description("Wolof")]
        Wo = 0x488,
        
        /// <summary>
        /// Dari
        /// </summary>
        [Description("Dari")]
        Prs = 0x48C,
        
        /// <summary>
        /// Kurdish
        /// </summary>
        [Description("Kurdish")]
        Ku = 0x492
    }
}