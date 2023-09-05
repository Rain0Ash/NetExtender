﻿
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
                    null => new STRONGID(null),
                    System.SByte result => new STRONGID(result),
                    System.Byte result => new STRONGID(result),
                    System.Int16 result => new STRONGID(result),
                    System.UInt16 result => new STRONGID(result),
                    System.Int32 result => new STRONGID(result),
                    System.UInt32 result => new STRONGID(result),
                    System.Int64 result => new STRONGID(result),
                    System.UInt64 result => new STRONGID(result),
                    System.Byte[] result => new STRONGID(new TYPE(result)),
                    System.String result when TYPE.TryParse(result, System.Globalization.NumberStyles.Any, null, out TYPE convert) => new STRONGID(convert),
                    _ => throw new System.InvalidCastException($"Unable to cast object of type {value.GetType().Name} to STRONGID")
                };
            }
        }