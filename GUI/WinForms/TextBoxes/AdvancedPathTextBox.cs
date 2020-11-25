// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using NetExtender.GUI.WinForms.Buttons;
using NetExtender.Utils.IO;

namespace NetExtender.GUI.WinForms.TextBoxes
{
    public class AdvancedPathTextBox : AdvancedTextBox<FormatPathTextBox>
    {
        private event BooleanHandler PathDialogButtonChanged;
        public readonly DialogButton PathDialogButton;

        public CommonFileDialog PathDialog
        {
            get
            {
                return PathDialogButton.FileDialog;
            }
        }
        
        public CommonOpenFileDialog OpenPathDialog
        {
            get
            {
                return PathDialogButton.OpenFileDialog;
            }
        }
        
        public CommonSaveFileDialog SavePathDialog
        {
            get
            {
                return PathDialogButton.SaveFileDialog;
            }
        }

        public Boolean PathDialogButtonEnabled
        {
            get
            {
                return PathDialogButton.Enabled;
            }
            set
            {
                if (PathDialogButton.Enabled == value)
                {
                    return;
                }

                PathDialogButton.Enabled = value;
                PathDialogButtonChanged?.Invoke(value);
            }
        }

        private event BooleanHandler PathFormatHelpButtonChanged;
        private readonly Button _pathFormatHelpButton;

        public Boolean PathFormatHelpButtonEnabled
        {
            get
            {
                return _pathFormatHelpButton.Enabled;
            }
            set
            {
                if (_pathFormatHelpButton.Enabled == value)
                {
                    return;
                }

                _pathFormatHelpButton.Enabled = value;
                PathFormatHelpButtonChanged?.Invoke(value);
            }
        }

        private readonly Button _pathTypeChangeButton;

        private event BooleanHandler PathTypeChangeButtonChanged;

        public Boolean PathTypeChangeButtonEnabled
        {
            get
            {
                return _pathTypeChangeButton.Enabled;
            }
            set
            {
                if (_pathTypeChangeButton.Enabled == value)
                {
                    return;
                }

                _pathTypeChangeButton.Enabled = value;
                PathTypeChangeButtonChanged?.Invoke(value);
            }
        }

        public new event EventHandler TextChanged;

        public override String Text
        {
            get
            {
                return TextBox.Text;
            }
            set
            {
                if (TextBox.Text == value)
                {
                    return;
                }

                TextBox.Text = value;
                TextChanged?.Invoke(TextBox, EventArgs.Empty);
            }
        }

        public HorizontalAlignment TextAlign
        {
            get
            {
                return TextBox.TextAlign;
            }
            set
            {
                TextBox.TextAlign = value;
            }
        }

        public PathType PathType
        {
            get
            {
                return TextBox.PathType;
            }
            set
            {
                TextBox.PathType = value;
            }
        }
        
        public PathStatus PathStatus
        {
            get
            {
                return TextBox.PathStatus;
            }
            set
            {
                TextBox.PathStatus = value;
            }
        }

        public Boolean LongPath
        {
            get
            {
                return TextBox.MaxLength > 255;
            }
            set
            {
                TextBox.MaxLength = value ? 65535 : 255;
            }
        }

        public event StringHandler PathBeenSelected;
        public event EnumerableHandler PathsBeenSelected;

        public event EventHandler FormatHelpButtonClicked;


        public String PathTypeChangeToolTip
        {
            get
            {
                return HelpToolTip.GetToolTip(_pathTypeChangeButton);
            }
        }

        private event EmptyHandler PathTypeChangeToolTipChanged;

        private String _pathTypeChangeToRelativeToolTip;

        public String PathTypeChangeToRelativeToolTip
        {
            get
            {
                return _pathTypeChangeToRelativeToolTip;
            }
            set
            {
                if (_pathTypeChangeToRelativeToolTip == value)
                {
                    return;
                }

                _pathTypeChangeToRelativeToolTip = value;
                PathTypeChangeToolTipChanged?.Invoke();
            }
        }

        private String _pathTypeChangeToAbsoluteToolTip;

        public String PathTypeChangeToAbsoluteToolTip
        {
            get
            {
                return _pathTypeChangeToAbsoluteToolTip;
            }
            set
            {
                if (_pathTypeChangeToAbsoluteToolTip == value)
                {
                    return;
                }

                _pathTypeChangeToAbsoluteToolTip = value;
                PathTypeChangeToolTipChanged?.Invoke();
            }
        }

        public String PathFormatHelpToolTip
        {
            get
            {
                return HelpToolTip.GetToolTip(_pathFormatHelpButton);
            }
            set
            {
                HelpToolTip.SetToolTip(_pathFormatHelpButton, value);
            }
        }

        public String PathDialogToolTip
        {
            get
            {
                return HelpToolTip.GetToolTip(PathDialogButton);
            }
            set
            {
                HelpToolTip.SetToolTip(PathDialogButton, value);
            }
        }


