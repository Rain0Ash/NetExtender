using System;
using System.ComponentModel;

namespace NetExtender.Utilities.Core
{
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [AttributeUsage(AttributeTargets.All)]
    public class ReflectionNamingAttribute : Attribute
    {
    }
}