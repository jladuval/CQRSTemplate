namespace Security.Interfaces.Domain.Readers
{
    using System;
    using System.Collections.Generic;

    using Security.Interfaces.Common;

    public class CheckUserCredentialsDto
    {
        public Guid UserId { get; set; }

        public IList<UserRoles> Roles { get; set; }
    }
}
