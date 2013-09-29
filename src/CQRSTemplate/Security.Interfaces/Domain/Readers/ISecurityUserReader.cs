namespace Security.Interfaces.Domain.Readers
{
    public interface ISecurityUserReader
    {
        // CheckUserCredentialsQuery - request, CheckUserCredentialsDto - response
        CheckUserCredentialsDto CheckUserCredentials(CheckUserCredentialsQuery query);
        
        bool UserExists(UserExistsQuery query);
    }
}
