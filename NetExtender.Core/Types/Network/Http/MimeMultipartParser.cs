// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Text;
using NetExtender.Types.Exceptions;

namespace NetExtender.Types.Network
{
    public class MimeMultipartParser
    {
        private Int64 _total;
        protected Int64 Total
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

        protected Int64 Size { get; }

        private BodyPartState _state;
        protected BodyPartState State
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

        protected String Boundary { get; }
        protected CurrentBodyPartStore Current { get; }
        
        public Boolean IsWaiting
        {
            get
            {
                return State == BodyPartState.AfterBoundary && Current.IsFinal;
            }
        }

        public MimeMultipartParser(String boundary, Int64 size)
        {
            if (String.IsNullOrWhiteSpace(boundary))
            {
                throw new ArgumentNullOrWhiteSpaceStringException(boundary, nameof(boundary));
            }

            if (boundary.Length > 246)
            {
                throw new ArgumentOutOfRangeException(nameof(boundary), boundary.Length, null);
            }

            if (boundary.EndsWith(" ", StringComparison.Ordinal))
            {
                throw new ArgumentException("MIME multipart boundary cannot end with an empty space.", nameof(boundary));
            }
            
            if (size < 10)
            {
                throw new ArgumentOutOfRangeException(nameof(size), size, null);
            }
            
            Size = size;
            Boundary = boundary;
            Current = new CurrentBodyPartStore(Boundary);
            State = BodyPartState.AfterFirstLineFeed;
        }

        public virtual Boolean CanParseMore(Int32 read, Int32 consumed)
        {
            return consumed < read || read == 0 && IsWaiting;
        }

        public virtual MimeParserState ParseBuffer(Byte[] buffer, Int32 ready, ref Int32 consumed, out ArraySegment<Byte> remaining, out ArraySegment<Byte> part, out Boolean final)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            Int64 temp = consumed;
            MimeParserState state = ParseBuffer(buffer, ready, ref temp, out remaining, out part, out final);
            consumed = (Int32) temp;
            return state;
        }

        public virtual MimeParserState ParseBuffer(Byte[] buffer, Int32 ready, ref Int64 consumed, out ArraySegment<Byte> remaining, out ArraySegment<Byte> part, out Boolean final)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            remaining = new ArraySegment<Byte>(Array.Empty<Byte>());
            part = new ArraySegment<Byte>(Array.Empty<Byte>());
            final = false;
            MimeParserState state;
            
            try
            {
                state = ParseBodyPart(buffer, Current, ready, ref consumed, ref _state, Size, ref _total);
            }
            catch (Exception)
            {
                state = MimeParserState.Invalid;
            }
            
            remaining = Current.GetDiscardedBoundary();
            part = Current.BodyPart;

            if (state != MimeParserState.BodyPartCompleted)
            {
                Current.BodyPart = new ArraySegment<Byte>(Array.Empty<Byte>());
                return state;
            }

