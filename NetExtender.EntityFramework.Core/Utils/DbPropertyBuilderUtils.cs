// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetExtender.Utils.Serialization;
using Newtonsoft.Json;

namespace NetExtender.Utils.EntityFrameworkCore
{
    public static class DbPropertyBuilderUtils
    {
        public static PropertyBuilder<TProperty> IsOptional<TProperty>(this PropertyBuilder<TProperty> builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.IsRequired(false);
        }
    
        public static PropertyBuilder<String> HasMaxLength(this PropertyBuilder<String> builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.HasColumnType("NVARCHAR(MAX)");
        }

        public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.HasConversion(item => item.JsonSerializeObject(),
                json => json.JsonDeserializeObject<T>()!);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> builder, JsonSerializerSettings? settings)
        {
            return HasJsonConversion(builder, settings, settings);
        }
        
        public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> builder, JsonSerializerSettings? serializer, JsonSerializerSettings? deserializer)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.HasConversion(item => item.JsonSerializeObject(serializer),
                json => json.JsonDeserializeObject<T>(deserializer)!);
        }
    }
}