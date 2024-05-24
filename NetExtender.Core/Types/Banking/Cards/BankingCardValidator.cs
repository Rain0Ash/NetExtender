// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Text.RegularExpressions;
using NetExtender.Types.Banking.Cards.Interfaces;

namespace NetExtender.Types.Banking.Cards
{
    public abstract class BankingCardValidator : IBankingCardValidator
    {
        public static IBankingCardValidator Simple
        {
            get
            {
                return SimpleValidator.Instance;
            }
        }
        
        public static IBankingCardValidator AmericanExpress
        {
            get
            {
                return AmericanExpressValidator.Instance;
            }
        }

        public static IBankingCardValidator Dankort
        {
            get
            {
                return DankortValidator.Instance;
            }
        }

        public static IBankingCardValidator DinersClub
        {
            get
            {
                return DinersClubValidator.Instance;
            }
        }

        public static IBankingCardValidator Discovery
        {
            get
            {
                return DiscoveryValidator.Instance;
            }
        }

        public static IBankingCardValidator Forbrugsforeningen
        {
            get
            {
                return ForbrugsforeningenValidator.Instance;
            }
        }

        public static IBankingCardValidator HiperCard
        {
            get
            {
                return HiperCardValidator.Instance;
            }
        }

        public static IBankingCardValidator Jcb
        {
            get
            {
                return JcbValidator.Instance;
            }
        }

        public static IBankingCardValidator Maestro
        {
            get
            {
                return MaestroValidator.Instance;
            }
        }

        public static IBankingCardValidator MasterCard
        {
            get
            {
                return MasterCardValidator.Instance;
            }
        }

        public static IBankingCardValidator Mir
        {
            get
            {
                return MirValidator.Instance;
            }
        }

        public static IBankingCardValidator RalfRinger
        {
            get
            {
                return RalfRingerValidator.Instance;
            }
        }

        public static IBankingCardValidator Troy
        {
            get
            {
                return TroyValidator.Instance;
            }
        }

        public static IBankingCardValidator UnionPay
        {
            get
            {
                return UnionPayValidator.Instance;
            }
        }

        public static IBankingCardValidator Visa
        {
            get
            {
                return VisaValidator.Instance;
            }
        }

        public static IBankingCardValidator VisaElectron
        {
            get
            {
                return VisaElectronValidator.Instance;
            }
        }

        public static IBankingCardValidator YvesRocher
        {
            get
            {
                return YvesRocherValidator.Instance;
            }
        }

        public abstract BankingCardType Type { get; }
        protected IBankingCardChecksumValidator? Validator { get; }
        
        protected virtual Regex? Pattern
        {
            get
            {
                return null;
            }
        }

        protected static Regex Clear { get; } = new Regex(@"\D", RegexOptions.Compiled);

        protected BankingCardValidator(IBankingCardChecksumValidator? validator)
        {
            Validator = validator;
        }

        public Boolean Validate(UInt64 number)
        {
            return Validate(number.ToString());
        }

        public virtual Boolean Validate(String number)
        {
            number = Clear.Replace(number, String.Empty);
            return IsLength(number) && IsPattern(number) && IsNumber(number);
        }

        protected virtual Boolean IsLength(String number)
        {
            return number.Length > 0;
        }

        protected virtual Boolean IsPattern(String number)
        {
            return Pattern?.IsMatch(number) is not false;
        }

        protected virtual Boolean IsNumber(String number)
        {
            return Validator?.Validate(number) is not false;
        }

        protected abstract BankingCardValidator With(IBankingCardChecksumValidator? validator);

        IBankingCardValidator IBankingCardValidator.With(IBankingCardChecksumValidator? validator)
        {
            return With(validator);
        }
        
        private sealed class SimpleValidator : BankingCardValidator
        {
            public static SimpleValidator Instance { get; } = new SimpleValidator();
            
            public override BankingCardType Type
            {
                get
                {
                    return BankingCardType.Simple;
                }
            }

            private SimpleValidator()
                : this(BankingCardChecksumValidator.Default)
            {
            }

