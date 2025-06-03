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
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Application
{
    public static class ResourceUtilities
    {
        public static Stream? TryGetResourceStream(Assembly assembly)
        {
            return TryGetResourceStream(assembly, null);
        }

        public static Stream? TryGetResourceStream(Assembly assembly, String? resource)
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

        public static ResourceReader? TryGetResourceReader(Stream stream)
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
            return Enumerate((String?) null);
        }

        public static IEnumerable<KeyValuePair<String, Object?>> Enumerate(String? resource)
        {
            Assembly? assembly = ReflectionUtilities.GetEntryAssembly();
            return assembly is not null ? Enumerate(assembly, resource) : Array.Empty<KeyValuePair<String, Object?>>();
        }

        public static IEnumerable<KeyValuePair<String, Object?>> Enumerate(Assembly assembly)
        {
            return Enumerate(assembly, (String?) null);
        }

        public static IEnumerable<KeyValuePair<String, Object?>> Enumerate(Assembly assembly, String? resource)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            using Stream? stream = TryGetResourceStream(assembly, resource);

            if (stream is null)
            {
                yield break;
            }

            using ResourceReader? reader = TryGetResourceReader(stream);

            if (reader is null)
            {
                yield break;
            }

            foreach (KeyValuePair<String, Object?> entry in reader.Enumerate())
            {
                yield return entry;
            }
        }

        public static IEnumerable<KeyValuePair<String, Stream>> Enumerate(Assembly assembly, Func<String, Boolean> predicate)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (String name in assembly.GetManifestResourceNames().Where(predicate))
            {
                Stream? stream = assembly.GetManifestResourceStream(name);

                if (stream is null)
                {
                    continue;
                }

                yield return new KeyValuePair<String, Stream>(name, stream);
            }
        }

        public static IEnumerable<KeyValuePair<String, Stream>> ToStream(IEnumerable<KeyValuePair<String, Object?>> entries)
        {
            if (entries is null)
            {
                throw new ArgumentNullException(nameof(entries));
            }

            foreach ((String? key, Object? value) in entries)
            {
                if (value is Stream stream)
                {
                    yield return new KeyValuePair<String, Stream>(key, stream);
                }
            }
        }

        public static Boolean Resource(String resource, [MaybeNullWhen(false)] out Byte[] result)
        {
            return Resource(resource, out _, out result);
        }

        public static Boolean Resource(String resource, [MaybeNullWhen(false)] out String type, [MaybeNullWhen(false)] out Byte[] result)
        {
            Assembly? assembly = ReflectionUtilities.GetEntryAssembly();

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
                throw new ArgumentNullOrEmptyStringException(resource, nameof(resource));
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

        public static IEnumerable<KeyValuePair<String, Object?>> Enumerate(this ResourceReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            foreach (DictionaryEntry entry in reader.OfType<DictionaryEntry>())
            {
                if (entry.Key is String key)
                {
                    yield return new KeyValuePair<String, Object?>(key, entry.Value);
                }
            }
        }
    }
}