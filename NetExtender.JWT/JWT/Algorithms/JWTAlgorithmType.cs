// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.JWT.Algorithms
{
    public enum JWTAlgorithmType
    {
        /// <summary>
        /// HMAC using SHA-256
        /// </summary>
        HS256,

        /// <summary>
        /// HMAC using SHA-384
        /// </summary>
        HS384,

        /// <summary>
        /// HMAC using SHA-512
        /// </summary>
        HS512,

        /// <summary>
        /// RSASSA-PKCS1-v1_5 using SHA-256
        /// </summary>
        RS256,

        /// <summary>
        /// RSASSA-PKCS1-v1_5 using SHA-384
        /// </summary>
        RS384,

        /// <summary>
        /// RSASSA-PKCS1-v1_5 using SHA-512
        /// </summary>
        RS512,

        /// <summary>
        /// RSASSA-PKCS1-v1_5 using SHA-1024
        /// </summary>
        RS1024,

        /// <summary>
        /// RSASSA-PKCS1-v1_5 using SHA-2048
        /// </summary>
        RS2048,

        /// <summary>
        /// RSASSA-PKCS1-v1_5 using SHA-4096
        /// </summary>
        RS4096,

        /// <summary>
        /// ECDSA using SHA-256
        /// </summary>
        ES256,

        /// <summary>
        /// ECDSA using SHA-384
        /// </summary>
        ES384,

        /// <summary>
        /// ECDSA using SHA-512
        /// </summary>
        ES512,

        /// <summary>
        /// Algorithm used when no signing is wanted
        /// </summary>
        None
    }
}