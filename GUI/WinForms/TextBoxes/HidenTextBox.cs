// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.GUI.WinForms.TextBoxes
{
    public class HidenTextBox : ValidableTextBox
    {
        private const Char ResetChar = '\0';

        private event EmptyHandler PasswdCharChanged;
        private Char _passwdChar = '*';

        public Char PasswdChar
        {
            get
            {
                return _passwdChar;
            }
            set
            {
                if (_passwdChar == value)
                {
                    return;
                }

                _passwdChar = value;
                PasswdCharChanged?.Invoke();
            }
        }

        public HidenTextBox()
        {
            PasswordChar = PasswdChar;

            Enter += (sender, args) => PasswordChar = ResetChar;

            Leave += (sender, args) => PasswordChar = PasswdChar;

            PasswdCharChanged += () => PasswordChar = PasswdChar;
        }
    }
}