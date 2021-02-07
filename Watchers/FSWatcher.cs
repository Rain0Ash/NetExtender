// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using NetExtender.Exceptions;
using NetExtender.IO.NTFS.DataStreams;
using NetExtender.Utils.IO;
using NetExtender.Watchers.FileSystem;
using NetExtender.Watchers.FileSystem.Interfaces;

namespace NetExtender.Watchers
{
    public class FSWatcher : WatcherBase
    {
        public PathType PathType { get; set; }

        private FileSystemInfo _info;

        public FileSystemInfo Info
        {
            get
            {
                if (_info is not null)
                {
                    return _info;
                }

                if (!PathUtils.IsExist(Path, PathType))
                {
                    throw new IOException($"Folder or file '{Path}' not found");
                }

                return _info ??= PathUtils.GetInfo(Path);
            }
        }
        
        private readonly IWatcher _watcher;
        public IReadOnlyWatcher Watcher
        {
            get
            {
                return _watcher;
            }
        }

        public TimeSpan? Polling
        {
            get
            {
                CheckWatcher();

                if (_watcher is IPoller poller)
                {
                    return poller.Polling;
                }
                
                return null;
            }
            set
            {
                CheckWatcher();

                if (value is not null && _watcher is IPoller poller)
                {
                    poller.Polling = value.Value;
                }
            }
        }

        public Boolean WatcherEnabled
        {
            get
            {
                CheckWatcher();

                return _watcher.EnableRaisingEvents;
            }
            set
            {
                CheckWatcher();

                _watcher.EnableRaisingEvents = value;
            }
        }

        public event EmptyHandler RecursiveChanged;
        private Boolean _isRecursive;
        public Boolean IsRecursive
        {
            get
            {
                return _isRecursive;
            }
            set
            {
                if (_isRecursive == value)
                {
                    return;
                }

                _isRecursive = value;
                RecursiveChanged?.Invoke();
            }
        }

        public event EmptyHandler IconExistCheckChanged;

        private Boolean _iconExistCheck = true;

        public Boolean IconExistCheck
        {
            get
            {
                return _iconExistCheck;
            }
            set
            {
                if (_iconExistCheck == value)
                {
                    return;
                }

                _iconExistCheck = value;
                IconExistCheckChanged?.Invoke();
            }
        }

        private Image _folderImage;

        public Image FolderImage
        {
            get
            {
                return _folderImage ?? Images.Images.Lineal.Folder;
            }
            set
            {
                _folderImage = value;
            }
        }

        private Image _notFolderImage;

        public Image NotFolderImage
        {
            get
            {
                return _notFolderImage ?? Images.Images.Lineal.NotFolder;
            }
            set
            {
                _notFolderImage = value;
            }
        }

        private Image _fileImage;

        public Image FileImage
        {
            get
            {
                return _fileImage ?? Images.Images.Lineal.File;
            }
            set
            {
                _fileImage = value;
            }
        }

        private Image _notFileImage;

        public Image NotFileImage
        {
            get
            {
                return _notFileImage ?? Images.Images.Lineal.NotFile;
            }
            set
            {
                _notFileImage = value;
            }
        }

        public override Image Icon
        {
            get
            {
                return PathUtils.GetPathType(Path) switch
                {
                    PathType.Folder => !IconExistCheck || IsExistAsFolder() ? FolderImage : NotFolderImage,
                    PathType.LocalFolder => !IconExistCheck || IsExistAsFolder() ? FolderImage : NotFolderImage,
                    PathType.NetworkFolder => !IconExistCheck || IsExistAsFolder() ? FolderImage : NotFolderImage,
                    PathType.File => !IconExistCheck || IsExistAsFile() ? FileImage : NotFileImage,
                    PathType.LocalFile => !IconExistCheck || IsExistAsFile() ? FileImage : NotFileImage,
                    PathType.NetworkFile => !IconExistCheck || IsExistAsFile() ? FileImage : NotFileImage,
                    _ => Images.Images.Basic.Null
                };
            }
        }
        
        public FSWatcher(FileSystemInfo info, PathStatus status = Utils.IO.PathStatus.All, WatcherType watcher = WatcherType.None)
            : this(info.FullName, info switch
            {
                FileInfo => PathType.File,
                DirectoryInfo => PathType.Folder,
                _ => PathType.All
            }, status, watcher)
        {
        }
        
        public FSWatcher(DirectoryInfo info, PathStatus status = Utils.IO.PathStatus.All, WatcherType watcher = WatcherType.None)
            : this(info.FullName, PathType.Folder, status, watcher)
        {
        }

