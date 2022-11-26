// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using NetExtender.IO.FileSystem.Lock.Content;

namespace NetExtender.IO.FileSystem.Lock.Common
{
    public static class LockHelper
    {
        private static DataContractJsonSerializer JsonSerializer { get; }

        static LockHelper()
        {
            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings
            {
                KnownTypes = new[] {typeof(FileLockContent)},
                IgnoreExtensionDataObject = true,
                EmitTypeInformation = EmitTypeInformation.Always,
                MaxItemsInObjectGraph = Int32.MaxValue
            };

            JsonSerializer = new DataContractJsonSerializer(typeof(FileLockContent), settings);
        }

        public static String GetFilePath(String name)
        {
            return Path.Combine(Path.GetTempPath(), name);
        }

        public static Boolean LockExists(String path)
        {
            return File.Exists(path);
        }

        public static FileLockContent ReadLock(String path)
        {
            try
            {
                using FileStream stream = File.OpenRead(path);

                return JsonSerializer.ReadObject(stream) as FileLockContent ?? new MissingFileLockContent();
            }
            catch (FileNotFoundException)
            {
                return new MissingFileLockContent();
            }
            catch (IOException)
            {
                return new OtherProcessOwnsFileLockContent();
            }
            catch (Exception)
            {
                return new MissingFileLockContent();
            }
        }

        public static Boolean WriteLock(String path, FileLockContent content)
        {
            try
            {
                using FileStream stream = File.Create(path);

                JsonSerializer.WriteObject(stream, content);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void DeleteLock(String path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}