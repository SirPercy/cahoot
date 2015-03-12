using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Text;

namespace cahoot.Helpers
{
    public static class MenuItemHelper
    {
        public static MvcHtmlString MenuItem(this HtmlHelper helper, string linkText, string actionName, string controllerName)
        {
            return MenuItem(helper, linkText, actionName, controllerName, null, null);
        }
        public static MvcHtmlString MenuItem(this HtmlHelper helper, string linkText, string actionName, string controllerName, Dictionary<string, string> subItems) {
            return MenuItem(helper, linkText, actionName, controllerName, null, subItems);
        }
        public static MvcHtmlString MenuItem(this HtmlHelper helper, string linkText, string actionName, string controllerName, string className, Dictionary<string, string> subItems)
        {
            string currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
            string currentActionName = (string)helper.ViewContext.RouteData.Values["action"];
            var sb = new StringBuilder();
            
            if (currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase) && currentActionName.Equals(actionName, StringComparison.CurrentCultureIgnoreCase))
            {
            //if (currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase)) {

                sb.Append("<li id=\"active" + (subItems != null && subItems.Count > 0 ? " dropdown" : "") + "\">");
            }
            else { sb.Append("<li" + (subItems != null && subItems.Count > 0 ? " class=\"dropdown\"" : "") + ">"); }


            if (subItems != null && subItems.Count > 0)
            {
                sb.Append("<a href=\"#\" class=\"dropdown-toggle" + (!string.IsNullOrEmpty(className) ? " " + className : "") +"\" data-toggle=\"dropdown\" role=\"button\">" + linkText +
                          "<span class=\"caret\"></span></a>");
                sb.Append("<ul class=\"dropdown-menu\" role=\"menu\">");
                foreach (var subItem in subItems) {
                    sb.Append("<li>");
                    sb.Append(helper.ActionLink(subItem.Key, "Index", subItem.Value));
                    sb.Append("</li>");
                }
                sb.Append("</ul>");
            }
            else if (!string.IsNullOrEmpty(className))
                sb.Append(helper.ActionLink(linkText, actionName, controllerName, null, new {@class=className }));
            else
            {
                sb.Append(helper.ActionLink(linkText, actionName, controllerName));
            }



            sb.Append("</li>");

            return MvcHtmlString.Create(sb.ToString());

        }
    }
}