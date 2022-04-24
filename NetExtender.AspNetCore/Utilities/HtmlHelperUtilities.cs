// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class HtmlHelperUtilities
    {
        public static HtmlString GetTitle(this IHtmlHelper helper)
        {
            if (helper is null)
            {
                throw new ArgumentNullException(nameof(helper));
            }

            String title = helper.ViewBag.Title;
            return !String.IsNullOrEmpty(title) ? new HtmlString(title) : HtmlString.Empty;
        }
        
        public static void SetTitle(this IHtmlHelper helper, HtmlString? value)
        {
            if (helper is null)
            {
                throw new ArgumentNullException(nameof(helper));
            }

            helper.ViewBag.Title = value ?? HtmlString.Empty;
        }

        public static void SetTitle(this IHtmlHelper helper, String? value)
        {
            if (helper is null)
            {
                throw new ArgumentNullException(nameof(helper));
            }

            helper.ViewBag.Title = !String.IsNullOrEmpty(value) ? new HtmlString(value) : HtmlString.Empty;
        }
    }
}