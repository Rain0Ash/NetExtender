// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Config.Common;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.DataBase;

namespace NetExtender.Config.Sql
{
    public class SqlConfig : Config
    {
        public SqlConfig(String path, DatabaseConnection databaseConnection, ConfigOptions options)
            : base(path, options)
        {
        }

        public SqlConfig(String path, DatabaseConnection databaseConnection, ICryptKey crypt, ConfigOptions options)
            : base(path, crypt, options)
        {
        }

        protected override String Get(String key, params String[] sections)
        {
            throw new NotImplementedException();
        }

        protected override Boolean Set(String key, String value, params String[] sections)
        {
            throw new NotImplementedException();
        }
    }
}