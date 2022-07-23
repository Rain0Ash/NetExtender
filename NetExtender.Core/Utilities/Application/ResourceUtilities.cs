// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Application
{
    public static class ResourceUtilities
    {
        private static Stream? TryGetResourceStream(Assembly assembly)
        {
            return TryGetResourceStream(assembly, null);
        }

        private static Stream? TryGetResourceStream(Assembly assembly, String? resource)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                resource ??= assembly.GetName().Name + ".g.resources";
                return assembly.GetManifestResourceStream(resource);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        private static ResourceReader? TryGetResourceReader(Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            try
            {
                return new ResourceReader(stream);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public static IEnumerable<KeyValuePair<String, Object?>> Enumerate()
        {
            Assembly? assembly = Assembly.GetEntryAssembly();
            return assembly is not null ? Enumerate(assembly) : Array.Empty<KeyValuePair<String, Object?>>();
        }
        
        public static IEnumerable<KeyValuePair<String, Object?>> Enumerate(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            using Stream? stream = TryGetResourceStream(assembly);

            if (stream is null)
            {
                yield break;
            }

            using ResourceReader? reader = TryGetResourceReader(stream);

            if (reader is null)
            {
                yield break;
            }

            foreach (DictionaryEntry entry in reader.OfType<DictionaryEntry>())
            {
                if (entry.Key is String key)
                {
                    yield return new KeyValuePair<String, Object?>(key, entry.Value);
                }
            }
        }

        public static IEnumerable<KeyValuePair<String, UnmanagedMemoryStream>> ToStream(this IEnumerable<KeyValuePair<String, Object?>> entries)
        {
            if (entries is null)
            {
                throw new ArgumentNullException(nameof(entries));
            }

            foreach ((String? key, Object? value) in entries)
            {
                if (value is UnmanagedMemoryStream stream)
                {
                    yield return new KeyValuePair<String, UnmanagedMemoryStream>(key, stream);
                }
            }
        }

        public static Boolean Resource(String resource, [MaybeNullWhen(false)] out Byte[] result)
        {
            return Resource(resource, out _, out result);
        }

        public static Boolean Resource(String resource, [MaybeNullWhen(false)] out String type, [MaybeNullWhen(false)] out Byte[] result)
        {
            Assembly? assembly = Assembly.GetEntryAssembly();
            
            if (assembly is not null && Resource(assembly, resource, out type, out result))
            {
                return true;
            }

            type = default;
            result = default;
            return false;
        }

        public static Boolean Resource(Assembly assembly, String resource, [MaybeNullWhen(false)] out Byte[] result)
        {
            return Resource(assembly, resource, out _, out result);
        }

        public static Boolean Resource(Assembly assembly, String resource, [MaybeNullWhen(false)] out String type, [MaybeNullWhen(false)] out Byte[] result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (String.IsNullOrEmpty(resource))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(resource));
            }

            using Stream? stream = TryGetResourceStream(assembly);

            if (stream is null)
            {
                type = default;
                result = default;
                return false;
            }

            using ResourceReader? reader = TryGetResourceReader(stream);

            if (reader is null)
            {
                type = default;
                result = default;
                return false;
            }

            try
            {
                reader.GetResourceData(resource, out type, out result);
                return true;
            }
            catch (Exception)
            {
                type = default;
                result = default;
                return false;
            }
        }
    }
}