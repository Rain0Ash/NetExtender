// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Utils.OS;

namespace NetExtender.GUI.WinForms.Buttons
{
    public class WebBrowserButton : FixedButton
    {
        public String Url { get; set; }

        public WebBrowserButton()
        {
            Click += OnClick;
        }

        private void OnClick(Object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Url))
            {
                return;
            }
            
            ProcessUtils.OpenBrowser(Url);
        }

        protected override void Dispose(Boolean disposing)
        {
            Click -= OnClick;
            base.Dispose(disposing);
        }
    }
}