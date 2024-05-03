using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NetExtender.Types.Culture;
using NetExtender.Types.Entities;
using NetExtender.Types.Enums;
using NetExtender.Types.Reflection;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Core;
using Newtonsoft.Json;

namespace NetExtender.Utilities.Types
{
    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public static partial class EnumUtilities
    {
        public class Properties
        {
            private static ConcurrentDictionary<Type, Properties> Cache { get; } = new ConcurrentDictionary<Type, Properties>();

            public Type Type { get; }
            public Type Underlying { get; }
            public Func<Object, Object> GetId { get; }
            public Func<Object, String> GetTitle { get; }
            public Func<Object, LocalizationIdentifier?> GetIdentifier { get; }
            public ImmutableArray<IReflectionProperty> Others { get; }
            public CreateMethod Create { get; }
            public TryParseMethod TryParse { get; }

            private Properties(Type type)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));

                Type[]? arguments = type.GetGenericArguments(typeof(Enum<,>)) ?? type.GetGenericArguments(typeof(Enum<>));

                if (arguments is null || arguments.Length <= 0)
                {
                    throw new ArgumentException($"Can't get generic arguments of '{type}'.", nameof(type));
                }

                Underlying = arguments[0];
                PropertyInfo id = Property(type, nameof(Enum<Any.Value>.Id));
                PropertyInfo title = Property(type, nameof(Enum<Any.Value>.Title));
                PropertyInfo identifier = Property(type, nameof(Enum<Any.Value>.Identifier));
                GetId = CreateGetExpression<Object>(type, id).Compile();
                GetTitle = CreateGetExpression<String>(type, title).Compile();
                GetIdentifier = CreateGetExpression<LocalizationIdentifier?>(type, identifier).Compile();

                Boolean IsProperty(PropertyInfo property)
                {
                    return property.CanRead && property != id && property != title && property != identifier && !property.HasAttribute<JsonIgnoreAttribute>();
                }

                IReflectionProperty Convert(PropertyInfo property)
                {
                    return new ReflectionProperty(property);
                }

                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;
                Others = type.GetProperties(binding).Where(IsProperty).Select(Convert).ToImmutableArray();
                Create = new CreateMethod(type);
                TryParse = new TryParseMethod(type);
            }

            public static Boolean Contains(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                return Cache.ContainsKey(type);
            }

            public static Properties Factory(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                return Cache.GetOrAdd(type, static type => new Properties(type));
            }

            public static Boolean TryFactory(Type type, [MaybeNullWhen(false)] out Properties result)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                try
                {
                    result = Factory(type);
                    return true;
                }
                catch (Exception)
                {
                    result = default;
                    return false;
                }
            }

            private static PropertyInfo Property(Type type, String name)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public;
                return type.GetProperty(name, binding) ?? throw new InvalidOperationException($"Type '{type}' doesn't contains property '{name}'.");
            }
            
            private static Boolean TryGetGenericArguments(Type type, [MaybeNullWhen(false)] out Type underlying, [MaybeNullWhen(false)] out Type @enum)
            {
                return TryGetGenericArguments(type, out underlying, out @enum, out _);
            }

            // ReSharper disable once OutParameterValueIsAlwaysDiscarded.Local
            private static Boolean TryGetGenericArguments(Type type, [MaybeNullWhen(false)] out Type underlying, [MaybeNullWhen(false)] out Type @enum,
                [MaybeNullWhen(false)] out Type[] arguments)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                arguments = type.GetGenericArguments(typeof(Enum<,>)) ?? type.GetGenericArguments(typeof(Enum<>));

                if (arguments is not null && arguments.Length > 0)
                {
                    underlying = arguments[0];
                    @enum = arguments.Length > 1 ? arguments[1] : type;
                    return true;
                }

                arguments = default;
                underlying = default;
                @enum = default;
                return false;
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

            public ImmutableArray<IReflectionProperty>.Enumerator GetEnumerator()
            {
                return Others.GetEnumerator();
            }

            public override String ToString()
            {
                return Type.Name;
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

                    if (!TryGetGenericArguments(type, out Type? underlying, out Type? @enum))
                    {
                        throw new ArgumentException($"Can't get generic arguments of '{type}'.", nameof(type));
                    }

                    static MethodInfo? GetMethod(Type type, Type underlying, Type @enum, params Type[] types)
                    {
                        if (type is null)
                        {
                            throw new ArgumentNullException(nameof(type));
                        }

                        const BindingFlags binding = BindingFlags.Static | BindingFlags.Public |
                                                     BindingFlags.FlattenHierarchy;
                        Int32? generic = type.IsSuperclassOfRawGeneric(typeof(Enum<,>)) ? 0 : type.IsSuperclassOfRawGeneric(typeof(Enum<>)) ? 1 : null;

                        return generic switch
                        {
                            null => null,
                            0 => @enum.GetMethod("Create", 0, binding, types),
                            1 => @enum.GetMethod("Create", 1, binding, types)?.MakeGenericMethod(type),
                            _ => throw new NotSupportedException($"Type '{type}' with underlying '{underlying}' and enum type '{@enum}' is not supported for get method 'Create'.")
                        };
                    }

                    MethodInfo? method = GetMethod(type, underlying, @enum, underlying);

                    if (method is null)
                    {
                        throw new InvalidOperationException($"Type '{@enum}' doesn't contain method 'Create({underlying.Name} value)'.");
                    }

                    ParameterExpression value = Expression.Parameter(typeof(Enum), nameof(value));
                    UnaryExpression convert = Expression.Convert(value, underlying);
                    MethodCallExpression call = Expression.Call(null, method, convert);
                    UnaryExpression result = Expression.Convert(call, typeof(Object));
                    Expression<Func<Enum, Object>> enumlambda = Expression.Lambda<Func<Enum, Object>>(result, value);

                    method = GetMethod(type, underlying, @enum, underlying, typeof(String));

                    if (method is null)
                    {
                        throw new InvalidOperationException($"Type '{@enum}' doesn't contain method 'Create({underlying.Name} value, {nameof(String)} title)'.");
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

                    if (!TryGetGenericArguments(type, out Type? underlying, out Type? @enum))
                    {
                        throw new ArgumentException($"Can't get generic arguments of '{type}'.", nameof(type));
                    }

                    static MethodInfo? GetMethod(Type type, Type underlying, Type @enum, params Type[] types)
                    {
                        if (type is null)
                        {
                            throw new ArgumentNullException(nameof(type));
                        }

                        const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy;
                        Int32? generic = type.IsSuperclassOfRawGeneric(typeof(Enum<,>)) ? 0 : type.IsSuperclassOfRawGeneric(typeof(Enum<>)) ? 1 : null;

                        return generic switch
                        {
                            null => null,
                            0 => @enum.GetMethod(nameof(System.Enum.TryParse), 0, binding, types),
                            1 => @enum.GetMethod(nameof(Enum.TryParse), binding, new[] { type }, types),
                            _ => throw new NotSupportedException($"Type '{type}' with underlying '{underlying}' and enum type '{@enum}' is not supported for get method '{nameof(System.Enum.TryParse)}'.")
                        };
                    }

                    underlying = typeof(TInput) == typeof(String) ? typeof(String) : underlying;
                    MethodInfo? method = GetMethod(type, underlying, @enum, underlying, @enum.MakeByRefType());

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
    }
}