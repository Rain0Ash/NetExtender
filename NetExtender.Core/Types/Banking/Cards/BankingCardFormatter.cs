// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Banking.Cards.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Banking.Cards
{
    public class BankingCardFormatter : IBankingCardFormatter
    {
        public static IBankingCardFormatter Null { get; } = new NullBankingCardFormatter();
        public static IBankingCardFormatter Default { get; } = new BankingCardFormatter();
        public static IBankingCardFormatter Bank { get; } = new BankingCardFormatter { Length = 16, Delimiter = " " };
        public static IBankingCardFormatter Loyalty { get; } = new LoyaltyBankingCardFormatter();

        public Byte Length { get; protected init; } = 4;
        public Byte Splitting { get; protected init; } = 4;
        public String Delimiter { get; init; } = "-";

        public virtual String Format(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            value = value.PadLeft(Length, '0');
            return Pretty(value, value.Length);
        }

        protected virtual String Pretty(String value, Int32 length)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return length switch
            {
                <= 4 => value,
                <= 6 => Split(value, 3),
                _ => Split(value, Splitting)
            };
        }

        protected String Split(String value, Int32 length)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            String[] chunks = SplitIntoChunks(value.Reverse(), length).Select(chunk => chunk.Reverse()).ToArray();
            return String.Join(Delimiter, chunks);
        }

        private static IEnumerable<String> SplitIntoChunks(String value, Int32 size)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            for (Int32 i = 0; i < value.Length; i += size)
            {
                if (i + size > value.Length)
                {
                    size = value.Length - i;
                }

                yield return value.Substring(i, size);
            }
        }

        private class NullBankingCardFormatter : BankingCardFormatter
        {
            public override String Format(String value)
            {
                return value ?? throw new ArgumentNullException(nameof(value));
            }
        }
    }
}