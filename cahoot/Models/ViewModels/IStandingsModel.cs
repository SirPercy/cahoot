using System.Collections.Generic;
using System.ServiceModel.Syndication;
using cahoot.Models;

namespace cahoot.Models.ViewModels
{
    interface IStandingsModel
    {
        List<MenuItem> StandingLinks { get; set; }
        List<MenuItem> StatisticsLinks { get; set; }
        MenuItem StandingItem { get; set; }
       

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