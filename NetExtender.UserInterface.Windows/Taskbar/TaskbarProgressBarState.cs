// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.UserInterface.Windows.Taskbar
{
    /// <summary>Represents the thumbnail progress bar state.</summary>
    public enum TaskbarProgressBarState
    {
        /// <summary>No progress is displayed.</summary>
        NoProgress = 0,

        /// <summary>The progress is indeterminate (marquee).</summary>
        Indeterminate = 1,

        /// <summary>Normal progress is displayed.</summary>
        Normal = 2,

        /// <summary>An error occurred (red).</summary>
        Error = 4,

        /// <summary>The operation is paused (yellow).</summary>
        Paused = 8
    }
}