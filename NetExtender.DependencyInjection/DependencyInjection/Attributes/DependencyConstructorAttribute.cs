using System;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class DependencyConstructorAttribute : ActivatorUtilitiesConstructorAttribute
    {
    }
}