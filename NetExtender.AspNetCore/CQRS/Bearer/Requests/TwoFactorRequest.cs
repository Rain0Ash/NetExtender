// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if !NET8_0_OR_GREATER
using System;

namespace Microsoft.AspNetCore.Identity.Data
{
    public record TwoFactorRequest
    {
        public Boolean? Enable { get; init; }
        public String? TwoFactorCode { get; init; }
        public Boolean ResetSharedKey { get; init; }
        public Boolean ResetRecoveryCodes { get; init; }
        public Boolean ForgetMachine { get; init; }
    }
}
#endif