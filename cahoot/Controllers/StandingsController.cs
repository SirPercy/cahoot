using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using cahoot.Code;
using cahoot.Models;
using cahoot.Models.ViewModels;
using System.Text;
using System.Web.Caching;
using cahoot.Models.ViewModels;

namespace cahoot.Controllers
{
    public class StandingsController : BaseController
    {
        readonly IStandingsModel _model;
        public StandingsController()
        {
            _model = new StandingsModel();
        }

        public ActionResult Index()
        {
            _model.StandingLinks = XmlUtil.GetStatItems(Constants.XmlDataType.Standings);
            _model.StatisticsLinks = XmlUtil.GetStatItems(Constants.XmlDataType.Statistics);

            return View(_model);
        }

        [Authorize(Users = "User,Admin")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(Constants.XmlDataType type)
        {
            ViewBag.Type = type;
            if(type == Constants.XmlDataType.Standings){
                _model.StandingLinks = XmlUtil.GetStatItems(Constants.XmlDataType.Standings);
            }
            else
                _model.StatisticsLinks = XmlUtil.GetStatItems(Constants.XmlDataType.Statistics);

            return View(_model);
        }

        [Authorize(Users = "User,Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(StandingsModel model2Save, Constants.XmlDataType type)
        {
            if(type == Constants.XmlDataType.Standings)          
                XmlUtil.SaveStatItems(stat => stat.MenuItem = model2Save.StandingLinks, type);
            else
                XmlUtil.SaveStatItems(stat => stat.MenuItem = model2Save.StatisticsLinks, type);

            return RedirectToAction("Index");

        }

        [Authorize(Users = "User,Admin")]
        public ActionResult Delete(string name, Constants.XmlDataType type)
        {
            List<MenuItem> items = XmlUtil.GetStatItems(type);
            var matchedItem = items.Find(item => item.Name == name);
            var index = items.IndexOf(matchedItem);

            items.RemoveAt(index);
            XmlUtil.SaveStatItems(stat => stat.MenuItem = items, type);
            return RedirectToAction("Index");

        }

        [Authorize(Users = "User,Admin")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            return View(_model);
        }

        [Authorize(Users = "User,Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(StandingsModel model2Save, Constants.XmlDataType type)
        {
            List<MenuItem> items = XmlUtil.GetStatItems(type);
            items.Add(new MenuItem { Name = model2Save.StandingItem.Name, Link = model2Save.StandingItem.Link });
            XmlUtil.SaveStatItems(stat => stat.MenuItem = items, type);
            return RedirectToAction("Index");

        }

        public string MenuItems()
        {
            var cachedHtml = HttpRuntime.Cache["statitems"] as string;
            if (cachedHtml == null)
            {
                var html = new StringBuilder();
                html.Append("<ul class=\"dropdown-menu\" role=\"menu\">");

                var statitems = XmlUtil.GetStatItems(Constants.XmlDataType.Statistics);
                var standingItems = XmlUtil.GetStatItems(Constants.XmlDataType.Standings);
                statitems.AddRange(standingItems);



                if (statitems.Count == 0 && (User.Identity.Name != "Admin" || User.Identity.Name == "User"))
                    return string.Empty;

                foreach (var item in statitems)
                {
                    html.Append(string.Format("<li><a taget=\"_blank\" href=\"{0}\">{1}</a></li>", item.Link, item.Name));
                }

                if (User != null && (User.Identity.Name == "Admin" || User.Identity.Name == "User"))
                    html.Append(string.Format("<li><a href=\"{0}\">{1}</a></li>", Url.Action("Index"), "Hantera länkar"));

                html.Append("</ul>");
                html.Append("</div>");
                cachedHtml = html.ToString();
                HttpRuntime.Cache.Insert("statitems", html.ToString(), null, DateTime.Now.AddHours(4), TimeSpan.Zero,
                                         CacheItemPriority.Normal, null);
            }

            return cachedHtml;
        
        }
    }
}
