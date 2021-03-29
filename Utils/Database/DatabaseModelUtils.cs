// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NetExtender.Utils.Database
{
    public static class DatabaseModelUtils
    {
        public static String GetTableName<TEntity>([NotNull] this IModel model)
        {
            return GetTableName(model, typeof(TEntity));
        }
        
        public static String GetTableName([NotNull] this IModel model, [NotNull] Type type)
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