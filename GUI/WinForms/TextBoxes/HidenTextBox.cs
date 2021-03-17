// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.GUI.WinForms.TextBoxes
{
    public class HidenTextBox : ValidableTextBox
    {
        protected const Char ResetPasswordChar = '\0';
        protected const Char DefaultPasswordChar = '*';
        
        private Char _password = DefaultPasswordChar;
        
        public override Char PasswordChar
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password == value)
                {
                    return;
                }

                _password = value;
                OnPasswordCharChanged(EventArgs.Empty);
            }
        }

        public HidenTextBox()
        {
            Enter += OnEnter;
            Leave += OnLeave;
        }
        
        private void OnEnter(Object? sender, EventArgs e)
        {
            base.PasswordChar = ResetPasswordChar;
        }
        
        private void OnLeave(Object? sender, EventArgs e)
        {
            base.PasswordChar = PasswordChar;
        }
    }
}