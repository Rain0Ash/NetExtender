// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetExtender.Types.Parallel
{
    public sealed class ParallelLinqOptions : ParallelOptions
    {
        public ParallelExecutionMode ExecutionMode { get; }

        public ParallelMergeOptions MergeOptions { get; }

        public Boolean Order { get; }

        public ParallelLinqOptions()
            : this(ParallelExecutionMode.Default, ParallelMergeOptions.Default)
        {
        }

        public ParallelLinqOptions(ParallelExecutionMode mode)
            : this(mode, ParallelMergeOptions.Default)
        {
        }
        
        public ParallelLinqOptions(ParallelMergeOptions options)
            : this(ParallelExecutionMode.Default, options)
        {
        }

        public ParallelLinqOptions(ParallelExecutionMode mode, ParallelMergeOptions options)
            : this(mode, options, false)
        {
        }

        public ParallelLinqOptions(ParallelExecutionMode mode, ParallelMergeOptions options, Boolean order)
        {
            ExecutionMode = mode;
            MergeOptions = options;
            Order = order;
        }

        public ParallelOptions ToParallelOptions()
        {
            return new ParallelOptions
            {
                CancellationToken = CancellationToken,
                MaxDegreeOfParallelism = MaxDegreeOfParallelism,
                TaskScheduler = TaskScheduler
            };
        }
    }
}