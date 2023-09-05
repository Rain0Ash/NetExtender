
        public class STRONGIDTypeConverter : System.ComponentModel.TypeConverter
        {
            public override System.Boolean CanConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Type? type)
            {
                return type == typeof(System.Guid) ||
                       type == typeof(System.Byte[]) ||
                       type == typeof(System.String) ||
                       base.CanConvertTo(context, type);
            }

            public override System.Object? ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, System.Object? value, System.Type type)
            {
                if (value is not STRONGID strong)
                {
                    return base.ConvertTo(context, culture, value, type);
                }

                if (type == typeof(System.Guid))
                {
                    return strong.Value;
                }

                if (type == typeof(System.Byte[]))
                {
                    return strong.Value.ToByteArray();
                }

                if (type == typeof(System.String))
                {
                    return strong.Value.ToString(null, culture);
                }

                return base.ConvertTo(context, culture, value, type);
            }

            public override System.Boolean CanConvertFrom(System.ComponentModel.ITypeDescriptorContext? context, System.Type type)
            {
                return type == typeof(System.Guid) ||
                       type == typeof(System.Byte[]) ||
                       type == typeof(System.String) ||
                       base.CanConvertFrom(context, type);
            }

            public override System.Object? ConvertFrom(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, System.Object value)
            {
                return value switch
                {
                    System.Guid result => new STRONGID(result)
                    System.Byte[] result => new STRONGID(new TYPE(result)),
                    System.String result when TYPE.TryParse(result, out TYPE convert) => new STRONGID(convert),
                    _ => base.ConvertFrom(context, culture, value),
                };
            }
        }