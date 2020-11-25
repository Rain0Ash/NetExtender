// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Times;
using NetExtender.Utils.IO;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.Network
{
    public static class HttpClientUtils
    {
        public static HttpClient Create(HttpMessageHandler handler = null, String agent = null, Boolean disposeHandler = true)
        {
            handler ??= new HttpClientHandler();
            HttpClient client = new HttpClient(handler, disposeHandler);

            if (agent != String.Empty)
            {
                client.DefaultRequestHeaders.UserAgent.TryParseAdd(agent ?? WebUtils.RandomUserAgent);
            }

            return client;
        }
        
        private static Task<HttpResponseMessage> DownloadTaskAsync(this HttpClient client, String address, CancellationToken token)
        {
            return DownloadTaskAsync(client, address, HttpCompletionOption.ResponseHeadersRead, token);
        }
        
        private static async Task<HttpResponseMessage> DownloadTaskAsync(this HttpClient client, String address, HttpCompletionOption option, CancellationToken token)
        {
            HttpResponseMessage message = await client.GetAsync(address, option, token).ConfigureAwait(false);
            message.EnsureSuccessStatusCode();

            return message;
        }

        private static Task<HttpResponseMessage> DownloadTaskAsync(this HttpClient client, String address, HttpCompletionOption option,
            Byte tries, TimeSpan timeout, Action<Byte> callback, CancellationToken token)
        {
            Task<HttpResponseMessage> DownloadTask(CancellationToken cancel)
            {
                return DownloadTaskAsync(client, address, option, cancel);
            }

            return TaskUtils.TimeoutRetryTaskAsync(DownloadTask, tries, timeout, callback, token);
        }
        
        public static Task<String> GetHeadAsync(this HttpClient client, String address)
        {
            return GetHeadAsync(client, address, CancellationToken.None);
        }
        
        public static async Task<String> GetHeadAsync(this HttpClient client, String address, CancellationToken token)
        {
            using HttpResponseMessage message = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, address), token).ConfigureAwait(false);
            return await message.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        public static Task<String> GetHeadAsync(this HttpClient client, String address, Byte tries, TimeSpan timeout, Action<Byte> callback, CancellationToken token)
        {
            Task<String> HandlerAsync(CancellationToken cancel)
            {
                return GetHeadAsync(client, address, cancel);
            }

            return TaskUtils.TimeoutRetryTaskAsync(HandlerAsync, tries, timeout, callback, token);
        }

        public static Task<String> DownloadStringAsync(this HttpClient client, String address)
        {
            return DownloadStringAsync(client, address, CancellationToken.None);
        }
        
        public static Task<String> DownloadStringAsync(this HttpClient client, String address, CancellationToken token)
        {
            return DownloadStringAsync(client, address, Encoding.UTF8, token);
        }

        public static Task<String> DownloadStringAsync(this HttpClient client, String address, Encoding encoding)
        {
            return DownloadStringAsync(client, address, encoding, CancellationToken.None);
        }

        public static async Task<String> DownloadStringAsync(this HttpClient client, String address, Encoding encoding, CancellationToken token)
        {
            using HttpResponseMessage message = await DownloadTaskAsync(client, address, token).ConfigureAwait(false);
            using HttpContent content = message.Content;
            await using Stream stream = await content.ReadAsStreamAsync().ConfigureAwait(false);
            using StreamReader reader = new StreamReader(stream, encoding);
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }
        
        public static Task<String> DownloadStringAsync(this HttpClient client, String address, Byte tries)
        {
            return DownloadStringAsync(client, address, tries, CancellationToken.None);
        }
        
        public static Task<String> DownloadStringAsync(this HttpClient client, String address, Byte tries, CancellationToken token)
        {
            return DownloadStringAsync(client, address, Encoding.UTF8, tries, token);
        }
        
        public static Task<String> DownloadStringAsync(this HttpClient client, String address, Encoding encoding, Byte tries)
        {
            return DownloadStringAsync(client, address, encoding, tries, CancellationToken.None);
        }
        
        public static Task<String> DownloadStringAsync(this HttpClient client, String address, Encoding encoding, Byte tries, CancellationToken token)
        {
            return DownloadStringAsync(client, address, encoding, tries, Time.Minute.OneHalf, token);
        }

        private const Byte DefaultTries = 3;
        public static Task<String> DownloadStringAsync(this HttpClient client, String address, TimeSpan timeout)
        {
            return DownloadStringAsync(client, address, DefaultTries, timeout);
        }
        
        public static Task<String> DownloadStringAsync(this HttpClient client, String address, Byte tries, TimeSpan timeout)
        {
            return DownloadStringAsync(client, address, tries, timeout, CancellationToken.None);
        }
        
        public static Task<String> DownloadStringAsync(this HttpClient client, String address, Byte tries, TimeSpan timeout, CancellationToken token)
        {
            return DownloadStringAsync(client, address, tries, timeout, null, token);
        }
        
        public static Task<String> DownloadStringAsync(this HttpClient client, String address, Encoding encoding, TimeSpan timeout)
        {
            return DownloadStringAsync(client, address, encoding, DefaultTries, timeout);
        }
        
        public static Task<String> DownloadStringAsync(this HttpClient client, String address, Encoding encoding, Byte tries, TimeSpan timeout)
        {
            return DownloadStringAsync(client, address, encoding, tries, timeout, CancellationToken.None);
        }
        
        public static Task<String> DownloadStringAsync(this HttpClient client, String address, Encoding encoding, Byte tries, TimeSpan timeout, CancellationToken token)
        {
            return DownloadStringAsync(client, address, encoding, tries, timeout, null, token);
        }
        
        public static Task<String> DownloadStringAsync(this HttpClient client, String address, Byte tries, TimeSpan timeout, Action<Byte> callback)
        {
            return DownloadStringAsync(client, address, tries, timeout, callback, CancellationToken.None);
        }
        
        public static Task<String> DownloadStringAsync(this HttpClient client, String address, Byte tries, TimeSpan timeout, Action<Byte> callback, CancellationToken token)
        {
            return DownloadStringAsync(client, address, Encoding.UTF8, tries, timeout, callback, token);
        }
        
        public static Task<String> DownloadStringAsync(this HttpClient client, String address, Encoding encoding, Byte tries, TimeSpan timeout, Action<Byte> callback)
        {
            return DownloadStringAsync(client, address, encoding, tries, timeout, callback, CancellationToken.None);
        }
        
        public static Task<String> DownloadStringAsync(this HttpClient client, String address, Encoding encoding, Byte tries, TimeSpan timeout, Action<Byte> callback, CancellationToken token)
        {
            Task<String> HandlerAsync(CancellationToken cancel)
            {
                return DownloadStringAsync(client, address, encoding, cancel);
            }

            return TaskUtils.TimeoutRetryTaskAsync(HandlerAsync, tries, timeout, callback, token);
        }

        private const Boolean DefaultOverwrite = false;
        public static Task<FileInfo> DownloadFileTaskAsync(this HttpClient client, String address, String path, Int32 bufferSize = BufferUtils.DefaultBuffer)
        {
            return DownloadFileTaskAsync(client, address, path, DefaultOverwrite, bufferSize, CancellationToken.None);
        }
        
        public static Task<FileInfo> DownloadFileTaskAsync(this HttpClient client, String address, String path, Boolean overwrite = DefaultOverwrite, Int32 bufferSize = BufferUtils.DefaultBuffer)
        {
            return DownloadFileTaskAsync(client, address, path, overwrite, bufferSize, CancellationToken.None);
        }

        public static Task<FileInfo> DownloadFileTaskAsync(this HttpClient client, String address, String path, CancellationToken token)
        {
            return DownloadFileTaskAsync(client, address, path, DefaultOverwrite, token);
        }
        
        public static Task<FileInfo> DownloadFileTaskAsync(this HttpClient client, String address, String path, Boolean overwrite, CancellationToken token)
        {
            return DownloadFileTaskAsync(client, address, path, overwrite, BufferUtils.DefaultBuffer, token);
        }
        
        public static Task<FileInfo> DownloadFileTaskAsync(this HttpClient client, String address, String path, Int32 bufferSize, CancellationToken token)
        {
            return DownloadFileTaskAsync(client, address, path, DefaultOverwrite, bufferSize, token);
        }

        public static async Task<FileInfo> DownloadFileTaskAsync(this HttpClient client, String address, String path, Boolean overwrite, Int32 bufferSize, CancellationToken token)
        {
            using HttpResponseMessage message = await DownloadTaskAsync(client, address, HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false);
            using HttpContent content = message.Content;
            await using Stream stream = await content.ReadAsStreamAsync().ConfigureAwait(false);
            
            try
            {
                return await FileUtils.SafeCreateFileAsync(path, stream, overwrite, bufferSize, token).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}