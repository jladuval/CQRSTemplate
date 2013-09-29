namespace Shipping.Handlers
{
    using Base.CQRS.Commands.Attributes;
    using Base.CQRS.Commands.Handler;
    using Base.StorageQueue;
    using Interfaces.Commands;

    [CommandHandler]
    public class CreateShipCommandHandler : ICommandHandler<CreateShipCommand>
    {
        public void Handle(CreateShipCommand command)
        {
            ShipQueue.Push("Verifying " + command.Name);
        }
    }
}
