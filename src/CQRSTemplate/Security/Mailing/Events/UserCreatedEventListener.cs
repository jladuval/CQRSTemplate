using System.Collections.Generic;
using CQRS.Base.DDD.Infrastructure.Events;
using CQRS.Base.Mailing;
using CQRS.Security.Application.Events;
using CQRS.Security.Infrastructure.Mailing.MailData;

namespace CQRS.Security.Infrastructure.Mailing.Events
{
    [EventListeners]
    public class UserCreatedEventListener : IEventListener<UserCreatedEvent>
    {
        public IMailMessageFactory MailMessageFactory { get; set; }

        public IMailer Mailer { get; set; }

        [EventListener(IsAsync = true)]
        public void Handle(UserCreatedEvent @event)
        {
            var message = MailMessageFactory.Create(new VerificationEmailData(@event.UserId, @event.VerificationToken));
            message.TemplateName = "verifyEmail";
            message.Recipients = new List<string>
                {
                    @event.Email
                };
            Mailer.Send(message);
        }
    }
}
