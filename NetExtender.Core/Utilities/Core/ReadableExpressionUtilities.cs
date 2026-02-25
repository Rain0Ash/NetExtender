using System;
using AgileObjects.ReadableExpressions;

namespace NetExtender.Utilities.Core
{
    public static class ReadableExpressionUtilities
    {
        public static Func<ITranslationSettings?, ITranslationSettings>? Settings { get; set; }
    }
}