// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Initializer
{
    public enum AssemblySignInitialization : Byte
    {
        None = 0,
        Weak = 1,
        Strong = 2,
        Sign = 4,
        WeakSign = Weak | Sign,
        StrongSign = Strong | Sign,
        Hash = 8,
        WeakHash = Weak | Hash,
        StrongHash = Strong | Hash
    }
}