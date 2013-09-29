namespace Shipping.Interfaces.Readers
{
    using System.Collections;
    using System.Collections.Generic;
    using Presentation;

    public interface IShipReader
    {
        ICollection<ShipDto> GetAllShips();
    }
}
