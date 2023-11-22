// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using NetExtender.NewtonSoft.Utilities;
using NetExtender.Types.Enums;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace NetExtender.NewtonSoft.Types.Enums
{
    public class EnumJsonConverter : JsonConverter
    {
        private class EnumProperties
        {
            public Type Type { get; }
            public Type Underlying { get; }
            public Func<Object, Object> GetId { get; }
            public Func<Object, String> GetTitle { get; }
            public CreateMethod Create { get; }
            public TryParseMethod TryParse { get; }

            public EnumProperties(Type type)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
                
                Type[]? arguments = type.GetGenericArguments(typeof(Enum<,>)) ?? type.GetGenericArguments(typeof(Enum<>));

                if (arguments is null || arguments.Length <= 0)
                {
                    throw new ArgumentException($"Can't get generic arguments of '{type}'.", nameof(type));
                }

                Underlying = arguments[0];
                PropertyInfo id = type.GetProperty("Id") ?? throw new InvalidOperationException($"Type '{type}' doesn't contains property 'Id'.");
                PropertyInfo title = type.GetProperty("Title") ?? throw new InvalidOperationException($"Type '{type}' doesn't contains property 'Title'.");
                GetId = CreateGetExpression<Object>(type, id).Compile();
                GetTitle = CreateGetExpression<String>(type, title).Compile();
                Create = new CreateMethod(type);
                TryParse = new TryParseMethod(type);
            }

            private static Expression<Func<Object, TValue>> CreateGetExpression<TValue>(Type type, PropertyInfo property)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                if (property is null)
                {
                    throw new ArgumentNullException(nameof(property));
                }

                ParameterExpression value = Expression.Parameter(typeof(Object), nameof(value));
                UnaryExpression source = Expression.Convert(value, type);
                MemberExpression access = Expression.Property(source, property);
                UnaryExpression result = Expression.Convert(access, typeof(TValue));
                return Expression.Lambda<Func<Object, TValue>>(result, value);
            }

            public record CreateMethod
            {
                public Func<Enum, Object> Id { get; }
                public Func<Enum, String, Object> Title { get; }
                
                public CreateMethod(Type type)
                {
                    if (type is null)
                    {
                        throw new ArgumentNullException(nameof(type));
                    }

                    (Expression<Func<Enum, Object>> Id, Expression<Func<Enum, String, Object>> Title) expression = CreateCreateExpression(type);
                    Id = expression.Id.Compile();
                    Title = expression.Title.Compile();
                }
                
                private static (Expression<Func<Enum, Object>> Id, Expression<Func<Enum, String, Object>> Title) CreateCreateExpression(Type type)
                {
                    if (type is null)
                    {
                        throw new ArgumentNullException(nameof(type));
                    }

                    Type[]? arguments = type.GetGenericArguments(typeof(Enum<,>)) ?? type.GetGenericArguments(typeof(Enum<>));

                    if (arguments is null || arguments.Length <= 0)
                    {
                        throw new ArgumentException($"Can't get generic arguments of '{type}'.", nameof(type));
                    }
    
                    Type underlying = arguments[0];
                    Type @enum = arguments.Length > 1 ? arguments[1] : typeof(Enum<>).MakeGenericType(underlying);

                    const BindingFlags binding = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;
                    MethodInfo? method = @enum.GetMethod("Create", binding, new[] { underlying });
                
                    if (method is null)
                    {
                        throw new InvalidOperationException($"Type '{@enum}' doesn't contain method 'Create'.");
                    }
                
                    ParameterExpression value = Expression.Parameter(typeof(Enum), nameof(value));
                    UnaryExpression convert = Expression.Convert(value, underlying);
                    MethodCallExpression call = Expression.Call(null, method, convert);
                    UnaryExpression result = Expression.Convert(call, typeof(Object));
                    Expression<Func<Enum, Object>> enumlambda = Expression.Lambda<Func<Enum, Object>>(result, value);
                    
                    method = @enum.GetMethod("Create", binding, new[] { underlying, typeof(String) });
                
                    if (method is null)
                    {
                        throw new InvalidOperationException($"Type '{@enum}' doesn't contain method 'Create'.");
                    }
                    
                    ParameterExpression title = Expression.Parameter(typeof(String), nameof(title));
                    call = Expression.Call(null, method, convert, title);
                    result = Expression.Convert(call, typeof(Object));
                    Expression<Func<Enum, String, Object>> titlelambda = Expression.Lambda<Func<Enum, String, Object>>(result, value, title);

                    return (enumlambda, titlelambda);
                }
            }

            public record TryParseMethod
            {
                public TryParseHandler<Enum, Object> Id { get; }
                public TryParseHandler<String, Object> Title { get; }

                public TryParseMethod(Type type)
                {
                    if (type is null)
                    {
                        throw new ArgumentNullException(nameof(type));
                    }
                    
                    Id = CreateTryParseExpression<Enum, Object>(type).Compile();
                    Title = CreateTryParseExpression<String, Object>(type).Compile();
                }
                
                private static Expression<TryParseHandler<TInput, TOutput>> CreateTryParseExpression<TInput, TOutput>(Type type)
                {
                    if (type is null)
                    {
                        throw new ArgumentNullException(nameof(type));
                    }

                    Type[]? arguments = type.GetGenericArguments(typeof(Enum<,>)) ?? type.GetGenericArguments(typeof(Enum<>));

                    if (arguments is null || arguments.Length <= 0)
                    {
                        throw new ArgumentException($"Can't get generic arguments of '{type}'.", nameof(type));
                    }
                    
                    Type underlying = arguments[0];
                    Type @enum = arguments.Length > 1 ? arguments[1] : typeof(Enum<>).MakeGenericType(underlying);

                    const BindingFlags binding = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;
                    Type generic = typeof(TInput) == typeof(String) ? typeof(String) : underlying;
                    MethodInfo? method = type.GetMethod(nameof(System.Enum.TryParse), binding, new[] { generic, @enum.MakeByRefType() });

                    if (method is null)
                    {
                        throw new InvalidOperationException($"Type '{type}' doesn't contains method '{nameof(System.Enum.TryParse)}'.");
                    }

                    ParameterExpression value = Expression.Parameter(typeof(TInput), nameof(value));
                    ParameterExpression result = Expression.Parameter(typeof(TOutput).MakeByRefType(), nameof(result));

                    ParameterExpression temp = Expression.Variable(@enum, nameof(temp));
                    ParameterExpression @return = Expression.Variable(typeof(Boolean), nameof(@return));

                    MethodCallExpression call = Expression.Call(null, method, typeof(TInput) == typeof(String) ? value : Expression.Convert(value, underlying), temp);
                    BinaryExpression assign = Expression.Assign(result, Expression.Convert(temp, typeof(TOutput)));

                    BlockExpression body = Expression.Block(new[] { temp, @return }, Expression.Assign(@return, call), assign, @return);
                    return Expression.Lambda<TryParseHandler<TInput, TOutput>>(body, value, result);
                }
            }
        }

        private static ConcurrentDictionary<Type, EnumProperties> EnumPropertiesCache { get; } = new ConcurrentDictionary<Type, EnumProperties>();
        
        public override Boolean CanConvert(Type objectType)
        {
            return EnumPropertiesCache.ContainsKey(objectType) || ReflectionUtilities.IsSubclassOfRawGeneric(typeof(Enum<>), objectType);
        }
        
        private static EnumProperties Properties(Type objectType)
        {
            return EnumPropertiesCache.GetOrAdd(objectType, type => new EnumProperties(type));
        }

        public override void WriteJson(JsonWriter writer, Object? value, JsonSerializer serializer)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            if (value is null)
            {
                writer.WriteNull();
                return;
            }

            Type type = value.GetType();
            if (!EnumPropertiesCache.TryGetOrAdd(type, Properties, out EnumProperties? properties))
            {
                throw new JsonException($"Unsupported type '{type}' for {GetType()}.");
            }

            NamingStrategy? strategy = serializer.GetNamingStrategy();
            
            writer.WriteStartObject();
            writer.WritePropertyName(strategy.NamingStrategy("Id", false));
            writer.WriteValue(properties.GetId(value));
            writer.WritePropertyName(strategy.NamingStrategy("Title", false));
            writer.WriteValue(properties.GetTitle(value));
            writer.WriteEndObject();
        }

        public override Object? ReadJson(JsonReader reader, Type type, Object? existingValue, JsonSerializer serializer)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }
            
            JToken token = JToken.Load(reader);
                
            @switch:
            switch (token.Type)
            {
                case JTokenType.Integer:
                {
                    Int64 value = token.Value<Int64>();
                    
                    if (type.IsEnum)
                    {
                        return Enum.ToObject(type, value);
                    }
                    
                    if (!EnumPropertiesCache.TryGetOrAdd(type, Properties, out EnumProperties? properties))
                    {
                        throw new JsonException($"Unsupported type '{type}' for {GetType()}.");
                    }

                    Enum id = (Enum) Enum.ToObject(properties.Underlying, value);
                    return properties.TryParse.Id(id, out Object? @enum) ? @enum : properties.Create.Id(id);
                }
                case JTokenType.String:
                {
                    String? value = token.Value<String>();

                    if (value is null)
                    {
                        return null;
                    }

                    Object? result;
                    if (type.IsEnum)
                    {
                        return Enum.TryParse(type, value, true, out result) ? result : throw new JsonException($"Value '{value}' is not valid for enum type '{type}'.");
                    }
                    
                    if (!EnumPropertiesCache.TryGetOrAdd(type, Properties, out EnumProperties? properties))
                    {
                        throw new JsonException($"Unsupported type '{type}' for {GetType()}.");
                    }

                    if (properties.TryParse.Title(value, out result))
                    {
                        return result;
                    }
                    
                    if (properties.TryParse.Title(value, out result))
                    {
                        return result;
                    }

                    if (Enum.TryParse(properties.Underlying, value, true, out result) && result is Enum id)
                    {
                        return properties.TryParse.Id(id, out Object? @enum) ? @enum : properties.Create.Id(id);
                    }
                    
                    throw new JsonException($"Unsupported value '{value}' for type '{type}' for {GetType()}.");
                }
                case JTokenType.Object when token is JObject jobject:
                {
                    NamingStrategy? strategy = serializer.GetNamingStrategy();

                    if (type.IsEnum)
                    {
                        token = jobject[strategy.NamingStrategy("Id", false)] ?? jobject[strategy.NamingStrategy("Title", false)] ?? throw new JsonException("Object must have 'Id' property.");
                        goto @switch;
                    }
                    
                    if (!EnumPropertiesCache.TryGetOrAdd(type, Properties, out EnumProperties? properties))
                    {
                        throw new JsonException($"Unsupported type '{type}' for {GetType()}.");
                    }

                    Object? result;
                    String? title = jobject[strategy.NamingStrategy("Title", false)]?.ToString();
                    if (jobject[strategy.NamingStrategy("Id", false)]?.ToObject(properties.Underlying) is Enum id)
                    {
                        if (properties.TryParse.Id(id, out result))
                        {
                            return title is null ? result : properties.GetTitle(result) == title ? result : properties.Create.Title(id, title);
                        }

                        return title is null ? properties.Create.Id(id) : properties.Create.Title(id, title);
                    }

                    if (title is null)
                    {
                        throw new JsonException($"Id is required for conversion to enum type '{properties.Underlying}'.");
                    }

                    if (properties.TryParse.Title(title, out result))
                    {
                        return result;
                    }

                    if (Enum.TryParse(properties.Underlying, title, true, out result) && (id = (result as Enum)!) is not null)
                    {
                        return properties.TryParse.Id(id, out result) ? result : properties.Create.Id(id);
                    }
                        
                    throw new JsonException($"Unsupported value '{title}' for enum type '{type}' for {GetType()}.");
                }
                default:
                {
                    throw new JsonException($"Expected {JTokenType.Integer}, {JTokenType.String} or {JTokenType.Object} token for conversion to enum type '{type}'.");
                }
            }
        }
    }
}