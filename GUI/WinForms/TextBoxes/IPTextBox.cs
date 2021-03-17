// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Windows.Forms;
using NetExtender.Utils.Network;
using NetExtender.Utils.Types;

namespace NetExtender.GUI.WinForms.TextBoxes
{
    public class IPTextBox : HidenTextBox
    {
        private String _defaultHost = @"127.0.0.1";

        public String DefaultHost
        {
            get
            {
                return _defaultHost;
            }
            set
            {
                if (NetworkUtils.ValidateIPv4(value))
                {
                    _defaultHost = value;
                }
            }
        }

        public override String Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = String.IsNullOrEmpty(value) || !NetworkUtils.ValidateIPv4(value) ? DefaultHost : value;
            }
        }

        public IPTextBox()
        {
            Validate = NetworkUtils.ValidateIPv4;
            HandleCreated += OnCreate;
            Leave += OnLeave;
        }
        
        private void OnCreate(Object? sender, EventArgs e)
        {
            MaxLength = 15;
            PasswordChar = ResetPasswordChar;
        }
        
        private void OnLeave(Object? sender, EventArgs e)
        {
            Text = IsValid ? Text : DefaultHost;
        }
        
        protected override Boolean IsAllowedChar(Char c)
        {
            if (Char.IsControl(c))
            {
                return true;
            }

            if (Char.IsDigit(c) && Text.Split(".").LastOr(String.Empty).Length < 3)
            {
                return true;
            }

            return c == '.' && Text.Length > 0 && Text[^1] != '.' && Text.Count(chr => chr == '.') < 3;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (IsAllowedChar(e.KeyChar))
            {
                base.OnKeyPress(e);
                return;
            }

            e.Handled = true;
        }
    }
}