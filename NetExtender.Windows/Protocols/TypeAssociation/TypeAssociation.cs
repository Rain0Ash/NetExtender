// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using NetExtender.Registry;
using NetExtender.Registry.Interfaces;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Registry;
using NetExtender.Utilities.Types;
using NetExtender.Workstation;

namespace NetExtender.Windows.Protocols
{
    /// <summary>
    /// https://github.com/DanysysTeam/PS-SFTA
    /// </summary>
    public partial class TypeAssociationProtocol
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetFileTypeAssociationProgId(String extension)
        {
            return GetFileTypeAssociation(extension).Key;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetFileTypeAssociationHash(String extension)
        {
            return GetFileTypeAssociation(extension).Value;
        }
        
        public static KeyValuePair<String?, String?> GetFileTypeAssociation(String extension)
        {
            if (String.IsNullOrEmpty(extension))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(extension));
            }

            using IRegistry registry = RegistryKeys.CurrentUser.Create(FileAccess.Read, "Software", "Microsoft", "Windows", "CurrentVersion", "Explorer", "FileExts");

            String? progid = registry.GetValue("ProgId", extension, "UserChoice");
            String? hash = registry.GetValue("Hash", extension, "UserChoice");

            return new KeyValuePair<String?, String?>(progid, hash);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<String?, String?>> GetFileTypeAssociationProgId()
        {
            return GetFileTypeAssociation().Select(entry => new KeyValuePair<String?, String?>(entry.Key, entry.Value.Key));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<String?, String?>> GetFileTypeAssociationHash()
        {
            return GetFileTypeAssociation().Select(entry => new KeyValuePair<String?, String?>(entry.Key, entry.Value.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<String, KeyValuePair<String?, String?>>> GetFileTypeAssociationUserChoice()
        {
            return GetFileTypeAssociation().Where(entry => entry.Value.Key is not null || entry.Value.Value is not null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<String?, String?>> GetFileTypeAssociationProgIdUserChoice()
        {
            return GetFileTypeAssociationUserChoice().Select(KeyValuePairUtilities.FlattenByKey!)!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<String?, String?>> GetFileTypeAssociationHashUserChoice()
        {
            return GetFileTypeAssociationUserChoice().Select(KeyValuePairUtilities.FlattenByValue!)!;
        }

        public static IEnumerable<KeyValuePair<String, KeyValuePair<String?, String?>>> GetFileTypeAssociation()
        {
            using IRegistry registry = RegistryKeys.CurrentUser.Create(FileAccess.Read, "Software", "Microsoft", "Windows", "CurrentVersion", "Explorer", "FileExts");

            String[]? subkeys = registry.GetSubKeyNames();

            if (subkeys is null)
            {
                yield break;
            }

            foreach (String? entry in subkeys)
            {
                String? progid = registry.GetValue("ProgId", entry, "UserChoice");
                String? hash = registry.GetValue("Hash", entry, "UserChoice");

                yield return new KeyValuePair<String, KeyValuePair<String?, String?>>(entry, new KeyValuePair<String?, String?>(progid, hash));
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetProtocolTypeAssociationProgId(String protocol)
        {
            return GetProtocolTypeAssociation(protocol).Key;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetProtocolTypeAssociationHash(String protocol)
        {
            return GetProtocolTypeAssociation(protocol).Value;
        }

        public static KeyValuePair<String?, String?> GetProtocolTypeAssociation(String protocol)
        {
            if (String.IsNullOrEmpty(protocol))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(protocol));
            }

            using IRegistry registry = RegistryKeys.CurrentUser.Create(FileAccess.Read, "Software", "Microsoft", "Windows", "Shell", "Associations", "UrlAssociations");

            String? progid = registry.GetValue("ProgId", protocol, "UserChoice");
            String? hash = registry.GetValue("Hash", protocol, "UserChoice");

            return new KeyValuePair<String?, String?>(progid, hash);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<String?, String?>> GetProtocolTypeAssociationProgId()
        {
            return GetProtocolTypeAssociation().Select(entry => new KeyValuePair<String?, String?>(entry.Key, entry.Value.Key));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<String?, String?>> GetProtocolTypeAssociationHash()
        {
            return GetProtocolTypeAssociation().Select(entry => new KeyValuePair<String?, String?>(entry.Key, entry.Value.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<String, KeyValuePair<String?, String?>>> GetProtocolTypeAssociationUserChoice()
        {
            return GetProtocolTypeAssociation().Where(entry => entry.Value.Key is not null || entry.Value.Value is not null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<String, String?>> GetProtocolTypeAssociationProgIdUserChoice()
        {
            return GetProtocolTypeAssociationUserChoice().Select(KeyValuePairUtilities.FlattenByInnerKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<String, String?>> GetProtocolTypeAssociationHashUserChoice()
        {
            return GetProtocolTypeAssociationUserChoice().Select(KeyValuePairUtilities.FlattenByInnerValue);
        }

        public static IEnumerable<KeyValuePair<String, KeyValuePair<String?, String?>>> GetProtocolTypeAssociation()
        {
            using IRegistry registry = RegistryKeys.CurrentUser.Create(FileAccess.Read, "Software", "Microsoft", "Windows", "Shell", "Associations", "UrlAssociations");

            String[]? subkeys = registry.GetSubKeyNames();

            if (subkeys is null)
            {
                yield break;
            }

            foreach (String entry in subkeys)
            {
                String? progid = registry.GetValue("ProgId", entry, "UserChoice");
                String? hash = registry.GetValue("Hash", entry, "UserChoice");

                yield return new KeyValuePair<String, KeyValuePair<String?, String?>>(entry, new KeyValuePair<String?, String?>(progid, hash));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean RegisterFileTypeAssociation(String path, String extension)
        {
            return RegisterFileTypeAssociation(path, extension, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean RegisterFileTypeAssociation(String path, String extension, String? icon)
        {
            return RegisterFileTypeAssociation(path, extension, null, icon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean RegisterFileTypeAssociation(String path, String extension, String? progid, String? icon)
        {
            return RegisterFileTypeAssociation(path, extension, progid, icon, WorkStation.CurrentUserSID);
        }

        public static Boolean RegisterFileTypeAssociation(String path, String extension, String? progid, String? icon, String? sid)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            }

            if (String.IsNullOrEmpty(extension))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(extension));
            }

            if (!PathUtilities.IsExistAsFile(path))
            {
                throw new FileNotFoundException(null, path);
            }

            if (String.IsNullOrEmpty(progid))
            {
                progid = $"SFTA.{PathUtilities.ChangeExtension(PathUtilities.GetFileNameWithoutExtension(path).RemoveAllWhiteSpace(), extension)}";
            }

            using IRegistry registry = RegistryKeys.CurrentUser.Create(FileAccess.ReadWrite, "Software", "Classes");

            try
            {
                registry.SetValue("ProgId", Array.Empty<Byte>(), RegistryValueKind.None, extension, "OpenWithProgids");
                registry.SetValue(null, $"\"{path}\" \"%1\"", progid, "shell", "open", "command");
            }
            catch (Exception)
            {
                return false;
            }

            return SetFileTypeAssociation(progid, extension, icon, sid);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean RegisterProtocolTypeAssociation(String path, String protocol)
        {
            return RegisterProtocolTypeAssociation(path, protocol, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean RegisterProtocolTypeAssociation(String path, String protocol, String? icon)
        {
            return RegisterProtocolTypeAssociation(path, protocol, null, icon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean RegisterProtocolTypeAssociation(String path, String protocol, String? progid, String? icon)
        {
            return RegisterFileTypeAssociation(path, protocol, progid, icon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean RegisterProtocolTypeAssociation(String path, String protocol, String? progid, String? icon, String? sid)
        {
            return RegisterFileTypeAssociation(path, protocol, progid, icon, sid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetFileTypeAssociation(String progid, String extension, String? icon)
        {
            return SetFileTypeAssociation(progid, extension, icon, WorkStation.CurrentUserSID);
        }

        public static Boolean SetFileTypeAssociation(String progid, String extension, String? icon, String? sid)
        {
            if (String.IsNullOrEmpty(progid))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(progid));
            }

            if (String.IsNullOrEmpty(extension))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(extension));
            }

            if (PathUtilities.IsExistAsFile(progid))
            {
                progid = $"SFTA.{PathUtilities.ChangeExtension(PathUtilities.GetFileNameWithoutExtension(progid).RemoveAllWhiteSpace(), extension)}";
            }

            String? experience = GetUserExperience();
            String datetime = GetHexDateTime();

            String info = $"{extension}{sid}{progid}{datetime}{experience}".ToLower();
            String hash = GetHash(info);

            if (!(extension.StartsWith(".") ? WriteExtensionKeys(progid, extension, hash) : WriteProtocolKeys(progid, extension, hash)))
            {
                return false;
            }

            if (!String.IsNullOrEmpty(icon))
            {
                using IRegistry registry = RegistryKeys.CurrentUser.Create(FileAccess.ReadWrite, "Software", "Classes");
                registry.SetValue(null, icon, progid, "DefaultIcon");
            }

            Refresh();
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetProtocolTypeAssociation(String progid, String protocol, String? icon)
        {
            return SetFileTypeAssociation(progid, protocol, icon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetProtocolTypeAssociation(String progid, String protocol, String? icon, String? sid)
        {
            return SetFileTypeAssociation(progid, protocol, icon, sid);
        }

        public static Boolean RemoveFileTypeAssociation(String progid, String extension)
        {
            if (String.IsNullOrEmpty(progid))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(progid));
            }

            if (String.IsNullOrEmpty(extension))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(extension));
            }

            if (PathUtilities.IsExistAsFile(progid))
            {
                progid = $"SFTA.{PathUtilities.ChangeExtension(PathUtilities.GetFileNameWithoutExtension(progid).RemoveAllWhiteSpace(), extension)}";
            }

            using (IRegistry registry = RegistryKeys.CurrentUser.Create(true, "Software", "Microsoft", "Windows", "CurrentVersion", "Explorer", "FileExts", extension))
            {
                registry.RemoveSubKeyTree("UserChoice");
            }

            using (IRegistry registry = RegistryKeys.CurrentUser.Create(true, "Software", "Classes"))
            {
                registry.RemoveSubKeyTree(progid);
                registry.RemoveSubKeyTree("OpenWithProgids", extension);
            }

            Refresh();
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean RemoveProtocolTypeAssociation(String progid, String protocol)
        {
            return RemoveFileTypeAssociation(progid, protocol);
        }

        private static Boolean WriteExtensionKeys(String progid, String extension, String hash)
        {
            if (String.IsNullOrEmpty(progid))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(progid));
            }

            if (String.IsNullOrEmpty(extension))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(extension));
            }

            if (String.IsNullOrEmpty(hash))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(hash));
            }

            using IRegistry registry = RegistryKeys.CurrentUser.Create(true, "Software", "Microsoft", "Windows", "CurrentVersion", "Explorer", "FileExts", extension, "UserChoice");
            registry.RemoveSubKeyTree();
            registry.SetValue("Hash", hash);
            return registry.SetValue("ProgId", progid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean WriteProtocolKeys(String progid, String protocol, String hash)
        {
            if (String.IsNullOrEmpty(progid))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(progid));
            }

            if (String.IsNullOrEmpty(protocol))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(protocol));
            }

            if (String.IsNullOrEmpty(hash))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(hash));
            }

            using IRegistry registry = RegistryKeys.CurrentUser.Create(true, "Software", "Microsoft", "Windows", "Shell", "Associations", "UrlAssociations", protocol, "UserChoice");
            registry.RemoveSubKeyTree();
            registry.SetValue("Hash", hash);
            return registry.SetValue("ProgId", progid);
        }

        private static String? UserExperience { get; set; }

        private static String? GetUserExperience()
        {
            return GetUserExperienceAsync().GetAwaiter().GetResult();
        }

        private static async Task<String?> GetUserExperienceAsync()
        {
            if (!String.IsNullOrEmpty(UserExperience))
            {
                return UserExperience;
            }

            String path = System.IO.Path.Combine(Environment.SpecialFolder.SystemX86.GetPath(), "shell32.dll");
            await using FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            static (Int32 Index, String Value) Index(String value)
            {
                return (value.IndexOf("User Choice set via Windows User Experience", StringComparison.Ordinal), value);
            }

            static (Int32 Start, Int32 End, String Value) Find((Int32 Index, String Value) value)
            {
                return (Start: value.Index, End: value.Value.IndexOf("}", value.Index, StringComparison.Ordinal), value.Value);
            }

            static String Substring((Int32 Start, Int32 End, String Value) value)
            {
                return value.Value.Substring(value.Start, value.End - value.Start + 1);
            }

            return UserExperience ??= stream.ReadAsSequential(Encoding.Unicode)
                .Select(Index)
                .SelectWhere(item => item.Index > -1, Find)
                .SelectWhere(item => item.End > -1, Substring)
                .FirstOrDefault();
        }

        private static String GetHexDateTime()
        {
            unchecked
            {
                DateTime now = DateTime.Now;
                DateTime datetime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
                UInt64 filetime = (UInt64) datetime.ToFileTime();

                return filetime.ToString("x8").ToLower();
            }
        }

        private static Int64 GetShiftRight(Int64 value, Int32 count)
        {
            if ((value & 0x80000000) == 0)
            {
                return value >> count;
            }

            return (value >> count) ^ 0xFFFF0000;
        }

        private static Int64 GetLong(Byte[] bytes, Int32 index = 0)
        {
            return BitConverter.ToInt32(bytes, index);
        }

        private static Int32 ConvertInt32(Int64 value)
        {
            Byte[] bytes = BitConverter.GetBytes(value);
            return BitConverter.ToInt32(bytes);
        }

        private static String GetHash(String info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            unchecked
            {
                Byte[] infobytes = Encoding.Unicode.GetBytes(info);
                Array.Resize(ref infobytes, infobytes.Length + 2);

                MD5 provider = MD5.Create();
                Byte[] md5bytes = provider.ComputeHash(infobytes);

                Int32 infolength = info.Length * 2 + 2;
                Int64 length = ((infolength & 4) <= 1 ? 1 : 0) + GetShiftRight(infolength, 2) - 1;

                if (length <= 1)
                {
                    return String.Empty;
                }

                Int32 pdata = 0;
                Int64 cache = 0;
                Int64 md5_1 = (GetLong(md5bytes) | 1) + 0x69FB0000L;
                Int64 md5_2 = (GetLong(md5bytes, 4) | 1) + 0x13DB0000L;
                Int64 counter = GetShiftRight(length - 2, 1) + 1;
                Int32 outhash1 = 0;
                Int32 outhash2 = 0;

                while (counter-- > 0)
                {
                    Int32 r0 = ConvertInt32(GetLong(infobytes, pdata) + outhash1);
                    Int32 r1_1 = ConvertInt32(GetLong(infobytes, pdata + 4));
                    pdata += 8;
                    Int32 r2_1 = ConvertInt32(r0 * md5_1 - 0x10FA9605L * GetShiftRight(r0, 16));
                    Int32 r2_2 = ConvertInt32(0x79F8A395L * r2_1 + 0x689B6B9FL * GetShiftRight(r2_1, 16));
                    Int32 r3 = ConvertInt32(0xEA970001L * r2_2 - 0x3C101569L * GetShiftRight(r2_2, 16));
                    Int32 r4_1 = ConvertInt32(r3 + r1_1);
                    Int32 r5_1 = ConvertInt32(cache + r3);
                    Int32 r6_1 = ConvertInt32(r4_1 * md5_2 - 0x3CE8EC25L * GetShiftRight(r4_1, 16));
                    Int32 r6_2 = ConvertInt32(0x59C3AF2DL * r6_1 - 0x2232E0F1L * GetShiftRight(r6_1, 16));
                    outhash1 = ConvertInt32(0x1EC90001L * r6_2 + 0x35BD1EC9L * GetShiftRight(r6_2, 16));
                    outhash2 = ConvertInt32(r5_1 + outhash1);
                    cache = outhash2;
                }

                Byte[] outhash = new Byte[16];
                Byte[] buffer = BitConverter.GetBytes(outhash1);
                buffer.CopyTo(outhash, 0);
                buffer = BitConverter.GetBytes(outhash2);
                buffer.CopyTo(outhash, 4);

                pdata = 0;
                cache = 0;
                md5_1 = GetLong(md5bytes) | 1;
                md5_2 = GetLong(md5bytes, 4) | 1;
                counter = GetShiftRight(length - 2, 1) + 1;
                outhash1 = 0;
                outhash2 = 0;

                while (counter-- > 0)
                {
                    Int32 r0 = ConvertInt32(GetLong(infobytes, pdata) + outhash1);
                    pdata += 8;
                    Int32 r1_1 = ConvertInt32(r0 * md5_1);
                    Int32 r1_2 = ConvertInt32(0xB1110000L * r1_1 - 0x30674EEFL * GetShiftRight(r1_1, 16));
                    Int32 r2_1 = ConvertInt32(0x5B9F0000L * r1_2 - 0x78F7A461L * GetShiftRight(r1_2, 16));
                    Int32 r2_2 = ConvertInt32(0x12CEB96DL * GetShiftRight(r2_1, 16) - 0x46930000L * r2_1);
                    Int32 r3 = ConvertInt32(0x1D830000L * r2_2 + 0x257E1D83L * GetShiftRight(r2_2, 16));
                    Int32 r4_1 = ConvertInt32(md5_2 * (r3 + GetLong(infobytes, pdata - 4)));
                    Int32 r4_2 = ConvertInt32(0x16F50000L * r4_1 - 0x5D8BE90BL * GetShiftRight(r4_1, 16));
                    Int32 r5_1 = ConvertInt32(0x96FF0000L * r4_2 - 0x2C7C6901L * GetShiftRight(r4_2, 16));
                    Int32 r5_2 = ConvertInt32(0x2B890000L * r5_1 + 0x7C932B89L * GetShiftRight(r5_1, 16));
                    outhash1 = ConvertInt32(0x9F690000L * r5_2 - 0x405B6097L * GetShiftRight(r5_2, 16));
                    outhash2 = ConvertInt32(outhash1 + cache + r3);
                    cache = outhash2;
                }

                buffer = BitConverter.GetBytes(outhash1);
                buffer.CopyTo(outhash, 8);
                buffer = BitConverter.GetBytes(outhash2);
                buffer.CopyTo(outhash, 12);

                Byte[] outhashbase = new Byte[8];
                buffer = BitConverter.GetBytes((Int32) (GetLong(outhash, 8) ^ GetLong(outhash)));
                buffer.CopyTo(outhashbase, 0);
                buffer = BitConverter.GetBytes((Int32) (GetLong(outhash, 12) ^ GetLong(outhash, 4)));
                buffer.CopyTo(outhashbase, 4);

                return Convert.ToBase64String(outhashbase);
            }
        }
        
        [DllImport("shell32.dll")]
        private static extern void SHChangeNotify(Int32 eventId, Int32 flags, IntPtr item1, IntPtr item2);

        private static void Refresh()
        {
            SHChangeNotify(0x8000000, 0, IntPtr.Zero, IntPtr.Zero);
        }
    }
}