            final = Current.IsFinal;
            Current.ClearAll();
            return state;
        }

        // ReSharper disable once CognitiveComplexity
        protected virtual MimeParserState ParseBodyPart(Byte[] buffer, CurrentBodyPartStore current, Int32 ready, ref Int64 consumed, ref BodyPartState state, Int64 length, ref Int64 total)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (current is null)
            {
                throw new ArgumentNullException(nameof(current));
            }

            Int64 offset = consumed;
            if (ready <= 0 && state == BodyPartState.AfterBoundary && current.IsFinal)
            {
                return MimeParserState.BodyPartCompleted;
            }

            MimeParserState partstate = MimeParserState.DataTooBig;
            Int64 num = length <= 0L ? Int64.MaxValue : length - total + consumed;
            if (num <= 0)
            {
                return MimeParserState.DataTooBig;
            }

            if (ready <= num)
            {
                partstate = MimeParserState.NeedMoreData;
                num = ready;
            }
            
            current.ResetBoundaryOffset();
            switch (state)
            {
                case BodyPartState.BodyPart:
                {
                    while (buffer[consumed] != 13)
                    {
                        if (++consumed == num)
                        {
                            goto end;
                        }
                    }
                    
                    current.AppendBoundary(13);
                    state = BodyPartState.AfterFirstCarriageReturn;
                    if (++consumed == num)
                    {
                        break;
                    }

                    goto case BodyPartState.AfterFirstCarriageReturn;
                }
                case BodyPartState.AfterFirstCarriageReturn:
                {
                    if (buffer[consumed] != 10)
                    {
                        current.ResetBoundary();
                        state = BodyPartState.BodyPart;
                        goto case BodyPartState.BodyPart;
                    }

                    current.AppendBoundary(10);
                    state = BodyPartState.AfterFirstLineFeed;
                    if (++consumed == num)
                    {
                        break;
                    }

                    goto case BodyPartState.AfterFirstLineFeed;
                }
                case BodyPartState.AfterFirstLineFeed:
                {
                    if (buffer[consumed] == 13)
                    {
                        current.ResetBoundary();
                        current.AppendBoundary(13);
                        state = BodyPartState.AfterFirstCarriageReturn;
                        if (++consumed == num)
                        {
                            break;
                        }

                        goto case BodyPartState.AfterFirstCarriageReturn;
                    }

                    if (buffer[consumed] != 45)
                    {
                        current.ResetBoundary();
                        state = BodyPartState.BodyPart;
                        goto case BodyPartState.BodyPart;
                    }

                    current.AppendBoundary(45);
                    state = BodyPartState.AfterFirstDash;
                    if (++consumed == num)
                    {
                        break;
                    }

                    goto case BodyPartState.AfterFirstDash;
                }
                case BodyPartState.AfterFirstDash:
                {
                    if (buffer[consumed] != 45)
                    {
                        current.ResetBoundary();
                        state = BodyPartState.BodyPart;
                        goto case BodyPartState.BodyPart;
                    }

                    current.AppendBoundary(45);
                    state = BodyPartState.Boundary;
                    if (++consumed == num)
                    {
                        break;
                    }

                    goto case BodyPartState.Boundary;
                }
                case BodyPartState.Boundary:
                {
                    Int64 start = consumed;
                    while (buffer[consumed] != 13)
                    {
                        if (++consumed != num)
                        {
                            continue;
                        }

                        if (!current.AppendBoundary(buffer, (Int32) start, (Int32) (consumed - start)))
                        {
                            current.ResetBoundary();
                            state = BodyPartState.BodyPart;
                            goto end;
                        }

                        if (current.IsBoundaryComplete())
                        {
                            state = BodyPartState.AfterBoundary;
                        }

                        goto end;
                    }

                    if (consumed <= start || current.AppendBoundary(buffer, (Int32) start, (Int32) (consumed - start)))
                    {
                        goto case BodyPartState.AfterBoundary;
                    }

                    current.ResetBoundary();
                    state = BodyPartState.BodyPart;
                    goto case BodyPartState.BodyPart;
                }
                case BodyPartState.AfterBoundary:
                {
                    if (buffer[consumed] == 45 && !current.IsFinal)
                    {
                        current.AppendBoundary(45);
                        if (++consumed == num)
                        {
                            state = BodyPartState.AfterSecondDash;
                            break;
                        }
                        goto case BodyPartState.AfterSecondDash;
                    }

                    Int64 start = consumed;
                    while (buffer[consumed] != 13)
                    {
                        if (++consumed != num)
                        {
                            continue;
                        }

                        if (!current.AppendBoundary(buffer, (Int32) start, (Int32) (consumed - start)))
                        {
                            current.ResetBoundary();
                            state = BodyPartState.BodyPart;
                        }

                        goto end;
                    }
                    
                    if (consumed > start && !current.AppendBoundary(buffer, (Int32) start, (Int32) (consumed - start)))
                    {
                        current.ResetBoundary();
                        state = BodyPartState.BodyPart;
                        goto case BodyPartState.BodyPart;
                    }

                    if (buffer[consumed] == 13)
                    {
                        current.AppendBoundary(13);
                        if (++consumed == num)
                        {
                            state = BodyPartState.AfterSecondCarriageReturn;
                            break;
                        }
                        
                        goto case BodyPartState.AfterSecondCarriageReturn;
                    }

                    current.ResetBoundary();
                    state = BodyPartState.BodyPart;
                    goto case BodyPartState.BodyPart;
                }
                case BodyPartState.AfterSecondDash:
                {
                    if (buffer[consumed] != 45)
                    {
                        current.ResetBoundary();
                        state = BodyPartState.BodyPart;
                        goto case BodyPartState.BodyPart;
                    }

                    current.AppendBoundary(45);
                    ++consumed;

                    if (current.IsBoundaryComplete())
                    {
                        state = BodyPartState.AfterBoundary;
                        partstate = MimeParserState.NeedMoreData;
                        break;
                    }

                    current.ResetBoundary();
                    if (consumed == num)
                    {
                        break;
                    }

                    goto case BodyPartState.BodyPart;
                }
                case BodyPartState.AfterSecondCarriageReturn:
                {
                    if (buffer[consumed] != 10)
                    {
                        current.ResetBoundary();
                        state = BodyPartState.BodyPart;
                        goto case BodyPartState.BodyPart;
                    }

                    current.AppendBoundary(10);
                    ++consumed;
                    state = BodyPartState.BodyPart;
                    
                    if (current.IsBoundaryComplete())
                    {
                        partstate = MimeParserState.BodyPartCompleted;
                        break;
                    }
                    
                    current.ResetBoundary();
                    if (consumed == num)
                    {
                        break;
                    }

                    goto case BodyPartState.BodyPart;
                }
            }

            end:
            if (offset < consumed)
            {
                Int32 boundaryDelta = current.BoundaryDelta;
                if (boundaryDelta > 0 && partstate != MimeParserState.BodyPartCompleted)
                {
                    current.HasPotentialBoundaryLeftOver = true;
                }

                Int64 count = consumed - offset - boundaryDelta;
                current.BodyPart = new ArraySegment<Byte>(buffer, (Int32) offset, (Int32) count);
            }
            
            total += consumed - offset;
            return partstate;
        }

        protected enum BodyPartState
        {
            BodyPart,
            AfterFirstCarriageReturn,
            AfterFirstLineFeed,
            AfterFirstDash,
            Boundary,
            AfterBoundary,
            AfterSecondDash,
            AfterSecondCarriageReturn
        }

        protected enum MessageState
        {
            Boundary,
            BodyPart,
            CloseDelimiter
        }

        public enum MimeParserState
        {
            NeedMoreData,
            BodyPartCompleted,
            Invalid,
            DataTooBig
        }

        protected class CurrentBodyPartStore
        {
            protected Byte[] Boundary { get; } = new Byte[256];
            protected Int32 BoundaryLength { get; set; }
            protected Int32 BoundaryOffset { get; set; }

            protected Byte[] ReferenceBoundary { get; } = new Byte[256];
            protected Int32 ReferenceBoundaryLength { get; }

            protected Byte[] BoundaryStore { get; } = new Byte[256];
            protected Int32 BoundaryStoreLength { get; set; }

            protected Boolean ReleaseDiscardedBoundary { get; set; }
            public Boolean HasPotentialBoundaryLeftOver { get; set; }

            public Int32 BoundaryDelta
            {
                get
                {
                    return BoundaryLength - BoundaryOffset <= 0 ? BoundaryLength : BoundaryLength - BoundaryOffset;
                }
            }

            public Boolean IsFirst { get; protected set; } = true;
            public Boolean IsFinal { get; protected set; }

            private ArraySegment<Byte> _bodypart { get; set; } = new ArraySegment<Byte>(Array.Empty<Byte>());
            public ArraySegment<Byte> BodyPart
            {
                get
                {
                    return _bodypart;
                }
                set
                {
                    _bodypart = value;
                }
            }

            public CurrentBodyPartStore(String referenceBoundary)
            {
                ReferenceBoundary[0] = 13;
                ReferenceBoundary[1] = 10;
                ReferenceBoundary[2] = 45;
                ReferenceBoundary[3] = 45;
                ReferenceBoundaryLength = 4 + Encoding.UTF8.GetBytes(referenceBoundary, 0, referenceBoundary.Length, ReferenceBoundary, 4);
                Boundary[0] = 13;
                Boundary[1] = 10;
                BoundaryLength = 2;
            }

            public void ResetBoundary()
            {
                if (HasPotentialBoundaryLeftOver)
                {
                    Buffer.BlockCopy(Boundary, 0, BoundaryStore, 0, BoundaryOffset);
                    BoundaryStoreLength = BoundaryOffset;
                    HasPotentialBoundaryLeftOver = false;
                    ReleaseDiscardedBoundary = true;
                }
                
                BoundaryLength = 0;
                BoundaryOffset = 0;
            }

            public void ResetBoundaryOffset()
            {
                BoundaryOffset = BoundaryLength;
            }

            public void AppendBoundary(Byte data)
            {
                Boundary[BoundaryLength++] = data;
            }

            public Boolean AppendBoundary(Byte[] data, Int32 offset, Int32 count)
            {
                if (data is null)
                {
                    throw new ArgumentNullException(nameof(data));
                }

                if (BoundaryLength + count > ReferenceBoundaryLength + 6)
                {
                    return false;
                }

                Int32 length = BoundaryLength;
                Buffer.BlockCopy(data, offset, Boundary, BoundaryLength, count);
                BoundaryLength += count;
                
                for (Int32 index = Math.Min(BoundaryLength, ReferenceBoundaryLength); length < index; ++length)
                {
                    if (Boundary[length] != ReferenceBoundary[length])
                    {
                        return false;
                    }
                }
                
                return true;
            }

            public ArraySegment<Byte> GetDiscardedBoundary()
            {
                if (BoundaryStoreLength <= 0 || !ReleaseDiscardedBoundary)
                {
                    return new ArraySegment<Byte>(Array.Empty<Byte>());
                }

                ArraySegment<Byte> boundary = new ArraySegment<Byte>(BoundaryStore, 0, BoundaryStoreLength);
                BoundaryStoreLength = 0;
                return boundary;
            }

            public Boolean ValidateBoundary()
            {
                Int32 position = 0;
                
                if (IsFirst)
                {
                    position = 2;
                }

                Int32 index;
                for (index = position; index < ReferenceBoundaryLength; ++index)
                {
                    if (Boundary[index] != ReferenceBoundary[index])
                    {
                        return false;
                    }
                }
                
                Boolean flag = false;
                if (Boundary[index] == 45 && Boundary[index + 1] == 45)
                {
                    flag = true;
                    index += 2;
                }
                
                for (; index < BoundaryLength - 2; ++index)
                {
                    if (Boundary[index] != 32 && Boundary[index] != 9)
                    {
                        return false;
                    }
                }

                IsFirst = false;
                IsFinal = flag;
                return true;
            }

            public Boolean IsBoundaryComplete()
            {
                return ValidateBoundary() && BoundaryLength >= ReferenceBoundaryLength && (BoundaryLength != ReferenceBoundaryLength + 1 || Boundary[ReferenceBoundaryLength] != 45);
            }

            public void ClearAll()
            {
                BoundaryLength = 0;
                BoundaryOffset = 0;
                BoundaryStoreLength = 0;
                ReleaseDiscardedBoundary = false;
                HasPotentialBoundaryLeftOver = false;
                IsFinal = false;
                BodyPart = new ArraySegment<Byte>(Array.Empty<Byte>());
            }
        }
    }
}