using Base.DDD.Domain.Annotations;
using Base.Infrastructure.NHibernate.Repositories;
using Security.Domain;

namespace Security.Infrastructure.Repositories
{
    [DomainRepositoryImplementation]
    public class UserRepository : GenericRepositoryForBaseEntity<User>, IUserRepository
    {
    }
}
