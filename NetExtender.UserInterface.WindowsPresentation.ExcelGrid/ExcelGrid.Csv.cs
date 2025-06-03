// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Text;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public partial class ExcelGrid
    {
        protected const String CsvSeparator = ";";
        
        private static String? CsvEncodeString(String? input)
        {
            if (input is null)
            {
                return null;
            }
            
            input = input.Replace("\"", "\"\"");
            if (input.Contains(';') || input.Contains('"'))
            {
                input = "\"" + input + "\"";
            }
            
            return input;
        }
        
        protected String ToCsv(ExcelCellRange range)
        {
            return ToCsv(range, CsvSeparator);
        }
        
        protected virtual String ToCsv(ExcelCellRange range, String? separator)
        {
            if (range is null)
            {
                throw new ArgumentNullException(nameof(range));
            }
            
            StringBuilder builder = new StringBuilder(2048);
            
            for (Int32 column = range.LeftColumn; column <= range.RightColumn; column++)
            {
                String? h = GetColumnHeader(column).ToString();
                h = CsvEncodeString(h);
                if (builder.Length > 0)
                {
                    builder.Append(separator);
                }
                
                builder.Append(h);
            }
            
            if (!TryGet(range, out String?[,]? strings))
            {
                return builder.ToString();
            }
            
            builder.AppendLine();
            String csv = ConvertToCsv(strings, separator, true);
            builder.Append(csv);
            return builder.ToString();
        }
        
        protected String ToCsv(ExcelCellRange range, Boolean header)
        {
            return ToCsv(range, CsvSeparator, header);
        }
        
        protected virtual String ToCsv(ExcelCellRange range, String? separator, Boolean header)
        {
            if (range is null)
            {
                throw new ArgumentNullException(nameof(range));
            }
            
            if (header)
            {
                return ToCsv(range, separator);
            }
            
            StringBuilder builder = new StringBuilder(2048);
            
            if (!TryGet(range, out String?[,]? strings))
            {
                return builder.ToString();
            }

            String csv = ConvertToCsv(strings, separator, true);
            builder.Append(csv);
            return builder.ToString();
        }
        
        protected String ConvertToCsv(String?[,] source, String? separator)
        {
            return ConvertToCsv(source, separator, false);
        }
        
        // ReSharper disable once CognitiveComplexity
        protected virtual String ConvertToCsv(String?[,] source, String? separator, Boolean encode)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            Int32 rows = source.GetLength(0);
            
            if (rows <= 0)
            {
                return String.Empty;
            }
            
            Int32 columns = source.GetLength(1);
            StringBuilder builder = new StringBuilder();
            
            if (encode)
            {
                WriteEncodeCsv(builder, source, 0, columns, separator);
                
                for (Int32 row = 1; row < rows; row++)
                {
                    builder.AppendLine();
                    WriteEncodeCsv(builder, source, row, columns, separator);
                }
            }
            else
            {
                WriteCsv(builder, source, 0, columns, separator);
                
                for (Int32 row = 1; row < rows; row++)
                {
                    builder.AppendLine();
                    WriteCsv(builder, source, row, columns, separator);
                }
            }
            
            return builder.ToString();
        }
        
        private static void WriteCsv(StringBuilder builder, String?[,] source, Int32 row, Int32 columns, String? separator)
        {
            if (columns <= 0)
            {
                return;
            }
            
            String? cell = source[row, 0];
            if (cell is not null)
            {
                builder.Append(cell);
            }
            
            for (Int32 column = 1; column < columns; column++)
            {
                builder.Append(separator);
                
                cell = source[row, column];
                if (cell is not null)
                {
                    builder.Append(cell);
                }
            }
        }
        
        private static void WriteEncodeCsv(StringBuilder builder, String?[,] source, Int32 row, Int32 columns, String? separator)
        {
            if (columns <= 0)
            {
                return;
            }
            
            String? cell = CsvEncodeString(source[row, 0]);
            if (cell is not null)
            {
                builder.Append(cell);
            }
            
            for (Int32 column = 1; column < columns; column++)
            {
                builder.Append(separator);
                
                cell = source[row, column];
                if (cell is not null)
                {
                    builder.Append(cell);
                }
            }
        }
    }
}