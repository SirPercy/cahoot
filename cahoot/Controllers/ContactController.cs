using System.Web.Mvc;
using cahoot.Models.ViewModels;

namespace cahoot.Controllers
{
    public class ContactController : BaseController
    {
        //
        // GET: /Contact/
        private readonly IContactModel _model;

        public ContactController()
        {
            _model = new ContactModel();
        }

        public ActionResult Index()
        {
            return View(_model);
        }

    }
}
