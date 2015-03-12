using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cahoot.Models.ViewModels;

namespace cahoot.Models.ViewModels
{
    public class ResultModel : ViewModelBase, IResultModel
    {
        public MatchData Match { get; set; }
        public List<MatchData> MatchData { get; set; }
        public List<Result> HigestResultMen { get; set; }        
        public List<Result> HigestResultWomen { get; set; }
        public List<Result> HigestResultClub { get; set; }
        public List<Result> HigestResult { get; set; }
    }
}