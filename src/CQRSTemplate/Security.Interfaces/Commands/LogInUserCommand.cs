namespace Security.Interfaces.Commands
{
    using System;
    using System.Collections.Generic;

    using Base.CQRS.Commands.Attributes;

    using Security.Interfaces.Common;

    [Command]
    public class LogInUserCommand
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public bool RememberMe { get; set; }

        public IEnumerable<UserRoles> Roles { get; set; } 
    }
}
