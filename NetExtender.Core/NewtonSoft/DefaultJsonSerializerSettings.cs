// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Newtonsoft.Json;

namespace NetExtender.NewtonSoft
{
    public class DefaultJsonSerializerSettings : JsonSerializerSettings
    {
        public DefaultJsonSerializerSettings()
        {
            Formatting = Formatting.Indented;
            NullValueHandling = NullValueHandling.Ignore;
            ContractResolver = GenericContractResolver.Instance;
        }
    }
}