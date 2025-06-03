// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if !NET8_0_OR_GREATER
using System;

namespace Microsoft.AspNetCore.Identity.Data
{
    public record ResetPasswordRequest
    {
        private readonly String _email = String.Empty;
        public String Email
        {
            get
            {
                return _email;
            }
            init
            {
                _email = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        private readonly String _code = String.Empty;
        public String ResetCode
        {
            get
            {
                return _code;
            }
            init
            {
                _code = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        private readonly String _password = String.Empty;
        public String NewPassword
        {
            get
            {
                return _password;
            }
            init
            {
                _password = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        internal ResetPasswordRequest()
            : this(String.Empty, String.Empty, String.Empty)
        {
        }
        
        public ResetPasswordRequest(String email, String code, String password)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            ResetCode = code ?? throw new ArgumentNullException(nameof(code));
            NewPassword = password ?? throw new ArgumentNullException(nameof(password));
        }
    }
}
#endif