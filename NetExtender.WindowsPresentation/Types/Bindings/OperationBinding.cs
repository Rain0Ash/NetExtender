// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.WindowsPresentation.Types.Converters;

namespace NetExtender.WindowsPresentation.Types.Bindings
{
    public class OperationBinding : CustomBinding
    {
        public new OperationConverter Converter
        {
            get
            {
                return base.Converter as OperationConverter ?? (OperationConverter) (base.Converter = new OperationConverter());
            }
            set
            {
                base.Converter = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public ConverterMathOperation Operation
        {
            get
            {
                return Converter.Operation;
            }
            set
            {
                Converter.Operation = value;
            }
        }

        public OperationBinding()
        {
            Converter = new OperationConverter();
        }

        public OperationBinding(String path)
            : base(path)
        {
            Converter = new OperationConverter();
        }

        public OperationBinding(ConverterMathOperation operation)
        {
            Converter = new OperationConverter(operation);
        }

        public OperationBinding(String path, ConverterMathOperation operation)
            : base(path)
        {
            Converter = new OperationConverter(operation);
        }
    }

    public class EqualBinding : OperationBinding
    {
        private new const ConverterMathOperation Operation = ConverterMathOperation.Equal;
        
        public EqualBinding()
            : base(Operation)
        {
        }
        
        public EqualBinding(String path)
            : base(path, Operation)
        {
        }
    }

    public class NotEqualBinding : OperationBinding
    {
        private new const ConverterMathOperation Operation = ConverterMathOperation.NotEqual;
    
        public NotEqualBinding()
            : base(Operation)
        {
        }
    
        public NotEqualBinding(String path)
            : base(path, Operation)
        {
        }
    }

    public class LessBinding : OperationBinding
    {
        private new const ConverterMathOperation Operation = ConverterMathOperation.Less;
    
        public LessBinding()
            : base(Operation)
        {
        }
    
        public LessBinding(String path)
            : base(path, Operation)
        {
        }
    }

    public class LessOrEqualBinding : OperationBinding
    {
        private new const ConverterMathOperation Operation = ConverterMathOperation.LessOrEqual;
    
        public LessOrEqualBinding()
            : base(Operation)
        {
        }
    
        public LessOrEqualBinding(String path)
            : base(path, Operation)
        {
        }
    }

    public class GreaterBinding : OperationBinding
    {
        private new const ConverterMathOperation Operation = ConverterMathOperation.Greater;
    
        public GreaterBinding()
            : base(Operation)
        {
        }
    
        public GreaterBinding(String path)
            : base(path, Operation)
        {
        }
    }

    public class GreaterOrEqualBinding : OperationBinding
    {
        private new const ConverterMathOperation Operation = ConverterMathOperation.GreaterOrEqual;
    
        public GreaterOrEqualBinding()
            : base(Operation)
        {
        }
    
        public GreaterOrEqualBinding(String path)
            : base(path, Operation)
        {
        }
    }

    public class NotBinding : OperationBinding
    {
        private new const ConverterMathOperation Operation = ConverterMathOperation.Not;
    
        public NotBinding()
            : base(Operation)
        {
        }
    
        public NotBinding(String path)
            : base(path, Operation)
        {
        }
    }
}