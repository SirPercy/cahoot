using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace cahoot.Models
{
    public class MatchData
    {
        public int MatchId { get; set; }
        [Required(ErrorMessage = "Vänligen ange ett omgångsnummer")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage="Omgång måste vara ett heltal")]
        public int Round { get; set; }
        [Required(ErrorMessage = "Vänligen ange ett datum")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Vänligen ange en plats")]
        public string Location { get; set; }
        public string Team { get; set; }
        //duplicate
        public int TeamId { get; set; }
        [Required(ErrorMessage = "Vänligen ange en motståndare")]
        public string Opponent { get; set; }
        [Required(ErrorMessage = "Vänligen ange vår poäng")]
        [RegularExpression("([0-9][0-9]?)", ErrorMessage = "Vår matchpoäng kan max innehålla två siffor")]
        public int OurPoints { get; set; }
        [Required(ErrorMessage = "Vänligen ange motståndarens poäng")]
        [RegularExpression("([0-9][0-9]?)", ErrorMessage = "Deras matchpoäng kan max innehålla två siffor")]
         public int OpponentPoints { get; set; }
        [Required(ErrorMessage = "Vänligen ange vårt resultat")]
        [RegularExpression("([1-9][0-9]{3})", ErrorMessage = "Vårt resultat måste innehålla fyra siffor")]
         public int OurScore { get; set; }
        [Required(ErrorMessage = "Vänligen ange motståndarens resultat")]
        [RegularExpression("([1-9][0-9]{3})", ErrorMessage = "Deras resultat måste innehålla fyra siffor")]
        public int OpponentScore { get; set; }
        public List<ScoringReport> Results {get;set;}
    }
}