using System.Linq;
using NHibernate.Linq;
using Base.CQRS.Query.Attributes;
using Base.Infrastructure.NHibernate;
using Security.Application.Services;
using Security.Domain;
using Security.Interfaces.Presentation;
using Security.Interfaces.Queries;

namespace Security.Application.Readers
{
    [Reader]
    public class UserReader : ISecurityUserReader
    {
        public IEntityManager EntityManager { get; set; }

        public ICryptoService CryptoService { get; set; }

        public CheckUserCredentialsDto CheckUserCredentials(CheckUserCredentialsQuery query)
        {
            var user = EntityManager.CurrentSession.Query<User>().FirstOrDefault(x => x.Email == query.Email);
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
        	return EntityManager.CurrentSession.Query<User>().FirstOrDefault(x => x.Email == query.Email) != null;
        }
    }
}
