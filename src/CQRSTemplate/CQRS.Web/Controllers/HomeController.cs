using System.Web.Mvc;
using CQRS.Base.CQRS.Commands;
using CQRS.Security.Interfaces.Queries;

namespace CQRS.Web.Controllers
{
    using CQRS.Infrastructure.Logging.Interfaces;

    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        public IGate Gate { get; set; }


        public ISecurityUserReader SecurityUserReader { get; set; }

        public ActionResult Index()
        {
            _logger.Trace("Index action of Home controller is called");
            return View();
        }
    }
}
