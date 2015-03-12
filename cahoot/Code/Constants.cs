using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cahoot.Code
{
    public static class Constants
    {
        public enum XmlDataType
        {
            Standings,
            Statistics
        }
        public const string TeamName = "Bk Cahoot";
        public const string TeamCity = "kalmar";
        public const string FLICKR_API_KEY = "a08688dd18703348a0c5a2e6e0463449";
        public const string FLICKR_USERID = "61124764@N02";
        public const string FLICKR_SECRET = "df351045f01a4811";
        public const string FLICKR_TOKEN = "72157627700202504-ab3beede04a381d0";

        public const string FLICKR_CACHEPATH = "~/Files/Flickr/";
        public const string SWEBOWL_RSSPATH = "http://iof1.idrottonline.se/SvenskaBowlingforbundet/Lastasidor/IONF/";
        public const string SWEBOWL_RSSCACHEKEY = "swebowlrss";
    }
}