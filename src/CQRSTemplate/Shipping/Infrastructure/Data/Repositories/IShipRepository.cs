namespace Shipping.Infrastructure.Data.Repositories
{
    using Base.DDD.Domain.Annotations;
    using Domain;

    [DomainRepository]
    public interface IShipRepository
    {
        void Save(Ship ship);
    }
}
