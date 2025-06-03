// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Spans
{
    public readonly ref struct PaginationObserver<T>
    {
        public static implicit operator ReadOnlyPaginationObserver<T>(PaginationObserver<T> value)
        {
            return new ReadOnlyPaginationObserver<T>(value.Internal, value.Index, value.Size, value.Total);
        }
        
        public static implicit operator ReadOnlySpan<T>(PaginationObserver<T> value)
        {
            return value.View;
        }
        
        public static implicit operator Span<T>(PaginationObserver<T> value)
        {
            return value.View;
        }
        
        private readonly Span<T> Internal;
        public readonly Span<T> View;
        
        public Int32 Index { get; }
        
        public Int32 Page
        {
            get
            {
                return Index + 1;
            }
        }
        
        public Int32 Total { get; }
        public Int32 Items { get; }
        public Int32 Size { get; }
        
        public Int32 Count
        {
            get
            {
                return Internal.Length;
            }
        }
        
        public Boolean HasPrevious
        {
            get
            {
                return Index > 0;
            }
        }
        
        public Boolean HasNext
        {
            get
            {
                return Page < Total;
            }
        }
        
        public PaginationObserver<T> Start
        {
            get
            {
                return new PaginationObserver<T>(Internal, 0, Size, Total);
            }
        }
        
        public PaginationObserver<T> End
        {
            get
            {
                return new PaginationObserver<T>(Internal, Total - 1, Size, Total);
            }
        }
        
        public PaginationObserver<T> Previous
        {
            get
            {
                return new PaginationObserver<T>(Internal, Index - 1, Size, Total);
            }
        }
        
        public PaginationObserver<T> Next
        {
            get
            {
                return new PaginationObserver<T>(Internal, Index + 1, Size, Total);
            }
        }
        
        public PaginationObserver(Span<T> @internal, Int32 size)
            : this(@internal, 0, size)
        {
        }
        
        public PaginationObserver(Span<T> @internal, Int32 index, Int32 size)
        {
            Internal = @internal;
            Index = index >= 0 ? index : throw new ArgumentOutOfRangeException(nameof(index), index, null);
            Size = size > 0 ? size : throw new ArgumentOutOfRangeException(nameof(size), size, null);
            
            Int32 start = Index * Size;
            Items = Math.Max(Math.Min(Size, Internal.Length - start), 0);
            Total = Math.Abs((Int32) Math.Ceiling(@internal.Length / (Double) Size));
            View = start >= 0 && start < Internal.Length && Items > 0 ? Internal.Slice(start, Items) : Span<T>.Empty;
        }
        
        internal PaginationObserver(Span<T> @internal, Int32 index, Int32 size, Int32 total)
        {
            Internal = @internal;
            Index = index;
            Size = size;
            Total = total;
            
            Int32 start = Index * Size;
            Items = Math.Max(Math.Min(Size, Internal.Length - start), 0);
            View = start >= 0 && start < Internal.Length && Items > 0 ? Internal.Slice(start, Items) : Span<T>.Empty;
        }
        
        public PaginationObserver<T> Resize(Int32 size)
        {
            return Resize(size, false);
        }
        
        public PaginationObserver<T> Resize(Int32 size, Boolean resize)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), size, null);
            }
            
            return resize ? new PaginationObserver<T>(Internal, Math.Clamp(Index * Size / size, 0, Total - 1), size) : new PaginationObserver<T>(Internal, size);
        }
        
        public Boolean MovePrevious(ref PaginationObserver<T> observer)
        {
            if (!HasPrevious)
            {
                return false;
            }
            
            observer = Previous;
            return true;
        }
        
        public Boolean MoveNext(ref PaginationObserver<T> observer)
        {
            if (!HasNext)
            {
                return false;
            }
            
            observer = Next;
            return true;
        }
        
        public ReadOnlySpan<T> AsReadOnlySpan()
        {
            return this;
        }
        
        public Span<T> AsSpan()
        {
            return this;
        }
        
        public void CopyTo(Span<T> destination)
        {
            View.CopyTo(destination);
        }
        
        public Boolean TryCopyTo(Span<T> destination)
        {
            return View.TryCopyTo(destination);
        }
        
        public T[] ToArray()
        {
            return View.ToArray();
        }
        
        public Span<T>.Enumerator GetEnumerator()
        {
            return View.GetEnumerator();
        }
        
        public T this[Int32 index]
        {
            get
            {
                return View[index];
            }
            set
            {
                View[index] = value;
            }
        }
        
        public T this[Index index]
        {
            get
            {
                return View[index];
            }
            set
            {
                View[index] = value;
            }
        }
    }
}