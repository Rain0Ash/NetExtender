
        public class EntityFrameworkValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<STRONGID, UNDERLYING>
        {
            public EntityFrameworkValueConverter()
                : this(null)
            {
            }

            public EntityFrameworkValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mapping)
                : base(id => id.Value, value => new STRONGID(value), mapping)
            {
            }
        }