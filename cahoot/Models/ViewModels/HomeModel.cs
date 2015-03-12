using System.Collections.Generic;
using cahoot.Models;

namespace cahoot.Models.ViewModels
{
    public class HomeModel : ViewModelBase, IHomeModel
    {
        //public FlickrNet.PhotoCollection flickrPhotos { get; set; }
        public List<News> NewsEntries { get; set; }
        public List<GuestBook> GuestBookPosts { get { return GuestbookEntries; } }

     }
}