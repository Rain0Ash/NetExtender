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
using JetBrains.Annotations;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.IO
{
    public enum ClipboardType
    {
        None,
        Text,
        Image,
        Audio,
        Files
    }
    
    public static class ClipboardUtils
    {
        private static IImmutableList<String> DataFormats { get; }
        
        static ClipboardUtils()
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
            return Clipboard.ContainsText();
        }
        
        public static Boolean ContainsText(TextDataFormat format)
        {
            return Clipboard.ContainsText(format);
        }
        
        public static Boolean ContainsImage()
        {
            return Clipboard.ContainsImage();
        }
        
        public static Boolean ContainsAudio()
        {
            return Clipboard.ContainsAudio();
        }
        
        public static Boolean ContainsFiles()
        {
            return Clipboard.ContainsFileDropList();
        }
        
        public static Boolean ContainsData(String format)
        {
            return Clipboard.ContainsData(format);
        }

        public static Boolean Contains()
        {
            return DataFormats.Any(Clipboard.ContainsData);
        }

        public static Boolean Contains(ClipboardType type)
        {
            return type switch
            {
                ClipboardType.None => IsEmpty,
                ClipboardType.Text => ContainsText(),
                ClipboardType.Image => ContainsImage(),
                ClipboardType.Audio => ContainsAudio(),
                ClipboardType.Files => ContainsFiles(),
                _ => throw new NotSupportedException()
            };
        }

        public static String GetText()
        {
            return Clipboard.GetText();
        }
        
        public static String GetText(TextDataFormat format)
        {
            return Clipboard.GetText(format);
        }
        
        public static Byte[]? GetRaw()
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
            return Clipboard.GetImage();
        }
        
        public static Stream? GetAudio()
        {
            return Clipboard.GetAudioStream();
        }
        
        public static StringCollection GetFiles()
        {
            return Clipboard.GetFileDropList();
        }
        
        public static IDataObject? GetData()
        {
            return Clipboard.GetDataObject();
        }
        
        public static Object GetData(String format)
        {
            return Clipboard.GetData(format);
        }
        
        public static Boolean SetText([NotNull] String text)
        {
            if (text is null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            Clipboard.SetText(text);
            return true;
        }

        public static Boolean SetText([NotNull] String text, TextDataFormat format)
        {
            if (text is null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            Clipboard.SetText(text, format);
            return true;
        }
        
        public static Boolean SetRaw([NotNull] Byte[] raw)
        {
            if (raw is null)
            {
                throw new ArgumentNullException(nameof(raw));
            }

            using MemoryStream stream = raw.ToStream();
            return SetRaw(stream);
        }

        public static Boolean SetRaw([NotNull] Stream stream)
        {
            if (stream is MemoryStream memory)
            {
                return SetRaw(memory);
            }

            using MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            
            return SetRaw(ms);
        }

        public static Boolean SetRaw([NotNull] MemoryStream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            try
            {
                DataObject data = new DataObject();
                data.SetData("rawbinary", stream, false);
                Clipboard.SetDataObject(data, true);
                return true;
            }
            catch (ExternalException)
            {
                return false;
            }
        }
        
        public static async Task<Boolean> SetRawAsync([NotNull] Byte[] raw)
        {
            if (raw is null)
            {
                throw new ArgumentNullException(nameof(raw));
            }

            await using MemoryStream stream = await raw.ToStreamAsync();
            return SetRaw(stream);
        }
        
        public static async Task<Boolean> SetRawAsync([NotNull] Stream stream)
        {
            if (stream is MemoryStream memory)
            {
                return SetRaw(memory);
            }

            await using MemoryStream ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            
            return SetRaw(ms);
        }
        
        public static Boolean SetImage([NotNull] BitmapSource image)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            Clipboard.SetImage(image);
            return true;
        }

        public static Boolean SetAudio([NotNull] Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Clipboard.SetAudio(stream);
            return true;
        }
        
        public static Boolean SetAudio([NotNull] Byte[] audio)
        {
            if (audio is null)
            {
                throw new ArgumentNullException(nameof(audio));
            }

            Clipboard.SetAudio(audio);
            return true;
        }
        
        public static Boolean SetFiles([NotNull] IEnumerable<String> files)
        {
            if (files is null)
            {
                throw new ArgumentNullException(nameof(files));
            }

            return SetFiles(files.ToStringCollection());
        }
        
        public static Boolean SetFiles([NotNull] StringCollection files)
        {
            if (files is null)
            {
                throw new ArgumentNullException(nameof(files));
            }

            Clipboard.SetFileDropList(files);
            return true;
        }
        
        public static Boolean SetData([NotNull] IDataObject data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            Clipboard.SetDataObject(data);
            return true;
        }
        
        public static Boolean SetData([NotNull] IDataObject data, Boolean copy)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            Clipboard.SetDataObject(data, copy);
            return true;
        }
        
        public static Boolean SetData([NotNull] Object data, [NotNull] String format)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            Clipboard.SetData(format, data);
            return true;
        }
        
        public static Boolean FlushSetText([NotNull] String text)
        {
            return SetText(text) && Flush();
        }

        public static Boolean FlushSetText([NotNull] String text, TextDataFormat format)
        {
            return SetText(text, format) && Flush();
        }
        
        public static Boolean FlushSetRaw([NotNull] Byte[] raw)
        {
            return SetRaw(raw) && Flush();
        }

        public static Boolean FlushSetRaw([NotNull] Stream stream)
        {
            return SetRaw(stream) && Flush();
        }

        public static Boolean FlushSetRaw([NotNull] MemoryStream stream)
        {
            return SetRaw(stream) && Flush();
        }
        
        public static async Task<Boolean> FlushSetRawAsync([NotNull] Byte[] raw)
        {
            return await SetRawAsync(raw) && Flush();
        }
        
        public static async Task<Boolean> FlushSetRawAsync([NotNull] Stream stream)
        {
            return await SetRawAsync(stream) && Flush();
        }
        
        public static Boolean FlushSetImage([NotNull] BitmapSource image)
        {
            return SetImage(image) && Flush();
        }

        public static Boolean FlushSetAudio([NotNull] Stream stream)
        {
            return SetAudio(stream) && Flush();
        }
        
        public static Boolean FlushSetAudio([NotNull] Byte[] audio)
        {
            return SetAudio(audio) && Flush();
        }
        
        public static Boolean FlushSetFiles([NotNull] IEnumerable<String> files)
        {
            return SetFiles(files) && Flush();
        }
        
        public static Boolean FlushSetFiles([NotNull] StringCollection files)
        {
            return SetFiles(files) && Flush();
        }
        
        public static Boolean FlushSetData([NotNull] IDataObject data)
        {
            return SetData(data) && Flush();
        }
        
        public static Boolean FlushSetData([NotNull] IDataObject data, Boolean copy)
        {
            return SetData(data, copy) && Flush();
        }
        
        public static Boolean FlushSetData([NotNull] Object data, [NotNull] String format)
        {
            return SetData(data, format) && Flush();
        }
        
        public static Boolean SetText([NotNull] String text, Boolean flush)
        {
            return flush ? FlushSetText(text) : SetText(text);
        }

        public static Boolean SetText([NotNull] String text, TextDataFormat format, Boolean flush)
        {
            return flush ? FlushSetText(text, format) : SetText(text, format);
        }
        
        public static Boolean SetRaw([NotNull] Byte[] raw, Boolean flush)
        {
            return flush ? FlushSetRaw(raw) : SetRaw(raw);
        }

        public static Boolean SetRaw([NotNull] Stream stream, Boolean flush)
        {
            return flush ? FlushSetRaw(stream) : SetRaw(stream);
        }

        public static Boolean SetRaw([NotNull] MemoryStream stream, Boolean flush)
        {
            return flush ? FlushSetRaw(stream) : SetRaw(stream);
        }
        
        public static Task<Boolean> SetRawAsync([NotNull] Byte[] raw, Boolean flush)
        {
            return flush ? FlushSetRawAsync(raw) : SetRawAsync(raw);
        }
        
        public static Task<Boolean> SetRawAsync([NotNull] Stream stream, Boolean flush)
        {
            return flush ? FlushSetRawAsync(stream) : SetRawAsync(stream);
        }
        
        public static Boolean SetImage([NotNull] BitmapSource image, Boolean flush)
        {
            return flush ? FlushSetImage(image) : SetImage(image);
        }

        public static Boolean SetAudio([NotNull] Stream stream, Boolean flush)
        {
            return flush ? FlushSetAudio(stream) : SetAudio(stream);
        }
        
        public static Boolean SetAudio([NotNull] Byte[] audio, Boolean flush)
        {
            return flush ? FlushSetAudio(audio) : SetAudio(audio);
        }
        
        public static Boolean SetFiles([NotNull] IEnumerable<String> files, Boolean flush)
        {
            return flush ? FlushSetFiles(files) : SetFiles(files);
        }
        
        public static Boolean SetFiles([NotNull] StringCollection files, Boolean flush)
        {
            return flush ? FlushSetFiles(files) : SetFiles(files);
        }

        public static Boolean SetData([NotNull] IDataObject data, Boolean copy, Boolean flush)
        {
            return flush ? FlushSetData(data, copy) : SetData(data, copy);
        }
        
        public static Boolean SetData([NotNull] Object data, [NotNull] String format, Boolean flush)
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
        public static Boolean IsCurrent([NotNull] IDataObject data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            return Clipboard.IsCurrent(data);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Flush()
        {
            Clipboard.Flush();
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Clear()
        {
            Clipboard.Clear();
            return true;
        }
    }
}