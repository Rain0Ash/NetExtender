// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.ComponentModel;

namespace NetExtender.JWT
{
    /// <summary>Predefined RFC 7515 parameters</summary>
    /// <remarks>https://tools.ietf.org/html/rfc7515</remarks>
    public enum JWTHeader
    {
        [Description("typ")]
        Type,

        [Description("cty")]
        ContentType,

        [Description("alg")]
        Algorithm,

        [Description("kid")]
        KeyId,

        [Description("x5u")]
        X5u,

        [Description("x5c")]
        X5c,

        [Description("x5t")]
        X5t
    }
}