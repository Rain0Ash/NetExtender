// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.StrongId
{
    /// <summary>
    /// Converters used to to serialize/deserialize strongly-typed ID values
    /// </summary>
    [Flags]
    public enum StrongIdConverterType : Byte
    {
        /// <summary>
        /// Don't create any converters for the strongly-typed ID
        /// </summary>
        None = 0,

        /// <summary>
        /// Creates a <see cref="String"/> for converting from the strongly-typed ID to and from a string
        /// </summary>
        String = 1,

        /// <summary>
        /// Creates a <see cref="System.Runtime.Serialization.ISerializable"/> for serializing the strongly-typed ID
        /// </summary>
        Serializable = 2,

        /// <summary>
        /// Creates a System.Text.Json.Serialization.JsonConverter for serializing the strongly-typed ID to its primitive value
        /// </summary>
        TextJson = 4,

        /// <summary>
        /// Creates a Newtonsoft.Json.JsonConverter for serializing the strongly-typed ID to its primitive value
        /// </summary>
        Newtonsoft = 8,

        /// <summary>
        /// Creates an Entity Framework Core Value Converter for extracting the primitive value
        /// </summary>
        EntityFramework = 16,

        /// <summary>
        /// Creates a Dapper TypeHandler for converting to and from the type
        /// </summary>
        Dapper = 32,
        
        /// <summary>
        /// Creates a Swagger SchemaFilter for OpenApi documentation
        /// </summary>
        Swagger = 64,
        
        All = String | Serializable | TextJson | Newtonsoft | EntityFramework | Dapper | Swagger
    }
}