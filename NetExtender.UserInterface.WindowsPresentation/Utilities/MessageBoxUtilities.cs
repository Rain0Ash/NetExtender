using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows;
using NetExtender.Types.Strings.Interfaces;

namespace NetExtender.Utilities.UserInterface
{
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public static class MessageBoxUtilities
    {
        public static class OK
        {
            public static class None
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text)
                {
                    return Show(caption, text, MessageBoxImage.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.OK, image, MessageBoxResult.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.OK, image, MessageBoxResult.None, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text)
                {
                    return Show(window, caption, text, MessageBoxImage.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.OK, image, MessageBoxResult.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.OK, image, MessageBoxResult.None, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text)
                {
                    return Show(caption, text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image)
                {
                    return Show(caption, text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption, text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text)
                {
                    return Show(window, caption, text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image)
                {
                    return Show(window, caption, text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption, text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text)
                {
                    return Show(caption?.ToString(), text);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image)
                {
                    return Show(caption?.ToString(), text, image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption?.ToString(), text, image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text)
                {
                    return Show(window, caption?.ToString(), text);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image)
                {
                    return Show(window, caption?.ToString(), text, image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption?.ToString(), text, image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text)
                {
                    return Show(caption?.ToString(), text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image)
                {
                    return Show(caption?.ToString(), text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption?.ToString(), text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text)
                {
                    return Show(window, caption?.ToString(), text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image)
                {
                    return Show(window, caption?.ToString(), text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption?.ToString(), text?.ToString(), image, options);
                }
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, String? text)
            {
                return Show(caption, text, MessageBoxImage.None);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image)
            {
                return MessageBox.Show(text, caption, MessageBoxButton.OK, image, MessageBoxResult.OK);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return MessageBox.Show(text, caption, MessageBoxButton.OK, image, MessageBoxResult.OK, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, String? text)
            {
                return Show(window, caption, text, MessageBoxImage.None);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image)
            {
                return MessageBox.Show(window, text, caption, MessageBoxButton.OK, image, MessageBoxResult.OK);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return MessageBox.Show(window, text, caption, MessageBoxButton.OK, image, MessageBoxResult.OK, options);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, IString? text)
            {
                return Show(caption, text?.ToString());
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image)
            {
                return Show(caption, text?.ToString(), image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(caption, text?.ToString(), image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, IString? text)
            {
                return Show(window, caption, text?.ToString());
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image)
            {
                return Show(window, caption, text?.ToString(), image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(window, caption, text?.ToString(), image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, String? text)
            {
                return Show(caption?.ToString(), text);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image)
            {
                return Show(caption?.ToString(), text, image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(caption?.ToString(), text, image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, String? text)
            {
                return Show(window, caption?.ToString(), text);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image)
            {
                return Show(window, caption?.ToString(), text, image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(window, caption?.ToString(), text, image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, IString? text)
            {
                return Show(caption?.ToString(), text?.ToString());
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image)
            {
                return Show(caption?.ToString(), text?.ToString(), image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(caption?.ToString(), text?.ToString(), image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, IString? text)
            {
                return Show(window, caption?.ToString(), text?.ToString());
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image)
            {
                return Show(window, caption?.ToString(), text?.ToString(), image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(window, caption?.ToString(), text?.ToString(), image, options);
            }
        }

        public static class YesNo
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, String? text)
            {
                return Show(caption, text, MessageBoxImage.None);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image)
            {
                return MessageBox.Show(text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.None);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return MessageBox.Show(text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.None, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, String? text)
            {
                return Show(window, caption, text, MessageBoxImage.None);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image)
            {
                return MessageBox.Show(window, text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.None);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return MessageBox.Show(window, text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.None, options);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, IString? text)
            {
                return Show(caption, text?.ToString());
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image)
            {
                return Show(caption, text?.ToString(), image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(caption, text?.ToString(), image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, IString? text)
            {
                return Show(window, caption, text?.ToString());
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image)
            {
                return Show(window, caption, text?.ToString(), image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(window, caption, text?.ToString(), image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, String? text)
            {
                return Show(caption?.ToString(), text);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image)
            {
                return Show(caption?.ToString(), text, image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(caption?.ToString(), text, image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, String? text)
            {
                return Show(window, caption?.ToString(), text);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image)
            {
                return Show(window, caption?.ToString(), text, image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(window, caption?.ToString(), text, image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, IString? text)
            {
                return Show(caption?.ToString(), text?.ToString());
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image)
            {
                return Show(caption?.ToString(), text?.ToString(), image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(caption?.ToString(), text?.ToString(), image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, IString? text)
            {
                return Show(window, caption?.ToString(), text?.ToString());
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image)
            {
                return Show(window, caption?.ToString(), text?.ToString(), image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(window, caption?.ToString(), text?.ToString(), image, options);
            }
            
            public static class Yes
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text)
                {
                    return Show(caption, text, MessageBoxImage.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.Yes);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.Yes, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text)
                {
                    return Show(window, caption, text, MessageBoxImage.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.Yes);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.Yes, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text)
                {
                    return Show(caption, text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image)
                {
                    return Show(caption, text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption, text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text)
                {
                    return Show(window, caption, text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image)
                {
                    return Show(window, caption, text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption, text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text)
                {
                    return Show(caption?.ToString(), text);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image)
                {
                    return Show(caption?.ToString(), text, image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption?.ToString(), text, image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text)
                {
                    return Show(window, caption?.ToString(), text);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image)
                {
                    return Show(window, caption?.ToString(), text, image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption?.ToString(), text, image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text)
                {
                    return Show(caption?.ToString(), text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image)
                {
                    return Show(caption?.ToString(), text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption?.ToString(), text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text)
                {
                    return Show(window, caption?.ToString(), text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image)
                {
                    return Show(window, caption?.ToString(), text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption?.ToString(), text?.ToString(), image, options);
                }
            }

            public static class No
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text)
                {
                    return Show(caption, text, MessageBoxImage.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.No);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.No, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text)
                {
                    return Show(window, caption, text, MessageBoxImage.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.No);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.No, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text)
                {
                    return Show(caption, text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image)
                {
                    return Show(caption, text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption, text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text)
                {
                    return Show(window, caption, text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image)
                {
                    return Show(window, caption, text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption, text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text)
                {
                    return Show(caption?.ToString(), text);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image)
                {
                    return Show(caption?.ToString(), text, image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption?.ToString(), text, image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text)
                {
                    return Show(window, caption?.ToString(), text);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image)
                {
                    return Show(window, caption?.ToString(), text, image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption?.ToString(), text, image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text)
                {
                    return Show(caption?.ToString(), text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image)
                {
                    return Show(caption?.ToString(), text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption?.ToString(), text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text)
                {
                    return Show(window, caption?.ToString(), text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image)
                {
                    return Show(window, caption?.ToString(), text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption?.ToString(), text?.ToString(), image, options);
                }
            }
        }
        
        public static class OKCancel
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, String? text)
            {
                return Show(caption, text, MessageBoxImage.None);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image)
            {
                return MessageBox.Show(text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.None);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return MessageBox.Show(text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.None, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, String? text)
            {
                return Show(window, caption, text, MessageBoxImage.None);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image)
            {
                return MessageBox.Show(window, text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.None);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return MessageBox.Show(window, text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.None, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, IString? text)
            {
                return Show(caption, text?.ToString());
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image)
            {
                return Show(caption, text?.ToString(), image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(caption, text?.ToString(), image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, IString? text)
            {
                return Show(window, caption, text?.ToString());
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image)
            {
                return Show(window, caption, text?.ToString(), image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(window, caption, text?.ToString(), image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, String? text)
            {
                return Show(caption?.ToString(), text);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image)
            {
                return Show(caption?.ToString(), text, image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(caption?.ToString(), text, image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, String? text)
            {
                return Show(window, caption?.ToString(), text);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image)
            {
                return Show(window, caption?.ToString(), text, image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(window, caption?.ToString(), text, image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, IString? text)
            {
                return Show(caption?.ToString(), text?.ToString());
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image)
            {
                return Show(caption?.ToString(), text?.ToString(), image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(caption?.ToString(), text?.ToString(), image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, IString? text)
            {
                return Show(window, caption?.ToString(), text?.ToString());
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image)
            {
                return Show(window, caption?.ToString(), text?.ToString(), image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(window, caption?.ToString(), text?.ToString(), image, options);
            }
            
            public static class OK
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text)
                {
                    return Show(caption, text, MessageBoxImage.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.OK);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.OK, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text)
                {
                    return Show(window, caption, text, MessageBoxImage.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.OK);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.OK, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text)
                {
                    return Show(caption, text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image)
                {
                    return Show(caption, text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption, text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text)
                {
                    return Show(window, caption, text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image)
                {
                    return Show(window, caption, text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption, text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text)
                {
                    return Show(caption?.ToString(), text);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image)
                {
                    return Show(caption?.ToString(), text, image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption?.ToString(), text, image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text)
                {
                    return Show(window, caption?.ToString(), text);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image)
                {
                    return Show(window, caption?.ToString(), text, image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption?.ToString(), text, image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text)
                {
                    return Show(caption?.ToString(), text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image)
                {
                    return Show(caption?.ToString(), text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption?.ToString(), text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text)
                {
                    return Show(window, caption?.ToString(), text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image)
                {
                    return Show(window, caption?.ToString(), text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption?.ToString(), text?.ToString(), image, options);
                }
            }

            public static class Cancel
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text)
                {
                    return Show(caption, text, MessageBoxImage.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.Cancel);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.Cancel, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text)
                {
                    return Show(window, caption, text, MessageBoxImage.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.Cancel);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.Cancel, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text)
                {
                    return Show(caption, text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image)
                {
                    return Show(caption, text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption, text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text)
                {
                    return Show(window, caption, text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image)
                {
                    return Show(window, caption, text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption, text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text)
                {
                    return Show(caption?.ToString(), text);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image)
                {
                    return Show(caption?.ToString(), text, image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption?.ToString(), text, image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text)
                {
                    return Show(window, caption?.ToString(), text);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image)
                {
                    return Show(window, caption?.ToString(), text, image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption?.ToString(), text, image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text)
                {
                    return Show(caption?.ToString(), text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image)
                {
                    return Show(caption?.ToString(), text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption?.ToString(), text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text)
                {
                    return Show(window, caption?.ToString(), text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image)
                {
                    return Show(window, caption?.ToString(), text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption?.ToString(), text?.ToString(), image, options);
                }
            }
        }
        
        public static class YesNoCancel
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, String? text)
            {
                return Show(caption, text, MessageBoxImage.None);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image)
            {
                return MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.None);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.None, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, String? text)
            {
                return Show(window, caption, text, MessageBoxImage.None);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image)
            {
                return MessageBox.Show(window, text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.None);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return MessageBox.Show(window, text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.None, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, IString? text)
            {
                return Show(caption, text?.ToString());
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image)
            {
                return Show(caption, text?.ToString(), image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(caption, text?.ToString(), image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, IString? text)
            {
                return Show(window, caption, text?.ToString());
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image)
            {
                return Show(window, caption, text?.ToString(), image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(window, caption, text?.ToString(), image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, String? text)
            {
                return Show(caption?.ToString(), text);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image)
            {
                return Show(caption?.ToString(), text, image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(caption?.ToString(), text, image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, String? text)
            {
                return Show(window, caption?.ToString(), text);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image)
            {
                return Show(window, caption?.ToString(), text, image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(window, caption?.ToString(), text, image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, IString? text)
            {
                return Show(caption?.ToString(), text?.ToString());
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image)
            {
                return Show(caption?.ToString(), text?.ToString(), image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(caption?.ToString(), text?.ToString(), image, options);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, IString? text)
            {
                return Show(window, caption?.ToString(), text?.ToString());
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image)
            {
                return Show(window, caption?.ToString(), text?.ToString(), image);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
            {
                return Show(window, caption?.ToString(), text?.ToString(), image, options);
            }
            
            public static class Yes
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text)
                {
                    return Show(caption, text, MessageBoxImage.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.Yes);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.Yes, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text)
                {
                    return Show(window, caption, text, MessageBoxImage.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.Yes);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.Yes, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text)
                {
                    return Show(caption, text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image)
                {
                    return Show(caption, text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption, text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text)
                {
                    return Show(window, caption, text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image)
                {
                    return Show(window, caption, text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption, text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text)
                {
                    return Show(caption?.ToString(), text);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image)
                {
                    return Show(caption?.ToString(), text, image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption?.ToString(), text, image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text)
                {
                    return Show(window, caption?.ToString(), text);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image)
                {
                    return Show(window, caption?.ToString(), text, image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption?.ToString(), text, image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text)
                {
                    return Show(caption?.ToString(), text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image)
                {
                    return Show(caption?.ToString(), text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption?.ToString(), text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text)
                {
                    return Show(window, caption?.ToString(), text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image)
                {
                    return Show(window, caption?.ToString(), text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption?.ToString(), text?.ToString(), image, options);
                }
            }

            public static class No
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text)
                {
                    return Show(caption, text, MessageBoxImage.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.No);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.No, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text)
                {
                    return Show(window, caption, text, MessageBoxImage.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.No);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.No, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text)
                {
                    return Show(caption, text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image)
                {
                    return Show(caption, text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption, text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text)
                {
                    return Show(window, caption, text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image)
                {
                    return Show(window, caption, text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption, text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text)
                {
                    return Show(caption?.ToString(), text);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image)
                {
                    return Show(caption?.ToString(), text, image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption?.ToString(), text, image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text)
                {
                    return Show(window, caption?.ToString(), text);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image)
                {
                    return Show(window, caption?.ToString(), text, image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption?.ToString(), text, image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text)
                {
                    return Show(caption?.ToString(), text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image)
                {
                    return Show(caption?.ToString(), text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption?.ToString(), text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text)
                {
                    return Show(window, caption?.ToString(), text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image)
                {
                    return Show(window, caption?.ToString(), text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption?.ToString(), text?.ToString(), image, options);
                }
            }

            public static class Cancel
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text)
                {
                    return Show(caption, text, MessageBoxImage.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.Cancel);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.Cancel, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text)
                {
                    return Show(window, caption, text, MessageBoxImage.None);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.Cancel);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.Cancel, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text)
                {
                    return Show(caption, text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image)
                {
                    return Show(caption, text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption, text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text)
                {
                    return Show(window, caption, text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image)
                {
                    return Show(window, caption, text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, String? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption, text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text)
                {
                    return Show(caption?.ToString(), text);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image)
                {
                    return Show(caption?.ToString(), text, image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption?.ToString(), text, image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text)
                {
                    return Show(window, caption?.ToString(), text);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image)
                {
                    return Show(window, caption?.ToString(), text, image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, String? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption?.ToString(), text, image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text)
                {
                    return Show(caption?.ToString(), text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image)
                {
                    return Show(caption?.ToString(), text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(caption?.ToString(), text?.ToString(), image, options);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text)
                {
                    return Show(window, caption?.ToString(), text?.ToString());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image)
                {
                    return Show(window, caption?.ToString(), text?.ToString(), image);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static MessageBoxResult Show(Window window, IString? caption, IString? text, MessageBoxImage image, MessageBoxOptions options)
                {
                    return Show(window, caption?.ToString(), text?.ToString(), image, options);
                }
            }
        }
    }
}