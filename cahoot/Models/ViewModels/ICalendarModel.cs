using System.Collections.Generic;
using cahoot.Models;
using System.ServiceModel.Syndication;

namespace cahoot.Models.ViewModels
{
    interface ICalendarModel
    {

        List<Calendar> TeamCalendarEvents { get; set; }
        Calendar Calendar { get; set; }
       /* **********************************************
          *                General
          * ********************************************* */
        List<Calendar> CalendarEntries { get; }
        


    }
}
