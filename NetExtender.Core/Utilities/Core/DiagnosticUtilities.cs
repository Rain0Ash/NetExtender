// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;

namespace NetExtender.Utilities.Core
{
    public static class DiagnosticUtilities
    {
        public static TimeSpan ExecutionTime(this Action process)
        {
            return ExecutionTime(process, 1);
        }

        public static TimeSpan ExecutionTime(this Action process, Int32 repeat)
        {
            if (process is null)
            {
                throw new ArgumentNullException(nameof(process));
            }

            if (repeat <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(repeat), repeat, "Repeat must be greater than 0.");
            }

            Stopwatch watch = Stopwatch.StartNew();
            for (Int32 count = 0; count < repeat; count++)
            {
                process.Invoke();
            }

            watch.Stop();
            return watch.Elapsed;
        }

        public static TimeSpan ExecutionTime<T>(this Func<T> process)
        {
            return ExecutionTime(process, 1);
        }

        public static TimeSpan ExecutionTime<T>(this Func<T> process, Int32 repeat)
        {
            if (process is null)
            {
                throw new ArgumentNullException(nameof(process));
            }

            if (repeat <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(repeat), repeat, "Repeat must be greater than 0.");
            }

            Stopwatch watch = Stopwatch.StartNew();
            for (Int32 count = 0; count < repeat; count++)
            {
                process.Invoke();
            }

            watch.Stop();
            return watch.Elapsed;
        }
    }
}