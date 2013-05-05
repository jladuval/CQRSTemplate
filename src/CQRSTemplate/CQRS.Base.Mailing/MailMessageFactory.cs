using CQRS.Base.DDD.Domain.Annotations;

namespace CQRS.Base.Mailing
{
    [DomainFactory]
    public class MailMessageFactory : IMailMessageFactory
    {
        public IMailMessage<T> Create<T>(T model) where T : class
        {
            return new MailMessage<T>(model);
        }

        public IMailMessage<T> Create<T>() where T : class
        {
            return new MailMessage<T>();
        }
    }
}
