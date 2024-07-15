using System;

namespace NetExtender.Types.Collections.Interfaces
{
    public interface IObservableAutoCollection<T> : IObservableCollection<T>
    {
        public void Set(Int32 count);
        public void Fill(Int32 count);
        public void Fill(Int32 start, Int32 count);
        public void Reset();
        public void Reset(Int32 count);
    }
}