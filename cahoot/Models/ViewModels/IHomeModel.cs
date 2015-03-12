using System.Collections.Generic;
using System.ServiceModel.Syndication;
using cahoot.Models;

namespace cahoot.Models.ViewModels
{
    interface IHomeModel
    {
         //FlickrNet.PhotoCollection flickrPhotos {get;set;}
         List<News> NewsEntries { get; set; }
         List<GuestBook> GuestBookPosts { get; }

         /* **********************************************
       *                General
       * ********************************************* */

       



 

   }
}