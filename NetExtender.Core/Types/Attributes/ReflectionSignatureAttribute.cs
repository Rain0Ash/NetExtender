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