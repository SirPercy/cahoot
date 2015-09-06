using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using cahoot.Code;
using cahoot.Models;
using cahoot.Models.ViewModels;

namespace cahoot.Controllers
{
    public class CalendarController : BaseController
    {
        readonly ICalendarModel _model;
        public CalendarController()
        {
            _model = new CalendarModel();
        }

        public ActionResult Index(int? newsid, int? monthindex)
        {
            var dateByIndex = DateTime.Now.AddMonths(monthindex ?? 0);
            ViewBag.DateToShow = new DateTime(dateByIndex.Year, dateByIndex.Month, 1);
            var calendarEntries = _model.CalendarEntries;
  
            if (!string.IsNullOrEmpty(Request.QueryString["eventdate"])) {
                var selectedDate = DateTime.Parse(Request.QueryString["eventdate"]);
                _model.TeamCalendarEvents = calendarEntries.Where(e => e.EventDate.ToShortDateString().Equals(selectedDate.ToShortDateString())).ToList();
            }
            else if (newsid != null) {
                _model.TeamCalendarEvents = calendarEntries.Where(e => e.EventId == newsid).ToList();

            }
            else {

                var yesterDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMinutes(-1);
                _model.TeamCalendarEvents = calendarEntries.Where(e => e.EventDate > yesterDay && e.EventDate.Month.Equals(monthindex != null ? dateByIndex.Month : DateTime.Now.Month)).ToList();
                _model.TeamCalendarEvents.RemoveAll(e => e.EventDate.Year != dateByIndex.Year);
            }
           return View(_model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [Authorize(Users="User,Admin")]
        public ActionResult Create()
        {
           return View(_model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Users = "User,Admin")]
        public ActionResult Create(CalendarModel calenderEntryToCreate)
        {
            if (ModelState.IsValid)
            {
                if (base.Repository.CreateCalendarEntry(calenderEntryToCreate))
                    return RedirectToAction("Index");

                ModelState.AddModelError("_FORM", "Något gick fel när posten skulle skapas.");
            }
            return View(_model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [Authorize(Users = "User,Admin")]
        public ActionResult Edit(int id)
        {
            _model.Calendar = base.Repository.GetCalendarEvents(id).ElementAt(0);
            return View(_model);
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(CalendarModel calendarEntryToUpdate)
        {

            if (ModelState.IsValid)
            {
                if (base.Repository.UpdateCalendarEntry(calendarEntryToUpdate))
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError("_FORM", "Något gick fel när posten skulle updateras.");
            return View(_model);
        }
        
        [Authorize(Users = "User,Admin")]
        public ActionResult Delete(int id)
        {
            if (base.Repository.DeleteCalendarEntry(id))
                return RedirectToAction("Index");
            return View("Error", _model);
        }


        //[Authorize(Users = "Admin")]
        [HttpGet]
        public ActionResult Add()
        {
            var model = new MatchSchemeModel();
            model.Teams = Repository.GetTeams().Select(i => new SelectListItem { Text = i.Name, Value = i.Name });

            return View(model);
        }

        //[Authorize(Users = "Admin")]
        [HttpPost]
        public ActionResult ViewAdded(MatchSchemeModel model)
        {
            //var url = "http://bits.swebowl.se/Matches/MatchScheme.aspx?DivisionId=12&SeasonId=2015&LeagueId=1&LevelId=3";
            var extractor = new MatchSchemeExtractor { Team = model.Team, Url = model.Url };
            model.CalendarItems = extractor.Extract();

            if (!model.IsDebug && model.ClearFutrurePosts)
            {
                var calendarItems = Repository.GetCalendarEvents(null);
                foreach (var calendarItem in calendarItems)
                {
                    //if (calendarItem.EventInfo.ToLower().Contains("bk cahoot"))
                    //{
                        Repository.DeleteCalendarEntry(calendarItem.EventId.GetValueOrDefault(0));
                    //}
                }
                foreach (var calendarItem in model.CalendarItems)
                {
                    Repository.CreateCalendarEntry(new CalendarModel { Calendar = calendarItem });
                }
            }
            else if (!model.IsDebug)
            {
                foreach (var calendarItem in model.CalendarItems)
                {
                    Repository.CreateCalendarEntry(new CalendarModel{Calendar = calendarItem});
                }
            }

            return View(model);
        }


    }
}
