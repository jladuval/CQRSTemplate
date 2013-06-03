using System.Web.Mvc;
using Base.CQRS.Commands;
using Security.Interfaces.Queries;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISecurityUserReader SecurityUserReader;

        private readonly IGate Gate;

        public HomeController(ISecurityUserReader securityUserReader, IGate gate)
        {
            SecurityUserReader = securityUserReader;
            Gate = gate;
        }        

        public ActionResult Index()
        {
            return View();
        }
    }
}
