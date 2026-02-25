using NetExtender.CQRS.Requests.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Requests
{
    public abstract record MultiFileBusinessIdRequestCQRS<T, TResult> : MultiFileIdRequestCQRS<T, BusinessResult<TResult>>, IBusinessIdRequestCQRS<T, TResult>
    {
        protected MultiFileBusinessIdRequestCQRS()
        {
        }

        protected MultiFileBusinessIdRequestCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record MultiFileBusinessRequestCQRS<TResult> : MultiFileRequestCQRS<BusinessResult<TResult>>, IBusinessRequestCQRS<TResult>
    {
    }

    public abstract record MultiFileBusinessIdRequestCQRS<T> : MultiFileIdRequestCQRS<T, BusinessResult>, IBusinessIdRequestCQRS<T>
    {
        protected MultiFileBusinessIdRequestCQRS()
        {
        }

        protected MultiFileBusinessIdRequestCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record MultiFileBusinessRequestCQRS : MultiFileRequestCQRS<BusinessResult>, IBusinessRequestCQRS
    {
    }

    public abstract record MultiFileIdRequestCQRS<T, TResult> : MultiFileIdRequestCQRS<T>, IIdRequestCQRS<T, TResult>
    {
        protected MultiFileIdRequestCQRS()
        {
        }

        protected MultiFileIdRequestCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record MultiFileRequestCQRS<TResult> : MultiFileRequestCQRS, IRequestCQRS<TResult>
    {
    }

    public abstract record MultiFileIdRequestCQRS<T> : MultiFileIdEntityCQRS<T>, IIdRequestCQRS<T>
    {
        protected MultiFileIdRequestCQRS()
        {
        }

        protected MultiFileIdRequestCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record MultiFileRequestCQRS : MultiFileEntityCQRS, IRequestCQRS
    {
    }

    public abstract record FileBusinessIdRequestCQRS<T, TResult> : FileIdRequestCQRS<T, BusinessResult<TResult>>, IBusinessIdRequestCQRS<T, TResult>
    {
        protected FileBusinessIdRequestCQRS()
        {
        }

        protected FileBusinessIdRequestCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record FileBusinessRequestCQRS<TResult> : FileRequestCQRS<BusinessResult<TResult>>, IBusinessRequestCQRS<TResult>
    {
    }

    public abstract record FileBusinessIdRequestCQRS<T> : FileIdRequestCQRS<T, BusinessResult>, IBusinessIdRequestCQRS<T>
    {
        protected FileBusinessIdRequestCQRS()
        {
        }

        protected FileBusinessIdRequestCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record FileBusinessRequestCQRS : FileRequestCQRS<BusinessResult>, IBusinessRequestCQRS
    {
    }

    public abstract record FileIdRequestCQRS<T, TResult> : FileIdRequestCQRS<T>, IIdRequestCQRS<T, TResult>
    {
        protected FileIdRequestCQRS()
        {
        }

        protected FileIdRequestCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record FileRequestCQRS<TResult> : FileRequestCQRS, IRequestCQRS<TResult>
    {
    }

    public abstract record FileIdRequestCQRS<T> : FileIdEntityCQRS<T>, IIdRequestCQRS<T>
    {
        protected FileIdRequestCQRS()
        {
        }

        protected FileIdRequestCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record FileRequestCQRS : FileEntityCQRS, IRequestCQRS
    {
    }
}