// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Drawing;
using NetExtender.Images.icons.blue;
using NetExtender.Images.icons.circular;
using NetExtender.Images.icons.fill;
using NetExtender.Images.icons.flat;
using NetExtender.Images.icons.gradient;
using NetExtender.Images.icons.line;
using NetExtender.Images.icons.lineal;
using NetExtender.Images.icons.linear_color;
using NetExtender.Images.icons.other;

namespace NetExtender.Images
{
    public static class Images
    {
        public static class Basic
        {
            public static readonly Bitmap Null = OtherImages._null;

            public static readonly Bitmap Application = SystemIcons.Application.ToBitmap();

            public static readonly Bitmap Asterisk = SystemIcons.Asterisk.ToBitmap();

            public static readonly Bitmap Error = SystemIcons.Error.ToBitmap();

            public static readonly Bitmap Exclamation = SystemIcons.Exclamation.ToBitmap();

            public static readonly Bitmap Hand = SystemIcons.Hand.ToBitmap();

            public static readonly Bitmap Information = SystemIcons.Information.ToBitmap();

            public static readonly Bitmap Question = SystemIcons.Question.ToBitmap();

            public static readonly Bitmap Shield = SystemIcons.Shield.ToBitmap();

            public static readonly Bitmap Warning = SystemIcons.Warning.ToBitmap();

            public static readonly Bitmap WinLogo = SystemIcons.WinLogo.ToBitmap();
        }

        public static class Line
        {
            public static readonly Bitmap ResetGear = LineImages.reset;

            public static readonly Bitmap Optimization = LineImages.optimization;

            public static readonly Bitmap Program = LineImages.program;

            public static readonly Bitmap Settings = LineImages.settings;

            public static readonly Bitmap Tech = LineImages.tech;
        }

        public static class Lineal
        {
            public static readonly Bitmap Gear = LinealImages.gear;

            public static readonly Bitmap Plus = LinealImages.plus;

            public static readonly Bitmap Minus = LinealImages.minus;

            public static readonly Bitmap Download = LinealImages.download;

            public static readonly Bitmap Reload = LinealImages.reload;

            public static readonly Bitmap Refresh = LinealImages.refresh;

            public static readonly Bitmap Reuse = LinealImages.reuse;

            public static readonly Bitmap File = LinealImages.file;

            public static readonly Bitmap NotFile = LinealImages.not_file;

            public static readonly Bitmap Folder = LinealImages.folder;

            public static readonly Bitmap NotFolder = LinealImages.not_folder;
            
            public static readonly Bitmap WWW = LinealImages.www;
            
            public static readonly Bitmap Wifi = LinealImages.wifi;
        }

        public static class Fill
        {
            public static readonly Bitmap ResetGear = FillImages.reset;

            public static readonly Bitmap Program = FillImages.program;

            public static readonly Bitmap File = FillImages.file;

            public static readonly Bitmap NotFile = FillImages.not_file;

            public static readonly Bitmap Folder = FillImages.folder;

            public static readonly Bitmap NotFolder = FillImages.not_folder;
        }

        public static class Flat
        {
            public static readonly Bitmap Program = FlatImages.program;

            public static readonly Bitmap File = FlatImages.file;

            public static readonly Bitmap NotFile = FlatImages.not_file;

            public static readonly Bitmap Folder = FlatImages.folder;

            public static readonly Bitmap NotFolder = FlatImages.not_folder;

            public static readonly Bitmap GitHub = FlatImages.github;
        }

        public static class Gradient
        {
            public static readonly Bitmap Program = GradientImages.program;

            public static readonly Bitmap File = GradientImages.file;

            public static readonly Bitmap NotFile = GradientImages.not_file;

            public static readonly Bitmap Folder = GradientImages.folder;

            public static readonly Bitmap NotFolder = GradientImages.not_folder;
            
            public static readonly Bitmap Wifi = GradientImages.wifi;
        }
        
        public static class LinearColor
        {
            public static readonly Bitmap File = LinearColorImages.file;

            public static readonly Bitmap NotFile = LinearColorImages.not_file;

            public static readonly Bitmap Folder = LinearColorImages.folder;

            public static readonly Bitmap NotFolder = LinearColorImages.not_folder;
        }

        public static class Blue
        {
            public static readonly Bitmap Question = BlueImages.question;
        }
        
        public static class Circular
        {
            public static readonly Bitmap Wifi = CircularImages.wifi;
        }
        
        public static class Others
        {
            public static readonly Bitmap Monitor = OtherImages.monitor;

            public static readonly Bitmap Proxy = OtherImages.proxy;

            public static readonly Bitmap XButton = OtherImages.xbutton;
        }
    }
}