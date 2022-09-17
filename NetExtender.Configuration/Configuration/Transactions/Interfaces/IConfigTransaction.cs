// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Configuration.Interfaces;
using NetExtender.Types.Transactions.Interfaces;

namespace NetExtender.Configuration.Transactions.Interfaces
{
    public interface IConfigTransaction : ITransaction
    {
        public IConfig Transaction { get; }
    }
}