using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cahoot.Models.ViewModels;

namespace cahoot.Models.ViewModels
{
    public class StandingsModel : ViewModelBase, IStandingsModel
    {

        public List<MenuItem> StandingLinks { get; set; }        
        public List<MenuItem> StatisticsLinks { get; set; }
        public MenuItem StandingItem { get; set; }
     }
}