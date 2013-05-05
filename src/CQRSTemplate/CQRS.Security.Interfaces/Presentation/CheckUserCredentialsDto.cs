using System;
using System.Collections.Generic;
using CQRS.Security.Interfaces.Application;

namespace CQRS.Security.Interfaces.Presentation
{
    public class CheckUserCredentialsDto
    {
        public Guid UserId { get; set; }
        public IEnumerable<UserRoles> Roles { get; set; }
    }
}
