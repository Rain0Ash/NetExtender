using System;
using System.Collections.Generic;
using System.Text;
using NetExtender.Network.Networking.Common;

namespace NetExtender.Network.Networking.Http
{
    /// <summary>
    ///     HTTP request is used to create or process parameters of HTTP protocol request(method, URL, headers, etc).
    /// </summary>
    /// <remarks>Not thread-safe.</remarks>
    public class HttpNetworkRequest
    {
        // HTTP request cookies
        private readonly List<Tuple<String, String>> _cookies = new List<Tuple<String, String>>();

        // HTTP request headers
        private readonly List<Tuple<String, String>> _headers = new List<Tuple<String, String>>();

        // HTTP request body
        private Int32 _bodyIndex;
        private Int32 _bodyLength;
        private Boolean _bodyLengthProvided;
        private Int32 _bodySize;

        // HTTP request cache

        private Int32 _cacheSize;

        // HTTP request method

        // HTTP request protocol

        // HTTP request URL

        /// <summary>
        ///     Initialize an empty HTTP request
        /// </summary>
        public HttpNetworkRequest()
        {
            Clear();
        }

        /// <summary>
        ///     Initialize a new HTTP request with a given method, URL and protocol
        /// </summary>
        /// <param name="method">HTTP method</param>
        /// <param name="url">Requested URL</param>
        /// <param name="protocol">Protocol version (default is "HTTP/1.1")</param>
        public HttpNetworkRequest(String method, String url, String protocol = "HTTP/1.1")
        {
            SetBegin(method, url, protocol);
        }

        /// <summary>
        ///     Is the HTTP request empty?
        /// </summary>
        public Boolean IsEmpty
        {
            get
            {
                return Cache.Size == 0;
            }
        }

        /// <summary>
        ///     Is the HTTP request error flag set?
        /// </summary>
        public Boolean IsErrorSet { get; private set; }

        /// <summary>
        ///     Get the HTTP request method
        /// </summary>
        public String Method { get; private set; }

        /// <summary>
        ///     Get the HTTP request URL
        /// </summary>
        public String Url { get; private set; }

        /// <summary>
        ///     Get the HTTP request protocol version
        /// </summary>
        public String Protocol { get; private set; }

        /// <summary>
        ///     Get the HTTP request headers count
        /// </summary>
        public Int64 Headers
        {
            get
            {
                return _headers.Count;
            }
        }

        /// <summary>
        ///     Get the HTTP request cookies count
        /// </summary>
        private Int64 Cookies
        {
            get
            {
                return _cookies.Count;
            }
        }

        /// <summary>
        ///     Get the HTTP request body as string
        /// </summary>
        public String Body
        {
            get
            {
                return Cache.ExtractString(_bodyIndex, _bodySize);
            }
        }

        /// <summary>
        ///     Get the HTTP request body as byte array
        /// </summary>
        public Byte[] BodyBytes
        {
            get
            {
                return Cache.Data[_bodyIndex..(_bodyIndex + _bodySize)];
            }
        }

        /// <summary>
        ///     Get the HTTP request body as byte span
        /// </summary>
        public Span<Byte> BodySpan
        {
            get
            {
                return new Span<Byte>(Cache.Data, _bodyIndex, _bodySize);
            }
        }

        /// <summary>
        ///     Get the HTTP request body length
        /// </summary>
        public Int64 BodyLength
        {
            get
            {
                return _bodyLength;
            }
        }

        /// <summary>
        ///     Get the HTTP request cache content
        /// </summary>
        public NetworkDataBuffer Cache { get; } = new NetworkDataBuffer();

        /// <summary>
        ///     Get the HTTP request header by index
        /// </summary>
        public Tuple<String, String> Header(Int32 i)
        {
            if (i >= _headers.Count)
            {
                return new Tuple<String, String>("", "");
            }

            return _headers[i];
        }

