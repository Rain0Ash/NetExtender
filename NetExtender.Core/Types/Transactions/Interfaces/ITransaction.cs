// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Transactions.Interfaces
{
    public interface ITransaction : IDisposable
    {
        public Boolean? IsCommit { get; }
        public TransactionCommitPolicy Policy { get; }
        
        public Boolean Commit();
        public Boolean Rollback();
    }
}