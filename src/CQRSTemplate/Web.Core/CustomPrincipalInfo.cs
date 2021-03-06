﻿namespace Web.Core
{
    using System;
    using System.Collections.Generic;

    public class CustomPrincipalInfo
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public IList<string> Roles { get; set; }

        public static explicit operator CustomPrincipal(CustomPrincipalInfo info)
        {
            if (null == info) return null;

            var user = new CustomPrincipal(info.Email)
            {
                UserId = info.UserId,
                Email = info.Email,
                Roles = info.Roles
            };
            return user;
        }

        public static explicit operator CustomPrincipalInfo(CustomPrincipal info)
        {
            if (null == info)
            {
                return null;
            }

            var user = new CustomPrincipalInfo
            {
                UserId = info.UserId,
                Email = info.Identity.Name,
                Roles = info.Roles
            };

            return user;
        }
    }
}
