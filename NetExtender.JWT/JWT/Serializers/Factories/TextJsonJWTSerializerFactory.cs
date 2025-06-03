// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.JWT.Interfaces;

namespace NetExtender.JWT
{
    public class TextJsonJWTSerializerFactory : JWTSerializerFactory<TextJsonJWTSerializer>
    {
        public static IJWTSerializerFactory Default { get; } = new Factory();

        public sealed override JWTSerializerType Type
        {
            get
            {
                return JWTSerializerType.TextJson;
            }
        }

        public TextJsonJWTSerializerFactory()
            : this(new TextJsonJWTSerializer())
        {
        }

        public TextJsonJWTSerializerFactory(TextJsonJWTSerializer serializer)
            : base(serializer)
        {
        }

        public class Factory : TextJsonJWTSerializerFactory
        {
            public Factory()
            {
            }

            public Factory(TextJsonJWTSerializer serializer)
                : base(serializer)
            {
            }
        }
    }
}