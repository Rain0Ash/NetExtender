using System;
using NetExtender.FileSystems.Interfaces;

namespace NetExtender.FileSystems
{
    public abstract class FileSystemDynamicStorage<TLink, TFile, TDirectory, TDrive> : FileSystemStorage<TLink, TFile, TDirectory, TDrive>, IFileSystemDynamicStorage where TLink : FileSystemStorage<TLink, TFile, TDirectory, TDrive>.LinkNode where TFile : FileSystemStorage<TLink, TFile, TDirectory, TDrive>.FileNode where TDirectory : FileSystemStorage<TLink, TFile, TDirectory, TDrive>.DirectoryNode where TDrive : FileSystemStorage<TLink, TFile, TDirectory, TDrive>.DriveNode
    {
        protected FileSystemDynamicStorage(String name)
            : base(name)
        {
        }
        
        protected FileSystemDynamicStorage(Guid id, String name)
            : base(id, name)
        {
        }
    }
}