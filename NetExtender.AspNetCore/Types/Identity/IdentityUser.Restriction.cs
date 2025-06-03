using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using NetExtender.AspNetCore.Identity.Interfaces;

namespace NetExtender.AspNetCore.Identity
{
    public static partial class IdentityUser
    {
        public readonly struct UserRestriction : IEquatable<IUserInfo>, IEquatable<Restriction>, IEquatable<UserRestriction>
        {
            public static implicit operator Restriction(UserRestriction value)
            {
                return new Restriction(value.Unsafe);
            }

            public static Boolean operator ==(UserRestriction first, UserRestriction second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(UserRestriction first, UserRestriction second)
            {
                return !(first == second);
            }

            public static Boolean operator ==(UserRestriction first, Restriction second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(UserRestriction first, Restriction second)
            {
                return !(first == second);
            }

            internal IUnsafeUserInfo Unsafe { get; }

            internal UserRestriction(IUnsafeUserInfo @unsafe)
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
                    UserRestriction value => Equals(value.Unsafe),
                    Restriction value => Equals(value.Unsafe),
                    IUserInfo value => Equals(value),
                    _ => false
                };
            }

            public Boolean Equals(IUserInfo? other)
            {
                return Equals(Unsafe, other);
            }

            public Boolean Equals(Restriction other)
            {
                return Equals(other.Unsafe);
            }

            public Boolean Equals(UserRestriction other)
            {
                return Equals(other.Unsafe);
            }

            public override String? ToString()
            {
                return Unsafe?.ToString();
            }
        }

        public readonly struct Restriction : IEquatable<IUserInfo>, IEquatable<Restriction>, IEquatable<UserRestriction>
        {
            public static Boolean operator ==(Restriction first, Restriction second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(Restriction first, Restriction second)
            {
                return !(first == second);
            }

            public static Boolean operator ==(Restriction first, UserRestriction second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(Restriction first, UserRestriction second)
            {
                return !(first == second);
            }

            internal IUnsafeUserInfo Unsafe { get; }

            internal Restriction(IUnsafeUserInfo @unsafe)
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
                    Restriction value => Equals(value.Unsafe),
                    UserRestriction value => Equals(value.Unsafe),
                    IUserInfo value => Equals(value),
                    _ => false
                };
            }

            public Boolean Equals(IUserInfo? other)
            {
                return Equals(Unsafe, other);
            }

            public Boolean Equals(Restriction other)
            {
                return Equals(other.Unsafe);
            }

            public Boolean Equals(UserRestriction other)
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