// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AutoMapper;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities
{
    public static class MapperUtilities
    {
        private static IMapper Mapper { get; }

        static MapperUtilities()
        {
            MapperConfiguration config = new MapperConfiguration(_ => { });
            Mapper = config.CreateMapper();
        }

        public static T Map<T>(this Object? value)
        {
            return Mapper.Map<T>(value);
        }
        
        public static T? Map<T>(this Object? value, T? alternate)
        {
            return Map(Mapper, value, alternate);
        }
        
        public static T? Map<T>(this IMapper mapper, Object? value, T? alternate)
        {
            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            return mapper.TryMap(value, out T? result) ? result : alternate;
        }
        
        public static T? Map<T>(this Object value, Func<T> factory)
        {
            return Map(Mapper, value, factory);
        }

        public static T? Map<T>(this IMapper mapper, Object? value, Func<T> factory)
        {
            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return mapper.TryMap(value, out T? result) ? result : factory.Invoke();
        }

        public static Boolean TryMap<T>(this Object value, out T? result)
        {
            return TryMap(Mapper, value, out result);
        }

        public static Boolean TryMap<T>(this IMapper mapper, Object? value, out T? result)
        {
            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            try
            {
                result = mapper.Map<T>(value);
                return true;
            }
            catch (AutoMapperMappingException)
            {
                result = default;
                return false;
            }
        }
        
        public static IEnumerable<TMap> MapSelect<T, TMap>(this IEnumerable<T?> source)
        {
            return MapSelect<T, TMap>(source, Mapper);
        }

        public static IEnumerable<TMap> MapSelect<T, TMap>(this IEnumerable<T?> source, IMapper mapper)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            TMap MapInternal(T? item)
            {
                return mapper.Map<TMap>(item);
            }

            return source.Select(MapInternal);
        }
        
        public static IEnumerable<TMap> TryMapSelect<T, TMap>(this IEnumerable<T> source)
        {
            return TryMapSelect<T, TMap>(source, Mapper);
        }

        public static IEnumerable<TMap> TryMapSelect<T, TMap>(this IEnumerable<T> source, IMapper mapper)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }
            
            Boolean MapInternal(T item, [MaybeNullWhen(false)] out TMap? result)
            {
                return mapper.TryMap(item, out result);
            }

            return source.TryParse<T, TMap>(MapInternal);
        }
    }
}