using NetExtender.CQRS.Commands.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Commands
{
    public abstract record BusinessIdCommandCQRS<T, TResult> : IdCommandCQRS<T, BusinessResult<TResult>>, IBusinessIdCommandCQRS<T, TResult>
    {
        protected BusinessIdCommandCQRS()
        {
        }

        protected BusinessIdCommandCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record BusinessValueCommandCQRS<T, TResult> : ValueCommandCQRS<T, BusinessResult<TResult>>, IBusinessValueCommandCQRS<T, TResult>
    {
        protected BusinessValueCommandCQRS()
        {
        }

        protected BusinessValueCommandCQRS(T value)
            : base(value)
        {
        }
    }

    public abstract record BusinessCommandCQRS<TResult> : CommandCQRS<BusinessResult<TResult>>, IBusinessCommandCQRS<TResult>
    {
    }

    public abstract record BusinessIdCommandCQRS<T> : IdCommandCQRS<T, BusinessResult>, IBusinessIdCommandCQRS<T>
    {
        protected BusinessIdCommandCQRS()
        {
        }

        protected BusinessIdCommandCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record BusinessValueCommandCQRS<T> : ValueCommandCQRS<T, BusinessResult>, IBusinessValueCommandCQRS<T>
    {
        protected BusinessValueCommandCQRS()
        {
        }

        protected BusinessValueCommandCQRS(T value)
            : base(value)
        {
        }
    }

    public abstract record BusinessCommandCQRS : CommandCQRS<BusinessResult>, IBusinessCommandCQRS
    {
    }

    public abstract record IdCommandCQRS<T, TResult> : IdCommandCQRS<T>, IIdCommandCQRS<T, TResult>
    {
        protected IdCommandCQRS()
        {
        }

        protected IdCommandCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record ValueCommandCQRS<T, TResult> : ValueCommandCQRS<T>, IValueCommandCQRS<T, TResult>
    {
        protected ValueCommandCQRS()
        {
        }

        protected ValueCommandCQRS(T value)
            : base(value)
        {
        }
    }

    public abstract record CommandCQRS<TResult> : CommandCQRS, ICommandCQRS<TResult>
    {
    }

    public abstract record IdCommandCQRS<T> : IdEntityCQRS<T>, IIdCommandCQRS<T>
    {
        protected IdCommandCQRS()
        {
        }

        protected IdCommandCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record ValueCommandCQRS<T> : ValueEntityCQRS<T>, IValueCommandCQRS<T>
    {
        protected ValueCommandCQRS()
        {
        }

        protected ValueCommandCQRS(T value)
            : base(value)
        {
        }
    }

    public abstract record CommandCQRS : EntityCQRS, ICommandCQRS
    {
    }
}