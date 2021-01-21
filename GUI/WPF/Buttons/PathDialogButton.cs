// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Linq;
using Microsoft.WindowsAPICodePack.Dialogs;
using NetExtender.Utils.IO;
using NetExtender.Utils.Types;

namespace NetExtender.GUI.WPF.Buttons
{
    public class PathDialogButton : LocalizableButton, IDisposable
    {
        public event StringHandler PathBeenSelected;
        public event EnumerableHandler PathsBeenSelected;
        
        public Boolean AddSeparatorToPickedFolder { get; set; } = true;

        public Func<String> GetCurrentPath { get; set; } = null;
        
        private CommonOpenFileDialog _openFileDialog;

        public CommonOpenFileDialog OpenFileDialog
        {
            get
            {
                return _openFileDialog;
            }
            private set
            {
                if (value is null || _openFileDialog == value)
                {
                    return;
                }

                _openFileDialog = value;
            }
        }

        public PathDialogButton()
        {
            OpenFileDialog = new CommonOpenFileDialog
            {
                Multiselect = false,
                IsFolderPicker = true,
            };
            
            Content = @"...";

            Click += OpenPathDialog;
        }
        
        private String CheckLinkedControlText()
        {
            String fullPath = PathUtils.GetFullPath(GetCurrentPath?.Invoke()) ?? Directory.GetCurrentDirectory();

            if (Directory.Exists(fullPath))
            {
                return fullPath;
            }

            fullPath = StringUtils.TrimAfterFormatVariables(fullPath);
            if (!String.IsNullOrEmpty(fullPath) && Directory.Exists(fullPath))
            {
                return fullPath;
            }

            return Directory.Exists(OpenFileDialog.InitialDirectory) ? OpenFileDialog.InitialDirectory : Directory.GetCurrentDirectory();
        }

        protected virtual void OpenPathDialog(Object sender, EventArgs e)
        {
            OpenFileDialog.InitialDirectory = CheckLinkedControlText();

            if (OpenFileDialog.ShowDialog() != CommonFileDialogResult.Ok)
            {
                return;
            }

            Boolean addSeparator = OpenFileDialog.IsFolderPicker && AddSeparatorToPickedFolder;

            if (OpenFileDialog.Multiselect)
            {
                PathsBeenSelected?.Invoke(addSeparator
                    ? OpenFileDialog.FileNames.Select(PathUtils.ConvertToFolder)
                    : OpenFileDialog.FileNames);
            }
            else
            {
                PathBeenSelected?.Invoke(addSeparator ? PathUtils.ConvertToFolder(OpenFileDialog.FileName) : OpenFileDialog.FileName);
            }
        }

        public void Dispose()
        {
            PathBeenSelected = null;
            PathsBeenSelected = null;
            OpenFileDialog.Dispose();
        }
    }
}