// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using NetExtender.Configuration.Common;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Types.Culture;
using NetExtender.Utilities.Types;

namespace NetExtender.Localization.Common
{
    public class LocalizationConverter : ILocalizationConverter
    {
        public static LocalizationConverter Default { get; } = new LocalizationConverter();

        [return: NotNullIfNotNull("sections")]
        public virtual IEnumerable<String>? Convert(LocalizationIdentifier identifier, IEnumerable<String>? sections, LocalizationOptions options)
        {
            Convert(null, identifier, ref sections, options);
            return sections;
        }

        [return: NotNullIfNotNull("sections")]
        public virtual IEnumerable<String>? Convert(String? key, IEnumerable<String>? sections, LocalizationOptions options)
        {
            if (key is not null)
            {
                sections = sections.AppendOr(key);
            }

            return sections;
        }

        public virtual String Convert(String? key, LocalizationIdentifier identifier, ref IEnumerable<String>? sections, LocalizationOptions options)
        {
            sections = Convert(key, sections, options);

            if (!identifier.TryGetCultureInfo(out CultureInfo info))
            {
                return identifier.ToString();
            }

            return options.HasFlag(LocalizationOptions.ThreeLetterName) ? info.ThreeLetterISOLanguageName : info.TwoLetterISOLanguageName;
        }

        public virtual Boolean Extract(ConfigurationEntry entry, out LocalizationEntry result)
        {
            return LocalizationEntry.TryConvert(entry, out result);
        }

        public virtual Boolean Extract(ConfigurationValueEntry entry, out LocalizationValueEntry result)
        {
            return LocalizationValueEntry.TryConvert(entry, out result);
        }

        [return: NotNullIfNotNull("entries")]
        public IEnumerable<LocalizationEntry>? Extract(IEnumerable<ConfigurationEntry>? entries)
        {
            return entries?.TryParse<ConfigurationEntry, LocalizationEntry>(Extract);
        }

        [return: NotNullIfNotNull("entries")]
        public IEnumerable<LocalizationValueEntry>? Extract(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return entries?.TryParse<ConfigurationValueEntry, LocalizationValueEntry>(Extract);
        }

        public virtual Boolean Pack(LocalizationEntry entry, out ConfigurationEntry result)
        {
            result = entry;
            return true;
        }

        public virtual Boolean Pack(LocalizationValueEntry entry, out ConfigurationValueEntry result)
        {
            result = entry;
            return true;
        }

        public virtual Boolean Pack(LocalizationMultiValueEntry entry, out ConfigurationValueEntry[] result)
        {
            result = entry;
            return true;
        }

        [return: NotNullIfNotNull("entries")]
        public IEnumerable<ConfigurationEntry>? Pack(IEnumerable<LocalizationEntry>? entries)
        {
            return entries?.TryParse<LocalizationEntry, ConfigurationEntry>(Pack);
        }

        [return: NotNullIfNotNull("entries")]
        public IEnumerable<ConfigurationValueEntry>? Pack(IEnumerable<LocalizationValueEntry>? entries)
        {
            return entries?.TryParse<LocalizationValueEntry, ConfigurationValueEntry>(Pack);
        }

        [return: NotNullIfNotNull("entries")]
        public IEnumerable<ConfigurationValueEntry>? Pack(IEnumerable<LocalizationMultiValueEntry>? entries)
        {
            return entries?.TryParse<LocalizationMultiValueEntry, ConfigurationValueEntry[]>(Pack).SelectMany();
        }
    }
}