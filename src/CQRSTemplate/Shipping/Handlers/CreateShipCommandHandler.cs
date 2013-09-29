namespace Shipping.Handlers
{
    using System;
    using System.Threading;
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Base.StorageQueue;
    using Interfaces.Commands;

    [CommandHandler]
    public class CreateShipCommandHandler : ICommandHandler<CreateShipCommand>
    {
        public void Handle(CreateShipCommand command)
        {
            ShipQueue.PushEvent("Building Hull for  " + command.Name);
            Thread.Sleep(new TimeSpan(0,0,5));
            ShipQueue.PushEvent("Finding Guests For " + command.Name);
            Thread.Sleep(new TimeSpan(0, 0, 5));
            ShipQueue.PushEvent("Emailing Passengers For  " + command.Name);
            Thread.Sleep(new TimeSpan(0, 0, 5));
            ShipQueue.PushEvent(command.Name + " created, id is " + Guid.NewGuid());
        }
    }
}
