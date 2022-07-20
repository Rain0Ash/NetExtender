// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// <copyright file="StreamHelper.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

using System;
using System.IO;
using System.Threading.Tasks;
using NetExtender.Utilities.Types;

namespace NetExtender.Cryptography.Base.Common
{
    /// <summary>
    /// Provides Stream functionality to any buffer-based encoding operation.
    /// </summary>
    internal static class StreamHelper
    {
        public static void Encode(Stream input, TextWriter output, Func<ReadOnlyMemory<Byte>, Boolean, String> encode, Int32 bufferSize = BufferUtilities.DefaultBuffer)
        {
            Byte[] buffer = new Byte[bufferSize];
            while (true)
            {
                Int32 bytesRead = input.Read(buffer, 0, bufferSize);
                if (bytesRead < 1)
                {
                    break;
                }

                String result = encode(buffer.AsMemory(0, bytesRead), bytesRead < bufferSize);
                output.Write(result);
            }
        }

        public static async Task EncodeAsync(Stream input, TextWriter output, Func<ReadOnlyMemory<Byte>, Boolean, String> encode, Int32 bufferSize = BufferUtilities.DefaultBuffer)
        {
            Byte[] buffer = new Byte[bufferSize];
            while (true)
            {
                Int32 bytesRead = await input.ReadAsync(buffer.AsMemory(0, bufferSize)).ConfigureAwait(false);
                if (bytesRead < 1)
                {
                    break;
                }

                String result = encode(buffer.AsMemory(0, bytesRead), bytesRead < bufferSize);
                await output.WriteAsync(result).ConfigureAwait(false);
            }
        }

        public static void Decode(
            TextReader input,
            Stream output,
            Func<ReadOnlyMemory<Char>, Memory<Byte>> decodeBufferFunc,
            Int32 bufferSize = BufferUtilities.DefaultBuffer)
        {
            Char[] buffer = new Char[bufferSize];
            while (true)
            {
                Int32 bytesRead = input.Read(buffer, 0, bufferSize);
                if (bytesRead < 1)
                {
                    break;
                }

                Memory<Byte> result = decodeBufferFunc(buffer.AsMemory(0, bytesRead));
                output.Write(result.ToArray(), 0, result.Length);
            }
        }

        public static async Task DecodeAsync(TextReader input, Stream output, Func<ReadOnlyMemory<Char>, Memory<Byte>> decode, Int32 bufferSize = BufferUtilities.DefaultBuffer)
        {
            Char[] buffer = new Char[bufferSize];
            while (true)
            {
                Int32 bytesRead = await input.ReadAsync(buffer, 0, bufferSize).ConfigureAwait(false);
                if (bytesRead < 1)
                {
                    break;
                }

                Memory<Byte> result = decode(buffer.AsMemory(0, bytesRead));
                await output.WriteAsync(result.ToArray(), 0, result.Length).ConfigureAwait(false);
            }
        }
    }
}