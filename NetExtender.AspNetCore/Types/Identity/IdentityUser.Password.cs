using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.AspNetCore.Identity.Interfaces;

namespace NetExtender.AspNetCore.Identity
{
    public static partial class IdentityUser
    {
        public readonly struct UserPassword : IEquatable<IUserInfo>, IEquatable<Password>, IEquatable<UserPassword>
        {
            public static implicit operator Password(UserPassword value)
            {
                return new Password(value.Unsafe);
            }

            public static Boolean operator ==(UserPassword first, UserPassword second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(UserPassword first, UserPassword second)
            {
                return !(first == second);
            }

            public static Boolean operator ==(UserPassword first, Password second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(UserPassword first, Password second)
            {
                return !(first == second);
            }

            internal IUnsafeUserInfo Unsafe { get; }

            internal UserPassword(IUnsafeUserInfo @unsafe)
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
                    UserPassword value => Equals(value.Unsafe),
                    Password value => Equals(value.Unsafe),
                    IUserInfo value => Equals(value),
                    _ => false
                };
            }

            public Boolean Equals(IUserInfo? other)
            {
                return Equals(Unsafe, other);
            }

            public Boolean Equals(Password other)
            {
                return Equals(other.Unsafe);
            }

            public Boolean Equals(UserPassword other)
            {
                return Equals(other.Unsafe);
            }

            public override String? ToString()
            {
                return Unsafe?.ToString();
            }
        }

        public readonly struct Password : IEquatable<IUserInfo>, IEquatable<Password>, IEquatable<UserPassword>
        {
            public static Boolean operator ==(Password first, Password second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(Password first, Password second)
            {
                return !(first == second);
            }

            public static Boolean operator ==(Password first, UserPassword second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(Password first, UserPassword second)
            {
                return !(first == second);
            }

            internal IUnsafeUserInfo Unsafe { get; }

            internal Password(IUnsafeUserInfo @unsafe)
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
                    Password value => Equals(value.Unsafe),
                    UserPassword value => Equals(value.Unsafe),
                    IUserInfo value => Equals(value),
                    _ => false
                };
            }

            public Boolean Equals(IUserInfo? other)
            {
                return Equals(Unsafe, other);
            }

            public Boolean Equals(Password other)
            {
                return Equals(other.Unsafe);
            }

            public Boolean Equals(UserPassword other)
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