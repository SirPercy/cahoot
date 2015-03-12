using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace cahoot.Models
{
    public class Calendar
    {
        public int? EventId { get; set; }
        [Required(ErrorMessage = "Vänligen ange en rubrik")]
        [StringLength(80, ErrorMessage = "Rubrik får max innehålla 80 tecken")]
        public string EventText { get; set; }
        [Required(ErrorMessage = "Vänligen ange en information om händelsen")]
        public string EventInfo { get; set; }
        [Required(ErrorMessage = "Vänligen ange ett datum")]
        public DateTime EventDate { get; set; }
    }
}