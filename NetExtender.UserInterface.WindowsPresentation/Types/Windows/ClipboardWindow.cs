using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NetExtender.Types.Monads.Debouce;
using NetExtender.Utilities.Types;
using NetExtender.Utilities.Windows.IO;
using NetExtender.Windows;
using NetExtender.Windows.Types;
using NetExtender.WindowsPresentation.Types.Clipboard;
using NetExtender.WindowsPresentation.Types.Clipboard.Interfaces;
using Process = System.Diagnostics.Process;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public abstract class ClipboardWindow : WndProcWindow, IClipboardExceptionHandler
    {
        public static readonly DependencyProperty AllowMonitorClipboardProperty = DependencyProperty.Register(nameof(AllowMonitorClipboard), typeof(Boolean), typeof(ClipboardWindow), new PropertyMetadata(false, AllowMonitorClipboardChanged));
        
        public Boolean AllowMonitorClipboard
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Boolean) GetValue(AllowMonitorClipboardProperty) && ReferenceEquals(ClipboardUtilities.EventSender, this);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(AllowMonitorClipboardProperty, value);
            }
        }
        
        public Boolean IsMonitorClipboardReady { get; protected set; }
        
        public event ClipboardChangedEventHandler? ClipboardChanged;
        public event ClipboardChangedEventHandler? ClipboardDataChanged;
        
        protected virtual ClipboardSource ClipboardSource
        {
            get
            {
                try
                {
                    IntPtr handle = GetForegroundWindow();
                    Process process = Process.GetProcessById(GetProcessId(handle.ToInt32()));
                    
                    String? name;
                    String? title;
                    String? path;
                    
                    try
                    {
                        name = process.ProcessName;
                    }
                    catch (Exception)
                    {
                        name = null;
                    }
                    
                    try
                    {
                        title = GetWindowTitle(handle) ?? process.MainWindowTitle;
                    }
                    catch (Exception)
                    {
                        title = null;
                    }
                    
                    try
                    {
                        path = process.MainModule?.FileName;
                    }
                    catch (Exception)
                    {
                        path = null;
                    }
                    
                    return new ClipboardSource(process, handle, name, title, path?.Substring(path.LastIndexOf(@"\", StringComparison.InvariantCulture) + 1), path);
                }
                catch (Exception)
                {
                    return default;
                }
            }
        }
        
        protected ClipboardWindow()
        {
            Started += OnStarted;
            Closed += OnClosed;
        }
        
        private async void OnStarted(Object? sender, EventArgs args)
        {
            ClipboardUtilities.Changed += OnClipboardChanged;
            ClipboardUtilities.DataChanged += OnClipboardDataChanged;
            
            try
            {
                await Do(() => AddClipboardFormatListener(Handle), Time.Millisecond.Hundred, 10);
                IsMonitorClipboardReady = true;
            }
            catch (Exception)
            {
            }
        }
        
        private async void OnClosed(Object? sender, EventArgs args)
        {
            ClipboardUtilities.Changed -= OnClipboardChanged;
            ClipboardUtilities.DataChanged -= OnClipboardDataChanged;
            
            try
            {
                await Do(() => RemoveClipboardFormatListener(Handle), Time.Millisecond.Hundred, 10);
                IsMonitorClipboardReady = false;
                
                if (ReferenceEquals(ClipboardUtilities.EventSender, this))
                {
                    ClipboardUtilities.EventSender = null;
                }
            }
            catch (Exception)
            {
            }
        }
        
        private void OnClipboardChanged(Object? sender, ClipboardChangedEventArgs args)
        {
            if (!args.Handled)
            {
                ClipboardChanged?.Invoke(this, args);
            }
        }
        
        private void OnClipboardDataChanged(Object? sender, ClipboardChangedEventArgs args)
        {
            if (!args.Handled)
            {
                ClipboardDataChanged?.Invoke(this, args);
            }
        }
        
        private Debounce<ClipboardType> _debounce = new Debounce<ClipboardType>(Time.Millisecond.Fifty);
        protected virtual void UpdateClipboard()
        {
            if (_debounce.IsDebounce)
            {
                return;
            }
            
            ClipboardType type = ClipboardType.NoContent;
            Func<Object?>? getter = null;
            
            if (ClipboardUtilities.ContainsText())
            {
                type = ClipboardType.Text;
                getter = ClipboardUtilities.GetText;
            }
            else if (ClipboardUtilities.ContainsImage())
            {
                type = ClipboardType.Image;
                getter = ClipboardUtilities.GetImage;
            }
            else if (ClipboardUtilities.ContainsAudio())
            {
                type = ClipboardType.Audio;
                getter = ClipboardUtilities.GetAudio;
            }
            else if (ClipboardUtilities.ContainsFiles())
            {
                type = ClipboardType.Files;
                getter = ClipboardUtilities.GetFiles;
            }
            else if (ClipboardUtilities.ContainsRaw())
            {
                type = ClipboardType.Raw;
                getter = ClipboardUtilities.GetRaw;
            }
            else if (ClipboardUtilities.ContainsData())
            {
                type = ClipboardType.Data;
                getter = ClipboardUtilities.GetData;
            }
            
            if (_debounce.Set(type, out _debounce))
            {
                ClipboardUtilities.InvokeClipboardChanged(this, ClipboardSource, type, getter);
            }
        }
        
        protected virtual Boolean HandleClipboardException(Exception? exception)
        {
            return true;
        }
        
        Boolean IClipboardExceptionHandler.Handle(Exception? exception)
        {
            return HandleClipboardException(exception);
        }
        
        protected override Boolean WndProc(ref WindowsMessage message)
        {
            switch (message.Message)
            {
                case WM.CLIPBOARDUPDATE when AllowMonitorClipboard && IsMonitorClipboardReady:
                    UpdateClipboard();
                    return true;
                default:
                    return base.WndProc(ref message);
            }
        }
        
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean AddClipboardFormatListener(IntPtr handle);
        
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean RemoveClipboardFormatListener(IntPtr handle);
        
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindowPtr();
        
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern Int32 GetWindowText(IntPtr handle, StringBuilder text, Int32 count);
        
        [DllImport("user32")]
        private static extern UInt32 GetWindowThreadProcessId(Int32 handle, ref Int32 lpdw);
        
        private static Int32 GetProcessId(Int32 handle)
        {
            Int32 process = 1;
            GetWindowThreadProcessId(handle, ref process);
            return process;
        }
        
        private static String? GetWindowTitle(IntPtr handle)
        {
            try
            {
                const Int32 capacity = 256;
                StringBuilder content = new StringBuilder(capacity);
                return GetWindowText(handle, content, capacity) > 0 ? content.ToString() : null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        private static async Task Do(Action action, TimeSpan interval, Int32 count)
        {
            await Do<Object?>(() => { action(); return null; }, interval, count);
        }
        
        private static async Task<T> Do<T>(Func<T> action, TimeSpan interval, Int32 count)
        {
            List<Exception> exceptions = new List<Exception>();
            
            for (Int32 attempted = 0; attempted < count; attempted++)
            {
                try
                {
                    if (attempted > 0)
                    {
                        await Task.Delay(interval);
                    }
                    
                    return action();
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }

            throw new AggregateException(exceptions);
        }
        
        private static void AllowMonitorClipboardChanged(DependencyObject? sender, DependencyPropertyChangedEventArgs args)
        {
            if (sender is not Window window || args.NewValue is not Boolean value)
            {
                return;
            }
            
            if (ClipboardUtilities.EventSender is null && value)
            {
                ClipboardUtilities.EventSender = window;
                return;
            }
            
            if (ReferenceEquals(ClipboardUtilities.EventSender, window))
            {
                if (!value)
                {
                    ClipboardUtilities.EventSender = null;
                }
                
                return;
            }
            
            if (value)
            {
                throw new InvalidOperationException("Clipboard event sender must be unique.");
            }
        }
    }
}