using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Core
{
    public static class EventInfoUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsAbstract(this EventInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.AddMethod is { IsAbstract: true } || info.RemoveMethod is { IsAbstract: true };
        }

        public static IEnumerable<EventInfo> GetEvents(this Type type, Type attribute)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (EventInfo @event in type.GetEvents())
            {
                if (@event.HasAttribute(attribute))
                {
                    yield return @event;
                }
            }
        }

        public static IEnumerable<EventInfo> GetEvents(this Type type, Type attribute, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (EventInfo @event in type.GetEvents())
            {
                if (@event.HasAttribute(attribute, inherit))
                {
                    yield return @event;
                }
            }
        }

        public static IEnumerable<EventInfo> GetEvents(this Type type, Type attribute, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (EventInfo @event in type.GetEvents(binding))
            {
                if (@event.HasAttribute(attribute))
                {
                    yield return @event;
                }
            }
        }

        public static IEnumerable<EventInfo> GetEvents(this Type type, Type attribute, BindingFlags binding, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (EventInfo @event in type.GetEvents(binding))
            {
                if (@event.HasAttribute(attribute, inherit))
                {
                    yield return @event;
                }
            }
        }

        public static IEnumerable<EventInfo> GetEvents<TAttribute>(this Type type) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            EventInfo[] events = type.GetEvents();
            return events.Where(static @event => @event.HasAttribute<TAttribute>());
        }

        public static IEnumerable<EventInfo> GetEvents<TAttribute>(this Type type, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            EventInfo[] events = type.GetEvents();
            return inherit ? events.Where(static @event => @event.HasAttribute<TAttribute>(false)) : events.Where(static @event => @event.HasAttribute<TAttribute>(true));
        }

        public static IEnumerable<EventInfo> GetEvents<TAttribute>(this Type type, BindingFlags binding) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            EventInfo[] events = type.GetEvents(binding);
            return events.Where(static @event => @event.HasAttribute<TAttribute>());
        }

        public static IEnumerable<EventInfo> GetEvents<TAttribute>(this Type type, BindingFlags binding, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            EventInfo[] events = type.GetEvents(binding);
            return inherit ? events.Where(static @event => @event.HasAttribute<TAttribute>(false)) : events.Where(static @event => @event.HasAttribute<TAttribute>(true));
        }
    }
}