// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.ComponentModel;

namespace NetExtender.JWT
{
    /// <summary>IANA JWT</summary>
    /// <remarks>https://www.iana.org/assignments/jwt/jwt.xhtml</remarks>
    public enum JWTClaim
    {
        [Description("iss")]
        Issuer,

        [Description("sub")]
        Subject,

        [Description("aud")]
        Audience,

        [Description("exp")]
        ExpirationTime,

        [Description("nbf")]
        NotBefore,

        [Description("iat")]
        IssuedAt,

        [Description("jti")]
        JwtId,

        [Description("name")]
        FullName,

        [Description("given_name")]
        GivenName,

        [Description("family_name")]
        FamilyName,

        [Description("middle_name")]
        MiddleName,

        [Description("nickname")]
        CasualName,

        [Description("preferred_username")]
        PreferredUsername,

        [Description("profile")]
        ProfilePageUrl,

        [Description("picture")]
        ProfilePictureUrl,

        [Description("website")]
        Website,

        [Description("email")]
        PreferredEmail,

        [Description("email_verified")]
        VerifiedEmail,

        [Description("gender")]
        Gender,

        [Description("birthdate")]
        Birthday,

        [Description("zoneinfo")]
        TimeZone,

        [Description("locale")]
        Locale,

        [Description("phone_number")]
        PreferredPhoneNumber,

        [Description("phone_number_verified")]
        VerifiedPhoneNumber,

        [Description("address")]
        Address,

        [Description("update_at")]
        UpdatedAt,

        [Description("azp")]
        AuthorizedParty,

        [Description("nonce")]
        Nonce,

        [Description("auth_time")]
        AuthenticationTime,

        [Description("at_hash")]
        AccessTokenHash,

        [Description("c_hash")]
        CodeHashValue,

        [Description("acr")]
        Acr,

        [Description("amr")]
        Amr,

        [Description("sub_jwk")]
        PublicKey,

        [Description("cnf")]
        Confirmation,

        [Description("sip_from_tag")]
        SipFromTag,

        [Description("sip_date")]
        SipDate,

        [Description("sip_callid")]
        SipCallId,

        [Description("sip_cseq_num")]
        SipCseqNumber,

        [Description("sip_via_branch")]
        SipViaBranch,

        [Description("orig")]
        OriginatingIdentityString,

        [Description("dest")]
        DestinationIdentityString,

        [Description("mky")]
        MediaKeyFingerprintString
    }
}