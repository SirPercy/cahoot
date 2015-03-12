using System;
using System.Linq;
using System.Web.Mvc;
using cahoot.Models.ViewModels;
using cahoot.Extensions;
using PagedList;

namespace cahoot.Controllers
{
    public class GbookController : BaseController
    {
        readonly IGbookModel _model;
        public GbookController()
        {
            _model = new GbookModel();
        }

        public ActionResult Index(int? id, int? page)
        {
            var gbookEntries = _model.GuestBookPosts;
            if (id != null)
            {
                var gbookId = id.Value;
                _model.PagedGuestBookEntries = gbookEntries.Where(entry => entry.EntryId == gbookId).ToList().ToPagedList(1, 1);
            }
            else
            {

                var pageIndex = page ?? 1;
                _model.PagedGuestBookEntries = gbookEntries.ToPagedList(pageIndex, 5);
            }
            
            return View(_model);
        }

        //
        // GET: /Gbook/Create

        public ActionResult Create()
        {
            return View(_model);
        } 

        //
        // POST: /Gbook/Create

        [HttpPost]
        public ActionResult Create(GbookModel guestBookEntryToCreate, FormCollection form)
        {
            var writtenCaptcha = form["uniquekey"];
            var capthaToWrite = form["checksum"].FromBase64String();
            if(writtenCaptcha.ToLower() != capthaToWrite.ToLower())
                ModelState.AddModelError("_FORM", "Felaktigt kontrollord angivet."); 
            
            if (ModelState.IsValid)
            {
                guestBookEntryToCreate.Gbook.Date = DateTime.Now;
                if (base.Repository.CreateGuestBookEntry(guestBookEntryToCreate))
                    return RedirectToAction("Index");

                ModelState.AddModelError("_FORM", "Något gick fel när posten skulle skapas."); 
            }
            return View(_model);
        }
        [Authorize(Users = "User,Admin")]
        public ActionResult Delete(int id){
            if(Repository.DeleteGuestBookEntry(id))
                return RedirectToAction("Index");
            return View("Error",_model);
            
        }
    }
}
