// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.FileSystems.Interfaces
{
    public interface IShortcut
    {
        public String Name { get; }
        public String FullName { get; }
        public String? Arguments { get; init; }
        public String? Description { get; init; }
        public String? Hotkey { get; init; }
        public String? IconLocation { get; init; }
        public String RelativePath { init; }
        public String TargetPath { get; init; }
        public Int32 WindowStyle { get; init; }
        public String WorkingDirectory { get; init; }
        public String CreatingPath { get; }
        public String? SavePath { get; }
        public String? SaveDirectory { get; init; }
        public Boolean Overwrite { get; set; }
        public Boolean Save();
    }
}