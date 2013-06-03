using NHibernate;

namespace Base.Infrastructure.NHibernate
{
    public interface IPerRequestSessionFactory
    {
        ISession Create();
    }
}