// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Behavior.Transactions.Interfaces;
using NetExtender.Configuration.Interfaces;
using NetExtender.Configuration.Transactions.Interfaces;
using NetExtender.Utilities.Configuration;

namespace NetExtender.Configuration.Transactions
{
    public class ConfigTransaction : IConfigTransaction
    {
        public IConfig Original { get; }
        public IConfig Transaction { get; }
        protected Boolean? IsCommit { get; set; }

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

        public virtual void Commit()
        {
            IsCommit = true;
            Original.Replace(Transaction.GetExistsValues(null));
        }

        public virtual void Rollback()
        {
            IsCommit = false;
        }

        public virtual void Dispose()
        {
            if (IsCommit is null)
            {
                Rollback();
            }

            Transaction.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}