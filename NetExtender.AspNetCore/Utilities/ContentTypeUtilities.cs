using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.StaticFiles;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class ContentTypeUtilities
    {
        public static String GetFileExtension(this ContentType type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return GetFileExtension(type, new FileExtensionContentTypeProvider());
        }
        
        public static String GetFileExtension(this ContentType type, FileExtensionContentTypeProvider provider)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            Boolean Predicate(KeyValuePair<String, String> pair)
            {
                return String.Equals(pair.Value, type.MediaType, StringComparison.Ordinal);
            }
            
            if (provider.Mappings.Where(Predicate).Keys().FirstOrDefault() is not { } mapping)
            {
                throw new InvalidOperationException($"Unable to map file extension for Media Type: {type.MediaType}");
            }
            
            return mapping;
        }
    }
}