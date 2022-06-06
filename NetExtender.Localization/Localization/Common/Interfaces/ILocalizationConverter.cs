// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Configuration.Common;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Common.Interfaces
{
    public interface ILocalizationConverter
    {
        [return: NotNullIfNotNull("sections")]
        public IEnumerable<String>? Convert(LocalizationIdentifier identifier, IEnumerable<String>? sections, LocalizationOptions options);

        [return: NotNullIfNotNull("sections")]
        public IEnumerable<String>? Convert(String? key, IEnumerable<String>? sections, LocalizationOptions options);
        public String Convert(String? key, LocalizationIdentifier identifier, ref IEnumerable<String>? sections, LocalizationOptions options);
        
        public Boolean Extract(ConfigurationEntry entry, out LocalizationEntry result);
        public Boolean Extract(ConfigurationValueEntry entry, out LocalizationValueEntry result);
        
        [return: NotNullIfNotNull("entries")]
        public IEnumerable<LocalizationEntry>? Extract(IEnumerable<ConfigurationEntry>? entries);
        
        [return: NotNullIfNotNull("entries")]
        public IEnumerable<LocalizationValueEntry>? Extract(IEnumerable<ConfigurationValueEntry>? entries);
        
        public Boolean Pack(LocalizationEntry entry, out ConfigurationEntry result);
        public Boolean Pack(LocalizationValueEntry entry, out ConfigurationValueEntry result);
        public Boolean Pack(LocalizationMultiValueEntry entry, out ConfigurationValueEntry[] result);
        
        [return: NotNullIfNotNull("entries")]
        public IEnumerable<ConfigurationEntry>? Pack(IEnumerable<LocalizationEntry>? entries);
        
        [return: NotNullIfNotNull("entries")]
        public IEnumerable<ConfigurationValueEntry>? Pack(IEnumerable<LocalizationValueEntry>? entries);
        
        [return: NotNullIfNotNull("entries")]
        public IEnumerable<ConfigurationValueEntry>? Pack(IEnumerable<LocalizationMultiValueEntry>? entries);
    }
}