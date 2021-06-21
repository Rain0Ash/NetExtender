// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NetExtender.Utils.EntityFrameworkCore
{
    public static class DbModelUtils
    {
        public static String? GetTableName<TEntity>(this IModel model)
        {
            return GetTableName(model, typeof(TEntity));
        }
        
        public static String? GetTableName(this IModel model, Type type)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return model.FindEntityType(type)?.GetTableName();
        }
    }
}