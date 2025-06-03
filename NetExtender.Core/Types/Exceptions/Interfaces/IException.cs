using System;
using System.Collections;
using System.Reflection;

namespace NetExtender.Types.Exceptions.Interfaces
{
    public interface IException
    {
        public Int32 HResult { get; }
        public String? Source { get; }
        public String Message { get; }
        public String? HelpLink { get; }
        public String? StackTrace { get; }
        public IDictionary Data { get; }
        public MethodBase? TargetSite { get; }
        public Exception? InnerException { get; }

        public Exception Exception
        {
            get
            {
                return this as Exception ?? throw new InvalidOperationException($"Type '{GetType()}' must inherit '{typeof(Exception)}'.");
            }
        }
    }
}