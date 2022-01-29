// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com


using NetExtender.Configuration.Cryptography.Interfaces;
using NetExtender.Configuration.Transactions.Interfaces;

namespace NetExtender.Configuration.Cryptography.Transactions.Interfaces
{
    public interface ICryptographyConfigTransaction : IConfigTransaction
    {
        public new ICryptographyConfig Transaction { get; }
    }
}