#IF StrongIdFormatType.Provider

        public System.String ToString(System.IFormatProvider? provider)
        {
            return Value?.ToString(null, provider) ?? System.String.Empty;
        }
#ENDIF
#IF StrongIdFormatType.ProviderDirect

        public System.String ToString(System.IFormatProvider? provider)
        {
            return Value?.ToString(provider) ?? System.String.Empty;
        }
#ENDIF
#IF StrongIdFormatType.Format

        public System.String ToString(System.String? format)
        {
            return Value?.ToString(format, null) ?? System.String.Empty;
        }
#ENDIF
#IF StrongIdFormatType.FormatDirect

        public System.String ToString(System.String? format)
        {
            return Value?.ToString(format) ?? System.String.Empty;
        }
#ENDIF
#IF StrongIdFormatType.FormatProvider

        public System.String ToString(System.String? format, System.IFormatProvider? provider)
        {
            return Value?.ToString(format, provider) ?? System.String.Empty;
        }
#ENDIF
#IF StrongIdFormatType.FormatProviderDirect

        public System.String ToString(System.String? format, System.IFormatProvider? provider)
        {
            return Value?.ToString(format, provider) ?? System.String.Empty;
        }
#ENDIF