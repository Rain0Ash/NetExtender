// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Spans
{
    public readonly ref struct ReadOnlyPaginationObserver<T>
    {
        public static implicit operator ReadOnlySpan<T>(ReadOnlyPaginationObserver<T> value)
        {
            return value.View;
        }
        
        private readonly ReadOnlySpan<T> Internal;
        public readonly ReadOnlySpan<T> View;
        
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
        
        public ReadOnlyPaginationObserver<T> Start
        {
            get
            {
                return new ReadOnlyPaginationObserver<T>(Internal, 0, Size, Total);
            }
        }
        
        public ReadOnlyPaginationObserver<T> End
        {
            get
            {
                return new ReadOnlyPaginationObserver<T>(Internal, Total - 1, Size, Total);
            }
        }
        
        public ReadOnlyPaginationObserver<T> Previous
        {
            get
            {
                return new ReadOnlyPaginationObserver<T>(Internal, Index - 1, Size, Total);
            }
        }
        
        public ReadOnlyPaginationObserver<T> Next
        {
            get
            {
                return new ReadOnlyPaginationObserver<T>(Internal, Index + 1, Size, Total);
            }
        }
        
        public ReadOnlyPaginationObserver(ReadOnlySpan<T> @internal, Int32 size)
            : this(@internal, 0, size)
        {
        }
        
        public ReadOnlyPaginationObserver(ReadOnlySpan<T> @internal, Int32 index, Int32 size)
        {
            Internal = @internal;
            Index = index >= 0 ? index : throw new ArgumentOutOfRangeException(nameof(index), index, null);
            Size = size > 0 ? size : throw new ArgumentOutOfRangeException(nameof(size), size, null);
            
            Int32 start = Index * Size;
            Items = Math.Max(Math.Min(Size, Internal.Length - start), 0);
            Total = Math.Abs((Int32) Math.Ceiling(@internal.Length / (Double) Size));
            View = start >= 0 && start < Internal.Length && Items > 0 ? Internal.Slice(start, Items) : ReadOnlySpan<T>.Empty;
        }
        
        internal ReadOnlyPaginationObserver(ReadOnlySpan<T> @internal, Int32 index, Int32 size, Int32 total)
        {
            Internal = @internal;
            Index = index;
            Size = size;
            Total = total;
            
            Int32 start = Index * Size;
            Items = Math.Max(Math.Min(Size, Internal.Length - start), 0);
            View = start >= 0 && start < Internal.Length && Items > 0 ? Internal.Slice(start, Items) : ReadOnlySpan<T>.Empty;
        }
        
        public ReadOnlyPaginationObserver<T> Resize(Int32 size)
        {
            return Resize(size, false);
        }
        
        public ReadOnlyPaginationObserver<T> Resize(Int32 size, Boolean resize)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), size, null);
            }
            
            return resize ? new ReadOnlyPaginationObserver<T>(Internal, Math.Clamp(Index * Size / size, 0, Total - 1), size) : new ReadOnlyPaginationObserver<T>(Internal, size);
        }
        
        public Boolean MovePrevious(ref ReadOnlyPaginationObserver<T> observer)
        {
            if (!HasPrevious)
            {
                return false;
            }
            
            observer = Previous;
            return true;
        }
        
        public Boolean MoveNext(ref ReadOnlyPaginationObserver<T> observer)
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
        
        public ReadOnlySpan<T>.Enumerator GetEnumerator()
        {
            return View.GetEnumerator();
        }
        
        public T this[Int32 index]
        {
            get
            {
                return View[index];
            }
        }
        
        public T this[Index index]
        {
            get
            {
                return View[index];
            }
        }
    }
}