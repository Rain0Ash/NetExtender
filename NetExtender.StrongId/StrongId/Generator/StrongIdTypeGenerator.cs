// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using NetExtender.StrongId.Template;

namespace NetExtender.StrongId.Generator
{
    internal class StrongIdTypeGenerator
    {
        private const String Type = nameof(StrongIdUtilities.TYPE);
        private const String TypeName = nameof(StrongIdUtilities.TYPENAME);
        private const String StrongTypeName = nameof(StrongIdUtilities.STRONGID);
        private const String UnderlyingType = nameof(StrongIdUtilities.UNDERLYING);
        private const String UnderlyingTypeNullable = UnderlyingType + "?";
        private const String SwaggerType = nameof(StrongIdUtilities.SWAGGERTYPE);
        private const String SwaggerFormat = nameof(StrongIdUtilities.SWAGGERFORMAT);
        private const String Nullable = nameof(StrongIdUtilities.NULLABLE);
        private const String NumberStyle = nameof(StrongIdUtilities.NUMBERSTYLE);
        private const String Accessibility = nameof(StrongIdUtilities.ACCESSIBILITY);
        private const String Interfaces = nameof(StrongIdUtilities.INTERFACES);
        
        public virtual String Create(StringBuilder? builder, StrongIdTypeInfo info, StrongIdUnderlyingType type, StrongIdConversionType conversion, StrongIdConverterType converter, StrongIdInterfaceType interfaces)
        {
            return Create(builder, info, StrongIdTemplate.Get(type), type, conversion, converter, interfaces);
        }

