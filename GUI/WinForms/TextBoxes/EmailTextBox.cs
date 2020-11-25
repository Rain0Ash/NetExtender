// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Utils.Types;

namespace NetExtender.GUI.WinForms.TextBoxes
{
    public class EmailTextBox : HidenTextBox
    {
        public EmailTextBox()
        {
            Validate = EmailUtils.CheckValidEmail;
            PasswdChar = '\0';
        }
    }
}