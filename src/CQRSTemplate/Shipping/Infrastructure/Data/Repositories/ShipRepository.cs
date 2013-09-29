using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Infrastructure.Data.Repositories
{
    using Base.DDD.Domain.Annotations;
    using Base.DDD.Domain.Support;
    using Domain;
    using global::Infrastructure.NHibernate.Repositories;
    using NHibernate;

    [DomainRepositoryImplementation]
    public class ShipRepository : GenericRepositoryForBaseEntity<Ship>, IShipRepository
    {
        public ShipRepository(ISession session, InjectorHelper injectorHelper)
            : base(session, injectorHelper)
        {
        }
    }
}
