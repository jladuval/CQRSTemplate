using System.Web;
using System.Web.Security;
using CQRS.Base.CQRS.Commands.Handler;
using CQRS.Security.Interfaces.Commands;

namespace CQRS.Security.Application.Commands.Handlers
{
    public class LogOffUserCommandHandler : ICommandHandler<LogOffUserCommand>
    {
        public void Handle(LogOffUserCommand command)
        {
            HttpContext.Current.Session["IsLoggedIn"] = false;
            FormsAuthentication.SignOut();
        }
    }
}
