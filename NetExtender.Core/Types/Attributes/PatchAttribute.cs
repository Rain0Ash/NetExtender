// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Attributes.Interfaces;
using NetExtender.Types.Reflection;

namespace NetExtender.Patch
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class PatchAttribute : Attribute, IInvokeAttribute
    {
        public Type? Patch { get; }
        public ReflectionPatchCategory? Category { get; }
        public Boolean Exclude { get; init; }
        
        public PatchAttribute()
            : this(null)
        {
        }

        public PatchAttribute(Type? patch)
        {
            Patch = patch;
        }

        public PatchAttribute(ReflectionPatchCategory category)
        {
            Category = category;
        }

        internal Boolean Invoke()
        {
            return Invoke(null, null);
        }

        Boolean IInvokeAttribute.Invoke()
        {
            return Invoke();
        }

        internal Boolean Invoke(Object? value)
        {
            return Invoke(null, value);
        }

        Boolean IInvokeAttribute.Invoke(Object? value)
        {
            return Invoke(value);
        }

        internal virtual Boolean Invoke(Object? sender, Object? value)
        {
            if (Patch is not null)
            {
                return Initializer.Initializer.PatchUtilities.AutoInit(ReflectionPatch.GetName(Patch), Exclude);
            }

            if (value is not ReflectionPatchCategory category)
            {
                if (Category is null)
                {
                    return false;
                }
                
                category = Category.Value;
            }

            if (Exclude)
            {
                Initializer.Initializer.PatchUtilities.AutoInitPatchCategory &= ~category;
            }
            else
            {
                Initializer.Initializer.PatchUtilities.AutoInitPatchCategory |= category;
            }
            
            return true;
        }

        Boolean IInvokeAttribute.Invoke(Object? sender, Object? value)
        {
            return Invoke(sender, value);
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class AphilargyriaAttribute : PatchAttribute
    {
        internal override Boolean Invoke(Object? sender, Object? value)
        {
            return base.Invoke(sender, value ?? ReflectionPatchCategory.Aphilargyria);
        }
    }
}