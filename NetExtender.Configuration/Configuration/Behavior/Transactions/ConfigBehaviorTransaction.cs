// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Behavior.Transactions.Interfaces;
using NetExtender.Types.Transactions;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Behavior.Transactions
{
    public class ConfigBehaviorTransaction : IConfigBehaviorTransaction
    {
        public IConfigBehavior Original { get; }
        public IConfigBehavior Transaction { get; }
        public Boolean? IsCommit { get; protected set; }
        public TransactionCommitPolicy Policy { get; init; } = TransactionCommitPolicy.Manual;

        public ConfigBehaviorTransaction(IConfigBehavior original, IConfigBehavior transaction)
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