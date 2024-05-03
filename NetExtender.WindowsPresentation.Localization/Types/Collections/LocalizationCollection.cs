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