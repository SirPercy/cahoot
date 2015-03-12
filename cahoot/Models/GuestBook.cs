using System;
using System.ComponentModel.DataAnnotations;

namespace cahoot.Models
{
    public class GuestBook
    {
        public int EntryId { get; set; }
        [Required(ErrorMessage="Vänligen ange ett namn")]
        [StringLength(30, ErrorMessage = "Namn får max innehålla 30 tecken")]
        public string Name { get; set; }
        [Required(ErrorMessage="Vänligen ange ett inlägg")]
        public string Text { get; set; }
        public DateTime Date { get; set; }


    }
}