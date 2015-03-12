using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cahoot.Models.ViewModels;

namespace cahoot.Models.ViewModels
{
    public class MemberModel : ViewModelBase, IMemberModel
    {
        public List<Member> ListMembers { get; set; }
        public Member Member { get; set; }
        public IEnumerable<SelectListItem> Gender { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; } 
    }
}