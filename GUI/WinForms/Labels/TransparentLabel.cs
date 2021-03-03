// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Windows.Forms;
using NetExtender.GUI.WinForms.Controls;

namespace NetExtender.GUI.WinForms.Labels
{
    public class TransparentLabel : FixedControl
    {
        public TransparentLabel()
        {
            TabStop = false;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // do nothing
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawText();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x000F)
            {
                DrawText();
            }
        }

        private void DrawText()
        {
            Graphics graphics = CreateGraphics();
            SolidBrush brush = new SolidBrush(ForeColor);
            SizeF size = graphics.MeasureString(Text, Font);

            // first figure out the top
            Single top = _textAlign switch
            {
                ContentAlignment.MiddleLeft => (Height - size.Height) / 2,
                ContentAlignment.MiddleCenter => (Height - size.Height) / 2,
                ContentAlignment.MiddleRight => (Height - size.Height) / 2,
                ContentAlignment.BottomLeft => Height - size.Height,
                ContentAlignment.BottomCenter => Height - size.Height,
                ContentAlignment.BottomRight => Height - size.Height,
                _ => 0
            };

            Single left = -1;
            switch (_textAlign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    if (RightToLeft == RightToLeft.Yes)
                    {
                        left = Width - size.Width;
                    }
                    else
                    {
                        left = -1;
                    }

                    break;
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    left = (Width - size.Width) / 2;
                    break;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    if (RightToLeft == RightToLeft.Yes)
                    {
                        left = -1;
                    }
                    else
                    {
                        left = Width - size.Width;
                    }

                    break;
            }

            graphics.DrawString(Text, Font, brush, left, top);
            graphics.Dispose();
            brush.Dispose();
        }

        public override String Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                RecreateHandle();
            }
        }

        public override RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }
            set
            {
                base.RightToLeft = value;
                RecreateHandle();
            }
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                RecreateHandle();
            }
        }

        private ContentAlignment _textAlign = ContentAlignment.TopLeft;

        public ContentAlignment TextAlign
        {
            get
            {
                return _textAlign;
            }
            set
            {
                _textAlign = value;
                RecreateHandle();
            }
        }
    }
}