using System;
using System.Windows;
using System.Windows.Shell;
using NetExtender.WindowsPresentation.Types.Bindings;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public abstract class ChromeWindow : ClipboardWindow
    {
        public static readonly DependencyProperty UseChromeProperty = DependencyProperty.Register(nameof(Chrome), typeof(Boolean), typeof(ChromeWindow), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, UseChromeChanged));
        public static readonly DependencyProperty ChromeCaptionHeightProperty = DependencyProperty.Register(nameof(ChromeCaptionHeight), typeof(Int32), typeof(ChromeWindow), new FrameworkPropertyMetadata(32, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceCaptionHeight));
        public static readonly DependencyProperty ChromeResizeBorderThicknessProperty = DependencyProperty.Register(nameof(ChromeResizeBorderThickness), typeof(Thickness), typeof(ChromeWindow), new FrameworkPropertyMetadata(new Thickness(5, 5, 5, 5), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty ChromeGlassFrameThicknessProperty = DependencyProperty.Register(nameof(ChromeGlassFrameThickness), typeof(Thickness), typeof(ChromeWindow), new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty ChromeAeroCaptionButtonsProperty = DependencyProperty.Register(nameof(ChromeAeroCaptionButtons), typeof(Boolean), typeof(ChromeWindow), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty ChromeCornerRadiusProperty = DependencyProperty.Register(nameof(ChromeCornerRadius), typeof(CornerRadius), typeof(ChromeWindow), new FrameworkPropertyMetadata(new CornerRadius(0, 0, 0, 0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty ChromeNonClientFrameEdgesProperty = DependencyProperty.Register(nameof(ChromeNonClientFrameEdges), typeof(NonClientFrameEdges), typeof(ChromeWindow), new FrameworkPropertyMetadata(NonClientFrameEdges.None, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        
        protected WindowChrome? Chrome
        {
            get
            {
                return WindowChrome.GetWindowChrome(this);
            }
            private set
            {
                WindowChrome.SetWindowChrome(this, value);
            }
        }
        
        public Boolean UseChrome
        {
            get
            {
                return (Boolean) GetValue(UseChromeProperty);
            }
            set
            {
                SetValue(UseChromeProperty, value);
            }
        }
        
        public Int32 ChromeCaptionHeight
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Int32) GetValue(ChromeCaptionHeightProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(ChromeCaptionHeightProperty, value);
            }
        }
        
        public Thickness ChromeResizeBorderThickness
        {
            get
            {
                return (Thickness) GetValue(ChromeResizeBorderThicknessProperty);
            }
            set
            {
                SetValue(ChromeResizeBorderThicknessProperty, value);
            }
        }
        
        public Thickness ChromeGlassFrameThickness
        {
            get
            {
                return (Thickness) GetValue(ChromeGlassFrameThicknessProperty);
            }
            set
            {
                SetValue(ChromeGlassFrameThicknessProperty, value);
            }
        }
        
        public Boolean ChromeAeroCaptionButtons
        {
            get
            {
                return (Boolean) GetValue(ChromeAeroCaptionButtonsProperty);
            }
            set
            {
                SetValue(ChromeAeroCaptionButtonsProperty, value);
            }
        }
        
        public CornerRadius ChromeCornerRadius
        {
            get
            {
                return (CornerRadius) GetValue(ChromeCornerRadiusProperty);
            }
            set
            {
                SetValue(ChromeCornerRadiusProperty, value);
            }
        }
        
        public NonClientFrameEdges ChromeNonClientFrameEdges
        {
            get
            {
                return (NonClientFrameEdges) GetValue(ChromeNonClientFrameEdgesProperty);
            }
            set
            {
                SetValue(ChromeNonClientFrameEdgesProperty, value);
            }
        }
        
        private static void UseChromeChanged(DependencyObject? sender, DependencyPropertyChangedEventArgs args)
        {
            if (sender is not ChromeWindow window || args.NewValue is not Boolean state)
            {
                return;
            }
            
            if (window.Chrome is null && state)
            {
                WindowChrome chrome = new WindowChrome();
                chrome.SetBinding(WindowChrome.CaptionHeightProperty, new TwoWayBinding(nameof(ChromeCaptionHeight), window));
                chrome.SetBinding(WindowChrome.ResizeBorderThicknessProperty, new TwoWayBinding(nameof(ChromeResizeBorderThickness), window));
                chrome.SetBinding(WindowChrome.GlassFrameThicknessProperty, new TwoWayBinding(nameof(ChromeGlassFrameThickness), window));
                chrome.SetBinding(WindowChrome.UseAeroCaptionButtonsProperty, new TwoWayBinding(nameof(ChromeAeroCaptionButtons), window));
                chrome.SetBinding(WindowChrome.CornerRadiusProperty, new TwoWayBinding(nameof(ChromeCornerRadius), window));
                chrome.SetBinding(WindowChrome.NonClientFrameEdgesProperty, new TwoWayBinding(nameof(ChromeNonClientFrameEdges), window));
                window.Chrome = chrome;
                return;
            }
            
            if (window.Chrome is not null && !state)
            {
                window.Chrome = null;
            }
        }
        
        private static Object CoerceCaptionHeight(DependencyObject? sender, Object? value)
        {
            return value is > 0 ? value : 32;
        }
    }
}