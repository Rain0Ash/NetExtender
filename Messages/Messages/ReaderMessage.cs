// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using NetExtender.Messages.Interfaces;

namespace NetExtender.Messages
{
    [Flags]
    public enum ReaderMessageOptions
    {
        None = 0,
        External = 1,
        Protocol = 2
    }
    
    public class ReaderMessage<T> : IReaderMessage<T>
    {
        protected const ReaderMessageOptions DefaultOptions = ReaderMessageOptions.None;

        private readonly ImmutableList<T> _args;
        public Int32 Count
        {
            get
            {
                return _args.Count;
            }
        }

        public T Value
        {
            get
            {
                return _args[0];
            }
        }

        public ReaderMessageOptions Options { get; }

        public Boolean IsExternal
        {
            get
            {
                return Options.HasFlag(ReaderMessageOptions.External);
            }
        }
        
        public Boolean IsProtocol
        {
            get
            {
                return Options.HasFlag(ReaderMessageOptions.Protocol);
            }
        }

        public IReaderMessage<T> Next
        {
            get
            {
                return _args.Count <= 0 ? this : new ReaderMessage<T>(_args.RemoveAt(0), Options);
            }
        }

        public ReaderMessage(T arg, ReaderMessageOptions options = DefaultOptions)
            : this(new List<T>{arg}.ToImmutableList(), options)
        {
        }
        
        protected ReaderMessage(ImmutableList<T> args, ReaderMessageOptions options)
        {
            _args = args;
            Options = options;
        }

        public Boolean Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public Int32 IndexOf(T item)
        {
            return _args.IndexOf(item);
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return _args.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public T this[Int32 index]
        {
            get
            {
                return _args[index];
            }
        }
    }
}