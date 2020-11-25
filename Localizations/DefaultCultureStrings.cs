// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace NetExtender.Localizations
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
    public class DefaultCultureStrings : LocaleStrings
    {
        private static readonly ImmutableArray<Int32> _availableLocalization = new[] {"EN", "RU", "DE"}
            .Select(code => GetLCID(code.ToLower()))
            .ToImmutableArray();

        public override ImmutableArray<Int32> AvailableLocalization
        {
            get
            {
                return _availableLocalization;
            }
        }

        private static readonly Int32 RU = GetLCID("ru");

        public String ru
        {
            get
            {
                return ToString(RU);
            }
            private set
            {
                Languages[RU] = value;
            }
        }

        private static readonly Int32 DE = GetLCID("de");

        public String de
        {
            get
            {
                return ToString(DE);
            }
            private set
            {
                Languages[DE] = value;
            }
        }

        public DefaultCultureStrings()
            : this(null)
        {
        }

        public DefaultCultureStrings(String english, String russian = null, String deutch = null)
            : base(english)
        {
            ru = russian;
            de = deutch;
        }
    }
}