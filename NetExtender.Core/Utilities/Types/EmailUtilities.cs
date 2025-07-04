// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;

namespace NetExtender.Utilities.Types
{
    public static class EmailUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MailAddress? Create(String address)
        {
            return MailAddress.TryCreate(address, out MailAddress? result) ? result : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MailAddress? Create(String address, String? display)
        {
            return MailAddress.TryCreate(address, display, out MailAddress? result) ? result : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MailAddress? Create(String address, Encoding? encoding)
        {
            return Create(address, null, encoding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MailAddress? Create(String address, String? display, Encoding? encoding)
        {
            return MailAddress.TryCreate(address, display, encoding, out MailAddress? result) ? result : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryCreate(String address, [MaybeNullWhen(false)] out MailAddress result)
        {
            return MailAddress.TryCreate(address, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryCreate(String address, String? display, [MaybeNullWhen(false)] out MailAddress result)
        {
            return MailAddress.TryCreate(address, display, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryCreate(String address, Encoding? encoding, [MaybeNullWhen(false)] out MailAddress result)
        {
            return TryCreate(address, null, encoding, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryCreate(String address, String? display, Encoding? encoding, [MaybeNullWhen(false)] out MailAddress result)
        {
            return MailAddress.TryCreate(address, display, encoding, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Validate(String address)
        {
            return MailAddress.TryCreate(address, out _);
        }
    }
}