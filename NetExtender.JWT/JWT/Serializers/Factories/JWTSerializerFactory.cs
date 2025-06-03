// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.JWT.Interfaces;

namespace NetExtender.JWT
{
    public class JWTSerializerInstanceFactory<T> : JWTSerializerFactory<T> where T : class, IJWTSerializer, new()
    {
        public JWTSerializerInstanceFactory()
            : this(new T())
        {
        }
        
        public JWTSerializerInstanceFactory(T serializer)
            : base(serializer)
        {
        }
    }

    public class JWTSerializerFactory<T> : JWTSerializerFactory where T : class, IJWTSerializer
    {
        protected T Serializer { get; }

        public override JWTSerializerType Type
        {
            get
            {
                return Serializer.Type;
            }
        }

        public JWTSerializerFactory(T serializer)
        {
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public sealed override IJWTSerializer Create()
        {
            return Serializer;
        }
    }

    public abstract class JWTSerializerFactory : IJWTSerializerFactory
    {
        public virtual JWTSerializerType Type
        {
            get
            {
                return Create().Type;
            }
        }

        public abstract IJWTSerializer Create();
    }
}