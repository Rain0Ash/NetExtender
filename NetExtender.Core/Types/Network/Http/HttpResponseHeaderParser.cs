// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Network
{
    public class HttpResponseHeaderParser
    {
        private HttpUnsortedResponse Response { get; }
        private HttpResponseState ResponseStatus { get; set; }
        private HttpStatusLineParser StatusLineParser { get; }
        private InternetMessageFormatHeaderParser HeaderParser { get; }

        public HttpResponseHeaderParser(HttpUnsortedResponse response)
            : this(response, 2048, 16384)
        {
        }

        public HttpResponseHeaderParser(HttpUnsortedResponse response, Int32 linesize, Int32 header)
        {
            Response = response ?? throw new ArgumentNullException(nameof(response));
            StatusLineParser = new HttpStatusLineParser(Response, linesize);
            HeaderParser = new InternetMessageFormatHeaderParser(Response.HttpHeaders, header);
        }

        public HttpParserState ParseBuffer(Byte[] buffer, Int32 ready, ref Int32 consumed)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            switch (ResponseStatus)
            {
                case HttpResponseState.StatusLine:
                {
                    try
                    {
                        HttpParserState state = StatusLineParser.ParseBuffer(buffer, ready, ref consumed);
                        switch (state)
                        {
                            case HttpParserState.NeedMoreData:
                                return HttpParserState.NeedMoreData;
                            case HttpParserState.Done:
                                ResponseStatus = HttpResponseState.ResponseHeaders;
                                goto ResponseHeaders;
                            default:
                                return state;
                        }
                    }
                    catch (Exception)
                    {
                        return HttpParserState.Invalid;
                    }
                }
                case HttpResponseState.ResponseHeaders: ResponseHeaders:
                {
                    if (consumed >= ready)
                    {
                        break;
                    }

                    try
                    {
                        HttpParserState state = HeaderParser.ParseBuffer(buffer, ready, ref consumed);
                        return state switch
                        {
                            HttpParserState.NeedMoreData => HttpParserState.NeedMoreData,
                            HttpParserState.Done => state,
                            _ => state
                        };
                    }
                    catch (Exception)
                    {
                        return HttpParserState.Invalid;
                    }
                }
            }

            return HttpParserState.NeedMoreData;
        }

        private enum HttpResponseState
        {
            StatusLine,
            ResponseHeaders
        }
    }
}