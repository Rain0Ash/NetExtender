// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Behavior.Transactions.Interfaces;
using NetExtender.Configuration.Interfaces;
using NetExtender.Configuration.Transactions.Interfaces;
using NetExtender.Types.Transactions;
using NetExtender.Utilities.Configuration;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Transactions
{
    public class ConfigTransaction : IConfigTransaction
    {
        public IConfig Original { get; }
        public IConfig Transaction { get; }
        public Boolean? IsCommit { get; protected set; }
        public TransactionCommitPolicy Policy { get; init; } = TransactionCommitPolicy.Manual;

        public ConfigTransaction(IConfig original, IConfigBehaviorTransaction transaction)
        {
            Original = original ?? throw new ArgumentNullException(nameof(original));
            Transaction = transaction is not null ? transaction.Transaction.Create() : throw new ArgumentNullException(nameof(transaction));
        }

        public ConfigTransaction(IConfig original, IConfig transaction)
        {
            Original = original ?? throw new ArgumentNullException(nameof(original));
            Transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        public virtual Boolean Commit()
        {
            IsCommit = true;
            Original.Replace(Transaction.GetExistsValues(null));
            return true;
        }

        public virtual Boolean Rollback()
        {
            if (IsCommit == true)
            {
                return false;
            }
            
            IsCommit = false;
            return true;
        }

        public virtual void Dispose()
        {
            if (Policy.IsCommit(IsCommit))
            {
                Rollback();
            }

            Transaction.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}