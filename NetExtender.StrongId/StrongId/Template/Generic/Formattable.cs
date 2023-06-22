#IF StrongIdFormatType.Provider

        public System.String ToString(System.IFormatProvider? provider)
        {
            return Value.ToString(null, provider);
        }
#ENDIF
#IF StrongIdFormatType.ProviderDirect

        public System.String ToString(System.IFormatProvider? provider)
        {
            return Value.ToString(provider);
        }
#ENDIF
#IF StrongIdFormatType.Format

        public System.String ToString(System.String? format)
        {
            return Value.ToString(format, null);
        }
#ENDIF
#IF StrongIdFormatType.FormatDirect

        public System.String ToString(System.String? format)
        {
            return Value.ToString(format);
        }
#ENDIF
#IF StrongIdFormatType.FormatProvider

        public System.String ToString(System.String? format, System.IFormatProvider? provider)
        {
            return Value.ToString(format, provider);
        }
#ENDIF
#IF StrongIdFormatType.FormatProviderDirect

        public System.String ToString(System.String? format, System.IFormatProvider? provider)
        {
            return Value.ToString(format, provider);
        }
#ENDIF