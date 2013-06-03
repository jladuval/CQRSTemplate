using System.Web.Mvc;
using Base.CQRS.Commands;
using Security.Interfaces.Commands;
using Security.Interfaces.Queries;
using Web.Models.Membership;

namespace Web.Controllers
{
    public class MembershipController : Controller
    {
        private readonly ISecurityUserReader _securityUserReader;

        private readonly IGate _gate;

        public MembershipController(ISecurityUserReader securityUserReader, IGate gate)
        {
            _securityUserReader = securityUserReader;
            _gate = gate;
        }        

        [Authorize]
        public ActionResult LogOff()
        {
            _gate.Dispatch(new LogOffUserCommand());
            return RedirectToAction("Index", "Home");
        }
    }
}
