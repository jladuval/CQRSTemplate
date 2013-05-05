using CQRS.Base.DDD.Domain.Annotations;
using CQRS.Base.Infrastructure.NHibernate.Repositories;
using CQRS.Security.Domain;

namespace CQRS.Security.Infrastructure.Repositories
{
    [DomainRepositoryImplementation]
    public class UserRepository : GenericRepositoryForBaseEntity<User>, IUserRepository
    {
    }
}
