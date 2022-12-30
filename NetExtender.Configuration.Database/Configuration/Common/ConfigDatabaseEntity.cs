// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Configuration.Database.Configuration.Common
{
    public record ConfigDatabaseEntity
    {
        [Key]
        public String Key { get; private set; } = null!;
        public String? Value { get; set; }

        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
        public Boolean IsEmpty
        {
            get
            {
                return Key is null;
            }
        }

        protected ConfigDatabaseEntity()
        {
        }

        public ConfigDatabaseEntity(String key)
            : this(key, null)
        {
        }

        public ConfigDatabaseEntity(String key, String? value)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Value = value;
        }
    }
}