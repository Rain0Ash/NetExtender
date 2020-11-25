using System;
using System.Collections.Generic;
using System.Text;
using NetExtender.Network.Networking.Common;

namespace NetExtender.Network.Networking.Http
{
    /// <summary>
    ///     HTTP response is used to create or process parameters of HTTP protocol response(status, headers, etc).
    /// </summary>
    /// <remarks>Not thread-safe.</remarks>
    public class HttpNetworkResponse
    {
        // HTTP response headers
        private readonly List<Tuple<String, String>> _headers = new List<Tuple<String, String>>();

        // HTTP response body
        private Int32 _bodyIndex;
        private Int32 _bodyLength;
        private Boolean _bodyLengthProvided;
        private Int32 _bodySize;

        // HTTP response cache

        private Int32 _cacheSize;

        // HTTP response protocol

        // HTTP response status phrase

        /// <summary>
        ///     Initialize an empty HTTP response
        /// </summary>
        public HttpNetworkResponse()
        {
            Clear();
        }

        /// <summary>
        ///     Initialize a new HTTP response with a given status and protocol
        /// </summary>
        /// <param name="status">HTTP status</param>
        /// <param name="protocol">Protocol version (default is "HTTP/1.1")</param>
        public HttpNetworkResponse(Int32 status, String protocol = "HTTP/1.1")
        {
            SetBegin(status, protocol);
        }

        /// <summary>
        ///     Initialize a new HTTP response with a given status, status phrase and protocol
        /// </summary>
        /// <param name="status">HTTP status</param>
        /// <param name="statusPhrase">HTTP status phrase</param>
        /// <param name="protocol">Protocol version</param>
        public HttpNetworkResponse(Int32 status, String statusPhrase, String protocol)
        {
            SetBegin(status, statusPhrase, protocol);
        }

        /// <summary>
        ///     Is the HTTP response empty?
        /// </summary>
        public Boolean IsEmpty
        {
            get
            {
                return Cache.Size > 0;
            }
        }

        /// <summary>
        ///     Is the HTTP response error flag set?
        /// </summary>
        public Boolean IsErrorSet { get; private set; }

        /// <summary>
        ///     Get the HTTP response status
        /// </summary>
        public Int32 Status { get; private set; }

        /// <summary>
        ///     Get the HTTP response status phrase
        /// </summary>
        public String StatusPhrase { get; private set; }

        /// <summary>
        ///     Get the HTTP response protocol version
        /// </summary>
        public String Protocol { get; private set; }

        /// <summary>
        ///     Get the HTTP response headers count
        /// </summary>
        public Int64 Headers
        {
            get
            {
                return _headers.Count;
            }
        }

        /// <summary>
        ///     Get the HTTP response body as string
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
        ///     Get the HTTP request body as read-only byte span
        /// </summary>
        public ReadOnlySpan<Byte> BodySpan
        {
            get
            {
                return new ReadOnlySpan<Byte>(Cache.Data, _bodyIndex, _bodySize);
            }
        }

        /// <summary>
        ///     Get the HTTP response body length
        /// </summary>
        public Int64 BodyLength
        {
            get
            {
                return _bodyLength;
            }
        }

        /// <summary>
        ///     Get the HTTP response cache content
        /// </summary>
        public NetworkDataBuffer Cache { get; } = new NetworkDataBuffer();

        /// <summary>
        ///     Get the HTTP response header by index
        /// </summary>
        public Tuple<String, String> Header(Int32 i)
        {
            return i >= _headers.Count ? new Tuple<String, String>("", "") : _headers[i];
        }

        /// <summary>
        ///     Get string from the current HTTP response
        /// </summary>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Status: {Status}");
            sb.AppendLine($"Status phrase: {StatusPhrase}");
            sb.AppendLine($"Protocol: {Protocol}");
            sb.AppendLine($"Headers: {Headers}");
            for (Int32 i = 0; i < Headers; ++i)
            {
                Tuple<String, String> header = Header(i);
                sb.AppendLine($"{header.Item1} : {header.Item2}");
            }

            sb.AppendLine($"Body: {BodyLength}");
            sb.AppendLine(Body);
            return sb.ToString();
        }

