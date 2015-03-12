using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Web.Caching;

namespace cahoot.Code
{
    public static class RssUtil
    {

        public static List<SyndicationItem> GetSweBowlRss(){

            List<SyndicationItem> rssItems = HttpContext.Current.Cache[Constants.SWEBOWL_RSSCACHEKEY] as List<SyndicationItem>;
            if (rssItems == null)
            {
                try
                {
                    SyndicationFeed feed = SyndicationFeed.Load(XmlReader.Create(Constants.SWEBOWL_RSSPATH));
                    if (feed != null)
                    {
                        rssItems = feed.Items.Take(4).ToList();
                        HttpContext.Current.Cache.Insert(Constants.SWEBOWL_RSSCACHEKEY, rssItems, null, DateTime.Now.AddHours(2), TimeSpan.Zero, CacheItemPriority.Normal, null);
                    }
                    else
                        return new List<SyndicationItem>();
                }
                catch
                {
                    return new List<SyndicationItem>();
                }
            }
            return rssItems;


        }
    }
}