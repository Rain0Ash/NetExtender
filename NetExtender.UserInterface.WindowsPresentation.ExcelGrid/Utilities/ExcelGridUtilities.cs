using System;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid.Utilities
{
    public static class ExcelGridUtilities
    {
        public static bool Is(this Type firstType, Type secondType)
        {
            if (firstType.IsGenericType && secondType == firstType.GetGenericTypeDefinition())
            {
                return true;
            }
            
            // checking generic interfaces
            foreach (var @interface in firstType.GetInterfaces())
            {
                if (@interface.IsGenericType)
                {
                    if (secondType == @interface.GetGenericTypeDefinition())
                    {
                        return true;
                    }
                }
                
                if (secondType == @interface)
                {
                    return true;
                }
            }
            
            var ult = Nullable.GetUnderlyingType(firstType);
            if (ult != null)
            {
                if (secondType.IsAssignableFrom(ult))
                {
                    return true;
                }
            }
            
            if (secondType.IsAssignableFrom(firstType))
            {
                return true;
            }
            
            return false;
        }
    }
}