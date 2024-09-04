using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using NetExtender.Types.Entities;
using NetExtender.Types.Enums;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation.Types.ComboBoxes
{
    public class EnumComboBox : NullableComboBox
    {
        private delegate Object GetItemsSourceDelegate();
        private delegate void SetItemsSourceDelegate(EnumComboBox @object, Object value);

        private record Methods
        {
            public GetItemsSourceDelegate Get { get; }
            public SetItemsSourceDelegate Set { get; }

            public Methods(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }
                
                if (type.IsEnum)
                {
                    type = typeof(Enum<>).MakeGenericType(type);
                }

                if (!type.IsSuperclassOfRawGeneric(typeof(Enum<>)))
                {
                    throw new ArgumentException(null, nameof(type));
                }

                Get = CreateGet(type);
                Set = CreateSet(type);
            }

            private static GetItemsSourceDelegate CreateGet(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }
                
                const BindingFlags binding = BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static;
                MethodInfo? method = type.GetMethod(nameof(Enum<Any.Value>.Get), 0, binding, Type.EmptyTypes);

                if (method is null)
                {
                    throw new InvalidOperationException($"Method '{nameof(Enum<Any.Value>.Get)}' not found for type '{type}'.");
                }

                return DelegateUtilities.CreateDelegate<GetItemsSourceDelegate>(method);
            }

            private static SetItemsSourceDelegate CreateSet(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }
                
                const BindingFlags binding = BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Instance;
                MethodInfo? method = typeof(EnumComboBox).GetMethod(nameof(SetItemsSource), binding, new[] { typeof(Object) })?.MakeGenericMethod(type);
                
                if (method is null)
                {
                    throw new InvalidOperationException($"Method '{nameof(SetItemsSource)}' not found for type '{type}'.");
                }

                return DelegateUtilities.CreateDelegate<SetItemsSourceDelegate>(method);
            }
        }
        
        private static ConcurrentDictionary<Type, Methods> Storage { get; } = new ConcurrentDictionary<Type, Methods>();
        
        private readonly Type _type = typeof(Enum<Any.Value>);
        public Type Type
        {
            get
            {
                return _type;
            }
            init
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (!Storage.TryGetOrAdd(value, static type => new Methods(type), out _))
                {
                    throw new ArgumentException($"Type '{value}' is not supported.", nameof(value));
                }

                _type = value;
            }
        }

        public EnumComboBox()
        {
            Initialized += OnInitialized;
            HorizontalContentAlignment = HorizontalAlignment.Center;
            VerticalContentAlignment = VerticalAlignment.Center;
        }

        protected virtual void OnInitialized(Object? sender, EventArgs args)
        {
            SetItemsSource();
        }

        public virtual void SetItemsSource()
        {
            if (!Storage.TryGetValue(Type, out Methods? methods))
            {
                return;
            }

            methods.Set(this, methods.Get());
        }

        private void SetItemsSource<T>(Object? @object)
        {
            if (@object is not IEnumerable<T> source)
            {
                return;
            }
            
            SetItemsSource(source);
        }
    }
    
    public class EnumComboBox<T> : EnumComboBox where T : unmanaged, Enum
    {
        public new Type Type
        {
            get
            {
                return base.Type;
            }
            protected init
            {
                base.Type = value;
            }
        }
        
        public EnumComboBox()
        {
            base.Type = typeof(Enum<T>);
        }

        public override void SetItemsSource()
        {
            SetItemsSource(Enum<T>.Get());
        }

        public virtual void SetItemsSource(T nullable)
        {
            SetItemsSource((Enum<T>) nullable);
        }

        public virtual void SetItemsSource(Enum<T> nullable)
        {
            if (nullable is null)
            {
                throw new ArgumentNullException(nameof(nullable));
            }
            
            SetItemsSource(nullable, Enum<T>.Get());
        }

        public virtual void SetItemsSource(View<T>? nullable)
        {
            SetItemsSource(nullable is not null ? new View<Enum<T>>(nullable.Display, nullable.Value) : null);
        }

        public virtual void SetItemsSource(View<Enum<T>>? nullable)
        {
            SetItemsSource(nullable, Enum<T>.Get());
        }
    }

    public class EnumComboBox<T, TEnum> : EnumComboBox<T> where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
    {
        public EnumComboBox()
        {
            Type = typeof(TEnum);
        }
        
        public override void SetItemsSource()
        {
            SetItemsSource(Enum<T>.Get<TEnum>());
        }

        public override void SetItemsSource(T nullable)
        {
            SetItemsSource((TEnum) nullable);
        }

        public sealed override void SetItemsSource(Enum<T> nullable)
        {
            if (nullable is null)
            {
                throw new ArgumentNullException(nameof(nullable));
            }
            
            SetItemsSource(nullable.As<TEnum>());
        }

        public virtual void SetItemsSource(TEnum nullable)
        {
            if (nullable is null)
            {
                throw new ArgumentNullException(nameof(nullable));
            }
            
            SetItemsSource(nullable, Enum<T>.Get<TEnum>());
        }

        public override void SetItemsSource(View<T>? nullable)
        {
            SetItemsSource(nullable is not null ? new View<TEnum>(nullable.Display, (TEnum) nullable.Value) : null);
        }

        public sealed override void SetItemsSource(View<Enum<T>>? nullable)
        {
            if (nullable is null)
            {
                SetItemsSource();
                return;
            }

            SetItemsSource(new View<TEnum>(nullable.Display, nullable.Value?.As<TEnum>()!));
        }

        public virtual void SetItemsSource(View<TEnum>? nullable)
        {
            SetItemsSource(nullable, Enum<T>.Get<TEnum>());
        }
    }
}