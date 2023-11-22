// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Banking.Cards.Interfaces
{
    public interface IBankingCardGenerator
    {
        public String Generate(IBankingCardFactory factory);
        public String Generate(UInt64 number);
        public String Generate(String number);
    }
}