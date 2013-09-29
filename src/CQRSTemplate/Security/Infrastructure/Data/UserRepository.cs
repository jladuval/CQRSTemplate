namespace Security.Infrastructure.Data
{
    using Base.DDD.Domain.Annotations;
    using Base.DDD.Domain.Support;

    using global::Infrastructure.NHibernate.Repositories;

    using NHibernate;

    using Security.Domain;
    using Security.Domain.Model;

    [DomainRepositoryImplementation]
    public class UserRepository : GenericRepositoryForBaseEntity<User>, IUserRepository
    {
        public UserRepository(ISession session, InjectorHelper injectorHelper)
            : base(session, injectorHelper)
        {
        }
    }
}
