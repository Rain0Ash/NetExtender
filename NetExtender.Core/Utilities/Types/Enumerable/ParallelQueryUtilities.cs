// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetExtender.Core.Types.Parallel;

namespace NetExtender.Utilities.Types
{
    public static class ParallelQueryUtilities
    {
        public static ParallelQuery<TSource> AsParallel<TSource>(this IEnumerable<TSource> source, ParallelLinqOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (options.TaskScheduler != null && options.TaskScheduler != TaskScheduler.Default)
            {
                throw new ArgumentException("Parallel LINQ only supports the default TaskScheduler.");
            }
	
            ParallelQuery<TSource> result = source.AsParallel();
	
            if (options.Order)
            {
                result = result.AsOrdered();
            }
            
            if (options.CancellationToken.CanBeCanceled)
            {
                result = result.WithCancellation(options.CancellationToken);
            }
            
            if (options.MaxDegreeOfParallelism >= 1)
            {
                result = result.WithDegreeOfParallelism(options.MaxDegreeOfParallelism);
            }
            
            if (options.ExecutionMode != ParallelExecutionMode.Default)
            {
                result = result.WithExecutionMode(options.ExecutionMode);
            }
            
            if (options.MergeOptions != ParallelMergeOptions.Default)
            {
                result = result.WithMergeOptions(options.MergeOptions);
            }
            
            return result;
        }
        
        public static void ToProducerConsumerCollection<TSource>(this ParallelQuery<TSource> source, IProducerConsumerCollection<TSource> collection)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            source.ForAll(item => collection.TryAdd(item));
        }
    }
}