// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NetExtender.Utils.Numerics;
using NetExtender.Localizations;
using NetExtender.Loggers.Messages;
using NetExtender.Types.Lists;
using NetExtender.Utils.OS;

namespace NetExtender.GUI.WinForms.RichTextBoxes
{
    public class LoggerRichTextBox : FixedRichTextBox
    {
        private readonly EventQueueList<LogMessage> _messages = new EventQueueList<LogMessage>();

        public Int32 MaximumLength
        {
            get
            {
                return _messages.MaximumLength;
            }
            set
            {
                _messages.MaximumLength = value.ToRange();
                UpdateLog();
            }
        }

        private Boolean _reversed = true;

        public Boolean Reversed
        {
            get
            {
                return _reversed;
            }
            set
            {
                if (_reversed == value)
                {
                    return;
                }

                _reversed = value;
                UpdateLog();
            }
        }

        public LoggerRichTextBox()
        {
            ReadOnly = true;
            Localization.LanguageChanged += UpdateLog;
        }

        /*protected override void WndProc(ref Message m) {
            if(m.Msg == 0x0007) m.Msg = 0x0008;

            base.WndProc (ref m);
        }*/

        protected override void OnLinkClicked(LinkClickedEventArgs e)
        {
            ProcessUtils.OpenBrowser(e.LinkText);
            base.OnLinkClicked(e);
        }

        public void UpdateLog()
        {
            Clear();
            Boolean first = true;
            foreach (LogMessage item in Reversed ? (_messages?.AsEnumerable() ?? Array.Empty<LogMessage>()).Reverse() : _messages)
            {
                if (first)
                {
                    Display(item, item.GetColor(), false);
                    first = false;
                    continue;
                }

                Display(item, item.GetColor(), item.NewLine);
            }
        }

        public void ClearLog()
        {
            _messages.Clear();
            UpdateLog();
        }

        public void Log(LogMessage logMessage)
        {
            _messages.Add(logMessage);
            UpdateLog();
        }

        private void Display(String message, Color color, Boolean newLine)
        {
            if (newLine)
            {
                message = Environment.NewLine + message;
            }

            SelectionStart = TextLength;
            SelectionLength = 0;

            SelectionColor = color;
            AppendText(message);
            SelectionColor = ForeColor;
            SelectionStart = Reversed ? 0 : Text.Length;
            ScrollToCaret();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            UpdateLog();
        }

        protected override void Dispose(Boolean disposing)
        {
            Clear();
            Localization.LanguageChanged -= UpdateLog;
            base.Dispose(disposing);
        }
    }
}