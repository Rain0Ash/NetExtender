// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.UserInterface.WinForms.Forms
{
    public abstract class CenterForm : HelpToolTipForm
    {
        protected CenterForm()
        {
            Load += SetSizeTo;
            Load += CenterTo;
        }

        protected virtual void SetSizeTo(Object? sender, EventArgs args)
        {
        }

        protected virtual void CenterTo(Object? sender, EventArgs args)
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
                Load -= CenterTo;
            }

            base.Dispose(disposing);
        }
    }
}