using CQRS.Base.DDD.Domain.Annotations;

namespace CQRS.Security.Domain
{
    [DomainRepository]
    public interface IUserRepository
    {
        void Save(User user);
    }
}
