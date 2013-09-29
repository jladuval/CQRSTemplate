namespace Shipping.Domain.Readers
{
    using System.Collections.Generic;
    using System.Linq;
    using Base.CQRS.Query.Attributes;
    using Interfaces.Presentation;
    using Interfaces.Readers;
    using NHibernate;
    using NHibernate.Linq;

    [Reader]
    public class ShipReader : IShipReader
    {
        private readonly ISession _session;

        public ShipReader(ISession session)
        {
            _session = session;
        }

        public ICollection<ShipDto> GetAllShips()
        {
            return _session.Query<Ship>().Select(x => new ShipDto
            {
                Id = x.Id.ToString(),
                Name = x.Name.ToString()
            }).ToList();
        }
    }
}
