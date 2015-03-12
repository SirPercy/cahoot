using System.Collections.Generic;
using System.Web.Mvc;
using cahoot.Code;
using cahoot.Models;
using cahoot.Models.ViewModels;

namespace cahoot.Controllers
{
    public class SponsorsController : BaseController
    {
        readonly ISponsorsModel _model;
        public SponsorsController()
        {

            _model = new SponsorsModel();
        }

        public ActionResult Index()
        {
            return View(_model);
        }

        [Authorize(Users="Admin")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            return View(_model);
        }

        [Authorize(Users = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(SponsorsModel model2Save)
        {
            var items = XmlUtil.GetSponsorItems();
            items.Add(new Sponsor { Name = model2Save.SponsorItem.Name, Link = model2Save.SponsorItem.Link, LogotypeUrl = model2Save.SponsorItem.LogotypeUrl, TeaserText = model2Save.SponsorItem.TeaserText });
            XmlUtil.SaveSponsorItems(s => s.SponsorEntires = items);
            return RedirectToAction("Index");

        }

        [Authorize(Users = "Admin")]
        public ActionResult Delete(string name)
        {
            List<Sponsor> items = XmlUtil.GetSponsorItems();
            var matchedItem = items.Find(item => item.Name == name);
            var index = items.IndexOf(matchedItem);

            items.RemoveAt(index);
            XmlUtil.SaveSponsorItems(s => s.SponsorEntires = items);
            return RedirectToAction("Index");

        }
    }
}
