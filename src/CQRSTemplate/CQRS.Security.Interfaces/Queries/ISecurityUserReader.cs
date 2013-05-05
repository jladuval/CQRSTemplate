using CQRS.Security.Interfaces.Presentation;

namespace CQRS.Security.Interfaces.Queries
{
    public interface ISecurityUserReader
    {
        CheckUserCredentialsDto CheckUserCredentials(CheckUserCredentialsQuery query);
        bool UserExists(UserExistsQuery query);
    }
}
