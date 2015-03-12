using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cahoot.Models;
using System.Web.Security;
using cahoot.Models.ViewModels;
using cahoot.Extensions;

namespace cahoot.Controllers
{
    public class MemberController : BaseController
    {
        readonly IMemberModel _model;

        public MemberController()
        {
            _model = new MemberModel();
        }

        //
        // GET: /Member/

        public ActionResult Index()
        {
            _model.ListMembers = Repository.GetMembers(null).OrderBy(m => m.Name).ToList();
            return View(_model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(_model);
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "UserName, PassWord", Exclude = "Name, Phone, Email")] Member memberToValidate)
        {

            ModelState.Remove("Name");
            ModelState.Remove("Email");
            if ((!string.IsNullOrEmpty(memberToValidate.UserName) && !string.IsNullOrEmpty(memberToValidate.PassWord)) || memberToValidate.PassWord == "new")
            {
                string userGroup = Repository.ValidatePassWord(memberToValidate.UserName, memberToValidate.PassWord.ToBase64String());
                if (userGroup == string.Empty)
                {
                    ModelState.AddModelError("_FORM", "Felaktigt användarnamn eller lösenord.");
                    return View(_model);
                }
                var authTicket = new FormsAuthenticationTicket(1, userGroup, DateTime.Now, DateTime.Now.AddMinutes(60), true, "");
                var cookieContents = FormsAuthentication.Encrypt(authTicket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieContents)
                {
                    Expires = authTicket.Expiration,
                    Path = FormsAuthentication.FormsCookiePath

                };
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                HttpRuntime.Cache.Remove("statitems");
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("_FORM", "Vänligen ange ett användarnamn eller lösenord.");

            return View(_model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Member/Create
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create(string register)
        {
            _model.Gender = GetGenders();
            _model.Status = GetStatus();
            return View(_model);
        }

        //
        // POST: /Member/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(MemberModel memberToCreate, FormCollection form)
        {
            if (!User.Identity.IsAuthenticated)
                ModelState.Remove("Member.PassWord");

            var writtenCaptcha = form["uniquekey"];
            var capthaToWrite = form["checksum"].FromBase64String();
            if (writtenCaptcha.ToLower() != capthaToWrite.ToLower())
                ModelState.AddModelError("_FORM", "Felaktigt kontrollord angivet.");

            if (ModelState.IsValid)
            {
                if (User.Identity.Name == "Admin")
                    memberToCreate.Member.PassWord = memberToCreate.Member.PassWord.ToBase64String();
 
                if (Repository.CreateMemberEntry(memberToCreate))
                {
                    if (User.Identity.IsAuthenticated)
                        return RedirectToAction("Index");
                    return View("Register", _model);
                }

                ModelState.AddModelError("_FORM", "Något gick fel när posten skulle skapas.");
            }
            _model.Gender = GetGenders();
            _model.Status = GetStatus();
            return View(_model);
        }

        //
        // GET: /Member/Edit/5
        [Authorize(Users = "Admin, User")]
        public ActionResult Edit(int id)
        {

            ViewBag.Status = new List<string> { "Admin", "User" };
            _model.Member = Repository.GetMembers(id).ElementAt(0);
            _model.Gender = GetGenders();
            _model.Status = GetStatus();

            if (_model.Member.PassWord != "new")
                _model.Member.PassWord = _model.Member.PassWord.FromBase64String();

            return View(_model);
        }

        //
        // POST: /Member/Edit/5

        [HttpPost]
        [Authorize(Users = "Admin, User")]
        public ActionResult Edit(MemberModel memberToUpdate)
        {
            //Users cannot change pword, usernam and status though users can change other users data
            if (User.Identity.Name == "User")
            {
                Member member = Repository.GetMembers(memberToUpdate.Member.MemberID).ElementAt(0);
                memberToUpdate.Member.PassWord = member.PassWord.FromBase64String();
                memberToUpdate.Member.UserName = member.UserName;
                memberToUpdate.Member.Status = member.Status;
                ModelState.Remove("Member.PassWord");
                ModelState.Remove("Member.UserName");
                ModelState.Remove("Member.Status");
            }

            if (ModelState.IsValid)
            {
                if (Repository.UpdateMemberEntry(memberToUpdate))
                    return RedirectToAction("Index");
                ModelState.AddModelError("_FORM", "Något gick fel när posten skulle updateras.");

            }
            _model.Gender = GetGenders();
            _model.Status = GetStatus();

            return View(_model);
        }

        [Authorize(Users = "Admin")]
        public ActionResult Delete(int id)
        {
            if (Repository.DeleteMemberEntry(id))
                return RedirectToAction("Index");

            return View("Error", _model);

        }

        private IEnumerable<SelectListItem> GetGenders()
        {
            var genders = new List<string> { "Man", "Kvinna" };
            return genders.Select(x => new SelectListItem
            {
                Value = x.Substring(0, 1),
                Text = x.ToString()
            });
        }
        private IEnumerable<SelectListItem> GetStatus()
        {
            var genders = new List<string> { "User", "Admin" };
            return genders.Select(x => new SelectListItem
            {
                Value = x.ToString(),
                Text = x.ToString()
            });
        }
    }
}
