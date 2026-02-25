using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NetExtender.AspNetCore.Types.Providers
{
    public partial class SafeApiDescriptionProvider
    {
        protected readonly struct ApiParameterDescriptionContext
        {
            public BindingInfo? Binding { get; }
            public ModelMetadata Metadata { get; }
            public BindingSource? Source { get; }
            public String? Model { get; }
            public String? Property { get; }

            public ApiParameterDescriptionContext(ModelMetadata metadata, BindingInfo? binding, String? property)
            {
                Binding = binding;
                Metadata = metadata;
                Source = Binding?.BindingSource;
                Model = Binding?.BinderModelName;
                Property = property ?? Metadata.Name;
            }
        }

        protected class PseudoModelBindingVisitor
        {
            protected SafeApiDescriptionProvider Provider { get; }
            public ApiParameterContext Context { get; }
            public ParameterDescriptor Parameter { get; }
            protected HashSet<PropertyKey> VisitedKeys { get; } = new HashSet<PropertyKey>();
            protected HashSet<Type> VisitedTypes { get; } = new HashSet<Type>();

            public PseudoModelBindingVisitor(SafeApiDescriptionProvider provider, ApiParameterContext context, ParameterDescriptor parameter)
            {
                Provider = provider;
                Context = context;
                Parameter = parameter;
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            protected virtual PropertyInfo? GetProperty(ModelMetadata metadata)
            {
                if (String.IsNullOrEmpty(metadata.PropertyName))
                {
                    return null;
                }

                Type? container = metadata.ContainerType;

                start:
                if (container is not null)
                {
                    try
                    {
                        const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;
                        return container.GetProperty(metadata.PropertyName, binding);
                    }
                    catch (AmbiguousMatchException)
                    {
                        const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
                        PropertyInfo? property = container.GetProperty(metadata.PropertyName, binding);

                        if (property is not null)
                        {
                            return property;
                        }

                        Type? @base = container.BaseType;
                        while (@base is not null && @base != typeof(Object))
                        {
                            property = @base.GetProperty(metadata.PropertyName, binding);

                            if (property is not null)
                            {
                                return property;
                            }

                            @base = @base.BaseType;
                        }
                    }
                }

                if (metadata.ModelType == container)
                {
                    return null;
                }

                container = metadata.ModelType;
                goto start;
            }

            public void WalkParameter(ApiParameterDescriptionContext context)
            {
                Visit(context, BindingSource.ModelBinding, String.Empty);
            }

            protected virtual void Visit(ApiParameterDescriptionContext context, BindingSource binding, String container)
            {
                BindingSource? source = context.Source;

                if (source is { IsGreedy: true })
                {
                    Context.Results.Add(CreateResult(context, source, container));
                    return;
                }

                ModelMetadata metadata = context.Metadata;

                if (Provider.IsAtomicType(metadata.ModelType) || !metadata.IsComplexType || metadata.IsEnumerableType || metadata.Properties.Count <= 0 || !VisitedTypes.Add(metadata.ModelType))
                {
                    Context.Results.Add(CreateResult(context, source ?? binding, container));
                    return;
                }

                try
                {
                    String name = metadata.ContainerType is not null ? GetName(container, context) : container;

                    foreach (ModelMetadata property in metadata.Properties)
                    {
                        if (Provider.ShouldIgnoreProperty(property, GetProperty(property)))
                        {
                            continue;
                        }

                        PropertyKey key = new PropertyKey(property, source);
                        BindingInfo? info = BindingInfo.GetBindingInfo(Enumerable.Empty<Object>(), property);
                        ApiParameterDescriptionContext parameter = new ApiParameterDescriptionContext(property, info, null);

                        if (VisitedKeys.Add(key))
                        {
                            Visit(parameter, source ?? binding, name);
                            VisitedKeys.Remove(key);
                            continue;
                        }

                        IList<ApiParameterDescription> results = Context.Results;
                        ApiParameterDescription result = CreateResult(parameter, source ?? binding, name);
                        results.Add(result);
                    }
                }
                finally
                {
                    VisitedTypes.Remove(metadata.ModelType);
                }
            }

            protected ApiParameterDescription CreateResult(ApiParameterDescriptionContext context, BindingSource source, String container)
            {
                return new ApiParameterDescription
                {
                    ModelMetadata = context.Metadata,
                    Name = GetName(container, context),
                    Source = source,
                    Type = GetModelType(context.Metadata),
                    ParameterDescriptor = Parameter,
                    BindingInfo = context.Binding
                };
            }

            protected virtual Type GetModelType(ModelMetadata metadata)
            {
                return Provider.IsAtomicType(metadata.ModelType) || metadata.IsComplexType ? metadata.ModelType : GetDisplayType(metadata.ModelType);
            }

            protected virtual String GetName(String container, ApiParameterDescriptionContext metadata)
            {
                String? property = !String.IsNullOrEmpty(metadata.Model) ? metadata.Model : metadata.Property;
                return ModelNames.CreatePropertyModelName(container, property);
            }

            protected readonly struct PropertyKey : IEquatable<PropertyKey>
            {
                public readonly Type? ContainerType;
                public readonly String? PropertyName;
                public readonly BindingSource? Source;

                public PropertyKey(ModelMetadata metadata, BindingSource? source)
                {
                    ContainerType = metadata.ContainerType;
                    PropertyName = metadata.PropertyName;
                    Source = source;
                }

                public override Int32 GetHashCode()
                {
                    return HashCode.Combine(ContainerType, PropertyName, Source);
                }

                public override Boolean Equals(Object? other)
                {
                    return other is PropertyKey key && Equals(key);
                }

                public Boolean Equals(PropertyKey other)
                {
                    return ContainerType == other.ContainerType && PropertyName == other.PropertyName && Source == other.Source;
                }
            }
        }
    }
}