        /// <summary>
        ///     Get the HTTP request cookie by index
        /// </summary>
        private Tuple<String, String> Cookie(Int32 i)
        {
            if (i >= _cookies.Count)
            {
                return new Tuple<String, String>("", "");
            }

            return _cookies[i];
        }

        /// <summary>
        ///     Get string from the current HTTP request
        /// </summary>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Request method: {Method}");
            sb.AppendLine($"Request URL: {Url}");
            sb.AppendLine($"Request protocol: {Protocol}");
            sb.AppendLine($"Request headers: {Headers}");
            for (Int32 i = 0; i < Headers; ++i)
            {
                Tuple<String, String> header = Header(i);
                sb.AppendLine($"{header.Item1} : {header.Item2}");
            }

            sb.AppendLine($"Request body: {BodyLength}");
            sb.AppendLine(Body);
            return sb.ToString();
        }

        /// <summary>
        ///     Clear the HTTP request cache
        /// </summary>
        public HttpNetworkRequest Clear()
        {
            IsErrorSet = false;
            Method = "";
            Url = "";
            Protocol = "";
            _headers.Clear();
            _cookies.Clear();
            _bodyIndex = 0;
            _bodySize = 0;
            _bodyLength = 0;
            _bodyLengthProvided = false;

            Cache.Clear();
            _cacheSize = 0;
            return this;
        }

        /// <summary>
        ///     Set the HTTP request begin with a given method, URL and protocol
        /// </summary>
        /// <param name="method">HTTP method</param>
        /// <param name="url">Requested URL</param>
        /// <param name="protocol">Protocol version (default is "HTTP/1.1")</param>
        public HttpNetworkRequest SetBegin(String method, String url, String protocol = "HTTP/1.1")
        {
            // Clear the HTTP request cache
            Clear();

            // Append the HTTP request method
            Cache.Append(method);
            Method = method;

            Cache.Append(" ");

            // Append the HTTP request URL
            Cache.Append(url);
            Url = url;

            Cache.Append(" ");

            // Append the HTTP request protocol version
            Cache.Append(protocol);
            Protocol = protocol;

            Cache.Append("\r\n");
            return this;
        }

        /// <summary>
        ///     Set the HTTP request header
        /// </summary>
        /// <param name="key">Header key</param>
        /// <param name="value">Header value</param>
        public HttpNetworkRequest SetHeader(String key, String value)
        {
            // Append the HTTP request header's key
            Cache.Append(key);

            Cache.Append(": ");

            // Append the HTTP request header's value
            Cache.Append(value);

            Cache.Append("\r\n");

            // Add the header to the corresponding collection
            _headers.Add(new Tuple<String, String>(key, value));
            return this;
        }

        /// <summary>
        ///     Set the HTTP request cookie
        /// </summary>
        /// <param name="name">Cookie name</param>
        /// <param name="value">Cookie value</param>
        public HttpNetworkRequest SetCookie(String name, String value)
        {
            const String key = "Cookie";
            String cookie = name + "=" + value;

            // Append the HTTP request header's key
            Cache.Append(key);

            Cache.Append(": ");

            // Append Cookie
            Cache.Append(cookie);

            Cache.Append("\r\n");

            // Add the header to the corresponding collection
            _headers.Add(new Tuple<String, String>(key, cookie));
            // Add the cookie to the corresponding collection
            _cookies.Add(new Tuple<String, String>(name, value));
            return this;
        }

        /// <summary>
        ///     Add the HTTP request cookie
        /// </summary>
        /// <param name="name">Cookie name</param>
        /// <param name="value">Cookie value</param>
        public HttpNetworkRequest AddCookie(String name, String value)
        {
            // Append Cookie
            Cache.Append("; ");
            Cache.Append(name);
            Cache.Append("=");
            Cache.Append(value);

            // Add the cookie to the corresponding collection
            _cookies.Add(new Tuple<String, String>(name, value));
            return this;
        }

