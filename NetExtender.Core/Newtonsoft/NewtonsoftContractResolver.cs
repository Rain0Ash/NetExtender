using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace NetExtender.Newtonsoft
{
    public class NewtonsoftContractResolverWrapper : NewtonsoftContractResolver
    {
        private delegate List<MemberInfo> GetSerializableMembersDelegate(DefaultContractResolver resolver, Type type);
        private delegate JsonConverter? ResolveContractConverterDelegate(DefaultContractResolver resolver, Type type);
        private delegate String ResolvePropertyNameDelegate(DefaultContractResolver resolver, String property);
        private delegate String ResolveDictionaryKeyDelegate(DefaultContractResolver resolver, String key);
        private delegate String ResolveExtensionDataNameDelegate(DefaultContractResolver resolver, String name);
        private delegate JsonProperty CreatePropertyDelegate(DefaultContractResolver resolver, MemberInfo member, MemberSerialization serialization);
        private delegate IList<JsonProperty> CreatePropertiesDelegate(DefaultContractResolver resolver, Type type, MemberSerialization serialization);
        private delegate IValueProvider CreateMemberValueProviderDelegate(DefaultContractResolver resolver, MemberInfo member);
        private delegate IList<JsonProperty> CreateConstructorParametersDelegate(DefaultContractResolver resolver, ConstructorInfo constructor, JsonPropertyCollection properties);
        private delegate JsonProperty CreatePropertyFromConstructorParameterDelegate(DefaultContractResolver resolver, JsonProperty? property, ParameterInfo parameter);
        private delegate JsonContract CreateContractDelegate(DefaultContractResolver resolver, Type type);
        private delegate JsonISerializableContract CreateISerializableContractDelegate(DefaultContractResolver resolver, Type type);
        private delegate JsonObjectContract CreateObjectContractDelegate(DefaultContractResolver resolver, Type type);
        private delegate JsonDictionaryContract CreateDictionaryContractDelegate(DefaultContractResolver resolver, Type type);
        private delegate JsonArrayContract CreateArrayContractDelegate(DefaultContractResolver resolver, Type type);
        private delegate JsonLinqContract CreateLinqContractDelegate(DefaultContractResolver resolver, Type type);
        private delegate JsonStringContract CreateStringContractDelegate(DefaultContractResolver resolver, Type type);
        private delegate JsonPrimitiveContract CreatePrimitiveContractDelegate(DefaultContractResolver resolver, Type type);
        private delegate JsonDynamicContract CreateDynamicContractDelegate(DefaultContractResolver resolver, Type type);
        
        private static GetSerializableMembersDelegate GetSerializableMembersHandler { get; } = CreateDelegate<GetSerializableMembersDelegate>(nameof(GetSerializableMembers));
        private static ResolveContractConverterDelegate ResolveContractConverterHandler { get; } = CreateDelegate<ResolveContractConverterDelegate>(nameof(ResolveContractConverter));
        private static ResolvePropertyNameDelegate ResolvePropertyNameHandler { get; } = CreateDelegate<ResolvePropertyNameDelegate>(nameof(ResolvePropertyName));
        private static ResolveDictionaryKeyDelegate ResolveDictionaryKeyHandler { get; } = CreateDelegate<ResolveDictionaryKeyDelegate>(nameof(ResolvePropertyName));
        private static ResolveExtensionDataNameDelegate ResolveExtensionDataNameHandler { get; } = CreateDelegate<ResolveExtensionDataNameDelegate>(nameof(ResolveDictionaryKey));
        private static CreatePropertyDelegate CreatePropertyHandler { get; } = CreateDelegate<CreatePropertyDelegate>(nameof(CreateProperty));
        private static CreatePropertiesDelegate CreatePropertiesHandler { get; } = CreateDelegate<CreatePropertiesDelegate>(nameof(CreateProperties));
        private static CreateMemberValueProviderDelegate CreateMemberValueProviderHandler { get; } = CreateDelegate<CreateMemberValueProviderDelegate>(nameof(CreateMemberValueProvider));
        private static CreateConstructorParametersDelegate CreateConstructorParametersHandler { get; } = CreateDelegate<CreateConstructorParametersDelegate>(nameof(CreateConstructorParameters));
        private static CreatePropertyFromConstructorParameterDelegate CreatePropertyFromConstructorParameterHandler { get; } = CreateDelegate<CreatePropertyFromConstructorParameterDelegate>(nameof(CreatePropertyFromConstructorParameter));
        private static CreateContractDelegate CreateContractHandler { get; } = CreateDelegate<CreateContractDelegate>(nameof(CreateContract));
        private static CreateISerializableContractDelegate CreateISerializableContractHandler { get; } = CreateDelegate<CreateISerializableContractDelegate>(nameof(CreateISerializableContract));
        private static CreateObjectContractDelegate CreateObjectContractHandler { get; } = CreateDelegate<CreateObjectContractDelegate>(nameof(CreateObjectContract));
        private static CreateDictionaryContractDelegate CreateDictionaryContractHandler { get; } = CreateDelegate<CreateDictionaryContractDelegate>(nameof(CreateDictionaryContract));
        private static CreateArrayContractDelegate CreateArrayContractHandler { get; } = CreateDelegate<CreateArrayContractDelegate>(nameof(CreateArrayContract));
        private static CreateLinqContractDelegate CreateLinqContractHandler { get; } = CreateDelegate<CreateLinqContractDelegate>(nameof(CreateLinqContract));
        private static CreateStringContractDelegate CreateStringContractHandler { get; } = CreateDelegate<CreateStringContractDelegate>(nameof(CreateStringContract));
        private static CreatePrimitiveContractDelegate CreatePrimitiveContractHandler { get; } = CreateDelegate<CreatePrimitiveContractDelegate>(nameof(CreatePrimitiveContract));
        private static CreateDynamicContractDelegate CreateDynamicContractHandler { get; } = CreateDelegate<CreateDynamicContractDelegate>(nameof(CreateDynamicContract));

        public DefaultContractResolver? Resolver { get; private init; }

        public NewtonsoftContractResolverWrapper()
            : this(null)
        {
        }

        public NewtonsoftContractResolverWrapper(DefaultContractResolver? resolver)
        {
            Resolver = resolver;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("resolver")]
        internal static T? Wrap<T>(DefaultContractResolver? resolver) where T : NewtonsoftContractResolverWrapper, new()
        {
            return resolver is not null ? new T { Resolver = resolver } : null;
        }

        private static TDelegate CreateDelegate<TDelegate>(String name) where TDelegate : Delegate
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            const BindingFlags binding = BindingFlags.Instance | BindingFlags.NonPublic;
            MethodInfo? method = typeof(NewtonsoftContractResolver).GetMethod(name, binding);
            return method?.CreateTargetDelegate<DefaultContractResolver, TDelegate>() ?? throw new MissingMethodException(nameof(DefaultContractResolver), name);
        }

        protected override List<MemberInfo> GetSerializableMembers(Type type)
        {
            return Resolver is not null ? GetSerializableMembersHandler(Resolver, type) : base.GetSerializableMembers(type);
        }
        
        protected override JsonConverter? ResolveContractConverter(Type type)
        {
            return Resolver is not null ? ResolveContractConverterHandler(Resolver, type) : base.ResolveContractConverter(type);
        }

        protected override String ResolvePropertyName(String property)
        {
            return Resolver is not null ? ResolvePropertyNameHandler(Resolver, property) : base.ResolvePropertyName(property);
        }

        protected override String ResolveDictionaryKey(String key)
        {
            return Resolver is not null ? ResolveDictionaryKeyHandler(Resolver, key) : base.ResolveDictionaryKey(key);
        }

        protected override String ResolveExtensionDataName(String name)
        {
            return Resolver is not null ? ResolveExtensionDataNameHandler(Resolver, name) : base.ResolveExtensionDataName(name);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization serialization)
        {
            return Resolver is not null ? CreatePropertyHandler(Resolver, member, serialization) : base.CreateProperty(member, serialization);
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization serialization)
        {
            return Resolver is not null ? CreatePropertiesHandler(Resolver, type, serialization) : base.CreateProperties(type, serialization);
        }

        protected override IValueProvider CreateMemberValueProvider(MemberInfo member)
        {
            return Resolver is not null ? CreateMemberValueProviderHandler(Resolver, member) : base.CreateMemberValueProvider(member);
        }

        protected override IList<JsonProperty> CreateConstructorParameters(ConstructorInfo constructor, JsonPropertyCollection properties)
        {
            return Resolver is not null ? CreateConstructorParametersHandler(Resolver, constructor, properties) : base.CreateConstructorParameters(constructor, properties);
        }

        protected override JsonProperty CreatePropertyFromConstructorParameter(JsonProperty? property, ParameterInfo parameter)
        {
            return Resolver is not null ? CreatePropertyFromConstructorParameterHandler(Resolver, property, parameter) : base.CreatePropertyFromConstructorParameter(property, parameter);
        }

        protected override JsonContract CreateContract(Type type)
        {
            return Resolver is not null ? CreateContractHandler(Resolver, type) : base.CreateContract(type);
        }

        protected override JsonISerializableContract CreateISerializableContract(Type type)
        {
            return Resolver is not null ? CreateISerializableContractHandler(Resolver, type) : base.CreateISerializableContract(type);
        }

        protected override JsonObjectContract CreateObjectContract(Type type)
        {
            return Resolver is not null ? CreateObjectContractHandler(Resolver, type) : base.CreateObjectContract(type);
        }

        protected override JsonDictionaryContract CreateDictionaryContract(Type type)
        {
            return Resolver is not null ? CreateDictionaryContractHandler(Resolver, type) : base.CreateDictionaryContract(type);
        }

        protected override JsonArrayContract CreateArrayContract(Type type)
        {
            return Resolver is not null ? CreateArrayContractHandler(Resolver, type) : base.CreateArrayContract(type);
        }

        protected override JsonLinqContract CreateLinqContract(Type type)
        {
            return Resolver is not null ? CreateLinqContractHandler(Resolver, type) : base.CreateLinqContract(type);
        }

        protected override JsonStringContract CreateStringContract(Type type)
        {
            return Resolver is not null ? CreateStringContractHandler(Resolver, type) : base.CreateStringContract(type);
        }

        protected override JsonPrimitiveContract CreatePrimitiveContract(Type type)
        {
            return Resolver is not null ? CreatePrimitiveContractHandler(Resolver, type) : base.CreatePrimitiveContract(type);
        }

        protected override JsonDynamicContract CreateDynamicContract(Type type)
        {
            return Resolver is not null ? CreateDynamicContractHandler(Resolver, type) : base.CreateDynamicContract(type);
        }
    }
    
    public abstract class NewtonsoftContractResolver : DefaultContractResolver
    {
        protected override List<MemberInfo> GetSerializableMembers(Type type)
        {
            return base.GetSerializableMembers(type);
        }
        
        protected override JsonConverter? ResolveContractConverter(Type type)
        {
            return base.ResolveContractConverter(type);
        }

        protected override String ResolvePropertyName(String property)
        {
            return base.ResolvePropertyName(property);
        }

        protected override String ResolveDictionaryKey(String key)
        {
            return base.ResolveDictionaryKey(key);
        }

        protected override String ResolveExtensionDataName(String name)
        {
            return base.ResolveExtensionDataName(name);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization serialization)
        {
            return base.CreateProperty(member, serialization);
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization serialization)
        {
            return base.CreateProperties(type, serialization);
        }

        protected override IList<JsonProperty> CreateConstructorParameters(ConstructorInfo constructor, JsonPropertyCollection properties)
        {
            return base.CreateConstructorParameters(constructor, properties);
        }

        protected override JsonProperty CreatePropertyFromConstructorParameter(JsonProperty? property, ParameterInfo parameter)
        {
            return base.CreatePropertyFromConstructorParameter(property, parameter);
        }

        protected override JsonContract CreateContract(Type type)
        {
            return base.CreateContract(type);
        }

        protected override JsonISerializableContract CreateISerializableContract(Type type)
        {
            return base.CreateISerializableContract(type);
        }

        protected override JsonObjectContract CreateObjectContract(Type type)
        {
            return base.CreateObjectContract(type);
        }

        protected override JsonDictionaryContract CreateDictionaryContract(Type type)
        {
            return base.CreateDictionaryContract(type);
        }

        protected override JsonArrayContract CreateArrayContract(Type type)
        {
            return base.CreateArrayContract(type);
        }

        protected override JsonLinqContract CreateLinqContract(Type type)
        {
            return base.CreateLinqContract(type);
        }

        protected override JsonStringContract CreateStringContract(Type type)
        {
            return base.CreateStringContract(type);
        }

        protected override JsonPrimitiveContract CreatePrimitiveContract(Type type)
        {
            return base.CreatePrimitiveContract(type);
        }

        protected override JsonDynamicContract CreateDynamicContract(Type type)
        {
            return base.CreateDynamicContract(type);
        }
    }
}

namespace NetExtender.Serialization.Json
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class TextJsonConverterFactoryWrapper : TextJsonConverterFactory
    {
        private readonly JsonConverterFactory _factory;
        public JsonConverterFactory Factory
        {
            get
            {
                return _factory ?? throw new InvalidOperationException();
            }
            private init
            {
                _factory = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        protected TextJsonConverterFactoryWrapper()
        {
            _factory = null!;
        }

        public TextJsonConverterFactoryWrapper(JsonConverterFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("factory")]
        internal static T? Wrap<T>(JsonConverterFactory? factory) where T : TextJsonConverterFactoryWrapper, new()
        {
            return factory is not null ? new T { Factory = factory } : null;
        }

        public override Boolean CanConvert(Type type)
        {
            return Factory.CanConvert(type);
        }

        public override JsonConverter? CreateConverter(Type type, JsonSerializerOptions options)
        {
            return Factory.CreateConverter(type, options);
        }
    }

    public abstract class TextJsonConverterFactory : JsonConverterFactory
    {
        public abstract override Boolean CanConvert(Type type);
        public abstract override JsonConverter? CreateConverter(Type type, JsonSerializerOptions options);
    }
}