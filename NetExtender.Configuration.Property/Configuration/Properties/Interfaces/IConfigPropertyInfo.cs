// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.ComponentModel;
using NetExtender.Configuration.Common;

namespace NetExtender.Configuration.Properties.Interfaces
{
    public interface IConfigPropertyInfo : INotifyPropertyChanged, IDisposable
    {
        public String Path { get; }
        public String? Key { get; }
        public ImmutableArray<String> Sections { get; }
        public ConfigPropertyOptions Options { get; }
        public Boolean HasValue { get; }
        public Boolean IsCaching { get; }
        public Boolean IsThrowWhenValueSetInvalid { get; }
        public Boolean IsReadOnly { get; }
        public Boolean IsIgnoreEvent { get; }
        public Boolean IsDisableSave { get; }
        public Boolean IsAlwaysDefault { get; }
    }
}