// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Network
{
    public class HttpRequestHeaderParser
    {
        protected HttpUnsortedRequest Request { get; }
        protected HttpRequestState Status { get; set; }
        protected HttpRequestLineParser LineParser { get; }
        protected InternetMessageFormatHeaderParser HeaderParser { get; }

        public HttpRequestHeaderParser(HttpUnsortedRequest request)
            : this(request, 2048, 16384)
        {
        }

        public HttpRequestHeaderParser(HttpUnsortedRequest request, Int32 linesize, Int32 header)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            LineParser = new HttpRequestLineParser(Request, linesize);
            HeaderParser = new InternetMessageFormatHeaderParser(Request.HttpHeaders, header);
        }

        // ReSharper disable once CognitiveComplexity
        public HttpParserState ParseBuffer(Byte[] buffer, Int32 ready, ref Int32 consumed)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            switch (Status)
            {
                case HttpRequestState.RequestLine:
                {
                    HttpParserState state;
                    try
                    {
                        state = LineParser.ParseBuffer(buffer, ready, ref consumed);
                    }
                    catch (Exception)
                    {
                        state = HttpParserState.Invalid;
                    }

                    if (state is not HttpParserState.Done)
                    {
                        return state;
                    }

                    Status = HttpRequestState.RequestHeaders;
                    goto case HttpRequestState.RequestHeaders;
                }
                case HttpRequestState.RequestHeaders:
                {
                    if (consumed >= ready)
                    {
                        return HttpParserState.NeedMoreData;
                    }

                    HttpParserState state;
                    try
                    {
                        state = HeaderParser.ParseBuffer(buffer, ready, ref consumed);
                    }
                    catch (Exception)
                    {
                        state = HttpParserState.Invalid;
                    }

                    return state;
                }
            }

            return HttpParserState.NeedMoreData;
        }

        protected enum HttpRequestState
        {
            RequestLine,
            RequestHeaders
        }
    }
}