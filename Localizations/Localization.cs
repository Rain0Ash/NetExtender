// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using NetExtender.Utils.Types;
using NetExtender.Cultures;
using NetExtender.Cultures.Comparers;
using NetExtender.Types.Culture;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Types.Maps;
using NetExtender.Types.Strings;
using ReactiveUI;

namespace NetExtender.Localizations
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public abstract class Localization : ReactiveObject
    {
        private static event EmptyHandler LChanged;

        public static event EmptyHandler LanguageChanged
        {
            add
            {
                if (LChanged?.GetInvocationList().Contains(value) == true)
                {
                    return;
                }

                LChanged += value;
            }
            remove
            {
                LChanged -= value;
            }
        }

        public static String NewLine
        {
            get
            {
                return Environment.NewLine;
            }
        }

        static Localization()
        {
            DefaultCulture = new Culture(LCID.En) {CustomName = "English"};

            List<Culture> array = new List<Culture>
            {
                DefaultCulture,
                new Culture(LCID.Ru) {CustomName = "Русский"},
                new Culture(LCID.De) {CustomName = "Deutsch"},
                new Culture(LCID.Fr) {CustomName = "Française"}
            };

            CultureLCIDDictionary =
                new IndexDictionary<Int32, Culture>(array.Select(culture =>
                    new KeyValuePair<Int32, Culture>(culture.LCID, culture)));

            CodeByLCIDMap = new Map<Int32, String>(CultureLCIDDictionary.ToDictionary(pair => pair.Value.LCID, pair => pair.Value.Code.ToLower()));

            DefaultComparer = new CultureComparer(CultureLCIDDictionary.Select(pair => pair.Value));

            SystemCulture = CultureLCIDDictionary.TryGetValue(CultureInfo.CurrentUICulture.LCID, DefaultCulture);

            CurrentCulture = SystemCulture;
        }

        public static CultureComparer DefaultComparer { get; }

        private static readonly IndexDictionary<Int32, Culture> CultureLCIDDictionary;

        private static readonly Map<Int32, String> CodeByLCIDMap;

        public static IReadOnlyMap<Int32, String> CodeByLCID
        {
            get
            {
                return CodeByLCIDMap;
            }
        }

        public static IReadOnlyIndexDictionary<Int32, Culture> CultureByLCID
        {
            get
            {
                return CultureLCIDDictionary;
            }
        }

        public static void AddLanguage(Int32 lcid, Culture culture)
        {
            CultureLCIDDictionary.Add(lcid, culture);
            CodeByLCIDMap.Add(lcid, culture.Code);
        }

        public static void RemoveLanguage(Int32 lcid)
        {
            CultureLCIDDictionary.Remove(lcid);
            CodeByLCIDMap.Remove(lcid);
        }

        public static Culture DefaultCulture { get; }

        public static Culture SystemCulture { get; }

        public static Culture BasicCulture
        {
            get
            {
                return UseSystemCulture && CultureByLCID.ContainsKey(SystemCulture.LCID) ? SystemCulture : DefaultCulture;
            }
        }

        public static Culture CurrentCulture { get; protected set; }

        public static Boolean ChangeUIThreadLanguage { get; set; } = true;

        public static Boolean UseSystemCulture { get; set; } = true;

        public static IEnumerable<Int32> AvailableLocalization { get; private set; }
        public static IReadOnlyList<Culture> Cultures { get; private set; }

        protected Localization(LocaleMultiString multiString)
            : this(0, multiString)
        {
        }

        protected Localization(Int32 lcid, LocaleMultiString multiString = null)
        {
            multiString ??= new LocaleMultiString();
            AvailableLocalization = multiString.AvailableLocalization;
            Cultures = multiString.Cultures;
            InitializeLanguage();
            StringsNotify();
            UpdateLocalization(lcid);
        }

        protected virtual void InitializeLanguage()
        {
            //Override by language strings;
        }

        private void StringsNotify()
        {
            foreach (PropertyInfo info in GetType().GetProperties())
            {
                if (info.PropertyType == typeof(MultiString) || info.PropertyType.IsSubclassOf(typeof(MultiString)))
                {
                    LanguageChanged += () => this.RaisePropertyChanged(info.Name);
                }
            }
        }

        public static void UpdateLocalization(Int32 lcid)
        {
            if (!CultureByLCID.ContainsKey(lcid) || !AvailableLocalization.Contains(lcid))
            {
                lcid = BasicCulture.LCID;
            }

            if (CurrentCulture.LCID == lcid)
            {
                return;
            }

            CurrentCulture = CultureByLCID[lcid];

            if (ChangeUIThreadLanguage)
            {
                SetUILanguage();
            }

            LChanged?.Invoke();
        }

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern UInt16 SetThreadUILanguage(UInt16 langId);

        public static void SetUILanguage()
        {
            UInt16 lcid;
            try
            {
                lcid = CurrentCulture.LCID16;
            }
            catch (Exception)
            {
                lcid = DefaultCulture.LCID16;
            }

            SetUILanguage(lcid);
        }

        public static void SetUILanguage(UInt16 lcid)
        {
            SetThreadUILanguage(lcid);
        }

        public static Int32 GetLanguageOrderID(Int32 lcid)
        {
            return DefaultComparer.GetLanguageOrderID(lcid);
        }

        public static String GetCultureCode()
        {
            return CurrentCulture.Code;
        }

        public static String GetCultureCode(Int32 lcid)
        {
            return CodeByLCID.TryGetValue(lcid, out String code) ? code : DefaultCulture.Code;
        }
    }
}