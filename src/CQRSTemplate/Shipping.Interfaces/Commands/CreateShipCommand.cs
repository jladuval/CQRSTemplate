namespace Shipping.Interfaces.Commands
{
    using System;

    public class CreateShipCommand
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }

        public CreateShipCommand(string name, Guid userId)
        {
            Name = name;
            UserId = userId;
        }
    }
}
