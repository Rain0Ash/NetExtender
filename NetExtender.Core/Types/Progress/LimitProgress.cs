// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Progress.Interface;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Progress
{
    public readonly struct LimitProgressState<T> where T : IComparable<T>
    {
        public static implicit operator T(LimitProgressState<T> state)
        {
            return state.Value;
        }

        public T Value { get; }
        public T Maximum { get; }

        public Boolean IsMinimum
        {
            get
            {
                return Value.CompareTo(default) == 0;
            }
        }

        public Boolean IsMaximum
        {
            get
            {
                return Value.CompareTo(Maximum) >= 0;
            }
        }

        public LimitProgressState(T value, T maximum)
        {
            Value = value;
            Maximum = maximum;
        }
    }

    public class NumericLimitProgress<T> : LimitProgressAbstraction<T> where T : unmanaged, IConvertible, IComparable<T>
    {
        public NumericLimitProgress(T value, T maximum)
            : base(value, maximum, MathUnsafe.Increment)
        {
        }
    }

    public abstract class LimitProgressAbstraction<T> : Progress<LimitProgressState<T>>, ILimitProgress<T> where T : IComparable<T>
    {
        public T Value { get; protected set; }
        public T Maximum { get; }

        public Boolean IsLimit
        {
            get
            {
                return Value.CompareTo(Maximum) >= 0;
            }
        }

        protected Func<T, T> Handler { get; }

        protected LimitProgressAbstraction(T value, T maximum, Func<T, T> next)
        {
            Value = value;
            Maximum = maximum;
            Handler = next ?? throw new ArgumentNullException(nameof(next));
        }

        Boolean ILimitProgress<T>.Next()
        {
            return OnNext();
        }

        protected virtual Boolean OnNext()
        {
            if (Value.CompareTo(Maximum) == 0)
            {
                return false;
            }

            ((IProgress<T>) this).Report(Handler(Value));
            return true;
        }

        void IProgress<T>.Report(T value)
        {
            OnReport(value);
        }

        protected virtual void OnReport(T value)
        {
            if (value.CompareTo(Maximum) > 0)
            {
                return;
            }

            Value = value;
            base.OnReport(new LimitProgressState<T>(Value, Maximum));
        }
    }
}