        private protected virtual String Create(StringBuilder? builder, StrongIdTypeInfo info, StrongIdTemplate.Resource resource, StrongIdUnderlyingType type, StrongIdConversionType conversion, StrongIdConverterType converter, StrongIdInterfaceType interfaces)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            String name = info.Name;
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(info.Name));
            }

            Byte count = 0;

            builder ??= new StringBuilder();
            builder.Append(resource.Header);

            String @namespace = info.Namespace;
            if (!String.IsNullOrEmpty(@namespace))
            {
                builder.Append("namespace ").Append(@namespace).AppendLine().AppendLine("{");
            }

            StrongIdTemplateInfo? parent = info.Parent;
            while (parent is not null)
            {
                WriteNested(builder, parent);
                parent = parent.Child;
                count++;
            }

            AppendAttributes(builder, converter, count);
            AppendDeclaration(builder, resource, info, count);
            ReplaceInterfaces(builder, interfaces);
            AppendConversion(builder, resource, conversion, count);
            AppendOperators(builder, resource, interfaces, count);
            builder.AppendLine(resource.Template.Indentation(count));
            builder.AppendLine(resource.Properties.Indentation(count));
            builder.AppendLine(resource.Constructors.Indentation(count));
            AppendParseable(builder, resource, type, interfaces, count);
            builder.AppendLine(resource.Methods.Indentation(count));
            AppendInterfaces(builder, resource, interfaces, count);
            AppendFormattable(builder, resource, type, interfaces, count);
            AppendConverters(builder, resource, converter, count);

            builder.Replace(StrongTypeName, name);
            ReplacePlaceholders(builder, resource);

            for (Int32 i = count + 1; i > 0; i--)
            {
                builder.AppendLine(new String(' ', i * 4) + "}");
            }

            if (!String.IsNullOrEmpty(@namespace))
            {
                builder.Append('}').AppendLine();
            }

            return builder.ToString();
        }

        private protected virtual void WriteNested(StringBuilder builder, StrongIdTemplateInfo parent)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            
            String accessibility = parent.Accessibility.ToAccessibilityString();
            builder.Append("    ").Append(accessibility).Append(" partial ").Append(parent.Keyword).Append(' ').Append(parent.Name);

            if (!String.IsNullOrEmpty(parent.Constraints))
            {
                builder.Append(' ').Append(parent.Constraints);
            }
            
            builder.AppendLine().AppendLine("    {");
        }

        private protected virtual void ReplacePlaceholders(StringBuilder builder, StrongIdTemplate.Resource resource)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            builder.Replace(SwaggerType, $"\"{resource.SwaggerType}\"");
            builder.Replace(SwaggerFormat, $"\"{resource.SwaggerFormat}\"");
            builder.Replace(UnderlyingTypeNullable, resource.Type.EndsWith("?") ? resource.Type : resource.Type + "?");
            builder.Replace(UnderlyingType, resource.Type);
            builder.Replace(TypeName, resource.Name.Remove("?"));
            builder.Replace(Type, resource.Type.Remove("?"));
            builder.Replace(Nullable, resource.Underlying.IsNullable() ? "true" : "false");
        }

        private protected virtual StrongIdConverterType AppendAttributes(StringBuilder builder, StrongIdConverterType type, Byte count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (type.HasFlag(StrongIdConverterType.String))
            {
                builder.AppendLine(StrongIdTemplate.TypeConverterAttributeSource.Indentation(count));
            }

            if (type.HasFlag(StrongIdConverterType.TextJson))
            {
                builder.AppendLine(StrongIdTemplate.TextJsonAttributeSource.Indentation(count));
            }

            if (type.HasFlag(StrongIdConverterType.Newtonsoft))
            {
                builder.AppendLine(StrongIdTemplate.NewtonsoftJsonAttributeSource.Indentation(count));
            }

            if (type.HasFlag(StrongIdConverterType.Swagger))
            {
                builder.AppendLine(StrongIdTemplate.SwaggerSchemaFilterAttributeSource.Indentation(count));
            }

            return type;
        }

        private protected virtual void AppendDeclaration(StringBuilder builder, StrongIdTemplate.Resource resource, StrongIdTypeInfo info, Byte count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            String accessibility = info.Accessibility.ToAccessibilityString();
            builder.Append(resource.Declaration.Replace($"{Accessibility} ", !String.IsNullOrEmpty(accessibility) ? accessibility + " " : String.Empty).Indentation(count));
        }

        // ReSharper disable once CognitiveComplexity
        private protected virtual StrongIdParseType AppendParseable(StringBuilder builder, StrongIdTemplate.Resource resource, StrongIdUnderlyingType type, StrongIdInterfaceType interfaces, Byte count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            (StrongIdParseType flag, NumberStyles? style) = type.Parseable();
            if (flag == StrongIdParseType.None || !interfaces.HasFlag(StrongIdInterfaceType.Parseable))
            {
                return StrongIdParseType.None;
            }
            
            static StrongIdParseType Region(String line)
            {
                const String parse = $"{nameof(StrongIdParseType)}.";
                Int32 start = line.IndexOf(parse, StringComparison.Ordinal) + parse.Length;
                return (StrongIdParseType) Enum.Parse(typeof(StrongIdParseType), line.Substring(start, line.Length - start));
            }

            static String Replace(String line, NumberStyles? style)
            {
                if (style is null)
                {
                    return line;
                }

                static String Selector(String flag)
                {
                    return $"{nameof(System)}.{nameof(System.Globalization)}.{nameof(NumberStyles)}.{flag}";
                }

                String[] flags = style.Value.Split();
                return line.Replace(NumberStyle, String.Join(" | ", flags.Select(Selector)));
            }

            static Boolean Inside(StrongIdParseType region, StrongIdParseType flag)
            {
                (StrongIdParseType parse, StrongIdParseType direct) = region.Split();
                return flag.HasFlag(direct) && region == direct || !flag.HasFlag(direct) && flag.HasFlag(parse) && region == parse;
            }

            Boolean inside = false;
            StrongIdParseType result = StrongIdParseType.None;
            foreach (String line in resource.Parseable.Split('\n'))
            {
                if (line.Contains("#IF "))
                {
                    StrongIdParseType region = Region(line);
                    inside = Inside(region, flag);
                    result |= inside ? region : StrongIdParseType.None;
                    continue;
                }

                if (line.Contains("#ENDIF"))
                {
                    inside = false;
                    continue;
                }

                if (inside)
                {
                    builder.Append(Replace(line, style).Indentation(count));
                }
            }

            return result;
        }
        
        // ReSharper disable once CognitiveComplexity
        private protected virtual StrongIdFormatType AppendFormattable(StringBuilder builder, StrongIdTemplate.Resource resource, StrongIdUnderlyingType type, StrongIdInterfaceType interfaces, Byte count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            StrongIdFormatType flag = type.Formattable();
            if (flag == StrongIdFormatType.None || !interfaces.HasFlag(StrongIdInterfaceType.Formattable))
            {
                return StrongIdFormatType.None;
            }
            
            static StrongIdFormatType Region(String line)
            {
                const String parse = $"{nameof(StrongIdFormatType)}.";
                Int32 start = line.IndexOf(parse, StringComparison.Ordinal) + parse.Length;
                return (StrongIdFormatType) Enum.Parse(typeof(StrongIdFormatType), line.Substring(start, line.Length - start));
            }
            
            static Boolean Inside(StrongIdFormatType region, StrongIdFormatType flag)
            {
                (StrongIdFormatType format, StrongIdFormatType direct) = region.Split();
                return flag.HasFlag(direct) && region == direct || !flag.HasFlag(direct) && flag.HasFlag(format) && region == format;
            }
            
            Boolean inside = false;
            StrongIdFormatType result = StrongIdFormatType.None;
            foreach (String line in resource.Formattable.Split('\n'))
            {
                if (line.Contains("#IF "))
                {
                    StrongIdFormatType region = Region(line);
                    inside = Inside(region, flag);
                    result |= inside ? region : StrongIdFormatType.None;
                    continue;
                }

                if (line.Contains("#ENDIF"))
                {
                    inside = false;
                    continue;
                }

                if (inside)
                {
                    builder.Append(line.Indentation(count));
                }
            }

            return result;
        }

        private protected virtual StrongIdConversionType AppendConversion(StringBuilder builder, StrongIdTemplate.Resource resource, StrongIdConversionType type, Byte count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (type.HasFlag(StrongIdConversionType.FromExplicit))
            {
                builder.AppendLine(resource.ExplicitFrom.Indentation(count));
            }
            else if (type.HasFlag(StrongIdConversionType.FromImplicit))
            {
                builder.AppendLine(resource.ImplicitFrom.Indentation(count));
            }

            if (type.HasFlag(StrongIdConversionType.ToExplicit))
            {
                builder.AppendLine(resource.ExplicitTo.Indentation(count));
            }
            else if (type.HasFlag(StrongIdConversionType.ToImplicit))
            {
                builder.AppendLine(resource.ImplicitTo.Indentation(count));
            }

            return type;
        }

        private protected virtual StrongIdInterfaceType AppendOperators(StringBuilder builder, StrongIdTemplate.Resource resource, StrongIdInterfaceType type, Byte count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (type.HasFlag(StrongIdInterfaceType.Equatable))
            {
                builder.AppendLine(resource.EqualityOperators.Indentation(count));
            }

            if (type.HasFlag(StrongIdInterfaceType.Comparable))
            {
                builder.AppendLine(resource.ComparableOperators.Indentation(count));
            }

            return type;
        }

        private protected virtual StrongIdConverterType AppendConverters(StringBuilder builder, StrongIdTemplate.Resource resource, StrongIdConverterType type, Byte count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (type.HasFlag(StrongIdConverterType.Serializable))
            {
                builder.AppendLine(resource.Serializable.Indentation(count));
            }

            if (type.HasFlag(StrongIdConverterType.TextJson))
            {
                builder.AppendLine(resource.TextJsonConverter.Indentation(count));
            }

            if (type.HasFlag(StrongIdConverterType.Newtonsoft))
            {
                builder.AppendLine(resource.NewtonsoftJsonConverter.Indentation(count));
            }

            if (type.HasFlag(StrongIdConverterType.String))
            {
                builder.AppendLine(resource.TypeConverter.Indentation(count));
            }

            if (type.HasFlag(StrongIdConverterType.EntityFramework))
            {
                builder.AppendLine(resource.EntityFrameworkValueConverter.Indentation(count));
            }

            if (type.HasFlag(StrongIdConverterType.Dapper))
            {
                builder.AppendLine(resource.DapperTypeHandler.Indentation(count));
            }

            if (type.HasFlag(StrongIdConverterType.Swagger))
            {
                builder.AppendLine(resource.SwaggerSchemaFilter.Indentation(count));
            }

            return type;
        }

        private protected virtual StrongIdInterfaceType AppendInterfaces(StringBuilder builder, StrongIdTemplate.Resource resource, StrongIdInterfaceType type, Byte count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (type.HasFlag(StrongIdInterfaceType.Comparable))
            {
                builder.AppendLine(resource.Comparable.Indentation(count));
            }

            return type;
        }

        private protected virtual StrongIdInterfaceType ReplaceInterfaces(StringBuilder builder, StrongIdInterfaceType type)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            List<String> interfaces = new List<String> { $"{nameof(NetExtender)}.{nameof(StrongId)}.{nameof(IStrongId)}<{StrongTypeName}, {UnderlyingType}>" };
            StrongIdInterfaceType result = StrongIdInterfaceType.None;

            if (type.HasFlag(StrongIdInterfaceType.Parseable))
            {
#if NET7_0_OR_GREATER
                    interfaces.Add($"{nameof(System)}.{nameof(IParseable<StrongIdUtilities.STRONGID>)}<{TypeName}>");
                    result |= StrongIdInterfaceType.Parseable;
#endif
            }

            if (type.HasFlag(StrongIdInterfaceType.Equatable))
            {
                interfaces.Add($"{nameof(System)}.{nameof(IEquatable<StrongIdUtilities.STRONGID>)}<{StrongTypeName}>");
                result |= StrongIdInterfaceType.Equatable;
            }

            if (type.HasFlag(StrongIdInterfaceType.Comparable))
            {
                interfaces.Add($"{nameof(System)}.{nameof(IComparable<StrongIdUtilities.STRONGID>)}<{StrongTypeName}>");
                result |= StrongIdInterfaceType.Comparable;
            }

            if (type.HasFlag(StrongIdInterfaceType.Formattable))
            {
                interfaces.Add($"{nameof(System)}.{nameof(IFormattable)}");
                result |= StrongIdInterfaceType.Formattable;
            }

            if (interfaces.Count <= 0)
            {
                builder.Remove($": {Interfaces}");
                return result;
            }

            builder.Replace(Interfaces, String.Join(", ", interfaces));
            return result;
        }

        public virtual String CreateSourceName(StrongIdTypeInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            String name = info.Name;
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            String @namespace = info.Namespace;
            if (@namespace is null)
            {
                throw new ArgumentNullException(nameof(@namespace));
            }

            StrongIdTemplateInfo? parent = info.Parent;
            StringBuilder builder = new StringBuilder(@namespace).Append('.');
            while (parent is not null)
            {
                String result = parent.Name.Remove(" ").Remove(",").Replace("<", "__").Remove(">");
                builder.Append(result).Append('.');
                parent = parent.Child;
            }

            return builder.Append(name).Append(".g.cs").ToString();
        }
    }
}