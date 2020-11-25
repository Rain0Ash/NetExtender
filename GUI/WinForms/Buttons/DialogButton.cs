// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using NetExtender.Utils.IO;
using NetExtender.Utils.Types;

namespace NetExtender.GUI.WinForms.Buttons
{
    public enum DialogType
    {
        Open,
        Save
    }
    
    public sealed class DialogButton : LocalizationButton
    {
        private readonly Control _linkedControl;

        public event StringHandler PathBeenSelected;
        public event EnumerableHandler PathsBeenSelected;

        public DialogType Dialog { get; }
        
        public Boolean AddSeparatorToPickedFolder { get; set; } = true;

        private CommonFileDialog _fileDialog;

        public CommonFileDialog FileDialog
        {
            get
            {
                return _fileDialog;
            }
            private set
            {
                if (value is null || _fileDialog == value)
                {
                    return;
                }

                _fileDialog = value;
            }
        }

        public CommonOpenFileDialog OpenFileDialog
        {
            get
            {
                return _fileDialog as CommonOpenFileDialog;
            }
        }

        public CommonSaveFileDialog SaveFileDialog
        {
            get
            {
                return _fileDialog as CommonSaveFileDialog;
            }
        }

        public DialogButton()
            : this(null)
        {
        }

        public DialogButton(Control control, DialogType dialog = DialogType.Open)
        {
            Dialog = dialog;

            if (Dialog == DialogType.Save)
            {
                FileDialog = new CommonSaveFileDialog();
            }
            else
            {
                FileDialog = new CommonOpenFileDialog
                {
                    Multiselect = false,
                    IsFolderPicker = true,
                };
            }

            _linkedControl = control;
            Text = @"...";
            TextAlign = ContentAlignment.MiddleCenter;
        }

        private String CheckLinkedControlText()
        {
            String fullPath = PathUtils.GetFullPath(_linkedControl?.Text) ?? Directory.GetCurrentDirectory();

            if (Directory.Exists(fullPath))
            {
                return fullPath;
            }

            fullPath = StringUtils.BeforeFormatVariables(fullPath);
            if (!String.IsNullOrEmpty(fullPath) && Directory.Exists(fullPath))
            {
                return fullPath;
            }

            return Directory.Exists(FileDialog.InitialDirectory) ? FileDialog.InitialDirectory : Directory.GetCurrentDirectory();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            FileDialog.InitialDirectory = CheckLinkedControlText();

            if (FileDialog.ShowDialog() != CommonFileDialogResult.Ok)
            {
                return;
            }
            
            switch (FileDialog)
            {
                case CommonOpenFileDialog open:
                {
                    Boolean separator = open.IsFolderPicker && AddSeparatorToPickedFolder;

                    if (open.Multiselect)
                    {
                        PathsBeenSelected?.Invoke(separator ? open.FileNames.Select(PathUtils.ConvertToFolder) : open.FileNames);
                    }
                    else
                    {
                        PathBeenSelected?.Invoke(separator ? PathUtils.ConvertToFolder(open.FileName) : open.FileName);
                    }
                    
                    break;
                }
                case CommonSaveFileDialog save:
                    PathBeenSelected?.Invoke(save.FileName);
                    break;
            }
        }

        protected override void Dispose(Boolean disposing)
        {
            PathBeenSelected = null;
            PathsBeenSelected = null;
            FileDialog.Dispose();
            base.Dispose(disposing);
        }
    }
}