// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.JWT.Algorithms.Interfaces;

namespace NetExtender.JWT.Interfaces
{
    public interface IJWTValidator
    {
        public void Validate(IJWTAsymmetricAlgorithm algorithm, String payload, ReadOnlySpan<Byte> source, ReadOnlySpan<Byte> signature);
        public void Validate(IJWTAsymmetricAlgorithm algorithm, String payload, Byte[] source, Byte[] signature);
        public void Validate(String payload, String crypto, String signature);
        public void Validate(String payload, String crypto, ReadOnlySpan<String?> signatures);
        public void Validate(String payload, String crypto, params String[] signatures);
        public Exception? TryValidate(IJWTAsymmetricAlgorithm? algorithm, String? payload, ReadOnlySpan<Byte> source, ReadOnlySpan<Byte> signature);
        public Exception? TryValidate(IJWTAsymmetricAlgorithm? algorithm, String? payload, Byte[]? source, Byte[]? signature);
        public Exception? TryValidate(String? payload, String? crypto, String? signature);
        public Exception? TryValidate(String? payload, String? crypto, ReadOnlySpan<String?> signatures);
        public Exception? TryValidate(String? payload, String? crypto, params String?[]? signatures);
    }
}