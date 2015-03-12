using System.Linq;
using System.Web.Mvc;
using cahoot.Models;
using cahoot.Models.ViewModels;
using cahoot.Models.ViewModels;
using PagedList;

namespace cahoot.Controllers
{
    public class NewsController : BaseController
    {
        readonly INewsModel _model;

        public NewsController()
        {
            _model = new NewsModel();
        }

        public ActionResult Index(int? page)
        {
            var pageIndex = page ?? 1;
            _model.NewsEntries = Repository.GetNewsEntries(null).Where(n => !n.IsInternal).ToPagedList(pageIndex, 5);
            return View(_model);
        }

        public ActionResult NewsEntry(int id)
        {
             _model.NewsEntries = Repository.GetNewsEntries(id).ToList().ToPagedList(1, 1);
            return View(_model);
        }
        
        [Authorize(Users = "Admin")]
        public ActionResult Create()
        {
             return View(_model);
        } 

        //
        // POST: /News/Create
        [Authorize(Users="Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(NewsModel newsToCreate)
        {
            if (ModelState.IsValid)
            {
                if(Repository.CreateNewsEntry(newsToCreate))
                    return RedirectToAction("Index");
                
                ModelState.AddModelError("_FORM", "Något gick fel när posten skulle skapas.");
            }
            return View(_model);
        }
        
        //
        // GET: /News/Edit/5
        [Authorize(Users = "Admin")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            _model.News = Repository.GetNewsEntries(id).ElementAt(0);
            return View(_model);
        }

        //
        // POST: /News/Edit/5
        [Authorize(Users = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(NewsModel newsToUpdate)
        {

            if (ModelState.IsValid)
            {
                if (Repository.UpdateNewsEntry(newsToUpdate))
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError("_FORM", "Något gick fel när posten skulle updateras.");
            return View(_model);
        }

        //
        // POST: /News/Delete/5
        [Authorize(Users = "Admin")]
        public ActionResult Delete(int id)
        {
            if (Repository.DeleteNewsEntry(id))
                return RedirectToAction("Index");
            return View("Error", _model);
        }
       
    }
}
