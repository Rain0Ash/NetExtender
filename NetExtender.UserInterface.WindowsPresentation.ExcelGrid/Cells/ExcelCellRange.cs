﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public class ExcelCellRange : IEquatable<ExcelCellRange>
    {
        public ExcelCell TopLeft { get; }
        public ExcelCell BottomRight { get; }
        
        public Int32 Columns
        {
            get
            {
                return RightColumn - LeftColumn + 1;
            }
        }
        
        public Int32 Rows
        {
            get
            {
                return BottomRow - TopRow + 1;
            }
        }
        
        public Int32 TopRow
        {
            get
            {
                return TopLeft.Row;
            }
        }
        
        public Int32 BottomRow
        {
            get
            {
                return BottomRight.Row;
            }
        }
        
        public Int32 LeftColumn
        {
            get
            {
                return TopLeft.Column;
            }
        }
        
        public Int32 RightColumn
        {
            get
            {
                return BottomRight.Column;
            }
        }
        
        public ExcelCellRange(ExcelCell first, ExcelCell second)
        {
            TopLeft = new ExcelCell(Math.Min(first.Column, second.Column), Math.Min(first.Row, second.Row));
            BottomRight = new ExcelCell(Math.Max(first.Column, second.Column), Math.Max(first.Row, second.Row));
        }
        
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(TopLeft, BottomRight);
        }
        
        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                ExcelCellRange range => Equals(range),
                _ => false
            };
        }
        
        public Boolean Equals(ExcelCellRange? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            
            return TopLeft.Equals(other.TopLeft) && BottomRight.Equals(other.BottomRight);
        }
        
        public override String ToString()
        {
            return $"{TopLeft}:{BottomRight}";
        }
    }
}