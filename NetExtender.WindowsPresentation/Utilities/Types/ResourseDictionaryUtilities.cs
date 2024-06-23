using System;
using System.Windows;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    public static class ResourseDictionaryUtilities
    {
        public static Boolean AddStyle(this ResourceDictionary dictionary, Style? style)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }
            
            if (style?.TargetType is not { } type || dictionary.Contains(type))
            {
                return false;
            }
            
            try
            {
                dictionary.Add(type, style);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean RemoveStyle(this ResourceDictionary dictionary, Style? style)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }
            
            if (style?.TargetType is not { } type || !dictionary.Contains(type))
            {
                return false;
            }
            
            try
            {
                dictionary.Remove(type);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}