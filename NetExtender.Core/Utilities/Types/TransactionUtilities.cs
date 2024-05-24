using System;
using System.Runtime.CompilerServices;
using NetExtender.Types.Transactions;
using NetExtender.Types.Exceptions;

namespace NetExtender.Utilities.Types
{
    public static class TransactionUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsCommit(this TransactionCommitPolicy policy, Boolean? commit)
        {
            return policy switch
            {
                TransactionCommitPolicy.None => commit is not false,
                TransactionCommitPolicy.Required => commit is true,
                TransactionCommitPolicy.Manual => commit == null,
                _ => throw new EnumUndefinedOrNotSupportedException<TransactionCommitPolicy>(policy, nameof(policy), null)
            };
        }
    }
}