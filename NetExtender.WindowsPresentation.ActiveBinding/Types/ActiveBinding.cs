// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using NetExtender.Types.Monads;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    public sealed class ActiveBinding : ActiveBindingAbstraction
    {
        public IMultiValueConverter? Converter { get; set; }

        [TypeConverter(typeof(CultureInfoIetfLanguageTagConverter))]
        public CultureInfo? ConverterCulture { get; set; }
        public Object? ConverterParameter { get; set; }
        public BindingMode Mode { get; set; } = BindingMode.Default;
        public Boolean NotifyOnSourceUpdated { get; set; }
        public Boolean NotifyOnTargetUpdated { get; set; }
        public Boolean NotifyOnValidationError { get; set; }
        public UpdateSourceExceptionFilterCallback? UpdateSourceExceptionFilter { get; set; }
        public UpdateSourceTrigger UpdateSourceTrigger { get; set; }
        public Boolean ValidatesOnDataErrors { get; set; }
        public Boolean ValidatesOnExceptions { get; set; }
        public RelativeSource? RelativeSource { get; set; }
        public Object? Source { get; set; }
        public String? ElementName { get; set; }
        public String? StringFormat { get; set; }

        private static DynamicLazy<IActiveBindingExpressionParser> Parser { get; } = new DynamicLazy<IActiveBindingExpressionParser>(ActiveBindingCacheExpressionParser.Caching);

        private const String InputFormatExceptionMessage = @"Input format exception of RelativeSource define, it must be
zzzzz@PreviousData           // means {Binding Path=zzzzz, RelativeSource={RelativeSource PreviousData}}
yyyyy@TemplatedParent        // means {Binding Path=yyyyy, RelativeSource={RelativeSource TemplatedParent}}
Parent.Name@Self             // means {Binding Path=Parent.Name, RelativeSource={RelativeSource Self}}
xxxx@FindAncestor.Grid       // means {Binding Path=xxxx, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Grid}}}
xxxx@FindAncestor[2].Grid    // means {Binding Path=xxxx, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Grid}, AncestorLevel=2}}
xxxx@Grid                    // means {Binding Path=xxxx, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Grid}}}
Title@Window                 // means {Binding Path=Title, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Window}}}
";
        public ActiveBinding()
            : this(null)
        {
        }

        public ActiveBinding(String? path)
            : base(path)
        {
        }

        private void AdjustRelativeSource(IServiceProvider provider, Binding binding, String? relative)
        {
            if (relative is null)
            {
                if (RelativeSource is not null)
                {
                    binding.RelativeSource = RelativeSource;
                }

                return;
            }

            Match match = RelativeSourceRegex.Match(relative);
            if (!match.Success)
            {
                throw new ActiveBindingException(InputFormatExceptionMessage);
            }

            if (match.Groups[1].Success)
            {
                if (!Enum.TryParse(relative, out RelativeSourceMode mode))
                {
                    return;
                }

                binding.RelativeSource = new RelativeSource { Mode = mode };
                return;
            }

            if (!match.Groups[2].Success)
            {
                throw new ActiveBindingException($"Wrong grammar on '{relative}'");
            }

            binding.RelativeSource = new RelativeSource
            {
                Mode = RelativeSourceMode.FindAncestor,
                AncestorLevel = match.Groups[5].Success ? Int32.Parse(match.Groups[5].Value) : 1,
                AncestorType = new TypeExtension(match.Groups[6].Value).ProvideValue(provider) as Type,
            };
        }

        public override Object ProvideValue(IServiceProvider serviceProvider)
        {
            Type? targetPropertyType = GetPropertyType(serviceProvider);
            IXamlTypeResolver? typeResolver = (IXamlTypeResolver?) serviceProvider.GetService(typeof(IXamlTypeResolver));
            ITypeDescriptorContext? typeDescriptor = serviceProvider as ITypeDescriptorContext;

            String? normalizedPath = NormalizePath(Path);
            List<PathAppearance> pathes = GetSourcePath(normalizedPath, typeResolver);

            String expressionTemplate = GetExpressionTemplate(normalizedPath, pathes, out Dictionary<String, Type> enumParameters);

            ActiveBindingConverter mathConverter = new ActiveBindingConverter(Parser.Value, FallbackValue, enumParameters)
            {
                FalseIsCollapsed = FalseIsCollapsed,
                StringFormatDefined = StringFormat is not null,
            };

            List<PathAppearance> bindingPathes = pathes
                .Where(p => p.Id.Type == ActiveBindingPathTokenType.Property ||
                            p.Id.Type == ActiveBindingPathTokenType.StaticProperty).ToList();

            BindingBase resBinding;


            if (bindingPathes.Count == 1)
            {
                Binding binding = new Binding
                {
                    Mode = Mode,
                    NotifyOnSourceUpdated = NotifyOnSourceUpdated,
                    NotifyOnTargetUpdated = NotifyOnTargetUpdated,
                    NotifyOnValidationError = NotifyOnValidationError,
                    UpdateSourceExceptionFilter = UpdateSourceExceptionFilter,
                    UpdateSourceTrigger = UpdateSourceTrigger,
                    ValidatesOnDataErrors = ValidatesOnDataErrors,
                    ValidatesOnExceptions = ValidatesOnExceptions,
                    FallbackValue = FallbackValue
                };

                ActiveBindingPathTokenId pathId = bindingPathes.Single().Id;
                String pathValue = pathId.Value;
                AdjustRelativeSource(serviceProvider, binding, pathId.RelativeSource);

                if (pathId.Type == ActiveBindingPathTokenType.StaticProperty)
                {
                    pathValue = $"({pathValue})";
                }

                PropertyPath? resPath = (PropertyPath?) new PropertyPathConverter().ConvertFromString(typeDescriptor, pathValue);
                binding.Path = resPath;

                if (Source is not null)
                {
                    binding.Source = Source;
                }

                if (ElementName is not null)
                {
                    binding.ElementName = ElementName;
                }

                if (StringFormat is not null)
                {
                    binding.StringFormat = StringFormat;
                }

                if ((expressionTemplate != "{0}" && expressionTemplate != "({0})") || targetPropertyType == typeof(Visibility))
                {
                    binding.Converter = mathConverter;
                    binding.ConverterParameter = expressionTemplate;
                    binding.ConverterCulture = ConverterCulture;
                }

                resBinding = binding;
            }
            else
            {
                MultiBinding mBinding = new MultiBinding
                {
                    Converter = mathConverter,
                    ConverterParameter = expressionTemplate,
                    ConverterCulture = ConverterCulture,
                    Mode = BindingMode.OneWay,
                    NotifyOnSourceUpdated = NotifyOnSourceUpdated,
                    NotifyOnTargetUpdated = NotifyOnTargetUpdated,
                    NotifyOnValidationError = NotifyOnValidationError,
                    UpdateSourceExceptionFilter = UpdateSourceExceptionFilter,
                    UpdateSourceTrigger = UpdateSourceTrigger,
                    ValidatesOnDataErrors = ValidatesOnDataErrors,
                    ValidatesOnExceptions = ValidatesOnExceptions,
                    FallbackValue = FallbackValue
                };

                if (StringFormat is not null)
                {
                    mBinding.StringFormat = StringFormat;
                }

                foreach (PathAppearance? path in bindingPathes)
                {
                    Binding binding = new Binding();
                    String pathValue = path.Id.Value;

                    AdjustRelativeSource(serviceProvider, binding, path.Id.RelativeSource);

                    if (path.Id.Type == ActiveBindingPathTokenType.StaticProperty)
                    {
                        pathValue = String.Format("({0})", pathValue); // need to use brackets for Static property recognition in standart binding
                    }

                    PropertyPath? resPath = (PropertyPath?) new PropertyPathConverter().ConvertFromString(typeDescriptor, pathValue);

                    binding.Path = resPath;

                    if (Source is not null)
                    {
                        binding.Source = Source;
                    }

                    if (ElementName is not null)
                    {
                        binding.ElementName = ElementName;
                    }

                    //if (RelativeSource is not null)
                    //    binding.RelativeSource = RelativeSource;

                    mBinding.Bindings.Add(binding);
                }

                resBinding = mBinding;
            }

            return resBinding.ProvideValue(serviceProvider);
        }

        private static void ReplaceExpressionParser(IActiveBindingExpressionParser expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            Parser.Reset(expression);
        }

        private Type? GetPropertyType(IServiceProvider provider)
        {
            if (provider.GetService(typeof(IProvideValueTarget)) is not IProvideValueTarget target)
            {
                return null;
            }

            if (target.TargetProperty is DependencyProperty property)
            {
                return property.PropertyType;
            }

            return target.TargetProperty.GetType();
        }

        private String GetExpressionTemplate(String path, IReadOnlyList<PathAppearance> properties, out Dictionary<String, Type> parameters)
        {
            Dictionary<ActiveBindingPathTokenId, String> passing = new Dictionary<ActiveBindingPathTokenId, String>();
            Dictionary<ActiveBindingPathTokenId, String> names = new Dictionary<ActiveBindingPathTokenId, String>();
            parameters = new Dictionary<String, Type>();

            Int32 index = 0;
            StringBuilder result = new StringBuilder();
            while (index < path.Length)
            {
                Boolean replaced = false;
                foreach (PathAppearance appearance in properties)
                {
                    ActiveBindingPathTokenId id = appearance.Id;
                    ActiveBindingToken? target = appearance.Tokens.FirstOrDefault(token => token.Start == index);

                    if (target is null)
                    {
                        continue;
                    }

                    String property = id.RelativeSource is null ? id.Value : $"{id.Value}@{id.RelativeSource}";

                    if (id.Type == ActiveBindingPathTokenType.Property || id.Type == ActiveBindingPathTokenType.StaticProperty)
                    {
                        String? replace = null;
                        if (passing.ContainsKey(id))
                        {
                            replace = passing[id];
                        }
                        else
                        {
                            replace = passing.Count.ToString("{0}");
                            passing.Add(id, replace);
                        }

                        result.Append(replace);
                        index += property.Length;
                        replaced = true;
                    }
                    else if (id.Type == ActiveBindingPathTokenType.Enum)
                    {
                        ActiveBindingEnumToken? enumPath = appearance.Tokens.First() as ActiveBindingEnumToken;

                        String enumTypeName = null;
                        if (names.ContainsKey(id))
                        {
                            enumTypeName = names[id];
                        }
                        else
                        {
                            enumTypeName = GetEnumName(names.Count);
                            names.Add(id, enumTypeName);
                            parameters.Add(enumTypeName, enumPath.Enum);
                        }

                        result.Append(String.Join(".", enumTypeName, enumPath.Member));
                        index += property.Length;
                        replaced = true;
                    }

                    if (replaced)
                    {
                        break;
                    }
                }

                if (replaced)
                {
                    continue;
                }

                result.Append(path[index]);
                index++;
            }

            return result.ToString();
        }
    }
}