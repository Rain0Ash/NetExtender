using System.Windows.Media.Imaging;
using NetExtender.Localization.Types.Flags;
using NetExtender.Types.Culture;
using NetExtender.WindowsPresentation.Localization.Utilities;

namespace NetExtender.WindowsPresentation.Localization.Types.Flags
{
    public sealed record LocalizationFlagBitmapSourceEntry : LocalizationFlagEntry<BitmapSource>
    {
        public static implicit operator LocalizationFlagBitmapSourceEntry(LocalizationIdentifier value)
        {
            return new LocalizationFlagBitmapSourceEntry(value);
        }
        
        static LocalizationFlagBitmapSourceEntry()
        {
            WindowsPresentationLocalizationFlagsUtilities.Initialize();
        }

        public LocalizationFlagBitmapSourceEntry(LocalizationIdentifier identifier)
            : base(identifier)
        {
        }
        
        public new static LocalizationFlagBitmapSourceEntry Convert(LocalizationIdentifier identifier)
        {
            return identifier;
        }
    }
}