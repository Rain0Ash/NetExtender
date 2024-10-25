using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Events;

namespace NetExtender.WindowsPresentation.Types.Clipboard
{
    public delegate void ClipboardChangedEventHandler(Object? sender, ClipboardChangedEventArgs args);
    
    public sealed class ClipboardChangedEventArgs : HandledEventArgs<ClipboardObject>, IEquatable<ClipboardType>, IEquatable<ClipboardObject>, IEquatable<ClipboardSource>, IEquatable<ClipboardChangedEventArgs>
    {
        public static implicit operator Boolean(ClipboardChangedEventArgs? value)
        {
            return value?.IsEmpty is false;
        }
        
        public static implicit operator ClipboardType(ClipboardChangedEventArgs? value)
        {
            return value?.Type ?? ClipboardType.NoContent;
        }
        
        public static implicit operator ClipboardObject(ClipboardChangedEventArgs? value)
        {
            return value?.Data ?? default;
        }
        
        public static implicit operator ClipboardSource(ClipboardChangedEventArgs? value)
        {
            return value?.Source ?? default;
        }
        
        public ClipboardType Type
        {
            get
            {
                return Data.Type;
            }
        }
        
        [SuppressMessage("Design", "CA1061")]
        public new Object? Value
        {
            get
            {
                return Data.Value;
            }
            private set
            {
                Data = new ClipboardObject(Data.Type, value);
            }
        }
        
        public ClipboardObject Data
        {
            get
            {
                return base.Value;
            }
            private set
            {
                base.Value = value;
            }
        }
        
        public ClipboardSource Source { get; }
        
        public Boolean IsEmpty
        {
            get
            {
                return Data.IsEmpty;
            }
        }
        
        public ClipboardChangedEventArgs(ClipboardSource source, ClipboardObject value)
            : base(value)
        {
            Source = source;
        }
        
        public ClipboardChangedEventArgs(ClipboardSource source, ClipboardObject value, Boolean handled)
            : base(value, handled)
        {
            Source = source;
        }
        
        public ClipboardChangedEventArgs Materialize()
        {
            if (Value is Func<Object?> getter)
            {
                Value = getter.Invoke();
            }
            
            return this;
        }
        
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Source, Data);
        }
        
        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => false,
                ClipboardChangedEventArgs value => Equals(value),
                ClipboardObject value => Equals(value),
                ClipboardSource value => Equals(value),
                ClipboardType value => Equals(value),
                _ => Equals(Value, other)
            };
        }
        
        public Boolean Equals(ClipboardType other)
        {
            return Type == other;
        }
        
        public Boolean Equals(ClipboardObject other)
        {
            return Data == other;
        }
        
        public Boolean Equals(ClipboardSource other)
        {
            return Source == other;
        }
        
        public Boolean Equals(ClipboardChangedEventArgs? other)
        {
            return Equals(Source) && Equals(Data);
        }
        
        public override String? ToString()
        {
            return Data.ToString();
        }
    }
}