// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Progress.Interface;

namespace NetExtender.Types.Progress
{
    public class ConditionalProgress<T> : Progress<T>
    {
        private Func<T, Boolean> Condition { get; }

        public ConditionalProgress(Func<T, Boolean> condition, Action<T> handler)
            : base(handler)
        {
            Condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }

        protected override void OnReport(T value)
        {
            if (!Condition(value))
            {
                return;
            }
            
            base.OnReport(value);
        }
    }
    
    public class ConditionalProgress<T1, T2> : ConditionalProgress<(T1, T2)>, IProgress<T1, T2>
    {
        public ConditionalProgress(Func<(T1, T2), Boolean> condition, Action<(T1, T2)> handler)
            : base(condition, handler)
        {
        }
    }
    
    public class ConditionalProgress<T1, T2, T3> : ConditionalProgress<(T1, T2, T3)>, IProgress<T1, T2, T3>
    {
        public ConditionalProgress(Func<(T1, T2, T3), Boolean> condition, Action<(T1, T2, T3)> handler)
            : base(condition, handler)
        {
        }
    }
    
    public class ConditionalProgress<T1, T2, T3, T4> : ConditionalProgress<(T1, T2, T3, T4)>, IProgress<T1, T2, T3, T4>
    {
        public ConditionalProgress(Func<(T1, T2, T3, T4), Boolean> condition, Action<(T1, T2, T3, T4)> handler)
            : base(condition, handler)
        {
        }
    }
    
    public class ConditionalProgress<T1, T2, T3, T4, T5> : ConditionalProgress<(T1, T2, T3, T4, T5)>, IProgress<T1, T2, T3, T4, T5>
    {
        public ConditionalProgress(Func<(T1, T2, T3, T4, T5), Boolean> condition, Action<(T1, T2, T3, T4, T5)> handler)
            : base(condition, handler)
        {
        }
    }
}