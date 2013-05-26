using System.Web.Mvc;
using CQRS.Base.CQRS.Commands;
using CQRS.Security.Interfaces.Queries;

namespace CQRS.Web.Controllers
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
