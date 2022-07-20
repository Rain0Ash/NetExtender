// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.UserInterface.WindowsPresentation.Windows
{
    public abstract class LocalizationWindow : CenterWindow
    {
        public abstract WindowLocalizationAbstraction Localization { get; }
    }

    public abstract class WindowLocalizationAbstraction
    {
    }

    public abstract class WindowLocalizationSingleton<TWindowLocalization> : WindowLocalizationAbstraction where TWindowLocalization : WindowLocalizationAbstraction, new()
    {
        public static TWindowLocalization Instance { get; } = new TWindowLocalization();
    }
}