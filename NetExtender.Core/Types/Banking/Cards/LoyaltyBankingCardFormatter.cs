// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Text;

namespace NetExtender.Types.Banking.Cards
{
    public class LoyaltyBankingCardFormatter : BankingCardFormatter
    {
        protected virtual String ToChar(Int32 value)
        {
            return value switch
            {
                0 => "f",
                1 => "a",
                2 => "e",
                3 => "k",
                4 => "n",
                5 => "o",
                6 => "s",
                7 => "x",
                8 => "y",
                9 => "z",
                _ => throw new ArgumentOutOfRangeException(nameof(value), $"No mapping defined for {value}")
            };
        }

        protected override String Pretty(String number, Int32 length)
        {
            if (number is null)
            {
                throw new ArgumentNullException(nameof(number));
            }

            return base.Pretty(Encode(number).ToUpperInvariant(), length);
        }

        protected String Encode(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            StringBuilder builder = new StringBuilder(value.Length);

            foreach (Char character in value)
            {
                if (character >= '0' && character <= '9')
                {
                    builder.Append(ToChar(character - '0'));
                    continue;
                }

                builder.Append(character);
            }

            return builder.ToString();
        }
    }
}