        /// <summary>
        ///     Set the HTTP request body
        /// </summary>
        /// <param name="body">Body string content (default is "")</param>
        public HttpNetworkRequest SetBody(String body = "")
        {
            Int32 length = String.IsNullOrEmpty(body) ? 0 : Encoding.UTF8.GetByteCount(body);

            // Append content length header
            SetHeader("Content-Length", length.ToString());

            Cache.Append("\r\n");

            Int32 index = (Int32) Cache.Size;

            // Append the HTTP request body
            Cache.Append(body);
            _bodyIndex = index;
            _bodySize = length;
            _bodyLength = length;
            _bodyLengthProvided = true;
            return this;
        }

        /// <summary>
        ///     Set the HTTP request body
        /// </summary>
        /// <param name="body">Body binary content</param>
        public HttpNetworkRequest SetBody(Byte[] body)
        {
            // Append content length header
            SetHeader("Content-Length", body.Length.ToString());

            Cache.Append("\r\n");

            Int32 index = (Int32) Cache.Size;

            // Append the HTTP request body
            Cache.Append(body);
            _bodyIndex = index;
            _bodySize = body.Length;
            _bodyLength = body.Length;
            _bodyLengthProvided = true;
            return this;
        }

        /// <summary>
        ///     Set the HTTP request body
        /// </summary>
        /// <param name="body">Body buffer content</param>
        public HttpNetworkRequest SetBody(NetworkDataBuffer body)
        {
            // Append content length header
            SetHeader("Content-Length", body.Size.ToString());

            Cache.Append("\r\n");

            Int32 index = (Int32) Cache.Size;

            // Append the HTTP request body
            Cache.Append(body.Data, body.Offset, body.Size);
            _bodyIndex = index;
            _bodySize = (Int32) body.Size;
            _bodyLength = (Int32) body.Size;
            _bodyLengthProvided = true;
            return this;
        }

        /// <summary>
        ///     Set the HTTP request body length
        /// </summary>
        /// <param name="length">Body length</param>
        public HttpNetworkRequest SetBodyLength(Int32 length)
        {
            // Append content length header
            SetHeader("Content-Length", length.ToString());

            Cache.Append("\r\n");

            Int32 index = (Int32) Cache.Size;

            // Clear the HTTP request body
            _bodyIndex = index;
            _bodySize = 0;
            _bodyLength = length;
            _bodyLengthProvided = true;
            return this;
        }

        /// <summary>
        ///     Make HEAD request
        /// </summary>
        /// <param name="url">URL to request</param>
        public HttpNetworkRequest MakeHeadRequest(String url)
        {
            Clear();
            SetBegin("HEAD", url);
            SetBody();
            return this;
        }

        /// <summary>
        ///     Make GET request
        /// </summary>
        /// <param name="url">URL to request</param>
        public HttpNetworkRequest MakeGetRequest(String url)
        {
            Clear();
            SetBegin("GET", url);
            SetBody();
            return this;
        }

        /// <summary>
        ///     Make POST request
        /// </summary>
        /// <param name="url">URL to request</param>
        /// <param name="content">String content</param>
        /// <param name="contentType">Content type (default is "text/plain; charset=UTF-8")</param>
        public HttpNetworkRequest MakePostRequest(String url, String content, String contentType = "text/plain; charset=UTF-8")
        {
            Clear();
            SetBegin("POST", url);
            if (!String.IsNullOrEmpty(contentType))
            {
                SetHeader("Content-Type", contentType);
            }

            SetBody(content);
            return this;
        }

        /// <summary>
        ///     Make POST request
        /// </summary>
        /// <param name="url">URL to request</param>
        /// <param name="content">Binary content</param>
        /// <param name="contentType">Content type (default is "")</param>
        public HttpNetworkRequest MakePostRequest(String url, Byte[] content, String contentType = "")
        {
            Clear();
            SetBegin("POST", url);
            if (!String.IsNullOrEmpty(contentType))
            {
                SetHeader("Content-Type", contentType);
            }

            SetBody(content);
            return this;
        }

