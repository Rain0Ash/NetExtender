// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Cryptography.Behavior.Transactions.Interfaces;
using NetExtender.Configuration.Cryptography.Interfaces;
using NetExtender.Configuration.Cryptography.Transactions.Interfaces;
using NetExtender.Configuration.Cryptography.Utilities;
using NetExtender.Configuration.Transactions;

namespace NetExtender.Configuration.Cryptography.Transactions
{
    public class CryptographyConfigTransaction : ConfigTransaction, ICryptographyConfigTransaction
    {
        public new ICryptographyConfig Original { get; }
        public new ICryptographyConfig Transaction { get; }

        public CryptographyConfigTransaction(ICryptographyConfig original, ICryptographyConfigBehaviorTransaction transaction)
            : base(original, transaction)
        {
            Original = original ?? throw new ArgumentNullException(nameof(original));
            Transaction = transaction is not null ? transaction.Transaction.Create() : throw new ArgumentNullException(nameof(transaction));
        }

        public CryptographyConfigTransaction(ICryptographyConfig original, ICryptographyConfig transaction)
            : base(original, transaction)
        {
            Original = original ?? throw new ArgumentNullException(nameof(original));
            Transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }
    }
}