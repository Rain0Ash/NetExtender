// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Types.Banking.Cards.Interfaces;
using NetExtender.Utilities.Types.Banking.Cards;

namespace NetExtender.Types.Banking.Cards
{
    public readonly struct BankingCard : IEquatable<BankingCard>, IEquatable<String>
    {
        public static implicit operator BankingCardType(BankingCard value)
        {
            return value.Type;
        }
        
        public static implicit operator String(BankingCard value)
        {
            return value.Number ?? String.Empty;
        }

        public static Boolean operator ==(BankingCard first, BankingCard second)
        {
            return first.Type == second.Type && first.Number == second.Number;
        }

        public static Boolean operator !=(BankingCard first, BankingCard second)
        {
            return !(first == second);
        }

        public BankingCardType Type { get; }
        private String? Number { get; }

        public Boolean IsInvalid
        {
            get
            {
                return Number is null || !Type.Validate(Number);
            }
        }

        public BankingCard(UInt64 number)
            : this(BankingCardType.Simple, number)
        {
        }

        public BankingCard(BankingCardType type, UInt64 number)
            : this(type, number.ToString())
        {
        }

        public BankingCard(String number)
            : this(BankingCardType.Simple, number)
        {
        }

        public BankingCard(BankingCardType type, String number)
        {
            Type = type;
            Number = number ?? throw new ArgumentNullException(nameof(number));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Validate(UInt64 number)
        {
            return Validate(number, BankingCardType.Simple);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Validate(String number)
        {
            return Validate(number, BankingCardType.Simple);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Validate(UInt64 number, BankingCardType type)
        {
            return type.Validate(number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Validate(String number, BankingCardType type)
        {
            if (number is null)
            {
                throw new ArgumentNullException(nameof(number));
            }

            return type.Validate(number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Validate(UInt64 number, IBankingCardValidator validator)
        {
            if (validator is null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            return validator.Validate(number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Validate(String number, IBankingCardValidator validator)
        {
            if (number is null)
            {
                throw new ArgumentNullException(nameof(number));
            }

            if (validator is null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            return validator.Validate(number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Generate(IBankingCardFactory factory)
        {
            return Generate(factory, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Generate(IBankingCardFactory factory, IBankingCardFormatter? formatter)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return new BankingCardGenerator(formatter ?? BankingCardFormatter.Default).Generate(factory);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Generate(UInt64 number)
        {
            return Generate(number, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Generate(UInt64 number, IBankingCardFormatter? formatter)
        {
            return new BankingCardGenerator(formatter ?? BankingCardFormatter.Default).Generate(number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Generate(String number)
        {
            return Generate(number, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Generate(String number, IBankingCardFormatter? formatter)
        {
            if (number is null)
            {
                throw new ArgumentNullException(nameof(number));
            }

            return new BankingCardGenerator(formatter ?? BankingCardFormatter.Default).Generate(number);
        }
        
        public Boolean TryFormat(IBankingCardFormatter formatter, [MaybeNullWhen(false)] out String result)
        {
            if (formatter is null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            if (Number is null)
            {
                result = default;
                return false;
            }

            try
            {
                result = formatter.Format(Number);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Type, Number);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                BankingCard other => Equals(other),
                String other => Equals(other),
                BankingCardType type => Type == type,
                _ => false
            };
        }
        
        public Boolean Equals(String? other)
        {
            return Number == other;
        }
        
        public Boolean Equals(BankingCard other)
        {
            return Type == other.Type && Number == other.Number;
        }

        public override String? ToString()
        {
            return Number is not null ? $"({Type}) {Number}" : null;
        }
    }
}