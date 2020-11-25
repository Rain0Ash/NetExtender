// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace NetExtender.DataBase
{
    public class SQLiteWrapperReader : IDisposable
    {
        private readonly SqliteDataReader _reader;
        private readonly Dictionary<String, Int32> _ordinal;

        public SQLiteWrapperReader(SqliteDataReader reader)
        {
            _reader = reader;
            _ordinal = new Dictionary<String, Int32>();
        }

        public Boolean Read()
        {
            return _reader.Read();
        }

        public Object GetValue(String column)
        {
            return _reader.GetValue(GetOrdinal(column));
        }

        public Boolean GetBoolean(String column)
        {
            return _reader.GetBoolean(GetOrdinal(column));
        }

        public Boolean GetBoolean(String column, Boolean fromString)
        {
            if (!fromString)
            {
                return GetBoolean(column);
            }

            if (IsDBNull(column))
            {
                return false;
            }

            String value = GetString(column);
            return value == "t" || value == "1";
        }

        public Byte GetByte(String column)
        {
            return _reader.GetByte(GetOrdinal(column));
        }

        public Byte GetByte(String column, Byte defaultValue)
        {
            Int32 ordinal = GetOrdinal(column);
            return _reader.IsDBNull(ordinal) ? defaultValue : _reader.GetByte(ordinal);
        }

        public Int64 GetBytes(String column, Int64 fieldOffset, Byte[] buffer, Int32 bufferOffset, Int32 length)
        {
            return _reader.GetBytes(GetOrdinal(column), fieldOffset, buffer, bufferOffset, length);
        }

        public Char GetChar(String column)
        {
            return _reader.GetChar(GetOrdinal(column));
        }

        public Int64 GetChars(String column, Int64 fieldOffset, Char[] buffer, Int32 bufferOffset, Int32 length)
        {
            return _reader.GetChars(GetOrdinal(column), fieldOffset, buffer, bufferOffset, length);
        }

        public Guid GetGuid(String column)
        {
            return _reader.GetGuid(GetOrdinal(column));
        }

        public Int16 GetInt16(String column)
        {
            return _reader.GetInt16(GetOrdinal(column));
        }

        public UInt16 GetUInt16(String column)
        {
            return (UInt16) GetInt16(column);
        }

        public Int32 GetInt32(String column)
        {
            return _reader.GetInt32(GetOrdinal(column));
        }

        public Int32 GetInt32(String column, Int32 defaultValue)
        {
            Int32 ordinal = GetOrdinal(column);
            return _reader.IsDBNull(ordinal) ? defaultValue : _reader.GetInt32(ordinal);
        }

        public UInt32 GetUInt32(String column)
        {
            return (UInt32) GetInt32(column);
        }

        public UInt32 GetUInt32(String column, UInt32 defaultValue)
        {
            Int32 ordinal = GetOrdinal(column);
            if (_reader.IsDBNull(ordinal))
            {
                return defaultValue;
            }

            return (UInt32) GetInt32(column);
        }

        public Int64 GetInt64(String column)
        {
            return _reader.GetInt64(GetOrdinal(column));
        }

        public UInt64 GetUInt64(String column)
        {
            return (UInt64) GetInt64(column);
        }

        public Single GetFloat(String column)
        {
            return _reader.GetFloat(GetOrdinal(column));
        }

        public Single GetFloat(String column, Single defaultValue)
        {
            Int32 ordinal = GetOrdinal(column);
            return _reader.IsDBNull(ordinal) ? defaultValue : _reader.GetFloat(ordinal);
        }

        public Double GetDouble(String column)
        {
            return _reader.GetDouble(GetOrdinal(column));
        }

        public String GetString(String column)
        {
            return _reader.GetString(GetOrdinal(column));
        }

        public String GetString(String column, String defaultValue)
        {
            Int32 ordinal = GetOrdinal(column);
            return _reader.IsDBNull(ordinal) ? defaultValue : _reader.GetString(ordinal);
        }

        public Decimal GetDecimal(String column)
        {
            return _reader.GetDecimal(GetOrdinal(column));
        }

        public DateTime GetDateTime(String column)
        {
            return _reader.GetDateTime(GetOrdinal(column));
        }

        public Boolean IsDBNull(String column)
        {
            return _reader.IsDBNull(GetOrdinal(column));
        }

        public Int32 GetOrdinal(String column)
        {
            if (_ordinal.ContainsKey(column))
            {
                return _ordinal[column];
            }

            Int32 ordinal = _reader.GetOrdinal(column);
            _ordinal.Add(column, ordinal);
            return ordinal;
        }

        public void Dispose()
        {
            _ordinal.Clear();
            _reader.Dispose();
        }
    }
}