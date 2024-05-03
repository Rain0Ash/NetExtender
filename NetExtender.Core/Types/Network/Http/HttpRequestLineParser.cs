// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net.Http;
using System.Text;

namespace NetExtender.Types.Network
{
    public class HttpRequestLineParser
    {
        protected HttpUnsortedRequest Request { get; }
        
        private Int32 _total;
        protected Int32 Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
            }
        }

        protected Int32 Header { get; }

        private HttpRequestLineState _state;
        protected HttpRequestLineState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
        
        private StringBuilder Current { get; } = new StringBuilder(2048);

        public HttpRequestLineParser(HttpUnsortedRequest request, Int32 linesize)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            Header = linesize >= 14 ? linesize : throw new ArgumentOutOfRangeException(nameof(linesize), linesize, null);
        }

        public HttpParserState ParseBuffer(Byte[] buffer, Int32 ready, ref Int32 consumed)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (consumed >= ready)
            {
                return HttpParserState.NeedMoreData;
            }

            try
            {
                return ParseRequestLine(buffer, Request, ready, ref consumed, ref _state, Header, ref _total, Current);
            }
            catch (Exception)
            {
                return HttpParserState.Invalid;
            }
        }

        // ReSharper disable once CognitiveComplexity
        protected virtual HttpParserState ParseRequestLine(Byte[] buffer, HttpUnsortedRequest request, Int32 ready, ref Int32 consumed, ref HttpRequestLineState state, Int32 header, ref Int32 total, StringBuilder current)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (current is null)
            {
                throw new ArgumentNullException(nameof(current));
            }

            Int32 start = consumed;
            HttpParserState linestate = HttpParserState.DataTooBig;
            Int32 end = header > 0 ? header - total + consumed : Int32.MaxValue;
            if (ready < end)
            {
                linestate = HttpParserState.NeedMoreData;
                end = ready;
            }
            
            switch (state)
            {
                case HttpRequestLineState.RequestMethod:
                {
                    Int32 index = consumed;
                    while (buffer[consumed] != 32)
                    {
                        if (buffer[consumed] < 33 || buffer[consumed] > 122)
                        {
                            linestate = HttpParserState.Invalid;
                            total += consumed - start;
                            return linestate;
                        }

                        if (++consumed != end)
                        {
                            continue;
                        }

                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                        total += consumed - start;
                        return linestate;
                    }
                    
                    if (consumed > index)
                    {
                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                    }
                    
                    request.Method = new HttpMethod(current.ToString());
                    current.Clear();
                    state = HttpRequestLineState.RequestUri;
                    
                    if (++consumed == end)
                    {
                        break;
                    }

                    goto case HttpRequestLineState.RequestUri;
                }
                case HttpRequestLineState.RequestUri:
                {
                    Int32 index = consumed;
                    while (buffer[consumed] != 32)
                    {
                        if (buffer[consumed] == 13)
                        {
                            linestate = HttpParserState.Invalid;
                            total += consumed - start;
                            return linestate;
                        }

                        if (++consumed != end)
                        {
                            continue;
                        }

                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                        total += consumed - start;
                        return linestate;
                    }
                    
                    if (consumed > index)
                    {
                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                    }
                    
                    request.RequestUri = current.Length != 0 ? current.ToString() : throw new FormatException($"HTTP Request {nameof(Uri)} cannot be an empty string.");
                    current.Clear();
                    state = HttpRequestLineState.BeforeVersionNumbers;
                    
                    if (++consumed == end)
                    {
                        break;
                    }

                    goto case HttpRequestLineState.BeforeVersionNumbers;
                }
                case HttpRequestLineState.BeforeVersionNumbers:
                {
                    Int32 index = consumed;
                    while (buffer[consumed] != 47)
                    {
                        if (buffer[consumed] < 33 || buffer[consumed] > 122)
                        {
                            linestate = HttpParserState.Invalid;
                            total += consumed - start;
                            return linestate;
                        }

                        if (++consumed != end)
                        {
                            continue;
                        }

                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                        total += consumed - start;
                        return linestate;
                    }
                    
                    if (consumed > index)
                    {
                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                    }
                    
                    String version = current.ToString();
                    if (String.CompareOrdinal("HTTP", version) != 0)
                    {
                        throw new FormatException($"Invalid HTTP version: '{version}'. The version must start with the characters 'HTTP'.");
                    }

                    current.Clear();
                    state = HttpRequestLineState.MajorVersionNumber;
                    
                    if (++consumed == end)
                    {
                        break;
                    }

                    goto case HttpRequestLineState.MajorVersionNumber;
                }
                case HttpRequestLineState.MajorVersionNumber:
                {
                    Int32 index = consumed;
                    while (buffer[consumed] != 46)
                    {
                        if (buffer[consumed] < 48 || buffer[consumed] > 57)
                        {
                            linestate = HttpParserState.Invalid;
                            total += consumed - start;
                            return linestate;
                        }

                        if (++consumed != end)
                        {
                            continue;
                        }

                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                        total += consumed - start;
                        return linestate;
                    }
                    
                    if (consumed > index)
                    {
                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                    }
                    
                    current.Append('.');
                    state = HttpRequestLineState.MinorVersionNumber;
                    
                    if (++consumed == end)
                    {
                        break;
                    }

                    goto case HttpRequestLineState.MinorVersionNumber;
                }
                case HttpRequestLineState.MinorVersionNumber:
                {
                    Int32 index = consumed;
                    while (buffer[consumed] != 13)
                    {
                        if (buffer[consumed] < 48 || buffer[consumed] > 57)
                        {
                            linestate = HttpParserState.Invalid;
                            total += consumed - start;
                            return linestate;
                        }

                        if (++consumed != end)
                        {
                            continue;
                        }

                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                        total += consumed - start;
                        return linestate;
                    }
                    
                    if (consumed > index)
                    {
                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                    }
                    
                    request.Version = Version.Parse(current.ToString());
                    current.Clear();
                    state = HttpRequestLineState.AfterCarriageReturn;
                    
                    if (++consumed == end)
                    {
                        break;
                    }

                    goto case HttpRequestLineState.AfterCarriageReturn;
                }
                case HttpRequestLineState.AfterCarriageReturn:
                {
                    if (buffer[consumed] != 10)
                    {
                        linestate = HttpParserState.Invalid;
                        break;
                    }
                    
                    linestate = HttpParserState.Done;
                    ++consumed;
                    break;
                }
            }

            total += consumed - start;
            return linestate;
        }

        protected enum HttpRequestLineState
        {
            RequestMethod,
            RequestUri,
            BeforeVersionNumbers,
            MajorVersionNumber,
            MinorVersionNumber,
            AfterCarriageReturn
        }
    }
}