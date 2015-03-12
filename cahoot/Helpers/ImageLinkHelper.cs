using System.Web.Mvc;
using System.Web.Routing;

namespace cahoot.Helpers
{

    public static class ImageLinkHelper
    {
        public static MvcHtmlString ImageLink(this HtmlHelper helper, string actionName, string imageUrl, string alternateText)
        {
            return ImageLink(helper, null, actionName, imageUrl, alternateText, null, null, null);
        }

        public static MvcHtmlString ImageLink(this HtmlHelper helper, string actionName, string imageUrl, string alternateText, object routeValues)
        {
            return ImageLink(helper, null, actionName, imageUrl, alternateText, routeValues, null, null);
        }

        public static MvcHtmlString ImageLink(this HtmlHelper helper, string controllerName, string actionName, string imageUrl, string alternateText, object imageHtmlAttributes)
        {
            return ImageLink(helper, controllerName, actionName, imageUrl, alternateText, null, null, imageHtmlAttributes);
        }

        public static MvcHtmlString ImageLink(this HtmlHelper helper, string controllerName, string actionName, string imageUrl, string alternateText, object routeValues, object linkHtmlAttributes, object imageHtmlAttributes)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(actionName, controllerName, routeValues);

            // Create link  
            var linkTagBuilder = new TagBuilder("a");
            linkTagBuilder.MergeAttribute("href", url);
            linkTagBuilder.MergeAttributes(new RouteValueDictionary(linkHtmlAttributes));

            // Create image  
            var imageTagBuilder = new TagBuilder("img");
            imageTagBuilder.MergeAttribute("src", urlHelper.Content(imageUrl));
            imageTagBuilder.MergeAttribute("alt", alternateText);
            imageTagBuilder.MergeAttributes(new RouteValueDictionary(imageHtmlAttributes));

            // Add image to link  
            linkTagBuilder.InnerHtml = imageTagBuilder.ToString(TagRenderMode.SelfClosing);

            return new MvcHtmlString(linkTagBuilder.ToString());
        }


    }

}

