// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.EntityFrameworkCore.Transactions.Interfaces
{
    public interface IDbTransactionManager
    {
        public Boolean HasTransaction { get; }
        
        public Task BeginTransactionAsync();
        public Task BeginTransactionAsync(CancellationToken token);
        public Task CommitTransactionAsync();
        public Task CommitTransactionAsync(CancellationToken token);

        public Task CreateSavepointAsync(String name);
        public Task CreateSavepointAsync(String name, CancellationToken token);
        public Task RollbackToSavepointAsync(String name);
        public Task RollbackToSavepointAsync(String name, CancellationToken token);
        public Task RollbackToSavepointResetChangeTrackerOnLastTransactionAsync(String name);
        public Task RollbackToSavepointResetChangeTrackerOnLastTransactionAsync(String name, CancellationToken token);
    }
}