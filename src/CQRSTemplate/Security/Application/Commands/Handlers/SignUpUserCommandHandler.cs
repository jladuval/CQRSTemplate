using System.Collections.Generic;
using Base.CQRS.Commands.Attributes;
using Base.CQRS.Commands.Handler;
using Base.DDD.Application;
using Security.Application.Events;
using Security.Application.Services;
using Security.Domain;
using Security.Interfaces.Application;
using Security.Interfaces.Commands;

namespace Security.Application.Commands.Handlers
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