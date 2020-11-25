﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace NetExtender.Network.Packet
{
    public class BufferManager
    {
        private readonly Int32 _numBytes;
        private Byte[] _buffer;
        private readonly ConcurrentStack<Int32> _freeIndexPool;
        private Int32 _currentIndex;
        private readonly Int32 _bufferSize;

        public BufferManager(Int32 totalBytes, Int32 bufferSize)
        {
            _numBytes = totalBytes;
            _currentIndex = 0;
            _bufferSize = bufferSize;
            _freeIndexPool = new ConcurrentStack<Int32>();
        }

        public void InitBuffer()
        {
            _buffer = new Byte[_numBytes];
        }

        public Boolean SetBuffer(SocketAsyncEventArgs args)
        {
            lock (_freeIndexPool)
            {
                if (_freeIndexPool.Count > 0)
                {
                    if (!_freeIndexPool.TryPop(out Int32 offset))
                    {
                        //Console.WriteLine("TryPop from _freeIndexPool ConcurrentStack failed.");
                    }

                    args.SetBuffer(_buffer, offset, _bufferSize);
                }
                else
                {
                    if (_numBytes - _bufferSize < _currentIndex)
                    {
                        return false;
                    }

                    args.SetBuffer(_buffer, _currentIndex, _bufferSize);
                    _currentIndex += _bufferSize;
                }
            }

            return true;
        }

        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            _freeIndexPool.Push(args.Offset);
            //args.SetBuffer(null, 0, 0);
        }
    }
}