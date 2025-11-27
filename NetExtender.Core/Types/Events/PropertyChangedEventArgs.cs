using System;
using System.ComponentModel;
using NetExtender.Types.Monads;

namespace NetExtender.Types.Events
{
    public class PropertyChangedEventArgs<T> : PropertyValueChangedEventArgs<T>
    {
        public PropertyChangedEventArgs(String? property, T old, T @new)
            : base(property, old, @new)
        {
        }
    }

    public abstract class PropertyValueChangedEventArgs<T> : PropertyValueChangedEventArgs
    {
        private Maybe<Object?> _old;
        private Maybe<Object?> _new;

        public new T Old { get; }
        public new T New { get; }

        public PropertyValueChangedEventArgs(String? property, T old, T @new)
            : base(property)
        {
            Old = old;
            New = @new;
        }

        protected sealed override Object? GetOld()
        {
            if (!_old)
            {
                _old = new Maybe<Object?>(Old);
            }

            return _old.Internal;
        }

        protected sealed override Object? GetNew()
        {
            if (!_new)
            {
                _new = new Maybe<Object?>(New);
            }

            return _new.Internal;
        }
    }
    
    public abstract class PropertyValueChangedEventArgs : PropertyChangedEventArgs
    {
        public Object? Old
        {
            get
            {
                return GetOld();
            }
        }

        public Object? New
        {
            get
            {
                return GetNew();
            }
        }

        protected PropertyValueChangedEventArgs(String? property)
            : base(property)
        {
        }

        protected abstract Object? GetOld();
        protected abstract Object? GetNew();
    }
}