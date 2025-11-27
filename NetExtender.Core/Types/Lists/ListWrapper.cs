using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Lists
{
    public sealed class ListWrapperCOW : IList
    {
        private ICollection? Collection { get; set; }
        
        private readonly Lazy<IList> _cow;
        private IList COW
        {
            get
            {
                return _cow.Value;
            }
        }

        public Int32 Count
        {
            get
            {
                return Collection?.Count ?? COW.Count;
            }
        }

        public Boolean IsCOW
        {
            get
            {
                return _cow.IsValueCreated;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Collection switch
                {
                    null => COW.IsReadOnly,
                    IList convert => convert.IsReadOnly,
                    ICollection<Object?> convert => convert.IsReadOnly,
                    _ => true
                };
            }
        }

        public Boolean IsFixedSize
        {
            get
            {
                return Collection switch
                {
                    null => COW.IsFixedSize,
                    IList convert => convert.IsFixedSize,
                    _ => false
                };
            }
        }

        public Object SyncRoot
        {
            get
            {
                return Collection is { } collection ? collection.SyncRoot : COW.SyncRoot;
            }
        }

        public Boolean IsSynchronized
        {
            get
            {
                return Collection is { } collection ? collection.IsSynchronized : COW.IsSynchronized;
            }
        }

        public ListWrapperCOW(ICollection collection)
        {
            Collection = collection ?? throw new ArgumentNullException(nameof(collection));
            
            _cow = new Lazy<IList>(() => 
            {
                ArrayList cow = new ArrayList(Collection ?? Array.Empty<Object?>());
                Collection = null;
                return cow;
            }, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public ListWrapperCOW EnsureCOW()
        {
            _ = COW;
            return this;
        }

        public Boolean Contains(Object? value)
        {
            return Collection switch
            {
                null => COW.Contains(value),
                IList convert => convert.Contains(value),
                ICollection<Object?> convert => convert.Contains(value),
                { } convert => convert.Cast<Object?>().Contains(value)
            };
        }

        public Int32 IndexOf(Object? value)
        {
            return Collection switch
            {
                null => COW.IndexOf(value),
                IList convert => convert.IndexOf(value),
                IList<Object?> convert => convert.IndexOf(value),
                { } convert => convert.Cast<Object?>().IndexOf(value)
            };
        }

        public Int32 Add(Object? value)
        {
            return COW.Add(value);
        }

        public void Insert(Int32 index, Object? value)
        {
            COW.Insert(index, value);
        }

        public void Remove(Object? value)
        {
            COW.Remove(value);
        }

        public void RemoveAt(Int32 index)
        {
            COW.RemoveAt(index);
        }

        public void Clear()
        {
            COW.Clear();
        }

        public void CopyTo(Array array, Int32 index)
        {
            (Collection ?? COW).CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return Collection is { } collection ? collection.GetEnumerator() : COW.GetEnumerator();
        }

        public Object? this[Int32 index]
        {
            get
            {
                return Collection switch
                {
                    null => COW[index],
                    IList convert => convert[index],
                    { } convert => convert.Cast<Object?>().ElementAt(index)
                };
            }
            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
                }
                
                COW[index] = value;
            }
        }
    }
    
    public sealed class ListWrapper : IList
    {
        private ICollection Collection { get; }

        public Int32 Count
        {
            get
            {
                return Collection.Count;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Collection is not IList convert || convert.IsReadOnly;
            }
        }

        public Boolean IsFixedSize
        {
            get
            {
                return Collection is IList { IsFixedSize: true };
            }
        }

        public Object SyncRoot
        {
            get
            {
                return Collection.SyncRoot;
            }
        }

        public Boolean IsSynchronized
        {
            get
            {
                return Collection.IsSynchronized;
            }
        }

        public ListWrapper(ICollection collection)
        {
            Collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public Boolean Contains(Object? value)
        {
            return Collection switch
            {
                IList convert => convert.Contains(value),
                ICollection<Object?> convert => convert.Contains(value),
                _ => Collection.Cast<Object?>().Contains(value)
            };
        }

        public Int32 IndexOf(Object? value)
        {
            return Collection switch
            {
                IList convert => convert.IndexOf(value),
                IList<Object?> convert => convert.IndexOf(value),
                _ => Collection.Cast<Object?>().IndexOf(value)
            };
        }

        public Int32 Add(Object? value)
        {
            switch (Collection)
            {
                case IList convert:
                    return convert.Add(value);
                case ICollection<Object?> convert:
                    convert.Add(value);
                    return -1;
                default:
                    throw new NotSupportedException();
            }
        }

        public void Insert(Int32 index, Object? value)
        {
            switch (Collection)
            {
                case IList convert:
                    convert.Insert(index, value);
                    return;
                case IList<Object?> convert:
                    convert.Insert(index, value);
                    return;
                default:
                    throw new NotSupportedException();
            }
        }

        public void Remove(Object? value)
        {
            switch (Collection)
            {
                case IList convert:
                    convert.Remove(value);
                    return;
                case ICollection<Object?> convert:
                    convert.Remove(value);
                    return;
                default:
                    throw new NotSupportedException();
            }
        }

        public void RemoveAt(Int32 index)
        {
            switch (Collection)
            {
                case IList convert:
                    convert.RemoveAt(index);
                    return;
                case IList<Object?> convert:
                    convert.RemoveAt(index);
                    return;
                default:
                    throw new NotSupportedException();
            }
        }

        public void Clear()
        {
            switch (Collection)
            {
                case IList convert:
                    convert.Clear();
                    return;
                case ICollection<Object?> convert:
                    convert.Clear();
                    return;
                default:
                    throw new NotSupportedException();
            }
        }

        public void CopyTo(Array array, Int32 index)
        {
            Collection.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        public Object? this[Int32 index]
        {
            get
            {
                return Collection switch
                {
                    IList convert => convert[index],
                    IList<Object?> convert => convert[index],
                    _ => Collection.Cast<Object?>().ElementAt(index)
                };
            }
            set
            {
                switch (Collection)
                {
                    case IList convert:
                        convert[index] = value;
                        return;
                    case IList<Object?> convert:
                        convert[index] = value;
                        return;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
    }
}