using System.Linq;
using CQRS.Base.CQRS.Query.Attributes;
using CQRS.Base.Infrastructure.EntityFramework;
using CQRS.Security.Application.Services;
using CQRS.Security.Interfaces.Presentation;
using CQRS.Security.Interfaces.Queries;
using CQRS.Base.Entities.Models;

namespace CQRS.Security.Application.Readers
{
    [Reader]
    public class UserReader : ISecurityUserReader
    {
        public IEntityManager EntityManager { get; set; }

        public ICryptoService CryptoService { get; set; }

        public CheckUserCredentialsDto CheckUserCredentials(CheckUserCredentialsQuery query)
        {
            var user = EntityManager.CurrentContext.Set<User>().Where(x => x.Email == query.Email).FirstOrDefault();
            return user != null
                   && CryptoService.CheckPassword(user.Password, query.Password, user.Salt)
                       ? new CheckUserCredentialsDto
                           {
                               UserId = user.Id,
                               Roles = user.Roles
                           }
                       : null;
        }
        
        public bool UserExists(UserExistsQuery query) {
            return EntityManager.CurrentContext.Set<User>().FirstOrDefault(x => x.Email == query.Email) != null;
        }
    }
}
