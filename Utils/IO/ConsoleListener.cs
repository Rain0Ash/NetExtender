// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;

namespace Common_Library.Utils.IO
{
    public static partial class ConsoleUtils
    {
        private static class ConsoleListener
        {
            public static Boolean Running { get; private set; }

            public static void Start()
            {
                if (ConsoleInputHandle == IntPtr.Zero)
                {
                    return;
                }
                
                if (Running)
                {
                    return;
                }

                Running = true;
                
                IsEchoInputEnabled = true;
                IsWindowInputEnabled = true;
                IsMouseInputEnabled = true;

                IntPtr handleIn = ConsoleInputHandle;
                new Thread(() =>
                {
                    while (true)
                    {
                        UInt32 numRead = 0;
                        INPUT_RECORD[] record = new INPUT_RECORD[1];
                        record[0] = new INPUT_RECORD();
                        ReadConsoleInput(handleIn, record, 1, ref numRead);
                        if (Running)
                        {
                            switch (record[0].EventType)
                            {
                                case INPUT_RECORD.MOUSE_EVENT:
                                    MouseEvent?.Invoke(record[0].MouseEvent);
                                    break;
                                case INPUT_RECORD.KEY_EVENT:
                                    KeyEvent?.Invoke(record[0].KeyEvent);
                                    break;
                                case INPUT_RECORD.WINDOW_BUFFER_SIZE_EVENT:
                                    WindowBufferSizeEvent?.Invoke(record[0].WindowBufferSizeEvent);
                                    break;
                            }
                        }
                        else
                        {
                            UInt32 numWritten = 0;
                            WriteConsoleInput(handleIn, record, 1, ref numWritten);
                            return;
                        }
                    }
                }).Start();
            }

            public static void Stop()
            {
                Running = false;
            }

            [DllImportAttribute("kernel32.dll", CharSet = CharSet.Unicode)]
            private static extern Boolean ReadConsoleInput(IntPtr hConsoleInput, [Out] INPUT_RECORD[] lpBuffer, UInt32 nLength, ref UInt32 lpNumberOfEventsRead);

            [DllImportAttribute("kernel32.dll", CharSet = CharSet.Unicode)]
            private static extern Boolean WriteConsoleInput(IntPtr hConsoleInput, INPUT_RECORD[] lpBuffer, UInt32 nLength, ref UInt32 lpNumberOfEventsWritten);
        }

#pragma warning disable 649
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public struct MOUSE_EVENT_RECORD
        {
            public COORD dwMousePosition;

            public const UInt32 FROM_LEFT_1ST_BUTTON_PRESSED = 0x0001,
                FROM_LEFT_2ND_BUTTON_PRESSED = 0x0004,
                FROM_LEFT_3RD_BUTTON_PRESSED = 0x0008,
                FROM_LEFT_4TH_BUTTON_PRESSED = 0x0010,
                RIGHTMOST_BUTTON_PRESSED = 0x0002;

            public UInt32 dwButtonState;

            public const Int32 CAPSLOCK_ON = 0x0080,
                ENHANCED_KEY = 0x0100,
                LEFT_ALT_PRESSED = 0x0002,
                LEFT_CTRL_PRESSED = 0x0008,
                NUMLOCK_ON = 0x0020,
                RIGHT_ALT_PRESSED = 0x0001,
                RIGHT_CTRL_PRESSED = 0x0004,
                SCROLLLOCK_ON = 0x0040,
                SHIFT_PRESSED = 0x0010;

            public UInt32 dwControlKeyState;

            public const Int32 DOUBLE_CLICK = 0x0002,
                MOUSE_HWHEELED = 0x0008,
                MOUSE_MOVED = 0x0001,
                MOUSE_WHEELED = 0x0004;

            public UInt32 dwEventFlags;
        }
#pragma warning restore 649

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
        [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public struct KEY_EVENT_RECORD
        {
            [FieldOffset(0)] public Boolean bKeyDown;
            [FieldOffset(4)] public UInt16 wRepeatCount;
            [FieldOffset(6)] public UInt16 wVirtualKeyCode;
            [FieldOffset(8)] public UInt16 wVirtualScanCode;
            [FieldOffset(10)] public Char UnicodeChar;
            [FieldOffset(10)] public Byte AsciiChar;

            public const Int32 CAPSLOCK_ON = 0x0080;
            public const Int32 ENHANCED_KEY = 0x0100;
            public const Int32 LEFT_ALT_PRESSED = 0x0002;
            public const Int32 LEFT_CTRL_PRESSED = 0x0008;
            public const Int32 NUMLOCK_ON = 0x0020;
            public const Int32 RIGHT_ALT_PRESSED = 0x0001;
            public const Int32 RIGHT_CTRL_PRESSED = 0x0004;
            public const Int32 SCROLLLOCK_ON = 0x0040;
            public const Int32 SHIFT_PRESSED = 0x0010;

            [FieldOffset(12)] public UInt32 dwControlKeyState;
        }

#pragma warning disable 649
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public struct WINDOW_BUFFER_SIZE_RECORD
        {
            public COORD dwSize;
        }
#pragma warning restore 649

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
        [SuppressMessage("ReSharper", "NotAccessedField.Local")]
        public struct COORD
        {
            public Int16 X;
            public Int16 Y;

            public COORD(Int16 x, Int16 y)
            {
                X = x;
                Y = y;
            }

            public override String ToString()
            {
                return $"{X}:{Y}";
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private struct INPUT_RECORD
        {
            public const UInt16 KEY_EVENT = 0x0001;
            public const UInt16 MOUSE_EVENT = 0x0002;
            public const UInt16 WINDOW_BUFFER_SIZE_EVENT = 0x0004;

            [FieldOffset(0)] public readonly UInt16 EventType;
            [FieldOffset(4)] public KEY_EVENT_RECORD KeyEvent;
            [FieldOffset(4)] public MOUSE_EVENT_RECORD MouseEvent;
            [FieldOffset(4)] public WINDOW_BUFFER_SIZE_RECORD WindowBufferSizeEvent;
        }
    }
}