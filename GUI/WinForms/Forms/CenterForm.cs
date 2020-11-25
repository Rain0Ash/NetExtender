// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.GUI.WinForms.Forms
{
    public abstract class CenterForm : HelpToolTipForm
    {
        protected CenterForm()
        {
            Load += OnLoad;
        }

        protected virtual void OnLoad(Object sender, EventArgs args)
        {
            CenterTo();
        }

        protected virtual void CenterTo()
        {
            CenterToScreen();
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                Load -= OnLoad;
            }
            
            base.Dispose(disposing);
        }
    }
}