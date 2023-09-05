// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Initializer.Types.Banking.Cards.Interfaces
{
    public interface IBankingCardValidator
    {
        public BankingCardType Type { get; }
        
        public Boolean Validate(UInt64 number);
        public Boolean Validate(String number);
        public IBankingCardValidator With(IBankingCardChecksumValidator? validator);
    }
}