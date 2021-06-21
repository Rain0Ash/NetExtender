// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Interfaces
{
    public interface IMultiValidable : IMultiValidable<Object?>
    {
    }
    
    public interface IMultiValidable<T> : IBaseValidable
    {
        public Func<T?, Boolean> ValidateItem { get; set; }
        
        public new Boolean IsValid
        {
            get
            {
                // ReSharper disable once SuspiciousTypeConversion.Global
                if (this is IEnumerable<T?> enumerable)
                {
                    return enumerable.All(IsValidItem);
                }
            
                throw new NotImplementedException();
            }
        }

        public Boolean IsValidItem(T? item)
        {
            return ValidateItem?.Invoke(item) != false;
        }

        public Boolean IsValidIndex(Int32 index)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (this is IEnumerable<T> enumerable)
            {
                return IsValidItem(enumerable.ElementAt(index));
            }
            
            throw new NotSupportedException();
        }
    }
}