using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using NetExtender.AspNetCore.Identity.Interfaces;

namespace NetExtender.AspNetCore.Identity
{
    public static partial class IdentityUser
    {
        public readonly struct UserConfirmation : IEquatable<IUserInfo>, IEquatable<Confirmation>, IEquatable<UserConfirmation>
        {
            public static implicit operator Confirmation(UserConfirmation value)
            {
                return new Confirmation(value.Unsafe);
            }

            public static Boolean operator ==(UserConfirmation first, UserConfirmation second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(UserConfirmation first, UserConfirmation second)
            {
                return !(first == second);
            }

            public static Boolean operator ==(UserConfirmation first, Confirmation second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(UserConfirmation first, Confirmation second)
            {
                return !(first == second);
            }

            internal IUnsafeUserInfo Unsafe { get; }

            [PersonalData]
            public Boolean User
            {
                get
                {
                    return Unsafe.IsConfirm;
                }
                set
                {
                    Unsafe.IsConfirm = value;
                }
            }

            [PersonalData]
            public Boolean Email
            {
                get
                {
                    return Unsafe.EmailConfirmed;
                }
                set
                {
                    Unsafe.EmailConfirmed = value;
                }
            }

            [PersonalData]
            public Boolean Phone
            {
                get
                {
                    return Unsafe.PhoneNumberConfirmed;
                }
                set
                {
                    Unsafe.PhoneNumberConfirmed = value;
                }
            }

            [PersonalData]
            public Boolean TwoFactor
            {
                get
                {
                    return Unsafe.TwoFactorEnabled;
                }
                set
                {
                    Unsafe.TwoFactorEnabled = value;
                }
            }

            internal UserConfirmation(IUnsafeUserInfo @unsafe)
            {
                Unsafe = @unsafe;
            }

            public override Int32 GetHashCode()
            {
                return Unsafe?.GetHashCode() ?? 0;
            }

            public override Boolean Equals(Object? other)
            {
                return other switch
                {
                    null => Unsafe is null,
                    UserConfirmation value => Equals(value.Unsafe),
                    Confirmation value => Equals(value.Unsafe),
                    IUserInfo value => Equals(value),
                    _ => false
                };
            }

            public Boolean Equals(IUserInfo? other)
            {
                return Equals(Unsafe, other);
            }

            public Boolean Equals(Confirmation other)
            {
                return Equals(other.Unsafe);
            }

            public Boolean Equals(UserConfirmation other)
            {
                return Equals(other.Unsafe);
            }

            public override String? ToString()
            {
                return Unsafe?.ToString();
            }
        }

        public readonly struct Confirmation : IEquatable<IUserInfo>, IEquatable<Confirmation>, IEquatable<UserConfirmation>
        {
            public static Boolean operator ==(Confirmation first, Confirmation second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(Confirmation first, Confirmation second)
            {
                return !(first == second);
            }

            public static Boolean operator ==(Confirmation first, UserConfirmation second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(Confirmation first, UserConfirmation second)
            {
                return !(first == second);
            }

            internal IUnsafeUserInfo Unsafe { get; }

            [PersonalData]
            public Boolean User
            {
                get
                {
                    return Unsafe.IsConfirm;
                }
            }

            [PersonalData]
            public Boolean Email
            {
                get
                {
                    return Unsafe.EmailConfirmed;
                }
            }

            [PersonalData]
            public Boolean Phone
            {
                get
                {
                    return Unsafe.PhoneNumberConfirmed;
                }
            }

            [PersonalData]
            public Boolean TwoFactor
            {
                get
                {
                    return Unsafe.TwoFactorEnabled;
                }
            }

            internal Confirmation(IUnsafeUserInfo @unsafe)
            {
                Unsafe = @unsafe;
            }

            public override Int32 GetHashCode()
            {
                return Unsafe?.GetHashCode() ?? 0;
            }

            public override Boolean Equals(Object? other)
            {
                return other switch
                {
                    null => Unsafe is null,
                    Confirmation value => Equals(value.Unsafe),
                    UserConfirmation value => Equals(value.Unsafe),
                    IUserInfo value => Equals(value),
                    _ => false
                };
            }

            public Boolean Equals(IUserInfo? other)
            {
                return Equals(Unsafe, other);
            }

            public Boolean Equals(Confirmation other)
            {
                return Equals(other.Unsafe);
            }

            public Boolean Equals(UserConfirmation other)
            {
                return Equals(other.Unsafe);
            }

            public override String? ToString()
            {
                return Unsafe?.ToString();
            }
        }
    }
}