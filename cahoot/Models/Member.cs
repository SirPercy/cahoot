using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace cahoot.Models
{
    public class Member
    {
        public int MemberID { get; set; }        
        [Display(Name = "Namn")]
        [Required(ErrorMessage="Vänligen ange ett namn")]
        public string Name { get; set; }
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
        [Required(ErrorMessage="Vänligen ange en e-postadress")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$", ErrorMessage = "Du måste ange en giltig e-postadress")]
        [Display(Name = "E-postadress")]
        public string Email { get; set; }
        [Display(Name = "Kön")]
        public string Gender { get; set; }
        [Display(Name = "Roll")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Vänligen ange ett användarnamn")]
        [RegularExpression("[\\S]{6,}", ErrorMessage = "Användarnamn måste vara minst 6 tecken.")]
        [Display(Name = "Användarnamn")]
        public string UserName { get; set; }
        [Display(Name = "Lösenord")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Vänligen ange ett lösenord")]
        [RegularExpression("[\\S]{6,}", ErrorMessage = "Lösenord måste vara minst 6 tecken.")]
        public string PassWord { get; set; }
    }
}