using System;
using System.ComponentModel.DataAnnotations;

namespace cahoot.Models
{
    public class News
    {
        public int? Id { get; set; }
        [DataType(DataType.Date)]
        [UIHint("DateTime")]
        [Required(ErrorMessage="Vänligen ange ett datum")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Vänligen ange en rubrik")]
        [StringLength(45, ErrorMessage="Rubrik får max innehålla 45 tecken")]
        [UIHint("Heading")]
        public string Heading { get; set; }
        public string Text { get; set; }
        [Required(ErrorMessage= "Vänligen ange en skribent")]
        [StringLength(20, ErrorMessage = "Skribent får max innehålla 20 tecken")]
        public string Writer { get; set; }
        public bool IsInternal { get; set; }
    }
}