        public FSWatcher(FileInfo info, PathStatus status = Utils.IO.PathStatus.All, WatcherType watcher = WatcherType.None)
            : this(info.FullName, PathType.File, status, watcher)
        {
        }

        public FSWatcher(String path, PathType type = PathType.All, PathStatus status = Utils.IO.PathStatus.All, WatcherType watcher = WatcherType.None)
        {
            if (!PathUtils.IsValidPath(path))
            {
                throw new ArgumentException($"Path: \"{path}\" is invalid");
            }

            Path = path;
            PathType = type;
            PathStatus = status;

            _watcher = FileSystem.Watcher.Create(watcher, Path);

            if (_watcher is not null)
            {
                WatcherEnabled = false;
            }
        }
        
        public FSWatcher(String path, IWatcher watcher, PathType type = PathType.All, PathStatus status = Utils.IO.PathStatus.All)
            : this(path, type, status)
        {
            if (_watcher.Path != path)
            {
                throw new ArgumentException("Watcher path need to equals path");
            }
            
            _watcher = watcher;
        }

        protected override void OnPathChanged()
        {
            if (_watcher is not null)
            {
                _watcher.Path = Path;
            }
        }

        private void CheckWatcher()
        {
            if (_watcher is null)
            {
                throw new NotInitializedException("Watcher is not initialized");
            }
        }
        
        private void OnRecursive_Changed()
        {
            if (_watcher is null)
            {
                return;
            }
            
            _watcher.IncludeSubdirectories = IsRecursive;
        }

        protected override Boolean OnStart()
        {
            CheckWatcher();
            
            _watcher.StartWatch();
            return base.OnStart();
        }

        protected override Boolean OnStop()
        {
            _watcher?.StopWatch();
            return base.OnStop();
        }

        public override Boolean IsValid()
        {
            return IsValid(PathType, PathStatus);
        }

        public Boolean IsValid(PathType type)
        {
            return IsValid(type, PathStatus);
        }

        public Boolean IsValid(PathStatus status)
        {
            return IsValid(PathType, status);
        }

        public Boolean IsValid(PathType type, PathStatus status)
        {
            return PathUtils.IsValidPath(Path, type, status);
        }

        public Boolean IsExistAsFolder()
        {
            return PathUtils.IsExistAsFolder(Path);
        }

        public Boolean IsExistAsFile()
        {
            return PathUtils.IsExistAsFile(Path);
        }

        public override Boolean IsExist()
        {
            return PathUtils.IsExist(Path, PathType);
        }

        public Boolean IsExist(PathType type)
        {
            return PathUtils.IsExist(Path, type);
        }

        public String GetAbsolutePath()
        {
            return PathUtils.GetFullPath(Path);
        }

        public IEnumerable<String> GetEntries()
        {
            return GetEntries(PathType);
        }
        
        public IEnumerable<String> GetEntries(PathType type)
        {
            return GetEntries(type, IsRecursive);
        }
        
        public IEnumerable<String> GetEntries(Boolean recursive)
        {
            return GetEntries(PathType, recursive);
        }
        
        public IEnumerable<String> GetEntries(PathType type, Boolean recursive)
        {
            return GetEntries(new Regex(".*"), recursive, type);
        }

        public IEnumerable<String> GetEntries([NotNull][RegexPattern] String searchPattern, Boolean recursive = false, PathType type = PathType.All)
        {
            return GetEntries(new Regex(String.IsNullOrEmpty(searchPattern) ? ".*" : searchPattern), recursive, type);
        }

        public IEnumerable<String> GetEntries(Regex regex, Boolean recursive = false, PathType type = PathType.All)
        {
            regex ??= new Regex(".*");
            try
            {
                return DirectoryUtils.GetEntries(Path, recursive, type).Where(name => regex.IsMatch(name));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Byte[] ReadFileBytes()
        {
            return FileUtils.ReadFileBytes(Path);
        }
        
        public Byte[] TryReadFileBytes()
        {
            return FileUtils.TryReadFileBytes(Path);
        }

        public String ReadFileText()
        {
            return FileUtils.ReadFileText(Path);
        }
        
        public String TryReadFileText()
        {
            return FileUtils.TryReadFileText(Path);
        }

        public String[] ReadFileLines()
        {
            return FileUtils.ReadFileLines(Path);
        }
        
        public String[] TryReadFileLines()
        {
            return FileUtils.TryReadFileLines(Path);
        }

        public IDictionary<String, AlternateDataStreamInfo> GetAlternateDataStreams()
        {
            return Info.GetAlternateDataStreams();
        }
        
        public override void Dispose()
        {
            Watcher?.Dispose();
        }
    }
}