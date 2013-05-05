using System.Collections.Generic;
using CQRS.Base.CQRS.Commands.Attributes;
using CQRS.Base.CQRS.Commands.Handler;
using CQRS.Base.DDD.Application;
using CQRS.Security.Application.Events;
using CQRS.Security.Application.Services;
using CQRS.Security.Domain;
using CQRS.Security.Interfaces.Application;
using CQRS.Security.Interfaces.Commands;

namespace CQRS.Security.Application.Commands.Handlers
{
    [CommandHandler]
    public class SignUpUserCommandHandler : ICommandHandler<SignUpUserCommand>
    {
        public IUserRepository UserRepository { get; set; }

        public ICryptoService CryptoService { get; set; }

        public UserFactory UserFactory { get; set; }

        public void Handle(SignUpUserCommand command)
        {
            var salt = CryptoService.GenerateSalt();
            var user = UserFactory.CreateUser(command.Email, CryptoService.Hash(command.Password, salt), salt);
            user.Roles = new List<UserRoles> {UserRoles.Moderator};
            user.VerificationCode = CryptoService.GenerateRandomHash();
            UserRepository.Save(user);
            user.FinishedSignUp();
        }
    }
}