        /// <summary>
        ///     Clear the HTTP response cache
        /// </summary>
        public HttpNetworkResponse Clear()
        {
            IsErrorSet = false;
            Status = 0;
            StatusPhrase = "";
            Protocol = "";
            _headers.Clear();
            _bodyIndex = 0;
            _bodySize = 0;
            _bodyLength = 0;
            _bodyLengthProvided = false;

            Cache.Clear();
            _cacheSize = 0;
            return this;
        }

        /// <summary>
        ///     Set the HTTP response begin with a given status and protocol
        /// </summary>
        /// <param name="status">HTTP status</param>
        /// <param name="protocol">Protocol version (default is "HTTP/1.1")</param>
        public HttpNetworkResponse SetBegin(Int32 status, String protocol = "HTTP/1.1")
        {
            String statusPhrase;

            switch (status)
            {
                case 100:
                    statusPhrase = "Continue";
                    break;
                case 101:
                    statusPhrase = "Switching Protocols";
                    break;
                case 102:
                    statusPhrase = "Processing";
                    break;
                case 103:
                    statusPhrase = "Early Hints";
                    break;

                case 200:
                    statusPhrase = "OK";
                    break;
                case 201:
                    statusPhrase = "Created";
                    break;
                case 202:
                    statusPhrase = "Accepted";
                    break;
                case 203:
                    statusPhrase = "Non-Authoritative Information";
                    break;
                case 204:
                    statusPhrase = "No Content";
                    break;
                case 205:
                    statusPhrase = "Reset Content";
                    break;
                case 206:
                    statusPhrase = "Partial Content";
                    break;
                case 207:
                    statusPhrase = "Multi-Status";
                    break;
                case 208:
                    statusPhrase = "Already Reported";
                    break;

                case 226:
                    statusPhrase = "IM Used";
                    break;

                case 300:
                    statusPhrase = "Multiple Choices";
                    break;
                case 301:
                    statusPhrase = "Moved Permanently";
                    break;
                case 302:
                    statusPhrase = "Found";
                    break;
                case 303:
                    statusPhrase = "See Other";
                    break;
                case 304:
                    statusPhrase = "Not Modified";
                    break;
                case 305:
                    statusPhrase = "Use Proxy";
                    break;
                case 306:
                    statusPhrase = "Switch Proxy";
                    break;
                case 307:
                    statusPhrase = "Temporary Redirect";
                    break;
                case 308:
                    statusPhrase = "Permanent Redirect";
                    break;

                case 400:
                    statusPhrase = "Bad Request";
                    break;
                case 401:
                    statusPhrase = "Unauthorized";
                    break;
                case 402:
                    statusPhrase = "Payment Required";
                    break;
                case 403:
                    statusPhrase = "Forbidden";
                    break;
                case 404:
                    statusPhrase = "Not Found";
                    break;
                case 405:
                    statusPhrase = "Method Not Allowed";
                    break;
                case 406:
                    statusPhrase = "Not Acceptable";
                    break;
                case 407:
                    statusPhrase = "Proxy Authentication Required";
                    break;
                case 408:
                    statusPhrase = "Request Timeout";
                    break;
                case 409:
                    statusPhrase = "Conflict";
                    break;
                case 410:
                    statusPhrase = "Gone";
                    break;
                case 411:
                    statusPhrase = "Length Required";
                    break;
                case 412:
                    statusPhrase = "Precondition Failed";
                    break;
                case 413:
                    statusPhrase = "Payload Too Large";
                    break;
                case 414:
                    statusPhrase = "URI Too Long";
                    break;
                case 415:
                    statusPhrase = "Unsupported Media Type";
                    break;
                case 416:
                    statusPhrase = "Range Not Satisfiable";
                    break;
                case 417:
                    statusPhrase = "Expectation Failed";
                    break;

                case 421:
                    statusPhrase = "Misdirected Request";
                    break;
                case 422:
                    statusPhrase = "Unprocessable Entity";
                    break;
                case 423:
                    statusPhrase = "Locked";
                    break;
                case 424:
                    statusPhrase = "Failed Dependency";
                    break;
                case 425:
                    statusPhrase = "Too Early";
                    break;
                case 426:
                    statusPhrase = "Upgrade Required";
                    break;
                case 427:
                    statusPhrase = "Unassigned";
                    break;
                case 428:
                    statusPhrase = "Precondition Required";
                    break;
                case 429:
                    statusPhrase = "Too Many Requests";
                    break;
                case 431:
                    statusPhrase = "Request Header Fields Too Large";
                    break;

                case 451:
                    statusPhrase = "Unavailable For Legal Reasons";
                    break;

                case 500:
                    statusPhrase = "Internal Server Error";
                    break;
                case 501:
                    statusPhrase = "Not Implemented";
                    break;
                case 502:
                    statusPhrase = "Bad Gateway";
                    break;
                case 503:
                    statusPhrase = "Service Unavailable";
                    break;
                case 504:
                    statusPhrase = "Gateway Timeout";
                    break;
                case 505:
                    statusPhrase = "HTTP Version Not Supported";
                    break;
                case 506:
                    statusPhrase = "Variant Also Negotiates";
                    break;
                case 507:
                    statusPhrase = "Insufficient Storage";
                    break;
                case 508:
                    statusPhrase = "Loop Detected";
                    break;

                case 510:
                    statusPhrase = "Not Extended";
                    break;
                case 511:
                    statusPhrase = "Network Authentication Required";
                    break;

                default:
                    statusPhrase = "Unknown";
                    break;
            }

            SetBegin(status, statusPhrase, protocol);
            return this;
        }

