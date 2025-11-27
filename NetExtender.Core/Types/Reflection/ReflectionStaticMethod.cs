using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Reflection
{
    //TODO:
    /*public class ReflectionStaticMethod<TSelf, TGeneric> : ReflectionStaticMethod<TGeneric> where TGeneric : Delegate
    {
        public sealed override Type Type
        {
            get
            {
                return typeof(TSelf);
            }
        }

        protected ReflectionStaticMethod(TGeneric @delegate)
            : base(typeof(TSelf), @delegate)
        {
        }
        
        internal static ReflectionStaticMethod<TSelf, TGeneric>? Create(TGeneric @delegate)
        {
            try
            {
                return new ReflectionStaticMethod<TSelf, TGeneric>(@delegate);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    //TODO:
    public class ReflectionStaticMethod<TGeneric> : ReflectionStaticMethod where TGeneric : Delegate
    {
        public override Type Type { get; }
        protected sealed override MethodInfo Method { get; }
        
        private readonly Dictionary<Int32, Int32> _parameterMapping;
        private readonly Int32? _returnTypeMapping;

        protected ReflectionStaticMethod(Type type, TGeneric @delegate)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            MethodInfo delegateMethod = @delegate.Method;
            if (!delegateMethod.IsStatic)
            {
                throw new ArgumentException("Delegate method must be static", nameof(@delegate));
            }

            // Анализируем сигнатуру делегата для определения позиций Any/Any.Value
            ParameterInfo[] delegateParameters = delegateMethod.GetParameters();
            Type[] delegateParameterTypes = delegateParameters.Select(p => p.ParameterType).ToArray();
            Type delegateReturnType = delegateMethod.ReturnType;

            // Ищем generic методы с подходящим именем
            const BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Public | 
                                             BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
            
            MethodInfo[] methods = type.GetMethods(bindingFlags)
                .Where(m => m.IsGenericMethodDefinition && 
                           m.Name.Equals(delegateMethod.Name, StringComparison.Ordinal))
                .ToArray();

            if (methods.Length == 0)
            {
                throw new InvalidOperationException($"No generic method '{delegateMethod.Name}' found in type {type}");
            }

            // Ищем метод с совместимой сигнатурой
            MethodInfo? foundMethod = null;
            Dictionary<Int32, Int32> parameterMapping = new Dictionary<Int32, Int32>();
            Int32? returnMapping = null;

            foreach (MethodInfo method in methods)
            {
                parameterMapping.Clear();
                returnMapping = null;

                Type[] genericParams = method.GetGenericArguments();
                ParameterInfo[] methodParameters = method.GetParameters();
                
                if (methodParameters.Length != delegateParameters.Length)
                {
                    continue;
                }

                Boolean signatureMatch = true;

                // Сопоставляем параметры
                for (Int32 i = 0; i < methodParameters.Length; i++)
                {
                    Type methodParamType = methodParameters[i].ParameterType;
                    Type delegateParamType = delegateParameterTypes[i];

                    if (IsAnyType(delegateParamType))
                    {
                        // В делегате Any - должен быть generic параметр в методе
                        if (methodParamType.IsGenericParameter)
                        {
                            Int32 genericIndex = Array.IndexOf(genericParams, methodParamType);
                            if (genericIndex >= 0)
                            {
                                parameterMapping[i] = genericIndex;
                            }
                            else
                            {
                                signatureMatch = false;
                                break;
                            }
                        }
                        else
                        {
                            signatureMatch = false;
                            break;
                        }
                    }
                    else
                    {
                        // Конкретный тип - должен точно совпадать
                        if (methodParamType != delegateParamType)
                        {
                            signatureMatch = false;
                            break;
                        }
                    }
                }

                if (!signatureMatch)
                {
                    continue;
                }

                // Сопоставляем возвращаемый тип
                if (delegateReturnType.IsAnyType())
                {
                    if (method.ReturnType.IsGenericParameter)
                    {
                        Int32 genericIndex = Array.IndexOf(genericParams, method.ReturnType);
                        if (genericIndex >= 0)
                        {
                            returnMapping = genericIndex;
                        }
                        else
                        {
                            signatureMatch = false;
                        }
                    }
                    else
                    {
                        signatureMatch = false;
                    }
                }
                else
                {
                    if (method.ReturnType != delegateReturnType)
                    {
                        signatureMatch = false;
                    }
                }

                if (signatureMatch)
                {
                    foundMethod = method;
                    break;
                }
            }

            Method = foundMethod ?? throw new InvalidOperationException($"No compatible generic method found for signature of {typeof(TGeneric)}");
            _parameterMapping = new Dictionary<Int32, Int32>(parameterMapping);
            _returnTypeMapping = returnMapping;
        }
        
        internal static ReflectionStaticMethod<TGeneric>? Create(Type type, TGeneric @delegate)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            try
            {
                return new ReflectionStaticMethod<TGeneric>(type, @delegate);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public sealed override TDelegate Create<TDelegate>()
        {
            return Storage<TGeneric, TDelegate>.Get(this);
        }

        protected sealed override TDelegate New<TDelegate>()
        {
            MethodInfo? delegateMethod = typeof(TDelegate).GetMethod("Invoke");
            if (delegateMethod is null)
            {
                return null!;
            }

            ParameterInfo[] delegateParameters = delegateMethod.GetParameters();
            Type delegateReturnType = delegateMethod.ReturnType;

            // Проверяем совместимость сигнатур (игнорируя Any типы)
            if (!AreSignaturesCompatible(delegateMethod))
            {
                return null!;
            }

            // Определяем типы для generic параметров
            Type[] genericParameters = Method.GetGenericArguments();
            Type[] typeArguments = new Type[genericParameters.Length];
            
            // Инициализируем неопределенные параметры
            for (Int32 i = 0; i < typeArguments.Length; i++)
            {
                typeArguments[i] = typeof(void); // Маркер неопределенности
            }

            // Заполняем типы из параметров делегата
            foreach ((Int32 paramIndex, Int32 genericIndex) in _parameterMapping)
            {
                Type concreteType = delegateParameters[paramIndex].ParameterType;
                
                if (typeArguments[genericIndex] != typeof(void) && 
                    typeArguments[genericIndex] != concreteType)
                {
                    // Конфликт типов - один generic параметр получает разные типы
                    return null!;
                }
                
                typeArguments[genericIndex] = concreteType;
            }

            // Заполняем тип из возвращаемого значения
            if (_returnTypeMapping.HasValue)
            {
                Type concreteReturnType = delegateReturnType;
                Int32 genericIndex = _returnTypeMapping.Value;
                
                if (typeArguments[genericIndex] != typeof(void) && 
                    typeArguments[genericIndex] != concreteReturnType)
                {
                    // Конфликт типов
                    return null!;
                }
                
                typeArguments[genericIndex] = concreteReturnType;
            }

            // Проверяем, что все generic параметры определены
            if (typeArguments.Any(t => t == typeof(void)))
            {
                return null!;
            }

            MethodInfo specializedMethod = Method.MakeGenericMethod(typeArguments);
            return (TDelegate) Delegate.CreateDelegate(typeof(TDelegate), null, specializedMethod);
        }

        private Boolean AreSignaturesCompatible(MethodInfo method)
        {
            ParameterInfo[] genericParams = Method.GetParameters();
            ParameterInfo[] delegateParams = method.GetParameters();

            if (genericParams.Length != delegateParams.Length)
            {
                return false;
            }

            for (Int32 i = 0; i < genericParams.Length; i++)
            {
                Type genericParamType = genericParams[i].ParameterType;
                Type delegateParamType = delegateParams[i].ParameterType;

                if (genericParamType.IsGenericParameter)
                {
                    continue;
                }

                if (genericParamType != delegateParamType)
                {
                    return false;
                }
            }

            Type @return = Method.ReturnType;
            return @return.IsGenericParameter || @return == method.ReturnType;
        }
    }

    public abstract class ReflectionStaticMethod
    {
        protected static class Storage<TGeneric, TDelegate> where TGeneric : Delegate where TDelegate : Delegate
        {
            private static ConditionalWeakTable<ReflectionStaticMethod<TGeneric>, TDelegate> Container { get; } = new ConditionalWeakTable<ReflectionStaticMethod<TGeneric>, TDelegate>();

            public static TDelegate Get(ReflectionStaticMethod<TGeneric> @this)
            {
                return Container.GetValue(@this, static @this => @this.New<TDelegate>());
            }
        }
        
        public abstract Type Type { get; }
        protected abstract MethodInfo Method { get; }

        public abstract TDelegate Create<TDelegate>() where TDelegate : Delegate;
        protected abstract TDelegate New<TDelegate>() where TDelegate : Delegate;
        
        public static ReflectionStaticMethod<TGeneric>? Create<TGeneric>(Type type, TGeneric @delegate) where TGeneric : Delegate
        {
            return ReflectionStaticMethod<TGeneric>.Create(type, @delegate);
        }
        
        public static ReflectionStaticMethod<TGeneric>? Create<TSelf, TGeneric>(TGeneric @delegate) where TGeneric : Delegate
        {
            return ReflectionStaticMethod<TSelf, TGeneric>.Create(@delegate);
        }
    }*/
}