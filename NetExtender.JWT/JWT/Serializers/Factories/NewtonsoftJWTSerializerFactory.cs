// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.JWT.Interfaces;

namespace NetExtender.JWT
{
    public class NewtonsoftJWTSerializerFactory : JWTSerializerFactory<NewtonsoftJWTSerializer>
    {
        public static IJWTSerializerFactory Default { get; } = new Factory();

        public sealed override JWTSerializerType Type
        {
            get
            {
                return JWTSerializerType.Newtonsoft;
            }
        }

        public NewtonsoftJWTSerializerFactory()
            : this(new NewtonsoftJWTSerializer())
        {
        }

        public NewtonsoftJWTSerializerFactory(NewtonsoftJWTSerializer serializer)
            : base(serializer)
        {
        }

        public class Factory : NewtonsoftJWTSerializerFactory
        {
            public Factory()
            {
            }

            public Factory(NewtonsoftJWTSerializer serializer)
                : base(serializer)
            {
            }
        }
    }
}