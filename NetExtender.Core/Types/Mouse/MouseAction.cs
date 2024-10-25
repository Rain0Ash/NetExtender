using System;

namespace NetExtender.Types.Mouse
{
    [Flags]
    public enum MouseAction : UInt32
    {
        /// <summary>
        /// The unknown mouse button.
        /// </summary>
        Unknown = 0,
        
        /// <summary>
        /// The no client area.
        /// </summary>
        NoClient = 1,
        
        /// <summary>
        /// The standard button.
        /// </summary>
        Standard = 2,
        
        /// <summary>
        /// The mouse move.
        /// </summary>
        Move = 4 | Standard,
        
        /// <summary>
        /// The mouse hover.
        /// </summary>
        Hover = 8 | Move,
        
        /// <summary>
        /// The mouse wheel.
        /// </summary>
        Wheel = 16,
        
        /// <summary>
        /// The mouse vertical wheel.
        /// </summary>
        VerticalWheel = 32 | Wheel | Standard,
        
        /// <summary>
        /// The mouse wheel.
        /// </summary>
        HorizontalWheel = 64 | Wheel,
        
        /// <summary>
        /// The double click.
        /// </summary>
        DoubleClick = 128,
        
        /// <summary>
        /// The mouse wheel.
        /// </summary>
        Button = 256,
        
        /// <summary>
        /// The left mouse button.
        /// </summary>
        Left = 512 | Button | Standard,
        
        /// <summary>
        /// The middle mouse button.
        /// </summary>
        Middle = 1024 | Button | Standard,
        
        /// <summary>
        /// The right mouse button.
        /// </summary>
        Right = 2048 | Button | Standard,

        /// <summary>
        /// The extended mouse button.
        /// </summary>
        XButton = 4096 | Button,

        /// <summary>
        /// The first extended mouse button.
        /// </summary>
        XButton1 = XButton << 1 | XButton,
        
        /// <summary>
        /// The second extended mouse button.
        /// </summary>
        XButton2 = XButton << 2 | XButton,
        
        /// <summary>
        /// The third extended mouse button.
        /// </summary>
        XButton3 = XButton << 3 | XButton,
        
        /// <summary>
        /// The fourth extended mouse button.
        /// </summary>
        XButton4 = XButton << 4 | XButton,
        
        /// <summary>
        /// The fifth extended mouse button.
        /// </summary>
        XButton5 = XButton << 5 | XButton,
        
        /// <summary>
        /// The sixth extended mouse button.
        /// </summary>
        XButton6 = XButton << 6 | XButton,
        
        /// <summary>
        /// The seventh extended mouse button.
        /// </summary>
        XButton7 = XButton << 7 | XButton,
        
        /// <summary>
        /// The eighth extended mouse button.
        /// </summary>
        XButton8 = XButton << 8 | XButton,
        
        /// <summary>
        /// The ninth extended mouse button.
        /// </summary>
        XButton9 = XButton << 9 | XButton,
        
        /// <summary>
        /// The tenth extended mouse button.
        /// </summary>
        XButton10 = XButton << 10 | XButton,
        
        /// <summary>
        /// The eleventh extended mouse button.
        /// </summary>
        XButton11 = XButton << 11 | XButton,
        
        /// <summary>
        /// The twelfth extended mouse button.
        /// </summary>
        XButton12 = XButton << 12 | XButton,
        
        /// <summary>
        /// The mouse modifiers.
        /// </summary>
        Modifiers = NoClient | Standard | Wheel | DoubleClick | Button | XButton
    }
}