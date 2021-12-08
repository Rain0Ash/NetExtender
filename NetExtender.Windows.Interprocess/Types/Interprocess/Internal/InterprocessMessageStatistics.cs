// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;

namespace NetExtender.Types.Interprocess
{
    internal class InterprocessMessageStatistics
    {
        private Int64 _published;
        public Int64 Published
        {
            get
            {
                return _published;
            }
            private set
            {
                _published = value;
            }
        }

        private Int64 _received;
        public Int64 Received
        {
            get
            {
                return _received;
            }
            private set
            {
                _received = value;
            }
        }

        private Int32 _handlers;
        public Int32 Handlers
        {
            get
            {
                return _handlers;
            }
            set
            {
                _handlers = value;
            }
        }

        private Int32 _receivers;
        public Int32 Receivers
        {
            get
            {
                return _receivers;
            }
            set
            {
                _receivers = value;
            }
        }

        public Int64 PublishedIncrement()
        {
            Interlocked.Increment(ref _published);
            return Published;
        }

        public Int64 PublishedDecrement()
        {
            Interlocked.Decrement(ref _published);
            return Published;
        }

        public Int64 ReceivedIncrement()
        {
            Interlocked.Increment(ref _received);
            return Received;
        }

        public Int64 ReceivedDecrement()
        {
            Interlocked.Decrement(ref _received);
            return Received;
        }

        public Int32 HandlersIncrement()
        {
            Interlocked.Increment(ref _handlers);
            return Handlers;
        }

        public Int32 HandlersDecrement()
        {
            Interlocked.Decrement(ref _handlers);
            return Handlers;
        }

        public Int32 ReceiversIncrement()
        {
            Interlocked.Increment(ref _receivers);
            return Receivers;
        }

        public Int32 ReceiversDecrement()
        {
            Interlocked.Decrement(ref _receivers);
            return Receivers;
        }

        public void Reset()
        {
            Published = 0;
            Received = 0;
        }
    }
}