// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using NetExtender.Utils.IO;
using NetExtender.Watchers.Interfaces;
using ReactiveUI;

namespace NetExtender.Watchers
{
    public abstract class WatcherBase : ReactiveObject, IPathWatcher
    {
        public static implicit operator String(WatcherBase watcher)
        {
            return watcher.ToString();
        }
        
        public Boolean IsStarted { get; private set; } 
        
        public event EmptyHandler PathChanged;
        
        private String _path;
        public String Path
        {
            get
            {
                return _path;
            }
            set
            {
                if (_path == value)
                {
                    return;
                }

                _path = value;
                
                PathChanged?.Invoke();
            }
        }
        public PathStatus PathStatus { get; set; }

        public virtual Image Icon
        {
            get
            {
                return Images.Images.Lineal.WWW;
            }
        }

        public WatcherBase()
        {
            PathChanged += OnPathChanged;
        }
        
        protected virtual void OnPathChanged()
        {
        }

        public abstract Boolean IsValid();

        public abstract Boolean IsExist();

        public void Start()
        {
            if (OnStart())
            {
                IsStarted = true;
            }
        }

        protected virtual Boolean OnStart()
        {
            return true;
        }

        public void Stop()
        {
            if (OnStop())
            {
                IsStarted = false;
            }
        }

        protected virtual Boolean OnStop()
        {
            return true;
        }
        
        public override String ToString()
        {
            return Path;
        }
        
        public virtual void Dispose()
        {
        }
    }
}