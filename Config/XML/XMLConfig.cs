// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Config.Common;
using NetExtender.Config.JSON;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Types.Trees;
using NetExtender.Utils.Formats;

namespace NetExtender.Config.XML
{
    public class XMLConfig : JSONConfig
    {
        public XMLConfig(String path = null, ConfigOptions options = ConfigOptions.None)
            : this(path, null, options)
        {
        }
        
        public XMLConfig(String path, ICryptKey crypt, ConfigOptions options = ConfigOptions.None)
            : base(ValidatePathOrGetDefault(path, "xml"), crypt, options)
        {
        }

        private static String Convert(String key)
        {
            return key.Replace(' ', '_');
        }
        
        private static String[] Convert(IEnumerable<String> sections)
        {
            return sections.Select(Convert).ToArray();
        }
        
        protected override String Get(String key, params String[] sections)
        {
            return base.Get(Convert(key), Convert(sections));
        }

        protected override Boolean Set(String key, String value, params String[] sections)
        {
            return base.Set(Convert(key), value, Convert(sections));
        }

        protected override String SerializeConfig()
        {
            return XMLUtils.ToXML(base.SerializeConfig(), "Config");
        }

        protected override DictionaryTree<String, String> DeserializeConfig(String config)
        {
            if (String.IsNullOrWhiteSpace(config))
            {
                return new DictionaryTree<String, String>();
            }
            
            try
            {
                return base.DeserializeConfig(JSONUtils.ToJSON(config));
            }
            catch (Exception)
            {
                return new DictionaryTree<String, String>();
            }
        }
    }
}