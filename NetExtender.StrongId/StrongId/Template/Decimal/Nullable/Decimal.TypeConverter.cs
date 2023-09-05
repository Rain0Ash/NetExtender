﻿
        public class STRONGIDTypeConverter : System.ComponentModel.TypeConverter
        {
            public override System.Boolean CanConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Type? type)
            {
                return type == typeof(System.Decimal?) ||
                       type == typeof(System.String) ||
                       base.CanConvertTo(context, type);
            }

            public override System.Object? ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, System.Object? value, System.Type type)
            {
                if (value is not STRONGID strong)
                {
                    return base.ConvertTo(context, culture, value, type);
                }

                if (type == typeof(System.Decimal?))
                {
                    return strong.Value;
                }

                if (type == typeof(System.String))
                {
                    return strong.Value?.ToString(culture);
                }

                return base.ConvertTo(context, culture, value, type);
            }

            public override System.Boolean CanConvertFrom(System.ComponentModel.ITypeDescriptorContext? context, System.Type type)
            {
                return type == typeof(System.SByte) ||
                       type == typeof(System.SByte?) ||
                       type == typeof(System.Byte) ||
                       type == typeof(System.Byte?) ||
                       type == typeof(System.Int16) ||
                       type == typeof(System.Int16?) ||
                       type == typeof(System.UInt16) ||
                       type == typeof(System.UInt16?) ||
                       type == typeof(System.Int32) ||
                       type == typeof(System.Int32?) ||
                       type == typeof(System.UInt32) ||
                       type == typeof(System.UInt32?) ||
                       type == typeof(System.Int64) ||
                       type == typeof(System.Int64?) ||
                       type == typeof(System.UInt64) ||
                       type == typeof(System.UInt64?) ||
                       type == typeof(System.Single) ||
                       type == typeof(System.Single?) ||
                       type == typeof(System.Double) ||
                       type == typeof(System.Double?) ||
                       type == typeof(System.Decimal) ||
                       type == typeof(System.Decimal?) ||
                       type == typeof(System.String) ||
                       base.CanConvertFrom(context, type);
            }

            public override System.Object? ConvertFrom(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, System.Object value)
            {
                return value switch
                {
                    System.SByte result => new STRONGID(result),
                    System.Byte result => new STRONGID(result),
                    System.Int16 result => new STRONGID(result),
                    System.UInt16 result => new STRONGID(result),
                    System.Int32 result => new STRONGID(result),
                    System.UInt32 result => new STRONGID(result),
                    System.Int64 result => new STRONGID(result),
                    System.UInt64 result => new STRONGID(result),
                    System.Single result => new STRONGID((TYPE) result),
                    System.Double result => new STRONGID((TYPE) result),
                    System.Decimal result => new STRONGID(result),
                    System.String result when TYPE.TryParse(result, System.Globalization.NumberStyles.Any, culture, out TYPE convert) => new STRONGID(convert),
                    _ => base.ConvertFrom(context, culture, value),
                };
            }
        }