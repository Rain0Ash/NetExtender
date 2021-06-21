// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;

namespace NetExtender.UserInterface.WinForms.Forms
{
    public abstract class ChildForm : CenterForm
    {
        protected ChildForm()
        {
            Shown += OnShown;
        }

        protected Form? GetLastParentForm()
        {
            Control parent = Parent;
            while (parent is not null)
            {
                if (parent is Form form)
                {
                    return form;
                }

                parent = parent.Parent;
            }

            return null;
        }
        
        private void OnShown(Object? sender, EventArgs args)
        {
            Icon = Owner?.Icon ?? GetLastParentForm()?.Icon ?? Icon;
        }

        protected override void CenterTo()
        {
            CenterToParent();
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                Shown -= OnShown;
            }
            
            base.Dispose(disposing);
        }
    }
}