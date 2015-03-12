namespace cahoot.Extensions
{
    public static class StringExtensions
    {

        public static string ReplaceLineBreaks(this string s){

            if(!string.IsNullOrEmpty(s))
                return s.Replace("\r\n", "<br/>");

            return s;

        }
        public static string UppercaseFirst(this string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
        
        public static string ToBase64String(this string value)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(value);
            return System.Convert.ToBase64String(toEncodeAsBytes);
        }

        public static string FromBase64String(this string value)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(value);
            return System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
        }

    }
}