        /// <summary>
        ///     Set the HTTP response begin with a given status, status phrase and protocol
        /// </summary>
        /// <param name="status">HTTP status</param>
        /// <param name="statusPhrase"> HTTP status phrase</param>
        /// <param name="protocol">Protocol version</param>
        public HttpNetworkResponse SetBegin(Int32 status, String statusPhrase, String protocol)
        {
            // Clear the HTTP response cache
            Clear();

            // Append the HTTP response protocol version
            Cache.Append(protocol);
            Protocol = protocol;

            Cache.Append(" ");

            // Append the HTTP response status
            Cache.Append(status.ToString());
            Status = status;

            Cache.Append(" ");

            // Append the HTTP response status phrase
            Cache.Append(statusPhrase);
            StatusPhrase = statusPhrase;

            Cache.Append("\r\n");
            return this;
        }

        /// <summary>
        ///     Set the HTTP response content type
        /// </summary>
        /// <param name="extension">Content extension</param>
        public HttpNetworkResponse SetContentType(String extension)
        {
            // Base content types
            if (extension == ".html")
            {
                return SetHeader("Content-Type", "text/html");
            }

            if (extension == ".css")
            {
                return SetHeader("Content-Type", "text/css");
            }

            if (extension == ".js")
            {
                return SetHeader("Content-Type", "text/javascript");
            }

            if (extension == ".xml")
            {
                return SetHeader("Content-Type", "text/xml");
            }

            // Common content types
            if (extension == ".gzip")
            {
                return SetHeader("Content-Type", "application/gzip");
            }

            if (extension == ".json")
            {
                return SetHeader("Content-Type", "application/json");
            }

            if (extension == ".map")
            {
                return SetHeader("Content-Type", "application/json");
            }

            if (extension == ".pdf")
            {
                return SetHeader("Content-Type", "application/pdf");
            }

            if (extension == ".zip")
            {
                return SetHeader("Content-Type", "application/zip");
            }

            if (extension == ".mp3")
            {
                return SetHeader("Content-Type", "audio/mpeg");
            }

            if (extension == ".jpg")
            {
                return SetHeader("Content-Type", "image/jpeg");
            }

            if (extension == ".gif")
            {
                return SetHeader("Content-Type", "image/gif");
            }

            if (extension == ".png")
            {
                return SetHeader("Content-Type", "image/png");
            }

            if (extension == ".svg")
            {
                return SetHeader("Content-Type", "image/svg+xml");
            }

            if (extension == ".mp4")
            {
                return SetHeader("Content-Type", "video/mp4");
            }

            // Application content types
            if (extension == ".atom")
            {
                return SetHeader("Content-Type", "application/atom+xml");
            }

            if (extension == ".fastsoap")
            {
                return SetHeader("Content-Type", "application/fastsoap");
            }

            if (extension == ".ps")
            {
                return SetHeader("Content-Type", "application/postscript");
            }

            if (extension == ".soap")
            {
                return SetHeader("Content-Type", "application/soap+xml");
            }

            if (extension == ".sql")
            {
                return SetHeader("Content-Type", "application/sql");
            }

            if (extension == ".xslt")
            {
                return SetHeader("Content-Type", "application/xslt+xml");
            }

            if (extension == ".zlib")
            {
                return SetHeader("Content-Type", "application/zlib");
            }

            // Audio content types
            if (extension == ".aac")
            {
                return SetHeader("Content-Type", "audio/aac");
            }

            if (extension == ".ac3")
            {
                return SetHeader("Content-Type", "audio/ac3");
            }

            if (extension == ".ogg")
            {
                return SetHeader("Content-Type", "audio/ogg");
            }

            // Font content types
            if (extension == ".ttf")
            {
                return SetHeader("Content-Type", "font/ttf");
            }

            // Image content types
            if (extension == ".bmp")
            {
                return SetHeader("Content-Type", "image/bmp");
            }

            if (extension == ".jpm")
            {
                return SetHeader("Content-Type", "image/jpm");
            }

            if (extension == ".jpx")
            {
                return SetHeader("Content-Type", "image/jpx");
            }

            if (extension == ".jrx")
            {
                return SetHeader("Content-Type", "image/jrx");
            }

            if (extension == ".tiff")
            {
                return SetHeader("Content-Type", "image/tiff");
            }

            if (extension == ".emf")
            {
                return SetHeader("Content-Type", "image/emf");
            }

            if (extension == ".wmf")
            {
                return SetHeader("Content-Type", "image/wmf");
            }

            // Message content types
            if (extension == ".http")
            {
                return SetHeader("Content-Type", "message/http");
            }

            if (extension == ".s-http")
            {
                return SetHeader("Content-Type", "message/s-http");
            }

            // Model content types
            if (extension == ".mesh")
            {
                return SetHeader("Content-Type", "model/mesh");
            }

            if (extension == ".vrml")
            {
                return SetHeader("Content-Type", "model/vrml");
            }

            // Text content types
            if (extension == ".csv")
            {
                return SetHeader("Content-Type", "text/csv");
            }

            if (extension == ".plain")
            {
                return SetHeader("Content-Type", "text/plain");
            }

            if (extension == ".richtext")
            {
                return SetHeader("Content-Type", "text/richtext");
            }

            if (extension == ".rtf")
            {
                return SetHeader("Content-Type", "text/rtf");
            }

            if (extension == ".rtx")
            {
                return SetHeader("Content-Type", "text/rtx");
            }

            if (extension == ".sgml")
            {
                return SetHeader("Content-Type", "text/sgml");
            }

            if (extension == ".strings")
            {
                return SetHeader("Content-Type", "text/strings");
            }

            if (extension == ".url")
            {
                return SetHeader("Content-Type", "text/uri-list");
            }

            // Video content types
            if (extension == ".H264")
            {
                return SetHeader("Content-Type", "video/H264");
            }

            if (extension == ".H265")
            {
                return SetHeader("Content-Type", "video/H265");
            }

            if (extension == ".mpeg")
            {
                return SetHeader("Content-Type", "video/mpeg");
            }

            if (extension == ".raw")
            {
                return SetHeader("Content-Type", "video/raw");
            }

            return this;
        }

