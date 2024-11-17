using System;
using System.Reflection;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Interception.Interfaces;

namespace NetExtender.Types.Interception
{
    public abstract class MemberInterceptEventArgs<TMember, T> : MemberInterceptEventArgs<T>, IMemberInterceptEventArgs<TMember> where T : IMemberInterceptArgumentInfo<TMember> where TMember : MemberInfo
    {
        protected TMember Member
        {
            get
            {
                return Info.Member;
            }
        }

        TMember IMemberInterceptEventArgs<TMember>.Member
        {
            get
            {
                return Member;
            }
        }
        
        protected MemberInterceptEventArgs(T value)
            : base(value)
        {
        }
    }
    
    public abstract class MemberInterceptEventArgs<T> : InterceptEventArgs<T>, IMemberInterceptEventArgs where T : IMemberInterceptArgumentInfo
    {
        Object? ISimpleInterceptEventArgs.Value
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public Boolean IsSeal { get; private set; }

        public Boolean IsIgnore
        {
            get
            {
                return ReferenceEquals(Exception, IgnoreException.Instance);
            }
        }

        public override Boolean IsCancel
        {
            get
            {
                return IsSeal || !ReferenceEquals(Exception, Info.Exception);
            }
        }
        
        protected MemberInterceptEventArgs(T value)
            : base(value)
        {
        }

        protected internal abstract void Intercept(Exception exception);

        void ISimpleInterceptEventArgs.Intercept(Exception exception)
        {
            Intercept(exception);
        }

        public virtual void Ignore()
        {
            Exception = IgnoreException.Instance;
        }

        protected void Seal()
        {
            IsSeal = true;
        }

        protected internal void Unseal()
        {
            IsSeal = false;
        }

        [Serializable]
        private sealed class IgnoreException : SuccessfulOperationException
        {
            public static IgnoreException Instance { get; } = new IgnoreException();

            private IgnoreException()
            {
            }
        }
    }
}