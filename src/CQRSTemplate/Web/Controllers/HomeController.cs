namespace Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Base.CQRS.Commands;
    using Hubs;
    using Models.Home;
    using Security.Interfaces.Commands;
    using Security.Interfaces.Domain.Readers;
    using Shipping.Interfaces.Commands;
    using Shipping.Interfaces.Readers;

    public class HomeController : Controller
    {
        private readonly ISecurityUserReader _securityUserReader;

        private readonly IGate _gate;
        private readonly IShipReader _shipReader;

        public HomeController(ISecurityUserReader securityUserReader, IGate gate, IShipReader shipReader)
        {
            _securityUserReader = securityUserReader;
            _gate = gate;
            _shipReader = shipReader;
        }        

        public ActionResult Index()
        {
            var username = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            _gate.Dispatch(new SignUpUserCommand(username, password));
            _securityUserReader.CheckUserCredentials(
                new CheckUserCredentialsQuery { Email = username, Password = password });
            var model = new HomeModel
            {
                Ships = _shipReader.GetAllShips().Select(x => new ShipModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };
            return View(model);
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
