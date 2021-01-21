// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Globalization;
using NetExtender.Utils.Types;
using JetBrains.Annotations;
using NetExtender.Images.Flags;

namespace NetExtender.Cultures
{
    public class Culture : CultureInfo
    {
        private String _customName;
        public String CustomName
        {
            get
            {
                return _customName ??= this.GetNativeLanguageName();
            }
            init
            {
                _customName = value;
            }
        }

        public String Code
        {
            get
            {
                return TwoLetterISOLanguageName.ToLower();
            }
        }

        private Image _image;
        public Image Image
        {
            get
            {
                try
                {
                    return _image ??=
                        (Image) (FlagsImages.ResourceManager.GetObject(TwoLetterISOLanguageName) ??
                                 FlagsImages.ResourceManager.GetObject($"_{TwoLetterISOLanguageName}")) ?? Images.Images.Basic.Null;
                }
                catch (Exception)
                {
                    return _image ??= Images.Images.Basic.Null;
                }
            }
            init
            {
                _image = value;
            }
        }

        public UInt16 LCID16
        {
            get
            {
                return (UInt16) LCID;
            }
        }
        
        public Culture(Int32 lcid)
            : base(lcid)
        {
        }

        public Culture(Int32 lcid, Boolean useUserOverride)
            : base(lcid, useUserOverride)
        {
        }

        public Culture([NotNull] String name)
            : base(name)
        {
        }

        public Culture([NotNull] String name, Boolean useUserOverride)
            : base(name, useUserOverride)
        {
        }
    }
}