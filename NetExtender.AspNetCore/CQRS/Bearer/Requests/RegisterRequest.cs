// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if !NET8_0_OR_GREATER
using System;

namespace Microsoft.AspNetCore.Identity.Data
{
    public record RegisterRequest
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

        private readonly String _password = String.Empty;
        public String Password
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

        internal RegisterRequest()
            : this(String.Empty, String.Empty)
        {
        }
        
        public RegisterRequest(String email, String password)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }
    }
}
#endif