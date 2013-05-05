using System;
using System.Collections.Generic;
using CQRS.Base.CQRS.Commands.Attributes;
using CQRS.Security.Interfaces.Application;

namespace CQRS.Security.Interfaces.Commands
{
    [Command]
    public class LogInUserCommand
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public bool RememberMe { get; set; }

        public IEnumerable<UserRoles> Roles { get; set; } 
    }
}
