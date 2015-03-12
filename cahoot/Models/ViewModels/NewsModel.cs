using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cahoot.Models.ViewModels;
using PagedList;

namespace cahoot.Models.ViewModels
{
    public class NewsModel : ViewModelBase, INewsModel
    {
        public IPagedList<News> NewsEntries { get; set; }
        public News News{get;set;}
    }
}
