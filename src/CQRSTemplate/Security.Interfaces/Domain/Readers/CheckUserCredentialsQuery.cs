namespace Security.Interfaces.Domain.Readers
{
    public class CheckUserCredentialsQuery
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
