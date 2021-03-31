// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace NetExtender.Utils.EntityFramework
{
    public static class DatabaseFacedeUtils
    {
        public static Boolean CreateDatabaseIfNotExists([NotNull] this DatabaseFacade database)
        {
            if (database is null)
            {
                throw new ArgumentNullException(nameof(database));
            }
            
            return database.EnsureCreated();
        }
        
        public static Task<Boolean> CreateDatabaseIfNotExistsAsync([NotNull] this DatabaseFacade database)
        {
            return CreateDatabaseIfNotExistsAsync(database, CancellationToken.None);
        }
        
        public static Task<Boolean> CreateDatabaseIfNotExistsAsync([NotNull] this DatabaseFacade database, CancellationToken token)
        {
            if (database is null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            return database.EnsureCreatedAsync(token);
        }
        
        public static Boolean CreateDatabaseIfNotExists([NotNull] this DbContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return CreateDatabaseIfNotExists(context.Database);
        }
        
        public static Task<Boolean> CreateDatabaseIfNotExistsAsync([NotNull] this DbContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return CreateDatabaseIfNotExistsAsync(context.Database);
        }
        
        public static Task<Boolean> CreateDatabaseIfNotExistsAsync([NotNull] this DbContext context, CancellationToken token)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return CreateDatabaseIfNotExistsAsync(context.Database, token);
        }
        
        public static Boolean DeleteDatabaseIfExists([NotNull] this DatabaseFacade database)
        {
            if (database is null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            return database.EnsureDeleted();
        }
        
        public static Task<Boolean> DeleteDatabaseIfExistsAsync([NotNull] this DatabaseFacade database)
        {
            return DeleteDatabaseIfExistsAsync(database, CancellationToken.None);
        }
        
        public static Task<Boolean> DeleteDatabaseIfExistsAsync([NotNull] this DatabaseFacade database, CancellationToken token)
        {
            if (database is null)
            {
                throw new ArgumentNullException(nameof(database));
            }
            
            return database.EnsureDeletedAsync(token);
        }
        
        public static Boolean DeleteDatabaseIfExists([NotNull] this DbContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return DeleteDatabaseIfExists(context.Database);
        }
        
        public static Task<Boolean> DeleteDatabaseIfExistsAsync([NotNull] this DbContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return DeleteDatabaseIfExistsAsync(context.Database);
        }
        
        public static Task<Boolean> DeleteDatabaseIfExistsAsync([NotNull] this DbContext context, CancellationToken token)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return DeleteDatabaseIfExistsAsync(context.Database, token);
        }
        
        public static Boolean RecreateDatabase([NotNull] this DatabaseFacade database)
        {
            if (database is null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            database.EnsureDeleted();
            return database.EnsureCreated();
        }
        
        public static Task<Boolean> RecreateDatabaseAsync([NotNull] this DatabaseFacade database)
        {
            return RecreateDatabaseAsync(database, CancellationToken.None);
        }
        
        public static async Task<Boolean> RecreateDatabaseAsync([NotNull] this DatabaseFacade database, CancellationToken token)
        {
            if (database is null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            await database.EnsureDeletedAsync(token).ConfigureAwait(false);
            return await database.EnsureCreatedAsync(token).ConfigureAwait(false);
        }
        
        public static Boolean RecreateDatabase([NotNull] this DbContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return RecreateDatabase(context.Database);
        }
        
        public static Task<Boolean> RecreateDatabaseAsync([NotNull] this DbContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return RecreateDatabaseAsync(context.Database);
        }
        
        public static Task<Boolean> RecreateDatabaseAsync([NotNull] this DbContext context, CancellationToken token)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return RecreateDatabaseAsync(context.Database, token);
        }
    }
}