        /// <summary>
        ///     Set the HTTP response header
        /// </summary>
        /// <param name="key">Header key</param>
        /// <param name="value">Header value</param>
        public HttpNetworkResponse SetHeader(String key, String value)
        {
            // Append the HTTP response header's key
            Cache.Append(key);

            Cache.Append(": ");

            // Append the HTTP response header's value
            Cache.Append(value);

            Cache.Append("\r\n");

            // Add the header to the corresponding collection
            _headers.Add(new Tuple<String, String>(key, value));
            return this;
        }

        /// <summary>
        ///     Set the HTTP response cookie
        /// </summary>
        /// <param name="name">Cookie name</param>
        /// <param name="value">Cookie value</param>
        /// <param name="maxAge">Cookie age in seconds until it expires (default is 86400)</param>
        /// <param name="path">Cookie path (default is "")</param>
        /// <param name="domain">Cookie domain (default is "")</param>
        /// <param name="secure">Cookie secure flag (default is true)</param>
        /// <param name="httpOnly">Cookie HTTP-only flag (default is false)</param>
        public HttpNetworkResponse SetCookie(String name, String value, Int32 maxAge = 86400, String path = "", String domain = "", Boolean secure = true, Boolean httpOnly = false)
        {
            const String key = "Set-Cookie";

            // Append the HTTP response header's key
            Cache.Append(key);

            Cache.Append(": ");

            // Append the HTTP response header's value
            Int32 valueIndex = (Int32) Cache.Size;

            // Append cookie
            Cache.Append(name);
            Cache.Append("=");
            Cache.Append(value);
            Cache.Append("; Max-Age=");
            Cache.Append(maxAge.ToString());
            if (!String.IsNullOrEmpty(domain))
            {
                Cache.Append("; Domain=");
                Cache.Append(domain);
            }

            if (!String.IsNullOrEmpty(path))
            {
                Cache.Append("; Path=");
                Cache.Append(path);
            }

            if (secure)
            {
                Cache.Append("; Secure");
            }

            if (httpOnly)
            {
                Cache.Append("; HttpOnly");
            }

            Int32 valueSize = (Int32) Cache.Size - valueIndex;

            String cookie = Cache.ExtractString(valueIndex, valueSize);

            Cache.Append("\r\n");

            // Add the header to the corresponding collection
            _headers.Add(new Tuple<String, String>(key, cookie));
            return this;
        }

