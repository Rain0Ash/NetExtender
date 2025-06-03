// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.JWT
{
    public record JWTValidationInfo
    {
        public static JWTValidationInfo None
        {
            get
            {
                return new JWTValidationInfo
                {
                    TimeMargin = 0,
                    ValidateSignature = false,
                    ValidateExpirationTime = false,
                    ValidateNotBefore = false
                };
            }
        }

        public static JWTValidationInfo Default
        {
            get
            {
                return new JWTValidationInfo();
            }
        }

        public Int32 TimeMargin { get; set; }
        public Boolean ValidateSignature { get; set; } = true;
        public Boolean ValidateExpirationTime { get; set; } = true;
        public Boolean ValidateNotBefore { get; set; } = true;
    }
}
