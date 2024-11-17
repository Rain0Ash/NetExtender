using System;
using System.ComponentModel;
using System.Globalization;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    [TypeConverter(typeof(ExcelCellConverter))]
    public readonly struct ExcelCell : IEquatable<ExcelCell>
    {
        public static Boolean operator ==(ExcelCell left, ExcelCell right)
        {
            return left.Equals(right);
        }
        
        public static Boolean operator !=(ExcelCell left, ExcelCell right)
        {
            return !(left == right);
        }
        
        public Int32 Column { get; }
        public Int32 Row { get; }
        
        public Boolean IsEmpty
        {
            get
            {
                return Column < 0 || Row < 0;
            }
        }
        
        public ExcelCell(Int32 column, Int32 row)
        {
            Column = column;
            Row = row;
        }
        
        public static String ToColumn(Int32 column)
        {
            const Int32 alphabet = 'Z' - 'A' + 1;
            String result = String.Empty;
            while (column >= alphabet)
            {
                Int32 i = column / alphabet;
                result += ((Char) ('A' + i - 1)).ToString(CultureInfo.InvariantCulture);
                column -= i * alphabet;
            }
            
            result += ((Char) ('A' + column)).ToString(CultureInfo.InvariantCulture);
            return result;
        }
        
        public static String ToRow(Int32 row)
        {
            return (row + 1).ToString(CultureInfo.InvariantCulture);
        }
        
        public override Int32 GetHashCode()
        {
            return unchecked((Int32) (((Int64) Column << 16) + Row));
        }

        public override Boolean Equals(Object? other)
        {
            return other is ExcelCell cell && Equals(cell);
        }

        public Boolean Equals(ExcelCell other)
        {
            return Column == other.Column && Row == other.Row;
        }

        public override String ToString()
        {
            return $"{ToColumn(Column)}{Row + 1}";
        }
    }
}