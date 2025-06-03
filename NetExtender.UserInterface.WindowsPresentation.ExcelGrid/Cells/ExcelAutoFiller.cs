// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Core;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public class ExcelAutoFiller
    {
        protected TryGetter<ExcelCell, Object?> Getter { get; }
        protected TrySetter<ExcelCell, Object?> Setter { get; }
        
        public ExcelAutoFiller(TryGetter<ExcelCell, Object?> getter, TrySetter<ExcelCell, Object?> setter)
        {
            Getter = getter ?? throw new ArgumentNullException(nameof(getter));
            Setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public virtual void AutoFill(ExcelCell current, ExcelCell selection, ExcelCell autofill)
        {
            for (Int32 row = Math.Min(current.Row, autofill.Row); row <= Math.Max(current.Row, autofill.Row); row++)
            {
                for (Int32 column = Math.Min(current.Column, autofill.Column); column <= Math.Max(current.Column, autofill.Column); column++)
                {
                    ExcelCell cell = new ExcelCell(column, row);
                    if (TryExtrapolate(cell, current, selection, autofill, out Object? value))
                    {
                        Setter(cell, value);
                    }
                }
            }
        }

        public virtual Boolean TryExtrapolate(ExcelCell cell, ExcelCell current, ExcelCell selection, ExcelCell autofill, [MaybeNullWhen(false)] out Object result)
        {
            Int32 mincolumn = Math.Min(current.Column, selection.Column);
            Int32 maxcolumn = Math.Max(current.Column, selection.Column);
            Int32 minrow = Math.Min(current.Row, selection.Row);
            Int32 maxrow = Math.Max(current.Row, selection.Row);

            Int32 row = cell.Row;
            Int32 column = cell.Column;

            if (column >= mincolumn && column <= maxcolumn && row >= minrow && row <= maxrow)
            {
                result = null;
                return false;
            }

            Object? value = null;
            if (row < minrow)
            {
                TryExtrapolate(cell, new ExcelCell(column, minrow), new ExcelCell(column, maxrow), out value);
            }

            if (row > maxrow)
            {
                TryExtrapolate(cell, new ExcelCell(column, minrow), new ExcelCell(column, maxrow), out value);
            }

            if (column < mincolumn)
            {
                TryExtrapolate(cell, new ExcelCell(mincolumn, row), new ExcelCell(maxcolumn, row), out value);
            }

            if (column > maxcolumn)
            {
                TryExtrapolate(cell, new ExcelCell(mincolumn, row), new ExcelCell(maxcolumn, row), out value);
            }
            
            result = value;
            
            if (value is not null)
            {
                result = value;
                return true;
            }
            
            cell = new ExcelCell(PeriodicClamp(column, mincolumn, maxcolumn), PeriodicClamp(row, minrow, maxrow));
            Getter.Invoke(cell, out result);
            return result is not null;
        }
        
        // ReSharper disable once CognitiveComplexity
        protected virtual Boolean TryExtrapolate(ExcelCell cell, ExcelCell start, ExcelCell end, [MaybeNullWhen(false)] out Object result)
        {
            try
            {
                if (!Getter(start, out Object? first) || first is null || !Getter(end, out Object? last) || last is null)
                {
                    result = null;
                    return false;
                }

                Int32 Δcolumn = end.Column - start.Column;
                Int32 Δrow = end.Row - start.Row;
                Double extrapolation = 0;
                
                if ((cell.Column < start.Column || cell.Column > end.Column) && Δcolumn > 0)
                {
                    extrapolation = 1.0 * (cell.Column - start.Column) / Δcolumn;
                }

                if ((cell.Row < start.Row || cell.Row > end.Row) && Δrow > 0)
                {
                    extrapolation = 1.0 * (cell.Row - start.Row) / Δrow;
                }

                if (extrapolation.Equals(0))
                {
                    result = first;
                    return true;
                }
                
                Type type = first.GetType();
                if (ReflectionOperator.Get(last.GetType(), type, BinaryOperator.Subtraction)?.Invoke(last, first) is { } subtract &&
                    ReflectionOperator.Get(subtract.GetType(), extrapolation.GetType(), BinaryOperator.Multiply)?.Invoke(subtract, extrapolation) is { } multiply &&
                    ReflectionOperator.Get(type, multiply.GetType(), BinaryOperator.Addition)?.Invoke(first, multiply) is { } addition)
                {
                    result = addition;
                    return true;
                }

                result = null;
                return false;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }
        
        protected static Int32 PeriodicClamp(Int32 value, Int32 min, Int32 max)
        {
            if (min >= max)
            {
                return min;
            }
            
            Int32 difference = max - min + 1;
            Int32 offset = (value - (max + 1)) % difference;
            
            if (offset < 0)
            {
                offset += difference;
            }
            
            return min + offset;
        }
    }
}