using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.AspNetCore.Identity.Attributes
{
    public sealed class IdentityAttributeCollection : IReadOnlyList<IdentityAttribute>
    {
        [Flags]
        private enum Identity : Byte
        {
            None = 0,
            No = 1,
            Authorize = 2,
            Any = 3,
            Custom = 4
        }
        
        private ImmutableArray<IdentityAttribute> Attributes { get; }

        public Int32 Count
        {
            get
            {
                return Attributes.Length;
            }
        }

        private readonly Identity _identity;
        private Identity State
        {
            get
            {
                return _identity;
            }
            init
            {
                _identity = value;
            }
        }
        
        public Boolean IsNoIdentity
        {
            get
            {
                return _identity is Identity.None || _identity.HasFlag(Identity.No);
            }
        }

        public Boolean IsAuthorizeIdentity
        {
            get
            {
                return _identity.HasFlag(Identity.Authorize);
            }
        }

        public Boolean IsAll
        {
            get
            {
                return _identity.HasFlag(Identity.Any) && !_identity.HasFlag(Identity.Custom);
            }
        }

        public Boolean IsCustom
        {
            get
            {
                return _identity.HasFlag(Identity.Custom);
            }
        }

        public IdentityAttributeCollection(params IdentityAttribute?[]? attributes)
            : this((IEnumerable<IdentityAttribute?>?) attributes)
        {
        }

        public IdentityAttributeCollection(IEnumerable<IdentityAttribute?>? attributes)
        {
            Attributes = Initialize(attributes, out _identity);
        }

        private static ImmutableArray<IdentityAttribute> Initialize(IEnumerable<IdentityAttribute?>? attributes, out Identity identity)
        {
            identity = Identity.None;
            
            if (attributes is null)
            {
                return ImmutableArray<IdentityAttribute>.Empty;
            }
            
            List<IdentityAttribute> result = new List<IdentityAttribute>(attributes.CountIfMaterialized(16));
            
            foreach (IdentityAttribute? attribute in attributes)
            {
                switch (attribute)
                {
                    case null:
                        continue;
                    case NoIdentityAttribute:
                        identity |= Identity.No;
                        continue;
                    case AuthorizeIdentityAttribute:
                        identity |= Identity.Authorize;
                        continue;
                    case AnyIdentityAttribute:
                        identity |= Identity.Any;
                        continue;
                    default:
                        identity |= Identity.Custom;
                        result.Add(attribute);
                        continue;
                }
            }
            
            return result.ToImmutableArray();
        }

        [return: NotNullIfNotNull("type")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IdentityAttributeCollection? From(Type? type)
        {
            return type is not null ? new IdentityAttributeCollection(type.GetCustomAttributes<IdentityAttribute>()) : null;
        }

        [return: NotNullIfNotNull("method")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IdentityAttributeCollection? From(MethodInfo? method)
        {
            return method is not null ? new IdentityAttributeCollection(method.GetCustomAttributes<IdentityAttribute>()) : null;
        }

        public IdentityAttributeCollection FromController(IEnumerable<IdentityAttribute?>? controller)
        {
            return FromController(controller is not null ? new IdentityAttributeCollection(controller) : null);
        }

        public IdentityAttributeCollection FromController(IdentityAttributeCollection? controller)
        {
            return controller is not null ? new IdentityAttributeCollection(controller.Attributes.Concat(Attributes)) { State = controller.State | State } : this;
        }

        public Boolean IsAllow<T>(T? identity) where T : unmanaged, Enum
        {
            Boolean? result = null;
            foreach (IdentityAttribute attribute in Attributes)
            {
                if (!attribute.IsAllow<T>())
                {
                    continue;
                }

                if (attribute.IsStrict && attribute.IsDeny(identity))
                {
                    return false;
                }

                if (attribute.IsAllow(identity))
                {
                    result = true;
                }
            }

            return result ?? _identity switch
            {
                Identity.None => false,
                Identity.No => identity is null,
                Identity.Authorize => identity is not null,
                Identity.Any => true,
                Identity.Custom => false,
                Identity.Custom | Identity.No => identity is null,
                Identity.Custom | Identity.Authorize => identity is not null,
                Identity.Custom | Identity.Any => true,
                _ => throw new EnumUndefinedOrNotSupportedException<Identity>(_identity, nameof(_identity), null)
            };
        }

        public Boolean IsDeny<T>(T? identity) where T : unmanaged, Enum
        {
            Boolean? result = null;
            foreach (IdentityAttribute attribute in Attributes)
            {
                if (!attribute.IsAllow<T>())
                {
                    continue;
                }

                if (attribute.IsStrict && attribute.IsDeny(identity))
                {
                    return true;
                }

                if (attribute.IsAllow(identity))
                {
                    result = false;
                }
            }
            
            return result ?? _identity switch
            {
                Identity.None => true,
                Identity.No => identity is not null,
                Identity.Authorize => identity is null,
                Identity.Any => false,
                Identity.Custom => true,
                Identity.Custom | Identity.No => identity is not null,
                Identity.Custom | Identity.Authorize => identity is null,
                Identity.Custom | Identity.Any => false,
                _ => throw new EnumUndefinedOrNotSupportedException<Identity>(_identity, nameof(_identity), null)
            };
        }

        public ImmutableArray<IdentityAttribute>.Enumerator GetEnumerator()
        {
            return Attributes.GetEnumerator();
        }

        IEnumerator<IdentityAttribute> IEnumerable<IdentityAttribute>.GetEnumerator()
        {
            return ((IEnumerable<IdentityAttribute>) Attributes).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Attributes).GetEnumerator();
        }

        public IdentityAttribute this[Int32 index]
        {
            get
            {
                return Attributes[index];
            }
        }
    }
}