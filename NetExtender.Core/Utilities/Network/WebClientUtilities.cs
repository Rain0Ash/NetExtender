// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Static;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Network
{
    public readonly struct WebDownloadProgress
    {
        public String Address { get; }
        public Int64 Current { get; }
        public Int64 Bytes { get; }

        public Double Percent
        {
            get
            {
                return (Double) Current / Bytes * 100D;
            }
        }

        public WebDownloadProgress(String address, Int64 current, Int64 bytes)
        {
            Address = address;
            Current = current;
            Bytes = bytes != 0 ? bytes : 1;
        }
    }
    
    public static class WebClientUtilities
    {
        private static async Task<T?> DownloadTaskAsync<T>(WebClient client, Func<WebClient, String, Task<T?>> handler, String address, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            token.ThrowIfCancellationRequested();

            await using (token.Register(client.CancelAsync))
            {
                return await handler(client, address).ConfigureAwait(false);
            }
        }

        private static Task<T?> DownloadTaskAsync<T>(WebClient client, Func<WebClient, String, Task<T?>> handler, String address,
            Byte tries, TimeSpan timeout, Action<Byte>? callback, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            Task<T?> DownloadTask(CancellationToken cancel)
            {
                return DownloadTaskAsync(client, handler, address, cancel);
            }
            
            return TaskUtilities.TimeoutRetryTaskAsync(DownloadTask, tries, timeout, callback, token);
        }

        private static Task<String> DownloadStringHandlerAsync(WebClient client, String address)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return client.DownloadStringTaskAsync(address);
        }

        public static Task<String> DownloadStringTaskAsync(this WebClient client, String address)
        {
            return DownloadStringTaskAsync(client, address, CancellationToken.None);
        }

        public static Task<String> DownloadStringTaskAsync(this WebClient client, String address, CancellationToken token)
        {
            return DownloadTaskAsync(client, DownloadStringHandlerAsync!, address, token)!;
        }

        public static Task<String> DownloadStringTaskAsync(this WebClient client, String address, Byte tries)
        {
            return DownloadStringTaskAsync(client, address, tries, CancellationToken.None);
        }

        public static Task<String> DownloadStringTaskAsync(this WebClient client, String address, Byte tries, CancellationToken token)
        {
            return DownloadStringTaskAsync(client, address, tries, Time.Minute.OneHalf, token);
        }

        public static Task<String> DownloadStringTaskAsync(this WebClient client, String address, Byte tries, TimeSpan timeout)
        {
            return DownloadStringTaskAsync(client, address, tries, timeout, CancellationToken.None);
        }

        public static Task<String> DownloadStringTaskAsync(this WebClient client, String address, Byte tries, TimeSpan timeout, CancellationToken token)
        {
            return DownloadStringTaskAsync(client, address, tries, timeout, null, token);
        }

        public static Task<String> DownloadStringTaskAsync(this WebClient client, String address, Byte tries, TimeSpan timeout, Action<Byte>? callback)
        {
            return DownloadStringTaskAsync(client, address, tries, timeout, callback, CancellationToken.None);
        }

        public static Task<String> DownloadStringTaskAsync(this WebClient client, String address, Byte tries, TimeSpan timeout, Action<Byte>? callback, CancellationToken token)
        {
            return DownloadTaskAsync(client, DownloadStringHandlerAsync!, address, tries, timeout, callback, token)!;
        }

        private static Task<Byte[]> DownloadDataHandlerAsync(WebClient client, String address)
        {
            return client.DownloadDataTaskAsync(address);
        }

        public static Task<Byte[]> DownloadDataTaskAsync(this WebClient client, String address, CancellationToken token)
        {
            return DownloadTaskAsync(client, DownloadDataHandlerAsync!, address, token)!;
        }

        public static Task<Byte[]> DownloadDataTaskAsync(this WebClient client, String address, Byte tries)
        {
            return DownloadDataTaskAsync(client, address, tries, CancellationToken.None);
        }

        public static Task<Byte[]> DownloadDataTaskAsync(this WebClient client, String address, Byte tries, CancellationToken token)
        {
            return DownloadDataTaskAsync(client, address, tries, Time.Minute.One, token);
        }

        public static Task<Byte[]> DownloadDataTaskAsync(this WebClient client, String address, Byte tries, TimeSpan timeout)
        {
            return DownloadDataTaskAsync(client, address, tries, timeout, CancellationToken.None);
        }

        public static Task<Byte[]> DownloadDataTaskAsync(this WebClient client, String address, Byte tries, TimeSpan timeout, CancellationToken token)
        {
            return DownloadDataTaskAsync(client, address, tries, timeout, null, token);
        }

        public static Task<Byte[]> DownloadDataTaskAsync(this WebClient client, String address, Byte tries, TimeSpan timeout, Action<Byte>? callback)
        {
            return DownloadDataTaskAsync(client, address, tries, timeout, callback, CancellationToken.None);
        }

        public static Task<Byte[]> DownloadDataTaskAsync(this WebClient client, String address, Byte tries, TimeSpan timeout, Action<Byte>? callback, CancellationToken token)
        {
            return DownloadTaskAsync(client, DownloadDataHandlerAsync!, address, tries, timeout, callback, token)!;
        }

        private static async Task<FileInfo?> DownloadFileHandlerAsync(this WebClient client, String address, String path, Boolean overwrite)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!PathUtilities.IsValidFilePath(path))
            {
                throw new ArgumentException(@"Is not valid file path", nameof(path));
            }

            if (!overwrite && PathUtilities.IsExistAsFile(path))
            {
                throw new IOException();
            }
            
            await client.DownloadFileTaskAsync(address, path).ConfigureAwait(false);
            try
            {
                return new FileInfo(path);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Task<FileInfo?> DownloadFileTaskAsync(this WebClient client, String address, String path, Boolean overwrite = false)
        {
            return DownloadFileTaskAsync(client, address, path, overwrite, CancellationToken.None);
        }

        public static Task<FileInfo?> DownloadFileTaskAsync(this WebClient client, String address, String path, CancellationToken token)
        {
            return DownloadFileTaskAsync(client, address, path, false, token);
        }

        public static Task<FileInfo?> DownloadFileTaskAsync(this WebClient client, String address, String path, Boolean overwrite, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            Task<FileInfo?> DownloadWebFileTaskAsync(WebClient web, String url)
            {
                return DownloadFileHandlerAsync(web, url, path, overwrite);
            }

            return DownloadTaskAsync(client, DownloadWebFileTaskAsync, address, token);
        }

        public static Task<FileInfo?> DownloadFileTaskAsync(this WebClient client, String address, String path, Byte tries)
        {
            return DownloadFileTaskAsync(client, address, path, false, tries);
        }

        public static Task<FileInfo?> DownloadFileTaskAsync(this WebClient client, String address, String path, Boolean overwrite, Byte tries)
        {
            return DownloadFileTaskAsync(client, address, path, overwrite, tries, CancellationToken.None);
        }

        public static Task<FileInfo?> DownloadFileTaskAsync(this WebClient client, String address, String path, Byte tries, CancellationToken token)
        {
            return DownloadFileTaskAsync(client, address, path, false, tries, token);
        }

        public static Task<FileInfo?> DownloadFileTaskAsync(this WebClient client, String address, String path, Boolean overwrite, Byte tries, CancellationToken token)
        {
            return DownloadFileTaskAsync(client, address, path, overwrite, tries, Time.Minute.One, token);
        }

        public static Task<FileInfo?> DownloadFileTaskAsync(this WebClient client, String address, String path, Byte tries, TimeSpan timeout)
        {
            return DownloadFileTaskAsync(client, address, path, tries, timeout, CancellationToken.None);
        }

        public static Task<FileInfo?> DownloadFileTaskAsync(this WebClient client, String address, String path, Boolean overwrite, Byte tries, TimeSpan timeout)
        {
            return DownloadFileTaskAsync(client, address, path, overwrite, tries, timeout, CancellationToken.None);
        }

        public static Task<FileInfo?> DownloadFileTaskAsync(this WebClient client, String address, String path, Byte tries, TimeSpan timeout, CancellationToken token)
        {
            return DownloadFileTaskAsync(client, address, path, tries, timeout, null, token);
        }

        public static Task<FileInfo?> DownloadFileTaskAsync(this WebClient client, String address, String path, Boolean overwrite, Byte tries, TimeSpan timeout, CancellationToken token)
        {
            return DownloadFileTaskAsync(client, address, path, overwrite, tries, timeout, null, token);
        }

        public static Task<FileInfo?> DownloadFileTaskAsync(this WebClient client, String address, String path, Boolean overwrite, Byte tries, TimeSpan timeout, Action<Byte>? callback)
        {
            return DownloadFileTaskAsync(client, address, path, overwrite, tries, timeout, callback, CancellationToken.None);
        }

        public static Task<FileInfo?> DownloadFileTaskAsync(this WebClient client, String address, String path, Byte tries, TimeSpan timeout, Action<Byte>? callback, CancellationToken token)
        {
            return DownloadFileTaskAsync(client, address, path, false, tries, timeout, callback, token);
        }

        public static Task<FileInfo?> DownloadFileTaskAsync(this WebClient client, String address, String path, Boolean overwrite, Byte tries, TimeSpan timeout, Action<Byte>? callback, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            Task<FileInfo?> DownloadWebFileTaskAsync(WebClient web, String url)
            {
                return DownloadFileHandlerAsync(web, url, path, overwrite);
            }
            
            return DownloadTaskAsync(client, DownloadWebFileTaskAsync, address, tries, timeout, callback, token);
        }

        public static Task<Boolean> ReadAsync(this WebClient client, String address, Func<Stream, CancellationToken, Task> handler)
        {
            return ReadAsync(client, address, handler, CancellationToken.None);
        }

        public static Task<Boolean> ReadAsync(this WebClient client, String address, Func<Byte[], CancellationToken, Task<Boolean>> handler)
        {
            return ReadAsync(client, address, handler, CancellationToken.None);
        }

        public static Task<Boolean> ReadAsync(this WebClient client, String address, Func<Byte[], CancellationToken, Task<Boolean>> handler, CancellationToken token)
        {
            return ReadAsync(client, address, handler, new Byte[BufferUtilities.DefaultBuffer], token);
        }

        public static Task<Boolean> ReadAsync(this WebClient client, String address, Func<Byte[], CancellationToken, Task<Boolean>> handler, Int32 bufferSize)
        {
            return ReadAsync(client, address, handler, bufferSize, CancellationToken.None);
        }

        public static Task<Boolean> ReadAsync(this WebClient client, String address, Func<Byte[], CancellationToken, Task<Boolean>> handler, Byte[]? buffer)
        {
            return ReadAsync(client, address, handler, buffer, CancellationToken.None);
        }

        public static Task<Boolean> ReadAsync(this WebClient client, String address, Func<Byte[], CancellationToken, Task<Boolean>> handler, Int32 bufferSize, CancellationToken token)
        {
            return ReadAsync(client, address, handler, new Byte[bufferSize], token);
        }

        private static Task<Stream> GetStreamAsync(WebClient client, String address)
        {
            return client.OpenReadTaskAsync(address);
        }

        public static async Task<Boolean> ReadAsync(this WebClient client, String address, Func<Stream, CancellationToken, Task> handler, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            try
            {
                await using Stream stream = await GetStreamAsync(client, address).ConfigureAwait(false);
                await handler(stream, token).ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Task<Boolean> ReadAsync(this WebClient client, String address, Func<Byte[], CancellationToken, Task<Boolean>> handler, Byte[]? buffer, CancellationToken token)
        {
            return ReadAsync(client, address, handler, buffer, null, token);
        }

        public static async Task<Boolean> ReadAsync(this WebClient client, String address, Func<Byte[], CancellationToken, Task<Boolean>> handler, Byte[]? buffer,
            IProgress<WebDownloadProgress>? progress, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            try
            {
                await using Stream stream = await GetStreamAsync(client, address).ConfigureAwait(false);

                if (!Int32.TryParse(client.ResponseHeaders?[HttpResponseHeader.ContentLength], out Int32 bytes))
                {
                    return false;
                }

                buffer ??= new Byte[BufferUtilities.DefaultBuffer];
                
                Int32 received = 0;
                Int32 read;
                while ((read = await stream.ReadAsync(buffer.AsMemory(0, buffer.Length), token).ConfigureAwait(false)) > 0)
                {
                    Boolean successfull = await handler(buffer.SubArray(read), token).ConfigureAwait(false);

                    if (!successfull || token.IsCancellationRequested)
                    {
                        return false;
                    }

                    received += read;
                    progress?.Report(new WebDownloadProgress(address, received, bytes));
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}