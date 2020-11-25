// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace NetExtender.DataBase
{
    public class DatabaseContext : DbContext
    {
        public sealed override DatabaseFacade Database
        {
            get
            {
                return base.Database;
            }
        }

        public DatabaseConnection Connection { get; }
        public String ConnectionString { get; }
        public DataBaseType Type { get; }

        public DatabaseContext(DatabaseConnection connection)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            ConnectionString = connection.ConnectionString;
            Type = connection.Type;
            Database.EnsureCreated();
        }

        public DatabaseContext(String connection, DataBaseType type = DataBase.DefaultType)
        {
            ConnectionString = connection;
            Type = type;
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (builder.IsConfigured)
            {
                return;
            }

            switch (Type)
            {
                case DataBaseType.MySQL:
                    builder.UseSqlServer(ConnectionString);
                    break;
                case DataBaseType.SQLite:
                    builder.UseSqlite(ConnectionString);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}