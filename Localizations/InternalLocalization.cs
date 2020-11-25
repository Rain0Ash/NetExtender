// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Linq;
using NetExtender.Cultures;
using NetExtender.Localizations.Interfaces;

namespace NetExtender.Localizations
{
    public delegate void LanguageChangedHandler(Culture culture);
    
    internal class InternalLocalization : ILocalization
    {
        private event LanguageChangedHandler Changed;
        
        public event EmptyHandler LanguageChanged;
        public event LanguageChangedHandler LanguageCultureChanged
        {
            add
            {
                if (Changed?.GetInvocationList().Contains(value) == true)
                {
                    return;
                }

                Changed += value;
            }
            remove
            {
                Changed -= value;
            }
        }

        public InternalLocalization()
        {
            LanguageCultureChanged += OnLanguageCultureChanged;
        }

        private void OnLanguageCultureChanged(Culture _)
        {
            LanguageChanged?.Invoke();
        }

        public void Dispose()
        {
            Changed = null;
        }
    }
}