        /// <summary>
        ///     Set the HTTP response body
        /// </summary>
        /// <param name="body">Body string content (default is "")</param>
        public HttpNetworkResponse SetBody(String body = "")
        {
            Int32 length = String.IsNullOrEmpty(body) ? 0 : Encoding.UTF8.GetByteCount(body);

            // Append content length header
            SetHeader("Content-Length", length.ToString());

            Cache.Append("\r\n");

            Int32 index = (Int32) Cache.Size;

            // Append the HTTP response body
            Cache.Append(body);
            _bodyIndex = index;
            _bodySize = length;
            _bodyLength = length;
            _bodyLengthProvided = true;
            return this;
        }

        /// <summary>
        ///     Set the HTTP response body
        /// </summary>
        /// <param name="body">Body binary content</param>
        public HttpNetworkResponse SetBody(Byte[] body)
        {
            // Append content length header
            SetHeader("Content-Length", body.Length.ToString());

            Cache.Append("\r\n");

            Int32 index = (Int32) Cache.Size;

            // Append the HTTP response body
            Cache.Append(body);
            _bodyIndex = index;
            _bodySize = body.Length;
            _bodyLength = body.Length;
            _bodyLengthProvided = true;
            return this;
        }

        /// <summary>
        ///     Set the HTTP response body
        /// </summary>
        /// <param name="body">Body buffer content</param>
        public HttpNetworkResponse SetBody(NetworkDataBuffer body)
        {
            // Append content length header
            SetHeader("Content-Length", body.Size.ToString());

            Cache.Append("\r\n");

            Int32 index = (Int32) Cache.Size;

            // Append the HTTP response body
            Cache.Append(body.Data, body.Offset, body.Size);
            _bodyIndex = index;
            _bodySize = (Int32) body.Size;
            _bodyLength = (Int32) body.Size;
            _bodyLengthProvided = true;
            return this;
        }

        /// <summary>
        ///     Set the HTTP response body length
        /// </summary>
        /// <param name="length">Body length</param>
        public HttpNetworkResponse SetBodyLength(Int32 length)
        {
            // Append content length header
            SetHeader("Content-Length", length.ToString());

            Cache.Append("\r\n");

            Int32 index = (Int32) Cache.Size;

            // Clear the HTTP response body
            _bodyIndex = index;
            _bodySize = 0;
            _bodyLength = length;
            _bodyLengthProvided = true;
            return this;
        }

        /// <summary>
        ///     Make OK response
        /// </summary>
        /// <param name="status">OK status (default is 200 (OK))</param>
        public HttpNetworkResponse MakeOkResponse(Int32 status = 200)
        {
            Clear();
            SetBegin(status);
            SetBody();
            return this;
        }

        /// <summary>
        ///     Make ERROR response
        /// </summary>
        /// <param name="error">Error content (default is "")</param>
        /// <param name="status">OK status (default is 200 (OK))</param>
        public HttpNetworkResponse MakeErrorResponse(String error = "", Int32 status = 500)
        {
            Clear();
            SetBegin(status);
            SetBody(error);
            return this;
        }

        /// <summary>
        ///     Make HEAD response
        /// </summary>
        public HttpNetworkResponse MakeHeadResponse()
        {
            Clear();
            SetBegin(200);
            SetBody();
            return this;
        }

