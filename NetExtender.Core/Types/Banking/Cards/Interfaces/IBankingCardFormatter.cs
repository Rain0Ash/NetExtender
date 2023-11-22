// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Banking.Cards.Interfaces
{
    public interface IBankingCardFormatter
    {
        public Byte Length { get; }
        public Byte Splitting { get; }
        public String Delimiter { get; }
        
        public String Format(String value);
    }
}