// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.StrongId.Attributes
{
    /// <summary>
    /// Place on partial classes or structs to make the type the strongly-typed ID
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)] //TODO: class
    public sealed class StrongIdAttribute : Attribute
    {
        /// <summary>
        /// The default <see cref="System.Type"/> to use to store the strongly-typed ID values.
        /// </summary>
        public StrongIdUnderlyingType Type { get; }

        /// <summary>
        /// The type of conversion operator for the strongly-typed ID
        /// </summary>
        public StrongIdConversionType? Conversion { get; }

        /// <summary>
        /// The default converters to create for serializing/deserializing strongly-typed ID values.
        /// </summary>
        public StrongIdConverterType? Converter { get; }

        /// <summary>
        /// Interfaces and patterns that strongly-typed ID should implement
        /// </summary>
        public StrongIdInterfaceType? Interfaces { get; }

        public StrongIdAttribute(Type type)
            : this(Convert(type))
        {
        }

        public StrongIdAttribute(StrongIdUnderlyingType type)
        {
            Type = type;
            Conversion = default;
            Converter = default;
            Interfaces = default;
        }

        public StrongIdAttribute(Type type, StrongIdConversionType conversion)
            : this(Convert(type), conversion)
        {
        }

        public StrongIdAttribute(StrongIdUnderlyingType type, StrongIdConversionType conversion)
        {
            Type = type;
            Conversion = conversion;
            Converter = default;
            Interfaces = default;
        }

        public StrongIdAttribute(Type type, StrongIdConverterType converter)
            : this(Convert(type), converter)
        {
        }

        public StrongIdAttribute(StrongIdUnderlyingType type, StrongIdConverterType converter)
        {
            Type = type;
            Conversion = default;
            Converter = converter;
            Interfaces = default;
        }

        public StrongIdAttribute(Type type, StrongIdInterfaceType interfaces)
            : this(Convert(type), interfaces)
        {
        }

        public StrongIdAttribute(StrongIdUnderlyingType type, StrongIdInterfaceType interfaces)
        {
            Type = type;
            Conversion = default;
            Converter = default;
            Interfaces = interfaces;
        }

        public StrongIdAttribute(Type type, StrongIdConversionType conversion, StrongIdConverterType converter)
            : this(Convert(type), conversion, converter)
        {
        }

        public StrongIdAttribute(StrongIdUnderlyingType type, StrongIdConversionType conversion, StrongIdConverterType converter)
        {
            Type = type;
            Conversion = conversion;
            Converter = converter;
            Interfaces = default;
        }

        public StrongIdAttribute(Type type, StrongIdConversionType conversion, StrongIdInterfaceType interfaces)
            : this(Convert(type), conversion, interfaces)
        {
        }

        public StrongIdAttribute(StrongIdUnderlyingType type, StrongIdConversionType conversion, StrongIdInterfaceType interfaces)
        {
            Type = type;
            Conversion = conversion;
            Converter = default;
            Interfaces = interfaces;
        }

        public StrongIdAttribute(Type type, StrongIdConverterType converter, StrongIdInterfaceType interfaces)
            : this(Convert(type), converter, interfaces)
        {
        }

        public StrongIdAttribute(StrongIdUnderlyingType type, StrongIdConverterType converter, StrongIdInterfaceType interfaces)
        {
            Type = type;
            Conversion = default;
            Converter = converter;
            Interfaces = interfaces;
        }

        public StrongIdAttribute(Type type, StrongIdConversionType conversion, StrongIdConverterType converter, StrongIdInterfaceType interfaces)
            : this(Convert(type), conversion, converter, interfaces)
        {
        }

        public StrongIdAttribute(StrongIdUnderlyingType type, StrongIdConversionType conversion, StrongIdConverterType converter, StrongIdInterfaceType interfaces)
        {
            Type = type;
            Conversion = conversion;
            Converter = converter;
            Interfaces = interfaces;
        }

        private static String TypeName(Type type)
        {
            if (!type.IsGenericType)
            {
                return type.Namespace is not null ? $"{type.Namespace}.{type.Name}" : type.Name;
            }

            String typename = type.GetGenericTypeDefinition().Name;
            typename = typename.Substring(0, typename.IndexOf('`'));
            Type[] generic = type.GetGenericArguments();
            String[] arguments = new String[generic.Length];
            for (Int32 i = 0; i < generic.Length; i++)
            {
                arguments[i] = TypeName(generic[i]);
            }
            
            return type.Namespace is not null ? $"{type.Namespace}.{typename}<{String.Join(", ", arguments)}>" : $"{typename}<{String.Join(", ", arguments)}>";
        }

        private static StrongIdUnderlyingType Convert(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return TypeName(type) switch
            {
                "System.SByte" => StrongIdUnderlyingType.SByte,
                "System.Nullable<System.SByte>" => StrongIdUnderlyingType.SByteNullable,
                "System.Byte" => StrongIdUnderlyingType.Byte,
                "System.Nullable<System.Byte>" => StrongIdUnderlyingType.ByteNullable,
                "System.Int16" => StrongIdUnderlyingType.Int16,
                "System.Nullable<System.Int16>" => StrongIdUnderlyingType.Int16Nullable,
                "System.UInt16" => StrongIdUnderlyingType.UInt16,
                "System.Nullable<System.UInt16>" => StrongIdUnderlyingType.UInt16Nullable,
                "System.Int32" => StrongIdUnderlyingType.Int32,
                "System.Nullable<System.Int32>" => StrongIdUnderlyingType.Int32Nullable,
                "System.UInt32" => StrongIdUnderlyingType.UInt32,
                "System.Nullable<System.UInt32>" => StrongIdUnderlyingType.UInt32Nullable,
                "System.Int64" => StrongIdUnderlyingType.Int64,
                "System.Nullable<System.Int64>" => StrongIdUnderlyingType.Int64Nullable,
                "System.UInt64" => StrongIdUnderlyingType.UInt64,
                "System.Nullable<System.UInt64>" => StrongIdUnderlyingType.UInt64Nullable,
                "System.Single" => StrongIdUnderlyingType.Single,
                "System.Nullable<System.Single>" => StrongIdUnderlyingType.SingleNullable,
                "System.Double" => StrongIdUnderlyingType.Double,
                "System.Nullable<System.Double>" => StrongIdUnderlyingType.DoubleNullable,
                "System.Decimal" => StrongIdUnderlyingType.Decimal,
                "System.Nullable<System.Decimal>" => StrongIdUnderlyingType.DecimalNullable,
                "System.Numerics.BigInteger" => StrongIdUnderlyingType.BigInteger,
                "System.Nullable<System.Numerics.BigInteger>" => StrongIdUnderlyingType.BigIntegerNullable,
                "System.String" => StrongIdUnderlyingType.String,
                "System.Guid" => StrongIdUnderlyingType.Guid,
                "System.Nullable<System.Guid>" => StrongIdUnderlyingType.GuidNullable,
                "MassTransit.NewId" => StrongIdUnderlyingType.NewId,
                _ => throw new NotSupportedException($"Type '{type.FullName}' not supported.")
            };
        }
    }
}