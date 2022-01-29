// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Behavior.Transactions.Interfaces;

namespace NetExtender.Configuration.Behavior.Transactions
{
    public class ConfigBehaviorTransaction : IConfigBehaviorTransaction
    {
        public IConfigBehavior Original { get; }
        public IConfigBehavior Transaction { get; }
        protected Boolean? IsCommit { get; set; }

        public ConfigBehaviorTransaction(IConfigBehavior original, IConfigBehavior transaction)
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