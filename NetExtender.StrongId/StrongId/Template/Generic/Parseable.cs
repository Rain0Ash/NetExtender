#IF StrongIdParseType.ParseSpan

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static STRONGID Parse(System.ReadOnlySpan<System.Char> value)
        {
            return new STRONGID(TYPE.Parse(value, NUMBERSTYLE, null));
        }
#ENDIF
#IF StrongIdParseType.ParseSpanDirect

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static STRONGID Parse(System.ReadOnlySpan<System.Char> value)
        {
            return new STRONGID(TYPE.Parse(value));
        }
#ENDIF
#IF StrongIdParseType.ParseSpanProvider

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static STRONGID Parse(System.ReadOnlySpan<System.Char> value, System.IFormatProvider? provider)
        {
            return new STRONGID(TYPE.Parse(value, NUMBERSTYLE, provider));
        }
#ENDIF
#IF StrongIdParseType.ParseSpanProviderDirect

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static STRONGID Parse(System.ReadOnlySpan<System.Char> value, System.IFormatProvider? provider)
        {
            return new STRONGID(TYPE.Parse(value, provider));
        }
#ENDIF
#IF StrongIdParseType.ParseSpanProvider

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static STRONGID Parse(System.ReadOnlySpan<System.Char> value, System.Globalization.NumberStyles style)
        {
            return new STRONGID(TYPE.Parse(value, style, null));
        }
#ENDIF
#IF StrongIdParseType.ParseSpanProviderDirect

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static STRONGID Parse(System.ReadOnlySpan<System.Char> value, System.Globalization.NumberStyles style)
        {
            return new STRONGID(TYPE.Parse(value, style));
        }
#ENDIF
#IF StrongIdParseType.ParseSpanProvider

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static STRONGID Parse(System.ReadOnlySpan<System.Char> value, System.Globalization.NumberStyles style, System.IFormatProvider? provider)
        {
            return new STRONGID(TYPE.Parse(value, style, provider));
        }
#ENDIF
#IF StrongIdParseType.ParseSpanProviderDirect

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static STRONGID Parse(System.ReadOnlySpan<System.Char> value, System.Globalization.NumberStyles style, System.IFormatProvider? provider)
        {
            return new STRONGID(TYPE.Parse(value, style, provider));
        }
#ENDIF
#IF StrongIdParseType.ParseString

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static STRONGID Parse(System.String value)
        {
            return new STRONGID(TYPE.Parse(value, NUMBERSTYLE, null));
        }
#ENDIF
#IF StrongIdParseType.ParseStringDirect

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static STRONGID Parse(System.String value)
        {
            return new STRONGID(TYPE.Parse(value));
        }
#ENDIF
#IF StrongIdParseType.ParseStringProvider

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static STRONGID Parse(System.String value, System.IFormatProvider? provider)
        {
            return new STRONGID(TYPE.Parse(value, NUMBERSTYLE, provider));
        }
#ENDIF
#IF StrongIdParseType.ParseStringProviderDirect

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static STRONGID Parse(System.String value, System.IFormatProvider? provider)
        {
            return new STRONGID(TYPE.Parse(value, provider));
        }
#ENDIF
#IF StrongIdParseType.ParseStringProvider

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static STRONGID Parse(System.String value, System.Globalization.NumberStyles style)
        {
            return new STRONGID(TYPE.Parse(value, style, null));
        }
#ENDIF
#IF StrongIdParseType.ParseStringProviderDirect

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static STRONGID Parse(System.String value, System.Globalization.NumberStyles style)
        {
            return new STRONGID(TYPE.Parse(value, style));
        }
#ENDIF
#IF StrongIdParseType.ParseStringProvider

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static STRONGID Parse(System.String value, System.Globalization.NumberStyles style, System.IFormatProvider? provider)
        {
            return new STRONGID(TYPE.Parse(value, style, provider));
        }
#ENDIF
#IF StrongIdParseType.ParseStringProviderDirect

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static STRONGID Parse(System.String value, System.Globalization.NumberStyles style, System.IFormatProvider? provider)
        {
            return new STRONGID(TYPE.Parse(value, style, provider));
        }
#ENDIF
#IF StrongIdParseType.TryParseSpan

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static System.Boolean TryParse(System.ReadOnlySpan<System.Char> value, out STRONGID result)
        {
            if (!TYPE.TryParse(value, NUMBERSTYLE, null, out TYPE convert))
            {
                result = default;
                return false;
            }

            result = new STRONGID(convert);
            return true;
        }
#ENDIF
#IF StrongIdParseType.TryParseSpanDirect

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static System.Boolean TryParse(System.ReadOnlySpan<System.Char> value, out STRONGID result)
        {
            if (!TYPE.TryParse(value, out TYPE convert))
            {
                result = default;
                return false;
            }

            result = new STRONGID(convert);
            return true;
        }
