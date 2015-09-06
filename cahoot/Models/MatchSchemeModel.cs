using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using cahoot.Models.ViewModels;

namespace cahoot.Models
{
    public class MatchSchemeModel : ViewModelBase
    {
        public string Url { get; set; }
        [Display(Name = "Lag")]
        public string Team { get; set; }
        [Display(Name = "Granska poster som läggs upp innan de skapas")]
        public bool IsDebug { get; set; }
        [Display(Name = "Radera framtida poster")]
        public bool ClearFutrurePosts { get; set; }

        public IEnumerable<SelectListItem> Teams { get; set; }
        public IEnumerable<Calendar> CalendarItems { get; set; } 
    }
}