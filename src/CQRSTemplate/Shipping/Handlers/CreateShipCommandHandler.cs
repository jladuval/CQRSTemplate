namespace Shipping.Handlers
{
    using System;
    using System.Threading;
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Base.StorageQueue;
    using Domain;
    using Infrastructure.Data.Repositories;
    using Interfaces.Commands;

    [CommandHandler]
    public class CreateShipCommandHandler : ICommandHandler<CreateShipCommand>
    {
        private readonly IShipRepository _shipRepository;

        public CreateShipCommandHandler(IShipRepository shipRepository)
        {
            _shipRepository = shipRepository;
        }

        public void Handle(CreateShipCommand command)
        {
            ShipQueue.PushEvent("Building Hull for  " + command.Name);
            Thread.Sleep(new TimeSpan(0,0,5));
            ShipQueue.PushEvent("Finding Guests For " + command.Name);
            Thread.Sleep(new TimeSpan(0, 0, 5));
            ShipQueue.PushEvent("Emailing Passengers For  " + command.Name);
            Thread.Sleep(new TimeSpan(0, 0, 5));
            var ship = new Ship();
            ship.ChangeName(command.Name);
            _shipRepository.Save(ship);
            ShipQueue.PushShip(command.Name, ship.Id.ToString());
        }
    }
}
