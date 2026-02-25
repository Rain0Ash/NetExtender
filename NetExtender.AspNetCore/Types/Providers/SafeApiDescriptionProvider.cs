using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetExtender.Types.Enums;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.AspNetCore.Types.Providers
{
    public partial class SafeApiDescriptionProvider : IApiDescriptionProvider
    {
        private delegate Func<ControllerActionDescriptor, ICollection<ApiResponseType>> ApiResponseTypeDelegate(IModelMetadataProvider provider, IActionResultTypeMapper mapper, MvcOptions options);

        [ReflectionNaming]
        protected static Func<Type, Type> GetDisplayType { get; }

        [ReflectionNaming]
        protected static TryParseHandler<ParameterInfo, Object> TryGetDeclaredParameterDefaultValue { get; }

        private static ApiResponseTypeDelegate CreateApiResponseType { get; }

        static SafeApiDescriptionProvider()
        {
            {
                const String name = "_responseTypeProvider";
                Type type = typeof(DefaultApiDescriptionProvider).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic)?.FieldType ?? throw new MissingFieldException(nameof(DefaultApiDescriptionProvider), name);
                MethodInfo method = type.GetMethod(nameof(GetApiResponseTypes), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, new[] { typeof(ControllerActionDescriptor) }) ?? throw new MissingMethodException(nameof(GetApiResponseTypes), type.FullName);

                CreateApiResponseType = (provider, mapper, options) =>
                {
                    Object instance = Activator.CreateInstance(type, provider, mapper, options) ?? throw new InvalidOperationException($"Unable to create '{type.Name}'.");
                    return method.CreateDelegate<Func<ControllerActionDescriptor, ICollection<ApiResponseType>>>(instance);
                };
            }

            const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            {
                Assembly assembly = typeof(DefaultApiDescriptionProvider).Assembly;

                const String typename = "Microsoft.AspNetCore.Mvc.ApiExplorer.EndpointModelMetadata";
                Type type = assembly.GetType(typename) ?? throw new TypeLoadException(typename);
                MethodInfo? method = type.GetMethod(nameof(GetDisplayType), binding, new[] { typeof(Type) });

                Func<Type, Type>? @delegate = method?.CreateDelegate<Func<Type, Type>>();
                GetDisplayType = @delegate ?? (static type =>
                {
                    Type underlying = Nullable.GetUnderlyingType(type) ?? type;

                    return underlying.IsPrimitive ||
                           underlying == typeof(DateTime) || underlying == typeof(DateTimeOffset) || underlying == typeof(DateOnly) ||
                           underlying == typeof(TimeOnly) || underlying == typeof(TimeSpan) ||
                           underlying == typeof(Decimal) || underlying == typeof(Guid) ||
                           underlying == typeof(Uri) ? type : typeof(String);
                });
            }

            {
                Assembly assembly = typeof(ControllerActionDescriptor).Assembly;

                const String typename = "Microsoft.AspNetCore.Mvc.Infrastructure.ParameterDefaultValues";
                Type type = assembly.GetType(typename) ?? throw new TypeLoadException(typename);
                MethodInfo? method = type.GetMethod(nameof(TryGetDeclaredParameterDefaultValue), binding, new[] { typeof(ParameterInfo), typeof(Object).MakeByRefType() });

                TryParseHandler<ParameterInfo, Object>? @delegate = method?.CreateDelegate<TryParseHandler<ParameterInfo, Object>>();
                TryGetDeclaredParameterDefaultValue = @delegate ?? throw new MissingMethodException(nameof(TryGetDeclaredParameterDefaultValue), type.FullName);
            }
        }

        private static ServiceDescriptor? service;
        public static ServiceDescriptor ServiceDescriptor
        {
            get
            {
                return service ?? ServiceDescriptor.Transient<IApiDescriptionProvider, SafeApiDescriptionProvider>();
            }
            set
            {
                service = value;
            }
        }

        protected IInlineConstraintResolver ConstraintResolver { get; }
        protected IModelMetadataProvider MetadataProvider { get; }
        protected RouteOptions Route { get; }
        protected MvcOptions Options { get; }

        public Int32 Order
        {
            get
            {
                return -1000;
            }
        }

        [ReflectionNaming]
        protected Func<ControllerActionDescriptor, ICollection<ApiResponseType>> GetApiResponseTypes { get; }

        public static Boolean DefaultExcludeJsonIgnoreAttribute { get; set; }

        private readonly Boolean? _ignore;
        public Boolean ExcludeJsonIgnoreAttribute
        {
            get
            {
                return _ignore ?? DefaultExcludeJsonIgnoreAttribute;
            }
            init
            {
                _ignore = value;
            }
        }

        public SafeApiDescriptionProvider(IInlineConstraintResolver resolver, IModelMetadataProvider provider, IActionResultTypeMapper mapper, IOptions<RouteOptions> route, IOptions<MvcOptions> options)
        {
            if (route is null)
            {
                throw new ArgumentNullException(nameof(route));
            }

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            ConstraintResolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            MetadataProvider = provider ?? throw new ArgumentNullException(nameof(provider));
            Route = route.Value;
            Options = options.Value;

            GetApiResponseTypes = CreateApiResponseType(MetadataProvider, mapper, Options);
        }

        public virtual void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            foreach (ControllerActionDescriptor descriptor in context.Actions.OfType<ControllerActionDescriptor>())
            {
                if (descriptor.AttributeRouteInfo is { SuppressPathMatching: true } || descriptor.GetProperty<ApiDescriptionActionData>() is not { } property)
                {
                    continue;
                }

                foreach (String method in GetHttpMethods(descriptor))
                {
                    context.Results.Add(CreateApiDescription(descriptor, method, GetGroupName(descriptor, property)));
                }
            }
        }

        public virtual void OnProvidersExecuted(ApiDescriptionProviderContext context)
        {
        }

        protected virtual ApiDescription CreateApiDescription(ControllerActionDescriptor descriptor, String method, String? group)
        {
            RouteTemplate? template = ParseTemplate(descriptor);
            ApiDescription description = new ApiDescription
            {
                ActionDescriptor = descriptor,
                GroupName = group,
                HttpMethod = method,
                RelativePath = GetRelativePath(template)
            };

            List<TemplatePart> templates = template?.Parameters is { Count: > 0 } exist ? exist.ToList() : new List<TemplatePart>();

            description.ParameterDescriptions.AddRange(GetParameters(new ApiParameterContext(MetadataProvider, descriptor, templates)));
            description.SupportedResponseTypes.AddRange(GetApiResponseTypes(descriptor));

            if (description.ParameterDescriptions.Count <= 0)
            {
                return description;
            }

            IAcceptsMetadata? metadata = descriptor.EndpointMetadata.OfType<IAcceptsMetadata>().LastOrDefault();
            MediaTypeCollection formats = GetDeclaredContentTypes(GetRequestMetadataAttributes(descriptor), metadata);

            foreach (ApiParameterDescription parameter in description.ParameterDescriptions)
            {
                if (parameter.Source == BindingSource.Body)
                {
                    description.SupportedRequestFormats.AddRange(GetSupportedFormats(formats, parameter.Type));
                    continue;
                }

                if (parameter.Source != BindingSource.FormFile)
                {
                    continue;
                }

                foreach (String format in formats)
                {
                    description.SupportedRequestFormats.Add(new ApiRequestFormat { MediaType = format });
                }
            }

            return description;
        }

        protected virtual Boolean IsAtomicType(Type type)
        {
            return ReflectionUtilities.Inherit[typeof(Enum<>)].Contains(type);
        }

        protected virtual Boolean ShouldIgnoreProperty(ModelMetadata metadata, PropertyInfo? property)
        {
            if (property is null)
            {
                return false;
            }

            if (property.IsDefined(typeof(IgnoreDataMemberAttribute)))
            {
                return true;
            }

            if (!ExcludeJsonIgnoreAttribute)
            {
                return false;
            }

            if (property.IsDefined(typeof(JsonIgnoreAttribute)))
            {
                return true;
            }

            if (CustomAttributeExtensions.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>(property) is { Condition: System.Text.Json.Serialization.JsonIgnoreCondition.Always })
            {
                return true;
            }

            return false;
        }

        protected virtual PseudoModelBindingVisitor CreateVisitor(ApiParameterContext context, ParameterDescriptor parameter)
        {
            return new PseudoModelBindingVisitor(this, context, parameter);
        }

        protected virtual IList<ApiParameterDescription> GetParameters(ApiParameterContext context)
        {
            if (context.Descriptor?.Parameters is not null)
            {
                foreach (ParameterDescriptor parameter in context.Descriptor.Parameters)
                {
                    ApiParameterDescriptionContext api = new ApiParameterDescriptionContext(parameter is not ControllerParameterDescriptor descriptor || MetadataProvider is not ModelMetadataProvider provider ? MetadataProvider.GetMetadataForType(parameter.ParameterType) : provider.GetMetadataForParameter(descriptor.ParameterInfo), parameter.BindingInfo, parameter.Name);
                    CreateVisitor(context, parameter).WalkParameter(api);
                }
            }

            if (context.Descriptor?.BoundProperties is not null)
            {
                foreach (ParameterDescriptor property in context.Descriptor.BoundProperties)
                {
                    ApiParameterDescriptionContext api = new ApiParameterDescriptionContext(context.Provider.GetMetadataForProperty(context.Descriptor.ControllerTypeInfo.AsType(), property.Name), property.BindingInfo, property.Name);
                    CreateVisitor(context, property).WalkParameter(api);
                }
            }

            for (Int32 index = context.Results.Count - 1; index >= 0; --index)
            {
                if (!context.Results[index].Source.IsFromRequest)
                {
                    context.Results.RemoveAt(index);
                }
            }

            ProcessRouteParameters(context);
            ProcessIsRequired(context, Options);
            ProcessParameterDefaultValue(context);

            return context.Results;
        }

        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        protected virtual void ProcessRouteParameters(ApiParameterContext context)
        {
            Dictionary<String, ApiParameterRouteInfo> dictionary = new Dictionary<String, ApiParameterRouteInfo>(StringComparer.OrdinalIgnoreCase);
            foreach (TemplatePart parameter in context.Parameters)
            {
                if (parameter.Name is { } name)
                {
                    dictionary.Add(name, CreateRouteInfo(parameter));
                }
            }

            for (Int32 index = context.Results.Count - 1; index >= 0; --index)
            {
                ApiParameterDescription result = context.Results[index];

                if (result.Source != BindingSource.Path && result.Source != BindingSource.ModelBinding && result.Source != BindingSource.Custom)
                {
                    continue;
                }

                if (dictionary.TryGetValue(result.Name, out ApiParameterRouteInfo? route))
                {
                    result.RouteInfo = route;
                    dictionary.Remove(result.Name);

                    if (result.Source == BindingSource.ModelBinding && !result.RouteInfo.IsOptional)
                    {
                        result.Source = BindingSource.Path;
                    }

                    continue;
                }

                if (result.Source == BindingSource.Path && result.ModelMetadata is DefaultModelMetadata metadata && !metadata.Attributes.Attributes.OfType<IFromRouteMetadata>().Any())
                {
                    context.Results.RemoveAt(index);
                }
            }

            foreach (KeyValuePair<String, ApiParameterRouteInfo> keyValuePair in dictionary)
            {
                context.Results.Add(new ApiParameterDescription
                {
                    Name = keyValuePair.Key,
                    RouteInfo = keyValuePair.Value,
                    Source = BindingSource.Path
                });
            }
        }

        [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
        protected virtual TemplatePart? Find(IEnumerable<TemplatePart> parameters, String name)
        {
            foreach (TemplatePart template in parameters)
            {
                if (String.Equals(template.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return template;
                }
            }

            return null;
        }

        protected virtual void ProcessIsRequired(ApiParameterContext context, MvcOptions options)
        {
            foreach (ApiParameterDescription result in context.Results)
            {
                if (result.Source == BindingSource.Body)
                {
                    result.IsRequired = result.BindingInfo?.EmptyBodyBehavior is null or EmptyBodyBehavior.Default ? !options.AllowEmptyInputInBodyModelBinding : result.BindingInfo.EmptyBodyBehavior is not EmptyBodyBehavior.Allow;
                }

                if (result.ModelMetadata is { IsBindingRequired: true })
                {
                    result.IsRequired = true;
                }

                if (result.Source == BindingSource.Path && result.RouteInfo is not null)
                {
                    result.IsRequired = Find(context.Parameters, result.Name) is not { IsCatchAll: true } && (!result.RouteInfo.IsOptional || result.IsRequired);
                }
            }
        }

        protected virtual void ProcessParameterDefaultValue(ApiParameterContext context)
        {
            foreach (ApiParameterDescription result in context.Results)
            {
                if (result.Source == BindingSource.Path)
                {
                    result.DefaultValue = result.RouteInfo?.DefaultValue;
                    continue;
                }

                if (result.ParameterDescriptor is ControllerParameterDescriptor descriptor && TryGetDeclaredParameterDefaultValue(descriptor.ParameterInfo, out Object? value))
                {
                    result.DefaultValue = value;
                }
            }
        }

        [return: NotNullIfNotNull("template")]
        protected virtual ApiParameterRouteInfo? CreateRouteInfo(TemplatePart? template)
        {
            if (template is null)
            {
                return null;
            }

            List<IRouteConstraint> constraints = new List<IRouteConstraint>();

            ApiParameterRouteInfo result = new ApiParameterRouteInfo
            {
                Constraints = constraints,
                DefaultValue = template.DefaultValue,
                IsOptional = template.IsOptional || template.DefaultValue is not null
            };

            if (template.InlineConstraints is not { } exist)
            {
                return result;
            }

            foreach (InlineConstraint constraint in exist)
            {
                if (ConstraintResolver.ResolveConstraint(constraint.Constraint) is { } resolve)
                {
                    constraints.Add(resolve);
                }
            }

            return result;
        }

        protected virtual IEnumerable<String> GetHttpMethods(ControllerActionDescriptor? descriptor)
        {
            return descriptor?.ActionConstraints is { Count: > 0 } ? descriptor.ActionConstraints.OfType<HttpMethodActionConstraint>().SelectMany(static constraint => constraint.HttpMethods) : new String[1];
        }

        protected virtual RouteTemplate? ParseTemplate(ControllerActionDescriptor? descriptor)
        {
            return descriptor?.AttributeRouteInfo?.Template is not null ? TemplateParser.Parse(descriptor.AttributeRouteInfo.Template) : null;
        }

        [return: NotNullIfNotNull("route")]
        protected virtual String? GetRelativePath(RouteTemplate? route)
        {
            if (route is null)
            {
                return null;
            }

            List<String> values = new List<String>();

            foreach (TemplateSegment segment in route.Segments)
            {
                String current = String.Empty;

                foreach (TemplatePart template in segment.Parts)
                {
                    if (template.IsLiteral)
                    {
                        current += Route.LowercaseUrls ? template.Text?.ToLowerInvariant() ?? String.Empty : template.Text;
                    }
                    else if (template.IsParameter)
                    {
                        current = $"{current}{{{template.Name}}}";
                    }
                }

                values.Add(current);
            }

            return String.Join("/", values);
        }

        protected virtual IReadOnlyList<ApiRequestFormat> GetSupportedFormats(MediaTypeCollection media, Type type)
        {
            if (media.Count <= 0)
            {
                media = new MediaTypeCollection { (String?) null! };
            }

            List<ApiRequestFormat> formats = new List<ApiRequestFormat>();

            foreach (String content in media)
            {
                foreach (IInputFormatter formatter in Options.InputFormatters)
                {
                    if (formatter is IApiRequestFormatMetadataProvider provider && provider.GetSupportedContentTypes(content, type) is { } support)
                    {
                        formats.AddRange(support.Select(value => new ApiRequestFormat { Formatter = formatter, MediaType = value }));
                    }
                }
            }

            return formats;
        }

        protected virtual MediaTypeCollection GetDeclaredContentTypes(IReadOnlyList<IApiRequestMetadataProvider>? attributes, IAcceptsMetadata? metadata)
        {
            MediaTypeCollection media = new MediaTypeCollection();

            if (metadata is not null)
            {
                media.AddRange(metadata.ContentTypes);
            }

            if (attributes is null)
            {
                return media;
            }

            foreach (IApiRequestMetadataProvider attribute in attributes)
            {
                attribute.SetContentTypes(media);
            }

            return media;
        }

        protected virtual IApiRequestMetadataProvider[]? GetRequestMetadataAttributes(ControllerActionDescriptor descriptor)
        {
            if (descriptor.FilterDescriptors is { } descriptors)
            {
                return descriptors.Select(static descriptor => descriptor.Filter).OfType<IApiRequestMetadataProvider>().ToArray();
            }

            return null;
        }

        protected virtual String? GetGroupName(ControllerActionDescriptor descriptor, ApiDescriptionActionData data)
        {
            return descriptor.EndpointMetadata.OfType<IEndpointGroupNameMetadata>().LastOrDefault()?.EndpointGroupName ?? data.GroupName;
        }

        protected sealed class ApiParameterContext
        {
            public IModelMetadataProvider Provider { get; }
            public ControllerActionDescriptor? Descriptor { get; }
            public IReadOnlyList<TemplatePart> Parameters { get; }
            public IList<ApiParameterDescription> Results { get; }

            public ApiParameterContext(IModelMetadataProvider provider, ControllerActionDescriptor? descriptor, IReadOnlyList<TemplatePart> parameters)
            {
                Provider = provider;
                Descriptor = descriptor;
                Parameters = parameters;

                Results = new List<ApiParameterDescription>();
            }
        }
    }
}