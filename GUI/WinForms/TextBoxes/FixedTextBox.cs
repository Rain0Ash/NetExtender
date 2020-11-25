// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NetExtender.Utils.Numerics;
using NetExtender.GUI.Common;

namespace NetExtender.GUI.WinForms.TextBoxes
{
    public class FixedTextBox : TextBox
    {
        public event EmptyHandler ActionTypeChanged;

        private ActionType _actionType = ActionType.All;

        public ActionType ActionType
        {
            get
            {
                return _actionType;
            }
            set
            {
                if (_actionType == value)
                {
                    return;
                }

                _actionType = value;
                ActionTypeChanged?.Invoke();
            }
        }

        protected override void WndProc(ref Message m)
        {
            // override paste

            switch (m.Msg)
            {
                case 0x300:
                    if (!ActionType.HasFlag(ActionType.Cut))
                    {
                        return;
                    }

                    base.WndProc(ref m);
                    break;
                case 0x301:
                    if (!ActionType.HasFlag(ActionType.Copy))
                    {
                        return;
                    }

                    base.WndProc(ref m);
                    break;
                case 0x302:
                    if (!ActionType.HasFlag(ActionType.Paste) || !Clipboard.ContainsText())
                    {
                        return;
                    }

                    IEnumerable<Char> allowedChar = Clipboard.GetText().Where(IsAllowedChar);

                    if (MaxLength > 0)
                    {
                        allowedChar = allowedChar.Take(MaxLength - Text.Length + SelectedText.Length);
                    }

                    SelectedText = String.Join(String.Empty, allowedChar);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected virtual Boolean IsAllowedChar(Char c)
        {
            return true;
        }

        public void Replace(Dictionary<String, String> replaceDictionary)
        {
            foreach ((String key, String value) in replaceDictionary)
            {
                Replace(key, value);
            }
        }

        public void Replace(Dictionary<Regex, String> replaceDictionary)
        {
            foreach ((Regex key, String value) in replaceDictionary)
            {
                Replace(key, value);
            }
        }

        public void Replace(String pattern, String replacement, Int32 count = -1, Boolean isRegexPattern = false,
            RegexOptions options = RegexOptions.None)
        {
            Replace(new Regex(isRegexPattern ? pattern : $"({pattern})", options), replacement, count);
        }

        public void Replace(Regex regex, String replacement, Int32 count = -1, Boolean recursive = false)
        {
            while (true)
            {
                if (count == 0)
                {
                    return;
                }

                count = count.ToRange(1, Int32.MaxValue, true);

                String text = Text;
                Int32 selectionStart = SelectionStart;
                Int32 selectionLength = SelectionLength;

                Int32 offset = 0;

                Int32 current = 0;

                MatchCollection matches = regex.Matches(text);

                if (matches.Count <= 0)
                {
                    return;
                }

                foreach (Match match in matches)
                {
                    text = $"{text.Substring(0, match.Index)}{replacement}{text.Substring(match.Index + match.Length + offset)}";

                    if (match.Index + match.Length < selectionStart)
                    {
                        selectionStart += replacement.Length - match.Length;
                    }

                    offset += replacement.Length - match.Length;

                    current++;

                    if (current >= count)
                    {
                        break;
                    }
                }

                Text = text;
                SelectionStart = selectionStart.ToRange();
                SelectionLength = selectionLength.ToRange();

                if (recursive)
                {
                    continue;
                }

                break;
            }
        }

        public FixedTextBox()
        {
            DoubleBuffered = true;
        }
    }
}