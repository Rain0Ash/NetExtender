// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Utils.Numerics;
using NetExtender.Utils.Types;
using NetExtender.Cultures;
using NetExtender.Cultures.Comparers;

namespace NetExtender.Localizations
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
    public class LocaleStrings : Strings
    {
        public static CultureComparer Comparer
        {
            get
            {
                return Localization.DefaultComparer;
            }
        }

        protected const String StringMissing = @"String is missing";

        internal static IEnumerable<String> DefaultLocalization { get; } = new[] {Localization.DefaultCulture.Code};

        public virtual ImmutableArray<Int32> AvailableLocalization
        {
            get
            {
                return DefaultLocalization.Select(GetLCID).ToImmutableArray();
            }
        }

        public IImmutableList<Culture> Cultures { get; }
        protected readonly Dictionary<Int32, String> Languages;

        protected static Int32 GetLCID(String code)
        {
            return Localization.CodeByLCID.TryGetKey(code, Localization.DefaultCulture.LCID);
        }

        private static readonly Int32 DefaultLCID = GetLCID(Localization.DefaultCulture.Code);

        public Int32 FormatArgsCount { get; }
        
        private String Default
        {
            get
            {
                return Languages.TryGetValue(DefaultLCID, StringMissing) ?? StringMissing;
            }
            set
            {
                Languages[DefaultLCID] = value ?? StringMissing;
            }
        }

        protected void CheckFormatArgs(Int32 args, String str, String arg = null)
        {
            if (args < 0)
            {
                throw new ArgumentException(nameof(args));
            }

            if (str is null)
            {
                return;
            }
            
            if (str.FormatArgsExpected() != args)
            {
                throw new ArgumentException($"Count of expected args {FormatArgsCount} {arg ?? "arg"} not contains this count of expected args");
            }
        }
        
        public LocaleStrings()
            : this(0)
        {
        }

        public LocaleStrings([NotNull] String @default)
            : this(@default.FormatArgsExpected(), @default)
        {
        }

        public LocaleStrings(Int32 args = 0, [NotNull] String @default = StringMissing)
        {
            FormatArgsCount = args.ToRange();
            CheckFormatArgs(FormatArgsCount, @default, nameof(@default));
            
            Languages = AvailableLocalization.ToDictionary(lcid => lcid, lcid => (String) null);
            Cultures = Languages.Keys.Select(lcid => Localization.CultureByLCID[lcid]).OrderBy(culture => culture.Code, Comparer).ToImmutableList();
            Default = @default ?? StringMissing;
            
            Localization.LanguageChanged += ChangeText;
        }

        private void ChangeText()
        {
            Text = this;
        }

        public override String ToString()
        {
            return ToString(Localization.CurrentCulture.LCID);
        }
        
        public String ToString(Int32 lcid)
        {
            return Languages.TryGetValue(lcid, Default) ?? Default;
        }

        public override void Dispose()
        {
            Localization.LanguageChanged -= ChangeText;
        }

        ~LocaleStrings()
        {
            Dispose();
        }
    }
}