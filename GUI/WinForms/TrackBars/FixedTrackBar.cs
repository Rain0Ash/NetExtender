// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;

namespace NetExtender.GUI.WinForms.TrackBars
{
    public class FixedTrackBar : TrackBar
    {
        public FixedTrackBar()
        {
            DoubleBuffered = true;
        }
    }
}