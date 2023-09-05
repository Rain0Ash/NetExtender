// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Initializer.Types.Banking.Cards.Interfaces;

namespace NetExtender.Initializer.Types.Banking.Cards
{
    public class BankingCardChecksumValidator : IBankingCardChecksumValidator
    {
        public static IBankingCardChecksumValidator Default { get; } = new BankingCardChecksumValidator();
        
        public IBankingCardParser Parser { get; }

        public BankingCardChecksumValidator()
            : this(null)
        {
        }

        public BankingCardChecksumValidator(IBankingCardParser? parser)
        {
            Parser = parser ?? BankingCardParser.Default;
        }

        public virtual Boolean Validate(String number)
        {
            if (number is null)
            {
                throw new ArgumentNullException(nameof(number));
            }
            
            if (number.Length <= 1 || !Parser.TryParse(number, out Int32 checksum, out Int32 control))
            {
                return false;
            }

            return checksum % 10 == 0 && checksum - control == number[^1] - '0';
        }
    }
}