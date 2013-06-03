using FluentNHibernate.Mapping;
using Security.Interfaces.Application;

namespace Security.Domain
{
    public class UserMap : SubclassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Map(x => x.Name);
            Map(x => x.Email).Unique();
            Map(x => x.Password);
            Map(x => x.Salt);
            Map(x => x.IsVerified);
            Map(x => x.VerificationCode);
            HasMany(x => x.Roles)
                .Table("UserRoles")
                .Element("Roles", e => e.Type<NHibernate.Type.EnumStringType<UserRoles>>())
                .AsSet();
        }
    }
}
