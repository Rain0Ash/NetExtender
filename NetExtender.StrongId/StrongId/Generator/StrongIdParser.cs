// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NetExtender.StrongId.Diagnostics;
using NetExtender.StrongId.Template;

namespace NetExtender.StrongId.Generator
{
    [SuppressMessage("ReSharper", "CognitiveComplexity")]
    internal static class StrongIdParser
    {
        private const String StrongIdAttribute = $"{StrongIdTemplate.StrongId}.{nameof(Attributes)}.{nameof(Attributes.StrongIdAttribute)}";
        private const String StrongIdAssemblyAttribute = $"{StrongIdTemplate.StrongId}.{nameof(Attributes)}.{nameof(Attributes.StrongIdAssemblyAttribute)}";

        public static Boolean IsTarget(SyntaxNode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return node is StructDeclarationSyntax syntax && syntax.AttributeLists.Count > 0;
        }

        public static Boolean IsAttributeTarget(SyntaxNode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return node is AttributeListSyntax attributes && attributes.Target?.Identifier.IsKind(SyntaxKind.AssemblyKeyword) is true;
        }

        public static StructDeclarationSyntax? SemanticTarget(GeneratorSyntaxContext context)
        {
            StructDeclarationSyntax @struct = (StructDeclarationSyntax) context.Node;

            foreach (AttributeSyntax attribute in @struct.AttributeLists.SelectMany(attributes => attributes.Attributes))
            {
                if (ModelExtensions.GetSymbolInfo(context.SemanticModel, attribute).Symbol is IMethodSymbol symbol && symbol.ContainingType.ToDisplayString() == StrongIdAttribute)
                {
                    return @struct;
                }
            }

            return null;
        }

        public static AttributeSyntax? AttributeSemanticTarget(GeneratorSyntaxContext context)
        {
            AttributeListSyntax attributes = (AttributeListSyntax) context.Node;

            foreach (AttributeSyntax attribute in attributes.Attributes)
            {
                if (ModelExtensions.GetSymbolInfo(context.SemanticModel, attribute).Symbol is IMethodSymbol symbol && symbol.ContainingType.ToDisplayString() == StrongIdAssemblyAttribute)
                {
                    return attribute;
                }
            }

            return null;
        }

        private static Boolean ConstructorArguments(AttributeData attribute, out StrongIdUnderlyingType? type, out StrongIdConversionType? conversion, out StrongIdConverterType? converter, out StrongIdInterfaceType? interfaces)
        {
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            type = default;
            conversion = default;
            converter = default;
            interfaces = default;

            ImmutableArray<TypedConstant> arguments = attribute.ConstructorArguments;
            if (arguments.IsDefaultOrEmpty || arguments.Any(constant => constant.Kind == TypedConstantKind.Error))
            {
                return false;
            }

            switch (arguments.Length)
            {
                case 4:
                    interfaces = arguments[3].As<StrongIdInterfaceType>();
                    goto case 3;
                case 3:
                    try
                    {
                        converter = arguments[2].As<StrongIdConverterType>();
                        goto case 2;
                    }
                    catch (InvalidCastException)
                    {
                        interfaces = arguments[2].As<StrongIdInterfaceType>();
                        goto case 2;
                    }
                case 2:
                    try
                    {
                        conversion = arguments[1].As<StrongIdConversionType>();
                        goto case 1;
                    }
                    catch (InvalidCastException)
                    {
                        try
                        {
                            converter = arguments[2].As<StrongIdConverterType>();
                            goto case 1;
                        }
                        catch (InvalidCastException)
                        {
                            interfaces = arguments[2].As<StrongIdInterfaceType>();
                            goto case 1;
                        }
                    }
                case 1:
                    try
                    {
                        type = arguments[0].As<StrongIdUnderlyingType>();
                        goto case 0;
                    }
                    catch (InvalidCastException)
                    {
                        ITypeSymbol? symbol = arguments[0].Value as ITypeSymbol;
                        type = symbol?.Convert() ?? throw new InvalidOperationException($"Type '{arguments[0].Value?.GetType()}' is not supported.");
                        goto case 0;
                    }
                case 0:
                    break;
                default:
                    goto case 4;
            }

            return true;
        }

