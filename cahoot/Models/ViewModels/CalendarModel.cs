using System;
using System.Collections.Generic;
using cahoot.Models;
using cahoot.Models.ViewModels;

namespace cahoot.Models.ViewModels
{
    public class CalendarModel : ViewModelBase, ICalendarModel
    {
        public List<Calendar> TeamCalendarEvents { get; set; }
        public Calendar Calendar { get; set; }

        public List<Calendar> CalendarEntries
        {
            get { return CalendarEvents; }
        }
    }
}