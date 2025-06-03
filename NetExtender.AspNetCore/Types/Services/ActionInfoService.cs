// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Mvc.Abstractions;
using NetExtender.AspNetCore.Types.Services.Interfaces;

namespace NetExtender.AspNetCore.Types.Services
{
    public class ActionInfoService : IUnsafeActionInfoService
    {
        String? IActionInfoService.Id
        {
            get
            {
                return Descriptor?.Id;
            }
        }

        public String? Current { get; protected set; }
        public ActionDescriptor? Descriptor { get; protected set; }

        void IUnsafeActionInfoService.Set(String? current, ActionDescriptor? descriptor)
        {
            Current = current;
            Descriptor = descriptor;
        }
    }
}