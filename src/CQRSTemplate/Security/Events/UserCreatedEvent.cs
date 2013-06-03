using System;
using CQRS.Base.DDD.Domain;

namespace CQRS.Security.Application.Events
{
    public class UserCreatedEvent : IDomainEvent
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string VerificationToken { get; set; }

        public UserCreatedEvent(Guid userId, string email, string verificationToken)
        {
            UserId = userId;
            Email = email;
            VerificationToken = verificationToken;
        }
    }
}
