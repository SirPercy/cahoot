using System.Web.Mvc;
using cahoot.Models.Repository;

namespace cahoot.Controllers
{
    public abstract class BaseController : Controller
    {

        private readonly IRepository _repository = new Repository();
        public IRepository Repository
        {
            get { return _repository; }
        }


    }
}