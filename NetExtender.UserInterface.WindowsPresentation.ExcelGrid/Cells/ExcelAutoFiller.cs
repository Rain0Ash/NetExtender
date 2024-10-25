using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Core;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public class ExcelAutoFiller
    {
        private TryGetter<ExcelCell, Object?> Getter { get; }
        private TrySetter<ExcelCell, Object?> Setter { get; }
        
        public ExcelAutoFiller(TryGetter<ExcelCell, Object?> getter, TrySetter<ExcelCell, Object?> setter)
        {
            Getter = getter ?? throw new ArgumentNullException(nameof(getter));
            Setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public void AutoFill(ExcelCell current, ExcelCell selection, ExcelCell autofill)
        {
            for (Int32 i = Math.Min(current.Row, autofill.Row); i <= Math.Max(current.Row, autofill.Row); i++)
            {
                for (Int32 j = Math.Min(current.Column, autofill.Column); j <= Math.Max(current.Column, autofill.Column); j++)
                {
                    ExcelCell cell = new ExcelCell(i, j);
                    if (TryExtrapolate(cell, current, selection, autofill, out Object? value))
                    {
                        Setter(cell, value);
                    }
                }
            }
        }

        public Boolean TryExtrapolate(ExcelCell cell, ExcelCell current, ExcelCell selection, ExcelCell autofill, [MaybeNullWhen(false)] out Object result)
        {
            Int32 minrow = Math.Min(current.Row, selection.Row);
            Int32 maxrow = Math.Max(current.Row, selection.Row);
            Int32 mincolumn = Math.Min(current.Column, selection.Column);
            Int32 maxcolumn = Math.Max(current.Column, selection.Column);

            Int32 i = cell.Row;
            Int32 j = cell.Column;

            if (i >= minrow && i <= maxrow && j >= mincolumn && j <= maxcolumn)
            {
                result = null;
                return false;
            }

            Object? value = null;
            if (i < minrow)
            {
                TryExtrapolate(cell, new ExcelCell(minrow, j), new ExcelCell(maxrow, j), out value);
            }

            if (i > maxrow)
            {
                TryExtrapolate(cell, new ExcelCell(minrow, j), new ExcelCell(maxrow, j), out value);
            }

            if (j < mincolumn)
            {
                TryExtrapolate(cell, new ExcelCell(i, mincolumn), new ExcelCell(i, maxcolumn), out value);
            }

            if (j > maxcolumn)
            {
                TryExtrapolate(cell, new ExcelCell(i, mincolumn), new ExcelCell(i, maxcolumn), out value);
            }
            
            result = value;
            
            if (value is not null)
            {
                result = value;
                return true;
            }
            
            cell = new ExcelCell(PeriodicClamp(i, minrow, maxrow), PeriodicClamp(j, mincolumn, maxcolumn));
            Getter.Invoke(cell, out result);
            return result is not null;
        }

        private static Int32 PeriodicClamp(Int32 value, Int32 min, Int32 max)
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
        
        // ReSharper disable once CognitiveComplexity
        private Boolean TryExtrapolate(ExcelCell cell, ExcelCell start, ExcelCell end, [MaybeNullWhen(false)] out Object result)
        {
            try
            {
                if (!Getter(start, out Object? first) || first is null || Getter(end, out Object? last) || last is null)
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
                if (ReflectionOperator.Get(last.GetType(), type, BinaryOperator.Subtraction)?.Invoke(last, first) is not { } subtract)
                {
                    result = null;
                    return false;
                }
                
                if (ReflectionOperator.Get(subtract.GetType(), extrapolation.GetType(), BinaryOperator.Multiply)?.Invoke(subtract, extrapolation) is not { } multiply)
                {
                    result = null;
                    return false;
                }
                
                if (ReflectionOperator.Get(type, multiply.GetType(), BinaryOperator.Addition)?.Invoke(first, multiply) is not { } addition)
                {
                    result = null;
                    return false;
                }
                
                result = addition;
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }
    }
}