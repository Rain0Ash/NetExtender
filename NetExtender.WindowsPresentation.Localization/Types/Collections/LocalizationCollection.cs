// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using NetExtender.Types.Collections;
using NetExtender.WindowsPresentation.Localization.Types.Flags;

namespace NetExtender.WindowsPresentation.Localization.Types.Collections
{
    public class LocalizationCollection : SuppressObservableCollection<LocalizationFlagBitmapSourceEntry>
    {
        public LocalizationCollection()
        {
        }

        public LocalizationCollection(IEnumerable<LocalizationFlagBitmapSourceEntry> collection)
            : base(collection)
        {
        }

        public LocalizationCollection(List<LocalizationFlagBitmapSourceEntry> list)
            : base(list)
        {
        }
    }
}