        /// <summary>
        ///     Make GET response
        /// </summary>
        /// <param name="content">String content</param>
        /// <param name="contentType">Content type (default is "text/plain; charset=UTF-8")</param>
        public HttpNetworkResponse MakeGetResponse(String content = "", String contentType = "text/plain; charset=UTF-8")
        {
            Clear();
            SetBegin(200);
            if (!String.IsNullOrEmpty(contentType))
            {
                SetHeader("Content-Type", contentType);
            }

            SetBody(content);
            return this;
        }

        /// <summary>
        ///     Make GET response
        /// </summary>
        /// <param name="content">String content</param>
        /// <param name="contentType">Content type (default is "")</param>
        public HttpNetworkResponse MakeGetResponse(Byte[] content, String contentType = "")
        {
            Clear();
            SetBegin(200);
            if (!String.IsNullOrEmpty(contentType))
            {
                SetHeader("Content-Type", contentType);
            }

            SetBody(content);
            return this;
        }

        /// <summary>
        ///     Make GET response
        /// </summary>
        /// <param name="content">String content</param>
        /// <param name="contentType">Content type (default is "")</param>
        public HttpNetworkResponse MakeGetResponse(NetworkDataBuffer content, String contentType = "")
        {
            Clear();
            SetBegin(200);
            if (!String.IsNullOrEmpty(contentType))
            {
                SetHeader("Content-Type", contentType);
            }

            SetBody(content);
            return this;
        }

        /// <summary>
        ///     Make OPTIONS response
        /// </summary>
        /// <param name="allow">Allow methods (default is "HEAD,GET,POST,PUT,DELETE,OPTIONS,TRACE")</param>
        public HttpNetworkResponse MakeOptionsResponse(String allow = "HEAD,GET,POST,PUT,DELETE,OPTIONS,TRACE")
        {
            Clear();
            SetBegin(200);
            SetHeader("Allow", allow);
            SetBody();
            return this;
        }

        /// <summary>
        ///     Make TRACE response
        /// </summary>
        /// <param name="request">Request string content</param>
        public HttpNetworkResponse MakeTraceResponse(String request)
        {
            Clear();
            SetBegin(200);
            SetHeader("Content-Type", "message/http");
            SetBody(request);
            return this;
        }

        /// <summary>
        ///     Make TRACE response
        /// </summary>
        /// <param name="request">Request binary content</param>
        public HttpNetworkResponse MakeTraceResponse(Byte[] request)
        {
            Clear();
            SetBegin(200);
            SetHeader("Content-Type", "message/http");
            SetBody(request);
            return this;
        }

        /// <summary>
        ///     Make TRACE response
        /// </summary>
        /// <param name="request">Request buffer content</param>
        public HttpNetworkResponse MakeTraceResponse(NetworkDataBuffer request)
        {
            Clear();
            SetBegin(200);
            SetHeader("Content-Type", "message/http");
            SetBody(request);
            return this;
        }

        // Is pending parts of HTTP response
        internal Boolean IsPendingHeader()
        {
            return !IsErrorSet && _bodyIndex == 0;
        }

        internal Boolean IsPendingBody()
        {
            return !IsErrorSet && _bodyIndex > 0 && _bodySize > 0;
        }

        // Receive parts of HTTP response
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

                // Parse protocol version
                Int32 protocolIndex = index;
                Int32 protocolSize = 0;
                while (Cache[index] != ' ')
                {
                    ++protocolSize;
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

                Protocol = Cache.ExtractString(protocolIndex, protocolSize);

                // Parse status code
                Int32 statusIndex = index;
                Int32 statusSize = 0;
                while (Cache[index] != ' ')
                {
                    if (Cache[index] < '0' || Cache[index] > '9')
                    {
                        return false;
                    }

                    ++statusSize;
                    ++index;
                    if (index >= (Int32) Cache.Size)
                    {
                        return false;
                    }
                }

                Status = 0;
                for (Int32 j = statusIndex; j < statusIndex + statusSize; ++j)
                {
                    Status *= 10;
                    Status += Cache[j] - '0';
                }

                ++index;
                if (index >= (Int32) Cache.Size)
                {
                    return false;
                }

                // Parse status phrase
                Int32 statusPhraseIndex = index;
                Int32 statusPhraseSize = 0;
                while (Cache[index] != '\r')
                {
                    ++statusPhraseSize;
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

                StatusPhrase = Cache.ExtractString(statusPhraseIndex, statusPhraseSize);

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

                    // Try to find the body content length
                    if (headerName != "Content-Length")
                    {
                        continue;
                    }

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