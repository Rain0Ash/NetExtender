// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace NetExtender.Types.Reflection
{
    public readonly struct StackOverflowCounter : IEquatableStruct<StackOverflowCounter>, IDisposable
    {
        public static Boolean operator ==(StackOverflowCounter first, StackOverflowCounter second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator !=(StackOverflowCounter first, StackOverflowCounter second)
        {
            return !(first == second);
        }
        
        public static implicit operator Boolean(StackOverflowCounter value)
        {
            return value.IsLimit;
        }
        
        private readonly Stack? _stack;
        
        public String? Identifier
        {
            get
            {
                return _stack?.Identifier;
            }
        }
        
        public UInt64 Counter
        {
            get
            {
                return _stack?.Counter ?? 0;
            }
        }
        
        public UInt64 Limit { get; init; } = UInt64.MaxValue;
        
        public Boolean IsLimit
        {
            get
            {
                return Counter > Limit;
            }
        }
        
        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _stack is null;
            }
        }
        
        public StackOverflowCounter(String identifier)
        {
            _stack = Stack.Get(identifier);
        }
        
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(_stack, Limit);
        }
        
        public override Boolean Equals(Object? other)
        {
            return other is StackOverflowCounter stack && Equals(stack);
        }
        
        public Boolean Equals(StackOverflowCounter other)
        {
            return Equals(_stack, other._stack);
        }
        
        public override String? ToString()
        {
            return _stack?.ToString();
        }
        
        public void Dispose()
        {
            _stack?.Dispose();
        }
        
        private sealed record Stack : IDisposable
        {
            [ThreadStatic]
            private static Dictionary<String, Stack>? storage;
            private static Dictionary<String, Stack> Storage
            {
                get
                {
                    return storage ??= new Dictionary<String, Stack>();
                }
            }
            
            public Thread Thread { get; }
            public String Identifier { get; }
            
            private UInt64 _counter;
            public UInt64 Counter
            {
                get
                {
                    return _counter;
                }
            }
            
            private Stack(String identifier)
            {
                Thread = Thread.CurrentThread;
                Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
                _counter = 0;
            }
            
            [return: NotNullIfNotNull("identifier")]
            public static Stack? Get(String? identifier)
            {
                if (identifier is null)
                {
                    return null;
                }
                
                if (!Storage.TryGetValue(identifier, out Stack? stack))
                {
                    Storage[identifier] = stack = new Stack(identifier);
                }
                
                ++stack._counter;
                return stack;
            }
            
            public void Dispose()
            {
                if (Thread != Thread.CurrentThread)
                {
                    throw new InvalidOperationException();
                }
                
                switch (_counter)
                {
                    case > 1:
                        --_counter;
                        return;
                    case 1:
                        --_counter;
                        goto default;
                    default:
                        IDictionary<String, Stack> storage = Storage;
                        storage.Remove(new KeyValuePair<String, Stack>(Identifier, this));
                        break;
                }
            }
        }
    }
}