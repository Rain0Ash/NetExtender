// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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