        /// <summary>
        ///     Make POST request
        /// </summary>
        /// <param name="url">URL to request</param>
        /// <param name="content">Buffer content</param>
        /// <param name="contentType">Content type (default is "")</param>
        public HttpNetworkRequest MakePostRequest(String url, NetworkDataBuffer content, String contentType = "")
        {
            Clear();
            SetBegin("POST", url);
            if (!String.IsNullOrEmpty(contentType))
            {
                SetHeader("Content-Type", contentType);
            }

            SetBody(content);
            return this;
        }

        /// <summary>
        ///     Make PUT request
        /// </summary>
        /// <param name="url">URL to request</param>
        /// <param name="content">String content</param>
        /// <param name="contentType">Content type (default is "text/plain; charset=UTF-8")</param>
        public HttpNetworkRequest MakePutRequest(String url, String content, String contentType = "text/plain; charset=UTF-8")
        {
            Clear();
            SetBegin("PUT", url);
            if (!String.IsNullOrEmpty(contentType))
            {
                SetHeader("Content-Type", contentType);
            }

            SetBody(content);
            return this;
        }

        /// <summary>
        ///     Make PUT request
        /// </summary>
        /// <param name="url">URL to request</param>
        /// <param name="content">Binary content</param>
        /// <param name="contentType">Content type (default is "")</param>
        public HttpNetworkRequest MakePutRequest(String url, Byte[] content, String contentType = "")
        {
            Clear();
            SetBegin("PUT", url);
            if (!String.IsNullOrEmpty(contentType))
            {
                SetHeader("Content-Type", contentType);
            }

            SetBody(content);
            return this;
        }

        /// <summary>
        ///     Make PUT request
        /// </summary>
        /// <param name="url">URL to request</param>
        /// <param name="content">Buffer content</param>
        /// <param name="contentType">Content type (default is "")</param>
        public HttpNetworkRequest MakePutRequest(String url, NetworkDataBuffer content, String contentType = "")
        {
            Clear();
            SetBegin("PUT", url);
            if (!String.IsNullOrEmpty(contentType))
            {
                SetHeader("Content-Type", contentType);
            }

            SetBody(content);
            return this;
        }

        /// <summary>
        ///     Make DELETE request
        /// </summary>
        /// <param name="url">URL to request</param>
        public HttpNetworkRequest MakeDeleteRequest(String url)
        {
            Clear();
            SetBegin("DELETE", url);
            SetBody();
            return this;
        }

        /// <summary>
        ///     Make OPTIONS request
        /// </summary>
        /// <param name="url">URL to request</param>
        public HttpNetworkRequest MakeOptionsRequest(String url)
        {
            Clear();
            SetBegin("OPTIONS", url);
            SetBody();
            return this;
        }

        /// <summary>
        ///     Make TRACE request
        /// </summary>
        /// <param name="url">URL to request</param>
        public HttpNetworkRequest MakeTraceRequest(String url)
        {
            Clear();
            SetBegin("TRACE", url);
            SetBody();
            return this;
        }

        // Is pending parts of HTTP request
        internal Boolean IsPendingHeader()
        {
            return !IsErrorSet && _bodyIndex == 0;
        }

        internal Boolean IsPendingBody()
        {
            return !IsErrorSet && _bodyIndex > 0 && _bodySize > 0;
        }