            private SimpleValidator(IBankingCardChecksumValidator? validator)
                : base(validator)
            {
            }

            protected override SimpleValidator With(IBankingCardChecksumValidator? validator)
            {
                return validator is not null ? new SimpleValidator(validator) : Instance;
            }
        }
        
        private sealed class AmericanExpressValidator : BankingCardValidator
        {
            public static AmericanExpressValidator Instance { get; } = new AmericanExpressValidator();
            
            public override BankingCardType Type
            {
                get
                {
                    return BankingCardType.AmericanExpress;
                }
            }
            
            private static readonly Regex pattern = new Regex(@"^3[47][0-9]", RegexOptions.Compiled);
            protected override Regex Pattern
            {
                get
                {
                    return pattern;
                }
            }

            private AmericanExpressValidator()
                : this(BankingCardChecksumValidator.Default)
            {
            }

            private AmericanExpressValidator(IBankingCardChecksumValidator? validator)
                : base(validator)
            {
            }

            protected override Boolean IsLength(String number)
            {
                return number.Length switch
                {
                    15 => true,
                    16 => true,
                    _ => false
                };
            }
            
            protected override AmericanExpressValidator With(IBankingCardChecksumValidator? validator)
            {
                return validator is not null ? new AmericanExpressValidator(validator) : Instance;
            }
        }
        
        private sealed class DankortValidator : BankingCardValidator
        {
            public static DankortValidator Instance { get; } = new DankortValidator();
            
            public override BankingCardType Type
            {
                get
                {
                    return BankingCardType.Dankort;
                }
            }
            
            private static readonly Regex pattern = new Regex(@"^5019", RegexOptions.Compiled);
            protected override Regex Pattern
            {
                get
                {
                    return pattern;
                }
            }

            private DankortValidator()
                : this(BankingCardChecksumValidator.Default)
            {
            }

            private DankortValidator(IBankingCardChecksumValidator? validator)
                : base(validator)
            {
            }

            protected override Boolean IsLength(String number)
            {
                return number.Length switch
                {
                    16 => true,
                    _ => false
                };
            }
            
            protected override DankortValidator With(IBankingCardChecksumValidator? validator)
            {
                return validator is not null ? new DankortValidator(validator) : Instance;
            }
        }
        
        private sealed class DinersClubValidator : BankingCardValidator
        {
            public static DinersClubValidator Instance { get; } = new DinersClubValidator();
            
            public override BankingCardType Type
            {
                get
                {
                    return BankingCardType.DinersClub;
                }
            }
            
            private static readonly Regex pattern = new Regex(@"^3(0[0-5]|[68][0-9])[0-9]", RegexOptions.Compiled);
            protected override Regex Pattern
            {
                get
                {
                    return pattern;
                }
            }

            private DinersClubValidator()
                : this(BankingCardChecksumValidator.Default)
            {
            }

            private DinersClubValidator(IBankingCardChecksumValidator? validator)
                : base(validator)
            {
            }

            protected override Boolean IsLength(String number)
            {
                return number.Length switch
                {
                    14 => true,
                    _ => false
                };
            }
            
            protected override DinersClubValidator With(IBankingCardChecksumValidator? validator)
            {
                return validator is not null ? new DinersClubValidator(validator) : Instance;
            }
        }
        
        private sealed class DiscoveryValidator : BankingCardValidator
        {
            public static DiscoveryValidator Instance { get; } = new DiscoveryValidator();
            
            public override BankingCardType Type
            {
                get
                {
                    return BankingCardType.Discovery;
                }
            }
            
            private static readonly Regex pattern = new Regex(@"^6(011|22126|22925|4[4-9]|5)", RegexOptions.Compiled);
            protected override Regex Pattern
            {
                get
                {
                    return pattern;
                }
            }

            private DiscoveryValidator()
                : this(BankingCardChecksumValidator.Default)
            {
            }

            private DiscoveryValidator(IBankingCardChecksumValidator? validator)
                : base(validator)
            {
            }

            protected override Boolean IsLength(String number)
            {
                return number.Length switch
                {
                    16 => true,
                    _ => false
                };
            }
            
