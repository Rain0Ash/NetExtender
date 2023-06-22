// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.CodeAnalysis;

namespace NetExtender.StrongId.Template
{
    public class StrongIdTemplateInfo
    {
        public Accessibility Accessibility { get; }
        public String Keyword { get; }
        public String Name { get; }
        public String Constraints { get; }
        public StrongIdTemplateInfo? Child { get; }

        public StrongIdTemplateInfo(Accessibility accessibility, String keyword, String name, String constraints)
            : this(accessibility, keyword, name, constraints, null)
        {
        }

        public StrongIdTemplateInfo(Accessibility accessibility, String keyword, String name, String constraints, StrongIdTemplateInfo? child)
        {
            Accessibility = accessibility;
            Keyword = keyword ?? throw new ArgumentNullException(nameof(keyword));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Constraints = constraints ?? throw new ArgumentNullException(nameof(constraints));
            Child = child;
        }
    }
}