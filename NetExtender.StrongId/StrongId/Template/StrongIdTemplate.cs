// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using NetExtender.StrongId.Attributes;

namespace NetExtender.StrongId.Template
{
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    internal static class StrongIdTemplate
    {
        public const String StrongId = $"{nameof(NetExtender)}.{nameof(StrongId)}";
        private const String StrongIdSource = $"{StrongId}.{nameof(StrongId)}";
        private const String Template = $"{StrongIdSource}.{nameof(Template)}";
        private const String Generic = $"{Template}.{nameof(Generic)}";

        public const String TypeConverterAttributeSource = $"    [{nameof(System)}.{nameof(System.ComponentModel)}.{nameof(TypeConverter)}(typeof(STRONGID{nameof(TypeConverter)}))]";
        public const String NewtonsoftJsonAttributeSource = "    [Newtonsoft.Json.JsonConverter(typeof(STRONGIDNewtonsoftJsonConverter))]";
        public const String TextJsonAttributeSource = "    [System.Text.Json.Serialization.JsonConverter(typeof(STRONGIDTextJsonConverter))]";
        public const String SwaggerSchemaFilterAttributeSource = "    [Swashbuckle.AspNetCore.Annotations.SwaggerSchemaFilter(typeof(STRONGIDSchemaFilter))]";

        private static Assembly? assembly;
        public static Assembly Assembly
        {
            get
            {
                return assembly ??= typeof(StrongIdTemplate).Assembly;
            }
        }

        private static String? attributesource;
        public static String AttributeSource
        {
            get
            {
                return attributesource ??= LoadAttributeTemplateForEmitting(nameof(StrongIdAttribute));
            }
        }

        private static String? assemblyattributesource;
        public static String AssemblyAttributeSource
        {
            get
            {
                return assemblyattributesource ??= LoadAttributeTemplateForEmitting(nameof(StrongIdAssemblyAttribute));
            }
        }

        private static String? underlyingtypesource;
        public static String UnderlyingTypeSource
        {
            get
            {
                return underlyingtypesource ??= LoadTemplateForEmitting(nameof(StrongIdUnderlyingType));
            }
        }

        private static String? conversiontypesource;
        public static String ConversionTypeSource
        {
            get
            {
                return conversiontypesource ??= LoadTemplateForEmitting(nameof(StrongIdConversionType));
            }
        }

        private static String? convertertypesource;
        public static String ConverterTypeSource
        {
            get
            {
                return convertertypesource ??= LoadTemplateForEmitting(nameof(StrongIdConverterType));
            }
        }

        private static String? interfacetypesource;
        public static String InterfaceTypeSource
        {
            get
            {
                return interfacetypesource ??= LoadTemplateForEmitting(nameof(StrongIdInterfaceType));
            }
        }

        private static String? interfacesource;
        public static String InterfaceSource
        {
            get
            {
                return interfacesource ??= LoadInterfaceTemplateForEmitting(nameof(IStrongId));
            }
        }

        private static String? autogeneratedheader;
        private static String AutoGeneratedHeader
        {
            get
            {
                return autogeneratedheader ??= LoadResource($"{Generic}.{nameof(StrongId)}{nameof(AutoGeneratedHeader)}.cs");
            }
        }

        private static ImmutableDictionary<StrongIdUnderlyingType, Resource>? @internal;
        private static ImmutableDictionary<StrongIdUnderlyingType, Resource> Resources
        {
            get
            {
                if (@internal is not null)
                {
                    return @internal;
                }

                ImmutableDictionary<StrongIdUnderlyingType, Resource>.Builder builder = ImmutableDictionary.CreateBuilder<StrongIdUnderlyingType, Resource>();

                foreach (Object? item in Enum.GetValues(typeof(StrongIdUnderlyingType)))
                {
                    StrongIdUnderlyingType type = (StrongIdUnderlyingType) item;
                    
                    if (type.IsString())
                    {
                        continue;
                    }
                    
                    builder.Add(type, new Resource(type));
                }

                return @internal ??= builder.ToImmutable();
            }
        }

        public static Resource Get(StrongIdUnderlyingType type)
        {
            return Resources.TryGetValue(type, out Resource resources) ? resources : throw new InvalidOperationException($"Invalid enum type: {type}");
        }

        private static String LoadResource(String resource)
        {
            if (resource is null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            if (TryLoadResource(resource) is String result)
            {
                return result;
            }

            throw new StrongIdTemplateLoadException(Assembly, resource);
        }

        private static String? TryLoadResource(String resource)
        {
            if (resource is null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            using Stream? stream = Assembly.GetManifestResourceStream(resource);

            if (stream is null)
            {
                return null;
            }

            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            return reader.ReadToEnd();
        }

        private static String LoadTemplateForEmitting(String resource)
        {
            return AutoGeneratedHeader + LoadResource($"{StrongIdSource}.{resource}.cs");
        }

        private static String LoadAttributeTemplateForEmitting(String resource)
        {
            return AutoGeneratedHeader + LoadResource($"{StrongIdSource}.{nameof(Attributes)}.{resource}.cs");
        }

        private static String LoadInterfaceTemplateForEmitting(String resource)
        {
            return AutoGeneratedHeader + LoadResource($"{StrongIdSource}.Interfaces.{resource}.cs");
        }

        public readonly struct Resource
        {
            public StrongIdUnderlyingType Underlying { get; }
            public String Type { get; }
            public String Name { get; }
            public String Header { get; }
            public String Declaration { get; }
            public String ImplicitTo { get; }
            public String ExplicitTo { get; }
            public String ImplicitFrom { get; }
            public String ExplicitFrom { get; }
            public String EqualityOperators { get; }
            public String ComparableOperators { get; }
            public String Template { get; }
            public String Properties { get; }
            public String Constructors { get; }
            public String Methods { get; }
            public String TypeConverter { get; }
            public String TextJsonConverter { get; }
            public String NewtonsoftJsonConverter { get; }
            public String EntityFrameworkValueConverter { get; }
            public String DapperTypeHandler { get; }
            public String SwaggerSchemaFilter { get; }
            public String Parseable { get; }
            public String Equatable { get; }
            public String Comparable { get; }
            public String Formattable { get; }
            public String Serializable { get; }
            public String SwaggerType { get; }
            public String? SwaggerFormat { get; }

            public Resource(StrongIdUnderlyingType type)
            {
                if (!Convert(type, out String? @namespace, out String? typename) || @namespace is null || typename is null)
                {
                    throw new NotSupportedException($"Invalid enum type: {type}");
                }

                Underlying = type;
                (Type, Name) = type.UnderlyingType();
                Header = AutoGeneratedHeader;
                Declaration = Load(type, @namespace, nameof(Declaration));
                ImplicitTo = Load(type, @namespace, nameof(ImplicitTo));
                ExplicitTo = Load(type, @namespace, nameof(ExplicitTo));
                ImplicitFrom = Load(type, @namespace, nameof(ImplicitFrom));
                ExplicitFrom = Load(type, @namespace, nameof(ExplicitFrom));
                EqualityOperators = Load(type, @namespace, nameof(EqualityOperators));
                ComparableOperators = Load(type, @namespace, nameof(ComparableOperators));
                Template = Load(type, @namespace, nameof(Template));
                Properties = Load(type, @namespace, nameof(Properties));
                Constructors = Load(type, @namespace, nameof(Constructors));
                Methods = Load(type, @namespace, nameof(Methods));
                TypeConverter = Load(type, @namespace, nameof(TypeConverter));
                TextJsonConverter = Load(type, @namespace, nameof(TextJsonConverter));
                NewtonsoftJsonConverter = Load(type, @namespace, nameof(NewtonsoftJsonConverter));
                TypeConverter = Load(type, @namespace, nameof(TypeConverter));
                EntityFrameworkValueConverter = Load(type, @namespace, nameof(EntityFrameworkValueConverter));
                DapperTypeHandler = Load(type, @namespace, nameof(DapperTypeHandler));
                SwaggerSchemaFilter = Load(type, @namespace, nameof(SwaggerSchemaFilter));
                Parseable = Load(type, @namespace, nameof(Parseable));
                Equatable = Load(type, @namespace, nameof(Equatable));
                Comparable = Load(type, @namespace, nameof(Comparable));
                Formattable = Load(type, @namespace, nameof(Formattable));
                Serializable = Load(type, @namespace, nameof(Serializable));
                (SwaggerType, SwaggerFormat) = type.Swagger();
            }

            private static Boolean Convert(StrongIdUnderlyingType type, out String? @namespace, out String? typename)
            {
                typename = Enum.GetName(typeof(StrongIdUnderlyingType), type);

                if (typename is null)
                {
                    @namespace = null;
                    return false;
                }

                String filename = typename;
                switch (type)
                {
                    case StrongIdUnderlyingType.StringOrdinal:
                    case StrongIdUnderlyingType.StringOrdinalIgnoreCase:
                    case StrongIdUnderlyingType.StringNullableOrdinal:
                    case StrongIdUnderlyingType.StringNullableOrdinalIgnoreCase:
                        typename = typename.Replace("Ordinal", ".Ordinal");
                        filename = filename.Remove("Ordinal");
                        break;
                    case StrongIdUnderlyingType.StringCurrentCulture:
                    case StrongIdUnderlyingType.StringCurrentCultureIgnoreCase:
                    case StrongIdUnderlyingType.StringNullableCurrentCulture:
                    case StrongIdUnderlyingType.StringNullableCurrentCultureIgnoreCase:
                        typename = typename.Replace("CurrentCulture", ".Culture");
                        filename = filename.Remove("CurrentCulture");
                        break;
                    case StrongIdUnderlyingType.StringInvariantCulture:
                    case StrongIdUnderlyingType.StringInvariantCultureIgnoreCase:
                    case StrongIdUnderlyingType.StringNullableInvariantCulture:
                    case StrongIdUnderlyingType.StringNullableInvariantCultureIgnoreCase:
                        typename = typename.Replace("InvariantCulture", ".Invariant");
                        filename = filename.Remove("InvariantCulture");
                        break;
                    default:
                        break;
                }

                if (type.IsIgnoreCase())
                {
                    typename = typename.Replace("IgnoreCase", ".IgnoreCase");
                    filename = filename.Remove("IgnoreCase");
                }

                if (type.IsNullable())
                {
                    typename = typename.Replace("Nullable", ".Nullable");
                    filename = filename.Remove("Nullable");
                }
                
                @namespace = $"{StrongIdTemplate.Template}.{typename}.{filename}";
                return true;
            }

            private static String Load(StrongIdUnderlyingType type, String @namespace, String property)
            {
                if (String.IsNullOrEmpty(property))
                {
                    throw new ArgumentException("Value cannot be null or empty.", nameof(property));
                }

                if (type.IsString() && type.IsNullable())
                {
                    @namespace = @namespace.Remove($".{nameof(Nullable)}") + $".{nameof(Nullable)}";
                }

                String? result;
                if ((result = TryLoadResource($"{@namespace}.{property}.cs")) is not null)
                {
                    return result;
                }
                
                if (type.IsNullable() && (result = TryLoadResource($"{@namespace.Remove($".{nameof(Nullable)}")}.{property}.cs")) is not null)
                {
                    return result;
                }

                if (type.IsString() && (result = LoadString(type, @namespace, property)) is not null)
                {
                    return result;
                }

                if (type.IsNullable() && (result = TryLoadResource($"{Generic}.{nameof(Nullable)}.{property}.cs")) is not null)
                {
                    return result;
                }

                return TryLoadResource($"{Generic}.{property}.cs") ?? throw new StrongIdTemplateLoadException(Assembly, $"{@namespace}.{property}.cs");
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? LoadString(StrongIdUnderlyingType type, String @namespace, String property)
            {
                return type.IsNullable() ? LoadNullableString(type, @namespace, property) : LoadNotNullString(type, @namespace, property);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? LoadNotNullString(StrongIdUnderlyingType type, String @namespace, String property)
            {
                if (!type.IsIgnoreCase())
                {
                    return type.IsCultureString() ? TryLoadResource($"{@namespace.Remove(".Ordinal").Remove(".Culture").Remove(".Invariant")}.{property}.cs") : null;
                }

                String? result;
                if ((result = TryLoadResource($"{@namespace.Remove(".IgnoreCase")}.{property}.cs")) is not null)
                {
                    return result;
                }

                return type.IsCultureString() ? TryLoadResource($"{@namespace.Remove(".IgnoreCase").Remove(".Ordinal").Remove(".Culture").Remove(".Invariant")}.{property}.cs") : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? LoadNullableString(StrongIdUnderlyingType type, String @namespace, String property)
            {
                String? result;
                if ((result = TryLoadResource($"{@namespace.Remove($".{nameof(Nullable)}")}.{property}.cs")) is not null)
                {
                    return result;
                }

                if (!type.IsIgnoreCase())
                {
                    if (!type.IsCultureString())
                    {
                        return null;
                    }

                    return TryLoadResource($"{@namespace.Remove(".Ordinal").Remove(".Culture").Remove(".Invariant")}.{property}.cs") ??
                           TryLoadResource($"{@namespace.Remove($".{nameof(Nullable)}").Remove(".Ordinal").Remove(".Culture").Remove(".Invariant")}.{property}.cs");
                }

                if ((result = TryLoadResource($"{@namespace.Remove(".IgnoreCase")}.{property}.cs")) is not null)
                {
                    return result;
                }

                if ((result = TryLoadResource($"{@namespace.Remove($".{nameof(Nullable)}").Remove(".IgnoreCase")}.{property}.cs")) is not null)
                {
                    return result;
                }

                if (!type.IsCultureString())
                {
                    return null;
                }

                return TryLoadResource($"{@namespace.Remove(".IgnoreCase").Remove(".Ordinal").Remove(".Culture").Remove(".Invariant")}.{property}.cs") ??
                       TryLoadResource($"{@namespace.Remove($".{nameof(Nullable)}").Remove(".IgnoreCase").Remove(".Ordinal").Remove(".Culture").Remove(".Invariant")}.{property}.cs");
            }
        }
    }

    [Serializable]
    public class StrongIdTemplateLoadException : InvalidOperationException
    {
        public StrongIdTemplateLoadException(Assembly assembly, String resource)
            : base(Initialize(assembly, resource))
        {
        }

        protected StrongIdTemplateLoadException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static String Initialize(Assembly assembly, String resource)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (resource is null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            String[] exists = assembly.GetManifestResourceNames();
            throw new ArgumentException($"Could not find embedded resource {resource}. Available names: {String.Join(", ", exists)}");
        }
    }
}