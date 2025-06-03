// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if !NET8_0_OR_GREATER
using System;
using System.Linq;

namespace Microsoft.AspNetCore.Identity.Data
{
    public record TwoFactorResponse
    {
        private readonly String _key = String.Empty;
        public String SharedKey
        {
            get
            {
                return _key;
            }
            init
            {
                _key = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public Int32 RecoveryCodesLeft { get; init; }

        private readonly String[]? _recovery;
        public String[]? RecoveryCodes
        {
            get
            {
                return _recovery;
            }
            init
            {
                if (value is not null && value.Any(String.IsNullOrWhiteSpace))
                {
                    throw new ArgumentException($"'{nameof(value)}' item cannot be null or whitespace.", nameof(value));
                }
                
                _recovery = value;
            }
        }

        public Boolean IsTwoFactorEnabled { get; init; }
        public Boolean IsMachineRemembered { get; init; }

        internal TwoFactorResponse()
            : this(String.Empty)
        {
        }

        public TwoFactorResponse(String key)
        {
            SharedKey = key ?? throw new ArgumentNullException(nameof(key));
        }
    }
}
#endif