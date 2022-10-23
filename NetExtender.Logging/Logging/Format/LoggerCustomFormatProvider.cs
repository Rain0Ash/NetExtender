// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using NetExtender.Logging.Common;
using NetExtender.Utilities.Types;

namespace NetExtender.Logging.Format
{
    public enum LoggerFormatProviderEvaluatorType
    {
        Time,
        Prefix,
        Thread
    }
    
    public delegate String? LoggerFormatResolver(LoggingMessageType type, LoggingMessageOptions options, DateTimeOffset offset, IFormatProvider? provider);
    
    public class LoggerCustomFormatProvider : LoggerFormatProvider, IReadOnlyList<LoggerFormatResolver>
    {
        protected List<LoggerFormatResolver> Resolvers { get; }

        public Int32 Count
        {
            get
            {
                return Resolvers.Count;
            }
        }

        public LoggerCustomFormatProvider()
        {
            Resolvers = new List<LoggerFormatResolver>(6) { Prefix, Time, Thread };
        }

        public override String? Format(String? message, LoggingMessageType type, LoggingMessageOptions options, DateTimeOffset offset, IFormatProvider? provider)
        {
            if (message is null)
            {
                return null;
            }
            
            Int32 count = Resolvers.Count;
            String?[] buffer = ArrayPool<String?>.Shared.Rent(count);
            try
            {
                Int32 i = 0;
                Int32 capacity = 0;
                foreach (String? item in Resolvers.Select(resolver => resolver(type, options, offset, provider)).WhereNotNull())
                {
                    buffer[i++] = item;
                    capacity += item.Length;
                }

                if (capacity <= 0)
                {
                    return null;
                }

                capacity += message.Length + 2;
                StringBuilder result = new StringBuilder(capacity, capacity);
                result.AppendJoin(null, buffer);
                return result.Length > 0 ? result.Append(": ").Append(message).ToString() : null;
            }
            finally
            {
                ArrayPool<String?>.Shared.Return(buffer, true);
            }
        }

        protected virtual LoggerFormatResolver Resolve(LoggerFormatProviderEvaluatorType type)
        {
            return type switch
            {
                LoggerFormatProviderEvaluatorType.Time => Time,
                LoggerFormatProviderEvaluatorType.Prefix => Prefix,
                LoggerFormatProviderEvaluatorType.Thread => Thread,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public Boolean Contains(LoggerFormatProviderEvaluatorType type)
        {
            return Contains(Resolve(type));
        }

        public Boolean Contains(LoggerFormatResolver resolver)
        {
            if (resolver is null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            return Resolvers.Contains(resolver);
        }

        public Int32 IndexOf(LoggerFormatProviderEvaluatorType type)
        {
            return IndexOf(Resolve(type));
        }

        public Int32 IndexOf(LoggerFormatResolver resolver)
        {
            if (resolver is null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            return Resolvers.IndexOf(resolver);
        }

        public Boolean Add(LoggerFormatProviderEvaluatorType type)
        {
            return Add(Resolve(type));
        }

        public Boolean Add(LoggerFormatResolver resolver)
        {
            if (resolver is null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            lock (Resolvers)
            {
                if (Contains(resolver))
                {
                    return false;
                }

                Resolvers.Add(resolver);
                return true;
            }
        }

        public Boolean Insert(Int32 index, LoggerFormatProviderEvaluatorType type)
        {
            return Insert(index, Resolve(type));
        }

        public Boolean Insert(Int32 index, LoggerFormatResolver resolver)
        {
            if (resolver is null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            lock (Resolvers)
            {
                if (Contains(resolver))
                {
                    return false;
                }
                
                if (index >= Resolvers.Count)
                {
                    Resolvers.Add(resolver);
                    return true;
                }

                Resolvers.Insert(index, resolver);
                return true;
            }
        }

        public void Remove(Int32 index)
        {
            lock (Resolvers)
            {
                Resolvers.RemoveAt(index);
            }
        }

        public Boolean Remove(LoggerFormatProviderEvaluatorType type)
        {
            return Remove(Resolve(type));
        }

        public Boolean Remove(LoggerFormatResolver resolver)
        {
            if (resolver is null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            return Resolvers.Remove(resolver);
        }

        public void Clear()
        {
            Resolvers.Clear();
        }

        public IEnumerator<LoggerFormatResolver> GetEnumerator()
        {
            return Resolvers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public LoggerFormatResolver this[Int32 index]
        {
            get
            {
                return Resolvers[index];
            }
        }
    }
}