// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using NetExtender.Apps.Domains.GUIViews.Common;

namespace NetExtender.Apps.Domains.GUIViews.WPF
{
    public class AppWPFView : AppGUIView
    {
        private protected override void InitializeInternal()
        {
            Domain.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
    }
}