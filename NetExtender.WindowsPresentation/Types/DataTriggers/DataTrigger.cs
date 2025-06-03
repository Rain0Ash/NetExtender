// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;

namespace NetExtender.WindowsPresentation.Types.DataTriggers
{
    public class NullDataTrigger : DataTrigger
    {
        public NullDataTrigger()
        {
            Value = null;
        }
    }
    
    public class TrueDataTrigger : ValueDataTrigger<Boolean>
    {
        public TrueDataTrigger()
            : base(true)
        {
        }
    }
    
    public class FalseDataTrigger : ValueDataTrigger<Boolean>
    {
        public FalseDataTrigger()
            : base(false)
        {
        }
    }

    public class ValueDataTrigger<T> : DataTrigger
    {
        public ValueDataTrigger(T value)
        {
            Value = value;
        }
    }
}