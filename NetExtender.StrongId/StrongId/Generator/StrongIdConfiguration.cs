// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.StrongId.Attributes;

namespace NetExtender.StrongId.Generator
{
    internal readonly struct StrongIdConfiguration
    {
        public static StrongIdConfiguration Default { get; } = new StrongIdConfiguration(StrongIdAssemblyAttribute.Default);
        
        public StrongIdUnderlyingType? Type { get; }
        public StrongIdConversionType? Conversion { get; }
        public StrongIdConverterType? Converter { get; }
        public StrongIdInterfaceType? Interfaces { get; }
        
        public StrongIdConfiguration(StrongIdAttribute attribute)
        {
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            Type = attribute.Type;
            Conversion = attribute.Conversion;
            Converter = attribute.Converter;
            Interfaces = attribute.Interfaces;
        }
        
        public StrongIdConfiguration(StrongIdAssemblyAttribute attribute)
        {
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            Type = attribute.Type;
            Conversion = attribute.Conversion;
            Converter = attribute.Converter;
            Interfaces = attribute.Interfaces;
        }
        
        public StrongIdConfiguration(StrongIdUnderlyingType? type, StrongIdConversionType? conversion, StrongIdConverterType? converter, StrongIdInterfaceType? interfaces)
        {
            Type = type;
            Conversion = conversion;
            Converter = converter;
            Interfaces = interfaces;
        }

        /// <summary>
        /// Combines multiple <see cref="StrongIdConfiguration"/> values associated
        /// with a given <see cref="Attributes.StrongIdAttribute"/>, returning definite values.
        /// </summary>
        public static StrongIdConfiguration Combine(StrongIdConfiguration attribute, StrongIdConfiguration? configuration)
        {
            StrongIdUnderlyingType type = (attribute.Type, configuration?.Type) switch
            {
                (null, null) => StrongIdAssemblyAttribute.Default.Type,
                (null, StrongIdUnderlyingType specific) => specific,
                (StrongIdUnderlyingType specific, _) => specific
            };
            
            StrongIdConversionType conversion = (attribute.Conversion, configuration?.Conversion) switch
            {
                (null, null) => StrongIdAssemblyAttribute.Default.Conversion,
                (null, StrongIdConversionType specific) => specific,
                (StrongIdConversionType specific, _) => specific
            };
            
            StrongIdConverterType converter = (attribute.Converter, configuration?.Converter) switch
            {
                (null, null) => StrongIdAssemblyAttribute.Default.Converter,
                (null, StrongIdConverterType specific) => specific,
                (StrongIdConverterType specific, _) => specific
            };
            
            StrongIdInterfaceType interfaces = (attribute.Interfaces, configuration?.Interfaces) switch
            {
                (null, null) => StrongIdAssemblyAttribute.Default.Interfaces,
                (null, StrongIdInterfaceType specific) => specific,
                (StrongIdInterfaceType specific, _) => specific
            };

            return new StrongIdConfiguration(type, conversion, converter, interfaces);
        }
    }
}