using System;
using System.Collections;
using System.Globalization;
using System.Linq;
#if NET8_0_OR_GREATER
using Microsoft.OpenApi;
#else
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
#endif
using NetExtender.Utilities.IO;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetExtender.AspNetCore.Swagger.Schema.Filters
{
    public class EnumSchemaFilter : ISchemaFilter
    {
#if NET8_0_OR_GREATER
        public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
#else
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
#endif
        {
            // Проверяем, является ли тип generic Enum<> или Enum<,>
            /*if (!context.Type.IsGenericType)
                return;

            context.Type.ToConsole();

            var genericType = context.Type.GetGenericTypeDefinition();

            // Проверяем, что это наш кастомный Enum<T> или Enum<T, TEnum>
            if (genericType.FullName?.StartsWith("NetExtender.Types.Enums.Enum`") != true)
                return;

            // Получаем underlying enum тип (первый generic аргумент)
            var enumType = context.Type.GetGenericArguments()[0];

            if (!enumType.IsEnum)
                return;

            // Настраиваем схему как для обычного enum
            schema.Type = "string";
            schema.Format = null;
            schema.Enum.Clear();

            // Добавляем значения enum
            foreach (var name in Enum.GetNames(enumType))
            {
                schema.Enum.Add(new OpenApiString(name));
            }

            // Опционально: добавляем расширение с numeric значениями
            var enumValues = new OpenApiArray();
            enumValues.AddRange(
                Enum.GetValues(enumType)
                    .Cast<object>()
                    .Select(e => new OpenApiInteger(Convert.ToInt32(e)))
            );
            schema.Extensions["x-enum-values"] = enumValues;

            // Добавляем имена для совместимости с некоторыми генераторами
            var enumNames = new Open();
            enumNames.AddRange(Enum.GetNames(enumType).Select(n => new OpenApiString(n)));
            schema.Extensions["x-enum-varnames"] = enumNames;*/
        }
    }
}