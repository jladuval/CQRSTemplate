using NHibernate;

namespace Base.Infrastructure.NHibernate
{
    public interface IEntityManager
    {
        ISession CurrentSession { get; }
    }
}