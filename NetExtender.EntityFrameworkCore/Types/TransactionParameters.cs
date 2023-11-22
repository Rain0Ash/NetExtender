// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Transactions;

namespace NetExtender.EntityFrameworkCore
{
    public readonly struct TransactionParameters
    {
        public static TransactionParameters Serializable { get; } = new TransactionParameters(IsolationLevel.Serializable);
        public static TransactionParameters RepeatableRead { get; } = new TransactionParameters(IsolationLevel.RepeatableRead);
        public static TransactionParameters ReadCommitted { get; } = new TransactionParameters(IsolationLevel.ReadCommitted);
        public static TransactionParameters ReadUncommitted { get; } = new TransactionParameters(IsolationLevel.ReadUncommitted);
        public static TransactionParameters Snapshot { get; } = new TransactionParameters(IsolationLevel.Snapshot);
        public static TransactionParameters Chaos { get; } = new TransactionParameters(IsolationLevel.Chaos);
        public static TransactionParameters Unspecified { get; } = new TransactionParameters(IsolationLevel.Unspecified);

        public static implicit operator TransactionParameters(TransactionOptions options)
        {
            return new TransactionParameters(options);
        }

        public static implicit operator TransactionOptions(TransactionParameters parameters)
        {
            return new TransactionOptions {Timeout = parameters.Timeout, IsolationLevel = parameters.IsolationLevel};
        }

        public static Boolean operator ==(TransactionParameters first, TransactionParameters second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(TransactionParameters first, TransactionParameters second)
        {
            return !first.Equals(second);
        }

        public static Boolean operator ==(TransactionParameters first, TransactionOptions second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(TransactionParameters first, TransactionOptions second)
        {
            return !first.Equals(second);
        }

        public static Boolean operator ==(TransactionOptions first, TransactionParameters second)
        {
            return second == first;
        }

        public static Boolean operator !=(TransactionOptions first, TransactionParameters second)
        {
            return second != first;
        }

        public TimeSpan Timeout { get; init; }

        public IsolationLevel IsolationLevel { get; init; }

        public TransactionParameters(TransactionOptions options)
            : this(options.Timeout, options.IsolationLevel)
        {
        }

        public TransactionParameters(TimeSpan timeout)
            : this(timeout, default)
        {
        }

        public TransactionParameters(IsolationLevel isolation)
            : this(default, isolation)
        {
        }

        public TransactionParameters(TimeSpan timeout, IsolationLevel isolation)
        {
            Timeout = timeout;
            IsolationLevel = isolation;
        }

        public TransactionParameters With(TimeSpan timeout)
        {
            return new TransactionParameters(timeout, IsolationLevel);
        }

        public TransactionParameters With(IsolationLevel isolation)
        {
            return new TransactionParameters(Timeout, isolation);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Timeout, IsolationLevel);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is TransactionParameters parameters && Equals(parameters) || obj is TransactionOptions options && Equals(options);
        }

        public Boolean Equals(TransactionOptions other)
        {
            return Equals(other.Timeout, other.IsolationLevel);
        }

        public Boolean Equals(TransactionParameters other)
        {
            return Equals(other.Timeout, other.IsolationLevel);
        }

        public Boolean Equals(TimeSpan timeout, IsolationLevel isolation)
        {
            return Timeout == timeout && IsolationLevel == isolation;
        }

        public override String ToString()
        {
            return $"{nameof(Timeout)}: {Timeout}, {nameof(IsolationLevel)}: {IsolationLevel}";
        }
    }
}