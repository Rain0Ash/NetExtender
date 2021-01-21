// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Drawing;

namespace NetExtender.Types.Drawing
{
    public readonly struct DrawingData
    {
        public Color BackgroundColor { get; }
        public Color ForegroundColor { get; }
        public Font Font { get; }

        public DrawingData(Color background, Color foreground, Font font = null)
        {
            BackgroundColor = background;
            ForegroundColor = foreground;
            
            Font = font;
        }
    }
}