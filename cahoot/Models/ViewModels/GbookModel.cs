using System.Collections.Generic;
using PagedList;

namespace cahoot.Models.ViewModels
{
    public class GbookModel : ViewModelBase, IGbookModel
    {
        public List<GuestBook> GuestBookPosts { get { return GuestbookEntries; } }
        public GuestBook Gbook { get; set; }
        public IPagedList<GuestBook> PagedGuestBookEntries { get; set; }
 
    }
}