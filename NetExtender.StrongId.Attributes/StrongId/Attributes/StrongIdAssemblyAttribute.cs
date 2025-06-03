// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.StrongId.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class StrongIdAssemblyAttribute : Attribute
    {
        public static StrongIdAssemblyAttribute Default { get; } = new StrongIdAssemblyAttribute();
        
        /// <summary>
        /// The default <see cref="System.Type"/> to use to store the strongly-typed ID values.
        /// </summary>
        public StrongIdUnderlyingType Type { get; }

        /// <summary>
        /// The type of conversion operator for the strongly-typed ID
        /// </summary>
        public StrongIdConversionType Conversion { get; }

        /// <summary>
        /// The default converters to create for serializing/deserializing strongly-typed ID values.
        /// </summary>
        public StrongIdConverterType Converter { get; }

        /// <summary>
        /// Interfaces and patterns that strongly-typed ID should implement
        /// </summary>
        public StrongIdInterfaceType Interfaces { get; }

        public StrongIdAssemblyAttribute(Type type, StrongIdConversionType conversion = StrongIdConversionType.Explicit, StrongIdConverterType converter = StrongIdConverterType.String | StrongIdConverterType.TextJson | StrongIdConverterType.Newtonsoft, StrongIdInterfaceType interfaces = StrongIdInterfaceType.All)
            : this(Convert(type), conversion, converter, interfaces)
        {
        }
        
        public StrongIdAssemblyAttribute(StrongIdUnderlyingType type = StrongIdUnderlyingType.Int32, StrongIdConversionType conversion = StrongIdConversionType.Explicit, StrongIdConverterType converter = StrongIdConverterType.String | StrongIdConverterType.TextJson | StrongIdConverterType.Newtonsoft, StrongIdInterfaceType interfaces = StrongIdInterfaceType.All)
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