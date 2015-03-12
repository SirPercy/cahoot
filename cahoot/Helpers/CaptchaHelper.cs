using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.IO;
using cahoot.Extensions;

namespace cahoot.Helpers
{
    public static class CaptchaHelper
    {
        public static IHtmlString Captcha(this System.Web.Mvc.HtmlHelper helper, string labelText, int characters = 4)
        {
            var captha = GetCaptcha(characters);
            var writer = new HtmlTextWriter(new StringWriter());
            //Open div
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            //Open label
            writer.AddAttribute(HtmlTextWriterAttribute.For, "uniquekey");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "uniquekey");
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write(labelText);
            //End label
            writer.RenderEndTag();

            //Captcha input
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "uniquekey");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, "uniquekey");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            //Controlword text
            writer.RenderBeginTag(HtmlTextWriterTag.Strong);
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.RenderEndTag();
            writer.Write(captha);
            writer.RenderEndTag();

            //Hidden input
            writer.AddAttribute(HtmlTextWriterAttribute.Name, "checksum");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, captha.ToBase64String());
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            //close div
            writer.RenderEndTag();
            
            //Return the HTML string
            return new HtmlString(writer.InnerWriter.ToString());


            
        }

        public static string GetCaptcha(int characters)
        {
            var chars = "ABCDEFGHJKMNPQRSTUVWXYZ123456789".ToCharArray();
            var data = new byte[characters];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);

            var result = new StringBuilder(characters);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }

            return result.ToString();

        }
    }
}