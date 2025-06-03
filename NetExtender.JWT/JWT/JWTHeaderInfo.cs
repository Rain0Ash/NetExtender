// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace NetExtender.JWT
{
    public record JWTHeaderInfo
    {
        [JsonProperty("typ")]
        [JsonPropertyName("typ")]
        public String Type { get; set; } = null!;

        [JsonProperty("cty")]
        [JsonPropertyName("cty")]
        public String ContentType { get; set; } = null!;

        [JsonProperty("alg")]
        [JsonPropertyName("alg")]
        public String Algorithm { get; set; } = null!;

        [JsonProperty("kid")]
        [JsonPropertyName("kid")]
        public String KeyId { get; set; } = null!;

        [JsonProperty("x5u")]
        [JsonPropertyName("x5u")]
        public String X5u { get; set; } = null!;

        [JsonProperty("x5c")]
        [JsonPropertyName("x5c")]
        public String[] X5c { get; set; } = null!;

        [JsonProperty("x5t")]
        [JsonPropertyName("x5t")]
        public String X5t { get; set; } = null!;

        [System.Text.Json.Serialization.JsonConstructor]
        public JWTHeaderInfo()
        {
        }
    }
}