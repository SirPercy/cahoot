using System.Collections.Generic;
using cahoot.Models;
using System.ServiceModel.Syndication;

namespace cahoot.Models.ViewModels
{
    interface IResultModel
    {
        MatchData Match { get; set; }
        List<MatchData> MatchData { get; set; }
        List<Result> HigestResultMen { get; set; }
        List<Result> HigestResultWomen { get; set; }
        List<Result> HigestResult { get; set; }
        List<Result> HigestResultClub { get; set; }
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
