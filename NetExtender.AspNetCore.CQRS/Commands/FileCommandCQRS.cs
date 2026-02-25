using NetExtender.CQRS.Commands.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Commands
{
    public abstract record MultiFileBusinessIdCommandCQRS<T, TResult> : MultiFileIdCommandCQRS<T, BusinessResult<TResult>>, IBusinessIdCommandCQRS<T, TResult>
    {
        protected MultiFileBusinessIdCommandCQRS()
        {
        }

        protected MultiFileBusinessIdCommandCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record MultiFileBusinessCommandCQRS<TResult> : MultiFileCommandCQRS<BusinessResult<TResult>>, IBusinessCommandCQRS<TResult>
    {
    }

    public abstract record MultiFileBusinessIdCommandCQRS<T> : MultiFileIdCommandCQRS<T, BusinessResult>, IBusinessIdCommandCQRS<T>
    {
        protected MultiFileBusinessIdCommandCQRS()
        {
        }

        protected MultiFileBusinessIdCommandCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record MultiFileBusinessCommandCQRS : MultiFileCommandCQRS<BusinessResult>, IBusinessCommandCQRS
    {
    }

    public abstract record MultiFileIdCommandCQRS<T, TResult> : MultiFileIdCommandCQRS<T>, IIdCommandCQRS<T, TResult>
    {
        protected MultiFileIdCommandCQRS()
        {
        }

        protected MultiFileIdCommandCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record MultiFileCommandCQRS<TResult> : MultiFileCommandCQRS, ICommandCQRS<TResult>
    {
    }

    public abstract record MultiFileIdCommandCQRS<T> : MultiFileIdEntityCQRS<T>, IIdCommandCQRS<T>
    {
        protected MultiFileIdCommandCQRS()
        {
        }

        protected MultiFileIdCommandCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record MultiFileCommandCQRS : MultiFileEntityCQRS, ICommandCQRS
    {
    }

    public abstract record FileBusinessIdCommandCQRS<T, TResult> : FileIdCommandCQRS<T, BusinessResult<TResult>>, IBusinessIdCommandCQRS<T, TResult>
    {
        protected FileBusinessIdCommandCQRS()
        {
        }

        protected FileBusinessIdCommandCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record FileBusinessCommandCQRS<TResult> : FileCommandCQRS<BusinessResult<TResult>>, IBusinessCommandCQRS<TResult>
    {
    }

    public abstract record FileBusinessIdCommandCQRS<T> : FileIdCommandCQRS<T, BusinessResult>, IBusinessIdCommandCQRS<T>
    {
        protected FileBusinessIdCommandCQRS()
        {
        }

        protected FileBusinessIdCommandCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record FileBusinessCommandCQRS : FileCommandCQRS<BusinessResult>, IBusinessCommandCQRS
    {
    }

    public abstract record FileIdCommandCQRS<T, TResult> : FileIdCommandCQRS<T>, IIdCommandCQRS<T, TResult>
    {
        protected FileIdCommandCQRS()
        {
        }

        protected FileIdCommandCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record FileCommandCQRS<TResult> : FileCommandCQRS, ICommandCQRS<TResult>
    {
    }

    public abstract record FileIdCommandCQRS<T> : FileIdEntityCQRS<T>, IIdCommandCQRS<T>
    {
        protected FileIdCommandCQRS()
        {
        }

        protected FileIdCommandCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record FileCommandCQRS : FileEntityCQRS, ICommandCQRS
    {
    }
}