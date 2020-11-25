// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Reflection;
using NetExtender.Utils.IO;
using NetExtender.Converters;
using NetExtender.Messages.Rules.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Messages.Rules.Strings
{
    public class TypeCommandRule<T> : StringNestedCommandRule
    {
        public Dictionary<Type, TryConverter<IReadOnlyList<String>, Object>> ValueConverters { get; } = new Dictionary<Type, TryConverter<IReadOnlyList<String>, Object>>
        {
            {typeof(String), (IReadOnlyList<String> value, out Object converted) =>
            {
                converted = value.Join();
                return true;
            }},
            {typeof(Boolean), (IReadOnlyList<String> value, out Object converted) =>
            {
                converted = value[0].ToBoolean();
                return true;
            }}
        };
        
        public Dictionary<Type, TryConverter<Object, String>> StringConverters { get; } = new Dictionary<Type, TryConverter<Object, String>>
        {
        };
        
        protected readonly T Value;

        public TypeCommandRule(T value, ReaderHandler<String> handler = null)
            : this(value?.GetType().Name ?? typeof(T).Name, value, handler)
        {
        }
        
        public TypeCommandRule(String id, T value, ReaderHandler<String> handler = null)
            : base(id, handler)
        {
            Value = value;

            Initialize(typeof(T) == typeof(Type) ? value as Type : value?.GetType() ?? typeof(T));
        }

        protected virtual void Initialize(Type type)
        {
            foreach (PropertyInfo info in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                INestedCommandRule<String> rule = new NestedCommandRule<String>(info.Name, DefaultHandler);
                
                rule.AddRule(GetReadCommand(info));

                rule.AddRule(GetWriteCommand(info));

                AddRule(rule);
            }
        }

        protected virtual ICommandRule<String> GetReadCommand(PropertyInfo info)
        {
            return new CommandRule<String>("get", (rule, args) =>
            {
                try
                {
                    if (Value is null)
                    {
                        return TaskUtils.False;
                    }

                    Object value = info.GetValue(Value);

                    if (value is null)
                    {
                        return TaskUtils.True;
                    }

                    if (StringConverters.TryGetValue(value.GetType(), out TryConverter<Object, String> converter))
                    {
                        if (converter is not null && converter.Invoke(value, out String converted))
                        {
                            converted.ToConsole(ConsoleColor.Green);
                            return TaskUtils.True;
                        }
                    }
                    
                    value.GetString().ToConsole(ConsoleColor.Green);
                }
                catch (Exception e)
                {
                    e.ToConsole(ConsoleColor.Red);
                }
                
                return TaskUtils.True;
            });
        }

        protected virtual ICommandRule<String> GetWriteCommand(PropertyInfo info)
        {
            return new CommandRule<String>("set", (rule, args) =>
            {
                try
                {
                    if (Value is null)
                    {
                        return TaskUtils.False;
                    }

                    Object converted = null;
                    
                    try
                    {
                        converted = args.Join().ToBytes().Deserialize();
                    }
                    catch(Exception)
                    {
                        // ignore
                    }
                    

                    if (converted is not null)
                    {
                        info.SetValue(Value, converted);
                        return TaskUtils.True;
                    }
                    
                    if (!ValueConverters.TryGetValue(info.PropertyType, out TryConverter<IReadOnlyList<String>, Object> converter))
                    {
                        throw new NotSupportedException();
                    }

                    if (converter is null || !converter.Invoke(args, out converted))
                    {
                        throw new NotSupportedException();
                    }

                    info.SetValue(Value, converted);
                    return TaskUtils.True;

                }
                catch (Exception e)
                {
                    e.ToConsole(ConsoleColor.Red);
                }
                
                return TaskUtils.True;
            });
        }
    }
}