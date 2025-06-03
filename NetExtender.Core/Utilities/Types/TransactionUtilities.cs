// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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