// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using NetExtender.Logging.Format.Interfaces;

namespace NetExtender.Logging.Behavior
{
    public abstract class ColorLoggerBehavior<TLevel> : LoggerBehavior<TLevel> where TLevel : unmanaged, Enum
    {
        protected ColorLoggerBehavior()
        {
        }

        protected ColorLoggerBehavior(ILoggerFormatProvider<TLevel> formatter)
            : base(formatter)
        {
        }

        protected abstract void Color(TLevel level, out Color? foreground, out Color? background);
    }
}