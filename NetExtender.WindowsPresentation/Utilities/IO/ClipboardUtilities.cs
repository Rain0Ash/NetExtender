// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Threading;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Windows.IO
{
    public enum ClipboardType
    {
        None,
        Text,
        Raw,
        Image,
        Audio,
        Files
    }

    public static class ClipboardUtilities
    {
        private static IImmutableList<String> DataFormats { get; }

        static ClipboardUtilities()
        {
            DataFormats = typeof(DataFormats).GetFields(BindingFlags.Public | BindingFlags.Static).Select(x => x.Name).ToImmutableList();
        }

        public static Boolean IsEmpty
        {
            get
            {
                return !Contains();
            }
        }

        public static Boolean ContainsText()
        {
            return ThreadUtilities.IsSTA ? Clipboard.ContainsText() : ThreadUtilities.STA(Clipboard.ContainsText);
        }

        public static Boolean ContainsText(TextDataFormat format)
        {
            return ThreadUtilities.IsSTA ? Clipboard.ContainsText(format) : ThreadUtilities.STA(Clipboard.ContainsText, format);
        }

        public static Boolean ContainsRaw()
        {
            return ThreadUtilities.IsSTA ? ContainsRawInternal() : ThreadUtilities.STA(ContainsRawInternal);
        }

        private static Boolean ContainsRawInternal()
        {
            return Clipboard.GetDataObject() is DataObject data && data.GetDataPresent("rawbinary");
        }

        public static Boolean ContainsImage()
        {
            return ThreadUtilities.IsSTA ? Clipboard.ContainsImage() : ThreadUtilities.STA(Clipboard.ContainsImage);
        }

        public static Boolean ContainsAudio()
        {
            return ThreadUtilities.IsSTA ? Clipboard.ContainsAudio() : ThreadUtilities.STA(Clipboard.ContainsAudio);
        }

        public static Boolean ContainsFiles()
        {
            return ThreadUtilities.IsSTA ? Clipboard.ContainsFileDropList() : ThreadUtilities.STA(Clipboard.ContainsFileDropList);
        }

        public static Boolean ContainsData(String format)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            return ThreadUtilities.IsSTA ? Clipboard.ContainsData(format) : ThreadUtilities.STA(Clipboard.ContainsData, format);
        }

        public static Boolean Contains()
        {
            return ThreadUtilities.IsSTA ? DataFormats.Any(Clipboard.ContainsData) : ThreadUtilities.STA(() => DataFormats.Any(Clipboard.ContainsData));
        }

        public static Boolean Contains(ClipboardType type)
        {
            return type switch
            {
                ClipboardType.None => IsEmpty,
                ClipboardType.Raw => ContainsRaw(),
                ClipboardType.Text => ContainsText(),
                ClipboardType.Image => ContainsImage(),
                ClipboardType.Audio => ContainsAudio(),
                ClipboardType.Files => ContainsFiles(),
                _ => throw new EnumUndefinedOrNotSupportedException<ClipboardType>(type, nameof(type), null)
            };
        }

        public static String GetText()
        {
            return ThreadUtilities.IsSTA ? Clipboard.GetText() : ThreadUtilities.STA(Clipboard.GetText) ?? String.Empty;
        }

        public static String GetText(TextDataFormat format)
        {
            return ThreadUtilities.IsSTA ? Clipboard.GetText(format) : ThreadUtilities.STA(Clipboard.GetText, format) ?? String.Empty;
        }

        public static Byte[]? GetRaw()
        {
            return ThreadUtilities.IsSTA ? GetRawInternal() : ThreadUtilities.STA(GetRawInternal);
        }

        private static Byte[]? GetRawInternal()
        {
            if (Clipboard.GetDataObject() is not DataObject data || !data.GetDataPresent("rawbinary"))
            {
                return null;
            }

            using MemoryStream? raw = data.GetData("rawbinary") as MemoryStream;
            return raw?.ToArray();
        }

        public static BitmapSource? GetImage()
        {
            return ThreadUtilities.IsSTA ? Clipboard.GetImage() : ThreadUtilities.STA(Clipboard.GetImage);
        }

        public static Stream? GetAudio()
        {
            return ThreadUtilities.IsSTA ? Clipboard.GetAudioStream() : ThreadUtilities.STA(Clipboard.GetAudioStream);
        }

        public static StringCollection GetFiles()
        {
            return ThreadUtilities.IsSTA ? Clipboard.GetFileDropList() : ThreadUtilities.STA(Clipboard.GetFileDropList) ?? new StringCollection();
        }

        public static IDataObject? GetData()
        {
            return ThreadUtilities.IsSTA ? Clipboard.GetDataObject() : ThreadUtilities.STA(Clipboard.GetDataObject);
        }

        public static Object? GetData(String format)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            return ThreadUtilities.IsSTA ? Clipboard.GetData(format) : ThreadUtilities.STA(Clipboard.GetData, format);
        }

        public static Boolean SetText(String? text)
        {
            if (text is null)
            {
                return Clear();
            }

            try
            {
                if (ThreadUtilities.IsSTA)
                {
                    Clipboard.SetText(text);
                    return true;
                }

                ThreadUtilities.STA(Clipboard.SetText, text);
                return true;
            }
            catch (ExternalException)
            {
                return false;
            }
        }

        public static Boolean SetText(String? text, TextDataFormat format)
        {
            if (text is null)
            {
                return Clear();
            }

            try
            {
                if (ThreadUtilities.IsSTA)
                {
                    Clipboard.SetText(text, format);
                    return true;
                }

                ThreadUtilities.STA(Clipboard.SetText, text, format);
                return true;
            }
            catch (ExternalException)
            {
                return false;
            }
        }

        public static Boolean SetRaw(Byte[]? raw)
        {
            if (raw is null)
            {
                return Clear();
            }

            using MemoryStream stream = raw.ToStream();
            return SetRaw(stream);
        }

        public static Boolean SetRaw(Stream? stream)
        {
            if (stream is null)
            {
                return Clear();
            }

            if (stream is MemoryStream memory)
            {
                return SetRaw(memory);
            }

            using MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);

            return SetRaw(ms);
        }

        public static Boolean SetRaw(MemoryStream? stream)
        {
            if (stream is null)
            {
                return Clear();
            }

            try
            {
                DataObject data = new DataObject();
                data.SetData("rawbinary", stream, false);

                if (ThreadUtilities.IsSTA)
                {
                    Clipboard.SetDataObject(data, true);
                    return true;
                }

                ThreadUtilities.STA(Clipboard.SetDataObject, data, true);
                return true;
            }
            catch (ExternalException)
            {
                return false;
            }
        }

        public static async Task<Boolean> SetRawAsync(Byte[]? raw)
        {
            if (raw is null)
            {
                return Clear();
            }

            await using MemoryStream stream = await raw.ToStreamAsync().ConfigureAwait(false);
            return SetRaw(stream);
        }

        public static async Task<Boolean> SetRawAsync(Stream? stream)
        {
            if (stream is null)
            {
                return Clear();
            }

            if (stream is MemoryStream memory)
            {
                return SetRaw(memory);
            }

            await using MemoryStream ms = new MemoryStream();
            await stream.CopyToAsync(ms).ConfigureAwait(false);

            return SetRaw(ms);
        }

        public static Boolean SetImage(BitmapSource? image)
        {
            if (image is null)
            {
                return Clear();
            }

            try
            {
                if (ThreadUtilities.IsSTA)
                {
                    Clipboard.SetImage(image);
                    return true;
                }

                ThreadUtilities.STA(Clipboard.SetImage, image);
                return true;
            }
            catch (ExternalException)
            {
                return false;
            }
        }

        public static Boolean SetAudio(Stream? stream)
        {
            if (stream is null)
            {
                return Clear();
            }

            try
            {
                if (ThreadUtilities.IsSTA)
                {
                    Clipboard.SetAudio(stream);
                    return true;
                }

                ThreadUtilities.STA(Clipboard.SetAudio, stream);
                return true;
            }
            catch (ExternalException)
            {
                return false;
            }
        }

        public static Boolean SetAudio(Byte[]? audio)
        {
            if (audio is null)
            {
                return Clear();
            }

            try
            {
                if (ThreadUtilities.IsSTA)
                {
                    Clipboard.SetAudio(audio);
                    return true;
                }

                ThreadUtilities.STA(Clipboard.SetAudio, audio);
                return true;
            }
            catch (ExternalException)
            {
                return false;
            }
        }

        public static Boolean SetFiles(IEnumerable<String>? files)
        {
            return files is not null ? SetFiles(files.ToStringCollection()) : Clear();
        }

        public static Boolean SetFiles(StringCollection? files)
        {
            if (files is null)
            {
                return Clear();
            }

            try
            {
                if (ThreadUtilities.IsSTA)
                {
                    Clipboard.SetFileDropList(files);
                    return true;
                }

                ThreadUtilities.STA(Clipboard.SetFileDropList, files);
                return true;
            }
            catch (ExternalException)
            {
                return false;
            }
        }

        public static Boolean SetData(IDataObject? data)
        {
            if (data is null)
            {
                return Clear();
            }

            try
            {
                if (ThreadUtilities.IsSTA)
                {
                    Clipboard.SetDataObject(data);
                    return true;
                }

                ThreadUtilities.STA(Clipboard.SetDataObject, data);
                return true;
            }
            catch (ExternalException)
            {
                return false;
            }
        }

        public static Boolean SetData(IDataObject? data, Boolean copy)
        {
            if (data is null)
            {
                return Clear();
            }

            try
            {
                if (ThreadUtilities.IsSTA)
                {
                    Clipboard.SetDataObject(data, copy);
                    return true;
                }

                ThreadUtilities.STA(Clipboard.SetDataObject, data, copy);
                return true;
            }
            catch (ExternalException)
            {
                return false;
            }
        }

        public static Boolean SetData(Object? data, String format)
        {
            if (data is null)
            {
                return Clear();
            }

            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            try
            {
                if (ThreadUtilities.IsSTA)
                {
                    Clipboard.SetData(format, data);
                    return true;
                }

                ThreadUtilities.STA(Clipboard.SetData, format, data);
                return true;
            }
            catch (ExternalException)
            {
                return false;
            }
        }

        public static Boolean FlushSetText(String text)
        {
            return SetText(text) && Flush();
        }

        public static Boolean FlushSetText(String text, TextDataFormat format)
        {
            return SetText(text, format) && Flush();
        }

        public static Boolean FlushSetRaw(Byte[] raw)
        {
            return SetRaw(raw) && Flush();
        }

        public static Boolean FlushSetRaw(Stream stream)
        {
            return SetRaw(stream) && Flush();
        }

        public static Boolean FlushSetRaw(MemoryStream stream)
        {
            return SetRaw(stream) && Flush();
        }

        public static async Task<Boolean> FlushSetRawAsync(Byte[] raw)
        {
            return await SetRawAsync(raw).ConfigureAwait(false) && Flush();
        }

        public static async Task<Boolean> FlushSetRawAsync(Stream stream)
        {
            return await SetRawAsync(stream).ConfigureAwait(false) && Flush();
        }

        public static Boolean FlushSetImage(BitmapSource image)
        {
            return SetImage(image) && Flush();
        }

        public static Boolean FlushSetAudio(Stream stream)
        {
            return SetAudio(stream) && Flush();
        }

        public static Boolean FlushSetAudio(Byte[] audio)
        {
            return SetAudio(audio) && Flush();
        }

        public static Boolean FlushSetFiles(IEnumerable<String> files)
        {
            return SetFiles(files) && Flush();
        }

        public static Boolean FlushSetFiles(StringCollection files)
        {
            return SetFiles(files) && Flush();
        }

        public static Boolean FlushSetData(IDataObject data)
        {
            return SetData(data) && Flush();
        }

        public static Boolean FlushSetData(IDataObject data, Boolean copy)
        {
            return SetData(data, copy) && Flush();
        }

        public static Boolean FlushSetData(Object data, String format)
        {
            return SetData(data, format) && Flush();
        }

        public static Boolean SetText(String text, Boolean flush)
        {
            return flush ? FlushSetText(text) : SetText(text);
        }

        public static Boolean SetText(String text, TextDataFormat format, Boolean flush)
        {
            return flush ? FlushSetText(text, format) : SetText(text, format);
        }

        public static Boolean SetRaw(Byte[] raw, Boolean flush)
        {
            return flush ? FlushSetRaw(raw) : SetRaw(raw);
        }

        public static Boolean SetRaw(Stream stream, Boolean flush)
        {
            return flush ? FlushSetRaw(stream) : SetRaw(stream);
        }

        public static Boolean SetRaw(MemoryStream stream, Boolean flush)
        {
            return flush ? FlushSetRaw(stream) : SetRaw(stream);
        }

        public static Task<Boolean> SetRawAsync(Byte[] raw, Boolean flush)
        {
            return flush ? FlushSetRawAsync(raw) : SetRawAsync(raw);
        }

        public static Task<Boolean> SetRawAsync(Stream stream, Boolean flush)
        {
            return flush ? FlushSetRawAsync(stream) : SetRawAsync(stream);
        }

        public static Boolean SetImage(BitmapSource image, Boolean flush)
        {
            return flush ? FlushSetImage(image) : SetImage(image);
        }

        public static Boolean SetAudio(Stream stream, Boolean flush)
        {
            return flush ? FlushSetAudio(stream) : SetAudio(stream);
        }

        public static Boolean SetAudio(Byte[] audio, Boolean flush)
        {
            return flush ? FlushSetAudio(audio) : SetAudio(audio);
        }

        public static Boolean SetFiles(IEnumerable<String> files, Boolean flush)
        {
            return flush ? FlushSetFiles(files) : SetFiles(files);
        }

        public static Boolean SetFiles(StringCollection files, Boolean flush)
        {
            return flush ? FlushSetFiles(files) : SetFiles(files);
        }

        public static Boolean SetData(IDataObject data, Boolean copy, Boolean flush)
        {
            return flush ? FlushSetData(data, copy) : SetData(data, copy);
        }

        public static Boolean SetData(Object data, String format, Boolean flush)
        {
            return flush ? FlushSetData(data, format) : SetData(data, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboard(this String text)
        {
            return SetText(text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboard(this String text, TextDataFormat format)
        {
            return SetText(text, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboard(this Byte[] value)
        {
            return SetRaw(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ToClipboardAsync(this Byte[] value)
        {
            return SetRawAsync(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboard(this MemoryStream value)
        {
            return SetRaw(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboard(this BitmapSource image)
        {
            return SetImage(image);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardAudio(this Stream stream)
        {
            return SetAudio(stream);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardAudio(this Byte[] audio)
        {
            return SetAudio(audio);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardFiles(this IEnumerable<String> files)
        {
            return SetFiles(files);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboard(this IDataObject data)
        {
            return SetData(data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboard(this IDataObject data, Boolean copy)
        {
            return SetData(data, copy);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardData(this Object data, String format)
        {
            return SetData(data, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardFlush(this String text)
        {
            return FlushSetText(text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardFlush(this String text, TextDataFormat format)
        {
            return FlushSetText(text, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardFlush(this Byte[] value)
        {
            return FlushSetRaw(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ToClipboardFlushAsync(this Byte[] value)
        {
            return FlushSetRawAsync(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardFlush(this MemoryStream value)
        {
            return FlushSetRaw(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardFlush(this BitmapSource image)
        {
            return FlushSetImage(image);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardFlushAudio(this Stream stream)
        {
            return FlushSetAudio(stream);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardFlushAudio(this Byte[] audio)
        {
            return FlushSetAudio(audio);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardFlushFiles(this IEnumerable<String> files)
        {
            return FlushSetFiles(files);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardFlush(this IDataObject data)
        {
            return FlushSetData(data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardFlush(this IDataObject data, Boolean copy)
        {
            return FlushSetData(data, copy);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardFlushData(this Object data, String format)
        {
            return FlushSetData(data, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboard(this String text, Boolean flush)
        {
            return SetText(text, flush);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboard(this String text, TextDataFormat format, Boolean flush)
        {
            return SetText(text, format, flush);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboard(this Byte[] value, Boolean flush)
        {
            return SetRaw(value, flush);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ToClipboardAsync(this Byte[] value, Boolean flush)
        {
            return SetRawAsync(value, flush);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboard(this MemoryStream value, Boolean flush)
        {
            return SetRaw(value, flush);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboard(this BitmapSource image, Boolean flush)
        {
            return SetImage(image, flush);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardAudio(this Stream stream, Boolean flush)
        {
            return SetAudio(stream, flush);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardAudio(this Byte[] audio, Boolean flush)
        {
            return SetAudio(audio, flush);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardFiles(this IEnumerable<String> files, Boolean flush)
        {
            return SetFiles(files, flush);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboard(this IDataObject data, Boolean copy, Boolean flush)
        {
            return SetData(data, copy, flush);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToClipboardData(this Object data, String format, Boolean flush)
        {
            return SetData(data, format, flush);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsCurrent(IDataObject? data)
        {
            try
            {
                return data is null ? IsEmpty : ThreadUtilities.IsSTA ? Clipboard.IsCurrent(data) : ThreadUtilities.STA(Clipboard.IsCurrent, data);
            }
            catch (ExternalException)
            {
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean FlushInternal()
        {
            try
            {
                Clipboard.Flush();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Flush()
        {
            return ThreadUtilities.IsSTA ? FlushInternal() : ThreadUtilities.STA(FlushInternal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean ClearInternal()
        {
            try
            {
                Clipboard.Clear();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Clear()
        {
            return ThreadUtilities.IsSTA ? ClearInternal() : ThreadUtilities.STA(ClearInternal);
        }
    }
}