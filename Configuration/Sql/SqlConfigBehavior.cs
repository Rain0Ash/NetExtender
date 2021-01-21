// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Common;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.DataBase;

namespace NetExtender.Configuration.Sql
{
    public class SqlConfigBehavior : ConfigBehavior
    {
        private readonly DatabaseConnection _connection;
        
        public SqlConfigBehavior(String path, DatabaseConnection connection, ConfigOptions options)
            : base(path, options)
        {
            _connection = connection;
        }

        public SqlConfigBehavior(String path, DatabaseConnection connection, ICryptKey crypt, ConfigOptions options)
            : base(path, crypt, options)
        {
            _connection = connection;
        }

        public override String Get(String key, params String[] sections)
        {
            throw new NotImplementedException();
        }

        public override Boolean Set(String key, String value, params String[] sections)
        {
            throw new NotImplementedException();
        }
    }
}