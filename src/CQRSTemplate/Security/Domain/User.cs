namespace Security.Domain
{
    using System.Collections.Generic;

    using Base.DDD.Domain;
    using Security.Application.Events;
    using Security.Interfaces.Application;

    public class User : AggregateRoot
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Salt { get; set; }

        public string VerificationCode { get; set; }

        public bool IsVerified { get; set; }

        public IEnumerable<UserRoles> Roles { get; set; }

        public User()
        {
        }

        public User(string email, string password, string salt)
        {
            Email = email;
            Password = password;
            Salt = salt;
        }

        public void FinishedSignUp()
        {
            EventPublisher.Publish(new UserCreatedEvent(Id, Email, VerificationCode));
        }
    }
}
