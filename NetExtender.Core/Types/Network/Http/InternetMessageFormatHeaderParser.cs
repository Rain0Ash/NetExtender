// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net.Http.Headers;
using System.Text;

namespace NetExtender.Types.Network
{
    public class InternetMessageFormatHeaderParser
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

        public Int32 Header { get; }

        private HeaderFieldState _state;
        private HeaderFieldState State
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
        
        private HttpHeaders Headers { get; }
        private CurrentHeaderFieldStore Current { get; }
        private Boolean Ignore { get; }

        public InternetMessageFormatHeaderParser(HttpHeaders headers, Int32 header)
            : this(headers, header, false)
        {
        }

        public InternetMessageFormatHeaderParser(HttpHeaders headers, Int32 header, Boolean ignore)
        {
            Headers = headers ?? throw new ArgumentNullException(nameof(headers));
            Header = header >= 2 ? header : throw new ArgumentOutOfRangeException(nameof(header), header, null);
            Ignore = ignore;
            Current = new CurrentHeaderFieldStore();
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
                return ParseHeaderFields(buffer, ready, ref consumed, ref _state, Header, ref _total, Current, Headers, !Ignore);
            }
            catch (Exception)
            {
                return HttpParserState.Invalid;
            }
        }

        private static HttpParserState ParseHeaderFields(Byte[] buffer, Int32 ready, ref Int32 consumed, ref HeaderFieldState state, Int32 length, ref Int32 total, CurrentHeaderFieldStore current, HttpHeaders headers, Boolean validation)
        {
            Int32 start = consumed;
            HttpParserState fields = HttpParserState.DataTooBig;
            Int32 end = length <= 0 ? Int32.MaxValue : length - total + start;
            
            if (ready < end)
            {
                fields = HttpParserState.NeedMoreData;
                end = ready;
            }
            
            switch (state)
            {
                case HeaderFieldState.Name:
                {
                    Int32 index = consumed;
                    while (buffer[consumed] != 58)
                    {
                        if (buffer[consumed] == 13)
                        {
                            if (!current.IsEmpty)
                            {
                                fields = HttpParserState.Invalid;
                                total += consumed - start;
                                return fields;
                            }

                            state = HeaderFieldState.AfterCarriageReturn;
                            if (++consumed != end)
                            {
                                goto case HeaderFieldState.AfterCarriageReturn;
                            }

                            total += consumed - start;
                            return fields;
                        }

                        if (++consumed != end)
                        {
                            continue;
                        }

                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Name.Append(value);
                        total += consumed - start;
                        return fields;
                    }
                    
                    if (consumed > index)
                    {
                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Name.Append(value);
                    }
                    
                    state = HeaderFieldState.Value;
                    if (++consumed == end)
                    {
                        break;
                    }

                    goto case HeaderFieldState.Value;
                }
                case HeaderFieldState.Value:
                {
                    Int32 index = consumed;
                    while (buffer[consumed] != 13)
                    {
                        if (++consumed != end)
                        {
                            continue;
                        }

                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Value.Append(value);
                        total += consumed - start;
                        return fields;
                    }
                    
                    if (consumed > index)
                    {
                        String value = Encoding.UTF8.GetString(buffer, index, consumed - index);
                        current.Value.Append(value);
                    }
                    
                    state = HeaderFieldState.AfterCarriageReturn;
                    if (++consumed == end)
                    {
                        break;
                    }

                    goto case HeaderFieldState.AfterCarriageReturn;
                }
                case HeaderFieldState.AfterCarriageReturn:
                {
                    if (buffer[consumed] != 10)
                    {
                        fields = HttpParserState.Invalid;
                        break;
                    }
                    
                    if (current.IsEmpty)
                    {
                        fields = HttpParserState.Done;
                        ++consumed;
                        break;
                    }
                    
                    state = HeaderFieldState.FoldingLine;
                    if (++consumed == end)
                    {
                        break;
                    }

                    goto case HeaderFieldState.FoldingLine;
                }
                case HeaderFieldState.FoldingLine:
                {
                    if (buffer[consumed] != 32 && buffer[consumed] != 9)
                    {
                        current.CopyTo(headers, validation);
                        state = HeaderFieldState.Name;
                        if (consumed == end)
                        {
                            break;
                        }

                        goto case HeaderFieldState.Name;
                    }

                    current.Value.Append(' ');
                    state = HeaderFieldState.Value;
                    if (++consumed == end)
                    {
                        break;
                    }

                    goto case HeaderFieldState.Value;
                }
            }
            
            total += consumed - start;
            return fields;
        }

        private enum HeaderFieldState
        {
            Name,
            Value,
            AfterCarriageReturn,
            FoldingLine
        }

        private class CurrentHeaderFieldStore
        {
            private static Char[] LinearWhiteSpace { get; } =
            {
                ' ',
                '\t'
            };

            public StringBuilder Name { get; } = new StringBuilder(128);
            public StringBuilder Value { get; } = new StringBuilder(2048);
            
            public Boolean IsEmpty
            {
                get
                {
                    return Name.Length <= 0 && Value.Length <= 0;
                }
            }

            public void CopyTo(HttpHeaders headers, Boolean validation)
            {
                String name = Name.ToString();
                String value = Value.ToString().Trim(LinearWhiteSpace);
                
                if (String.Equals("expires", name, StringComparison.OrdinalIgnoreCase))
                {
                    validation = false;
                }

                if (validation)
                {
                    headers.Add(name, value);
                    Clear();
                    return;
                }

                headers.TryAddWithoutValidation(name, value);
                Clear();
            }

            private void Clear()
            {
                Name.Clear();
                Value.Clear();
            }
        }
    }
}