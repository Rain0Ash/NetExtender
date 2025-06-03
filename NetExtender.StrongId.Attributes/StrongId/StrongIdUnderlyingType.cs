// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.StrongId
{
    /// <summary>
    /// The <see cref="Type"/> to use to store the value of the strongly-typed ID
    /// </summary>
    public enum StrongIdUnderlyingType : Byte
    {
        SByte,
        SByteNullable,
        Byte,
        ByteNullable,
        Int16,
        Int16Nullable,
        UInt16,
        UInt16Nullable,
        Int32,
        Int32Nullable,
        UInt32,
        UInt32Nullable,
        Int64,
        Int64Nullable,
        UInt64,
        UInt64Nullable,
        Single,
        SingleNullable,
        Double,
        DoubleNullable,
        Decimal,
        DecimalNullable,
        BigInteger,
        BigIntegerNullable,
        String,
        StringCurrentCulture,
        StringCurrentCultureIgnoreCase,
        StringInvariantCulture,
        StringInvariantCultureIgnoreCase,
        StringOrdinal,
        StringOrdinalIgnoreCase,
        StringNullable,
        StringNullableCurrentCulture,
        StringNullableCurrentCultureIgnoreCase,
        StringNullableInvariantCulture,
        StringNullableInvariantCultureIgnoreCase,
        StringNullableOrdinal,
        StringNullableOrdinalIgnoreCase,
        Guid,
        GuidNullable,
        NewId
    }
}