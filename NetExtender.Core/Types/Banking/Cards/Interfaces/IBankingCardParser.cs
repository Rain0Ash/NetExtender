// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Banking.Cards.Interfaces
{
    public interface IBankingCardParser
    {
        public Boolean TryParse(String number, out Int32 checksum);
        public Boolean TryParse(String number, out Int32 checksum, out Int32 control);
    }
}