        internal Boolean ReceiveHeader(Byte[] buffer, Int32 offset, Int32 size)
        {
            // Update the request cache
            Cache.Append(buffer, offset, size);

            // Try to seek for HTTP header separator
            for (Int32 i = _cacheSize; i < (Int32) Cache.Size; ++i)
            {
                // Check for the request cache out of bounds
                if (i + 3 >= (Int32) Cache.Size)
                {
                    break;
                }

                // Check for the header separator
                if (Cache[i + 0] != '\r' || Cache[i + 1] != '\n' || Cache[i + 2] != '\r' || Cache[i + 3] != '\n')
                {
                    continue;
                }

                Int32 index = 0;

                // Set the error flag for a while...
                IsErrorSet = true;

                // Parse method
                Int32 methodIndex = index;
                Int32 methodSize = 0;
                while (Cache[index] != ' ')
                {
                    ++methodSize;
                    ++index;
                    if (index >= (Int32) Cache.Size)
                    {
                        return false;
                    }
                }

                ++index;
                if (index >= (Int32) Cache.Size)
                {
                    return false;
                }

                Method = Cache.ExtractString(methodIndex, methodSize);

                // Parse URL
                Int32 urlIndex = index;
                Int32 urlSize = 0;
                while (Cache[index] != ' ')
                {
                    ++urlSize;
                    ++index;
                    if (index >= (Int32) Cache.Size)
                    {
                        return false;
                    }
                }

                ++index;
                if (index >= (Int32) Cache.Size)
                {
                    return false;
                }

                Url = Cache.ExtractString(urlIndex, urlSize);

                // Parse protocol version
                Int32 protocolIndex = index;
                Int32 protocolSize = 0;
                while (Cache[index] != '\r')
                {
                    ++protocolSize;
                    ++index;
                    if (index >= (Int32) Cache.Size)
                    {
                        return false;
                    }
                }

                ++index;
                if (index >= (Int32) Cache.Size || Cache[index] != '\n')
                {
                    return false;
                }

                ++index;
                if (index >= (Int32) Cache.Size)
                {
                    return false;
                }

                Protocol = Cache.ExtractString(protocolIndex, protocolSize);

                // Parse headers
                while (index < (Int32) Cache.Size && index < i)
                {
                    // Parse header name
                    Int32 headerNameIndex = index;
                    Int32 headerNameSize = 0;
                    while (Cache[index] != ':')
                    {
                        ++headerNameSize;
                        ++index;
                        if (index >= i)
                        {
                            break;
                        }

                        if (index >= (Int32) Cache.Size)
                        {
                            return false;
                        }
                    }

                    ++index;
                    if (index >= i)
                    {
                        break;
                    }

                    if (index >= (Int32) Cache.Size)
                    {
                        return false;
                    }

                    // Skip all prefix space characters
                    while (Char.IsWhiteSpace((Char) Cache[index]))
                    {
                        ++index;
                        if (index >= i)
                        {
                            break;
                        }

                        if (index >= (Int32) Cache.Size)
                        {
                            return false;
                        }
                    }

                    // Parse header value
                    Int32 headerValueIndex = index;
                    Int32 headerValueSize = 0;
                    while (Cache[index] != '\r')
                    {
                        ++headerValueSize;
                        ++index;
                        if (index >= i)
                        {
                            break;
                        }

                        if (index >= (Int32) Cache.Size)
                        {
                            return false;
                        }
                    }

                    ++index;
                    if (index >= (Int32) Cache.Size || Cache[index] != '\n')
                    {
                        return false;
                    }

                    ++index;
                    if (index >= (Int32) Cache.Size)
                    {
                        return false;
                    }

                    // Validate header name and value
                    if (headerNameSize == 0 || headerValueSize == 0)
                    {
                        return false;
                    }

                    // Add a new header
                    String headerName = Cache.ExtractString(headerNameIndex, headerNameSize);
                    String headerValue = Cache.ExtractString(headerValueIndex, headerValueSize);
                    _headers.Add(new Tuple<String, String>(headerName, headerValue));

                    switch (headerName)
                    {
                        // Try to find the body content length
                        case "Content-Length":
                        {
                            _bodyLength = 0;
                            for (Int32 j = headerValueIndex; j < headerValueIndex + headerValueSize; ++j)
                            {
                                if (Cache[j] < '0' || Cache[j] > '9')
                                {
                                    return false;
                                }

                                _bodyLength *= 10;
                                _bodyLength += Cache[j] - '0';
                                _bodyLengthProvided = true;
                            }

                            break;
                        }
                        // Try to find Cookies
                        case "Cookie":
                        {
                            Boolean name = true;
                            Boolean token = false;
                            Int32 current = headerValueIndex;
                            Int32 nameIndex = index;
                            Int32 nameSize = 0;
                            Int32 cookieIndex = index;
                            Int32 cookieSize = 0;
                            for (Int32 j = headerValueIndex; j < headerValueIndex + headerValueSize; ++j)
                            {
                                if (Cache[j] == ' ')
                                {
                                    if (token)
                                    {
                                        if (name)
                                        {
                                            nameIndex = current;
                                            nameSize = j - current;
                                        }
                                        else
                                        {
                                            cookieIndex = current;
                                            cookieSize = j - current;
                                        }
                                    }

                                    token = false;
                                    continue;
                                }

                                if (Cache[j] == '=')
                                {
                                    if (token)
                                    {
                                        if (name)
                                        {
                                            nameIndex = current;
                                            nameSize = j - current;
                                        }
                                        else
                                        {
                                            cookieIndex = current;
                                            cookieSize = j - current;
                                        }
                                    }

                                    token = false;
                                    name = false;
                                    continue;
                                }

                                if (Cache[j] == ';')
                                {
                                    if (token)
                                    {
                                        if (name)
                                        {
                                            nameIndex = current;
                                            nameSize = j - current;
                                        }
                                        else
                                        {
                                            cookieIndex = current;
                                            cookieSize = j - current;
                                        }

                                        // Validate the cookie
                                        if (nameSize > 0 && cookieSize > 0)
                                        {
                                            // Add the cookie to the corresponding collection
                                            _cookies.Add(new Tuple<String, String>(Cache.ExtractString(nameIndex, nameSize), Cache.ExtractString(cookieIndex, cookieSize)));

                                            // Resset the current cookie values
                                            nameIndex = j;
                                            nameSize = 0;
                                            cookieIndex = j;
                                            cookieSize = 0;
                                        }
                                    }

                                    token = false;
                                    name = true;
                                    continue;
                                }

                                if (token)
                                {
                                    continue;
                                }

                                current = j;
                                token = true;
                            }

                            // Process the last cookie
                            if (token)
                            {
                                if (name)
                                {
                                    nameIndex = current;
                                    nameSize = headerValueIndex + headerValueSize - current;
                                }
                                else
                                {
                                    cookieIndex = current;
                                    cookieSize = headerValueIndex + headerValueSize - current;
                                }

                                // Validate the cookie
                                if (nameSize > 0 && cookieSize > 0)
                                {
                                    // Add the cookie to the corresponding collection
                                    _cookies.Add(new Tuple<String, String>(Cache.ExtractString(nameIndex, nameSize), Cache.ExtractString(cookieIndex, cookieSize)));
                                }
                            }

                            break;
                        }
                    }
                }

                // Reset the error flag
                IsErrorSet = false;

                // Update the body index and size
                _bodyIndex = i + 4;
                _bodySize = (Int32) Cache.Size - i - 4;

                // Update the parsed cache size
                _cacheSize = (Int32) Cache.Size;

                return true;
            }

            // Update the parsed cache size
            _cacheSize = (Int32) Cache.Size >= 3 ? (Int32) Cache.Size - 3 : 0;

            return false;
        }

        internal Boolean ReceiveBody(Byte[] buffer, Int32 offset, Int32 size)
        {
            // Update the request cache
            Cache.Append(buffer, offset, size);

            // Update the parsed cache size
            _cacheSize = (Int32) Cache.Size;

            // Update body size
            _bodySize += size;

            // GET request has no body
            if (Method == "HEAD" || Method == "GET" || Method == "OPTIONS" || Method == "TRACE")
            {
                _bodyLength = 0;
                _bodySize = 0;
                return true;
            }

            // Check if the body was fully parsed
            if (!_bodyLengthProvided || _bodySize < _bodyLength)
            {
                return false;
            }

            _bodySize = _bodyLength;
            return true;

        }
    }
}