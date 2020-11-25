// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using NetExtender.Interfaces;
using NetExtender.Utils.IO;
using ReactiveUI;

namespace NetExtender.Watchers.Interfaces
{
    public interface IPathWatcher : IStartable, IReactiveObject, IDisposable
    {
        public String Path { get; set; }
        public PathStatus PathStatus { get; set; }
        
        public Image Icon { get; }

        public Boolean IsValid();
        
        public Boolean IsExist();
    }
}