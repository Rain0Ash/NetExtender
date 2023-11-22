// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Banking.Cards.Interfaces;

namespace NetExtender.Types.Banking.Cards
{
    public class BankingCardGenerator : IBankingCardGenerator
    {
        protected IBankingCardParser Parser { get; }
        protected IBankingCardFormatter Formatter { get; }

        public BankingCardGenerator(IBankingCardFormatter formatter)
            : this(null, formatter)
        {
        }

        public BankingCardGenerator(IBankingCardParser? parser, IBankingCardFormatter formatter)
        {
            Formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
            Parser = parser ?? BankingCardParser.Default;
        }

        public String Generate(IBankingCardFactory factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return Generate(factory.Get());
        }

        public String Generate(UInt64 number)
        {
            return Generate(number.ToString());
        }

        public virtual String Generate(String number)
        {
            if (number is null)
            {
                throw new ArgumentNullException(nameof(number));
            }

            if (!Parser.TryParse(number + "0", out Int32 checksum))
            {
                throw new InvalidOperationException();
            }
            
            return checksum % 10 != 0 ? Formatter.Format(number + (10 - checksum % 10)) : Formatter.Format(number + "0");
        }
    }
}