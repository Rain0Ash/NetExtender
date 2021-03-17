// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.IO;
using NetExtender.Utils.IO;
using NetExtender.Utils.Types;

namespace NetExtender.GUI.WinForms.TextBoxes
{
    public class PathTextBox : HidenTextBox
    {
        public event EmptyHandler WellFormedCheckChanged;

        private Boolean _wellFormedCheck = true;

        public virtual Boolean WellFormedCheck
        {
            get
            {
                return _wellFormedCheck;
            }
            set
            {
                if (_wellFormedCheck == value)
                {
                    return;
                }

                _wellFormedCheck = value;
                WellFormedCheckChanged?.Invoke();
            }
        }
        
        public event EmptyHandler PathTypeChanged;

        private PathType _pathType = PathType.All;

        public PathType PathType
        {
            get
            {
                return _pathType;
            }
            set
            {
                if (_pathType == value)
                {
                    return;
                }

                _pathType = value;
                PathTypeChanged?.Invoke();
            }
        }
        
        public event EmptyHandler PathStatusChanged;

        private PathStatus _pathStatus = PathStatus.All;

        public PathStatus PathStatus
        {
            get
            {
                return _pathStatus;
            }
            set
            {
                if (_pathStatus == value)
                {
                    return;
                }

                _pathStatus = value;
                PathStatusChanged?.Invoke();
            }
        }

        public Boolean IsWellFormed
        {
            get
            {
                return !WellFormedCheck || WellFormedValidate();
            }
        }

        public PathTextBox()
        {
            Validate = text => IsValidPath() && IsWellFormed;
            HandleCreated += OnCreate;
            WellFormedCheckChanged += ItemValidateColor;
            PathTypeChanged += ItemValidateColor;
            PathStatusChanged += ItemValidateColor;
        }

        private void OnCreate(Object? sender, EventArgs e)
        {
            PasswordChar = ResetPasswordChar;
        }

        protected override void ItemValidateColor()
        {
            if (IsValid)
            {
                BackColor = Color.White;
            }
            else if (!IsWellFormed)
            {
                BackColor = Color.PaleVioletRed;
            }
            else
            {
                BackColor = Color.Coral;
            }
        }

        public virtual Boolean WellFormedValidate()
        {
            return StringUtils.IsBracketsWellFormed(Text);
        }

        public Boolean IsValidPath()
        {
            return IsValidPath(PathType);
        }

        public Boolean IsValidPath(PathType type)
        {
            return PathUtils.IsValidPath(Text, type, PathStatus);
        }
        
        public Boolean IsValidPath(PathStatus status)
        {
            return PathUtils.IsValidPath(Text, PathType, status);
        }
        
        public Boolean IsValidPath(PathType type, PathStatus status)
        {
            return PathUtils.IsValidPath(Text, type, status);
        }

        public String GetAbsolutePath()
        {
            return PathUtils.GetFullPath(Text);
        }

        public String GetRelativePath()
        {
            String absolutePath = PathUtils.GetFullPath(Text);
            return absolutePath.IsNullOrEmpty() ? null : PathUtils.GetRelativePath(absolutePath, Directory.GetCurrentDirectory());
        }

        public String GetRelativePath(String relativePath)
        {
            String absolutePath = PathUtils.GetFullPath(relativePath);
            return absolutePath.IsNullOrEmpty() ? GetRelativePath() : PathUtils.GetRelativePath(PathUtils.GetFullPath(Text), absolutePath);
        }
    }
}