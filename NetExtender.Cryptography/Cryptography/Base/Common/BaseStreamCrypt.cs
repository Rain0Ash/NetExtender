// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.IO;
using System.Threading.Tasks;
using NetExtender.Crypto.Base.Interfaces;

namespace NetExtender.Crypto.Base.Common
{
    public abstract class BaseCryptStream : BaseCrypt, IBaseStreamEncoder
    {
        /// <inheritdoc/>
        public abstract void Encode(Stream input, TextWriter output);

        /// <inheritdoc/>
        public abstract Task EncodeAsync(Stream input, TextWriter output);

        /// <inheritdoc/>
        public abstract void Decode(TextReader input, Stream output);

        /// <inheritdoc/>
        public abstract Task DecodeAsync(TextReader input, Stream output);
    }
}