using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NorthWeird.WebUI.Helpers.HtmlHelpers
{
    public static class NorthWeirdHtmlHelper
    {
        public static IHtmlContent NorthWeirdImageLink(this IHtmlHelper htmlHelper,
            int imageId, string linkText)
        {
            return htmlHelper.RouteLink(linkText, "Image", new {id = imageId});
        }
    }
}
