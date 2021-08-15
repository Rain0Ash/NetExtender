// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Utilities.Types
{
    public static class DrawingUtilities
    {
        public static Double GetAspectRatio(Int32 width, Int32 height)
        {
            return GetAspectRatio((Double) width, height);
        }
        
        public static Double GetAspectRatio(Double width, Double height)
        {
            return width / height;
        }
    }
}