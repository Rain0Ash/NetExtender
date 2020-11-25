// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;

namespace NetExtender.DataBase
{
    public enum DataBaseType
    {
        MySQL,
        SQLite
    }

    public static class DataBase
    {
        public const DataBaseType DefaultType = DataBaseType.SQLite;

        private static DbConnection GetConnection(String str, DataBaseType type)
        {
            try
            {
                return type switch
                {
                    DataBaseType.MySQL => new MySqlConnection(str),
                    DataBaseType.SQLite => new SqliteConnection(str),
                    _ => throw new NotSupportedException()
                };
            }
            catch (NotSupportedException)
            {
                throw;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static Boolean TryGetConnection(String str, DataBaseType type, out DbConnection connection)
        {
            connection = GetConnection(str, type);
            return connection is not null;
        }
        
        public static DbConnection Create(String str, DataBaseType type = DefaultType)
        {
            if (String.IsNullOrEmpty(str))
            {
                throw new ArgumentException(@"Value cannot be null or empty.", nameof(str));
            }

            if (!TryGetConnection(str, type, out DbConnection connection))
            {
                return null;
            }
            
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                connection.Dispose();
                return null;
            }
        }
        
        public static Task<DbConnection> CreateAsync(String str, DataBaseType type = DefaultType)
        {
            return CreateAsync(str, type, CancellationToken.None);
        }
        
        public static Task<DbConnection> CreateAsync(String str, CancellationToken token)
        {
            return CreateAsync(str, DefaultType, token);
        }
        
        public static async Task<DbConnection> CreateAsync(String str, DataBaseType type, CancellationToken token)
        {
            if (String.IsNullOrEmpty(str))
            {
                throw new ArgumentException(@"Value cannot be null or empty.", nameof(str));
            }

            if (!TryGetConnection(str, type, out DbConnection connection))
            {
                return null;
            }
            
            try
            {
                await connection.OpenAsync(token).ConfigureAwait(false);
                return connection;
            }
            catch (Exception)
            {
                await connection.DisposeAsync().ConfigureAwait(false);
                return null;
            }
        }

        public static void Close(DbConnection connection)
        {
            connection?.Close();
        }

        public static Object GetValue(this MySqlDataReader reader, String column)
        {
            return reader.GetValue(reader.GetOrdinal(column));
        }
    }
}