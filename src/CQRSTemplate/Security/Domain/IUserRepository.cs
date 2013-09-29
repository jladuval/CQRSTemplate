namespace Security.Domain
{
    using Base.DDD.Domain.Annotations;

    using Security.Domain.Model;

    [DomainRepository]
    public interface IUserRepository
    {
        void Save(User user);
    }
}
