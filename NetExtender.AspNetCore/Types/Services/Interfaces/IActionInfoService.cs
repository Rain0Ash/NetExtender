// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace NetExtender.AspNetCore.Types.Services.Interfaces
{
    internal interface IUnsafeActionInfoService : IActionInfoService
    {
        public ActionDescriptor? Descriptor { get; }
        
        public void Set(String? current, ActionDescriptor? descriptor);
    }
    
    public interface IActionInfoService
    {
        internal IUnsafeActionInfoService? Unsafe
        {
            get
            {
                return this as IUnsafeActionInfoService;
            }
        }
        
        public String? Id { get; }
        public String? Current { get; }
    }
}