#ENDIF
#IF StrongIdParseType.TryParseSpanProvider

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static System.Boolean TryParse(System.ReadOnlySpan<System.Char> value, System.IFormatProvider? provider, out STRONGID result)
        {
            if (!TYPE.TryParse(value, NUMBERSTYLE, provider, out TYPE convert))
            {
                result = default;
                return false;
            }

            result = new STRONGID(convert);
            return true;
        }
#ENDIF
#IF StrongIdParseType.TryParseSpanProviderDirect

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static System.Boolean TryParse(System.ReadOnlySpan<System.Char> value, System.IFormatProvider? provider, out STRONGID result)
        {
            if (!TYPE.TryParse(value, provider, out TYPE convert))
            {
                result = default;
                return false;
            }

            result = new STRONGID(convert);
            return true;
        }
#ENDIF
#IF StrongIdParseType.TryParseSpanNumber

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static System.Boolean TryParse(System.ReadOnlySpan<System.Char> value, System.Globalization.NumberStyles style, out STRONGID result)
        {
            if (!TYPE.TryParse(value, style, null, out TYPE convert))
            {
                result = default;
                return false;
            }

            result = new STRONGID(convert);
            return true;
        }
#ENDIF
#IF StrongIdParseType.TryParseSpanNumberDirect

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static System.Boolean TryParse(System.ReadOnlySpan<System.Char> value, System.Globalization.NumberStyles style, out STRONGID result)
        {
            if (!TYPE.TryParse(value, style, out TYPE convert))
            {
                result = default;
                return false;
            }

            result = new STRONGID(convert);
            return true;
        }
#ENDIF
#IF StrongIdParseType.TryParseSpanNumberProvider

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static System.Boolean TryParse(System.ReadOnlySpan<System.Char> value, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out STRONGID result)
        {
            if (!TYPE.TryParse(value, style, provider, out TYPE convert))
            {
                result = default;
                return false;
            }

            result = new STRONGID(convert);
            return true;
        }
#ENDIF
#IF StrongIdParseType.TryParseSpanNumberProviderDirect

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static System.Boolean TryParse(System.ReadOnlySpan<System.Char> value, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out STRONGID result)
        {
            if (!TYPE.TryParse(value, style, provider, out TYPE convert))
            {
                result = default;
                return false;
            }

            result = new STRONGID(convert);
            return true;
        }
#ENDIF
#IF StrongIdParseType.TryParseString

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static System.Boolean TryParse(System.String value, out STRONGID result)
        {
            if (!TYPE.TryParse(value, NUMBERSTYLE, null, out TYPE convert))
            {
                result = default;
                return false;
            }

            result = new STRONGID(convert);
            return true;
        }
#ENDIF
#IF StrongIdParseType.TryParseStringDirect

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static System.Boolean TryParse(System.String value, out STRONGID result)
        {
            if (!TYPE.TryParse(value, out TYPE convert))
            {
                result = default;
                return false;
            }

            result = new STRONGID(convert);
            return true;
        }
#ENDIF
#IF StrongIdParseType.TryParseStringProvider

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static System.Boolean TryParse(System.String value, System.IFormatProvider? provider, out STRONGID result)
        {
            if (!TYPE.TryParse(value, NUMBERSTYLE, provider, out TYPE convert))
            {
                result = default;
                return false;
            }

            result = new STRONGID(convert);
            return true;
        }
#ENDIF
#IF StrongIdParseType.TryParseStringProviderDirect

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static System.Boolean TryParse(System.String value, System.IFormatProvider? provider, out STRONGID result)
        {
            if (!TYPE.TryParse(value, provider, out TYPE convert))
            {
                result = default;
                return false;
            }

            result = new STRONGID(convert);
            return true;
        }
#ENDIF
#IF StrongIdParseType.TryParseStringNumber

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static System.Boolean TryParse(System.String value, System.Globalization.NumberStyles style, out STRONGID result)
        {
            if (!TYPE.TryParse(value, style, null, out TYPE convert))
            {
                result = default;
                return false;
            }

            result = new STRONGID(convert);
            return true;
        }
#ENDIF
#IF StrongIdParseType.TryParseStringNumberDirect

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static System.Boolean TryParse(System.String value, System.Globalization.NumberStyles style, out STRONGID result)
        {
            if (!TYPE.TryParse(value, style, out TYPE convert))
            {
                result = default;
                return false;
            }

            result = new STRONGID(convert);
            return true;
        }
#ENDIF
#IF StrongIdParseType.TryParseStringNumberProvider

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static System.Boolean TryParse(System.String value, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out STRONGID result)
        {
            if (!TYPE.TryParse(value, style, provider, out TYPE convert))
            {
                result = default;
                return false;
            }

            result = new STRONGID(convert);
            return true;
        }
#ENDIF
#IF StrongIdParseType.TryParseStringNumberProviderDirect

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static System.Boolean TryParse(System.String value, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out STRONGID result)
        {
            if (!TYPE.TryParse(value, style, provider, out TYPE convert))
            {
                result = default;
                return false;
            }

            result = new STRONGID(convert);
            return true;
        }
#ENDIF