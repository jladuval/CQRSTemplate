namespace Security.Interfaces.Commands
{
    using Base.CQRS.Commands.Attributes;

    [Command]
    public class SignUpUserCommand
    {
        public SignUpUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; private set; }

        public string Password { get; private set; }
    }
}
