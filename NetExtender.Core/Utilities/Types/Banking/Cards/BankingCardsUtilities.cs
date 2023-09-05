// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using NetExtender.Initializer.Types.Banking.Cards;
using NetExtender.Initializer.Types.Banking.Cards.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.Utilities.Types.Banking.Cards
{
    public static class BankingCardsUtilities
    {
        public static String ToShortName(this BankingCardType type)
        {
            return type switch
            {
                BankingCardType.Simple => "simple",
                BankingCardType.AmericanExpress => "amex",
                BankingCardType.Dankort => "dankort",
                BankingCardType.DinersClub => "dinersclub",
                BankingCardType.Discovery => "discovery",
                BankingCardType.Forbrugsforeningen => "forbrugsforeningen",
                BankingCardType.HiperCard => "hipercard",
                BankingCardType.Jcb => "jcb",
                BankingCardType.Maestro => "maestro",
                BankingCardType.MasterCard => "mastercard",
                BankingCardType.Mir => "mir",
                BankingCardType.RalfRinger => "ralfringer",
                BankingCardType.Troy => "troy",
                BankingCardType.UnionPay => "unionpay",
                BankingCardType.Visa => "visa",
                BankingCardType.VisaElectron => "visaelectron",
                BankingCardType.YvesRocher => "yvesrocher",
                _ => throw new EnumUndefinedOrNotSupportedException<BankingCardType>(type, nameof(type), null)
            };
        }

        public static IBankingCardValidator Validator(this BankingCardType type)
        {
            return type switch
            {
                BankingCardType.Simple => BankingCardValidator.Simple,
                BankingCardType.AmericanExpress => BankingCardValidator.AmericanExpress,
                BankingCardType.Dankort => BankingCardValidator.Dankort,
                BankingCardType.DinersClub => BankingCardValidator.DinersClub,
                BankingCardType.Discovery => BankingCardValidator.Discovery,
                BankingCardType.Forbrugsforeningen => BankingCardValidator.Forbrugsforeningen,
                BankingCardType.HiperCard => BankingCardValidator.HiperCard,
                BankingCardType.Jcb => BankingCardValidator.Jcb,
                BankingCardType.Maestro => BankingCardValidator.Maestro,
                BankingCardType.MasterCard => BankingCardValidator.MasterCard,
                BankingCardType.Mir => BankingCardValidator.Mir,
                BankingCardType.RalfRinger => BankingCardValidator.RalfRinger,
                BankingCardType.Troy => BankingCardValidator.Troy,
                BankingCardType.UnionPay => BankingCardValidator.UnionPay,
                BankingCardType.Visa => BankingCardValidator.Visa,
                BankingCardType.VisaElectron => BankingCardValidator.VisaElectron,
                BankingCardType.YvesRocher => BankingCardValidator.YvesRocher,
                _ => throw new EnumUndefinedOrNotSupportedException<BankingCardType>(type, nameof(type), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Validate(this BankingCardType type, UInt64 number)
        {
            return type.Validator().Validate(number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Validate(this BankingCardType type, String number)
        {
            if (number is null)
            {
                throw new ArgumentNullException(nameof(number));
            }

            return type.Validator().Validate(number);
        }
    }
}