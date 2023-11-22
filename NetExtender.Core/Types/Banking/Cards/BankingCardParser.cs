// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Banking.Cards.Interfaces;

namespace NetExtender.Types.Banking.Cards
{
    public class BankingCardParser : IBankingCardParser
    {
        public static IBankingCardParser Default { get; } = new BankingCardParser();

        public Boolean TryParse(String number, out Int32 checksum)
        {
            return TryParse(number, out checksum, out _);
        }

        public virtual Boolean TryParse(String number, out Int32 checksum, out Int32 control)
        {
            if (number is null)
            {
                throw new ArgumentNullException(nameof(number));
            }

            try
            {
                checksum = 0;
                Int32 length = number.Length - 1;

                for (Int32 i = length; i >= 0; i--)
                {
                    Int32 digit = number[i] - '0';

                    if ((length - i) % 2 == 1)
                    {
                        digit *= 2;
                    }

                    checksum += digit <= 9 ? digit : digit - 9;
                }

                control = checksum - number[length] + '0';
                return true;
            }
            catch (Exception)
            {
                checksum = default;
                control = default;
                return false;
            }
        }
    }
}