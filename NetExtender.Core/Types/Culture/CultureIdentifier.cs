// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Culture
{
    public enum CultureIdentifier : UInt16
    {
        /// <summary>
        /// Invariant
        /// </summary>
        Invariant = 0x7F,

        /// <summary>
        /// Arabic
        /// </summary>
        Ar = 0x401,

        /// <summary>
        /// Bulgarian
        /// </summary>
        Bg = 0x402,

        /// <summary>
        /// Catalan
        /// </summary>
        Ca = 0x403,

        /// <summary>
        /// Chinese (Taiwan)
        /// </summary>
        Zh = 0x404,

        /// <summary>
        /// Czech
        /// </summary>
        Cs = 0x405,

        /// <summary>
        /// Danish
        /// </summary>
        Da = 0x406,

        /// <summary>
        /// German
        /// </summary>
        De = 0x407,

        /// <summary>
        /// Greek
        /// </summary>
        El = 0x408,

        /// <summary>
        /// English (United States)
        /// </summary>
        Us = 0x409,
        
        /// <summary>
        /// English (United Kingdom)
        /// </summary>
        En = 0x809,

        /// <summary>
        /// Spanish
        /// </summary>
        Es = 0x40A,

        /// <summary>
        /// Finnish
        /// </summary>
        Fi = 0x40B,

        /// <summary>
        /// French
        /// </summary>
        Fr = 0x40C,

        /// <summary>
        /// Hebrew
        /// </summary>
        He = 0x40D,

        /// <summary>
        /// Hungarian
        /// </summary>
        Hu = 0x40E,

        /// <summary>
        /// Icelandic
        /// </summary>
        Is = 0x40F,

        /// <summary>
        /// Italian
        /// </summary>
        It = 0x410,

        /// <summary>
        /// Japanese
        /// </summary>
        Ja = 0x411,

        /// <summary>
        /// Korean
        /// </summary>
        Ko = 0x412,

        /// <summary>
        /// Dutch
        /// </summary>
        Nl = 0x413,

        /// <summary>
        /// Norwegian
        /// </summary>
        Nb = 0x414,

        /// <summary>
        /// Polish
        /// </summary>
        Pl = 0x415,

        /// <summary>
        /// Portuguese
        /// </summary>
        Pt = 0x416,

        /// <summary>
        /// Rhaeto-Romanic
        /// </summary>
        Rm = 0x417,

        /// <summary>
        /// Romanian
        /// </summary>
        Ro = 0x418,

        /// <summary>
        /// Russian
        /// </summary>
        Ru = 0x419,

        /// <summary>
        /// Croatian
        /// </summary>
        Hr = 0x41A,

        /// <summary>
        /// Slovak
        /// </summary>
        Sk = 0x41B,

        /// <summary>
        /// Albanian
        /// </summary>
        Sq = 0x41C,

        /// <summary>
        /// Swedish
        /// </summary>
        Sv = 0x41D,

        /// <summary>
        /// Thai
        /// </summary>
        Th = 0x41E,

        /// <summary>
        /// Turkish
        /// </summary>
        Tr = 0x41F,

        /// <summary>
        /// Urdu
        /// </summary>
        Ur = 0x420,

        /// <summary>
        /// Indonesian
        /// </summary>
        Id = 0x421,

        /// <summary>
        /// Ukrainian
        /// </summary>
        Uk = 0x422,

        /// <summary>
        /// Belarusian
        /// </summary>
        Be = 0x423,

        /// <summary>
        /// Slovenian
        /// </summary>
        Sl = 0x424,
        
        /// <summary>
        /// Estonian
        /// </summary>
        Et = 0x425,
        
        /// <summary>
        /// Latvian
        /// </summary>
        Lv = 0x426,
        
        /// <summary>
        /// Lithuanian
        /// </summary>
        Lt = 0x427,
        
        /// <summary>
        /// Tajik
        /// </summary>
        Tg = 0x428,
        
        /// <summary>
        /// Persian
        /// </summary>
        Fa = 0x429,
        
        /// <summary>
        /// Vietnamese
        /// </summary>
        Vi = 0x42A,
        
        /// <summary>
        /// Armenian
        /// </summary>
        Hy = 0x42B,
        
        /// <summary>
        /// Azeri
        /// </summary>
        Az = 0x42C,
        
        /// <summary>
        /// Basque
        /// </summary>
        Eu = 0x42D,
        
        /// <summary>
        /// Sorbian
        /// </summary>
        Hsb = 0x42E,
        
        /// <summary>
        /// Macedonian
        /// </summary>
        Mk = 0x42F,
        
        /// <summary>
        /// Sutu
        /// </summary>
        St = 0x430,
        
        /// <summary>
        /// Tsonga
        /// </summary>
        Ts = 0x431,
        
        /// <summary>
        /// Tswana
        /// </summary>
        Tn = 0x432,
        
        /// <summary>
        /// Venda
        /// </summary>
        Ve = 0x433,
        
        /// <summary>
        /// Xhosa
        /// </summary>
        Xh = 0x434,
        
        /// <summary>
        /// Zulu
        /// </summary>
        Zu = 0x435,
        
        /// <summary>
        /// Afrikaans
        /// </summary>
        Af = 0x436,
        
        /// <summary>
        /// Georgian
        /// </summary>
        Ka = 0x437,
        
        /// <summary>
        /// Faroese
        /// </summary>
        Fo = 0x438,
        
        /// <summary>
        /// Hindi
        /// </summary>
        Hi = 0x439,
        
        /// <summary>
        /// Maltese
        /// </summary>
        Mt = 0x43A,
        
        /// <summary>
        /// Sami
        /// </summary>
        Se = 0x43B,
        
        /// <summary>
        /// Gaelic
        /// </summary>
        Gd = 0x43C,
        
        /// <summary>
        /// Yiddish
        /// </summary>
        Yi = 0x43D,
        
        /// <summary>
        /// Malaysian
        /// </summary>
        Ms = 0x43E,
        
        /// <summary>
        /// Kazakh
        /// </summary>
        Kk = 0x43F,
        
        /// <summary>
        /// Kyrgyz
        /// </summary>
        Ky = 0x440,
        
        /// <summary>
        /// Swahili
        /// </summary>
        Sw = 0x441,
        
        /// <summary>
        /// Turkmen
        /// </summary>
        Tk = 0x442,
        
        /// <summary>
        /// Uzbek
        /// </summary>
        Uz = 0x443,
        
        /// <summary>
        /// Tatar
        /// </summary>
        Tt = 0x444,
        
        /// <summary>
        /// Bengali
        /// </summary>
        Bn = 0x445,
        
        /// <summary>
        /// Punjabi
        /// </summary>
        Pa = 0x446,
        
        /// <summary>
        /// Gujarati
        /// </summary>
        Gu = 0x447,
        
        /// <summary>
        /// Oriya
        /// </summary>
        Or = 0x448,
        
        /// <summary>
        /// Tamil
        /// </summary>
        Ta = 0x449,
        
        /// <summary>
        /// Telugu
        /// </summary>
        Te = 0x44A,
        
        /// <summary>
        /// Kannada
        /// </summary>
        Kn = 0x44B,
        
        /// <summary>
        /// Malayalam
        /// </summary>
        Ml = 0x44C,
        
        /// <summary>
        /// Assamese
        /// </summary>
        As = 0x44D,
        
        /// <summary>
        /// Marathi
        /// </summary>
        Mr = 0x44E,
        
        /// <summary>
        /// Sanskrit
        /// </summary>
        Sa = 0x44F,
        
        /// <summary>
        /// Mongolian
        /// </summary>
        Mn = 0x450,
        
        /// <summary>
        /// Tibetan
        /// </summary>
        Bo = 0x451,
        
        /// <summary>
        /// Welsh
        /// </summary>
        Cy = 0x452,
        
        /// <summary>
        /// Khmer
        /// </summary>
        Km = 0x453,
        
        /// <summary>
        /// Lao
        /// </summary>
        Lo = 0x454,
        
        /// <summary>
        /// Burmese
        /// </summary>
        My = 0x455,
        
        /// <summary>
        /// Galician
        /// </summary>
        Gl = 0x456,
        
        /// <summary>
        /// Konkani
        /// </summary>
        Kok = 0x457,
        
        /// <summary>
        /// Manipuri
        /// </summary>
        Mni = 0x458,
        
        /// <summary>
        /// Sindhi
        /// </summary>
        Sd = 0x459,
        
        /// <summary>
        /// Syriac
        /// </summary>
        Syr = 0x45A,
        
        /// <summary>
        /// Sinhalese
        /// </summary>
        Si = 0x45B,
        
        /// <summary>
        /// Cherokee
        /// </summary>
        Chr = 0x45C,
        
        /// <summary>
        /// Inuktitut
        /// </summary>
        Inuktitut = 0x45D,
        
        /// <summary>
        /// Amharic
        /// </summary>
        Am = 0x45E,
        
        /// <summary>
        /// Tamazight
        /// </summary>
        Tmz = 0x45F,
        
        /// <summary>
        /// Kashmiri
        /// </summary>
        Ks = 0x460,
        
        /// <summary>
        /// Nepali
        /// </summary>
        Ne = 0x461,
        
        /// <summary>
        /// Frisian
        /// </summary>
        Fy = 0x462,
        
        /// <summary>
        /// Pashto
        /// </summary>
        Ps = 0x463,
        
        /// <summary>
        /// Filipino
        /// </summary>
        Fil = 0x464,
        
        /// <summary>
        /// Divehi
        /// </summary>
        Dv = 0x465,
        
        /// <summary>
        /// Edo
        /// </summary>
        Bin = 0x466,
        
        /// <summary>
        /// Fulfulde
        /// </summary>
        Fuv = 0x467,
        
        /// <summary>
        /// Hausa
        /// </summary>
        Ha = 0x468,
        
        /// <summary>
        /// Ibibio
        /// </summary>
        Ibb = 0x469,
        
        /// <summary>
        /// Yoruba
        /// </summary>
        Yo = 0x46A,
        
        /// <summary>
        /// Quechua
        /// </summary>
        Quz = 0x46B,
        
        /// <summary>
        /// Sepedi
        /// </summary>
        Nso = 0x46C,
        
        /// <summary>
        /// Bashkir
        /// </summary>
        Ba = 0x46D,
        
        /// <summary>
        /// Luxembourgish
        /// </summary>
        Lb = 0x46E,
        
        /// <summary>
        /// Greenlandic
        /// </summary>
        Kl = 0x46F,
        
        /// <summary>
        /// Igbo
        /// </summary>
        Ig = 0x470,
        
        /// <summary>
        /// Kanuri
        /// </summary>
        Kr = 0x471,
        
        /// <summary>
        /// Oromo
        /// </summary>
        Om = 0x472,
        
        /// <summary>
        /// Tigrigna
        /// </summary>
        Ti = 0x473,
        
        /// <summary>
        /// Guarani
        /// </summary>
        Gn = 0x474,
        
        /// <summary>
        /// Hawaiian
        /// </summary>
        Haw = 0x475,
        
        /// <summary>
        /// Latin
        /// </summary>
        La = 0x476,
        
        /// <summary>
        /// Somali
        /// </summary>
        So = 0x477,
        
        /// <summary>
        /// Yi
        /// </summary>
        Ii = 0x478,
        
        /// <summary>
        /// Papiamento
        /// </summary>
        Pap = 0x479,
        
        /// <summary>
        /// Mapudungun
        /// </summary>
        Arn = 0x47A,
        
        /// <summary>
        /// Mohawk
        /// </summary>
        Moh = 0x47C,
        
        /// <summary>
        /// Breton
        /// </summary>
        Br = 0x47E,

        /// <summary>
        /// Uyghur
        /// </summary>
        Ug = 0x480,
        
        /// <summary>
        /// Maori
        /// </summary>
        Mi = 0x481,
        
        /// <summary>
        /// Occitan
        /// </summary>
        Oc = 0x482,
        
        /// <summary>
        /// Corsican
        /// </summary>
        Co = 0x483,
        
        /// <summary>
        /// Alsatian
        /// </summary>
        Gsw = 0x484,
        
        /// <summary>
        /// Sakha
        /// </summary>
        Sah = 0x485,
        
        /// <summary>
        /// K'iche'
        /// </summary>
        Quc = 0x486,
        
        /// <summary>
        /// Kinyarwanda
        /// </summary>
        Rw = 0x487,
        
        /// <summary>
        /// Wolof
        /// </summary>
        Wo = 0x488,
        
        /// <summary>
        /// Dari
        /// </summary>
        Prs = 0x48C,
        
        /// <summary>
        /// Kurdish
        /// </summary>
        Ku = 0x492
    }
}