// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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