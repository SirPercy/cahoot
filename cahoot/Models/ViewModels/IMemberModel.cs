using System.Collections.Generic;
using System.Web.Mvc;
using System.ServiceModel.Syndication;
using cahoot.Models;

namespace cahoot.Models.ViewModels
{
    interface IMemberModel
    {
        List<Member> ListMembers { get; set; }
        Member Member { get; set; }
        IEnumerable<SelectListItem> Gender { get; set; }
        IEnumerable<SelectListItem> Status { get; set; } 
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