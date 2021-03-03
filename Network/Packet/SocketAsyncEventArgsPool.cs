// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace NetExtender.Network.Packet
{
    public class SocketAsyncEventArgsPool
    {
        private readonly ConcurrentStack<SocketAsyncEventArgs> _pool;

        public Int32 Count
        {
            get
            {
                return _pool.Count;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return _pool.IsEmpty;
            }
        }

        public SocketAsyncEventArgsPool()
        {
            _pool = new ConcurrentStack<SocketAsyncEventArgs>();
        }

        public void Push(SocketAsyncEventArgs item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _pool.Push(item);
        }

        public SocketAsyncEventArgs Pop()
        {
            if (!_pool.TryPop(out SocketAsyncEventArgs output))
            {
                //_log.Error("TryPop from SocketAsyncEventArgs ConcurrentStack failed.");
            }

            return output;
        }
    }
}