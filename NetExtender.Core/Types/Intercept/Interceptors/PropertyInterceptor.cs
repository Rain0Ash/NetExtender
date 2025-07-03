// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Intercept.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Intercept
{
    public class PropertyInterceptor<TSender, TInfo> : PropertyInterceptor<TSender, IPropertyInterceptEventArgs, TInfo>, IPropertyInterceptor<TSender, TInfo> where TSender : IInterceptTargetRaise<IPropertyInterceptEventArgs>
    {
        public static PropertyInterceptor<TSender, TInfo> Default { get; } = new PropertyInterceptor<TSender, TInfo>();
        
        public PropertyInterceptor()
        {
            Factory = PropertyInterceptorUtilities.Factory<TInfo>.Instance;
        }
    }

    public class PropertyInterceptor<TSender, TArgument, TInfo> : MemberInterceptor<TSender, TArgument, PropertyInfo>, IPropertyInterceptor<TSender, TArgument, TInfo> where TSender : IInterceptTargetRaise<TArgument> where TArgument : class, IPropertyInterceptEventArgs
    {
        public IPropertyInterceptEventArgsFactory<TArgument, TInfo>? Factory { get; init; }

        public T InterceptGet<T>(TSender sender, [CallerMemberName] String? property = null)
        {
            return InterceptGet<T>(sender, default(TInfo), property);
        }

        public virtual T InterceptGet<T>(TSender sender, TInfo? info, [CallerMemberName] String? property = null)
        {
            return InterceptGet<T>(sender, info, true, property);
        }

        public T InterceptGet<T>(TSender sender, Boolean @base, [CallerMemberName] String? property = null)
        {
            return InterceptGet<T>(sender, default, @base, property);
        }

        public virtual T InterceptGet<T>(TSender sender, TInfo? info, Boolean @base, [CallerMemberName] String? property = null)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            if (PropertyInterceptorUtilities<TSender>.Get<T>(property, @base) is not { } member)
            {
                throw new MissingMemberException(typeof(TSender).Name, property);
            }

            if (member.Getter is null)
            {
                throw new NotSupportedException();
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<T>(member.Property, PropertyInterceptAccessor.Get, info);
                return InterceptGet<T>(sender, args);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        // ReSharper disable once CognitiveComplexity
        public virtual T InterceptGet<T>(TSender sender, TArgument args)
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (args is not IPropertyInterceptEventArgs<T> argument)
            {
                throw new TypeNotSupportedException(typeof(TArgument));
            }

            if (PropertyInterceptorUtilities<TSender>.Get<T>(args.Member, false)?.Getter is not { } getter)
            {
                throw new NotSupportedException();
            }

            args.Token.ThrowIfCancellationRequested();
            sender.RaiseIntercepting(args);
            
            if (args.Exception is not null)
            {
                throw args.Exception;
            }
            
            if (args.IsSeal)
            {
                return argument.Value;
            }

            Maybe<T> result = default;

            try
            {
                result = getter(sender);
            }
            catch (Exception exception)
            {
                args.Intercept(exception);
                sender.RaiseIntercepted(args);

                if (ReferenceEquals(args.Exception, exception))
                {
                    throw;
                }
            }

            if (result.HasValue)
            {
                argument.Intercept(result.Value);
                sender.RaiseIntercepted(args);
            }

            if (args.Exception is not null)
            {
                throw args.Exception;
            }

            return argument.Value;
        }

        public void InterceptSet<T>(TSender sender, T value, String? property = null)
        {
            InterceptSet(sender, default, value, property);
        }

        public void InterceptSet<T>(TSender sender, TInfo? info, T value, String? property = null)
        {
            InterceptSet(sender, info, value, true, property);
        }

        public void InterceptSet<T>(TSender sender, T value, Boolean @base, String? property = null)
        {
            InterceptSet(sender, default, value, @base, property);
        }

        public void InterceptSet<T>(TSender sender, TInfo? info, T value, Boolean @base, String? property = null)
        {
            InterceptSet(sender, info, value, true, false, property);
        }

        public virtual void InterceptSet<T>(TSender sender, T value, Boolean @base, Boolean init, String? property = null)
        {
            InterceptSet(sender, default, value, @base, init, property);
        }

        public virtual void InterceptSet<T>(TSender sender, TInfo? info, T value, Boolean @base, Boolean init, String? property = null)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            if (PropertyInterceptorUtilities<TSender>.Get<T>(property, @base) is not { } member)
            {
                throw new MissingMemberException(typeof(TSender).Name, property);
            }

            if (member.Setter is null)
            {
                throw new NotSupportedException();
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create(member.Property, init ? PropertyInterceptAccessor.Init : PropertyInterceptAccessor.Set, value, info);
                InterceptSet(sender, value, args);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public void InterceptInit<T>(TSender sender, T value, String? property = null)
        {
            InterceptInit(sender, default, value, property);
        }

        public void InterceptInit<T>(TSender sender, TInfo? info, T value, String? property = null)
        {
            InterceptInit(sender, info, value, true, property);
        }

        public void InterceptInit<T>(TSender sender, T value, Boolean @base, String? property = null)
        {
            InterceptInit(sender, default, value, @base, property);
        }

        public void InterceptInit<T>(TSender sender, TInfo? info, T value, Boolean @base, String? property = null)
        {
            InterceptSet(sender, info, value, @base, true, property);
        }

        // ReSharper disable once CognitiveComplexity
        public virtual void InterceptSet<T>(TSender sender, T value, TArgument args)
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (args is not IPropertyInterceptEventArgs<T> argument)
            {
                throw new TypeNotSupportedException(typeof(TArgument));
            }

            if (PropertyInterceptorUtilities<TSender>.Get<T>(args.Member, false)?.Setter is not { } setter)
            {
                throw new NotSupportedException();
            }

            args.Token.ThrowIfCancellationRequested();
            sender.RaiseIntercepting(args);

            if (args.IsIgnore)
            {
                return;
            }

            if (args.Exception is not null)
            {
                throw args.Exception;
            }
            
            if (args.IsSeal)
            {
                value = argument.Value;
            }

            Boolean successful = true;

            try
            {
                setter(sender, value);
            }
            catch (Exception exception)
            {
                successful = false;
                args.Intercept(exception);
                sender.RaiseIntercepted(args);

                if (ReferenceEquals(args.Exception, exception))
                {
                    throw;
                }
            }

            if (successful)
            {
                argument.Intercept(value);
                sender.RaiseIntercepted(args);
            }

            if (args.IsIgnore)
            {
                return;
            }

            if (args.Exception is not null)
            {
                throw args.Exception;
            }
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    internal static class PropertyInterceptorUtilities<TSender> where TSender : notnull
    {
        internal record Info<T> : Info
        {
            private static ConcurrentDictionary<String, Info<T>?> Storage { get; } = new ConcurrentDictionary<String, Info<T>?>();
            
            public Func<TSender, T>? Getter { get; }
            public Action<TSender, T>? Setter { get; }

            public Info(PropertyInfo property, Boolean @base)
                : base(property)
            {
                if (@base)
                {
                    (Getter, Setter) = property.GetParentProperty<TSender, T>();
                }
                else
                {
                    Getter = property.TryCreateGetExpression<TSender, T>(out Expression<Func<TSender, T>>? get) ? get.Compile() : null;
                    Setter = property.TryCreateSetExpression<TSender, T>(out Expression<Action<TSender, T>>? set) ? set.Compile() : null;
                }
            }
            
            public static Info<T>? Get(String property, Boolean @base)
            {
                if (property is null)
                {
                    throw new ArgumentNullException(nameof(property));
                }

                return Storage.GetOrAdd(property, static (property, @base) => PropertyInterceptorUtilities<TSender>.Get(property) is { } value ? new Info<T>(value, @base) : null, @base);
            }
            
            public static Info<T>? Get(PropertyInfo property, Boolean @base)
            {
                if (property is null)
                {
                    throw new ArgumentNullException(nameof(property));
                }

                return Storage.GetOrAdd(property.Name, static (_, property) => new Info<T>(property.Property, property.Base), (Property: property, Base: @base));
            }
        }
        
        internal record Info
        {
            public PropertyInfo Property { get; }
            
            public Info(PropertyInfo property)
            {
                Property = property ?? throw new ArgumentNullException(nameof(property));
            }
        }
        
        private static ConcurrentDictionary<String, PropertyInfo?> Storage { get; } = new ConcurrentDictionary<String, PropertyInfo?>();

        internal static PropertyInfo? Get(String property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            return Storage.GetOrAdd(property, static property => typeof(TSender).GetProperty(property, binding));
        }

        internal static Info<T>? Get<T>(String property, Boolean @base)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return Info<T>.Get(property, @base);
        }

        internal static Info<T>? Get<T>(PropertyInfo property, Boolean @base)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return Info<T>.Get(property, @base);
        }
    }

    internal static class PropertyInterceptorUtilities
    {
        internal sealed class Factory<TInfo> : PropertyInterceptEventArgsFactory<IPropertyInterceptEventArgs, TInfo>
        {
            public static Factory<TInfo> Instance { get; } = new Factory<TInfo>();

            private Factory()
            {
            }
            
            public override IPropertyInterceptEventArgs Create<T>(PropertyInfo property, PropertyInterceptAccessor accessor, TInfo? info)
            {
                return new PropertyInterceptEventArgs<T>(property, accessor);
            }

            public override IPropertyInterceptEventArgs Create<T>(PropertyInfo property, PropertyInterceptAccessor accessor, T value, TInfo? info)
            {
                return new PropertyInterceptEventArgs<T>(property, accessor, value);
            }

            public override IPropertyInterceptEventArgs Create<T>(PropertyInfo property, PropertyInterceptAccessor accessor, Exception exception, TInfo? info)
            {
                return new PropertyInterceptEventArgs<T>(property, accessor, exception);
            }
        }
    }
}