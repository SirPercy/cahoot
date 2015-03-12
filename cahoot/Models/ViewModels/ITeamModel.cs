using System.Collections.Generic;
using cahoot.Models;
using PagedList;
using System.ServiceModel.Syndication;

namespace cahoot.Models.ViewModels
{
    interface ITeamModel
    {
        IPagedList<News> TeamInfoEntries { get; set; }


        /* **********************************************
           *                General
           * ********************************************* */
        //List<Calendar> CalendarEntries { get; set; }
        //List<TopFive> TopFive { get; set; }
        //List<LatestResult> LatestResult { get; set; }
        //List<Sponsor> Sponsors { get; set; }
        //List<SyndicationItem> SwebowlRss { get; set; }
    }
}
