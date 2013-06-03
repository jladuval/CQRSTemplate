using System.Web;
using CQRS.Base.DDD.Domain.Annotations;
using CQRS.Security.Interfaces.Application;
using CQRS.Security.Interfaces.Application.Impersonation;
using CQRS.Security.Interfaces.Application.Services;

namespace CQRS.Security.Application.Impersonation
{
    [DomainService]
    public class DefaultIdentityProvider : IIdentityProvider
    {
        private readonly IAuthCookieService _authCookieService;

        public DefaultIdentityProvider(IAuthCookieService authCookieService)
        {
            _authCookieService = authCookieService;
        }

        public void ProcessIdentity(HttpApplication application)
        {
            var authCookiesPayload = _authCookieService.GetUserCookiesInfo();
            if (authCookiesPayload != null)
            {
                ProcessAuthCookiesPayload(application, authCookiesPayload);
            }
        }

        protected virtual void ProcessAuthCookiesPayload(HttpApplication application, CustomPrincipalInfo principalInfo)
        {
            application.Context.User = (CustomPrincipal)principalInfo;
        }
    }
}
