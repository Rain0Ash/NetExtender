using System;

namespace NetExtender.Domains.Applications
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ApplicationInitializerAttribute : Attribute
    {
    }
}