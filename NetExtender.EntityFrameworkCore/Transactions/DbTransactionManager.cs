// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NetExtender.DependencyInjection.Context.Interfaces;
using NetExtender.EntityFrameworkCore.Transactions.Interfaces;

namespace NetExtender.EntityFrameworkCore.Transactions
{
    public class DbTransactionManager<TLazy, TEager> : IDbTransactionManager where TLazy : DbContext, ILazyDbContext where TEager : DbContext, IEagerDbContext
    {
        public Boolean HasTransaction
        {
            get
            {
                return _counter > 0;
            }
        }

        protected TLazy Lazy { get; }
        protected TEager Eager { get; }
        protected IDbContextTransaction? Transaction { get; set; }
        protected Int32 _counter;

        public DbTransactionManager(TLazy lazy, TEager eager)
        {
            Lazy = lazy ?? throw new ArgumentNullException(nameof(lazy));
            Eager = eager ?? throw new ArgumentNullException(nameof(eager));
        }

        public Task BeginTransactionAsync()
        {
            return BeginTransactionAsync(default);
        }

        public virtual async Task BeginTransactionAsync(CancellationToken token)
        {
            if (_counter++ == 0)
            {
                Transaction = await Lazy.Database.BeginTransactionAsync(token).ConfigureAwait(false);

                Eager.Database.SetDbConnection(Lazy.Database.GetDbConnection());
                Transaction = (await Eager.Database.UseTransactionAsync(Transaction.GetDbTransaction(), token))!;
            }
        }

        public Task CommitTransactionAsync()
        {
            return CommitTransactionAsync(default);
        }

        public virtual async Task CommitTransactionAsync(CancellationToken token)
        {
            if (Transaction is null)
            {
                return;
            }
            
            if (--_counter == 0)
            {
                await Lazy.Database.CommitTransactionAsync(token);
                await Transaction.DisposeAsync();
            }
        }

        public Task CreateSavepointAsync(String name)
        {
            return CreateSavepointAsync(name, default);
        }

        public virtual async Task CreateSavepointAsync(String name, CancellationToken token)
        {
            if (Transaction is not null)
            {
                await Transaction.CreateSavepointAsync(name, token);
            }
        }

        public Task RollbackToSavepointAsync(String name)
        {
            return RollbackToSavepointAsync(name, new CancellationToken());
        }

        public virtual async Task RollbackToSavepointAsync(String name, CancellationToken token)
        {
            if (Transaction is not null)
            {
                await Transaction.RollbackToSavepointAsync(name, token);
            }
        }

        public Task RollbackToSavepointResetChangeTrackerOnLastTransactionAsync(String name)
        {
            return RollbackToSavepointResetChangeTrackerOnLastTransactionAsync(name, new CancellationToken());
        }

        public virtual async Task RollbackToSavepointResetChangeTrackerOnLastTransactionAsync(String name, CancellationToken token)
        {
            if (Transaction is not null)
            {
                await Transaction.RollbackToSavepointAsync(name, token);
            }

            if (_counter == 1)
            {
                Lazy.ChangeTracker.Clear();
                Eager.ChangeTracker.Clear();
            }
        }
    }
}