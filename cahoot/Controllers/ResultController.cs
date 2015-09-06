using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using cahoot.Models.ViewModels;
using cahoot.Models;
using cahoot.Models.ViewModels;

namespace cahoot.Controllers
{
    //public delegate List<MatchData> MatchDataCaller();
    //public delegate List<Result> HighResultCaller(string type);

    public class ResultController : BaseController
    {
        readonly IResultModel _model;
        
        public ResultController()
        {
            _model = new ResultModel();
        }
        public ActionResult Index()
        {

            _model.MatchData = Repository.GetMatchData();
            _model.HigestResultMen = Repository.GetScoring("men");
            _model.HigestResultWomen = Repository.GetScoring("women");
            _model.HigestResult = Repository.GetScoring("");

            return View(_model);
        }
        
        [Authorize(Users = "Admin")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            ViewBag.Teams = Repository.GetTeams();
            List<Member> members = Repository.GetMembers(null).OrderBy(i => i.Name).ToList();
            members.Insert(0, new Member{MemberID=0, Name=""});
            ViewBag.Members = members;
            return View(_model);
        }
        
        [Authorize(Users = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(ResultModel matchToCreate, FormCollection formData)
        {
            if (ModelState.IsValid)
            {
                List<ScoringReport> scoringReport = new List<ScoringReport>(5);
                if (!string.IsNullOrEmpty(formData["BestResult"]))
                    scoringReport.Add(new ScoringReport { MatchId = 0, MemberId = int.Parse(formData["BestPlayer"]), Result = int.Parse(formData["BestResult"]) });
                if (!string.IsNullOrEmpty(formData["SecondBestResult"]))
                    scoringReport.Add(new ScoringReport { MatchId = 0, MemberId = int.Parse(formData["SecondBestPlayer"]), Result = int.Parse(formData["SecondBestResult"]) });
                if (!string.IsNullOrEmpty(formData["ThirdBestResult"]))
                    scoringReport.Add(new ScoringReport { MatchId = 0, MemberId = int.Parse(formData["ThirdBestPlayer"]), Result = int.Parse(formData["ThirdBestResult"]) });
                if (!string.IsNullOrEmpty(formData["FourthBestResult"]))
                    scoringReport.Add(new ScoringReport { MatchId = 0, MemberId = int.Parse(formData["FourthBestPlayer"]), Result = int.Parse(formData["FourthBestResult"]) });
                if (!string.IsNullOrEmpty(formData["FifthBestResult"]))
                    scoringReport.Add(new ScoringReport { MatchId = 0, MemberId = int.Parse(formData["FifthBestPlayer"]), Result = int.Parse(formData["FifthBestResult"]) });

                matchToCreate.Match.Results = scoringReport;
                if (Repository.InsertMatch(matchToCreate))
                    return RedirectToAction("Index");
                ModelState.AddModelError("_FORM", "Något gick fel när posten skulle skapas.");
            }
            ViewBag.Teams = Repository.GetTeams();
            ViewBag.Members = Repository.GetMembers(null).OrderBy(i => i.Name).ToList();
            return View(_model);
        }

        //
        // Get: /Result/Delete/5
        [Authorize(Users = "Admin")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Delete(int matchid)
        {
            if (Repository.DeleteMatchEntry(matchid))
                return RedirectToAction("Index");
            return View("Error", _model);
        }

      }
}
