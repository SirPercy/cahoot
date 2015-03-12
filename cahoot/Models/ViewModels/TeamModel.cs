using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cahoot.Models.ViewModels;
using PagedList;

namespace cahoot.Models.ViewModels
{
    public class TeamModel : ViewModelBase, ITeamModel
    {

        public IPagedList<News> TeamInfoEntries { get; set; }
     }
}