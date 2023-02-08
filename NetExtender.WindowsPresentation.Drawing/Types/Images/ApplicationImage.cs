// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Media.Imaging;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.IO;

namespace NetExtender.WindowsPresentation.Types.Images
{
    public class ApplicationImage
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator BitmapImage?(ApplicationImage? value)
        {
            return value?.Source;
        }

        private Lazy<BitmapImage> Internal { get; }

        public BitmapImage Source
        {
            get
            {
                return Internal.Value;
            }
        }
        
        public ApplicationImage(String path)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            }

            String @namespace = ReflectionUtilities.GetEntryAssemblyNamespace();
            Internal = new Lazy<BitmapImage>(() => Initialize(@namespace, path), true);
        }

        public ApplicationImage(String directory, String filename)
        {
            if (String.IsNullOrEmpty(directory))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(directory));
            }

            if (String.IsNullOrEmpty(filename))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(filename));
            }

            String @namespace = ReflectionUtilities.GetEntryAssemblyNamespace();
            Internal = new Lazy<BitmapImage>(() => Initialize(@namespace, directory, filename), true);
        }

        public ApplicationImage(String @namespace, String directory, String filename)
        {
            if (String.IsNullOrEmpty(@namespace))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(@namespace));
            }

            if (String.IsNullOrEmpty(directory))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(directory));
            }

            if (String.IsNullOrEmpty(filename))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(filename));
            }

            Internal = new Lazy<BitmapImage>(() => Initialize(@namespace, directory, filename), true);
        }

        protected BitmapImage Initialize(String @namespace, String directory, String filename)
        {
            if (@namespace is null)
            {
                throw new ArgumentNullException(nameof(@namespace));
            }

            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            if (filename is null)
            {
                throw new ArgumentNullException(nameof(filename));
            }

            return Initialize(@namespace, PathUtilities.AddEndSeparator(directory) + filename);
        }
        
        protected virtual BitmapImage Initialize(String @namespace, String path)
        {
            if (@namespace is null)
            {
                throw new ArgumentNullException(nameof(@namespace));
            }
            
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            
            return Initialize(new Uri($"pack://application:,,,/{@namespace};component/{path}"));
        }
        
        protected virtual BitmapImage Initialize(Uri uri)
        {
            return uri is not null ? new BitmapImage(uri) : throw new ArgumentNullException(nameof(uri));
        }
    }
}