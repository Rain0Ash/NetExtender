// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetExtender.Utils.Database
{
    public static class PropertyBuilderUtils
    {
        public static PropertyBuilder<TProperty> IsOptional<TProperty>(this PropertyBuilder<TProperty> builder)
        {
            return builder.IsRequired(false);
        }
    
        public static PropertyBuilder<String> HasMaxLength(this PropertyBuilder<String> builder)
        {
            return builder.HasColumnType("NVARCHAR(MAX)");
        }
    }
}