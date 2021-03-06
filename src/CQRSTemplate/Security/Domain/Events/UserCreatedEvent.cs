﻿namespace Security.Domain.Events
{
    using System;

    using Base.DDD.Domain;

    public class UserCreatedEvent : IDomainEvent
    {
        public UserCreatedEvent(Guid userId, string email, string verificationToken)
        {
            UserId = userId;
            Email = email;
            VerificationToken = verificationToken;
        }

        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string VerificationToken { get; set; }
    }
}