            protected override DiscoveryValidator With(IBankingCardChecksumValidator? validator)
            {
                return validator is not null ? new DiscoveryValidator(validator) : Instance;
            }
        }
        
        private sealed class ForbrugsforeningenValidator : BankingCardValidator
        {
            public static ForbrugsforeningenValidator Instance { get; } = new ForbrugsforeningenValidator();
            
            public override BankingCardType Type
            {
                get
                {
                    return BankingCardType.Forbrugsforeningen;
                }
            }
            
            private static readonly Regex pattern = new Regex(@"^600", RegexOptions.Compiled);
            protected override Regex Pattern
            {
                get
                {
                    return pattern;
                }
            }

            private ForbrugsforeningenValidator()
                : this(BankingCardChecksumValidator.Default)
            {
            }

            private ForbrugsforeningenValidator(IBankingCardChecksumValidator? validator)
                : base(validator)
            {
            }

            protected override Boolean IsLength(String number)
            {
                return number.Length switch
                {
                    16 => true,
                    _ => false
                };
            }
            
            protected override ForbrugsforeningenValidator With(IBankingCardChecksumValidator? validator)
            {
                return validator is not null ? new ForbrugsforeningenValidator(validator) : Instance;
            }
        }
        
        private sealed class HiperCardValidator : BankingCardValidator
        {
            public static HiperCardValidator Instance { get; } = new HiperCardValidator();
            
            public override BankingCardType Type
            {
                get
                {
                    return BankingCardType.HiperCard;
                }
            }
            
            private static readonly Regex pattern = new Regex(@"^(606282\d{7}(\d{3})?)|(3841\d{15})", RegexOptions.Compiled);
            protected override Regex Pattern
            {
                get
                {
                    return pattern;
                }
            }

            private HiperCardValidator()
                : this(BankingCardChecksumValidator.Default)
            {
            }

            private HiperCardValidator(IBankingCardChecksumValidator? validator)
                : base(validator)
            {
            }

            protected override Boolean IsLength(String number)
            {
                return number.Length switch
                {
                    13 => true,
                    16 => true,
                    19 => true,
                    _ => false
                };
            }
            
            protected override HiperCardValidator With(IBankingCardChecksumValidator? validator)
            {
                return validator is not null ? new HiperCardValidator(validator) : Instance;
            }
        }
        
        private sealed class JcbValidator : BankingCardValidator
        {
            public static JcbValidator Instance { get; } = new JcbValidator();
            
            public override BankingCardType Type
            {
                get
                {
                    return BankingCardType.Jcb;
                }
            }
            
            private static readonly Regex pattern = new Regex(@"^(?:2131|1800|35\d{3})", RegexOptions.Compiled);
            protected override Regex Pattern
            {
                get
                {
                    return pattern;
                }
            }

            private JcbValidator()
                : this(BankingCardChecksumValidator.Default)
            {
            }

            private JcbValidator(IBankingCardChecksumValidator? validator)
                : base(validator)
            {
            }

            protected override Boolean IsLength(String number)
            {
                return number.Length switch
                {
                    16 => true,
                    17 => true,
                    18 => true,
                    19 => true,
                    _ => false
                };
            }
            
            protected override JcbValidator With(IBankingCardChecksumValidator? validator)
            {
                return validator is not null ? new JcbValidator(validator) : Instance;
            }
        }
        
        private sealed class MaestroValidator : BankingCardValidator
        {
            public static MaestroValidator Instance { get; } = new MaestroValidator();
            
            public override BankingCardType Type
            {
                get
                {
                    return BankingCardType.Maestro;
                }
            }
            
            private static readonly Regex pattern = new Regex(@"^(5(018|0[235]|[678])|6(1|39|7|8|9))", RegexOptions.Compiled);
            protected override Regex Pattern
            {
                get
                {
                    return pattern;
                }
            }

            private MaestroValidator()
                : this(BankingCardChecksumValidator.Default)
            {
            }

            private MaestroValidator(IBankingCardChecksumValidator? validator)
                : base(validator)
            {
            }

