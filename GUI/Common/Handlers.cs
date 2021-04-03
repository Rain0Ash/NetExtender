// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using JetBrains.Annotations;
using NetExtender.GUI.Common.EventArgs;

namespace NetExtender.GUI.Common
{
    public delegate void SizeChangeToggleHandler([CanBeNull] Object? sender, SizeChangeToggleEventArgs args);
}