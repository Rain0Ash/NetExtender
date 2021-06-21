// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;

namespace Core.Workstation.Interfaces
{
    public interface IScreen
    {
        public Int32 Id { get; }
        public String Name { get; }
        public Boolean Primary { get; }
        public Rectangle Bounds { get; }
        public Rectangle WorkingArea { get; }
        public Int32 BitsPerPixel { get; }
    }
}