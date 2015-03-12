using System.Linq;
using System.Web;
using System.Web.Mvc;
using cahoot.Models.ViewModels;
using System.Web.Security;

namespace cahoot.Controllers
{
    public class HomeController : BaseController
    {
        readonly IHomeModel _model;
       
        public HomeController()
        {
            _model = new HomeModel();
        }

        public ActionResult Index()
        {
            var news = Repository.GetNewsEntries(null).Where(n => !n.IsInternal);
            news = news.Concat(
                _model.GuestBookPosts.Select(
                    i =>
                    new Models.News
                        {
                            Date = i.Date,
                            Heading = string.Format("Gästboksinlägg av {0}", i.Name),
                            Text = i.Text,
                            Id = i.EntryId,
                            Writer = i.Name
                        }));
            _model.NewsEntries = news.ToList().OrderByDescending(i => i.Date).Take(7).ToList();
            return View(_model);
        }

    }
}