        public AdvancedPathTextBox(DialogType dialog = DialogType.Open)
            : base(new FormatPathTextBox
            {
                Multiline = false,
                AutoSize = false,
                TextAlign = HorizontalAlignment.Left,
                MaxLength = 255
            })
        {
            _pathTypeChangeButton = new Button
            {
                Text = @"R"
            };
            _pathFormatHelpButton = new Button();
            PathDialogButton = new DialogButton(TextBox, dialog);

            TextBox.TextChanged += (sender, args) =>
            {
                TextChanged?.Invoke(sender, args);
                OnPathTextBox_TextChanged();
            };
            TextBox.AvailableFormatingPartsChanged += OnAvailableFormatingParts_Changed;

            _pathFormatHelpButton.Click += (sender, args) => FormatHelpButtonClicked?.Invoke(sender, args);

            _pathTypeChangeButton.Click += (sender, args) => OnRelativeButton_Click();

            PathDialogButton.PathBeenSelected += str => PathBeenSelected?.Invoke(str);
            PathDialogButton.PathsBeenSelected += list => PathsBeenSelected?.Invoke(list);

            PathTypeChangeButtonChanged += boolean => OnSizeChanged(EventArgs.Empty);
            PathDialogButtonChanged += boolean => OnSizeChanged(EventArgs.Empty);
            PathFormatHelpButtonChanged += boolean => OnSizeChanged(EventArgs.Empty);
            LocationChanged += (sender, args) => OnSizeChanged(args);

            FormatHelpButtonClicked += OnFormatHelpButton_Click;

            PathTypeChangeToolTipChanged += OnPathTextBox_TextChanged;

            OnPathTextBox_TextChanged();

            Controls.Add(TextBox);
            Controls.Add(_pathTypeChangeButton);
            Controls.Add(_pathFormatHelpButton);
            Controls.Add(PathDialogButton);
        }

        public void UpdateAvailableFormatingParts(IReflect type)
        {
            TextBox.UpdateAvailableFormatingParts(type);
        }

        public Boolean IsValid()
        {
            return TextBox.IsValid;
        }

        private void OnAvailableFormatingParts_Changed()
        {
            PathFormatHelpButtonEnabled = PathFormatHelpButtonEnabled &&
                                          GetType() != typeof(AdvancedPathTextBox) &&
                                          TextBox.AvailableFormatingParts is not null && TextBox.AvailableFormatingParts.Any();
        }

        protected virtual void OnFormatHelpButton_Click(Object sender, EventArgs e)
        {
            //override
        }

        private void OnPathTextBox_TextChanged()
        {
            if (PathUtils.IsAbsolutePath(TextBox.Text))
            {
                _pathTypeChangeButton.Text = @"R";
                HelpToolTip.SetToolTip(_pathTypeChangeButton, PathTypeChangeToRelativeToolTip);
                return;
            }

            _pathTypeChangeButton.Text = @"A";
            HelpToolTip.SetToolTip(_pathTypeChangeButton, PathTypeChangeToAbsoluteToolTip);
        }

        private void OnRelativeButton_Click()
        {
            String text = TextBox.Text;
            if (PathUtils.IsAbsolutePath(text))
            {
                TextBox.Text = PathUtils.GetRelativePath(text) ?? text;
                return;
            }

            TextBox.Text = PathUtils.GetFullPath(text) ?? text;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateLocation();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            _pathFormatHelpButton.Enabled = true;
        }

        private void UpdateLocation()
        {
            Int32 buttonHeightSize = Size.Height + 2;
            _pathTypeChangeButton.Size = new Size(PathTypeChangeButtonEnabled ? buttonHeightSize : 0, buttonHeightSize);
            PathDialogButton.Size = new Size(PathDialogButtonEnabled ? buttonHeightSize : 0, buttonHeightSize);
            _pathFormatHelpButton.Size = new Size(PathFormatHelpButtonEnabled ? buttonHeightSize : 0, buttonHeightSize);

            Int32 imageHeightSize = Math.Max(buttonHeightSize / 2, 1);
            _pathFormatHelpButton.Image = new Bitmap(Images.Images.Basic.Question, new Size(imageHeightSize, imageHeightSize));
            TextBox.Size = new Size(
                Size.Width - _pathTypeChangeButton.Size.Width - _pathFormatHelpButton.Size.Width - PathDialogButton.Size.Width - 1,
                Size.Height);

            TextBox.Location = new Point(_pathTypeChangeButton.Size.Width, 0);
            Int32 buttonHeightLocation = (Size.Height - buttonHeightSize) / 2;
            _pathTypeChangeButton.Location = new Point(0, buttonHeightLocation);
            _pathFormatHelpButton.Location = new Point(_pathTypeChangeButton.Size.Width + TextBox.Size.Width + 1, buttonHeightLocation);
            PathDialogButton.Location = new Point(_pathTypeChangeButton.Size.Width + TextBox.Size.Width + _pathFormatHelpButton.Size.Width,
                buttonHeightLocation);
        }
    }
}