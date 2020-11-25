// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Text;

namespace NetExtender.Types.Strings.Interfaces
{
    public interface IString
    {
        public Int32 Length
        {
            get
            {
                return ToString()?.Length ?? 0;
            }
        }

        public String ToUpper()
        {
            return ToString()?.ToUpper();
        }
        
        public String ToUpper(CultureInfo info)
        {
            return ToString()?.ToUpper(info);
        }

        public String ToUpperInvariant()
        {
            return ToString()?.ToUpperInvariant();
        }
        
        public String ToLower()
        {
            return ToString()?.ToLower();
        }
        
        public String ToLower(CultureInfo info)
        {
            return ToString()?.ToLower(info);
        }

        public String ToLowerInvariant()
        {
            return ToString()?.ToLowerInvariant();
        }

        public String Normalize()
        {
            return ToString()?.Normalize();
        }

        public String Normalize(NormalizationForm normalization)
        {
            return ToString()?.Normalize(normalization);
        }
    }
}