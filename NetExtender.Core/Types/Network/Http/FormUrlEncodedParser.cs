// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NetExtender.Types.Network
{
    internal class FormUrlEncodedParser
    {
        protected enum NameValueState
        {
            Name,
            Value
        }
        
        private Int64 Size { get; }

        private NameValueState _state;
        private NameValueState State
        {
            get
            {
                return _state;
            }
        }

        private Int64 _read;
        private Int64 Read
        {
            get
            {
                return _read;
            }
        }
        
        private ICollection<KeyValuePair<String, String>> Collection { get; }
        private CurrentNameValuePair Pair { get; }

        public FormUrlEncodedParser(ICollection<KeyValuePair<String, String>> collection, Int64 size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), size, null);
            }

            Collection = collection ?? throw new ArgumentNullException(nameof(collection));
            Size = size;
            Pair = new CurrentNameValuePair();
        }

        public virtual HttpParserState ParseBuffer(Byte[] buffer, Int32 ready, ref Int32 read, Boolean final)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (read >= ready)
            {
                return final ? CopyCurrent(HttpParserState.NeedMoreData) : HttpParserState.NeedMoreData;
            }
            
            try
            {
                HttpParserState state = ParseNameValuePairs(Collection, buffer, ready, ref read, ref _state, Size, ref _read, Pair);
                return final ? CopyCurrent(state) : state;
            }
            catch (Exception)
            {
                return HttpParserState.Invalid;
            }
        }

        protected virtual HttpParserState ParseNameValuePairs(ICollection<KeyValuePair<String, String>> source, Byte[] buffer, Int32 ready, ref Int32 read, ref NameValueState state, Int64 length, ref Int64 total, CurrentNameValuePair current)
        {
            Int32 startread = read;
            Int64 endread = length <= 0 ? Int64.MaxValue : length - total + startread;
            
            HttpParserState parserstate = HttpParserState.DataTooBig;
            if (ready < endread)
            {
                parserstate = HttpParserState.NeedMoreData;
                endread = ready;
            }
            
            switch (state)
            {
                case NameValueState.Name:
                {
                    do {
                        Int32 index = read;
                        while (buffer[read] != 38 && buffer[read] != 61)
                        {
                            if (++read != endread)
                            {
                                continue;
                            }

                            String value = Encoding.UTF8.GetString(buffer, index, read - index);
                            current.Name.Append(value);
                                
                            total += read - startread;
                            return parserstate;
                        }
                        
                        if (read > index)
                        {
                            String value = Encoding.UTF8.GetString(buffer, index, read - index);
                            current.Name.Append(value);
                        }
                        
                        if (buffer[read] == 61)
                        {
                            state = NameValueState.Value;
                            if (++read != endread)
                            {
                                goto case NameValueState.Value;
                            }

                            break;
                        }

                        current.CopyTo(source, false);
                    } while (++read != endread);
                    
                    break;
                }
                case NameValueState.Value:
                {
                    Int32 index = read;
                    while (buffer[read] != 38)
                    {
                        if (++read != endread)
                        {
                            continue;
                        }

                        String value = Encoding.UTF8.GetString(buffer, index, read - index);
                        current.Value.Append(value);
                        
                        total += read - startread;
                        return parserstate;
                    }
                    
                    if (read > index)
                    {
                        String str = Encoding.UTF8.GetString(buffer, index, read - index);
                        current.Value.Append(str);
                    }
                    
                    current.CopyTo(source);
                    state = NameValueState.Name;
                    
                    if (++read != endread)
                    {
                        goto case NameValueState.Name;
                    }

                    break;
                }
            }

            total += read - startread;
            return parserstate;
        }

        protected virtual HttpParserState CopyCurrent(HttpParserState state)
        {
            if (State != NameValueState.Name)
            {
                Pair.CopyTo(Collection);
                return state != HttpParserState.NeedMoreData ? state : HttpParserState.Done;
            }

            if (Read > 0L)
            {
                Pair.CopyTo(Collection, false);
            }

            return state != HttpParserState.NeedMoreData ? state : HttpParserState.Done;
        }

        protected class CurrentNameValuePair
        {
            public StringBuilder Name { get; } = new StringBuilder(128);
            public StringBuilder Value { get; } = new StringBuilder(2048);

            public void CopyTo(ICollection<KeyValuePair<String, String>> destination)
            {
                CopyTo(destination, true);
            }

            public void CopyTo(ICollection<KeyValuePair<String, String>> destination, Boolean full)
            {
                if (destination is null)
                {
                    throw new ArgumentNullException(nameof(destination));
                }

                String key = WebUtility.UrlDecode(Name.ToString());
                String value = full ? WebUtility.UrlDecode(Value.ToString()) : String.Empty;
                destination.Add(new KeyValuePair<String, String>(key, value));
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