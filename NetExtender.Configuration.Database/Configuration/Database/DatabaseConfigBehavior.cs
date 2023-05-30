// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetExtender.Configuration.Behavior;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Database.Configuration.Common;

namespace NetExtender.Configuration.Database.Configuration.Database
{
    public class DatabaseConfigBehavior : DatabaseConfigBehavior<ConfigDatabaseEntity>
    {
        public DatabaseConfigBehavior(DbContext context, ConfigOptions options)
            : base(context, options)
        {
        }

        public DatabaseConfigBehavior(DbContext context, String? name, ConfigOptions options)
            : base(context, name, options)
        {
        }

        protected override ConfigDatabaseEntity New(String key, String? value)
        {
            return new ConfigDatabaseEntity(key, value);
        }
    }

    public abstract class DatabaseConfigBehavior<T> : SingleKeyConfigBehavior where T : ConfigDatabaseEntity
    {
        protected DbContext Context { get; }

        protected virtual DbSet<T> Storage
        {
            get
            {
                return Context.Set<T>(!String.IsNullOrEmpty(Path) ? Path : "Config");
            }
        }

        protected DatabaseConfigBehavior(DbContext context, ConfigOptions options)
            : this(context, null, options)
        {
        }

        protected DatabaseConfigBehavior(DbContext context, String? name, ConfigOptions options)
            : base(name ?? String.Empty, options & ~ConfigOptions.LazyWrite)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected abstract T New(String key, String? value);

        protected virtual ConfigurationSingleKeyEntry Convert(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return new ConfigurationSingleKeyEntry(entity.Key, entity.Value);
        }

        protected override String? TryGetValue(String? key)
        {
            if (key is null)
            {
                return null;
            }
            
            try
            {
                T? entity = Storage.FirstOrDefault(entity => entity.Key == key);
                return entity?.Value;
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected override async Task<String?> TryGetValueAsync(String? key, CancellationToken token)
        {
            if (key is null)
            {
                return null;
            }
            
            try
            {
                T? entity = await Storage.FirstOrDefaultAsync(entity => entity.Key == key, token).ConfigureAwait(false);
                return entity?.Value;
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected override Boolean TrySetValue(String? key, String? value)
        {
            if (key is null)
            {
                return false;
            }
            
            try
            {
                T? entity = Storage.FirstOrDefault(entity => entity.Key == key);

                if (value is null)
                {
                    if (entity is null)
                    {
                        return false;
                    }

                    Storage.Remove(entity);
                    return true;
                }

                if (entity is not null)
                {
                    entity.Value = value;
                }
                else
                {
                    entity = New(key, value);
                    Storage.Add(entity);
                }

                Context.SaveChanges();
                return true;
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected override async Task<Boolean> TrySetValueAsync(String? key, String? value, CancellationToken token)
        {
            if (key is null)
            {
                return false;
            }
            
            try
            {
                T? entity = await Storage.FirstOrDefaultAsync(entity => entity.Key == key, token).ConfigureAwait(false);

                if (value is null)
                {
                    if (entity is null)
                    {
                        return false;
                    }

                    Storage.Remove(entity);
                    return true;
                }

                if (entity is not null)
                {
                    entity.Value = value;
                }
                else
                {
                    entity = New(key, value);
                    await Storage.AddAsync(entity, token).ConfigureAwait(false);
                }

                await Context.SaveChangesAsync(token).ConfigureAwait(false);
                return true;
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected override String[]? TryGetExists()
        {
            try
            {
                return Storage.Select(entity => entity.Key).ToArray();
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected override async Task<String[]?> TryGetExistsAsync(CancellationToken token)
        {
            try
            {
                return await Storage.Select(entity => entity.Key).ToArrayAsync(token).ConfigureAwait(false);
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected override ConfigurationSingleKeyEntry[]? TryGetExistsValues()
        {
            try
            {
                return Storage.Select(Convert).ToArray();
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected override async Task<ConfigurationSingleKeyEntry[]?> TryGetExistsValuesAsync(CancellationToken token)
        {
            try
            {
                T[] entities = await Storage.ToArrayAsync(token).ConfigureAwait(false);
                return entities.Select(Convert).ToArray();
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}