        private static Boolean Arguments(AttributeData attribute, out StrongIdUnderlyingType? type, out StrongIdConversionType? conversion, out StrongIdConverterType? converter, out StrongIdInterfaceType? interfaces)
        {
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            type = default;
            conversion = default;
            converter = default;
            interfaces = default;

            ImmutableArray<KeyValuePair<String, TypedConstant>> arguments = attribute.NamedArguments;
            if (arguments.IsEmpty)
            {
                return false;
            }

            foreach (KeyValuePair<String, TypedConstant> pair in attribute.NamedArguments)
            {
                if (pair.Value.Kind == TypedConstantKind.Error)
                {
                    return false;
                }

                switch (pair.Key)
                {
                    case nameof(type):
                        try
                        {
                            type = pair.Value.As<StrongIdUnderlyingType>();
                            continue;
                        }
                        catch (InvalidCastException)
                        {
                            ITypeSymbol? symbol = pair.Value.Value as ITypeSymbol;
                            type = symbol?.Convert() ?? throw new InvalidOperationException($"Type '{pair.Value.Value?.GetType()}' is not supported.");
                            continue;
                        }
                    case nameof(conversion):
                        conversion = pair.Value.As<StrongIdConversionType>();
                        continue;
                    case nameof(converter):
                        converter = pair.Value.As<StrongIdConverterType>();
                        continue;
                    case nameof(interfaces):
                        interfaces = pair.Value.As<StrongIdInterfaceType>();
                        continue;
                }
            }

            return true;
        }

        private static Diagnostic? Diagnosic(AttributeData attribute, StrongIdUnderlyingType? type, StrongIdConversionType? conversion, StrongIdConverterType? converter, StrongIdInterfaceType? interfaces)
        {
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            SyntaxNode? syntax = attribute.ApplicationSyntaxReference?.GetSyntax();
            return syntax is not null ? Diagnosic(syntax, type, conversion, converter, interfaces) : null;
        }

        private static Diagnostic? Diagnosic(SyntaxNode syntax, StrongIdUnderlyingType? type, StrongIdConversionType? conversion, StrongIdConverterType? converter, StrongIdInterfaceType? interfaces)
        {
            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            if (type is not null && !Enum.IsDefined(typeof(StrongIdUnderlyingType), type.Value))
            {
                return InvalidUnderlyingTypeDiagnostic.Create(syntax);
            }

            if (conversion > StrongIdConversionType.Explicit)
            {
                return InvalidConversionDiagnostic.Create(syntax);
            }

            if (converter > StrongIdConverterType.All)
            {
                return InvalidConverterDiagnostic.Create(syntax);
            }

            if (interfaces > StrongIdInterfaceType.All)
            {
                return InvalidInterfaceDiagnostic.Create(syntax);
            }

            return null;
        }

