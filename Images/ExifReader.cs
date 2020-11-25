using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace NetExtender.Images
{
    /// <summary>
    /// A class for reading Exif data from a JPEG file. The file will be open for reading for as long as the class exists.
    /// </summary>
    public sealed class ExifReader : IDisposable
    {
        private readonly Stream _stream;
        private readonly BinaryReader _reader;

        /// <summary>
        /// If set, the underlying stream will not be closed when the reader is disposed
        /// </summary>
        private readonly Boolean _leaveOpen;

        private static readonly Regex NullDateTimeMatcher = new Regex(@"^[\s0]{4}[:\s][\s0]{2}[:\s][\s0]{5}[:\s][\s0]{2}[:\s][\s0]{2}$");

        /// <summary>
        /// The primary tag catalogue (absolute file offsets to tag data, indexed by tag ID)
        /// </summary>
        private Dictionary<UInt16, Int64> _ifd0PrimaryCatalogue;

        /// <summary>
        /// The EXIF tag catalogue (absolute file offsets to tag data, indexed by tag ID)
        /// </summary>
        private Dictionary<UInt16, Int64> _ifdExifCatalogue;

        /// <summary>
        /// The GPS tag catalogue (absolute file offsets to tag data, indexed by tag ID)
        /// </summary>
        private Dictionary<UInt16, Int64> _ifdGPSCatalogue;

        /// <summary>
        /// The thumbnail tag catalogue (absolute file offsets to tag data, indexed by tag ID)
        /// </summary>
        /// <remarks>JPEG images contain 2 main sections - one for the main image (which contains most of the useful EXIF data), and one for the thumbnail
        /// image (which contains little more than the thumbnail itself). This catalogue is only used by <see cref="GetJpegThumbnailBytes"/>.</remarks>
        private Dictionary<UInt16, Int64> _ifd1Catalogue;

        /// <summary>
        /// Indicates whether to read data using big or little endian byte aligns
        /// </summary>
        private Boolean _isLittleEndian;

        /// <summary>
        /// The position in the filestream at which the TIFF header starts
        /// </summary>
        private Int64 _tiffHeaderStart;

        private static readonly Dictionary<UInt16, IFD> IfdLookup;

        static ExifReader()
        {
            // Prepare the tag-IFD lookup table
            IfdLookup = new Dictionary<UInt16, IFD>();

            Type type = typeof(ExifTags);

            FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo tag in fields)
            {
                IFDAttribute ifd = (IFDAttribute) tag.GetCustomAttributes(typeof(IFDAttribute), false)[0];
                IfdLookup[(UInt16) (tag.GetValue(null) ?? throw new ArgumentNullException())] = ifd.IFD;
            }
        }

        public ExifReader(String path)
            : this(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), false, true)
        {
        }

        public ExifReader(Stream stream)
            : this(stream, false, false)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="leaveOpen">Indicates whether <see cref="stream"/> should be closed when <see cref="Dispose"/> is called</param>
        /// <param name="istream">Indicates whether <see cref="stream"/> was instantiated by this reader</param>
        private ExifReader(Stream stream, Boolean leaveOpen, Boolean istream)
        {
            _stream = stream;
            _leaveOpen = leaveOpen;
            Int64 position = 0;

            try
            {
                if (stream is null)
                {
                    throw new ArgumentNullException(nameof(stream));
                }

                if (!stream.CanSeek)
                {
                    throw new ExifException("Requires a seekable stream");
                }

                // JPEG encoding uses big endian (i.e. Motorola) byte aligns. The TIFF encoding
                // found later in the document will specify the byte aligns used for the rest of the document.
                _isLittleEndian = false;

                // The initial stream position is cached so it can be restored in the case of an exception within this constructor
                position = stream.Position;

                _reader = new BinaryReader(_stream);

                // Make sure the file's a JPEG. If the file length is less than 2 bytes, an EndOfStreamException will be thrown.
                if (ReadUInt16() != 0xFFD8)
                {
                    throw new ExifException("File is not a valid JPEG");
                }

                // Scan to the start of the Exif content
                try
                {
                    ReadToExifStart();
                }
                catch (Exception ex)
                {
                    throw new ExifException("Unable to locate EXIF content", ex);
                }

                // Create an index of all Exif tags found within the document
                try
                {
                    CreateTagIndex();
                }
                catch (Exception ex)
                {
                    throw new ExifException("Error indexing EXIF tags", ex);
                }
            }
            catch (Exception)
            {
                // Cleanup. Note that the stream is not closed unless it was created internally
                try
                {
                    _reader?.Close();

                    if (_stream is not null)
                    {
                        if (istream)
                        {
                            _stream.Dispose();
                        }
                        else if (_stream.CanSeek)
                        {
                            // Try to restore the stream to its initial position
                            _stream.Position = position;
                        }
                    }
                }
                catch (Exception)
                {
                    // ignored
                }

                throw;
            }
        }
        
        /// <summary>
        /// Returns the length (in bytes) per component of the specified TIFF data type
        /// </summary>
        /// <returns></returns>
        private static Byte GetTiffFieldLength(UInt16 type)
        {
            switch (type)
            {
                case 0:
                    // Unknown datatype, therefore it can't be interpreted reliably
                    return 0;
                case 1:
                case 2:
                case 7:
                case 6:
                    return 1;
                case 3:
                case 8:
                    return 2;
                case 4:
                case 9:
                case 11:
                    return 4;
                case 5:
                case 10:
                case 12:
                    return 8;
                default:
                    throw new ExifException($"Unknown TIFF datatype: {type}");
            }
        }

        /// <summary>
        /// Gets a 2 byte unsigned integer from the file
        /// </summary>
        /// <returns></returns>
        private UInt16 ReadUInt16()
        {
            return ToUInt16(ReadBytes(2));
        }

        /// <summary>
        /// Gets a 4 byte unsigned integer from the file
        /// </summary>
        /// <returns></returns>
        private UInt32 ReadUInt32()
        {
            return ToUInt32(ReadBytes(4));
        }

        private String ReadString(Int32 chars)
        {
            Byte[] bytes = ReadBytes(chars);
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        private Byte[] ReadBytes(Int32 count)
        {
            Byte[] bytes = _reader.ReadBytes(count);

            // ReadBytes may return less than the bytes requested if the end of the stream is reached
            if (bytes.Length != count)
            {
                throw new EndOfStreamException();
            }

            return bytes;
        }

        /// <summary>
        /// Reads some bytes from the specified TIFF offset
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private Byte[] ReadBytes(UInt16 offset, Int32 count)
        {
            // Keep the current file offset
            Int64 original = _stream.Position;

            // Move to the TIFF offset and retrieve the data
            _stream.Seek(offset + _tiffHeaderStart, SeekOrigin.Begin);

            Byte[] data = _reader.ReadBytes(count);

            // Restore the file offset
            _stream.Position = original;

            return data;
        }

        /// <summary>
        /// Converts 8 bytes to the numerator and denominator
        /// components of an unsigned rational using the current byte aligns
        /// </summary>
        private UInt32[] ToURationalFraction(Byte[] data)
        {
            Byte[] numeratorData = new Byte[4];
            Byte[] denominatorData = new Byte[4];

            Array.Copy(data, numeratorData, 4);
            Array.Copy(data, 4, denominatorData, 0, 4);

            UInt32 numerator = ToUInt32(numeratorData);
            UInt32 denominator = ToUInt32(denominatorData);

            return new[] {numerator, denominator};
        }


        /// <summary>
        /// Converts 8 bytes to an unsigned rational using the current byte aligns
        /// </summary>
        /// <seealso cref="ToRational"/>
        private Double ToURational(Byte[] data)
        {
            UInt32[] fraction = ToURationalFraction(data);

            return fraction[0] / (Double) fraction[1];
        }

        /// <summary>
        /// Converts 8 bytes to the numerator and denominator
        /// components of an unsigned rational using the current byte aligns
        /// </summary>
        /// <remarks>
        /// A TIFF rational contains 2 4-byte integers, the first of which is
        /// the numerator, and the second of which is the denominator.
        /// </remarks>
        private Int32[] ToRationalFraction(Byte[] data)
        {
            Byte[] numeratorData = new Byte[4];
            Byte[] denominatorData = new Byte[4];

            Array.Copy(data, numeratorData, 4);
            Array.Copy(data, 4, denominatorData, 0, 4);

            Int32 numerator = ToInt32(numeratorData);
            Int32 denominator = ToInt32(denominatorData);

            return new[] {numerator, denominator};
        }

        /// <summary>
        /// Converts 8 bytes to a signed rational using the current byte aligns.
        /// </summary>
        /// <seealso cref="ToRationalFraction"/>
        private Double ToRational(Byte[] data)
        {
            Int32[] fraction = ToRationalFraction(data);

            return fraction[0] / (Double) fraction[1];
        }
        
        private static SByte ToSByte(Byte[] data)
        {
            // An sbyte should just be a byte with an offset range.
            return (SByte) (data[0] - Byte.MaxValue);
        }
        
        private Int16 ToInt16(Byte[] data)
        {
            if (_isLittleEndian != BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }

            return BitConverter.ToInt16(data, 0);
        }
        
        /// <summary>
        /// Converts 2 bytes to a ushort using the current byte aligns
        /// </summary>
        /// <returns></returns>
        private UInt16 ToUInt16(Byte[] data)
        {
            if (_isLittleEndian != BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }

            return BitConverter.ToUInt16(data, 0);
        }
        
        /// <summary>
        /// Converts 4 bytes to an int using the current byte aligns
        /// </summary>
        private Int32 ToInt32(Byte[] data)
        {
            if (_isLittleEndian != BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }

            return BitConverter.ToInt32(data, 0);
        }

        /// <summary>
        /// Converts 4 bytes to a uint using the current byte aligns
        /// </summary>
        private UInt32 ToUInt32(Byte[] data)
        {
            if (_isLittleEndian != BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }

            return BitConverter.ToUInt32(data, 0);
        }

        private Single ToSingle(Byte[] data)
        {
            if (_isLittleEndian != BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }

            return BitConverter.ToSingle(data, 0);
        }

        private Double ToDouble(Byte[] data)
        {
            if (_isLittleEndian != BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }

            return BitConverter.ToDouble(data, 0);
        }

        /// <summary>
        /// Retrieves an array from a byte array using the supplied converter
        /// to read each individual element from the supplied byte array
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        private static Array GetArray<T>(Byte[] data, Int32 length, ConverterMethod<T> converter)
        {
            Array convertedData = new T[data.Length / length];

            Byte[] buffer = new Byte[length];

            // Read each element from the array
            for (Int32 i = 0; i < data.Length / length; i++)
            {
                // Place the data for the current element into the buffer
                Array.Copy(data, i * length, buffer, 0, length);

                // Process the data and place it into the output array
                convertedData.SetValue(converter(buffer), i);
            }

            return convertedData;
        }

        /// <summary>
        /// A delegate used to invoke any of the data conversion methods
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <remarks>Although this could be defined as covariant, it wouldn't work on Windows Phone 7</remarks>
        private delegate T ConverterMethod<out T>(Byte[] data);

        /// <summary>
        /// Scans to the Exif block
        /// </summary>
        private void ReadToExifStart()
        {
            // The file has a number of blocks (Exif/JFIF), each of which
            // has a tag number followed by a length. We scan the document until the required tag (0xFFE1)
            // is found. All tags start with FF, so a non FF tag indicates an error.

            // Get the next tag.
            Byte markerStart;
            Byte markerNumber = 0;
            while ((markerStart = _reader.ReadByte()) == 0xFF && (markerNumber = _reader.ReadByte()) != 0xE1)
            {
                // Get the length of the data.
                UInt16 dataLength = ReadUInt16();

                // Jump to the end of the data (note that the size field includes its own size)!
                Int32 offset = dataLength - 2;
                Int64 expectedPosition = _stream.Position + offset;
                _stream.Seek(offset, SeekOrigin.Current);

                // It's unfortunate that we have to do this, but some streams report CanSeek but don't actually seek
                // (i.e. Microsoft.Phone.Tasks.DssPhotoStream), so we have to make sure the seek actually worked. The check is performed
                // here because this is the first time we perform a seek operation.
                if (_stream.Position != expectedPosition)
                {
                    throw new ExifException($"Supplied stream of type {_stream.GetType()} reports CanSeek=true, but fails to seek");
                }
            }

            // It's only success if we found the 0xFFE1 marker
            if (markerStart != 0xFF || markerNumber != 0xE1)
            {
                throw new ExifException("Could not find Exif data block");
            }
        }

        /// <summary>
        /// Reads through the Exif data and builds an index of all Exif tags in the document
        /// </summary>
        /// <returns></returns>
        private void CreateTagIndex()
        {
            // The next 4 bytes are the size of the Exif data.
            ReadUInt16();

            // Next is the Exif data itself. It starts with the ASCII "Exif" followed by 2 zero bytes.
            if (ReadString(4) != "Exif")
            {
                throw new ExifException("Exif data not found");
            }

            // 2 zero bytes
            if (ReadUInt16() != 0)
            {
                throw new ExifException("Malformed Exif data");
            }

            // We're now into the TIFF format
            _tiffHeaderStart = _stream.Position;

            // What byte align will be used for the TIFF part of the document? II for Intel, MM for Motorola
            _isLittleEndian = ReadString(2) == "II";

            // Next 2 bytes are always the same.
            if (ReadUInt16() != 0x002A)
            {
                throw new ExifException("Error in TIFF data");
            }

            // Get the offset to the IFD (image file directory)
            UInt32 ifdOffset = ReadUInt32();

            // Note that this offset is from the first byte of the TIFF header. Jump to the IFD.
            _stream.Position = ifdOffset + _tiffHeaderStart;

            // Catalogue this first IFD (there will be another IFD)
            _ifd0PrimaryCatalogue = CatalogueIFD();

            // The address to the IFD1 (the thumbnail IFD) is located immediately after the main IFD
            UInt32 ifd1Offset = ReadUInt32();

            // There's more data stored in the EXIF subifd, the offset to which is found in tag 0x8769.
            // As with all TIFF offsets, it will be relative to the first byte of the TIFF header.
            if (GetTagValue(_ifd0PrimaryCatalogue, 0x8769, out UInt32 offset))
            {
                // Jump to the exif SubIFD
                _stream.Position = offset + _tiffHeaderStart;

                // Add the subIFD to the catalogue too
                _ifdExifCatalogue = CatalogueIFD();
            }

            // Go to the GPS IFD and catalogue that too. It's an optional section.
            if (GetTagValue(_ifd0PrimaryCatalogue, 0x8825, out offset))
            {
                // Jump to the GPS SubIFD
                _stream.Position = offset + _tiffHeaderStart;

                // Add the subIFD to the catalogue too
                _ifdGPSCatalogue = CatalogueIFD();
            }

            // Finally, catalogue the thumbnail IFD if it's present
            if (ifd1Offset == 0)
            {
                return;
            }

            _stream.Position = ifd1Offset + _tiffHeaderStart;
            _ifd1Catalogue = CatalogueIFD();
        }

        public Boolean GetTagValue<T>(ExifTags tag, out T result)
        {
            return GetTagValue((UInt16) tag, out result);
        }

        public Boolean GetTagValue<T>(UInt16 tagID, out T result)
        {
            if (IfdLookup.TryGetValue(tagID, out IFD ifd))
            {
                return GetTagValue(tagID, ifd, out result);
            }

            // It's an unknown tag. Try all IFDs. Note that the thumbnail catalogue (IFD1)
            // is only used for thumbnails, never for tag retrieval
            return GetTagValue(_ifd0PrimaryCatalogue, tagID, out result) || GetTagValue(_ifdExifCatalogue, tagID, out result) || GetTagValue(_ifdGPSCatalogue, tagID, out result);
        }

        /// <summary>
        ///  Retrieves a numbered tag from a specific IFD
        /// </summary>
        /// <remarks>Useful for cases where a new or non-standard tag isn't present in the <see cref="ExifTags"/> enumeration</remarks>
        public Boolean GetTagValue<T>(UInt16 tagID, IFD ifd, out T result)
        {
            return GetTagValue(ifd switch
            {
                IFD.IFD0 => _ifd0PrimaryCatalogue,
                IFD.EXIF => _ifdExifCatalogue,
                IFD.GPS => _ifdGPSCatalogue,
                _ => throw new ArgumentOutOfRangeException()
            }, tagID, out result);
        }

        /// <summary>
        /// Retrieves an Exif value with the requested tag ID
        /// </summary>
        private Boolean GetTagValue<T>(Dictionary<UInt16, Int64> tags, UInt16 tagID, out T result)
        {
            Byte[] tag = GetTagBytes(tags, tagID, out UInt16 type, out UInt32 components);

            if (tag is null)
            {
                result = default;
                return false;
            }

            Byte length = GetTiffFieldLength(type);

            if (length == 0)
            {
                // Some fields have no data at all. Treat them as though they're absent, as they're bogus
                result = default;
                return false;
            }

            // Convert the data to the appropriate datatype. Note the weird boxing via object.
            // The compiler doesn't like it otherwise.
            switch (type)
            {
                case 1:
                    // unsigned byte
                    if (components == 1)
                    {
                        result = (T) (Object) tag[0];
                    }
                    else
                    {
                        // If a string is requested from a byte array, it will be unicode encoded.
                        if (typeof(T) == typeof(String))
                        {
                            String decoded = Encoding.Unicode.GetString(tag, 0, tag.Length);
                            // Unicode strings are null-terminated
                            result = (T) (Object) decoded.TrimEnd('\0');
                        }
                        else
                        {
                            result = (T) (Object) tag;
                        }
                    }

                    return true;
                case 2:
                    // ascii string
                    String str = Encoding.UTF8.GetString(tag, 0, tag.Length);

                    // There may be a null character within the string
                    Int32 inullchar = str.IndexOf('\0');
                    if (inullchar != -1)
                    {
                        str = str.Substring(0, inullchar);
                    }

                    // Special processing for dates.
                    if (typeof(T) == typeof(DateTime))
                    {
                        Boolean success = ToDateTime(str, out DateTime dt);

                        result = (T) (Object) dt;
                        return success;
                    }

                    result = (T) (Object) str;
                    return true;
                case 3:
                    // unsigned short
                    if (components == 1)
                    {
                        result = (T) (Object) ToUInt16(tag);
                    }
                    else
                    {
                        result = (T) (Object) GetArray(tag, length, ToUInt16);
                    }

                    return true;
                case 4:
                    // unsigned long
                    if (components == 1)
                    {
                        result = (T) (Object) ToUInt32(tag);
                    }
                    else
                    {
                        result = (T) (Object) GetArray(tag, length, ToUInt32);
                    }

                    return true;
                case 5:
                    // unsigned rational
                    if (components == 1)
                    {
                        // Special case - sometimes it's useful to retrieve the numerator and
                        // denominator in their raw format
                        if (typeof(T).IsArray)
                        {
                            result = (T) (Object) ToURationalFraction(tag);
                        }
                        else
                        {
                            result = (T) (Object) ToURational(tag);
                        }
                    }
                    else
                    {
                        result = (T) (Object) GetArray(tag, length, ToURational);
                    }

                    return true;
                case 6:
                    // signed byte
                    if (components == 1)
                    {
                        result = (T) (Object) ToSByte(tag);
                    }
                    else
                    {
                        result = (T) (Object) GetArray(tag, length, ToSByte);
                    }

                    return true;
                case 7:
                    // undefined. Treat it as a byte.
                    if (components == 1)
                    {
                        result = (T) (Object) tag[0];
                    }
                    else
                    {
                        result = (T) (Object) tag;
                    }

                    return true;
                case 8:
                    // Signed short
                    if (components == 1)
                    {
                        result = (T) (Object) ToInt16(tag);
                    }
                    else
                    {
                        result = (T) (Object) GetArray(tag, length, ToInt16);
                    }

                    return true;
                case 9:
                    // Signed long
                    if (components == 1)
                    {
                        result = (T) (Object) ToInt32(tag);
                    }
                    else
                    {
                        result = (T) (Object) GetArray(tag, length, ToInt32);
                    }

                    return true;
                case 10:
                    // signed rational
                    if (components == 1)
                    {
                        // Special case - sometimes it's useful to retrieve the numerator and
                        // denominator in their raw format
                        if (typeof(T).IsArray)
                        {
                            result = (T) (Object) ToRationalFraction(tag);
                        }
                        else
                        {
                            result = (T) (Object) ToRational(tag);
                        }
                    }
                    else
                    {
                        result = (T) (Object) GetArray(tag, length, ToRational);
                    }

                    return true;
                case 11:
                    // single float
                    if (components == 1)
                    {
                        result = (T) (Object) ToSingle(tag);
                    }
                    else
                    {
                        result = (T) (Object) GetArray(tag, length, ToSingle);
                    }

                    return true;
                case 12:
                    // double float
                    if (components == 1)
                    {
                        result = (T) (Object) ToDouble(tag);
                    }
                    else
                    {
                        result = (T) (Object) GetArray(tag, length, ToDouble);
                    }

                    return true;
                default:
                    throw new ExifException($"Unknown TIFF datatype: {type}");
            }
        }

        private static Boolean ToDateTime(String str, out DateTime result)
        {
            // From page 28 of the Exif 2.2 spec (http://www.exif.org/Exif2-2.PDF): 

            // "When the field is left blank, it is treated as unknown ... When the date and time are unknown, 
            // all the character spaces except colons (":") may be filled with blank characters"
            if (String.IsNullOrEmpty(str) || NullDateTimeMatcher.IsMatch(str))
            {
                result = DateTime.MinValue;
                return false;
            }

            // There are 2 types of date - full date/time stamps, and plain dates. Dates are 10 characters long.
            if (str.Length == 10)
            {
                result = DateTime.ParseExact(str, "yyyy:MM:dd", CultureInfo.InvariantCulture);
                return true;
            }

            // "The format is "YYYY:MM:DD HH:MM:SS" with time shown in 24-hour format, and the date and time separated by one blank character [20.H].
            result = DateTime.ParseExact(str, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture);
            return true;
        }

        /// <summary>
        /// Gets the data in the specified tag ID, starting from before the IFD block.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="components">The number of items which make up the data item - i.e. for a string, this will be the
        /// number of characters in the string</param>
        /// <param name="tags"></param>
        /// <param name="tagID"></param>
        private Byte[] GetTagBytes(IReadOnlyDictionary<UInt16, Int64> tags, UInt16 tagID, out UInt16 type, out UInt32 components)
        {
            // Get the tag's offset from the catalogue and do some basic error checks
            if (_stream is null || _reader is null || tags is null || !tags.ContainsKey(tagID))
            {
                type = 0;
                components = 0;
                return null;
            }

            Int64 offset = tags[tagID];

            // Jump to the TIFF offset
            _stream.Position = offset;

            // Read the tag number from the file
            UInt16 currentTagID = ReadUInt16();

            if (currentTagID != tagID)
            {
                throw new ExifException("Tag number not at expected offset");
            }

            // Read the offset to the Exif IFD
            type = ReadUInt16();
            components = ReadUInt32();
            Byte[] tag = ReadBytes(4);

            // If the total space taken up by the field is longer than the
            // 2 bytes afforded by the tagData, tagData will contain an offset
            // to the actual data.
            Int32 size = (Int32) (components * GetTiffFieldLength(type));

            if (size > 4)
            {
                UInt16 address = ToUInt16(tag);
                return ReadBytes(address, size);
            }

            // The value is stored in the tagData starting from the left
            Array.Resize(ref tag, size);

            return tag;
        }

        /// <summary>
        /// Reads the current IFD header and records all Exif tags and their offsets in a <see cref="Dictionary{TKey,TValue}"/>
        /// </summary>
        private Dictionary<UInt16, Int64> CatalogueIFD()
        {
            Dictionary<UInt16, Int64> offsets = new Dictionary<UInt16, Int64>();

            // Assume we're just before the IFD.

            // First 2 bytes is the number of entries in this IFD
            UInt16 count = ReadUInt16();

            for (UInt16 i = 0; i < count; i++)
            {
                UInt16 current = ReadUInt16();

                // Record this in the catalogue
                offsets[current] = _stream.Position - 2;

                // Go to the end of this item (10 bytes, as each entry is 12 bytes long)
                _stream.Seek(10, SeekOrigin.Current);
            }

            return offsets;
        }
        
        /// <summary>
        /// Retrieves a JPEG thumbnail from the image if one is present. Note that this method cannot retrieve thumbnails encoded in other formats,
        /// but since the DCF specification specifies that thumbnails must be JPEG, this method will be sufficient for most purposes
        /// See http://gvsoft.homedns.org/exif/exif-explanation.html#TIFFThumbs or http://partners.adobe.com/public/developer/en/tiff/TIFF6.pdf for 
        /// details on the encoding of TIFF thumbnails
        /// </summary>
        /// <returns></returns>
        public Byte[] GetJpegThumbnailBytes()
        {
            if (_ifd1Catalogue is null)
            {
                return null;
            }

            // Get the thumbnail encoding
            if (!GetTagValue(_ifd1Catalogue, (UInt16) ExifTags.Compression, out UInt16 compression))
            {
                return null;
            }

            // This method only handles JPEG thumbnails (compression type 6)
            if (compression != 6)
            {
                return null;
            }

            // Get the location of the thumbnail
            if (!GetTagValue(_ifd1Catalogue, (UInt16) ExifTags.JPEGInterchangeFormat, out UInt32 offset))
            {
                return null;
            }

            // Get the length of the thumbnail data
            if (!GetTagValue(_ifd1Catalogue, (UInt16) ExifTags.JPEGInterchangeFormatLength, out UInt32 length))
            {
                return null;
            }

            _stream.Position = offset;

            // The thumbnail may be padded, so we scan forward until we reach the JPEG header (0xFFD8) or the end of the file
            Int32 current;
            Int32 previous = -1;
            while ((current = _stream.ReadByte()) != -1)
            {
                if (previous == 0xFF && current == 0xD8)
                {
                    break;
                }

                previous = current;
            }

            if (current != 0xD8)
            {
                return null;
            }

            // Step back to the start of the JPEG header
            _stream.Position -= 2;

            Byte[] image = new Byte[length];
            _stream.Read(image, 0, (Int32) length);

            // A valid JPEG stream ends with 0xFFD9. The stream may be padded at the end with multiple 0xFF or 0x00 bytes.
            Int32 endofjpeg = (Int32) length - 1;
            for (; endofjpeg > 0; endofjpeg--)
            {
                Byte last = image[endofjpeg];
                if (last != 0xFF && last != 0x00)
                {
                    break;
                }
            }

            if (endofjpeg <= 0 || image[endofjpeg] != 0xD9 || image[endofjpeg - 1] != 0xFF)
            {
                return null;
            }

            return image;
        }

        public void Dispose()
        {
            // Make sure the stream is released if appropriate. Note the different options for Windows Store apps.
            _reader?.Close();

            if (_stream is not null && !_leaveOpen)
            {
                _stream.Dispose();
            }
        }
    }

    [Serializable]
    public class ExifException : Exception
    {
        public ExifException()
        {
        }

        public ExifException(String message)
            : base(message)
        {
        }

        public ExifException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}