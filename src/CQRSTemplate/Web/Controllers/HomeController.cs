namespace Web.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;

    using Base.CQRS.Commands;
    using Hubs;
    using Security.Interfaces.Commands;
    using Security.Interfaces.Domain.Readers;
    using Shipping.Interfaces.Commands;

    public class HomeController : Controller
    {
        private readonly ISecurityUserReader _securityUserReader;

        private readonly IGate _gate;

        public HomeController(ISecurityUserReader securityUserReader, IGate gate)
        {
            _securityUserReader = securityUserReader;
            _gate = gate;
        }        

        public ActionResult Index()
        {
            var username = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            _gate.Dispatch(new SignUpUserCommand(username, password));
            _securityUserReader.CheckUserCredentials(
                new CheckUserCredentialsQuery { Email = username, Password = password });
            return View();
        }

        [HttpPost]
        public ActionResult CreateCruiseShip(string name)
        {
            EventHub.Send("Creating " + name);
            _gate.Dispatch(new CreateShipCommand(name, Guid.NewGuid()));
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }
    }
}
