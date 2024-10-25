// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Serialization
{
    [Serializable]
    public class ParseException : Exception
    {
        public Int32 Line { get; }

        public Boolean HasLine
        {
            get
            {
                return Line >= 0;
            }
        }

        public ParseException(Int32 line)
            : this(line, null)
        {
        }

        public ParseException(Int32 line, Exception? exception)
            : this(null, line, exception)
        {
        }

        public ParseException(String? message)
            : this(message, null)
        {
        }

        public ParseException(String? message, Exception? exception)
            : this(message, -1, exception)
        {
        }

        public ParseException(String? message, Int32 line)
            : this(message, line, null)
        {
        }

        public ParseException(String? message, Int32 line, Exception? exception)
            : base(message, exception)
        {
            Line = line;
        }
        
        protected ParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Line = info.GetInt32(nameof(Line));
        }
        
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Line), Line);
            base.GetObjectData(info, context);
        }
    }
}

