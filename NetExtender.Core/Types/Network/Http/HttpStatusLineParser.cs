// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Net;
using System.Text;

namespace NetExtender.Types.Network
{
    public class HttpStatusLineParser
    {
        private Int32 _total;
        private Int32 Total
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

        private Int32 Length { get; }
        
        private HttpStatusLineState _statusLineState;
        private HttpStatusLineState StatusLineState
        {
            get
            {
                return _statusLineState;
            }
            set
            {
                _statusLineState = value;
            }
        }

        private HttpUnsortedResponse Response { get; }
        private StringBuilder Current { get; } = new StringBuilder(2048);

        public HttpStatusLineParser(HttpUnsortedResponse response, Int32 linesize)
        {
            Response = response ?? throw new ArgumentNullException(nameof(response));
            Length = linesize >= 15 ? linesize : throw new ArgumentOutOfRangeException(nameof(linesize), linesize, null);
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
                return ParseStatusLine(buffer, Response, ready, ref consumed, ref _statusLineState, Length, ref _total, Current);
            }
            catch (Exception)
            {
                return HttpParserState.Invalid;
            }
        }

        // ReSharper disable once CognitiveComplexity
        private static HttpParserState ParseStatusLine(Byte[] buffer, HttpUnsortedResponse response, Int32 ready, ref Int32 consumed, ref HttpStatusLineState state, Int32 header, ref Int32 total, StringBuilder current)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            Int32 start = consumed;
            HttpParserState status = HttpParserState.DataTooBig;
            Int32 end = header <= 0 ? Int32.MaxValue : header - total + consumed;
            
            if (ready < end)
            {
                status = HttpParserState.NeedMoreData;
                end = ready;
            }
            
            switch (state)
            {
                case HttpStatusLineState.BeforeVersionNumbers:
                {
                    Int32 index = consumed;
                    while (buffer[consumed] != 47)
                    {
                        if (buffer[consumed] < 33 || buffer[consumed] > 122)
                        {
                            status = HttpParserState.Invalid;
                            total += consumed - start;
                            return status;
                        }

                        if (++consumed != end)
                        {
                            continue;
                        }

                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                        total += consumed - start;
                        return status;
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
                    state = HttpStatusLineState.MajorVersionNumber;
                    
                    if (++consumed == end)
                    {
                        break;
                    }

                    goto case HttpStatusLineState.MajorVersionNumber;
                }
                case HttpStatusLineState.MajorVersionNumber:
                {
                    Int32 index = consumed;
                    while (buffer[consumed] != 46)
                    {
                        if (buffer[consumed] < 48 || buffer[consumed] > 57)
                        {
                            status = HttpParserState.Invalid;
                            total += consumed - start;
                            return status;
                        }

                        if (++consumed != end)
                        {
                            continue;
                        }

                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                        total += consumed - start;
                        return status;
                    }
                    
                    if (consumed > index)
                    {
                        String str = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(str);
                    }
                    
                    current.Append('.');
                    state = HttpStatusLineState.MinorVersionNumber;
                    
                    if (++consumed == end)
                    {
                        break;
                    }

                    goto case HttpStatusLineState.MinorVersionNumber;
                }
                case HttpStatusLineState.MinorVersionNumber:
                {
                    Int32 index = consumed;
                    while (buffer[consumed] != 32)
                    {
                        if (buffer[consumed] < 48 || buffer[consumed] > 57)
                        {
                            status = HttpParserState.Invalid;
                            total += consumed - start;
                            return status;
                        }

                        if (++consumed != end)
                        {
                            continue;
                        }

                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                        total += consumed - start;
                        return status;
                    }
                    
                    if (consumed > index)
                    {
                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                    }
                    
                    response.Version = Version.Parse(current.ToString());
                    current.Clear();
                    state = HttpStatusLineState.StatusCode;
                    
                    if (++consumed == end)
                    {
                        break;
                    }

                    goto case HttpStatusLineState.StatusCode;
                }
                case HttpStatusLineState.StatusCode:
                {
                    Int32 index = consumed;
                    while (buffer[consumed] != 32)
                    {
                        if (buffer[consumed] < 48 || buffer[consumed] > 57)
                        {
                            status = HttpParserState.Invalid;
                            total += consumed - start;
                            return status;
                        }

                        if (++consumed != end)
                        {
                            continue;
                        }

                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                        total += consumed - start;
                        return status;
                    }
                    
                    if (consumed > index)
                    {
                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                    }
                    
                    Int32 code = Int32.Parse(current.ToString(), CultureInfo.InvariantCulture);
                    response.StatusCode = code >= 100 && code <= 1000 ? (HttpStatusCode) code : throw new FormatException($"Invalid HTTP status code: '{code}'. The status code must be between {100} and {1000}.");
                    current.Clear();
                    state = HttpStatusLineState.ReasonPhrase;
                    
                    if (++consumed == end)
                    {
                        break;
                    }

                    goto case HttpStatusLineState.ReasonPhrase;
                }
                case HttpStatusLineState.ReasonPhrase:
                {
                    Int32 index = consumed;
                    while (buffer[consumed] != 13)
                    {
                        if (buffer[consumed] < 32 || buffer[consumed] > 122)
                        {
                            status = HttpParserState.Invalid;
                            total += consumed - start;
                            return status;
                        }

                        if (++consumed != end)
                        {
                            continue;
                        }

                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                        total += consumed - start;
                        return status;
                    }
                    
                    if (consumed > index)
                    {
                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Append(value);
                    }
                    
                    response.ReasonPhrase = current.ToString();
                    current.Clear();
                    state = HttpStatusLineState.AfterCarriageReturn;
                    
                    if (++consumed == end)
                    {
                        break;
                    }

                    goto case HttpStatusLineState.AfterCarriageReturn;
                }
                case HttpStatusLineState.AfterCarriageReturn:
                {
                    if (buffer[consumed] != 10)
                    {
                        status = HttpParserState.Invalid;
                        break;
                    }
                    
                    status = HttpParserState.Done;
                    ++consumed;
                    break;
                }
            }

            total += consumed - start;
            return status;
        }
    }
}