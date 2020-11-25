// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NetExtender.GUI.WinForms.Forms
{
    public class SubForm : FlashForm
    {
        private TabAlignment _alignment;

        public event EmptyHandler AlignmentChanged;

        public TabAlignment Alignment
        {
            get
            {
                return _alignment;
            }
            set
            {
                if (_alignment == value)
                {
                    return;
                }

                _alignment = value;
                AlignmentChanged?.Invoke();
            }
        }

        private readonly Form _baseForm;

        public SubForm(Form baseForm)
        {
            _baseForm = baseForm;
            _baseForm.VisibleChanged += (sender, args) => Visible = _baseForm.Visible;
            _baseForm.EnabledChanged += (sender, args) => Enabled = _baseForm.Enabled;
            _baseForm.SizeChanged += (sender, args) => OnBaseFormMove();
            _baseForm.LocationChanged += (sender, args) => OnBaseFormMove();

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            TopLevel = true;
            ShowInTaskbar = false;
            SizeChanged += (sender, args) => OnBaseFormMove();
            AlignmentChanged += OnBaseFormMove;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            OnBaseFormMove();
        }

        protected void OnBaseFormMove()
        {
            Point location = Alignment switch
            {
                TabAlignment.Left => new Point(_baseForm.Location.X - ClientSize.Width, _baseForm.Location.Y),
                TabAlignment.Top => new Point(_baseForm.Location.X, _baseForm.Location.Y - ClientSize.Height),
                _ => new Point(_baseForm.Location.X + _baseForm.ClientSize.Width, _baseForm.Location.Y)
            };

            Location = location;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Visible = false;
        }


        [DllImport("user32.dll")]
        private static extern Int32 EnableMenuItem(IntPtr hMenu, Int32 uIDEnableItem, Int32 uEnable);

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        protected override void WndProc(ref Message m)
        {
            const Int32 HTCAPTION = 0x00000002;
            const Int32 MF_BYCOMMAND = 0x00000000;
            const Int32 MF_GRAYED = 0x00000001;
            const Int32 MF_DISABLED = 0x00000002;
            const Int32 SC_MOVE = 0xF010;
            const Int32 WM_NCLBUTTONDOWN = 0xA1;
            const Int32 WM_SYSCOMMAND = 0x112;
            const Int32 WM_INITMENUPOPUP = 0x117;
            switch (m.Msg)
            {
                case WM_INITMENUPOPUP:
                {
                    if (m.LParam.ToInt32() / 65536 != 0) // 'divide by 65536 to get hiword
                    {
                        EnableMenuItem(m.WParam, SC_MOVE, MF_BYCOMMAND | MF_DISABLED | MF_GRAYED);
                    }

                    break;
                }

                //cancels the drag this is IMP
                case WM_NCLBUTTONDOWN when m.WParam.ToInt32() == HTCAPTION:

                // Cancels any clicks on move menu
                case WM_SYSCOMMAND when (m.WParam.ToInt32() & 0xFFF0) == SC_MOVE:
                    return;
            }

            base.WndProc(ref m);
        }

        public new void ShowDialog()
        {
            Show();
        }
    }
}