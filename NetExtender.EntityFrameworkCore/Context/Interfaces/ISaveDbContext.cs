// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.DependencyInjection.Context.Interfaces
{
    public interface ISaveDbContext
    {
        public Task<Int32> SaveChangesAsync(CancellationToken token = default);
        public Task<Int32> SaveChangesDeferEventsAsync(CancellationToken token = default);
    }
}