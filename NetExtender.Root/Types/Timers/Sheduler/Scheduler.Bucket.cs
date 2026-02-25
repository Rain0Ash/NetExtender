using System;
using System.Collections.Generic;

namespace NetExtender.Types.Timers
{
    public partial class Scheduler
    {
        internal sealed class Bucket
        {
            private Timeout? _head;
            private Timeout? _tail;

            public void Add(Timeout timeout)
            {
                timeout.Bucket = this;

                if (_head is null)
                {
                    _head = _tail = timeout;
                    return;
                }

                _tail!.Next = timeout;
                timeout.Previous = _tail;
                _tail = timeout;
            }

            private Timeout? Poll()
            {
                Timeout? head = _head;
                if (head is null)
                {
                    return null;
                }

                Timeout? next = head.Next;
                if (next is null)
                {
                    _tail = _head = null;
                }
                else
                {
                    _head = next;
                    next.Previous = null;
                }

                head.Next = null;
                head.Previous = null;
                head.Bucket = null;
                return head;
            }

            internal void Expire(Int64 deadline)
            {
                Timeout? timeout = _head;

                while (timeout is not null)
                {
                    Timeout? next = timeout.Next;

                    if (timeout.IsCancelled)
                    {
                        timeout = Remove(timeout);
                        continue;
                    }

                    if (timeout.Rounds > 0)
                    {
                        timeout.Rounds--;
                        timeout = next;
                        continue;
                    }

                    if (timeout.Deadline > deadline)
                    {
                        timeout = next;
                        continue;
                    }

                    timeout = Remove(timeout);
                    timeout?.Expire();
                }
            }

            internal Timeout? Remove(Timeout timeout)
            {
                Timeout? next = timeout.Next;

                if (timeout.Previous is not null)
                {
                    timeout.Previous.Next = next;
                }

                if (timeout.Next is not null)
                {
                    timeout.Next.Previous = timeout.Previous;
                }

                if (timeout == _head)
                {
                    if (timeout == _tail)
                    {
                        _tail = null;
                        _head = null;
                    }
                    else
                    {
                        _head = next;
                    }
                }
                else if (timeout == _tail)
                {
                    _tail = timeout.Previous;
                }

                timeout.Previous = null;
                timeout.Next = null;
                timeout.Bucket = null;
                timeout.Scheduler.DecreasePendingTimeouts();
                return next;
            }

            internal void Clear(ISet<Timeout> set)
            {
                while (Poll() is { } timeout)
                {
                    if (timeout.IsActive)
                    {
                        set.Add(timeout);
                    }
                }
            }
        }
    }
}