            protected override Boolean IsLength(String number)
            {
                return number.Length switch
                {
                    12 => true,
                    13 => true,
                    14 => true,
                    15 => true,
                    16 => true,
                    17 => true,
                    18 => true,
                    19 => true,
                    _ => false
                };
            }
            
            protected override MaestroValidator With(IBankingCardChecksumValidator? validator)
            {
                return validator is not null ? new MaestroValidator(validator) : Instance;
            }
        }
        
        private sealed class MasterCardValidator : BankingCardValidator
        {
            public static MasterCardValidator Instance { get; } = new MasterCardValidator();
            
            public override BankingCardType Type
            {
                get
                {
                    return BankingCardType.MasterCard;
                }
            }
            
            private static readonly Regex pattern = new Regex(@"^(5[0-5]|2(2(2[1-9]|[3-9])|[3-6]|7(0|1|20)))", RegexOptions.Compiled);
            protected override Regex Pattern
            {
                get
                {
                    return pattern;
                }
            }

            private MasterCardValidator()
                : this(BankingCardChecksumValidator.Default)
            {
            }

            private MasterCardValidator(IBankingCardChecksumValidator? validator)
                : base(validator)
            {
            }

            protected override Boolean IsLength(String number)
            {
                return number.Length switch
                {
                    16 => true,
                    _ => false
                };
            }
            
            protected override MasterCardValidator With(IBankingCardChecksumValidator? validator)
            {
                return validator is not null ? new MasterCardValidator(validator) : Instance;
            }
        }
        
        private sealed class MirValidator : BankingCardValidator
        {
            public static MirValidator Instance { get; } = new MirValidator();
            
            public override BankingCardType Type
            {
                get
                {
                    return BankingCardType.Mir;
                }
            }
            
            private static readonly Regex pattern = new Regex(@"^220", RegexOptions.Compiled);
            protected override Regex Pattern
            {
                get
                {
                    return pattern;
                }
            }

            private MirValidator()
                : this(BankingCardChecksumValidator.Default)
            {
            }

            private MirValidator(IBankingCardChecksumValidator? validator)
                : base(validator)
            {
            }

            protected override Boolean IsLength(String number)
            {
                return number.Length switch
                {
                    16 => true,
                    19 => true,
                    _ => false
                };
            }
            
            protected override MirValidator With(IBankingCardChecksumValidator? validator)
            {
                return validator is not null ? new MirValidator(validator) : Instance;
            }
        }
        
        private sealed class RalfRingerValidator : BankingCardValidator
        {
            public static RalfRingerValidator Instance { get; } = new RalfRingerValidator();
            
            public override BankingCardType Type
            {
                get
                {
                    return BankingCardType.RalfRinger;
                }
            }
            
            private static readonly Regex pattern = new Regex(@"^20(\d{11})", RegexOptions.Compiled);
            protected override Regex Pattern
            {
                get
                {
                    return pattern;
                }
            }

            private RalfRingerValidator()
                : this(BankingCardChecksumValidator.Default)
            {
            }

            private RalfRingerValidator(IBankingCardChecksumValidator? validator)
                : base(validator)
            {
            }

            protected override Boolean IsLength(String number)
            {
                return number.Length switch
                {
                    13 => true,
                    _ => false
                };
            }
            
            protected override RalfRingerValidator With(IBankingCardChecksumValidator? validator)
            {
                return validator is not null ? new RalfRingerValidator(validator) : Instance;
            }
        }
        
        private sealed class TroyValidator : BankingCardValidator
        {
            public static TroyValidator Instance { get; } = new TroyValidator();
            
            public override BankingCardType Type
            {
                get
                {
                    return BankingCardType.Troy;
                }
            }
            
            private static readonly Regex pattern = new Regex(@"^9(?!(79200|79289))", RegexOptions.Compiled);
            protected override Regex Pattern
            {
                get
                {
                    return pattern;
                }
            }

            private TroyValidator()
                : this(BankingCardChecksumValidator.Default)
            {
            }

            private TroyValidator(IBankingCardChecksumValidator? validator)
                : base(validator)
            {
            }

