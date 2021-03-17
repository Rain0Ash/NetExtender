// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Configuration.Common;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Configuration.Sql
{
    public class SqlConfigBehavior : ConfigBehavior
    {
        public SqlConfigBehavior(String path, ConfigOptions options)
            : base(path, options)
        {
            throw new NotImplementedException();
        }

        public SqlConfigBehavior(String path, ICryptKey crypt, ConfigOptions options)
            : base(path, crypt, options)
        {
            throw new NotImplementedException();
        }

        public override String Get(String key, IEnumerable<String> sections)
        {
            throw new NotImplementedException();
        }

        public override Boolean Set(String key, String value, IEnumerable<String> sections)
        {
            throw new NotImplementedException();
        }
    }
}