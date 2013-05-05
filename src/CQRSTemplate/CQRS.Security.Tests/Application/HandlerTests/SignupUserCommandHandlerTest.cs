using Moq;
using CQRS.Security.Application.Commands.Handlers;
using CQRS.Security.Domain;
using CQRS.Security.Interfaces.Commands;
using Xunit;

namespace CQRS.Security.Tests.Application.HandlerTests
{
    public class SignupUserCommandHandlerTest
    {
        readonly SignUpUserCommandHandler _handler = new SignUpUserCommandHandler();

        public SignupUserCommandHandlerTest()
        {
            _handler.UserRepository = new Mock<IUserRepository>().SetupAllProperties().Object;
        }

        [Fact]
        public void SignupCorrect()
        {
            Assert.DoesNotThrow(() => _handler.Handle(new SignUpUserCommand("a@a.c", "password")));
        }
    }
}
