// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data.Common;

namespace NetExtender.DataBase
{
    public enum SSLMode
    {
        None
    }

    public enum CharSet
    {
        Utf8
    }

    public record DatabaseConnection
    {
        public static implicit operator String(DatabaseConnection connection)
        {
            return connection.ToString();
        }

        public String ConnectionString
        {
            get
            {
                return this;
            }
        }
        
        public String Host { get; init; }
        public UInt16 Port { get; init; }
        public String User { get; init; }
        public String Password { get; init; }
        public String Database { get; init; }
        public Boolean TrustedConnection { get; init; } = true;
        public UInt16 Timeout { get; init; } = 60;
        public UInt16 CommandTimeout { get; init; } = 180;
        public UInt16 Lifetime { get; init; } = 600;
        public CharSet CharSet { get; init; } = CharSet.Utf8;
        public Boolean Pooling { get; init; } = false;
        public Int32 MinPoolSize { get; init; } = 0;
        public Int32 MaxPoolSize { get; init; } = 10;
        public Boolean AllowZeroDatetime { get; init; } = true;
        public Boolean ConvertZeroDatetime { get; init; } = true;
        public SSLMode SSLMode { get; init; } = SSLMode.None;
        public DataBaseType Type { get; init; } = DataBase.DefaultType;

        public DatabaseConnection(String host, UInt16 port, String user, String password, String database)
        {
            Host = host;
            Port = port;
            User = user;
            Password = password;
            Database = database ?? "Main";
        }

        public DbConnection Create()
        {
            return Create(Type);
        }
        
        public DbConnection Create(DataBaseType type)
        {
            return DataBase.Create(this, type);
        }

        public override String ToString()
        {
            return $"Server={Host},{Port};User={User};Password={Password};Database={Database};Pooling={Pooling};Min Pool Size={MinPoolSize};Max Pool Size={MaxPoolSize};Connection Timeout={Timeout};Connection Lifetime={Lifetime}";
        }
    }
}