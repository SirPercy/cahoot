using System.Collections.Generic;
using cahoot.Models;
using PagedList;
using System.ServiceModel.Syndication;

namespace cahoot.Models.ViewModels
{
    interface IGbookModel
    {
        GuestBook Gbook { get; set; }
       /* **********************************************
          *                General
          * ********************************************* */
        List<GuestBook> GuestBookPosts { get; }
        IPagedList<GuestBook> PagedGuestBookEntries { get; set; }
   
    }
}
