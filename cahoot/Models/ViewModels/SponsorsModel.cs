using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cahoot.Models.ViewModels;

namespace cahoot.Models.ViewModels
{
    public class SponsorsModel : ViewModelBase, ISponsorsModel
    {
        public Sponsor SponsorItem { get; set; }
     }
}