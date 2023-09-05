
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<STRONGID>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, STRONGID value)
            {
                if (parameter is null)
                {
                    throw new System.ArgumentNullException(nameof(parameter));
                }
                
                parameter.Value = value.Value;
            }

            public override STRONGID Parse(System.Object? value)
            {
                return value switch
                {
                    null => new STRONGID(),
                    MassTransit.NewId result => new STRONGID(result),
                    System.Guid result => new STRONGID(result),
                    System.Byte[] result => new STRONGID(new TYPE(result)),
                    System.String result when System.Guid.TryParse(result, out System.Guid convert) => new STRONGID(convert),
                    _ => throw new System.InvalidCastException($"Unable to cast object of type {value.GetType().Name} to STRONGID")
                };
            }
        }