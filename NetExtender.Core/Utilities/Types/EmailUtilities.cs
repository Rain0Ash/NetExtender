// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net.Mail;

namespace NetExtender.Utilities.Types
{
    public static class EmailUtilities
    {
        public static Boolean CheckValidEmail(String address)
        {
            return MailAddress.TryCreate(address, out _);
        }
    }
}