using System.Linq;
using System.Web.Mvc;
using cahoot.Models.ViewModels;
using cahoot.Models.ViewModels;
using PagedList;

namespace cahoot.Controllers
{
    public class TeamController : BaseController
    {
        readonly ITeamModel _model;
        public TeamController()
        {
            _model = new TeamModel();
        }


        public ActionResult Index(int? page) {
            var pageIndex = page ?? 1;
            _model.TeamInfoEntries = Repository.GetNewsEntries(null).Where(n => n.IsInternal).ToList().ToPagedList(pageIndex, 4);
            return View(_model);
        
        }

    }
}
