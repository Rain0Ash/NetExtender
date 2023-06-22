// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.CodeAnalysis;
using NetExtender.StrongId.Template;

namespace NetExtender.StrongId.Generator
{
    internal record StrongIdTypeInfo
    {
        public Accessibility Accessibility { get; }
        public String Name { get; }
        public String Namespace { get; }
        public StrongIdConfiguration Config { get; }
        public StrongIdTemplateInfo? Parent { get; }
        
        public StrongIdTypeInfo(Accessibility accessibility, String name, String @namespace, StrongIdConfiguration config, StrongIdTemplateInfo? parent)
        {
            Accessibility = accessibility;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Namespace = @namespace ?? throw new ArgumentNullException(nameof(@namespace));
            Config = config;
            Parent = parent;
        }
    }
}