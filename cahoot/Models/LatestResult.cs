using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cahoot.Models
{
    public class LatestResult
    {
        public string Team { get; set; }
        public string Location { get; set; }
        public byte PointsHomeTeam { get; set; }
        public byte PointsAwayTeam { get; set; }
        public string Opponent { get; set; }

    }
}