            protected override Boolean IsLength(String number)
            {
                return number.Length switch
                {
                    16 => true,
                    _ => false
                };
            }
            
            protected override TroyValidator With(IBankingCardChecksumValidator? validator)
            {
                return validator is not null ? new TroyValidator(validator) : Instance;
            }
        }
        
        private sealed class UnionPayValidator : BankingCardValidator
        {
            public static UnionPayValidator Instance { get; } = new UnionPayValidator();
            
            public override BankingCardType Type
            {
                get
                {
                    return BankingCardType.UnionPay;
                }
            }
            
            private static readonly Regex pattern = new Regex(@"^62(?!(2126|2925))", RegexOptions.Compiled);
            protected override Regex Pattern
            {
                get
                {
                    return pattern;
                }
            }

            private UnionPayValidator()
                : this(BankingCardChecksumValidator.Default)
            {
            }

            private UnionPayValidator(IBankingCardChecksumValidator? validator)
                : base(validator)
            {
            }

            protected override Boolean IsLength(String number)
            {
                return number.Length switch
                {
                    16 => true,
                    17 => true,
                    18 => true,
                    19 => true,
                    _ => false
                };
            }
            
            protected override UnionPayValidator With(IBankingCardChecksumValidator? validator)
            {
                return validator is not null ? new UnionPayValidator(validator) : Instance;
            }
        }
        
        private sealed class VisaValidator : BankingCardValidator
        {
            public static VisaValidator Instance { get; } = new VisaValidator();
            
            public override BankingCardType Type
            {
                get
                {
                    return BankingCardType.Visa;
                }
            }
            
            private static readonly Regex pattern = new Regex(@"^4", RegexOptions.Compiled);
            protected override Regex Pattern
            {
                get
                {
                    return pattern;
                }
            }

            private VisaValidator()
                : this(BankingCardChecksumValidator.Default)
            {
            }

            private VisaValidator(IBankingCardChecksumValidator? validator)
                : base(validator)
            {
            }

            protected override Boolean IsLength(String number)
            {
                return number.Length switch
                {
                    13 => true,
                    16 => true,
                    _ => false
                };
            }
            
            protected override VisaValidator With(IBankingCardChecksumValidator? validator)
            {
                return validator is not null ? new VisaValidator(validator) : Instance;
            }
        }
        
        private sealed class VisaElectronValidator : BankingCardValidator
        {
            public static VisaElectronValidator Instance { get; } = new VisaElectronValidator();
            
            public override BankingCardType Type
            {
                get
                {
                    return BankingCardType.VisaElectron;
                }
            }
            
            private static readonly Regex pattern = new Regex(@"^4(026|17500|405|508|844|91[37])", RegexOptions.Compiled);
            protected override Regex Pattern
            {
                get
                {
                    return pattern;
                }
            }

            private VisaElectronValidator()
                : this(BankingCardChecksumValidator.Default)
            {
            }

            private VisaElectronValidator(IBankingCardChecksumValidator? validator)
                : base(validator)
            {
            }

            protected override Boolean IsLength(String number)
            {
                return number.Length switch
                {
                    16 => true,
                    17 => true,
                    _ => false
                };
            }
            
            protected override VisaElectronValidator With(IBankingCardChecksumValidator? validator)
            {
                return validator is not null ? new VisaElectronValidator(validator) : Instance;
            }
        }

        private sealed class YvesRocherValidator : BankingCardValidator
        {
            public static YvesRocherValidator Instance { get; } = new YvesRocherValidator();

            public override BankingCardType Type
            {
                get
                {
                    return BankingCardType.YvesRocher;
                }
            }

            private YvesRocherValidator()
                : this(BankingCardChecksumValidator.Default)
            {
            }

            private YvesRocherValidator(IBankingCardChecksumValidator? validator)
                : base(validator)
            {
            }

            protected override Boolean IsLength(String number)
            {
                return number.Length switch
                {
                    9 => true,
                    _ => false
                };
            }

            protected override YvesRocherValidator With(IBankingCardChecksumValidator? validator)
            {
                return validator is not null ? new YvesRocherValidator(validator) : Instance;
            }
        }
    }
}