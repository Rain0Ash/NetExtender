using System;
using System.ComponentModel;
using System.Reflection;

namespace NetExtender.Utilities.Core
{
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ReflectionNamingAttribute : Attribute
    {
        public Type? Reference { get; }
        
        public Assembly? Assembly
        {
            get
            {
                return Reference?.Assembly;
            }
        }
        
        public ReflectionNamingAttribute()
            : this(null)
        {
        }
        
        public ReflectionNamingAttribute(Type? reference)
        {
            Reference = reference;
        }
    }
    
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ReflectionSystemResourceAttribute : ReflectionNamingAttribute
    {
        public new Type Reference
        {
            get
            {
                return base.Reference ?? throw new InvalidOperationException();
            }
        }
        
        public new Assembly Assembly
        {
            get
            {
                return base.Assembly ?? throw new InvalidOperationException();
            }
        }
        
        public ReflectionSystemResourceAttribute(Type reference)
            : base(reference ?? throw new ArgumentNullException(nameof(reference)))
        {
        }
    }
}