        public static StrongIdTypeInfo[] Types(Compilation compilation, ImmutableArray<StructDeclarationSyntax> targets, Action<Diagnostic> handler, CancellationToken token)
        {
            if (compilation is null)
            {
                throw new ArgumentNullException(nameof(compilation));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
            
            INamedTypeSymbol? symbol = compilation.GetTypeByMetadataName(StrongIdAttribute);
            if (symbol is null)
            {
                return Array.Empty<StrongIdTypeInfo>();
            }

            List<StrongIdTypeInfo> result = new List<StrongIdTypeInfo>();
            foreach (StructDeclarationSyntax syntax in targets)
            {
                token.ThrowIfCancellationRequested();

                SemanticModel model = compilation.GetSemanticModel(syntax.SyntaxTree);
                if (model.GetDeclaredSymbol(syntax, token) is not INamedTypeSymbol @struct)
                {
                    continue;
                }

                Boolean successful = false;
                StrongIdConfiguration? config = null;
                foreach (AttributeData attribute in @struct.GetAttributes().Where(attribute => symbol.Equals(attribute.AttributeClass, SymbolEqualityComparer.Default)))
                {
                    StrongIdUnderlyingType? type = null;
                    StrongIdConversionType? conversion = null;
                    StrongIdConverterType? converter = null;
                    StrongIdInterfaceType? interfaces = null;
                    
                    if (!attribute.ConstructorArguments.IsEmpty)
                    {
                        successful = ConstructorArguments(attribute, out type, out conversion, out converter, out interfaces);
                    }

                    if (!attribute.NamedArguments.IsEmpty)
                    {
                        successful = Arguments(attribute, out type, out conversion, out converter, out interfaces);
                    }

                    if (!successful)
                    {
                        break;
                    }

                    if (!syntax.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.PartialKeyword)))
                    {
                        handler(NotPartialDiagnostic.Create(syntax));
                    }

                    if (Diagnosic(syntax, type, conversion, converter, interfaces) is Diagnostic diagnostic)
                    {
                        handler(diagnostic);
                    }

                    config = new StrongIdConfiguration(type, conversion, converter, interfaces);
                    break;
                }

                if (!successful || config is null)
                {
                    continue;
                }

                result.Add(new StrongIdTypeInfo(syntax.GetAccessibility(), @struct.Name, Namespace(syntax), config.Value, TemplateInfo(syntax)));
            }

            return result.ToArray();
        }

        public static StrongIdConfiguration? Configuration(Compilation compilation, ImmutableArray<AttributeSyntax> assembly, Action<Diagnostic> handler)
        {
            if (compilation is null)
            {
                throw new ArgumentNullException(nameof(compilation));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (assembly.IsDefaultOrEmpty)
            {
                return null;
            }

            ImmutableArray<AttributeData> attributes = compilation.Assembly.GetAttributes();
            if (attributes.IsDefaultOrEmpty)
            {
                return null;
            }

            INamedTypeSymbol? symbol = compilation.GetTypeByMetadataName(StrongIdAssemblyAttribute);
            if (symbol is null)
            {
                return null;
            }

            foreach (AttributeData attribute in attributes.Where(attribute => symbol.Equals(attribute.AttributeClass, SymbolEqualityComparer.Default)))
            {
                StrongIdUnderlyingType? type = null;
                StrongIdConversionType? conversion = null;
                StrongIdConverterType? converter = null;
                StrongIdInterfaceType? interfaces = null;
                Boolean successful = false;

                if (!attribute.ConstructorArguments.IsEmpty)
                {
                    successful = ConstructorArguments(attribute, out type, out conversion, out converter, out interfaces);
                }

                if (!attribute.NamedArguments.IsEmpty)
                {
                    successful = Arguments(attribute, out type, out conversion, out converter, out interfaces);
                }

                if (!successful)
                {
                    return null;
                }

                if (Diagnosic(attribute, type, conversion, converter, interfaces) is Diagnostic diagnostic)
                {
                    handler(diagnostic);
                }

                return new StrongIdConfiguration(type, conversion, converter, interfaces);
            }

            return null;
        }

        private static String Namespace(SyntaxNode? node)
        {
            if (node is null)
            {
                return String.Empty;
            }

            while ((node = node?.Parent) is not null && node is not NamespaceDeclarationSyntax && node is not FileScopedNamespaceDeclarationSyntax) { }

            if (node is not BaseNamespaceDeclarationSyntax parent)
            {
                return String.Empty;
            }

            String @namespace = parent.Name.ToString();
            while (parent.Parent is NamespaceDeclarationSyntax next)
            {
                parent = next;
                @namespace = $"{parent.Name}.{@namespace}";
            }

            return @namespace;
        }

        private static StrongIdTemplateInfo? TemplateInfo(SyntaxNode node)
        {
            StrongIdTemplateInfo? parent = null;
            while (node.Parent is TypeDeclarationSyntax syntax && syntax.Kind().IsDeclaration())
            {
                parent = new StrongIdTemplateInfo(syntax.GetAccessibility(), syntax.Keyword.ValueText, syntax.Identifier.ToString() + syntax.TypeParameterList, syntax.ConstraintClauses.ToString(), parent);
                node = syntax;
            }

            return parent;
        }
    }
}