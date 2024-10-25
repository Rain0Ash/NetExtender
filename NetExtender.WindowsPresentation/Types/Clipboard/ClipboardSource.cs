using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NetExtender.WindowsPresentation.Types.Clipboard
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ClipboardSource : IEquatable<ClipboardSource>
    {
        public static Boolean operator ==(ClipboardSource first, ClipboardSource second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator !=(ClipboardSource first, ClipboardSource second)
        {
            return !(first == second);
        }
        
        /// <summary>
        /// Gets the application process.
        /// </summary>
        public Process? Process { get; }
        
        /// <summary>
        /// Gets the application window handle.
        /// </summary>
        public IntPtr Handle { get; }
        
        /// <summary>
        /// Gets the application name.
        /// </summary>
        public String? Name { get; }
        
        /// <summary>
        /// Gets the application title text.
        /// </summary>
        public String? Title { get; }
        
        /// <summary>
        /// Gets the application executable path.
        /// </summary>
        public String? Executable { get; }
        
        /// <summary>
        /// Gets the application absolute name.
        /// </summary>
        public String? Path { get; }
        
        public Boolean IsEmpty
        {
            get
            {
                return Process is null || Handle == IntPtr.Zero;
            }
        }
        
        public ClipboardSource(Process process, IntPtr handle, String? name, String? title, String? executable, String? path)
        {
            Process = process ?? throw new ArgumentNullException(nameof(process));
            Handle = handle;
            Name = name;
            Title = title;
            Executable = executable;
            Path = path;
        }
        
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Process?.Id, Handle, Name, Title, Path);
        }
        
        public override Boolean Equals(Object? other)
        {
            return other is ClipboardSource source && Equals(source);
        }
        
        public Boolean Equals(ClipboardSource other)
        {
            return Process?.Id == other.Process?.Id && Handle.Equals(other.Handle) && Name == other.Name && Title == other.Title && Path == other.Path;
        }
        
        public override String ToString()
        {
            return $"{{ {nameof(Process)}: {Process?.Id}, {nameof(Handle)}: {Handle}, {nameof(Name)}: {Name}, {nameof(Title)}: {Title}, {nameof(Executable)}: {Executable}, {nameof(Path)}: {Path} }}";
        }
    }
}