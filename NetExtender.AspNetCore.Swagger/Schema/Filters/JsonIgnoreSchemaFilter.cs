using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
#if NET8_0_OR_GREATER
using Microsoft.OpenApi;
#else
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
#endif
using Swashbuckle.AspNetCore.SwaggerGen;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace NetExtender.AspNetCore.Swagger.Schema.Filters
{
    public class JsonIgnoreSchemaFilter : ISchemaFilter
    {
#if NET8_0_OR_GREATER
        public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
#else
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
#endif
        {
            if (schema.Properties is not { Count: > 0 })
            {
                return;
            }

            PropertyInfo[] properties = context.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                if (!IsIgnored(property))
                {
                    continue;
                }

                foreach (String key in schema.Properties.Keys)
                {
                    if (!String.Equals(key, property.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    schema.Properties.Remove(key);
                    return;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsIgnored(JsonIgnoreAttribute? attribute)
        {
            return attribute is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsIgnored(System.Text.Json.Serialization.JsonIgnoreAttribute? attribute)
        {
            return attribute?.Condition is JsonIgnoreCondition.Always;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsIgnored(PropertyInfo property)
        {
            return IsIgnored(property.GetCustomAttribute<JsonIgnoreAttribute>()) || IsIgnored(property.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>());
        }
    }
}