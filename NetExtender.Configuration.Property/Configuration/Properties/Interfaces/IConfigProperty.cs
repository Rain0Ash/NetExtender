// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Types.Behavior.Interfaces;
using NetExtender.Interfaces;

namespace NetExtender.Configuration.Properties.Interfaces
{
    public interface IConfigProperty<T> : IConfigPropertyValueInfo<T>, IChangeableBehavior<ConfigPropertyOptions>, IReadOnlyValidable<T>, IFormattable
    {
        public new T Value { get; set; }
        public TryConverter<String?, T> Converter { get; }
        public T GetValue();
        public T GetValue(Func<T, Boolean>? predicate);
        public Task<T> GetValueAsync();
        public Task<T> GetValueAsync(CancellationToken token);
        public Task<T> GetValueAsync(Func<T, Boolean>? predicate);
        public Task<T> GetValueAsync(Func<T, Boolean>? predicate, CancellationToken token);
        public Boolean SetValue(T value);
        public Task<Boolean> SetValueAsync(T value);
        public Task<Boolean> SetValueAsync(T value, CancellationToken token);
        public Boolean RemoveValue();
        public Task<Boolean> RemoveValueAsync();
        public Task<Boolean> RemoveValueAsync(CancellationToken token);
        public Boolean KeyExist();
        public Task<Boolean> KeyExistAsync();
        public Task<Boolean> KeyExistAsync(CancellationToken token);
    }

    public interface IConfigProperty : IConfigPropertyValueInfo, IChangeableBehavior<ConfigPropertyOptions>, IFormattable
    {
        public new String? Value { get; set; }
        public String? GetValue();
        public Task<String?> GetValueAsync();
        public Task<String?> GetValueAsync(CancellationToken token);
        public Boolean SetValue(String? value);
        public Task<Boolean> SetValueAsync(String? value);
        public Task<Boolean> SetValueAsync(String? value, CancellationToken token);
        public Boolean RemoveValue();
        public Task<Boolean> RemoveValueAsync();
        public Task<Boolean> RemoveValueAsync(CancellationToken token);
        public Boolean KeyExist();
        public Task<Boolean> KeyExistAsync();
        public Task<Boolean> KeyExistAsync(CancellationToken token);
    }
}