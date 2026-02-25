using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Transactions;
using NetExtender.Types.Transactions;
using NetExtender.Types.Transactions.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.CQRS
{
    public partial interface ICQRS
    {
        public struct Transaction : IStruct<Transaction>, ITransaction
        {
            [Flags]
            public enum Mode : Byte
            {
                NotRequired = 0,
                Self = 1,
                External = 2,
                Nested = Self | External
            }

            public ITransaction? Active { get; }

            private Boolean _commit;
            public Boolean? IsCommit
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    throw new NotImplementedException();
                }
            }

            public TransactionCommitPolicy Policy
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    throw new NotImplementedException();
                }
            }

            public IsolationLevel Isolation { get; }
            public TimeSpan Timeout { get; }

            public Boolean IsEmpty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    throw new NotImplementedException();
                }
            }

            public static Transaction New()
            {
                throw new NotImplementedException();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean Commit()
            {
                _commit = true;
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean Rollback()
            {
                _commit = false;
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Dispose()
            {
                if (Policy.IsCommit(IsCommit))
                {
                    Rollback();
                }
            }

            public ValueTask DisposeAsync()
            {
                throw new NotImplementedException();
            }
        }
    }
}