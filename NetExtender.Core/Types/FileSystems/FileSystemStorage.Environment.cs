// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.FileSystems.Interfaces;

namespace NetExtender.FileSystems
{
    public abstract partial class FileSystemStorage<TLink, TFile, TDirectory, TDrive>
    {
        public override IDirectoryInfo? GetFolderPath(Environment.SpecialFolder folder)
        {
            return GetFolderPath(folder, System.Environment.SpecialFolderOption.None);
        }

        public abstract override IDirectoryInfo? GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option);
    }
}