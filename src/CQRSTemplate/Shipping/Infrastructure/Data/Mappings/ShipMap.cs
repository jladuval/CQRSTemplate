namespace Shipping.Infrastructure.Data.Mappings
{
    using System;
    using Domain;
    using FluentNHibernate.Mapping;

    public class ShipMap: ClassMap<Ship>
    {
        public ShipMap()
        {
            Table("Ships");
            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue(Guid.Empty);
            Map(x => x.CreatedDate).Not.Nullable();
            Map(x => x.ModifiedDate).Not.Nullable();
            Map(x => x.Name).Nullable();
        }
    }
}
