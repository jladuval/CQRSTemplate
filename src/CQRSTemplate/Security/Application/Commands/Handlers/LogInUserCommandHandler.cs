﻿using System;
using System.Web;
using Base.CQRS.Commands.Attributes;
using Base.CQRS.Commands.Handler;
using Security.Application.Extensions;
using Security.Interfaces.Application;
using Security.Interfaces.Commands;

namespace Security.Application.Commands.Handlers
{
    [CommandHandler]
    public class LogInUserCommandHandler : ICommandHandler<LogInUserCommand>
    {
        private const int Timeout = 30;

        public void Handle(LogInUserCommand command)
        {
            var info = new CustomPrincipalInfo
                {
                    Email = command.Email,
                    UserId = command.UserId,
                    Roles = command.Roles
                };
            var cookie = info.CreateAuthenticationCookie(DateTime.Now, Timeout, command.RememberMe);
            HttpContext.Current.Response.Cookies.Add(cookie);
            HttpContext.Current.Session["IsLoggedIn"] = true;
        }
    }
}
