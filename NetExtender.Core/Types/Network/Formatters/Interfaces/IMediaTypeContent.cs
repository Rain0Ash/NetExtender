// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Network.Formatters.Interfaces
{
    public interface IMediaTypeContent<T> : IMediaTypeContent
    {
        public new T Value { get; set; }
    }
    
    public interface IMediaTypeContent
    {
        public Type Type { get; }
        public MediaTypeFormatter Formatter { get; }
        public Object? Value { get; }
    }
}