using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace NetExtender.Utilities.UserInterface
{
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public static class MessageBoxUtilities
    {
        public static class OK
        {
            public static class None
            {
                public static MessageBoxResult Show(String text, String caption)
                {
                    return Show(text, caption, MessageBoxImage.None);
                }

                public static MessageBoxResult Show(String text, String caption, MessageBoxImage image)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.OK, image, MessageBoxResult.None);
                }

                public static MessageBoxResult Show(String text, String caption, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.OK, image, MessageBoxResult.None, options);
                }

                public static MessageBoxResult Show(Window window, String text, String caption)
                {
                    return Show(window, text, caption, MessageBoxImage.None);
                }

                public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.OK, image, MessageBoxResult.None);
                }

                public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.OK, image, MessageBoxResult.None, options);
                }
            }

            public static MessageBoxResult Show(String text, String caption)
            {
                return Show(text, caption, MessageBoxImage.None);
            }

            public static MessageBoxResult Show(String text, String caption, MessageBoxImage image)
            {
                return MessageBox.Show(text, caption, MessageBoxButton.OK, image, MessageBoxResult.OK);
            }

            public static MessageBoxResult Show(String text, String caption, MessageBoxImage image, MessageBoxOptions options)
            {
                return MessageBox.Show(text, caption, MessageBoxButton.OK, image, MessageBoxResult.OK, options);
            }

            public static MessageBoxResult Show(Window window, String text, String caption)
            {
                return Show(window, text, caption, MessageBoxImage.None);
            }

            public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image)
            {
                return MessageBox.Show(window, text, caption, MessageBoxButton.OK, image, MessageBoxResult.OK);
            }

            public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image, MessageBoxOptions options)
            {
                return MessageBox.Show(window, text, caption, MessageBoxButton.OK, image, MessageBoxResult.OK, options);
            }
        }

        public static class YesNo
        {
            public static MessageBoxResult Show(String text, String caption)
            {
                return Show(text, caption, MessageBoxImage.None);
            }

            public static MessageBoxResult Show(String text, String caption, MessageBoxImage image)
            {
                return MessageBox.Show(text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.None);
            }

            public static MessageBoxResult Show(String text, String caption, MessageBoxImage image, MessageBoxOptions options)
            {
                return MessageBox.Show(text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.None, options);
            }

            public static MessageBoxResult Show(Window window, String text, String caption)
            {
                return Show(window, text, caption, MessageBoxImage.None);
            }

            public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image)
            {
                return MessageBox.Show(window, text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.None);
            }

            public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image, MessageBoxOptions options)
            {
                return MessageBox.Show(window, text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.None, options);
            }
            
            public static class Yes
            {
                public static MessageBoxResult Show(String text, String caption)
                {
                    return Show(text, caption, MessageBoxImage.None);
                }

                public static MessageBoxResult Show(String text, String caption, MessageBoxImage image)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.Yes);
                }

                public static MessageBoxResult Show(String text, String caption, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.Yes, options);
                }

                public static MessageBoxResult Show(Window window, String text, String caption)
                {
                    return Show(window, text, caption, MessageBoxImage.None);
                }

                public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.Yes);
                }

                public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.Yes, options);
                }
            }

            public static class No
            {
                public static MessageBoxResult Show(String text, String caption)
                {
                    return Show(text, caption, MessageBoxImage.None);
                }

                public static MessageBoxResult Show(String text, String caption, MessageBoxImage image)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.No);
                }

                public static MessageBoxResult Show(String text, String caption, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.No, options);
                }

                public static MessageBoxResult Show(Window window, String text, String caption)
                {
                    return Show(window, text, caption, MessageBoxImage.None);
                }

                public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.No);
                }

                public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNo, image, MessageBoxResult.No, options);
                }
            }
        }
        
        public static class OKCancel
        {
            public static MessageBoxResult Show(String text, String caption)
            {
                return Show(text, caption, MessageBoxImage.None);
            }

            public static MessageBoxResult Show(String text, String caption, MessageBoxImage image)
            {
                return MessageBox.Show(text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.None);
            }

            public static MessageBoxResult Show(String text, String caption, MessageBoxImage image, MessageBoxOptions options)
            {
                return MessageBox.Show(text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.None, options);
            }

            public static MessageBoxResult Show(Window window, String text, String caption)
            {
                return Show(window, text, caption, MessageBoxImage.None);
            }

            public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image)
            {
                return MessageBox.Show(window, text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.None);
            }

            public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image, MessageBoxOptions options)
            {
                return MessageBox.Show(window, text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.None, options);
            }
            
            public static class OK
            {
                public static MessageBoxResult Show(String text, String caption)
                {
                    return Show(text, caption, MessageBoxImage.None);
                }

                public static MessageBoxResult Show(String text, String caption, MessageBoxImage image)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.OK);
                }

                public static MessageBoxResult Show(String text, String caption, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.OK, options);
                }

                public static MessageBoxResult Show(Window window, String text, String caption)
                {
                    return Show(window, text, caption, MessageBoxImage.None);
                }

                public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.OK);
                }

                public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.OK, options);
                }
            }

            public static class Cancel
            {
                public static MessageBoxResult Show(String text, String caption)
                {
                    return Show(text, caption, MessageBoxImage.None);
                }

                public static MessageBoxResult Show(String text, String caption, MessageBoxImage image)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.Cancel);
                }

                public static MessageBoxResult Show(String text, String caption, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.Cancel, options);
                }

                public static MessageBoxResult Show(Window window, String text, String caption)
                {
                    return Show(window, text, caption, MessageBoxImage.None);
                }

                public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.Cancel);
                }

                public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.OKCancel, image, MessageBoxResult.Cancel, options);
                }
            }
        }
        
        public static class YesNoCancel
        {
            public static MessageBoxResult Show(String text, String caption)
            {
                return Show(text, caption, MessageBoxImage.None);
            }

            public static MessageBoxResult Show(String text, String caption, MessageBoxImage image)
            {
                return MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.None);
            }

            public static MessageBoxResult Show(String text, String caption, MessageBoxImage image, MessageBoxOptions options)
            {
                return MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.None, options);
            }

            public static MessageBoxResult Show(Window window, String text, String caption)
            {
                return Show(window, text, caption, MessageBoxImage.None);
            }

            public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image)
            {
                return MessageBox.Show(window, text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.None);
            }

            public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image, MessageBoxOptions options)
            {
                return MessageBox.Show(window, text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.None, options);
            }
            
            public static class Yes
            {
                public static MessageBoxResult Show(String text, String caption)
                {
                    return Show(text, caption, MessageBoxImage.None);
                }

                public static MessageBoxResult Show(String text, String caption, MessageBoxImage image)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.Yes);
                }

                public static MessageBoxResult Show(String text, String caption, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.Yes, options);
                }

                public static MessageBoxResult Show(Window window, String text, String caption)
                {
                    return Show(window, text, caption, MessageBoxImage.None);
                }

                public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.Yes);
                }

                public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.Yes, options);
                }
            }

            public static class No
            {
                public static MessageBoxResult Show(String text, String caption)
                {
                    return Show(text, caption, MessageBoxImage.None);
                }

                public static MessageBoxResult Show(String text, String caption, MessageBoxImage image)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.No);
                }

                public static MessageBoxResult Show(String text, String caption, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.No, options);
                }

                public static MessageBoxResult Show(Window window, String text, String caption)
                {
                    return Show(window, text, caption, MessageBoxImage.None);
                }

                public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.No);
                }

                public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.No, options);
                }
            }

            public static class Cancel
            {
                public static MessageBoxResult Show(String text, String caption)
                {
                    return Show(text, caption, MessageBoxImage.None);
                }

                public static MessageBoxResult Show(String text, String caption, MessageBoxImage image)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.Cancel);
                }

                public static MessageBoxResult Show(String text, String caption, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.Cancel, options);
                }

                public static MessageBoxResult Show(Window window, String text, String caption)
                {
                    return Show(window, text, caption, MessageBoxImage.None);
                }

                public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.Cancel);
                }

                public static MessageBoxResult Show(Window window, String text, String caption, MessageBoxImage image, MessageBoxOptions options)
                {
                    return MessageBox.Show(window, text, caption, MessageBoxButton.YesNoCancel, image, MessageBoxResult.Cancel, options);
                }
            }
        }
    }
}