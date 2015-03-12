using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlickrNet;
using System.Web.Caching;

namespace bkcahoot.Code
{
    public class FlickrUtil
    {
        public static FlickrNet.PhotoCollection BowlingPhotos(int count)
        {
            try
            {
                FlickrNet.PhotoCollection photos =
                    HttpContext.Current.Cache["flickrphotos"] as FlickrNet.PhotoCollection;
                if (photos == null)
                {
                    FlickrNet.Flickr flickrData = new FlickrNet.Flickr(Constants.FLICKR_API_KEY, Constants.FLICKR_SECRET,
                                                                       Constants.FLICKR_TOKEN);
                    FlickrNet.Flickr.CacheLocation = HttpContext.Current.Server.MapPath(Constants.FLICKR_CACHEPATH);
                    FlickrNet.PhotoSearchOptions options = new FlickrNet.PhotoSearchOptions();


                    //if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                    //{
                    //    options.UserId = Constants.FLICKR_USERID;
                    //    options.SortOrder = FlickrNet.PhotoSearchSortOrder.DateTakenDescending;
                    //}
                    //else if (DateTime.Now.DayOfWeek == DayOfWeek.Friday || DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                    //{
                    options.UserId = Constants.FLICKR_USERID;
                    options.SortOrder = FlickrNet.PhotoSearchSortOrder.DateTakenAscending;
                    //}
                    //else
                    //{
                    //    options.Tags = "bowling";
                    //    options.SortOrder = FlickrNet.PhotoSearchSortOrder.Relevance;
                    //}

                    options.PerPage = count;
                    photos = flickrData.PhotosSearch(options);
                    HttpContext.Current.Cache.Insert("flickrphotos", photos, null, DateTime.Now.AddDays(1),
                                                     TimeSpan.Zero, CacheItemPriority.Normal, null);
                }
                return photos;
            }
            catch{return new PhotoCollection();}
        }
        
 
    
    }
}