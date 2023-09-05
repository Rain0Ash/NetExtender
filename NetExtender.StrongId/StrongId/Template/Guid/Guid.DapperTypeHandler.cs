
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
                    System.Guid result => new STRONGID(result)
                    System.Byte[] result => new STRONGID(new TYPE(result)),
                    System.String result when TYPE.TryParse(result, out TYPE convert) => new STRONGID(convert),
                    _ => throw new System.InvalidCastException($"Unable to cast object of type {value.GetType().Name} to STRONGID")
                };
            }
        }