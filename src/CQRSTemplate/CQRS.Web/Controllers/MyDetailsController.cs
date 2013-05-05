using System.Collections.Generic;
using System.Web.Mvc;
using CQRS.Web.Models.MyDetails;

namespace CQRS.Web.Controllers
{
    public class MyDetailsController : Controller
    {
        public ActionResult Index()
        {
            return View(new MyDetailsModel { Tags = new List<string>{"ducks", "Ham", "Cattle"}});
        }

    }
}
