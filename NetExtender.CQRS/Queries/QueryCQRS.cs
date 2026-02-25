using System;
using NetExtender.CQRS.Queries.Interfaces;
using NetExtender.Types.Enumerables.Interfaces;
using NetExtender.Types.Lists.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Queries
{
    public abstract record BusinessPaginationIdQueryCQRS<T, TResult> : BusinessPaginationIdQueryCQRS<T, TResult, IPaginationList<TResult>>, IBusinessPaginationIdQueryCQRS<T, TResult>
    {
        protected BusinessPaginationIdQueryCQRS()
        {
        }

        protected BusinessPaginationIdQueryCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record BusinessPaginationQueryCQRS<TResult> : BusinessPaginationQueryCQRS<TResult, IPaginationList<TResult>>, IBusinessPaginationQueryCQRS<TResult>
    {
    }

    public abstract record BusinessPaginationIdQueryCQRS<T, TResult, TCollection> : BusinessIdQueryCQRS<T, TCollection>, IBusinessPaginationIdQueryCQRS<T, TResult, TCollection> where TCollection : IPaginationEnumerable<TResult>
    {
        public UInt32 Page { get; init; } = 1;
        public UInt32 PageSize { get; init; }
        public UInt32 RequestLimit { get; init; } = 0;

        protected BusinessPaginationIdQueryCQRS()
        {
        }

        protected BusinessPaginationIdQueryCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record BusinessPaginationQueryCQRS<TResult, TCollection> : BusinessQueryCQRS<TCollection>, IBusinessPaginationQueryCQRS<TResult, TCollection> where TCollection : IPaginationEnumerable<TResult>
    {
        public UInt32 Page { get; init; } = 1;
        public UInt32 PageSize { get; init; }
        public UInt32 RequestLimit { get; init; } = 0;
    }

    public abstract record PaginationIdQueryCQRS<T, TResult> : PaginationIdQueryCQRS<T, TResult, IPaginationList<TResult>>, IPaginationIdQueryCQRS<T, TResult>
    {
        protected PaginationIdQueryCQRS()
        {
        }

        protected PaginationIdQueryCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record PaginationQueryCQRS<TResult> : PaginationQueryCQRS<TResult, IPaginationList<TResult>>, IPaginationQueryCQRS<TResult>
    {
    }

    public abstract record PaginationIdQueryCQRS<T, TResult, TCollection> : IdQueryCQRS<T, TCollection>, IPaginationIdQueryCQRS<T, TResult, TCollection> where TCollection : IPaginationEnumerable<TResult>
    {
        public UInt32 Page { get; init; } = 1;
        public UInt32 PageSize { get; init; }
        public UInt32 RequestLimit { get; init; } = 0;

        protected PaginationIdQueryCQRS()
        {
        }

        protected PaginationIdQueryCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record PaginationQueryCQRS<TResult, TCollection> : QueryCQRS<TCollection>, IPaginationQueryCQRS<TResult, TCollection> where TCollection : IPaginationEnumerable<TResult>
    {
        public UInt32 Page { get; init; } = 1;
        public UInt32 PageSize { get; init; }
        public UInt32 RequestLimit { get; init; } = 0;
    }

    public abstract record BusinessIdQueryCQRS<T, TResult> : IdQueryCQRS<T, BusinessResult<TResult>>, IBusinessIdQueryCQRS<T, TResult>
    {
        protected BusinessIdQueryCQRS()
        {
        }

        protected BusinessIdQueryCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record BusinessValueQueryCQRS<T, TResult> : ValueQueryCQRS<T, BusinessResult<TResult>>, IBusinessValueQueryCQRS<T, TResult>
    {
        protected BusinessValueQueryCQRS()
        {
        }

        protected BusinessValueQueryCQRS(T value)
            : base(value)
        {
        }
    }

    public abstract record BusinessQueryCQRS<TResult> : QueryCQRS<BusinessResult<TResult>>, IBusinessQueryCQRS<TResult>
    {
    }

    public abstract record IdQueryCQRS<T, TResult> : IdEntityCQRS<T>, IIdQueryCQRS<T, TResult>
    {
        protected IdQueryCQRS()
        {
        }

        protected IdQueryCQRS(T id)
            : base(id)
        {
        }
    }


    public abstract record ValueQueryCQRS<T, TResult> : ValueEntityCQRS<T>, IValueQueryCQRS<T, TResult>
    {
        protected ValueQueryCQRS()
        {
        }

        protected ValueQueryCQRS(T value)
            : base(value)
        {
        }
    }

    public abstract record QueryCQRS<TResult> : EntityCQRS, IQueryCQRS<TResult>
    {
    }
}