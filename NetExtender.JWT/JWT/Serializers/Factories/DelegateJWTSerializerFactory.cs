// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.JWT.Interfaces;

namespace NetExtender.JWT
{
    public sealed class DelegateJWTSerializerFactory : JWTSerializerFactory
    {
        private Func<IJWTSerializer> Factory { get; }

        public DelegateJWTSerializerFactory(IJWTSerializer serializer) :
            this(serializer is not null ? () => serializer : throw new ArgumentNullException(nameof(serializer)))
        {
        }

        public DelegateJWTSerializerFactory(IJWTSerializerFactory factory) :
            this(factory is not null ? factory.Create : throw new ArgumentNullException(nameof(factory)))
        {
        }

        public DelegateJWTSerializerFactory(Func<IJWTSerializer> factory)
        {
            Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public override IJWTSerializer Create()
        {
            return Factory.Invoke();
        }
    }
    
    public sealed class InstanceJWTSerializerFactory : JWTSerializerFactory
    {
        private IJWTSerializer Serializer { get; }

        public InstanceJWTSerializerFactory(IJWTSerializer serializer)
        {
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public override IJWTSerializer Create()
        {
            return Serializer;
        }
    }
}