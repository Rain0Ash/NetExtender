using System;
using System.Threading.Tasks;
using System.Transactions;
using NetExtender.Types.Transactions.Interfaces;

/*namespace NetExtender.Types.Transactions
{
    public abstract class Transaction : System.Transactions., ITransaction
    {
        private class None : Transaction
        {
        }

        private class Wrapper : Transaction
        {
            private Transaction Transaction { get; }

            public Wrapper(Transaction transaction)
            {
                Transaction = transaction;
            }
        }

        public Boolean? IsCommit
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public TransactionCommitPolicy Policy
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IsolationLevel Isolation
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public TimeSpan Timeout
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Boolean Commit()
        {
            throw new NotImplementedException();
        }

        public Boolean Rollback()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}*/