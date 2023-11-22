// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NetExtender.Types.Network
{
    public class MultipartMemoryStreamProvider : MultipartStreamProvider
    {
        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (headers is null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            return new MemoryStream();
        }
    }
}