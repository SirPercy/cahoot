using System.Web;
using System.Web.Mvc;
using System.Text;
using cahoot.Extensions;

namespace cahoot.Helpers
{
    public static class PreviewTextHelper
    {
        
        public static MvcHtmlString GuestBookPreviewText(this HtmlHelper helper, string content, int length)
        {
            var encodedContent = HttpUtility.HtmlEncode(content);
            var b = new StringBuilder();
            if (content.Length > length)
            {
                b.Append(content.Substring(0, length-3) + "...");
                b.Append(string.Format("<span class=\"hidden\">{0}</span>", encodedContent));
                return MvcHtmlString.Create(b.ToString().ReplaceLineBreaks());
            }
            return MvcHtmlString.Create(encodedContent.ReplaceLineBreaks());
        }

        public static MvcHtmlString PreviewText(this HtmlHelper helper, string content, int length)
        {
            if (content == null)
                return MvcHtmlString.Create(string.Empty);

            var b = new StringBuilder();
            if (content.Length > length)
            {
                b.Append(content.Substring(0, length - 3) + "...");
                return MvcHtmlString.Create(b.ToString().ReplaceLineBreaks());
            }
            return MvcHtmlString.Create(content.ReplaceLineBreaks());
        }

   }
}