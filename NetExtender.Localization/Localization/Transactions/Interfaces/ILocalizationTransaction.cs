// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Configuration.Transactions.Interfaces;
using NetExtender.Localization.Interfaces;

namespace NetExtender.Localization.Transactions.Interfaces
{
    public interface ILocalizationTransaction : IConfigTransaction
    {
        public new ILocalizationConfig Transaction { get; }
    }
}