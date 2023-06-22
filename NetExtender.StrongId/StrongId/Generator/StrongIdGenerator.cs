// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using NetExtender.StrongId.Attributes;
using NetExtender.StrongId.Template;

namespace NetExtender.StrongId.Generator
{
    /// <inheritdoc />
    [Generator]
    public class StrongIdGenerator : IIncrementalGenerator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsTarget(SyntaxNode node, CancellationToken token)
        {
            return StrongIdParser.IsTarget(node);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static StructDeclarationSyntax? SemanticTarget(GeneratorSyntaxContext generator, CancellationToken token)
        {
            return StrongIdParser.SemanticTarget(generator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsAttributeTarget(SyntaxNode node, CancellationToken token)
        {
            return StrongIdParser.IsAttributeTarget(node);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static AttributeSyntax? AttributeSemanticTarget(GeneratorSyntaxContext generator, CancellationToken token)
        {
            return StrongIdParser.AttributeSemanticTarget(generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean NotNull<T>(T value)
        {
            return value is not null;
        }
        
        /// <inheritdoc />
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(Register);

            IncrementalValuesProvider<StructDeclarationSyntax> @struct = context.SyntaxProvider.CreateSyntaxProvider(IsTarget, SemanticTarget).WhereNotNull();
            IncrementalValuesProvider<AttributeSyntax> declarations = context.SyntaxProvider.CreateSyntaxProvider(IsAttributeTarget, AttributeSemanticTarget).WhereNotNull();
            IncrementalValueProvider<(ImmutableArray<StructDeclarationSyntax>, ImmutableArray<AttributeSyntax>)> targets = @struct.Collect().Combine(declarations.Collect());
            IncrementalValueProvider<(Compilation Left, (ImmutableArray<StructDeclarationSyntax>, ImmutableArray<AttributeSyntax>) Right)> compilation = context.CompilationProvider.Combine(targets);
            context.RegisterSourceOutput(compilation, Execute);
        }
        
        private static void Register(IncrementalGeneratorPostInitializationContext context)
        {
            context.AddSource($"{nameof(StrongIdAttribute)}.g.cs", StrongIdTemplate.AttributeSource);
            context.AddSource($"{nameof(StrongIdAssemblyAttribute)}.g.cs", StrongIdTemplate.AssemblyAttributeSource);
            context.AddSource($"{nameof(StrongIdUnderlyingType)}.g.cs", StrongIdTemplate.UnderlyingTypeSource);
            context.AddSource($"{nameof(StrongIdConversionType)}.g.cs", StrongIdTemplate.ConversionTypeSource);
            context.AddSource($"{nameof(StrongIdConverterType)}.g.cs", StrongIdTemplate.ConverterTypeSource);
            context.AddSource($"{nameof(StrongIdInterfaceType)}.g.cs", StrongIdTemplate.InterfaceTypeSource);
            context.AddSource($"{nameof(IStrongId)}.g.cs", StrongIdTemplate.InterfaceSource);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Execute(SourceProductionContext context, (Compilation Left, (ImmutableArray<StructDeclarationSyntax>, ImmutableArray<AttributeSyntax>) Right) source)
        {
            Execute(source.Left, source.Right.Item1, source.Right.Item2, context);
        }

        public static void Execute(Compilation compilation, ImmutableArray<StructDeclarationSyntax> structs, ImmutableArray<AttributeSyntax> assembly, SourceProductionContext context)
        {
            if (compilation is null)
            {
                throw new ArgumentNullException(nameof(compilation));
            }

            if (structs.IsDefaultOrEmpty)
            {
                return;
            }

            StrongIdTypeInfo[] result = StrongIdParser.Types(compilation, structs, context.ReportDiagnostic, context.CancellationToken);

            if (result.Length <= 0)
            {
                return;
            }

            StringBuilder builder = new StringBuilder();
            StrongIdConfiguration? configuration = StrongIdParser.Configuration(compilation, assembly, context.ReportDiagnostic);
            foreach (StrongIdTypeInfo info in result)
            {
                builder.Clear();
                StrongIdConfiguration values = StrongIdConfiguration.Combine(info.Config, configuration);

                if (values.Type is not { } type || values.Conversion is not { } conversion || values.Converter is not { } converter || values.Interfaces is not { } interfaces)
                {
                    continue;
                }

                StrongIdTypeGenerator generator = new StrongIdTypeGenerator();
                String filename = generator.CreateSourceName(info);
                String source = generator.Create(builder, info, type, conversion, converter, interfaces);
                context.AddSource(filename, SourceText.From(source, Encoding.UTF8));
            }
        }
    }
}