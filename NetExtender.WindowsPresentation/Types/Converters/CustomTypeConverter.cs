// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    public abstract class CustomTypeConverter : IValueConverter
    {
        protected delegate Object Handler(Object @object, Object? parameter, CultureInfo? culture);
        
        private Dictionary<Type, Handler>? _cache;
        protected virtual Dictionary<Type, Handler> Cache
        {
            get
            {
                return _cache ??= Build();
            }
        }

        private Dictionary<Type, Handler> Build()
        {
            return Initialize().DistinctBy(pair => pair.Key).ToDictionary();
        }

        protected abstract IEnumerable<KeyValuePair<Type, Handler>> Initialize();

        public abstract Object? Convert(Object? value, Type? targetType, Object? parameter, CultureInfo? culture);
        public abstract Object ConvertBack(Object? value, Type? targetType, Object? parameter, CultureInfo? culture);
    }
}