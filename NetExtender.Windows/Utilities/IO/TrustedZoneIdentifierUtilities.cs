// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Text;
using NetExtender.IO.FileSystem.NTFS.DataStreams;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Types;
using NetExtender.Utilities.Windows.IO;

namespace NetExtender.Windows.Utilities.IO
{
    public enum TrustedZoneIdentifier : Byte
    {
        LocalMachine = 0,
        Intranet = 1,
        Trusted = 2,
        Internet = 3,
        Restricted = 4
    }

    public static class TrustedZoneIdentifierUtilities
    {
        private const String ZoneIdentifier = "Zone.Identifier";
        private const String ZoneTransfer = "[ZoneTransfer]";
        private const String ZoneId = "ZoneId=";

        public static String? ReadTrustedZoneIdentifier(String path)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!PathUtilities.IsExist(path))
            {
                throw new FileNotFoundException(null, path);
            }

            return AlternateStreamUtilities.IsAlternateDataStreamExists(path, ZoneIdentifier) ? AlternateStreamUtilities.ReadAlternateDataStream(path, ZoneIdentifier) : null;
        }

        public static String? ReadTrustedZoneIdentifier(this FileSystemInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (!info.Exists)
            {
                throw new FileNotFoundException(null, info.FullName);
            }

            return info.IsAlternateDataStreamExists(ZoneIdentifier) ? info.ReadAlternateDataStream(ZoneIdentifier) : null;
        }

        private static TrustedZoneIdentifier GetTrustedZoneIdentifierInternal(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Int32 index = value.IndexOf(ZoneId, StringComparison.Ordinal);

            if (index < 0 || (index += ZoneId.Length) >= value.Length)
            {
                throw new InvalidOperationException();
            }

            return value[index] switch
            {
                '0' => TrustedZoneIdentifier.LocalMachine,
                '1' => TrustedZoneIdentifier.Intranet,
                '2' => TrustedZoneIdentifier.Trusted,
                '3' => TrustedZoneIdentifier.Internet,
                '4' => TrustedZoneIdentifier.Restricted,
                _ => throw new InvalidOperationException()
            };
        }

        public static TrustedZoneIdentifier GetTrustedZoneIdentifier(String path)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!PathUtilities.IsExist(path))
            {
                throw new FileNotFoundException(null, path);
            }

            String? value = ReadTrustedZoneIdentifier(path);

            if (value is null)
            {
                throw new FileNotFoundException(null, path + ":" + ZoneIdentifier);
            }

            return GetTrustedZoneIdentifierInternal(value);
        }

        public static TrustedZoneIdentifier GetTrustedZoneIdentifier(this FileSystemInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (!info.Exists)
            {
                throw new FileNotFoundException(null, info.FullName);
            }

            String? value = info.ReadTrustedZoneIdentifier();

            if (value is null)
            {
                throw new FileNotFoundException(null, info.FullName + ":" + ZoneIdentifier);
            }

            return GetTrustedZoneIdentifierInternal(value);
        }

        private static Boolean TryGetTrustedZoneIdentifierInternal(String? value, out TrustedZoneIdentifier identifier)
        {
            if (value is null)
            {
                identifier = default;
                return false;
            }
            
            try
            {
                Int32 index = value.IndexOf(ZoneId, StringComparison.Ordinal);

                if (index < 0 || (index += ZoneId.Length) >= value.Length)
                {
                    identifier = default;
                    return false;
                }

                switch (value[index])
                {
                    case '0':
                        identifier = TrustedZoneIdentifier.LocalMachine;
                        return true;
                    case '1':
                        identifier = TrustedZoneIdentifier.Intranet;
                        return true;
                    case '2':
                        identifier = TrustedZoneIdentifier.Trusted;
                        return true;
                    case '3':
                        identifier = TrustedZoneIdentifier.Internet;
                        return true;
                    case '4':
                        identifier = TrustedZoneIdentifier.Restricted;
                        return true;
                    default:
                        identifier = default;
                        return false;
                }
            }
            catch (Exception)
            {
                identifier = default;
                return false;
            }
        }

        public static Boolean TryGetTrustedZoneIdentifier(String path, out TrustedZoneIdentifier identifier)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            try
            {
                if (PathUtilities.IsExist(path))
                {
                    return TryGetTrustedZoneIdentifierInternal(ReadTrustedZoneIdentifier(path), out identifier);
                }

                identifier = default;
                return false;
            }
            catch (Exception)
            {
                identifier = default;
                return false;
            }
        }

        public static Boolean TryGetTrustedZoneIdentifier(this FileSystemInfo info, out TrustedZoneIdentifier identifier)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            try
            {
                if (info.Exists)
                {
                    return TryGetTrustedZoneIdentifierInternal(info.ReadTrustedZoneIdentifier(), out identifier);
                }

                identifier = default;
                return false;
            }
            catch (Exception)
            {
                identifier = default;
                return false;
            }
        }

        public static Boolean CreateTrustedZoneIdentifier(String path, TrustedZoneIdentifier identifier)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!PathUtilities.IsExist(path))
            {
                throw new FileNotFoundException(null, path);
            }

            TryDeleteTrustedZoneIdentifier(path);
            AlternateDataStreamInfo alternate = AlternateStreamUtilities.OpenAlternateDataStream(path, ZoneIdentifier, FileMode.Create);
            using FileStream? stream = alternate.OpenWrite();

            if (stream is null)
            {
                return false;
            }

            stream.WriteLine(ZoneTransfer, Encoding.UTF8);
            stream.WriteLine($"ZoneId={(Byte) identifier}", Encoding.UTF8);
            return true;
        }

        public static Boolean CreateTrustedZoneIdentifier(this FileSystemInfo info, TrustedZoneIdentifier identifier)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (!info.Exists)
            {
                throw new FileNotFoundException(null, info.FullName);
            }

            info.TryDeleteTrustedZoneIdentifier();
            AlternateDataStreamInfo alternate = info.OpenAlternateDataStream(ZoneIdentifier, FileMode.Create);
            using FileStream? stream = alternate.OpenWrite();

            if (stream is null)
            {
                return false;
            }

            stream.WriteLine(ZoneTransfer, Encoding.UTF8);
            stream.WriteLine($"ZoneId={(Byte) identifier}", Encoding.UTF8);
            return true;
        }

        public static Boolean TryCreateTrustedZoneIdentifier(String path, TrustedZoneIdentifier identifier)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            try
            {
                if (!PathUtilities.IsExist(path))
                {
                    return false;
                }

                TryDeleteTrustedZoneIdentifier(path);
                AlternateDataStreamInfo alternate = AlternateStreamUtilities.OpenAlternateDataStream(path, ZoneIdentifier, FileMode.Create);
                using FileStream? stream = alternate.OpenWrite();

                if (stream is null)
                {
                    return false;
                }

                stream.WriteLine(ZoneTransfer, Encoding.UTF8);
                stream.WriteLine($"ZoneId={(Byte) identifier}", Encoding.UTF8);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean TryCreateTrustedZoneIdentifier(this FileSystemInfo info, TrustedZoneIdentifier identifier)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            try
            {
                if (!info.Exists)
                {
                    return false;
                }
                
                info.TryDeleteTrustedZoneIdentifier();
                AlternateDataStreamInfo alternate = info.OpenAlternateDataStream(ZoneIdentifier, FileMode.Create);
                using FileStream? stream = alternate.OpenWrite();

                if (stream is null)
                {
                    return false;
                }

                stream.WriteLine(ZoneTransfer, Encoding.UTF8);
                stream.WriteLine($"ZoneId={(Byte) identifier}", Encoding.UTF8);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // ReSharper disable once CognitiveComplexity
        private static Boolean SetTrustedZoneIdentifierInternal(AlternateDataStreamInfo alternate, TrustedZoneIdentifier identifier)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            String value;

            using (StreamReader reader = alternate.OpenText())
            {
                value = reader.ReadToEnd();
            }

            using FileStream? stream = alternate.OpenWrite();

            if (stream is null)
            {
                return false;
            }

            stream.Seek(0, SeekOrigin.Begin);

            Int32 index = value.IndexOf(ZoneId, StringComparison.Ordinal);

            if (index < 0)
            {
                if (value.Contains(ZoneTransfer, StringComparison.Ordinal))
                {
                    stream.WriteLine(value.Replace(ZoneTransfer, ZoneTransfer + Environment.NewLine + $"ZoneId={(Byte) identifier}", StringComparison.Ordinal), Encoding.UTF8);
                    return true;
                }

                stream.WriteLine(ZoneTransfer, Encoding.UTF8);
                stream.WriteLine($"ZoneId={(Byte) identifier}", Encoding.UTF8);
                stream.WriteLine(value, Encoding.UTF8);
                return true;
            }

            if ((index += ZoneId.Length) >= value.Length)
            {
                throw new InvalidOperationException();
            }

            Char character = value[index];

            switch (identifier)
            {
                case TrustedZoneIdentifier.LocalMachine:
                    if (character == '0')
                    {
                        return true;
                    }

                    break;
                case TrustedZoneIdentifier.Intranet:
                    if (character == '1')
                    {
                        return true;
                    }

                    break;
                case TrustedZoneIdentifier.Trusted:
                    if (character == '2')
                    {
                        return true;
                    }

                    break;
                case TrustedZoneIdentifier.Internet:
                    if (character == '3')
                    {
                        return true;
                    }

                    break;
                case TrustedZoneIdentifier.Restricted:
                    if (character == '4')
                    {
                        return true;
                    }

                    break;
                default:
                    throw new NotSupportedException();
            }

            if (character < '0' || character > '4')
            {
                throw new InvalidOperationException();
            }

            StringBuilder builder = value.ToStringBuilder(value.Length);

            builder[index] = identifier switch
            {
                TrustedZoneIdentifier.LocalMachine => '0',
                TrustedZoneIdentifier.Intranet => '1',
                TrustedZoneIdentifier.Trusted => '2',
                TrustedZoneIdentifier.Internet => '3',
                TrustedZoneIdentifier.Restricted => '4',
                _ => throw new NotSupportedException()
            };

            stream.WriteLine(builder.ToString(), Encoding.UTF8);
            return true;
        }

        public static Boolean SetTrustedZoneIdentifier(String path, TrustedZoneIdentifier identifier)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!PathUtilities.IsExist(path))
            {
                throw new FileNotFoundException(null, path);
            }

            AlternateDataStreamInfo alternate = AlternateStreamUtilities.OpenAlternateDataStream(path, ZoneIdentifier, FileMode.OpenOrCreate);
            return SetTrustedZoneIdentifierInternal(alternate, identifier);
        }

        public static Boolean SetTrustedZoneIdentifier(this FileSystemInfo info, TrustedZoneIdentifier identifier)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (!info.Exists)
            {
                throw new FileNotFoundException(null, info.FullName);
            }

            AlternateDataStreamInfo alternate = info.OpenAlternateDataStream(ZoneIdentifier, FileMode.OpenOrCreate);
            return SetTrustedZoneIdentifierInternal(alternate, identifier);
        }

        // ReSharper disable once CognitiveComplexity
        private static Boolean TrySetTrustedZoneIdentifierInternal(AlternateDataStreamInfo alternate, TrustedZoneIdentifier identifier)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            try
            {
                String value;

                using (StreamReader reader = alternate.OpenText())
                {
                    value = reader.ReadToEnd();
                }

                using FileStream? stream = alternate.OpenWrite();

                if (stream is null)
                {
                    return false;
                }

                stream.Seek(0, SeekOrigin.Begin);

                Int32 index = value.IndexOf(ZoneId, StringComparison.Ordinal);

                if (index < 0)
                {
                    if (value.Contains(ZoneTransfer, StringComparison.Ordinal))
                    {
                        stream.WriteLine(value.Replace(ZoneTransfer, ZoneTransfer + Environment.NewLine + $"ZoneId={(Byte) identifier}", StringComparison.Ordinal), Encoding.UTF8);
                        return true;
                    }

                    stream.WriteLine(ZoneTransfer, Encoding.UTF8);
                    stream.WriteLine($"ZoneId={(Byte) identifier}", Encoding.UTF8);
                    stream.WriteLine(value, Encoding.UTF8);
                    return true;
                }

                if ((index += ZoneId.Length) >= value.Length)
                {
                    return false;
                }

                Char character = value[index];

                switch (identifier)
                {
                    case TrustedZoneIdentifier.LocalMachine:
                        if (character == '0')
                        {
                            return true;
                        }

                        break;
                    case TrustedZoneIdentifier.Intranet:
                        if (character == '1')
                        {
                            return true;
                        }

                        break;
                    case TrustedZoneIdentifier.Trusted:
                        if (character == '2')
                        {
                            return true;
                        }

                        break;
                    case TrustedZoneIdentifier.Internet:
                        if (character == '3')
                        {
                            return true;
                        }

                        break;
                    case TrustedZoneIdentifier.Restricted:
                        if (character == '4')
                        {
                            return true;
                        }

                        break;
                    default:
                        return false;
                }

                if (character < '0' || character > '4')
                {
                    return false;
                }

                StringBuilder builder = value.ToStringBuilder(value.Length);

                switch (identifier)
                {
                    case TrustedZoneIdentifier.LocalMachine:
                        builder[index] = '0';
                        break;
                    case TrustedZoneIdentifier.Intranet:
                        builder[index] = '1';
                        break;
                    case TrustedZoneIdentifier.Trusted:
                        builder[index] = '2';
                        break;
                    case TrustedZoneIdentifier.Internet:
                        builder[index] = '3';
                        break;
                    case TrustedZoneIdentifier.Restricted:
                        builder[index] = '4';
                        break;
                    default:
                        return false;
                }

                stream.WriteLine(builder.ToString(), Encoding.UTF8);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean TrySetTrustedZoneIdentifier(String path, TrustedZoneIdentifier identifier)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            try
            {
                if (!PathUtilities.IsExist(path))
                {
                    return false;
                }

                AlternateDataStreamInfo alternate = AlternateStreamUtilities.OpenAlternateDataStream(path, ZoneIdentifier, FileMode.OpenOrCreate);
                return TrySetTrustedZoneIdentifierInternal(alternate, identifier);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean TrySetTrustedZoneIdentifier(this FileSystemInfo info, TrustedZoneIdentifier identifier)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            try
            {
                if (!info.Exists)
                {
                    return false;
                }

                AlternateDataStreamInfo alternate = info.OpenAlternateDataStream(ZoneIdentifier, FileMode.OpenOrCreate);
                return TrySetTrustedZoneIdentifierInternal(alternate, identifier);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean DeleteTrustedZoneIdentifier(String path)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return PathUtilities.IsExist(path) && AlternateStreamUtilities.DeleteAlternateDataStream(path, ZoneIdentifier);
        }

        public static Boolean DeleteTrustedZoneIdentifier(this FileSystemInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.Exists && info.DeleteAlternateDataStream(ZoneIdentifier);
        }

        public static Boolean TryDeleteTrustedZoneIdentifier(String path)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            try
            {
                return PathUtilities.IsExist(path) && AlternateStreamUtilities.DeleteAlternateDataStream(path, ZoneIdentifier);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean TryDeleteTrustedZoneIdentifier(this FileSystemInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            try
            {
                return info.Exists && info.DeleteAlternateDataStream(ZoneIdentifier);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}