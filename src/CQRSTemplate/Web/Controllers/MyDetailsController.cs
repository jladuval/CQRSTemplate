using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models.MyDetails;

namespace Web.Controllers
{
    public class MyDetailsController : Controller
    {
        public ActionResult Index()
        {
            return View(new MyDetailsModel { Tags = new List<string>{"ducks", "Ham", "Cattle"}});
        }

    }
}
