// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Behavior.Transactions;
using NetExtender.Configuration.Cryptography.Behavior.Interfaces;
using NetExtender.Configuration.Cryptography.Behavior.Transactions.Interfaces;

namespace NetExtender.Configuration.Cryptography.Behavior.Transactions
{
    public class CryptographyConfigBehaviorTransaction : ConfigBehaviorTransaction, ICryptographyConfigBehaviorTransaction
    {
        public new ICryptographyConfigBehavior Original { get; }
        public new ICryptographyConfigBehavior Transaction { get; }

        public CryptographyConfigBehaviorTransaction(ICryptographyConfigBehavior original, ICryptographyConfigBehavior transaction)
            : base(original, transaction)
        {
            Original = original ?? throw new ArgumentNullException(nameof(original));
            Transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }
    }
}