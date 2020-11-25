// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Utils.Numerics;

namespace NetExtender.Web
{
    public readonly struct WebDownloadProgress
    {
        public String Address { get; }
        public Int64 Current { get; }
        public Int64 Bytes { get; }

        public Double Percent
        {
            get
            {
                return (Double) Current / Bytes * 100;
            }
        }

        public WebDownloadProgress(String address, Int64 current, Int64 bytes)
        {
            Address = address;
            Current = current;
            Bytes = bytes.ToRange(1);
        }
    }
}