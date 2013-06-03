using System.Collections.Generic;
using CQRS.Base.CQRS.Commands.Attributes;
using CQRS.Base.CQRS.Commands.Handler;
using CQRS.Base.DDD.Application;
using CQRS.Security.Application.Events;
using CQRS.Security.Application.Services;
using CQRS.Security.Interfaces.Application;
using CQRS.Security.Interfaces.Commands;
using VLQR.Base.Infrastructure.EntityFramework.Repositories;
using CQRS.Base.Entities.Models;
using CQRS.Enums;

namespace CQRS.Security.Application.Commands.Handlers
{
    [CommandHandler]
    public class SignUpUserCommandHandler : ICommandHandler<SignUpUserCommand>
    {
        public IRepository<User> UserRepository { get; set; }

        public ICryptoService CryptoService { get; set; }

        public void Handle(SignUpUserCommand command)
        {
            var salt = CryptoService.GenerateSalt();
            var user = new User(command.Email, CryptoService.Hash(command.Password, salt), salt);
            user.Roles = new List<UserRoles> {UserRoles.Moderator};
            user.VerificationCode = CryptoService.GenerateRandomHash();
        }
    }
}