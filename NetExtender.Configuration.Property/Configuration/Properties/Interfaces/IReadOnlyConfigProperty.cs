// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Interfaces;

namespace NetExtender.Configuration.Properties.Interfaces
{
    public interface IReadOnlyConfigProperty<T> : IConfigPropertyValueInfo<T>, IReadOnlyValidable<T>, IFormattable
    {
        public TryConverter<String?, T> Converter { get; }
        public T GetValue();
        public T GetValue(Func<T, Boolean>? predicate);
        public Task<T> GetValueAsync();
        public Task<T> GetValueAsync(CancellationToken token);
        public Task<T> GetValueAsync(Func<T, Boolean>? predicate);
        public Task<T> GetValueAsync(Func<T, Boolean>? predicate, CancellationToken token);
        public Boolean KeyExist();
        public Task<Boolean> KeyExistAsync();
        public Task<Boolean> KeyExistAsync(CancellationToken token);
        
        public Boolean Read();
        public Task<Boolean> ReadAsync();
        public Task<Boolean> ReadAsync(CancellationToken token);
    }

    public interface IReadOnlyConfigProperty : IConfigPropertyValueInfo, IFormattable
    {
        public String? GetValue();
        public Task<String?> GetValueAsync();
        public Task<String?> GetValueAsync(CancellationToken token);
        public Boolean KeyExist();
        public Task<Boolean> KeyExistAsync();
        public Task<Boolean> KeyExistAsync(CancellationToken token);
        
        public Boolean Read();
        public Task<Boolean> ReadAsync();
        public Task<Boolean> ReadAsync(CancellationToken token);
    }
}