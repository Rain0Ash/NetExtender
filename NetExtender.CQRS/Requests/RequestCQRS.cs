using System;
using NetExtender.CQRS.Requests.Interfaces;
using NetExtender.Types.Enumerables.Interfaces;
using NetExtender.Types.Lists.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Requests
{
    public abstract record BusinessPaginationIdRequestCQRS<T, TResult> : BusinessPaginationIdRequestCQRS<T, TResult, IPaginationList<TResult>>, IBusinessPaginationIdRequestCQRS<T, TResult>
    {
        protected BusinessPaginationIdRequestCQRS()
        {
        }

        protected BusinessPaginationIdRequestCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record BusinessPaginationRequestCQRS<TResult> : BusinessPaginationRequestCQRS<TResult, IPaginationList<TResult>>, IBusinessPaginationRequestCQRS<TResult>
    {
    }

    public abstract record BusinessPaginationIdRequestCQRS<T, TResult, TCollection> : BusinessIdRequestCQRS<T, TCollection>, IBusinessPaginationIdRequestCQRS<T, TResult, TCollection> where TCollection : IPaginationEnumerable<TResult>
    {
        public UInt32 Page { get; init; } = 1;
        public UInt32 PageSize { get; init; }
        public UInt32 RequestLimit { get; init; } = 0;

        protected BusinessPaginationIdRequestCQRS()
        {
        }

        protected BusinessPaginationIdRequestCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record BusinessPaginationRequestCQRS<TResult, TCollection> : BusinessRequestCQRS<TCollection>, IBusinessPaginationRequestCQRS<TResult, TCollection> where TCollection : IPaginationEnumerable<TResult>
    {
        public UInt32 Page { get; init; } = 1;
        public UInt32 PageSize { get; init; }
        public UInt32 RequestLimit { get; init; } = 0;
    }

    public abstract record PaginationIdRequestCQRS<T, TResult> : PaginationIdRequestCQRS<T, TResult, IPaginationList<TResult>>, IPaginationIdRequestCQRS<T, TResult>
    {
        protected PaginationIdRequestCQRS()
        {
        }

        protected PaginationIdRequestCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record PaginationRequestCQRS<TResult> : PaginationRequestCQRS<TResult, IPaginationList<TResult>>, IPaginationRequestCQRS<TResult>
    {
    }

    public abstract record PaginationIdRequestCQRS<T, TResult, TCollection> : IdRequestCQRS<T, TCollection>, IPaginationIdRequestCQRS<T, TResult, TCollection> where TCollection : IPaginationEnumerable<TResult>
    {
        public UInt32 Page { get; init; } = 1;
        public UInt32 PageSize { get; init; }
        public UInt32 RequestLimit { get; init; } = 0;

        protected PaginationIdRequestCQRS()
        {
        }

        protected PaginationIdRequestCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record PaginationRequestCQRS<TResult, TCollection> : RequestCQRS<TCollection>, IPaginationRequestCQRS<TResult, TCollection> where TCollection : IPaginationEnumerable<TResult>
    {
        public UInt32 Page { get; init; } = 1;
        public UInt32 PageSize { get; init; }
        public UInt32 RequestLimit { get; init; } = 0;
    }

    public abstract record BusinessIdRequestCQRS<T, TResult> : IdRequestCQRS<T, BusinessResult<TResult>>, IBusinessIdRequestCQRS<T, TResult>
    {
        protected BusinessIdRequestCQRS()
        {
        }

        protected BusinessIdRequestCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record BusinessValueRequestCQRS<T, TResult> : ValueRequestCQRS<T, BusinessResult<TResult>>, IBusinessValueRequestCQRS<T, TResult>
    {
        protected BusinessValueRequestCQRS()
        {
        }

        protected BusinessValueRequestCQRS(T value)
            : base(value)
        {
        }
    }

    public abstract record BusinessRequestCQRS<TResult> : RequestCQRS<BusinessResult<TResult>>, IBusinessRequestCQRS<TResult>
    {
    }

    public abstract record BusinessIdRequestCQRS<T> : IdRequestCQRS<T, BusinessResult>, IBusinessIdRequestCQRS<T>
    {
        protected BusinessIdRequestCQRS()
        {
        }

        protected BusinessIdRequestCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record BusinessValueRequestCQRS<T> : ValueRequestCQRS<T, BusinessResult>, IBusinessValueRequestCQRS<T>
    {
        protected BusinessValueRequestCQRS()
        {
        }

        protected BusinessValueRequestCQRS(T value)
            : base(value)
        {
        }
    }

    public abstract record BusinessRequestCQRS : RequestCQRS<BusinessResult>, IBusinessRequestCQRS
    {
    }

    public abstract record IdRequestCQRS<T, TResult> : IdRequestCQRS<T>, IIdRequestCQRS<T, TResult>
    {
        protected IdRequestCQRS()
        {
        }

        protected IdRequestCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record ValueRequestCQRS<T, TResult> : ValueRequestCQRS<T>, IValueRequestCQRS<T, TResult>
    {
        protected ValueRequestCQRS()
        {
        }

        protected ValueRequestCQRS(T value)
            : base(value)
        {
        }
    }

    public abstract record RequestCQRS<TResult> : RequestCQRS, IRequestCQRS<TResult>
    {
    }

    public abstract record IdRequestCQRS<T> : IdEntityCQRS<T>, IIdRequestCQRS<T>
    {
        protected IdRequestCQRS()
        {
        }

        protected IdRequestCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record ValueRequestCQRS<T> : ValueEntityCQRS<T>, IValueRequestCQRS<T>
    {
        protected ValueRequestCQRS()
        {
        }

        protected ValueRequestCQRS(T value)
            : base(value)
        {
        }
    }

    public abstract record RequestCQRS : EntityCQRS, IRequestCQRS
    {
    }
}