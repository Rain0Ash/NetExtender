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