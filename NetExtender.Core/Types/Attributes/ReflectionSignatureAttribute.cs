// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;

namespace NetExtender.Utilities.Core
{
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ReflectionSignatureAttribute : ReflectionNamingAttribute
    {
        public ReflectionSignatureAttribute()
        {
        }
        
        public ReflectionSignatureAttribute(Type? reference)
            : base(reference)
        {
        }
    }
}