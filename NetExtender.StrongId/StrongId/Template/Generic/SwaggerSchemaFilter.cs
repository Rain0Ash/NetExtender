
        public class STRONGIDSchemaFilter : Swashbuckle.AspNetCore.SwaggerGen.ISchemaFilter
        {
            public void Apply(Microsoft.OpenApi.Models.OpenApiSchema schema, Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext context)
            {
                Microsoft.OpenApi.Models.OpenApiSchema api = new Microsoft.OpenApi.Models.OpenApiSchema { Type = SWAGGERTYPE, Format = SWAGGERFORMAT, Minimum = UNDERLYING.MinValue, Maximum = UNDERLYING.MaxValue, Nullable = NULLABLE };
                schema.Type = api.Type;
                schema.Format = api.Format;
                schema.Example = api.Example;
                schema.Default = api.Default;
                schema.Properties = api.Properties;
                schema.Nullable = api.Nullable;
            }
        }